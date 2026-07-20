using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a CityGML Building instance to a PostgreSQL-compatible Building instance.
        /// </summary>
        /// <param name="building">The source CityGML building object to convert.</param>
        /// <param name="countyId">An optional county identifier associated with the building.</param>
        /// <returns>A converted <see cref="Building"/> instance, or null if the input is null.</returns>
        public static Building? ToPostgreSQL(this CityGML.Classes.Building? building, int? countyId = null)
        {
            if (building is null)
            {
                return null;
            }

            string? reference = CityGML.Query.Reference(building);
            if (string.IsNullOrWhiteSpace(reference))
            {
                reference = building.UniqueId;
            }

            building.GetValue<string>(Analytical.Enums.BuildingModelParameter.LOD);

            Building result = new()
            {
                Reference = reference,
                UniqueId = building.UniqueId,
                CountyId = countyId,
                Object = building.ToJsonObject(),
                BoundingBox3D = CityGML.Query.BoundingBox(building),
            };

            return result;
        }
    }
}
