using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.IO;
using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        /// <summary>
        /// Updates the Id column of the table based on the provided building2DReferences. If a matching row is found (based on CountyId and Reference), it updates the Id value. If no matching row is found, it adds a new row with the CountyId, Reference, and Id values.
        /// </summary>
        /// <param name="table">The table to update</param>
        /// <param name="building2DReferences">The building2DReferences to use for updating</param>
        public static void Update_Id(this Table? table, IEnumerable<Building2DReference>? building2DReferences)
        {
            if (table is null || building2DReferences is null)
            {
                return;
            }

            Column? column_Reference = table.UpdateColumn<Column>(IO.Constants.Column.Reference);
            if (column_Reference is null)
            {
                return;
            }

            Column? column_CountyId = table.UpdateColumn<Column>(IO.Constants.Column.CountyId);
            if (column_CountyId is null)
            {
                return;
            }

            Column? column_Id = table.UpdateColumn<Column>(IO.Constants.Column.DatabaseId);

            Dictionary<(int, string), Row> dictionary_Row = Query.RowsByCountyIdAndReference(table, column_CountyId, column_Reference);

            foreach (Building2DReference building2DReference in building2DReferences)
            {
                string? reference = building2DReference?.Reference;
                if (string.IsNullOrWhiteSpace(reference))
                {
                    continue;
                }

                int? county_Id = building2DReference!.CountyId;
                if (county_Id is null)
                {
                    continue;
                }

                if (!dictionary_Row.TryGetValue((county_Id.Value, reference!), out Row? row) || row is null)
                {
                    row = table.AddRow();

                    IO.Modify.SetValue(row, column_CountyId, county_Id);
                    IO.Modify.SetValue(row, column_Reference, reference);

                    dictionary_Row[(county_Id.Value, reference!)] = row;
                }

                IO.Modify.SetValue(row, column_Id, building2DReference.Id);

                table.AddRow(row, false);
            }
        }

        /// <summary>
        /// Updates the occupancy data in the specified table based on the provided collection of building occupancy records.
        /// <para>The occupancy table holds one row per stored object rather than one per building, so a reference can arrive here several times. The first record of a given county and reference is the one that is written and the rest are stepped over, which means the collection has to reach this method in the caller's order of preference - the converter returns the newest record first, so passing its result straight through stores the newest.</para>
        /// </summary>
        /// <param name="table">The PostgreSQL table to be updated.</param>
        /// <param name="building2DOccupancyDatas">A collection of <see cref="Building2DOccupancyData"/> objects containing the new occupancy information, most preferred record first.</param>
        public static void Update_Occupancy(this Table? table, IEnumerable<Building2DOccupancyData>? building2DOccupancyDatas)
        {
            if (table is null || building2DOccupancyDatas is null)
            {
                return;
            }

            Column? column_Reference = table.UpdateColumn<Column>(IO.Constants.Column.Reference);
            if (column_Reference is null)
            {
                return;
            }

            Column? column_CountyId = table.UpdateColumn<Column>(IO.Constants.Column.CountyId);
            if (column_CountyId is null)
            {
                return;
            }

            Column? column_Occupancy = table.UpdateColumn<Column>(IO.Constants.Column.CalculatedOccupancy);

            Dictionary<(int, string), Row> dictionary_Row = Query.RowsByCountyIdAndReference(table, column_CountyId, column_Reference);

            HashSet<(int, string)> written = [];

            foreach (Building2DOccupancyData building2DOccupancyData in building2DOccupancyDatas)
            {
                string? reference = building2DOccupancyData?.Reference;
                if (string.IsNullOrWhiteSpace(reference))
                {
                    continue;
                }

                int? county_Id = building2DOccupancyData!.CountyId;
                if (county_Id is null)
                {
                    continue;
                }

                if (!written.Add((county_Id.Value, reference!)))
                {
                    continue;
                }

                if (building2DOccupancyData.ToDiGi() is not OccupancyData occupancyData)
                {
                    continue;
                }

                if (!dictionary_Row.TryGetValue((county_Id.Value, reference!), out Row? row) || row is null)
                {
                    row = table.AddRow();

                    IO.Modify.SetValue(row, column_CountyId, county_Id);
                    IO.Modify.SetValue(row, column_Reference, reference);

                    dictionary_Row[(county_Id.Value, reference!)] = row;
                }

                IO.Modify.SetValue(row, column_Occupancy, occupancyData.Occupancy);

                table.AddRow(row, false);
            }
        }
    }
}
