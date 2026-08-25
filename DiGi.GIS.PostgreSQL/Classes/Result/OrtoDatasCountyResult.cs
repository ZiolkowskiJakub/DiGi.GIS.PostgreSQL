using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// What one county partition of the orthophoto store holds: how many rows, how many of them are filed under a subdivision, and when they were written.
    /// <para><see cref="WithSubdivisionIdCount"/> is the figure to watch across a refresh. A building's subdivision is resolved in another database and pushed across, and it can only ever be gained - a run that lowers this number is clearing subdivisions rather than filling them in, which is the defect of issues #23, #31 and #36.</para>
    /// <para><see cref="CreatedAt_First"/> and <see cref="CreatedAt_Last"/> place the county in time. Downloads are worked through a queue rather than county by county, so unlike the terrain store these do not reconstruct a single run's progress; they say when this county was last added to.</para>
    /// </summary>
    public class OrtoDatasCountyResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(CreatedAt_First))]
        private readonly DateTimeOffset? createdAt_First;

        [JsonInclude, JsonPropertyName(nameof(CreatedAt_Last))]
        private readonly DateTimeOffset? createdAt_Last;

        [JsonInclude, JsonPropertyName(nameof(SubdivisionIdCount))]
        private readonly long subdivisionIdCount;

        [JsonInclude, JsonPropertyName(nameof(WithSubdivisionIdCount))]
        private readonly long withSubdivisionIdCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasCountyResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county partition summarised.</param>
        /// <param name="count">The number of rows the partition holds.</param>
        /// <param name="withSubdivisionIdCount">How many of those rows name a subdivision.</param>
        /// <param name="subdivisionIdCount">How many distinct subdivisions they are spread across.</param>
        /// <param name="createdAt_First">When the earliest of the rows was written, or null when the partition is empty.</param>
        /// <param name="createdAt_Last">When the latest of the rows was written, or null when the partition is empty.</param>
        public OrtoDatasCountyResult(int countyId, long count, long withSubdivisionIdCount, long subdivisionIdCount, DateTimeOffset? createdAt_First, DateTimeOffset? createdAt_Last)
        {
            this.countyId = countyId;
            this.count = count;
            this.withSubdivisionIdCount = withSubdivisionIdCount;
            this.subdivisionIdCount = subdivisionIdCount;
            this.createdAt_First = createdAt_First;
            this.createdAt_Last = createdAt_Last;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasCountyResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="ortoDatasCountyResult">The <see cref="OrtoDatasCountyResult"/> to copy from.</param>
        public OrtoDatasCountyResult(OrtoDatasCountyResult? ortoDatasCountyResult)
            : base(ortoDatasCountyResult)
        {
            if (ortoDatasCountyResult is not null)
            {
                countyId = ortoDatasCountyResult.countyId;
                count = ortoDatasCountyResult.count;
                withSubdivisionIdCount = ortoDatasCountyResult.withSubdivisionIdCount;
                subdivisionIdCount = ortoDatasCountyResult.subdivisionIdCount;
                createdAt_First = ortoDatasCountyResult.createdAt_First;
                createdAt_Last = ortoDatasCountyResult.createdAt_Last;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasCountyResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public OrtoDatasCountyResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of rows the partition holds.
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the identifier of the county partition summarised.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets when the earliest of the partition's rows was written, or null when it holds none.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? CreatedAt_First => createdAt_First;

        /// <summary>
        /// Gets when the latest of the partition's rows was written, or null when it holds none.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? CreatedAt_Last => createdAt_Last;

        /// <summary>
        /// Gets how many distinct subdivisions the partition's rows are spread across.
        /// <para>One, on a county that has many, is the mark of a subdivision that was applied wholesale rather than resolved per building.</para>
        /// </summary>
        [JsonIgnore]
        public long SubdivisionIdCount => subdivisionIdCount;

        /// <summary>
        /// Gets how many of the partition's rows name a subdivision.
        /// </summary>
        [JsonIgnore]
        public long WithSubdivisionIdCount => withSubdivisionIdCount;

        /// <summary>
        /// Gets how many of the partition's rows name no subdivision.
        /// <para>Derived from the two stored counts rather than counted separately, so it cannot disagree with them.</para>
        /// </summary>
        [JsonIgnore]
        public long WithoutSubdivisionIdCount => count - withSubdivisionIdCount;
    }
}
