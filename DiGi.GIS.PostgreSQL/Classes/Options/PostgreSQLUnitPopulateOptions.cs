using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for populating territorial units from the Central Statistical Office (BDL) API into PostgreSQL.
    /// </summary>
    public class PostgreSQLUnitPopulateOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitPopulateOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object used to initialize the options.</param>
        public PostgreSQLUnitPopulateOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitPopulateOptions"/> class with default settings.
        /// </summary>
        public PostgreSQLUnitPopulateOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitPopulateOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLUnitPopulateOptions">The source options to copy from.</param>
        public PostgreSQLUnitPopulateOptions(PostgreSQLUnitPopulateOptions? postgreSQLUnitPopulateOptions)
            : base(postgreSQLUnitPopulateOptions)
        {
            if (postgreSQLUnitPopulateOptions is not null)
            {
                PageSize = postgreSQLUnitPopulateOptions.PageSize;
                Clear = postgreSQLUnitPopulateOptions.Clear;
                BatchSize = postgreSQLUnitPopulateOptions.BatchSize;
                ClientId = postgreSQLUnitPopulateOptions.ClientId;
            }
        }

        /// <summary>
        /// Gets or sets the page size for paginated requests to the BDL API.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(PageSize))]
        public int PageSize { get; set; } = 100;

        /// <summary>
        /// Gets or sets a value indicating whether existing unit records should be cleared before populating.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Clear))]
        public bool Clear { get; set; } = false;

        /// <summary>
        /// Gets or sets the number of units to insert or update per batch in PostgreSQL.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BatchSize))]
        public int BatchSize { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the optional client identifier (API key) for the Central Statistical Office (BDL) API.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ClientId))]
        public string? ClientId { get; set; }
    }
}
