using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides options for enqueuing the orthophoto downloads a county is short of.
    /// </summary>
    public class PostgreSQLOrtoDatasRefreshOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshOptions" /> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public PostgreSQLOrtoDatasRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshOptions" /> class.
        /// </summary>
        public PostgreSQLOrtoDatasRefreshOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshOptions" /> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLOrtoDatasRefreshOptions">The source options instance to copy from.</param>
        public PostgreSQLOrtoDatasRefreshOptions(PostgreSQLOrtoDatasRefreshOptions postgreSQLOrtoDatasRefreshOptions)
            : base(postgreSQLOrtoDatasRefreshOptions)
        {
            if (postgreSQLOrtoDatasRefreshOptions is not null)
            {
                BatchSize = postgreSQLOrtoDatasRefreshOptions.BatchSize;
                CommandTimeout = postgreSQLOrtoDatasRefreshOptions.CommandTimeout;
                CountyIds = postgreSQLOrtoDatasRefreshOptions.CountyIds == null ? null : [.. postgreSQLOrtoDatasRefreshOptions.CountyIds];
                OverrideExisting = postgreSQLOrtoDatasRefreshOptions.OverrideExisting;
                UpdateSubdivisionIds = postgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds;
            }
        }

        /// <summary>
        /// Gets or sets how many of a county's building references are carried to the database at a time.
        /// <para>A county is tens of thousands of references and the largest are over a hundred thousand, which is far past what one statement should carry. Each chunk is one round trip per stage, so the value trades statement size against round trip count.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BatchSize))]
        public int BatchSize { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the timeout in seconds applied to every statement the refresh issues. A value of 0 disables the timeout.
        /// <para>Well above the 30 second default, because these are bulk statements over a partitioned table: the reads and writes of a single chunk can each take minutes on a county that has never been refreshed.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CommandTimeout))]
        public int CommandTimeout { get; set; } = 600;

        /// <summary>
        /// Gets or sets the set of county identifiers for which data should be refreshed. Null refreshes every county.
        /// <para>Identifiers rather than codes: a county whose territory is in several pieces is held as one row per piece, each with its own identifier, so a code names several of them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether references already stored are enqueued again.
        /// <para>False enqueues only what is missing, which is what a routine top-up wants. True re-enqueues the whole county, so the download runs again for buildings that already have orthophoto data.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(OverrideExisting))]
        public bool OverrideExisting { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether each building's subdivision identifier is pushed onto its stored row before the enqueue.
        /// <para>A second, independent effect of a run: it copies <c>building_2d.subdivision_id</c> onto the matching <c>orto_datas</c> row. A building whose subdivision has not been resolved is skipped rather than clearing the stored one.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(UpdateSubdivisionIds))]
        public bool UpdateSubdivisionIds { get; set; } = true;
    }
}
