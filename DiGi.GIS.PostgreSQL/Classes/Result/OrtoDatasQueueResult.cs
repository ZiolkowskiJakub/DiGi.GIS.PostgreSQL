using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// What one county still has waiting in the orthophoto download queue.
    /// <para>The queue is worked through by deleting the rows that are handed out, so its depth is the work outstanding rather than the work ever scheduled. A refresh run appends to it and the download task drains it, which makes this the one figure that shows the two moving against each other.</para>
    /// <para><see cref="WithSubdivisionIdCount"/> matters for a different reason: the queue carries the subdivision each building belongs to, and a download that does not carry it through to the stored row is issue #36. Entries waiting here with a subdivision, against rows stored without one, is that defect measured.</para>
    /// </summary>
    public class OrtoDatasQueueResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(CreatedAt_First))]
        private readonly DateTimeOffset? createdAt_First;

        [JsonInclude, JsonPropertyName(nameof(CreatedAt_Last))]
        private readonly DateTimeOffset? createdAt_Last;

        [JsonInclude, JsonPropertyName(nameof(WithSubdivisionIdCount))]
        private readonly long withSubdivisionIdCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasQueueResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county the queued entries belong to.</param>
        /// <param name="count">How many entries are waiting.</param>
        /// <param name="withSubdivisionIdCount">How many of them name a subdivision.</param>
        /// <param name="createdAt_First">When the oldest waiting entry was queued, or null when none are.</param>
        /// <param name="createdAt_Last">When the newest waiting entry was queued, or null when none are.</param>
        public OrtoDatasQueueResult(int countyId, long count, long withSubdivisionIdCount, DateTimeOffset? createdAt_First, DateTimeOffset? createdAt_Last)
        {
            this.countyId = countyId;
            this.count = count;
            this.withSubdivisionIdCount = withSubdivisionIdCount;
            this.createdAt_First = createdAt_First;
            this.createdAt_Last = createdAt_Last;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasQueueResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="ortoDatasQueueResult">The <see cref="OrtoDatasQueueResult"/> to copy from.</param>
        public OrtoDatasQueueResult(OrtoDatasQueueResult? ortoDatasQueueResult)
            : base(ortoDatasQueueResult)
        {
            if (ortoDatasQueueResult is not null)
            {
                countyId = ortoDatasQueueResult.countyId;
                count = ortoDatasQueueResult.count;
                withSubdivisionIdCount = ortoDatasQueueResult.withSubdivisionIdCount;
                createdAt_First = ortoDatasQueueResult.createdAt_First;
                createdAt_Last = ortoDatasQueueResult.createdAt_Last;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasQueueResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public OrtoDatasQueueResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets how many entries are waiting for this county.
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the identifier of the county the queued entries belong to.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets when the oldest waiting entry was queued, or null when none are.
        /// <para>How far behind the download has fallen. Entries are handed out oldest first, so this is the age of the next one out.</para>
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? CreatedAt_First => createdAt_First;

        /// <summary>
        /// Gets when the newest waiting entry was queued, or null when none are.
        /// <para>When a refresh last added to this county, provided the download has not since drained past it.</para>
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? CreatedAt_Last => createdAt_Last;

        /// <summary>
        /// Gets how many of the waiting entries name a subdivision.
        /// </summary>
        [JsonIgnore]
        public long WithSubdivisionIdCount => withSubdivisionIdCount;

        /// <summary>
        /// Gets how many of the waiting entries name no subdivision.
        /// <para>Derived from the two stored counts rather than counted separately, so it cannot disagree with them.</para>
        /// </summary>
        [JsonIgnore]
        public long WithoutSubdivisionIdCount => count - withSubdivisionIdCount;
    }
}
