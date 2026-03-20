using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class OrtoDatas : SerializableObject, IOrtoData
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
                CountyId = ortoDatas.CountyId;
                CreatedAt = ortoDatas.CreatedAt;
                Id = ortoDatas.Id;
                Object = ortoDatas.Object;
                Reference = ortoDatas.Reference;
            }
        }

        public OrtoDatas()
        {

        }

        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}