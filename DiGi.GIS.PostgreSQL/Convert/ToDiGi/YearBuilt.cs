using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

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