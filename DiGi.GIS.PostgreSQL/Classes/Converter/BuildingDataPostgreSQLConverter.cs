using DiGi.GIS.IO;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using DiGi.PostgreSQL.Table;
using DiGi.PostgreSQL.Table.Classes;
using DiGi.PostgreSQL.Table.Enums;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides a PostgreSQL converter for building data, facilitating the translation between building table representations
    /// using <see cref="Core.IO.Table.Classes.Column"/> and their corresponding PostgreSQL database structures, while implementing GIS-specific conversion functionality.
    /// </summary>
    public class BuildingDataPostgreSQLConverter : TablePostgreSQLConverter<Core.IO.Table.Classes.Column>, IGISPostgreSQLConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataPostgreSQLConverter" /> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData" /> containing the connection settings required to establish a connection to the PostgreSQL database. This value can be <see langword="null"/>.</param>
        public BuildingDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary> Gets the name of the database table associated with building data. </summary>
        public override string TableName => Constants.TableName.BuildingData;

        /// <summary>
        /// Gets the table conversion options specifically configured for <see cref="Core.IO.Table.Classes.Column"/>.
        /// </summary>
        /// <value>
        /// A <see cref="TableConversionOptions{TColumn}"/> instance containing the configuration settings for the table conversion, or <c>null</c>.
        /// </value>
        protected override TableConversionOptions<Core.IO.Table.Classes.Column>? TableConversionOptions => new()
        {
            PrimaryKeyColumns = [IO.Constants.Column.CountyId, IO.Constants.Column.Reference],
            PartitioningOptions = new PartitioningOptions<Core.IO.Table.Classes.Column>()
            {
                Column = IO.Constants.Column.CountyId,
                PartitioningRule = new ValuePartitioningRule()
            }
        };

        /// <summary>
        /// Asynchronously retrieves a collection of unique values based on the specified unique identifier and county identifier, applying optional dynamic filters.
        /// </summary>
        /// <typeparam name="T">The type of the values to be retrieved.</typeparam>
        /// <param name="uniqueId">The nullable string representing the unique identifier used for filtering.</param>
        /// <param name="countyId">The integer identifier of the county.</param>
        /// <param name="filterGroup">The optional dynamic hierarchical filters to apply prior to retrieving the unique values.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a nullable collection of nullable elements of type <typeparam ref="T"/>, or null if no values are found.</returns>
        public async Task<IEnumerable<T?>?> GetUniqueValuesAsync<T>(string? uniqueId, int countyId, FilterGroup? filterGroup = null)
        {
            if(string.IsNullOrWhiteSpace(uniqueId))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            return await GetUniqueValuesAsync<T>(npgsqlConnection, uniqueId, countyId, filterGroup);
        }

        /// <summary>
        /// Asynchronously retrieves a collection of unique values for a specified identifier and county from the database, applying optional dynamic filters.
        /// </summary>
        /// <typeparam name="T">The type of the unique values to be retrieved.</typeparam>
        /// <param name="npgsqlConnection">The Npgsql connection instance used to execute the command.</param>
        /// <param name="uniqueId">The string identifier used to filter for unique values.</param>
        /// <param name="countyId">The integer identifier of the county.</param>
        /// <param name="filterGroup">The optional dynamic hierarchical filters to apply prior to retrieving the unique values.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of nullable values of type T, or null if no results are found or the connection is invalid.</returns>
        public async Task<IEnumerable<T?>?> GetUniqueValuesAsync<T>(NpgsqlConnection? npgsqlConnection, string? uniqueId, int countyId, FilterGroup? filterGroup = null)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(uniqueId))
            {
                return null;
            }

            if (filterGroup is not null)
            {
                FilterGroup filterGroup_Combined = new FilterGroup();
                filterGroup_Combined.LogicalOperator = FilterLogicalOperator.And;

                FilterCondition filterCondition_County = new FilterCondition();
                filterCondition_County.ColumnUniqueId = IO.Constants.Column.CountyId.UniqueId();
                filterCondition_County.FilterOperator = FilterOperator.Equals;
                filterCondition_County.Value = countyId;

                filterGroup_Combined.FilterConditions = new List<FilterCondition> { filterCondition_County };
                filterGroup_Combined.FilterGroups = new List<FilterGroup> { filterGroup };

                return await GetUniqueValuesAsync<T>(npgsqlConnection, uniqueId, filterGroup_Combined);
            }

            string commandQuery = $@"
                SELECT DISTINCT {uniqueId} 
                FROM {TableName} 
                WHERE (@countyId IS NULL OR county_id = @countyId) 
                  AND {uniqueId} IS NOT NULL 
                ORDER BY {uniqueId}";

            HashSet<T?> result = [];

            using NpgsqlCommand npgsqlCommand = new(commandQuery, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();

            while (await npgsqlDataReader.ReadAsync())
            {
                if (npgsqlDataReader.IsDBNull(0) || !Core.Query.TryConvert(npgsqlDataReader.GetValue(0), out T? value))
                {
                    result.Add(default);
                }
                else
                {
                    result.Add(value);
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a table based on the specified references, county identifier, and optional column filters.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to communicate with the PostgreSQL database; may be null.</param>
        /// <param name="references">An <see cref="IEnumerable{T}"/> of <see cref="string"/> values representing the references to be pulled.</param>
        /// <param name="countyId">An optional integer identifying the specific county.</param>
        /// <param name="columnUniqueIds">An optional <see cref="IEnumerable{T}"/> of <see cref="string"/> unique identifiers for columns to include in the operation.</param>
        /// <param name="batchSize">The integer number of records to process per batch. Defaults to 1000.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Table"/> object if the data is successfully retrieved; otherwise, null.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string> references, int? countyId, IEnumerable<string>? columnUniqueIds = null, int batchSize = 1000)
        {
            if (npgsqlConnection is null || references is null || !references.Any())
            {
                return null;
            }

            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];

            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(npgsqlConnection, columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table = new(columns);

            Core.IO.Table.Classes.Column? column_Reference = table.UpdateColumn<Core.IO.Table.Classes.Column>(IO.Constants.Column.Reference);
            if (column_Reference is null)
            {
                return null;
            }

            Core.IO.Table.Classes.Column? column_CountyId = countyId is null ? null : table.UpdateColumn<Core.IO.Table.Classes.Column>(IO.Constants.Column.CountyId);

            foreach (string reference in references)
            {
                Dictionary<int, object?> values = [];
                values[column_Reference.Index] = reference;
                if (column_CountyId is not null)
                {
                    values[column_CountyId.Index] = countyId;
                }

                table.AddRow(values);
            }

            await PullAsync(npgsqlConnection, table, batchSize);

            return table;
        }

        /// <summary>
        /// Asynchronously retrieves data from the database based on specified column identifiers and values.
        /// </summary>
        /// <typeparam name="TObject">The type of the values used for filtering the table data.</typeparam>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance to be used for the database operation. Can be null.</param>
        /// <param name="columnUniqueId">The unique identifier of the column used as the primary filter.</param>
        /// <param name="values">A collection of values of type <typeparam ref="TObject"/> to retrieve from the table. Can be null.</param>
        /// <param name="columnUniqueIds">An optional collection of additional column unique identifiers to include in the retrieval process. Defaults to null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Core.IO.Table.Classes.Table"/> object if data was successfully retrieved; otherwise, null.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync<TObject>(NpgsqlConnection? npgsqlConnection, string columnUniqueId, IEnumerable<TObject>? values, IEnumerable<string>? columnUniqueIds = null)
        {
            if (npgsqlConnection is null || values is null || !values.Any() || string.IsNullOrWhiteSpace(columnUniqueId))
            {
                return null;
            }

            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];

            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(npgsqlConnection, columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table = new(columns);

            await PullAsync(npgsqlConnection, table, columnUniqueId, values);

            return table;
        }

        /// <summary>
        /// Asynchronously retrieves a table record based on a specified column unique identifier and value.
        /// </summary>
        /// <typeparam name="TObject">The type of the object used for the retrieval operation.</typeparam>
        /// <param name="columnUniqueId">The unique identifier of the column to be used as the primary filter.</param>
        /// <param name="value">The value to search for in the specified column; can be null.</param>
        /// <param name="columnUniqueIds">An optional collection of string unique identifiers for additional columns to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Core.IO.Table.Classes.Table"/> instance if a matching record is found; otherwise, null.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync<TObject>(string columnUniqueId, object? value, IEnumerable<string>? columnUniqueIds = null)
        {
            if (string.IsNullOrWhiteSpace(columnUniqueId))
            {
                return null;
            }

            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];

            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table = new(columns);

            await PullAsync(table, columnUniqueId, value);

            return table;
        }

        /// <summary>
        /// Asynchronously retrieves a table record from the database based on a specified unique identifier and value.
        /// </summary>
        /// <typeparam name="TObject">The type of object associated with the data retrieval operation.</typeparam>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to communicate with the PostgreSQL database. Can be null.</param>
        /// <param name="columnUniqueId">The name of the column used as the unique identifier for filtering the record.</param>
        /// <param name="value">The value to match against the specified unique identifier column. Can be null.</param>
        /// <param name="columnUniqueIds">An optional collection of strings representing additional column identifiers to be processed. Can be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Core.IO.Table.Classes.Table"/> object if a matching record is found; otherwise, null.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync<TObject>(NpgsqlConnection? npgsqlConnection, string columnUniqueId, object? value, IEnumerable<string>? columnUniqueIds = null)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(columnUniqueId))
            {
                return null;
            }

            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];

            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(npgsqlConnection, columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table = new(columns);

            await PullAsync(npgsqlConnection, table, columnUniqueId, value);

            return table;
        }

        /// <summary>
        /// Asynchronously pulls data from a table based on the specified column unique identifiers and values.
        /// </summary>
        /// <typeparam name="TObject">The type of the objects contained in the values collection.</typeparam>
        /// <param name="columnUniqueId">The unique identifier of the primary column used for the pull operation.</param>
        /// <param name="values">An enumerable collection of <typeparamref name="TObject"/> values to be used as criteria. Can be null.</param>
        /// <param name="columnUniqueIds">An optional enumerable collection of additional unique identifiers for columns. Defaults to null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Core.IO.Table.Classes.Table"/> instance if data is successfully retrieved; otherwise, null.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync<TObject>(string columnUniqueId, IEnumerable<TObject>? values, IEnumerable<string>? columnUniqueIds = null)
        {
            if (values is null || !values.Any() || string.IsNullOrWhiteSpace(columnUniqueId))
            {
                return null;
            }

            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];

            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table = new(columns);

            await PullAsync(table, columnUniqueId, values);

            return table;
        }

        /// <summary>
        /// Asynchronously retrieves a table based on the specified references, county identifier, and optional column filters.
        /// </summary>
        /// <param name="references">An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the references to pull.</param>
        /// <param name="countyId">An optional <see cref="int"/> representing the unique identifier of the county.</param>
        /// <param name="columnUniqueIds">An optional <see cref="IEnumerable{T}"/> of <see cref="string"/> specifying the unique identifiers of the columns to retrieve.</param>
        /// <param name="batchSize">An <see cref="int"/> specifying the number of records to process per batch. Defaults to 1000.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a <see cref="Core.IO.Table.Classes.Table"/> object if successful; otherwise, <see langword="null"/>.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync(IEnumerable<string> references, int? countyId, IEnumerable<string>? columnUniqueIds = null, int batchSize = 1000)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            return await PullAsync(npgsqlConnection, references, countyId, columnUniqueIds, batchSize);
        }

        /// <summary>
        /// Asynchronously pulls a keyset-paginated chunk of building data from a partition county.
        /// </summary>
        /// <param name="countyId">The partition key identifying the county.</param>
        /// <param name="columnUniqueIds">The optional list of column unique identifiers to project.</param>
        /// <param name="lastReference">The last reference string from the previous page used as the cursor seek-key.</param>
        /// <param name="pageSize">The page size count limit.</param>
        /// <returns>A task representing the async operation, returning the populated <see cref="Core.IO.Table.Classes.Table"/> if successful; otherwise, null.</returns>
        public async Task<Core.IO.Table.Classes.Table?> PullAsync(int countyId, IEnumerable<string>? columnUniqueIds, string? lastReference, int pageSize = 250)
        {
            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];
            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table_Result = new(columns);
            table_Result.UpdateColumn<Core.IO.Table.Classes.Column>(IO.Constants.Column.Reference);
            table_Result.UpdateColumn<Core.IO.Table.Classes.Column>(IO.Constants.Column.CountyId);

            await using NpgsqlConnection? npgsqlConnection_Db = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection_Db is null)
            {
                return null;
            }
            await npgsqlConnection_Db.OpenAsync();

            bool isSuccess = await PullAsync(
                npgsqlConnection_Db,
                table_Result,
                IO.Constants.Column.Reference.UniqueId()!,
                lastReference,
                pageSize,
                countyId);

            return isSuccess ? table_Result : null;
        }

        /// <summary>
        /// Asynchronously computes single-value aggregate statistics on a specific building data column inside a county partition, applying optional dynamic filters.
        /// </summary>
        /// <param name="columnUniqueId">The unique identifier of the column to aggregate.</param>
        /// <param name="singlevalueAggregateFunction">The single-value aggregate calculation function.</param>
        /// <param name="countyId">The partition county identifier.</param>
        /// <param name="filterGroup">The optional dynamic hierarchical filters to apply prior to aggregation.</param>
        /// <returns>A task representing the async operation, returning the aggregate result as a <see cref="System.Text.Json.Nodes.JsonNode"/>.</returns>
        public async Task<System.Text.Json.Nodes.JsonNode?> GetAggregateSummaryAsync(
            string columnUniqueId,
            DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction singlevalueAggregateFunction,
            int countyId,
            FilterGroup? filterGroup = null)
        {
            await using NpgsqlConnection? npgsqlConnection_Db = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection_Db is null)
            {
                return null;
            }
            await npgsqlConnection_Db.OpenAsync();

            return await GetAggregateSummaryAsync<Core.IO.Table.Classes.Column>(
                npgsqlConnection_Db,
                columnUniqueId,
                singlevalueAggregateFunction,
                countyId,
                filterGroup);
        }

        /// <summary>
        /// Asynchronously computes multi-value aggregate statistics on a specific building data column inside a county partition, applying optional dynamic filters.
        /// </summary>
        /// <param name="columnUniqueId">The unique identifier of the column to aggregate.</param>
        /// <param name="multivalueAggregateFunction">The multi-value aggregate calculation function.</param>
        /// <param name="countyId">The partition county identifier.</param>
        /// <param name="separator">The optional custom string delimiter; if null, it is automatically detected.</param>
        /// <param name="filterGroup">The optional dynamic hierarchical filters to apply prior to aggregation.</param>
        /// <returns>A task representing the async operation, returning the aggregate result as a <see cref="System.Text.Json.Nodes.JsonNode"/>.</returns>
        public async Task<System.Text.Json.Nodes.JsonNode?> GetAggregateSummaryAsync(
            string columnUniqueId,
            DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction multivalueAggregateFunction,
            int countyId,
            string? separator = null,
            FilterGroup? filterGroup = null)
        {
            await using NpgsqlConnection? npgsqlConnection_Db = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection_Db is null)
            {
                return null;
            }
            await npgsqlConnection_Db.OpenAsync();

            return await GetAggregateSummaryAsync<Core.IO.Table.Classes.Column>(
                npgsqlConnection_Db,
                columnUniqueId,
                multivalueAggregateFunction,
                countyId,
                separator,
                filterGroup);
        }

        /// <summary>
        /// Asynchronously generates a value distribution histogram for a specific building data column inside a county partition, applying optional dynamic filters.
        /// </summary>
        /// <param name="columnUniqueId">The unique identifier of the column to aggregate.</param>
        /// <param name="bucketCount">The total number of buckets to segment the value range into.</param>
        /// <param name="countyId">The partition county identifier.</param>
        /// <param name="filterGroup">The optional dynamic hierarchical filters to apply prior to generating the histogram.</param>
        /// <returns>A task representing the async operation, returning the histogram aggregate result as a <see cref="System.Text.Json.Nodes.JsonArray"/>.</returns>
        public async Task<System.Text.Json.Nodes.JsonArray?> GetHistogramSummaryAsync(
            string columnUniqueId,
            int bucketCount,
            int countyId,
            FilterGroup? filterGroup = null)
        {
            await using NpgsqlConnection? npgsqlConnection_Db = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection_Db is null)
            {
                return null;
            }
            await npgsqlConnection_Db.OpenAsync();

            return await GetHistogramSummaryAsync<Core.IO.Table.Classes.Column>(
                npgsqlConnection_Db,
                columnUniqueId,
                bucketCount,
                countyId,
                filterGroup);
        }
    }
}

