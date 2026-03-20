using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static OrtoDatas? ToPostgreSQL(this GIS.Classes.OrtoDatas? ortoDatas)
        {
            if (ortoDatas is null)
            {
                return null;
            }

            throw new System.NotImplementedException();

            //ortoDatas.

            //AdministrativeAreal2D result = new()
            //{
            //    Reference = ortoDatas.Reference,
            //    BoundingBox2D = ortoDatas.PolygonalFace2D?.GetBoundingBox(),
            //    AdministrativeArealType = Query.AdministrativeArealType(ortoDatas),
            //    Object = ortoDatas.ToJsonObject(),
            //    Code = ortoDatas.Code,
            //    Name = ortoDatas.Name
            //};

            //return result;
        }
    }
}