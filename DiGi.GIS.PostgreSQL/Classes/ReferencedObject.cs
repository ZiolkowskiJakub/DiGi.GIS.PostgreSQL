using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class ReferencedObject<TUniqueObject> : TableSerializableObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        public ReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public ReferencedObject(ReferencedObject<TUniqueObject>? referencedObject)
            : base(referencedObject)
        {
            if (referencedObject is not null)
            {
                Reference = referencedObject.Reference;
                CreatedAt = referencedObject.CreatedAt;
                UniqueId = referencedObject.UniqueId;
            }
        }

        public ReferencedObject()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }

        [JsonInclude, JsonPropertyName("UniqueId")]
        public string? UniqueId { get; set; }

        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;
    }
}