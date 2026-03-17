using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class OrtoDatas : SerializableObject, IOrtoData
    {
        public OrtoDatas(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public OrtoDatas(OrtoDatas? ortoDatas)
            : base(ortoDatas)
        {
            if (ortoDatas is not null)
            {
                BoundingBox2D = ortoDatas.BoundingBox2D;
                Object = ortoDatas.Object;
            }
        }

        public OrtoDatas()
        {
        }

        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;

        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;
    }
}