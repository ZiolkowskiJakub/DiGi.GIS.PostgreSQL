using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for carrying the rows of the <c>unique_id</c>-keyed tables - <c>building_model</c>, <c>year_built_data</c> and <c>occupancy_data_building_2d</c> - onto the county part their building sits on.
    /// <para><see cref="DryRun"/> defaults to <see langword="true"/>: the run reports, per table, which references would move and which the destination would refuse, and writes nothing until it is turned off deliberately.</para>
    /// </summary>
    public class PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions" /> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object used to initialize the options.</param>
        public PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions" /> class.
        /// </summary>
        public PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions" /> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions">The source options to copy from.</param>
        public PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions(PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions)
            : base(postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions)
        {
            if (postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions is not null)
            {
                BatchSize = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions.BatchSize;
                Codes = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions.Codes is null ? null : [.. postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions.Codes];
                CommandTimeout = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions.CommandTimeout;
                DryRun = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions.DryRun;
                ReportDirectory = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshOptions.ReportDirectory;
            }
        }

        /// <summary>
        /// Gets or sets the number of references sent in one statement. Matches the mover's default, so the task and the mover cannot drift apart.
        /// </summary>
        [JsonInclude, JsonPropertyName("BatchSize")]
        public int BatchSize { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the county codes to examine. When null every code holding more than one polygon part is examined; a named code with a single part is examined as well, because its referenced objects can still sit under another code's part.
        /// </summary>
        [JsonInclude, JsonPropertyName("Codes")]
        public List<string>? Codes { get; set; } = null;

        /// <summary>
        /// Gets or sets the timeout in seconds for the statements the run executes. Matches the mover's default.
        /// </summary>
        [JsonInclude, JsonPropertyName("CommandTimeout")]
        public int CommandTimeout { get; set; } = 600;

        /// <summary>
        /// Gets or sets a value indicating whether the run only reports what it would do. Defaults to <see langword="true"/>; nothing is written until it is turned off.
        /// <para>The report a dry run produces is what the move should be reviewed against: per table, the references that would move and the ones the destination part would refuse because it already holds the stored object.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("DryRun")]
        public bool DryRun { get; set; } = true;

        /// <summary>
        /// Gets or sets the directory the report files are written into. When null the directory the application was launched from is used.
        /// </summary>
        [JsonInclude, JsonPropertyName("ReportDirectory")]
        public string? ReportDirectory { get; set; } = null;
    }
}
