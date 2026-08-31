using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides options for updating the building data table from Building2D and the other data sources.
    /// </summary>
    public class PostgreSQLBuildingDataUpdateOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataUpdateOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public PostgreSQLBuildingDataUpdateOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataUpdateOptions"/> class.
        /// </summary>
        public PostgreSQLBuildingDataUpdateOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataUpdateOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLBuildingDataUpdateOptions">The source options instance to copy from.</param>
        public PostgreSQLBuildingDataUpdateOptions(PostgreSQLBuildingDataUpdateOptions postgreSQLBuildingDataUpdateOptions)
            : base(postgreSQLBuildingDataUpdateOptions)
        {
            if (postgreSQLBuildingDataUpdateOptions is not null)
            {
                BuildingDataUpdateTypes = postgreSQLBuildingDataUpdateOptions.BuildingDataUpdateTypes == null ? null : [.. postgreSQLBuildingDataUpdateOptions.BuildingDataUpdateTypes];
                CommandTimeout = postgreSQLBuildingDataUpdateOptions.CommandTimeout;
                CountyIds = postgreSQLBuildingDataUpdateOptions.CountyIds == null ? null : [.. postgreSQLBuildingDataUpdateOptions.CountyIds];
                Radiuses = postgreSQLBuildingDataUpdateOptions.Radiuses == null ? null : [.. postgreSQLBuildingDataUpdateOptions.Radiuses];
                Years = postgreSQLBuildingDataUpdateOptions.Years == null ? null : new(postgreSQLBuildingDataUpdateOptions.Years);
            }
        }

        /// <summary>
        /// Gets or sets the collection of building data update types that specify which types of building data updates should be performed.
        /// <para>An empty or null collection means there is nothing to do and the run ends without reading anything.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BuildingDataUpdateTypes))]
        public HashSet<BuildingDataUpdateType>? BuildingDataUpdateTypes { get; set; } = [BuildingDataUpdateType.General, BuildingDataUpdateType.Database];

        /// <summary>
        /// Gets or sets the timeout in seconds applied to every statement the update issues. A value of 0 disables the timeout.
        /// <para>Well above the 30 second default, because these are bulk reads and writes over a partitioned table: a single subdivision can carry tens of thousands of buildings and the push writes every derived column of each of them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CommandTimeout))]
        public int CommandTimeout { get; set; } = 600;

        /// <summary>
        /// Gets or sets the set of county identifiers the run is limited to. Null updates every county.
        /// <para>Identifiers rather than codes: a county whose territory is in several pieces is held as one row per piece, each with its own identifier, so a code names several of them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets the radiuses in metres the radial building coverage and floor area ratios are measured over.
        /// <para>These values name columns rather than merely scaling them - each radius produces its own pair of columns - so changing the set changes the shape of the stored table rather than the numbers in it. The largest value also sets how far around each building the neighbour search reaches.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Radiuses))]
        public List<double>? Radiuses { get; set; } = [200, 400, 600, 1000];

        /// <summary>
        /// Gets or sets the range of years for statistical demographic data series updates.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Years))]
        public Range<int>? Years { get; set; } = new(2008, 2025);
    }
}
