using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the specified <see cref="GIS.Interfaces.IYearBuiltData"/> instance to a <see cref="YearBuiltData"/> object for PostgreSQL storage.
        /// </summary>
        /// <param name="yearBuiltData">The source year built data.</param>
        /// <param name="countyId">The county identifier associated with the data.</param>
        /// <returns>A <see cref="YearBuiltData"/> instance if <paramref name="yearBuiltData"/> is not null; otherwise, null.</returns>
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