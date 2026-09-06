using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Options for re-filing 2D buildings under the county polygon part their footprint lies in.
    /// <para><see cref="DryRun"/> defaults to <see langword="true"/>: the run reports what it would move and writes nothing until it is turned off deliberately.</para>
    /// </summary>
    public class PostgreSQLBuilding2DCountyPartRefreshOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DCountyPartRefreshOptions" /> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object used to initialize the options.</param>
        public PostgreSQLBuilding2DCountyPartRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DCountyPartRefreshOptions" /> class.
        /// </summary>
        public PostgreSQLBuilding2DCountyPartRefreshOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DCountyPartRefreshOptions" /> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLBuilding2DCountyPartRefreshOptions">The source options to copy from.</param>
        public PostgreSQLBuilding2DCountyPartRefreshOptions(PostgreSQLBuilding2DCountyPartRefreshOptions postgreSQLBuilding2DCountyPartRefreshOptions)
            : base(postgreSQLBuilding2DCountyPartRefreshOptions)
        {
            if (postgreSQLBuilding2DCountyPartRefreshOptions is not null)
            {
                BatchSize = postgreSQLBuilding2DCountyPartRefreshOptions.BatchSize;
                Codes = postgreSQLBuilding2DCountyPartRefreshOptions.Codes is null ? null : [.. postgreSQLBuilding2DCountyPartRefreshOptions.Codes];
                DryRun = postgreSQLBuilding2DCountyPartRefreshOptions.DryRun;
                ReferencedObjects = postgreSQLBuilding2DCountyPartRefreshOptions.ReferencedObjects;
                ReportDirectory = postgreSQLBuilding2DCountyPartRefreshOptions.ReportDirectory;
                Tolerance = postgreSQLBuilding2DCountyPartRefreshOptions.Tolerance;
            }
        }

        /// <summary>
        /// Gets or sets the number of building rows read out of the database in one page.
        /// </summary>
        [JsonInclude, JsonPropertyName("BatchSize")]
        public int BatchSize { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the county codes to examine. When null every code holding more than one polygon part is examined.
        /// </summary>
        [JsonInclude, JsonPropertyName("Codes")]
        public List<string>? Codes { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether the run only reports what it would do. Defaults to <see langword="true"/>; nothing is written until it is turned off.
        /// </summary>
        [JsonInclude, JsonPropertyName("DryRun")]
        public bool DryRun { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the rows keyed on a moved building - its 3D buildings, models, year built, occupancy, orthophotos and building data - are carried onto the same county part.
        /// <para>Leaving them behind makes them unreachable: every read of those tables filters on <c>county_id</c> first, so a row still filed under the part the building has left answers nothing. Turn it off only to move <c>building_2d</c> alone and knowingly accept that.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("ReferencedObjects")]
        public bool ReferencedObjects { get; set; } = true;

        /// <summary>
        /// Gets or sets the directory the report files are written into. When null the directory the application was launched from is used.
        /// </summary>
        [JsonInclude, JsonPropertyName("ReportDirectory")]
        public string? ReportDirectory { get; set; } = null;

        /// <summary>
        /// Gets or sets the distance tolerance used for the bounding box and containment tests.
        /// </summary>
        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}
