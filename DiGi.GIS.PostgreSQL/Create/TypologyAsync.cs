using DiGi.GIS.PostgreSQL.Classes;
using Npgsql;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        /// <summary>
        /// Asynchronously classifies the building data of the given county partitions into a typology tree, grouping it by the chained columns of a column typology filter.
        /// <para>The partitions to read are named explicitly and never defaulted to all of them. A county identifier addresses one polygon part rather than a county - there are 406 parts for 380 counties - and the table holds tens of thousands of rows per part, so classifying the whole country is millions of rows in memory rather than a larger query. Grouping by county name rather than county identifier is what re-merges the parts of one county into a single node.</para>
        /// <para>Only the columns the chain needs are projected, plus the reference and county identifier the pull adds itself. One connection is opened for the whole run and every partition is paged over it, because the overload opening its own connection would open one per page.</para>
        /// <para>Rows are classified by <see cref="GIS.Create.Typology(Core.IO.Table.Classes.Table, Typology.Classes.ColumnTypologyFilter{Core.IO.Table.Classes.Column}, Core.IO.Table.Classes.Column, Typology.Classes.TypologyItem, bool)"/>, whose remarks describe which rows a level excludes.</para>
        /// </summary>
        /// <param name="buildingDataPostgreSQLConverter">The converter reading the building data.</param>
        /// <param name="columnTypologyFilter">The root of the filter chain describing the grouping levels.</param>
        /// <param name="countyIds">The identifiers of the county partitions to read. One identifier is one polygon part.</param>
        /// <param name="column_Reference">The column identifying a row. Defaults to the shared reference column.</param>
        /// <param name="typologyItem_Root">The item naming the root node. When null the root is left unnamed.</param>
        /// <param name="includeReferences">A value indicating whether the identified references are stored on the nodes.</param>
        /// <param name="pageSize">The number of rows read per page.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout. Defaults to 600 seconds.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the solved typology, or null when the converter, the chain or the partition list is missing, when a page could not be read, or when the partitions hold no rows at all.</returns>
        public static async Task<Typology.Classes.Typology?> TypologyAsync(
            this BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter,
            Typology.Classes.ColumnTypologyFilter<Core.IO.Table.Classes.Column>? columnTypologyFilter,
            IEnumerable<int>? countyIds,
            Core.IO.Table.Classes.Column? column_Reference = null,
            Typology.Classes.TypologyItem? typologyItem_Root = null,
            bool includeReferences = true,
            int pageSize = 5000,
            int commandTimeout = 600,
            CancellationToken cancellationToken = default)
        {
            if (buildingDataPostgreSQLConverter is null || columnTypologyFilter is null || countyIds is null || pageSize < 1)
            {
                return null;
            }

            Core.IO.Table.Classes.Column column_Reference_Temp = column_Reference ?? IO.Constants.Column.Reference;

            List<string>? columnUniqueIds = GIS.Query.ColumnUniqueIds(columnTypologyFilter);
            if (columnUniqueIds is null)
            {
                return null;
            }

            HashSet<string> columnUniqueIds_Temp = [.. columnUniqueIds];

            // Fully qualified: DiGi.PostgreSQL.Table.Query and DiGi.Core.Query both declare an applicable UniqueId,
            // and which one an unqualified call binds to is decided by the using block rather than by the call site.
            AddUniqueId(Core.IO.Query.UniqueId(column_Reference_Temp));
            AddUniqueId(Core.IO.Query.UniqueId(IO.Constants.Column.CountyId));

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(buildingDataPostgreSQLConverter.ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            Core.IO.Table.Classes.Table? table_Result = null;
            string? name_Reference = column_Reference_Temp.Name;

            // A caller collecting the parts of a county code can hand the same part over twice, and reading a
            // partition twice would put every one of its rows into the table twice.
            HashSet<int> countyIds_Temp = [.. countyIds];

            foreach (int countyId in countyIds_Temp)
            {
                string? lastReference = null;

                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    Core.IO.Table.Classes.Table? table_Page = await buildingDataPostgreSQLConverter.PullAsync(npgsqlConnection, countyId, columnUniqueIds_Temp, lastReference, pageSize, commandTimeout, cancellationToken);
                    if (table_Page is null)
                    {
                        return null;
                    }

                    List<Core.IO.Table.Classes.Row> rows = [.. table_Page];
                    if (rows.Count == 0)
                    {
                        break;
                    }

                    table_Result ??= new Core.IO.Table.Classes.Table(table_Page.Columns);

                    // A page's column order is not guaranteed to repeat, so the columns are matched by name once per
                    // page rather than trusting a page index to mean the same column in the table being assembled.
                    Dictionary<int, int> indexes = [];
                    foreach (Core.IO.Table.Classes.Column column in table_Page.Columns)
                    {
                        int index = table_Result.GetColumnIndex(column.Name);
                        if (index != -1)
                        {
                            indexes[column.Index] = index;
                        }
                    }

                    foreach (Core.IO.Table.Classes.Row row in rows)
                    {
                        Dictionary<int, object?> values = [];
                        foreach (KeyValuePair<int, int> keyValuePair in indexes)
                        {
                            object? value = row[keyValuePair.Key];
                            if (value is null)
                            {
                                continue;
                            }

                            values[keyValuePair.Value] = value;
                        }

                        table_Result.AddRow(values);
                    }

                    if (rows.Count < pageSize)
                    {
                        break;
                    }

                    int index_Reference = table_Page.GetColumnIndex(name_Reference);
                    if (index_Reference == -1)
                    {
                        // Without the seek column the paging cannot advance. Stopping here would report part of
                        // the partition as the whole of it, so the read is failed instead.
                        return null;
                    }

                    // The page arrives ordered ascending by reference under the database's own collation, so its
                    // last row is the cursor. A maximum computed here would be an ordinal one, which under any
                    // other collation names a different row and silently steps over everything between the two.
                    string? reference_Last = rows[rows.Count - 1][index_Reference]?.ToString();

                    // A cursor that does not move re-reads the same page for ever.
                    if (string.IsNullOrEmpty(reference_Last) || reference_Last == lastReference)
                    {
                        return null;
                    }

                    lastReference = reference_Last;
                }
            }

            if (table_Result is null)
            {
                return null;
            }

            return GIS.Create.Typology(table_Result, columnTypologyFilter, column_Reference_Temp, typologyItem_Root, includeReferences);

            void AddUniqueId(string? uniqueId)
            {
                if (!string.IsNullOrWhiteSpace(uniqueId))
                {
                    columnUniqueIds_Temp.Add(uniqueId!);
                }
            }
        }
    }
}
