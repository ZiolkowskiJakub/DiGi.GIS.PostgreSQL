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

        public Areal2D(Areal2D? postgreSQLAreal2D)
            : base(postgreSQLAreal2D)
        {
            if (postgreSQLAreal2D is not null)
            {
                BoundingBox2D = postgreSQLAreal2D.BoundingBox2D;
                Reference = postgreSQLAreal2D.Reference;
                Object = postgreSQLAreal2D.Object;
            }
        }

        public Areal2D()
        {
        }

        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;

        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }

        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;
    }
}