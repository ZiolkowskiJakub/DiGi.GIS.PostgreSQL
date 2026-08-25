using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of an orthophoto refresh: what it queued, what it left alone, and what it could not reach.
    /// <para>A refresh walks every county it was given and steps over any that fails, so the run finishing is not by itself evidence that it did what it set out to do. <see cref="FailedCountyCount"/> is the figure that tells the two apart, and each of those counties is logged with the exception that stopped it.</para>
    /// <para><see cref="EnqueuedCount"/> counts rows accepted by the queue, not buildings looked at. A reference already waiting in the queue conflicts and is not counted twice, so a run repeated straight away reports far fewer than the first.</para>
    /// </summary>
    public class PostgreSQLOrtoDatasRefreshResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(CountyCount))]
        private readonly long countyCount;

        [JsonInclude, JsonPropertyName(nameof(EnqueuedCount))]
        private readonly long enqueuedCount;

        [JsonInclude, JsonPropertyName(nameof(FailedCountyCount))]
        private readonly long failedCountyCount;

        [JsonInclude, JsonPropertyName(nameof(ReadCount))]
        private readonly long readCount;

        [JsonInclude, JsonPropertyName(nameof(SubdivisionIdCount))]
        private readonly long subdivisionIdCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshResult"/> class.
        /// </summary>
        /// <param name="countyCount">The number of counties the run was scoped to.</param>
        /// <param name="readCount">The number of building references read out of the building table.</param>
        /// <param name="enqueuedCount">The number of references the queue accepted.</param>
        /// <param name="subdivisionIdCount">The number of stored rows that had a subdivision identifier written to them.</param>
        /// <param name="failedCountyCount">The number of counties that failed outright and were stepped over.</param>
        /// <param name="cancelled">Whether the run was cancelled before it reached the end of its counties.</param>
        public PostgreSQLOrtoDatasRefreshResult(long countyCount, long readCount, long enqueuedCount, long subdivisionIdCount, long failedCountyCount, bool cancelled)
        {
            this.countyCount = countyCount;
            this.readCount = readCount;
            this.enqueuedCount = enqueuedCount;
            this.subdivisionIdCount = subdivisionIdCount;
            this.failedCountyCount = failedCountyCount;
            this.cancelled = cancelled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="postgreSQLOrtoDatasRefreshResult">The <see cref="PostgreSQLOrtoDatasRefreshResult"/> to copy from.</param>
        public PostgreSQLOrtoDatasRefreshResult(PostgreSQLOrtoDatasRefreshResult? postgreSQLOrtoDatasRefreshResult)
            : base(postgreSQLOrtoDatasRefreshResult)
        {
            if (postgreSQLOrtoDatasRefreshResult is not null)
            {
                countyCount = postgreSQLOrtoDatasRefreshResult.countyCount;
                readCount = postgreSQLOrtoDatasRefreshResult.readCount;
                enqueuedCount = postgreSQLOrtoDatasRefreshResult.enqueuedCount;
                subdivisionIdCount = postgreSQLOrtoDatasRefreshResult.subdivisionIdCount;
                failedCountyCount = postgreSQLOrtoDatasRefreshResult.failedCountyCount;
                cancelled = postgreSQLOrtoDatasRefreshResult.cancelled;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public PostgreSQLOrtoDatasRefreshResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the run was cancelled before it reached the end of its counties.
        /// </summary>
        [JsonIgnore]
        public bool Cancelled => cancelled;

        /// <summary>
        /// Gets the number of counties the run was scoped to, whether or not each of them succeeded.
        /// </summary>
        [JsonIgnore]
        public long CountyCount => countyCount;

        /// <summary>
        /// Gets the number of references the queue accepted.
        /// <para>References already waiting in the queue conflict and are not counted, so this is work newly scheduled rather than work outstanding.</para>
        /// </summary>
        [JsonIgnore]
        public long EnqueuedCount => enqueuedCount;

        /// <summary>
        /// Gets the number of counties that failed outright and were stepped over.
        /// <para>Each one is logged with the exception that caused it, so this figure is a count of entries to go and read rather than the whole of what is known.</para>
        /// </summary>
        [JsonIgnore]
        public long FailedCountyCount => failedCountyCount;

        /// <summary>
        /// Gets the number of building references read out of the building table across every county visited.
        /// </summary>
        [JsonIgnore]
        public long ReadCount => readCount;

        /// <summary>
        /// Gets the number of stored rows that had a subdivision identifier written to them.
        /// <para>Zero when the run was told not to update them, and lower than <see cref="ReadCount"/> in any case: only a building whose own subdivision is resolved, and which already has a stored row to write to, is counted.</para>
        /// </summary>
        [JsonIgnore]
        public long SubdivisionIdCount => subdivisionIdCount;
    }
}
