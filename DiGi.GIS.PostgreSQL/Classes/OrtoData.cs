using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class OrtoData : SerializableObject, IOrtoData
    {
        public OrtoData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public OrtoData(OrtoData? ortoData)
            : base(ortoData)
        {
            if (ortoData is not null)
            {
                BoundingBox2D = ortoData.BoundingBox2D;
                Object = ortoData.Object;
            }
        }

        public OrtoData()
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