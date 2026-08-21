using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides options for going back for the terrain points a sampling run left behind.
    /// </summary>
    public class PostgreSQLTerrainPointFillGapsOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLTerrainPointFillGapsOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public PostgreSQLTerrainPointFillGapsOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLTerrainPointFillGapsOptions"/> class.
        /// </summary>
        public PostgreSQLTerrainPointFillGapsOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLTerrainPointFillGapsOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLTerrainPointFillGapsOptions">The source options instance to copy from.</param>
        public PostgreSQLTerrainPointFillGapsOptions(PostgreSQLTerrainPointFillGapsOptions postgreSQLTerrainPointFillGapsOptions)
            : base(postgreSQLTerrainPointFillGapsOptions)
        {
            if (postgreSQLTerrainPointFillGapsOptions is not null)
            {
                BatchSize = postgreSQLTerrainPointFillGapsOptions.BatchSize;
                CountyIds = postgreSQLTerrainPointFillGapsOptions.CountyIds == null ? null : [.. postgreSQLTerrainPointFillGapsOptions.CountyIds];
                GridSize = postgreSQLTerrainPointFillGapsOptions.GridSize;
                MaxConcurrentRequests = postgreSQLTerrainPointFillGapsOptions.MaxConcurrentRequests;
                OriginX = postgreSQLTerrainPointFillGapsOptions.OriginX;
                OriginY = postgreSQLTerrainPointFillGapsOptions.OriginY;
                RetryCount = postgreSQLTerrainPointFillGapsOptions.RetryCount;
                RetryDelayMilliseconds = postgreSQLTerrainPointFillGapsOptions.RetryDelayMilliseconds;
                TileSize = postgreSQLTerrainPointFillGapsOptions.TileSize;
                Tolerance = postgreSQLTerrainPointFillGapsOptions.Tolerance;
            }
        }

        /// <summary>
        /// Gets or sets how many missing nodes are asked for at once.
        /// <para>Every point is one request, and the elevation query holds a whole list in flight against <see cref="MaxConcurrentRequests"/> rather than dividing it up, so a county with a great many gaps is worked through a batch at a time.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BatchSize))]
        public int BatchSize { get; set; } = 1024;

        /// <summary>
        /// Gets or sets the counties to repair, by identifier. Null repairs every county.
        /// <para>Identifiers rather than codes: a county whose territory is in several pieces is held as one row per piece, each with its own identifier and its own subdivisions, so a code names several of them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets the spacing of the lattice a county is measured against, in model units.
        /// <para>This has to be the spacing the county was actually sampled at, because it is what decides which nodes count as missing. Set finer than the county holds and every node in between reads as a gap, which turns a repair into a full densification of the country.</para>
        /// <para>The default of 100 is what the whole of the store presently sits on. Check with the density or coverage endpoints before changing it.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(GridSize))]
        public double GridSize { get; set; } = 100;

        /// <summary>
        /// Gets or sets how many elevation requests may be in flight at once.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(MaxConcurrentRequests))]
        public int MaxConcurrentRequests { get; set; } = 16;

        /// <summary>
        /// Gets or sets the X coordinate the lattice is anchored at.
        /// <para>Leave at zero, matching the sampling task. A different anchor describes a different lattice, on which every stored point is off grid and every node is missing.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(OriginX))]
        public double OriginX { get; set; } = 0;

        /// <summary>
        /// Gets or sets the Y coordinate the lattice is anchored at.
        /// <para>Leave at zero, for the reasons given on <see cref="OriginX"/>.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(OriginY))]
        public double OriginY { get; set; } = 0;

        /// <summary>
        /// Gets or sets how many times a request the elevation service refused for a transient reason is sent again.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(RetryCount))]
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// Gets or sets the delay before the first retry, in milliseconds, doubling for each attempt after that.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(RetryDelayMilliseconds))]
        public double RetryDelayMilliseconds { get; set; } = 500;

        /// <summary>
        /// Gets or sets the edge of one measuring tile, counted in lattice steps.
        /// <para>Governs only how the county is walked while its gaps are found, not how they are then filled. Matches the sampling task so that both walk the same tiles.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(TileSize))]
        public int TileSize { get; set; } = 128;

        /// <summary>
        /// Gets or sets the distance tolerance.
        /// <para>Used both when deciding whether a node falls within an area and when matching a stored point to the node it belongs to. It is capped at half of <see cref="GridSize"/> while the task runs, so that a point can never be taken for its neighbour.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Tolerance))]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}
