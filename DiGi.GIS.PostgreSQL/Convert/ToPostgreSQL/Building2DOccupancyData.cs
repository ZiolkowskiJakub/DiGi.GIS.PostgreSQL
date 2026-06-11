using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the specified occupancy data to a PostgreSQL-compatible building 2D occupancy data object.
        /// </summary>
        /// <param name="occupancyData">The occupancy data to convert.</param>
        /// <param name="countyId">The optional county identifier associated with the occupancy data.</param>
        /// <returns>A <see cref="Building2DOccupancyData" /> object if the provided occupancy data is not null; otherwise, null.</returns>
        public static Building2DOccupancyData? ToPostgreSQL(this GIS.Interfaces.IOccupancyData? occupancyData, int? countyId)
        {
            if (occupancyData is null)
            {
                return null;
            }

            Building2DOccupancyData result = new()
            {
                Reference = occupancyData.Reference,
                Object = occupancyData.ToJsonObject(),
                UniqueId = occupancyData.UniqueId,
                CountyId = countyId
            };

            return result;
        }
    }
}