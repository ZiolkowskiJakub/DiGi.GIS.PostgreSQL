using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static YearBuilt? ToPostgreSQL(this GIS.Interfaces.IYearBuilt? yearBuilt, int? countyId, string? reference)
        {
            if (yearBuilt is null)
            {
                return null;
            }

            return new YearBuilt()
            {
                CountyId = countyId,
                Reference = reference,
                Source = yearBuilt.Source,
                Year = yearBuilt.Year,
                Object = yearBuilt.ToJsonObject(),
            };
        }
    }
}