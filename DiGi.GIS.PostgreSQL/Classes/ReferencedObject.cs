using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class ReferencedObject<TGISSerializableObject> : TableSerializableObject<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public ReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public ReferencedObject(ReferencedObject<TGISSerializableObject>? referencedObject)
            : base(referencedObject)
        {
            if (referencedObject is not null)
            {
                Reference = referencedObject.Reference;
                CreatedAt = referencedObject.CreatedAt;
            }
        }

        public ReferencedObject()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }

        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;
    }
}