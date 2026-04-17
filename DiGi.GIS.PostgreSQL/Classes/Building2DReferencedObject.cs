using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class Building2DReferencedObject<TUniqueObject> : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        public Building2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DReferencedObject(Building2DReferencedObject<TUniqueObject>? building2DReferencedObject)
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