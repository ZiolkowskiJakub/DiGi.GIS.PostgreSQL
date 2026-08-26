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
    }
}
