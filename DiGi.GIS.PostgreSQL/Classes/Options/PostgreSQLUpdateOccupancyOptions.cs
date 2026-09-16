using DiGi.Core.Classes;
using System.Collections.Generic;
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
                CountyIds = postgreSQLUpdateOccupancyOptions.CountyIds is null ? null : [.. postgreSQLUpdateOccupancyOptions.CountyIds];
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
        /// <para>With <see cref="CountyIds"/> set, the building side is not truncated: only the rows of the buildings in the named counties are removed before they are rewritten, so a building whose subdivision carries no figure does not keep a stale row from an earlier run. The administrative side is always truncated whole when cleared, because its roll-up is always written whole.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Clear))]
        public bool Clear { get; set; } = true;

        /// <summary>
        /// Gets or sets the county polygon part identifiers the building side of the update is limited to. <see langword="null"/> means every county.
        /// <para>Scopes <see cref="IncludeBuilding2Ds"/> only. The administrative roll-up - subdivision, municipality, county, voivodeship, country - is a sum over the whole hierarchy and stays nationwide whatever is named here. A county code is not a key - a multi-part county has one identifier per polygon part - so name every part that is wanted.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;
    }
}