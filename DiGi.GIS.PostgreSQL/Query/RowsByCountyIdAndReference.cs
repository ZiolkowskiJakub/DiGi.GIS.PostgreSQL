using DiGi.Core.IO.Table.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Indexes the rows of a table by their county identifier and reference so that a caller matching a collection of items against them does not rescan the table for each one.
        /// <para>Rows are read from the back, so where several rows carry the same county and reference the one at the highest index wins - which is what a backwards scan stopping at its first hit did.</para>
        /// </summary>
        /// <param name="table">The table whose rows are indexed.</param>
        /// <param name="column_CountyId">The county identifier column.</param>
        /// <param name="column_Reference">The reference column.</param>
        /// <returns>A dictionary of rows keyed by county identifier and reference. Rows missing either value are not included. Never null.</returns>
        public static Dictionary<(int, string), Row> RowsByCountyIdAndReference(this Table? table, Column? column_CountyId, Column? column_Reference)
        {
            Dictionary<(int, string), Row> result = [];

            if (table is null || column_CountyId is null || column_Reference is null)
            {
                return result;
            }

            int count = table.RowCount;
            for (int i = count - 1; i >= 0; i--)
            {
                Row? row = table.GetRow(i);
                if (row is null)
                {
                    continue;
                }

                if (!row.TryGetValue(column_CountyId.Index, out int countyId_Row))
                {
                    continue;
                }

                if (!row.TryGetValue(column_Reference.Index, out string? reference_Row) || string.IsNullOrWhiteSpace(reference_Row))
                {
                    continue;
                }

                result.TryAdd((countyId_Row, reference_Row!), row);
            }

            return result;
        }
    }
}
