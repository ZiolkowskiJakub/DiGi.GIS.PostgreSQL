using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class AdministrativeAreal2DReferencedObject<TUniqueObject> : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        public AdministrativeAreal2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public AdministrativeAreal2DReferencedObject(AdministrativeAreal2DReferencedObject<TUniqueObject>? administrativeAreal2DReferencedObject)
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