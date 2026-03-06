using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
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