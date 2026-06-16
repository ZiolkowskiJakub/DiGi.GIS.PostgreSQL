using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides configuration options for updating occupancy data within a PostgreSQL database.
    /// </summary>
    public class PostgreSQLUpdateOccupancyOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateOccupancyOptions"/> class using the provided JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the configuration data used to populate the options.</param>
        public PostgreSQLUpdateOccupancyOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateOccupancyOptions"/> class.
        /// </summary>
        public PostgreSQLUpdateOccupancyOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateOccupancyOptions"/> class by copying the values from an existing <see cref="PostgreSQLUpdateOccupancyOptions"/> instance.
        /// </summary>
        /// <param name="postgreSQLUpdateOccupancyOptions">The source <see cref="PostgreSQLUpdateOccupancyOptions"/> instance to copy settings from.</param>
        public PostgreSQLUpdateOccupancyOptions(PostgreSQLUpdateOccupancyOptions postgreSQLUpdateOccupancyOptions)
            : base(postgreSQLUpdateOccupancyOptions)
        {
            if (postgreSQLUpdateOccupancyOptions is not null)
            {
                IncludeBuilding2Ds = postgreSQLUpdateOccupancyOptions.IncludeBuilding2Ds;
                IncludeAdministrativeAreal2Ds = postgreSQLUpdateOccupancyOptions.IncludeAdministrativeAreal2Ds;
                Clear = postgreSQLUpdateOccupancyOptions.Clear;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether building 2D data should be included during the occupancy update process.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(IncludeBuilding2Ds))]
        public bool IncludeBuilding2Ds { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether administrative areal 2D data should be included during the occupancy update process.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(IncludeAdministrativeAreal2Ds))]
        public bool IncludeAdministrativeAreal2Ds { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the existing occupancy data should be cleared before performing the update operation.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Clear))]
        public bool Clear { get; set; } = true;
    }
}