using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the specified occupancy data to a PostgreSQL-compatible administrative areal 2D occupancy data object.
        /// </summary>
        /// <param name="occupancyData">The occupancy data to convert.</param>
        /// <returns>A <see cref="AdministrativeAreal2DOccupancyData"/> instance if the provided occupancy data is not null; otherwise, null.</returns>
        public static AdministrativeAreal2DOccupancyData? ToPostgreSQL(this GIS.Interfaces.IOccupancyData? occupancyData)
        {
            if (occupancyData is null)
            {
                return null;
            }

            AdministrativeAreal2DOccupancyData result = new()
            {
                Reference = occupancyData.Reference,
                Object = occupancyData.ToJsonObject(),
                UniqueId = occupancyData.UniqueId,
            };

            return result;
        }
    }
}