using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2DOccupancyData : AdministrativeAreal2DReferencedObject<IOccupancyData>
    {
        public AdministrativeAreal2DOccupancyData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public AdministrativeAreal2DOccupancyData(AdministrativeAreal2DOccupancyData? administrativeAreal2DOccupancyData)
            : base(administrativeAreal2DOccupancyData)
        {
        }

        public AdministrativeAreal2DOccupancyData()
            : base()
        {
        }
    }
}