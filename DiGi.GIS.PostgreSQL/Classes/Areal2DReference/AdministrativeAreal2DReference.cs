using DiGi.GIS.PostgreSQL.Enums;
using System.Collections.Generic;
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
                VoivodeshipId = administrativeAreal2DReference.VoivodeshipId;
                Code = administrativeAreal2DReference.Code;
            }
        }

        public AdministrativeAreal2DReference()
        {
        }

        [JsonInclude, JsonPropertyName("AdministrativeArealType")]
        public AdministrativeArealType AdministrativeArealType { get; set; }

        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;

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

        public List<int> GetIds()
        {
            List<int> result = [];
            if(CountryId is not null && CountryId.HasValue)
            {
                result.Add(CountryId.Value);
            }

            if (VoivodeshipId is not null && VoivodeshipId.HasValue)
            {
                result.Add(VoivodeshipId.Value);
            }

            if (CountyId is not null && CountyId.HasValue)
            {
                result.Add(CountyId.Value);
            }

            if (MunicipalityId is not null && MunicipalityId.HasValue)
            {
                result.Add(MunicipalityId.Value);
            }

            result.Add(Id);

            return result;
        }
    }
}