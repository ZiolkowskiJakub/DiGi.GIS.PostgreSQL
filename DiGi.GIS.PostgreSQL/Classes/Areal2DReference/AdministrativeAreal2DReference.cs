using DiGi.GIS.PostgreSQL.Enums;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2DReference : Areal2DReference
    {
        public AdministrativeAreal2DReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public AdministrativeAreal2DReference(AdministrativeAreal2DReference? administrativeAreal2DReference)
            : base(administrativeAreal2DReference)
        {
            if (administrativeAreal2DReference is not null)
            {
                AdministrativeArealType = administrativeAreal2DReference.AdministrativeArealType;
                CountyId = administrativeAreal2DReference.CountyId;
                Id = administrativeAreal2DReference.Id;
                MunicipalityId = administrativeAreal2DReference.MunicipalityId;
                Name = administrativeAreal2DReference.Name;
                VoivodeshipId = administrativeAreal2DReference?.VoivodeshipId;
            }
        }

        public AdministrativeAreal2DReference()
        {
        }

        [JsonInclude, JsonPropertyName("AdministrativeArealType")]
        public AdministrativeArealType AdministrativeArealType { get; set; }

        [JsonInclude, JsonPropertyName("CountryId")]
        public int? CountryId { get; set; } = null;

        [JsonInclude, JsonPropertyName("Id")]
        public int Id { get; set; } = -1;

        [JsonInclude, JsonPropertyName("MunicipalityId")]
        public int? MunicipalityId { get; set; } = null;

        [JsonInclude, JsonPropertyName("Name")]
        public string? Name { get; set; } = null;

        [JsonInclude, JsonPropertyName("VoivodeshipId")]
        public int? VoivodeshipId { get; set; } = null;
    }
}