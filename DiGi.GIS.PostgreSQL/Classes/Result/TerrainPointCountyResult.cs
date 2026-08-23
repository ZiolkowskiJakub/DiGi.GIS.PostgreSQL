using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// What one county partition of the terrain point table holds: how many points, over what extent, at what elevations, and when they were written.
    /// <para>This is the report a finished sampling run leaves behind. The run itself keeps its tallies in memory and discards them when it ends, so the only lasting account of what it did is the table - and reading that account is what tells a partial run apart from a complete one without sampling the county again.</para>
    /// <para>The two moments are offsets rather than plain dates. A plain date does not survive the round trip through JSON: a moment written as UTC is read back as local time, so serialising the result twice produces two different documents and a reader cannot tell a moment from a rendering of it. An offset carries its own zone in both directions.</para>
    /// <para><see cref="CreatedAt_First"/> and <see cref="CreatedAt_Last"/> are what place a county in time. A run walks the counties in ascending identifier order, so ordering the summaries by <see cref="CreatedAt_First"/> reconstructs how far it got, and the largest <see cref="CreatedAt_Last"/> across all counties is the moment it stopped writing.</para>
    /// </summary>
    public class TerrainPointCountyResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(MinX))]
        private readonly double minX;

        [JsonInclude, JsonPropertyName(nameof(MaxX))]
        private readonly double maxX;

        [JsonInclude, JsonPropertyName(nameof(MinY))]
        private readonly double minY;

        [JsonInclude, JsonPropertyName(nameof(MaxY))]
        private readonly double maxY;

        [JsonInclude, JsonPropertyName(nameof(MinZ))]
        private readonly double minZ;

        [JsonInclude, JsonPropertyName(nameof(MaxZ))]
        private readonly double maxZ;

        [JsonInclude, JsonPropertyName(nameof(ZeroElevationCount))]
        private readonly long zeroElevationCount;

        [JsonInclude, JsonPropertyName(nameof(SubdivisionCount))]
        private readonly long subdivisionCount;

        [JsonInclude, JsonPropertyName(nameof(UnassignedSubdivisionCount))]
        private readonly long unassignedSubdivisionCount;

        [JsonInclude, JsonPropertyName(nameof(CreatedAt_First))]
        private readonly DateTimeOffset? createdAt_First;

        [JsonInclude, JsonPropertyName(nameof(CreatedAt_Last))]
        private readonly DateTimeOffset? createdAt_Last;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointCountyResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county partition the summary describes.</param>
        /// <param name="count">The number of points stored for the county.</param>
        /// <param name="minX">The smallest X coordinate stored for the county.</param>
        /// <param name="maxX">The largest X coordinate stored for the county.</param>
        /// <param name="minY">The smallest Y coordinate stored for the county.</param>
        /// <param name="maxY">The largest Y coordinate stored for the county.</param>
        /// <param name="minZ">The lowest elevation stored for the county.</param>
        /// <param name="maxZ">The highest elevation stored for the county.</param>
        /// <param name="zeroElevationCount">The number of points stored at exactly zero elevation.</param>
        /// <param name="subdivisionCount">The number of distinct subdivisions the points are filed under.</param>
        /// <param name="unassignedSubdivisionCount">The number of points filed under no subdivision.</param>
        /// <param name="createdAt_First">The moment the earliest point of the county was written, or null when none carries one.</param>
        /// <param name="createdAt_Last">The moment the latest point of the county was written, or null when none carries one.</param>
        public TerrainPointCountyResult(int countyId, long count, double minX, double maxX, double minY, double maxY, double minZ, double maxZ, long zeroElevationCount, long subdivisionCount, long unassignedSubdivisionCount, DateTimeOffset? createdAt_First, DateTimeOffset? createdAt_Last)
        {
            this.countyId = countyId;
            this.count = count;
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.minZ = minZ;
            this.maxZ = maxZ;
            this.zeroElevationCount = zeroElevationCount;
            this.subdivisionCount = subdivisionCount;
            this.unassignedSubdivisionCount = unassignedSubdivisionCount;
            this.createdAt_First = createdAt_First;
            this.createdAt_Last = createdAt_Last;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointCountyResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="terrainPointCountyResult">The <see cref="TerrainPointCountyResult"/> to copy from.</param>
        public TerrainPointCountyResult(TerrainPointCountyResult? terrainPointCountyResult)
            : base(terrainPointCountyResult)
        {
            if (terrainPointCountyResult is not null)
            {
                countyId = terrainPointCountyResult.countyId;
                count = terrainPointCountyResult.count;
                minX = terrainPointCountyResult.minX;
                maxX = terrainPointCountyResult.maxX;
                minY = terrainPointCountyResult.minY;
                maxY = terrainPointCountyResult.maxY;
                minZ = terrainPointCountyResult.minZ;
                maxZ = terrainPointCountyResult.maxZ;
                zeroElevationCount = terrainPointCountyResult.zeroElevationCount;
                subdivisionCount = terrainPointCountyResult.subdivisionCount;
                unassignedSubdivisionCount = terrainPointCountyResult.unassignedSubdivisionCount;
                createdAt_First = terrainPointCountyResult.createdAt_First;
                createdAt_Last = terrainPointCountyResult.createdAt_Last;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointCountyResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public TerrainPointCountyResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the identifier of the county partition the summary describes.
        /// <para>A county whose territory is in several pieces is one partition per piece, and each is summarised on its own. Adding two of them together describes no area that exists.</para>
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the number of points stored for the county.
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the smallest X coordinate stored for the county.
        /// </summary>
        [JsonIgnore]
        public double MinX => minX;

        /// <summary>
        /// Gets the largest X coordinate stored for the county.
        /// </summary>
        [JsonIgnore]
        public double MaxX => maxX;

        /// <summary>
        /// Gets the smallest Y coordinate stored for the county.
        /// </summary>
        [JsonIgnore]
        public double MinY => minY;

        /// <summary>
        /// Gets the largest Y coordinate stored for the county.
        /// </summary>
        [JsonIgnore]
        public double MaxY => maxY;

        /// <summary>
        /// Gets the lowest elevation stored for the county.
        /// </summary>
        [JsonIgnore]
        public double MinZ => minZ;

        /// <summary>
        /// Gets the highest elevation stored for the county.
        /// </summary>
        [JsonIgnore]
        public double MaxZ => maxZ;

        /// <summary>
        /// Gets the number of points stored at exactly zero elevation.
        /// <para>The public elevation model answers <c>0</c> with a success status for a coordinate outside its coverage or over water bodies. <see cref="GIS.Query.ElevationAsync(System.Net.Http.HttpClient, Geometry.Planar.Classes.Point2D)"/> filters these sentinels as unresolved points, and <see cref="TerrainPointPostgreSQLConverter.DeleteZeroElevationsAsync(Npgsql.NpgsqlConnection, System.Collections.Generic.IEnumerable{int}, int, System.Threading.CancellationToken)"/> purges historical sentinels.</para>
        /// </summary>
        [JsonIgnore]
        public long ZeroElevationCount => zeroElevationCount;

        /// <summary>
        /// Gets the number of distinct subdivisions the points are filed under.
        /// </summary>
        [JsonIgnore]
        public long SubdivisionCount => subdivisionCount;

        /// <summary>
        /// Gets the number of points filed under no subdivision.
        /// <para>A sampling run assigns every point the subdivision that contains it, so anything counted here came from another source.</para>
        /// </summary>
        [JsonIgnore]
        public long UnassignedSubdivisionCount => unassignedSubdivisionCount;

        /// <summary>
        /// Gets the moment the earliest point of the county was written, or null when none carries one.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? CreatedAt_First => createdAt_First;

        /// <summary>
        /// Gets the moment the latest point of the county was written, or null when none carries one.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? CreatedAt_Last => createdAt_Last;
    }
}
