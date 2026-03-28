using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static OrtoDatas? ToPostgreSQL(this GIS.Classes.OrtoDatas? ortoDatas, int? countyId)
        {
            if (ortoDatas is null)
            {
                return null;
            }

            BoundingBox2D? boundingBox2D = ortoDatas.GetBoundingBox(GIS.Enums.GeometryContext.Global);

            OrtoDatas result = new()
            {
                Reference = ortoDatas.Reference,
                BoundingBox2D = boundingBox2D,
                Object = ortoDatas.ToJsonObject(),
                CountyId = countyId
            };

            return result;
        }
    }
}