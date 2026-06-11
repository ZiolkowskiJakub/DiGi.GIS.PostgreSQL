using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for refreshing 2D building data in a PostgreSQL database.
    /// </summary>
    public class PostgreSQLBuilding2DRefreshOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DRefreshOptions" /> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object used to initialize the options.</param>
        public PostgreSQLBuilding2DRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DRefreshOptions" /> class.
        /// </summary>
        public PostgreSQLBuilding2DRefreshOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DRefreshOptions" /> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLBuilding2DRefreshOptions">The source options to copy from.</param>
        public PostgreSQLBuilding2DRefreshOptions(PostgreSQLBuilding2DRefreshOptions postgreSQLBuilding2DRefreshOptions)
            : base(postgreSQLBuilding2DRefreshOptions)
        {
            if (postgreSQLBuilding2DRefreshOptions is not null)
            {
                BatchSize = postgreSQLBuilding2DRefreshOptions.BatchSize;
                Tolerance = postgreSQLBuilding2DRefreshOptions.Tolerance;
                OverrideExistingSubdivisionIds = postgreSQLBuilding2DRefreshOptions.OverrideExistingSubdivisionIds;
            }
        }

        /// <summary>
        /// Gets or sets the number of records to process in a single batch.
        /// </summary>
        [JsonInclude, JsonPropertyName("BatchSize")]
        public int BatchSize { get; set; } = 500;

        /// <summary>
        /// Gets or sets a value indicating whether existing subdivision IDs should be overridden during the refresh process.
        /// </summary>
        [JsonInclude, JsonPropertyName("OverrideExistingSubdivisionIds")]
        public bool OverrideExistingSubdivisionIds { get; set; } = false;

        /// <summary>
        /// Gets or sets the distance tolerance used for processing building data.
        /// </summary>
        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}