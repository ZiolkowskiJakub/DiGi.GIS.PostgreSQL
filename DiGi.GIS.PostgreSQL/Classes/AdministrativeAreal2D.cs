using DiGi.GIS.PostgreSQL.Enums;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a 2D administrative area within the Polish territorial division hierarchy (country, voivodeship, county, municipality).
    /// <para>An instance is one <b>polygon part</b> of a unit, not the whole unit. BDOT10k stores a unit whose territory is disconnected as several features, and each becomes its own instance, so a multi-part county yields several instances sharing a <c>Code</c> - each with its own <see cref="Id"/> and its own private country/voivodeship ancestor chain. Treat <see cref="Id"/> as the identity and <c>Code</c> as descriptive; see <see cref="AdministrativeAreal2DPostgreSQLConverter"/> for the full storage model.</para>
    /// </summary>
    public class AdministrativeAreal2D : Areal2D<GIS.Classes.AdministrativeAreal2D>
    {
        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2D class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public AdministrativeAreal2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2D class by copying data from another AdministrativeAreal2D instance.
        /// </summary>
        /// <param name="administrativeAreal2D">The AdministrativeAreal2D instance to copy data from.</param>
        public AdministrativeAreal2D(AdministrativeAreal2D? administrativeAreal2D)
            : base(administrativeAreal2D)
        {
            if (administrativeAreal2D is not null)
            {
                AdministrativeArealType = administrativeAreal2D.AdministrativeArealType;
                CountyId = administrativeAreal2D.CountyId;
                Id = administrativeAreal2D.Id;
                MunicipalityId = administrativeAreal2D.MunicipalityId;
                Name = administrativeAreal2D.Name;
                VoivodeshipId = administrativeAreal2D?.VoivodeshipId;
            }
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2D class.
        /// </summary>
        public AdministrativeAreal2D()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the type of the administrative area (e.g., country, voivodeship, county, municipality).
        /// </summary>
        [JsonInclude, JsonPropertyName("AdministrativeArealType")]
        public AdministrativeArealType AdministrativeArealType { get; set; }

        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountryId")]
        public int? CountryId { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the county.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; } = null;

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
    }
}