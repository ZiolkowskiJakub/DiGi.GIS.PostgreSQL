using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents the outcome of a PostgreSQL 2D building refresh operation.
    /// <para>Contains tallies of processed records, updated subdivision identifiers, failed batches, the last processed ID anchor, and cancellation status.</para>
    /// </summary>
    public class PostgreSQLBuilding2DRefreshResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(FailedBatchCount))]
        private readonly long failedBatchCount;

        [JsonInclude, JsonPropertyName(nameof(LastProcessedId))]
        private readonly long lastProcessedId;

        [JsonInclude, JsonPropertyName(nameof(ReadCount))]
        private readonly long readCount;

        [JsonInclude, JsonPropertyName(nameof(UpdatedCount))]
        private readonly long updatedCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DRefreshResult"/> class.
        /// </summary>
        /// <param name="readCount">The number of building records read out of the database.</param>
        /// <param name="updatedCount">The number of building records that had a subdivision identifier updated.</param>
        /// <param name="failedBatchCount">The number of batch iterations that failed and were stepped over.</param>
        /// <param name="lastProcessedId">The last building identifier anchor processed in keyset pagination.</param>
        /// <param name="cancelled">Whether the refresh operation was cancelled before completing all rows.</param>
        public PostgreSQLBuilding2DRefreshResult(long readCount, long updatedCount, long failedBatchCount, long lastProcessedId, bool cancelled)
        {
            this.readCount = readCount;
            this.updatedCount = updatedCount;
            this.failedBatchCount = failedBatchCount;
            this.lastProcessedId = lastProcessedId;
            this.cancelled = cancelled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DRefreshResult"/> class by copying an existing instance.
        /// </summary>
        /// <param name="postgreSQLBuilding2DRefreshResult">The <see cref="PostgreSQLBuilding2DRefreshResult"/> instance to copy from.</param>
        public PostgreSQLBuilding2DRefreshResult(PostgreSQLBuilding2DRefreshResult? postgreSQLBuilding2DRefreshResult)
            : base(postgreSQLBuilding2DRefreshResult)
        {
            if (postgreSQLBuilding2DRefreshResult is not null)
            {
                readCount = postgreSQLBuilding2DRefreshResult.readCount;
                updatedCount = postgreSQLBuilding2DRefreshResult.updatedCount;
                failedBatchCount = postgreSQLBuilding2DRefreshResult.failedBatchCount;
                lastProcessedId = postgreSQLBuilding2DRefreshResult.lastProcessedId;
                cancelled = postgreSQLBuilding2DRefreshResult.cancelled;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DRefreshResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing serialized properties.</param>
        public PostgreSQLBuilding2DRefreshResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the refresh operation was cancelled before reaching the end of the records.
        /// </summary>
        [JsonIgnore]
        public bool Cancelled => cancelled;

        /// <summary>
        /// Gets the number of batch iterations that encountered an error and were stepped over.
        /// </summary>
        [JsonIgnore]
        public long FailedBatchCount => failedBatchCount;

        /// <summary>
        /// Gets the last building identifier anchor reached during keyset pagination.
        /// </summary>
        [JsonIgnore]
        public long LastProcessedId => lastProcessedId;

        /// <summary>
        /// Gets the total number of building records read from the database.
        /// </summary>
        [JsonIgnore]
        public long ReadCount => readCount;

        /// <summary>
        /// Gets the total number of building records whose subdivision identifier was updated.
        /// </summary>
        [JsonIgnore]
        public long UpdatedCount => updatedCount;
    }
}
