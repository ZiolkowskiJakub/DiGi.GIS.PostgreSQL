using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.IO;
using DiGi.GIS.PostgreSQL.Classes;
using System;
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
        public static async void Update_Id(this Table? table, IEnumerable<Building2DReference>? building2DReferences)
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

            List<Tuple<Row, int, string>> tuples = [];

            int count = table.RowCount;
            if (count != 0)
            {
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

                    tuples.Add(new Tuple<Row, int, string>(row, countyId_Row, reference_Row));
                }
            }

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

                Row? row = tuples.Find(x => x.Item2 == county_Id && x.Item3 == reference)?.Item1;
                if (row is null)
                {
                    row = table.AddRow();

                    IO.Modify.SetValue(row, column_CountyId, county_Id);
                    IO.Modify.SetValue(row, column_Reference, reference);
                }

                IO.Modify.SetValue(row, column_Id, building2DReference.Id);

                table.AddRow(row, false);
            }
        }

        /// <summary>
        /// Updates the occupancy data in the specified table based on the provided collection of building occupancy records.
        /// </summary>
        /// <param name="table">The PostgreSQL table to be updated.</param>
        /// <param name="building2DOccupancyDatas">A collection of <see cref="Building2DOccupancyData"/> objects containing the new occupancy information.</param>
        public static async void Update_Occupancy(this Table? table, IEnumerable<Building2DOccupancyData>? building2DOccupancyDatas)
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

            List<Tuple<Row, int, string>> tuples = [];

            int count = table.RowCount;
            if (count != 0)
            {
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

                    tuples.Add(new Tuple<Row, int, string>(row, countyId_Row, reference_Row));
                }
            }

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

                OccupancyData? occupancyData = building2DOccupancyData.ToDiGi() as OccupancyData;
                if(occupancyData is null)
                {
                    continue;
                }


                Row? row = tuples.Find(x => x.Item2 == county_Id && x.Item3 == reference)?.Item1;
                if (row is null)
                {
                    row = table.AddRow();

                    IO.Modify.SetValue(row, column_CountyId, county_Id);
                    IO.Modify.SetValue(row, column_Reference, reference);
                }

                IO.Modify.SetValue(row, column_Occupancy, occupancyData.Occupancy);

                table.AddRow(row, false);
            }
        }
    }
}