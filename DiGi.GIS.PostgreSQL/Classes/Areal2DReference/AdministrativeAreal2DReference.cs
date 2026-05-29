using DiGi.GIS.PostgreSQL.Enums;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a reference to a 2D administrative area within the Polish territorial division hierarchy (country, voivodeship, county, municipality).
    /// </summary>
    public class AdministrativeAreal2DReference : Areal2DReference
    {
        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReference class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public AdministrativeAreal2DReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReference class by copying data from another AdministrativeAreal2DReference instance.
        /// </summary>
        /// <param name="administrativeAreal2DReference">The AdministrativeAreal2DReference instance to copy data from.</param>
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

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReference class.
        /// </summary>
        public AdministrativeAreal2DReference()
        {
        }

        /// <summary>
        /// Gets or sets the type of the administrative area (e.g., country, voivodeship, county, municipality).
        /// </summary>
        [JsonInclude, JsonPropertyName("AdministrativeArealType")]
        public AdministrativeArealType AdministrativeArealType { get; set; }

        /// <summary>
        /// Gets or sets the code of the administrative area.
        /// </summary>
        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountryId")]
        public int? CountryId { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the administrative area.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public int Id { get; set; } = -1;

        /// <summary>
        /// Gets or sets the ID of the municipality.
        /// </summary>
        [JsonInclude, JsonPropertyName("MunicipalityId")]
        public int? MunicipalityId { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of the administrative area.
        /// </summary>
        [JsonInclude, JsonPropertyName("Name")]
        public string? Name { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the voivodeship.
        /// </summary>
        [JsonInclude, JsonPropertyName("VoivodeshipId")]
        public int? VoivodeshipId { get; set; } = null;

        /// <summary>
        /// Gets a list of ids in the record including own Id (in order: CountryId, VoivodeshipId, CountyId, MunicipalityId, Id). Null values are skipped.
        /// </summary>
        /// <returns>A list of integer IDs representing the territorial hierarchy.</returns>
        public List<int> GetIds()
        {
            List<int> result = [];
            if (CountryId is not null && CountryId.HasValue)
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