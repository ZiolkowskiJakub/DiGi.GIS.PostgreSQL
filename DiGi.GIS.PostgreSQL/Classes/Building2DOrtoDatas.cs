using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DOrtoDatas : OrtoDatas
    {
        public Building2DOrtoDatas(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DOrtoDatas(Building2DOrtoDatas? building2DOrtoDatas)
            : base(building2DOrtoDatas)
        {
            if (building2DOrtoDatas is not null)
            {
                Reference = building2DOrtoDatas.Reference;
            }
        }

        public Building2DOrtoDatas()
        {
        }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}