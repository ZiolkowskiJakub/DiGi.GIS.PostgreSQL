using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static YearBuiltData? ToPostgreSQL(this GIS.Interfaces.IYearBuiltData? yearBuiltData, int? countyId)
        {
            if (yearBuiltData is null)
            {
                return null;
            }

            YearBuiltData result = new()
            {
                Reference = yearBuiltData.Reference,
                Object = yearBuiltData.ToJsonObject(),
                UniqueId = yearBuiltData.UniqueId,
                CountyId = countyId
            };

            return result;
        }
    }
}