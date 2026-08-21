using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// What one county partition of the terrain point table holds measured against what a sampling run on a given lattice should have put there.
    /// <para>The question a density figure cannot answer. Density says how much of a county is missing; this says which nodes, so a run that stepped over a batch can be sent back for exactly those rather than for the county.</para>
    /// <para>The expected nodes are derived from the same subdivision outlines and the same lattice the sampling run itself decides against, so the two agree by construction. A coverage worked out from anything else would report holes where the run was never going to sample, and none where it failed.</para>
    /// </summary>
    public class TerrainPointCoverageResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(GridSize))]
        private readonly double gridSize;

        [JsonInclude, JsonPropertyName(nameof(OriginX))]
        private readonly double originX;

        [JsonInclude, JsonPropertyName(nameof(OriginY))]
        private readonly double originY;

        [JsonInclude, JsonPropertyName(nameof(ExpectedCount))]
        private readonly long expectedCount;

        [JsonInclude, JsonPropertyName(nameof(StoredCount))]
        private readonly long storedCount;

        [JsonInclude, JsonPropertyName(nameof(MissingCount))]
        private readonly long missingCount;

        [JsonInclude, JsonPropertyName(nameof(OffGridCount))]
        private readonly long offGridCount;

        [JsonInclude, JsonPropertyName(nameof(Point2Ds_Missing))]
        private readonly List<Point2D> point2Ds_Missing = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointCoverageResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county partition the coverage describes.</param>
        /// <param name="gridSize">The spacing of the lattice the county was measured against.</param>
        /// <param name="originX">The X coordinate the lattice is anchored at.</param>
        /// <param name="originY">The Y coordinate the lattice is anchored at.</param>
        /// <param name="expectedCount">The number of lattice nodes lying inside the county's subdivisions.</param>
        /// <param name="storedCount">The number of those nodes the table holds a point for.</param>
        /// <param name="missingCount">The number of those nodes the table holds no point for.</param>
        /// <param name="offGridCount">The number of stored points that are not nodes of this lattice.</param>
        /// <param name="point2Ds_Missing">A sample of the missing nodes, or null for none.</param>
        public TerrainPointCoverageResult(int countyId, double gridSize, double originX, double originY, long expectedCount, long storedCount, long missingCount, long offGridCount, IEnumerable<Point2D>? point2Ds_Missing)
        {
            this.countyId = countyId;
            this.gridSize = gridSize;
            this.originX = originX;
            this.originY = originY;
            this.expectedCount = expectedCount;
            this.storedCount = storedCount;
            this.missingCount = missingCount;
            this.offGridCount = offGridCount;

            if (point2Ds_Missing is not null)
            {
                // Built by the query that is constructing this result and handed over whole, so the points are
                // taken as they are - only the list itself is copied, to stop the caller's collection aliasing it.
                this.point2Ds_Missing = [.. point2Ds_Missing];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointCoverageResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="terrainPointCoverageResult">The <see cref="TerrainPointCoverageResult"/> to copy from.</param>
        public TerrainPointCoverageResult(TerrainPointCoverageResult? terrainPointCoverageResult)
            : base(terrainPointCoverageResult)
        {
            if (terrainPointCoverageResult is not null)
            {
                countyId = terrainPointCoverageResult.countyId;
                gridSize = terrainPointCoverageResult.gridSize;
                originX = terrainPointCoverageResult.originX;
                originY = terrainPointCoverageResult.originY;
                expectedCount = terrainPointCoverageResult.expectedCount;
                storedCount = terrainPointCoverageResult.storedCount;
                missingCount = terrainPointCoverageResult.missingCount;
                offGridCount = terrainPointCoverageResult.offGridCount;

                foreach (Point2D point2D in terrainPointCoverageResult.point2Ds_Missing)
                {
                    if (Core.Query.Clone(point2D) is Point2D point2D_Clone)
                    {
                        point2Ds_Missing.Add(point2D_Clone);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointCoverageResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public TerrainPointCoverageResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the identifier of the county partition the coverage describes.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the spacing of the lattice the county was measured against.
        /// </summary>
        [JsonIgnore]
        public double GridSize => gridSize;

        /// <summary>
        /// Gets the X coordinate the lattice is anchored at.
        /// </summary>
        [JsonIgnore]
        public double OriginX => originX;

        /// <summary>
        /// Gets the Y coordinate the lattice is anchored at.
        /// </summary>
        [JsonIgnore]
        public double OriginY => originY;

        /// <summary>
        /// Gets the number of lattice nodes lying inside the county's subdivisions.
        /// <para>Nodes of the county's bounding rectangle that fall outside its land are not counted. A county outline is rarely a rectangle, so counting them would report a large and permanent shortfall for every county.</para>
        /// </summary>
        [JsonIgnore]
        public long ExpectedCount => expectedCount;

        /// <summary>
        /// Gets the number of expected nodes the table holds a point for.
        /// </summary>
        [JsonIgnore]
        public long StoredCount => storedCount;

        /// <summary>
        /// Gets the number of expected nodes the table holds no point for.
        /// <para>Reported in full however few of the coordinates were returned alongside it.</para>
        /// </summary>
        [JsonIgnore]
        public long MissingCount => missingCount;

        /// <summary>
        /// Gets the number of stored points that are not nodes of this lattice.
        /// <para>Expected to be zero for a county sampled by this task on these settings. A large figure means the points were sampled on a different anchor or a different spacing, in which case the shortfall reported here is about the question asked rather than about the county.</para>
        /// </summary>
        [JsonIgnore]
        public long OffGridCount => offGridCount;

        /// <summary>
        /// Gets a sample of the missing nodes.
        /// <para>Capped by the caller. <see cref="MissingCount"/> is the whole figure; this is what makes it possible to go and look.</para>
        /// </summary>
        [JsonIgnore]
        public List<Point2D> Point2Ds_Missing => point2Ds_Missing;
    }
}
