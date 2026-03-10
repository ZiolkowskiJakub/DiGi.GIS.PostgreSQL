using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DOrtoData : OrtoData
    {
        public Building2DOrtoData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DOrtoData(Building2DOrtoData? building2DOrtoData)
            : base(building2DOrtoData)
        {
            if (building2DOrtoData is not null)
            {
                Reference = building2DOrtoData.Reference;
            }
        }

        public Building2DOrtoData()
        {
        }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}