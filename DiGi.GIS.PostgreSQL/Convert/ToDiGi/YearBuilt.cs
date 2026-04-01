using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static GIS.Interfaces.IYearBuilt? ToDiGi(this YearBuilt? yearBuilt)
        {
            if (yearBuilt is null)
            {
                return null;
            }

            return Core.Create.SerializableObject<GIS.Interfaces.IYearBuilt>(yearBuilt?.Object);
        }
    }
}