using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class Building2DReferencedObject<TGISSerializableObject> : ReferencedObject<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public Building2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DReferencedObject(Building2DReferencedObject<TGISSerializableObject>? building2DReferencedObject)
            : base(building2DReferencedObject)
        {
            if (building2DReferencedObject is not null)
            {
                CountyId = building2DReferencedObject.CountyId;
                Id = building2DReferencedObject.Id;
            }
        }

        public Building2DReferencedObject()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }
    }
}