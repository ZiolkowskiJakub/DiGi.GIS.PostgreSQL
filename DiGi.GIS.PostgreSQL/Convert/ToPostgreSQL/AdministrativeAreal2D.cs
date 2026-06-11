using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a GIS administrative areal 2D object to its PostgreSQL representation.
        /// </summary>
        /// <param name="administrativeAreal2D">The source administrative areal 2D object to convert.</param>
        /// <returns>The converted <see cref="AdministrativeAreal2D"/> object, or null if the input is null.</returns>
        public static AdministrativeAreal2D? ToPostgreSQL(this GIS.Classes.AdministrativeAreal2D? administrativeAreal2D)
        {
            if (administrativeAreal2D is null)
            {
                return null;
            }

            AdministrativeAreal2D result = new()
            {
                Reference = administrativeAreal2D.Reference,
                BoundingBox2D = administrativeAreal2D.PolygonalFace2D?.GetBoundingBox(),
                AdministrativeArealType = Query.AdministrativeArealType(administrativeAreal2D),
                Object = administrativeAreal2D.ToJsonObject(),
                Code = administrativeAreal2D.Code,
                Name = administrativeAreal2D.Name
            };

            return result;
        }
    }
}