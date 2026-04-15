using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class AdministrativeAreal2DReferencedObject<TGISSerializableObject> : ReferencedObject<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public AdministrativeAreal2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public AdministrativeAreal2DReferencedObject(AdministrativeAreal2DReferencedObject<TGISSerializableObject> administrativeAreal2DReferencedObject)
            : base(administrativeAreal2DReferencedObject)
        {
            if (administrativeAreal2DReferencedObject is not null)
            {
                Id = administrativeAreal2DReferencedObject.Id;
            }
        }

        public AdministrativeAreal2DReferencedObject()
            : base()
        {
        }


        [JsonInclude, JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}