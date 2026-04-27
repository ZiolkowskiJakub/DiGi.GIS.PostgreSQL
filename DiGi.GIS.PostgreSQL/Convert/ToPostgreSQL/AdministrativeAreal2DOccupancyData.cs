using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
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