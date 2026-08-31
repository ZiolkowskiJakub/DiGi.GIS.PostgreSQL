using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for populating statistical data collections from .sdcf files into PostgreSQL.
    /// </summary>
    public class PostgreSQLStatisticalDataCollectionPopulateOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLStatisticalDataCollectionPopulateOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object used to initialize the options.</param>
        public PostgreSQLStatisticalDataCollectionPopulateOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLStatisticalDataCollectionPopulateOptions"/> class with default settings.
        /// </summary>
        public PostgreSQLStatisticalDataCollectionPopulateOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLStatisticalDataCollectionPopulateOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLStatisticalDataCollectionPopulateOptions">The source options to copy from.</param>
        public PostgreSQLStatisticalDataCollectionPopulateOptions(PostgreSQLStatisticalDataCollectionPopulateOptions? postgreSQLStatisticalDataCollectionPopulateOptions)
            : base(postgreSQLStatisticalDataCollectionPopulateOptions)
        {
            if (postgreSQLStatisticalDataCollectionPopulateOptions is not null)
            {
                Path = postgreSQLStatisticalDataCollectionPopulateOptions.Path;
                Clear = postgreSQLStatisticalDataCollectionPopulateOptions.Clear;
                BatchSize = postgreSQLStatisticalDataCollectionPopulateOptions.BatchSize;
                CommandTimeout = postgreSQLStatisticalDataCollectionPopulateOptions.CommandTimeout;
            }
        }

        /// <summary>
        /// Gets or sets the file path or directory path containing .sdcf statistical data collection files.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Path))]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether existing statistical data collection records should be cleared before populating.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Clear))]
        public bool Clear { get; set; } = false;

        /// <summary>
        /// Gets or sets the maximum number of statistical data collections to insert or update per batch in PostgreSQL.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BatchSize))]
        public int BatchSize { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the timeout in seconds for database commands executed during population.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CommandTimeout))]
        public int CommandTimeout { get; set; } = 600;
    }
}
