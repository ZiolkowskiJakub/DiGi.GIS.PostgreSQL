using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for populating territorial units from local JSON files into PostgreSQL.
    /// </summary>
    public class PostgreSQLUnitInsertFromFileOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitInsertFromFileOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object used to initialize the options.</param>
        public PostgreSQLUnitInsertFromFileOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitInsertFromFileOptions"/> class with default settings.
        /// </summary>
        public PostgreSQLUnitInsertFromFileOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitInsertFromFileOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLUnitInsertFromFileOptions">The source options to copy from.</param>
        public PostgreSQLUnitInsertFromFileOptions(PostgreSQLUnitInsertFromFileOptions? postgreSQLUnitInsertFromFileOptions)
            : base(postgreSQLUnitInsertFromFileOptions)
        {
            if (postgreSQLUnitInsertFromFileOptions is not null)
            {
                Path = postgreSQLUnitInsertFromFileOptions.Path;
                Clear = postgreSQLUnitInsertFromFileOptions.Clear;
                BatchSize = postgreSQLUnitInsertFromFileOptions.BatchSize;
                CommandTimeout = postgreSQLUnitInsertFromFileOptions.CommandTimeout;
            }
        }

        /// <summary>
        /// Gets or sets the file path or directory path containing territorial unit JSON files.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Path))]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether existing territorial unit records should be cleared before populating.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Clear))]
        public bool Clear { get; set; } = false;

        /// <summary>
        /// Gets or sets the maximum number of territorial units to insert or update per batch in PostgreSQL.
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
