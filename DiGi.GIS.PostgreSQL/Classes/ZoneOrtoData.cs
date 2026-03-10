using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class ZoneOrtoData : OrtoData
    {
        public ZoneOrtoData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public ZoneOrtoData(ZoneOrtoData? zoneOrtoData)
            : base(zoneOrtoData)
        {
            if (zoneOrtoData is not null)
            {

            }
        }

        public ZoneOrtoData()
        {
        }
    }
}