using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class CountyReferencedObject<TGISSerializableObject> : ReferencedObject<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public CountyReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public CountyReferencedObject(CountyReferencedObject<TGISSerializableObject>? countyReferencedObject)
            : base(countyReferencedObject)
        {
            if (countyReferencedObject is not null)
            {
                CountyId = countyReferencedObject.CountyId;
                Id = countyReferencedObject.Id;
            }
        }

        public CountyReferencedObject()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }
    }
}

