using DiGi.GIS.PostgreSQL.Enums;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2D : Areal2D
    {
        public AdministrativeAreal2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public AdministrativeAreal2D(AdministrativeAreal2D? postgreSQLAdministrativeAreal2D)
            : base(postgreSQLAdministrativeAreal2D)
        {
        }

        public AdministrativeAreal2D()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("AdministrativeArealType")]
        public AdministrativeArealType AdministrativeArealType { get; set; }

        [JsonInclude, JsonPropertyName("CountryId")]
        public int? CountryId { get; set; } = null;

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; } = null;

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