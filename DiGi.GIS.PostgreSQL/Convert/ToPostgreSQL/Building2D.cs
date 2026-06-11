using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a GIS Building2D instance to a PostgreSQL-compatible Building2D instance.
        /// </summary>
        /// <param name="building2D">The source building 2D object to convert.</param>
        /// <param name="code">An optional code associated with the building.</param>
        /// <returns>A converted <see cref="Building2D"/> instance, or null if the input is null.</returns>
        public static Building2D? ToPostgreSQL(this GIS.Classes.Building2D? building2D, string? code = null)
        {
            if (building2D is null)
            {
                return null;
            }

            Building2D result = new()
            {
                Reference = building2D.Reference,
                BoundingBox2D = building2D.PolygonalFace2D?.GetBoundingBox(),
                Object = building2D.ToJsonObject(),
                Code = code,
            };

            return result;
        }
    }
}