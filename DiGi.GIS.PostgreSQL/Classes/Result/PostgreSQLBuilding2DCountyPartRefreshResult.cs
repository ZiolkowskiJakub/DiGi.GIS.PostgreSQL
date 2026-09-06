using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents the outcome of re-filing 2D buildings under the county polygon part their footprint lies in.
    /// <para><see cref="MoveCount"/> is what the run decided, <see cref="MovedCount"/> what it wrote: a dry run reports the first and leaves the second at zero. The two also differ on a live run when a move is blocked, which is what <see cref="BlockedCount"/> counts - the destination part already holds that reference, so the row stays where it is rather than being deleted.</para>
    /// </summary>
    public class PostgreSQLBuilding2DCountyPartRefreshResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(BlockedCount))]
        private readonly long blockedCount;

        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(CodeCount))]
        private readonly long codeCount;

        [JsonInclude, JsonPropertyName(nameof(FailedCodeCount))]
        private readonly long failedCodeCount;

        [JsonInclude, JsonPropertyName(nameof(MoveCount))]
        private readonly long moveCount;

        [JsonInclude, JsonPropertyName(nameof(MovedCount))]
        private readonly long movedCount;

        [JsonInclude, JsonPropertyName(nameof(ReadCount))]
        private readonly long readCount;

        [JsonInclude, JsonPropertyName(nameof(ReferencedObjectMovedCount))]
        private readonly long referencedObjectMovedCount;

        [JsonInclude, JsonPropertyName(nameof(UnresolvedCount))]
        private readonly long unresolvedCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DCountyPartRefreshResult"/> class.
        /// </summary>
        /// <param name="codeCount">The number of multi-part county codes examined.</param>
        /// <param name="readCount">The number of building rows read out of the database.</param>
        /// <param name="moveCount">The number of building rows found to be filed under a part their footprint does not lie in.</param>
        /// <param name="movedCount">The number of building rows actually moved. Zero on a dry run.</param>
        /// <param name="blockedCount">The number of moves the destination part refused because it already holds that reference.</param>
        /// <param name="unresolvedCount">The number of building rows no part could be decided for, left where they are.</param>
        /// <param name="referencedObjectMovedCount">The number of references carried onto the new part in the tables keyed on a building.</param>
        /// <param name="failedCodeCount">The number of counties stepped over after a failure, which the run reports rather than ending on.</param>
        /// <param name="cancelled">Whether the run was cancelled before reaching the end.</param>
        public PostgreSQLBuilding2DCountyPartRefreshResult(long codeCount, long readCount, long moveCount, long movedCount, long blockedCount, long unresolvedCount, long referencedObjectMovedCount, long failedCodeCount, bool cancelled)
        {
            this.codeCount = codeCount;
            this.failedCodeCount = failedCodeCount;
            this.readCount = readCount;
            this.moveCount = moveCount;
            this.movedCount = movedCount;
            this.blockedCount = blockedCount;
            this.unresolvedCount = unresolvedCount;
            this.referencedObjectMovedCount = referencedObjectMovedCount;
            this.cancelled = cancelled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DCountyPartRefreshResult"/> class by copying an existing instance.
        /// </summary>
        /// <param name="postgreSQLBuilding2DCountyPartRefreshResult">The <see cref="PostgreSQLBuilding2DCountyPartRefreshResult"/> instance to copy from.</param>
        public PostgreSQLBuilding2DCountyPartRefreshResult(PostgreSQLBuilding2DCountyPartRefreshResult? postgreSQLBuilding2DCountyPartRefreshResult)
            : base(postgreSQLBuilding2DCountyPartRefreshResult)
        {
            if (postgreSQLBuilding2DCountyPartRefreshResult is not null)
            {
                codeCount = postgreSQLBuilding2DCountyPartRefreshResult.codeCount;
                failedCodeCount = postgreSQLBuilding2DCountyPartRefreshResult.failedCodeCount;
                readCount = postgreSQLBuilding2DCountyPartRefreshResult.readCount;
                moveCount = postgreSQLBuilding2DCountyPartRefreshResult.moveCount;
                movedCount = postgreSQLBuilding2DCountyPartRefreshResult.movedCount;
                blockedCount = postgreSQLBuilding2DCountyPartRefreshResult.blockedCount;
                unresolvedCount = postgreSQLBuilding2DCountyPartRefreshResult.unresolvedCount;
                referencedObjectMovedCount = postgreSQLBuilding2DCountyPartRefreshResult.referencedObjectMovedCount;
                cancelled = postgreSQLBuilding2DCountyPartRefreshResult.cancelled;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DCountyPartRefreshResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing serialized properties.</param>
        public PostgreSQLBuilding2DCountyPartRefreshResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of moves the destination part refused because it already holds that reference.
        /// </summary>
        [JsonIgnore]
        public long BlockedCount => blockedCount;

        /// <summary>
        /// Gets a value indicating whether the run was cancelled before reaching the end.
        /// </summary>
        [JsonIgnore]
        public bool Cancelled => cancelled;

        /// <summary>
        /// Gets the number of multi-part county codes examined.
        /// </summary>
        [JsonIgnore]
        public long CodeCount => codeCount;

        /// <summary>
        /// Gets the number of counties stepped over after a failure.
        /// <para>A county that fails is logged with the exception that caused it and the run carries on, because the counties are independent of each other. The run does not report success while this is not zero: what it left undone is finished by running it again, and a run reported as successful is a run nobody goes back to.</para>
        /// </summary>
        [JsonIgnore]
        public long FailedCodeCount => failedCodeCount;

        /// <summary>
        /// Gets the number of building rows found to be filed under a part their footprint does not lie in.
        /// </summary>
        [JsonIgnore]
        public long MoveCount => moveCount;

        /// <summary>
        /// Gets the number of building rows actually moved. Zero on a dry run.
        /// </summary>
        [JsonIgnore]
        public long MovedCount => movedCount;

        /// <summary>
        /// Gets the number of building rows read out of the database.
        /// </summary>
        [JsonIgnore]
        public long ReadCount => readCount;

        /// <summary>
        /// Gets the number of references carried onto the new part in the tables keyed on a building.
        /// </summary>
        [JsonIgnore]
        public long ReferencedObjectMovedCount => referencedObjectMovedCount;

        /// <summary>
        /// Gets the number of building rows no part could be decided for, left where they are.
        /// </summary>
        [JsonIgnore]
        public long UnresolvedCount => unresolvedCount;
    }
}
