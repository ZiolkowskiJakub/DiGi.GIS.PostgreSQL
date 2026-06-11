using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a GIS OrtoDatas instance to a PostgreSQL-compatible OrtoDatas instance.
        /// </summary>
        /// <param name="ortoDatas">The source GIS OrtoDatas object to convert.</param>
        /// <param name="countyId">The optional county identifier associated with the data.</param>
        /// <returns>A new PostgreSQL-compatible <see cref="OrtoDatas"/> instance, or null if the input is null.</returns>
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