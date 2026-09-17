using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides options for updating the external components area columns of the building data table from the stored building models.
    /// </summary>
    public class PostgreSQLBuildingDataExternalComponentsUpdateOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataExternalComponentsUpdateOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public PostgreSQLBuildingDataExternalComponentsUpdateOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataExternalComponentsUpdateOptions"/> class.
        /// </summary>
        public PostgreSQLBuildingDataExternalComponentsUpdateOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataExternalComponentsUpdateOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLBuildingDataExternalComponentsUpdateOptions">The source options instance to copy from.</param>
        public PostgreSQLBuildingDataExternalComponentsUpdateOptions(PostgreSQLBuildingDataExternalComponentsUpdateOptions postgreSQLBuildingDataExternalComponentsUpdateOptions)
            : base(postgreSQLBuildingDataExternalComponentsUpdateOptions)
        {
            if (postgreSQLBuildingDataExternalComponentsUpdateOptions is not null)
            {
                CommandTimeout = postgreSQLBuildingDataExternalComponentsUpdateOptions.CommandTimeout;
                CountyIds = postgreSQLBuildingDataExternalComponentsUpdateOptions.CountyIds == null ? null : [.. postgreSQLBuildingDataExternalComponentsUpdateOptions.CountyIds];
            }
        }

        /// <summary>
        /// Gets or sets the timeout in seconds applied to every statement the update issues. A value of 0 disables the timeout.
        /// <para>Well above the 30 second default, because these are bulk reads and writes over a partitioned table: a county can carry tens of thousands of building models, and the push writes every external components column of each of them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CommandTimeout))]
        public int CommandTimeout { get; set; } = 600;

        /// <summary>
        /// Gets or sets the set of county identifiers the run is limited to. Null updates every county.
        /// <para>Identifiers rather than codes: a county whose territory is in several pieces is held as one row per piece, each with its own identifier, so a code names several of them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;
    }
}
