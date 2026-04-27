using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DOccupancyData : Building2DReferencedObject<IOccupancyData>
    {
        public Building2DOccupancyData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DOccupancyData(Building2DOccupancyData? building2DOccupnacyData)
            : base(building2DOccupnacyData)
        {
        }

        public Building2DOccupancyData()
            : base()
        {
        }
    }
}