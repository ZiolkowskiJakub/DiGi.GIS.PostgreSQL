using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
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