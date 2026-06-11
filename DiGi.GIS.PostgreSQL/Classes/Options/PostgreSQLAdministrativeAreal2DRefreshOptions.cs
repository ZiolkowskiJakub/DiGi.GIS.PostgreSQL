using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for refreshing PostgreSQL administrative areal 2D data.
    /// </summary>
    public class PostgreSQLAdministrativeAreal2DRefreshOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLAdministrativeAreal2DRefreshOptions" /> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the refresh options.</param>
        public PostgreSQLAdministrativeAreal2DRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLAdministrativeAreal2DRefreshOptions" /> class.
        /// </summary>
        public PostgreSQLAdministrativeAreal2DRefreshOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLAdministrativeAreal2DRefreshOptions" /> class by copying another instance.
        /// </summary>
        /// <param name="postgreSQLAdministrativeAreal2DRefreshOptions">The source options to copy from.</param>
        public PostgreSQLAdministrativeAreal2DRefreshOptions(PostgreSQLAdministrativeAreal2DRefreshOptions postgreSQLAdministrativeAreal2DRefreshOptions)
            : base(postgreSQLAdministrativeAreal2DRefreshOptions)
        {
            if (postgreSQLAdministrativeAreal2DRefreshOptions is not null)
            {
                Tolerance = postgreSQLAdministrativeAreal2DRefreshOptions.Tolerance;
            }
        }

        /// <summary>
        /// Gets or sets the tolerance value used for the refresh process.
        /// </summary>
        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}