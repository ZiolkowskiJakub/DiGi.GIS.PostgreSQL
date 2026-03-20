using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class Areal2D : SerializableObject, IAreal2D
    {
        public Areal2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Areal2D(Areal2D? areal2D)
            : base(areal2D)
        {
            if (areal2D is not null)
            {
                BoundingBox2D = areal2D.BoundingBox2D;
                Code = areal2D.Code;
                CreatedAt = areal2D.CreatedAt;
                Object = areal2D.Object;
                Reference = areal2D.Reference;
            }
        }

        public Areal2D()
        {
        }

        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;
        
        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;

        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}