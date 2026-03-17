using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class ZoneOrtoDatas : OrtoDatas
    {
        public ZoneOrtoDatas(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public ZoneOrtoDatas(ZoneOrtoDatas? zoneOrtoDatas)
            : base(zoneOrtoDatas)
        {
            if (zoneOrtoDatas is not null)
            {
            }
        }

        public ZoneOrtoDatas()
        {
        }
    }
}