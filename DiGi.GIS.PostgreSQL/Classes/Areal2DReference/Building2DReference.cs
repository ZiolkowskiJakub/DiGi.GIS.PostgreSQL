using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DReference : Areal2DReference
    {
        public Building2DReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DReference(Building2DReference? building2DReference)
            : base(building2DReference)
        {
            if (building2DReference is not null)
            {
                Id = building2DReference.Id;
                SubdivisionId = building2DReference.SubdivisionId;
            }
        }

        public Building2DReference()
        {
        }

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}