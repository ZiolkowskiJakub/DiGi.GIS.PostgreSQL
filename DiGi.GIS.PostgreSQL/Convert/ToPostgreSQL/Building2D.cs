using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
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