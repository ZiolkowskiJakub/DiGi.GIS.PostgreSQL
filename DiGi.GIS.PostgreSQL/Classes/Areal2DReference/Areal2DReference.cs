using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class Areal2DReference : SerializableObject
    {
        public Areal2DReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Areal2DReference(Areal2DReference? areal2DReference)
            : base(areal2DReference)
        {
            if (areal2DReference is not null)
            {
                CountyId = areal2DReference.CountyId;
                Reference = areal2DReference.Reference;
            }
        }

        public Areal2DReference()
        {
        }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}