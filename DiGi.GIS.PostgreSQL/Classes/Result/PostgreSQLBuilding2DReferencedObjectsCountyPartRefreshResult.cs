using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents the outcome of carrying the <c>unique_id</c>-keyed referenced objects onto the county part their building sits on.
    /// <para><see cref="MovableReferenceCount"/> is what the probe found, <see cref="MovedReferenceCount"/> what was written: a dry run reports the first and leaves the second at zero. The two also differ on a live run when a move is blocked, which <see cref="BlockedReferenceCount"/> counts - the destination part already holds the stored object, so the row stays where it is rather than being deleted.</para>
    /// </summary>
    public class PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(BlockedReferenceCount))]
        private readonly long blockedReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(CodeCount))]
        private readonly long codeCount;

        [JsonInclude, JsonPropertyName(nameof(FailedCodeCount))]
        private readonly long failedCodeCount;

        [JsonInclude, JsonPropertyName(nameof(MovedReferenceCount))]
        private readonly long movedReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(MovableReferenceCount))]
        private readonly long movableReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(PartCount))]
        private readonly long partCount;

        [JsonInclude, JsonPropertyName(nameof(ReferenceCount))]
        private readonly long referenceCount;

        [JsonInclude, JsonPropertyName(nameof(StrayReferenceCount))]
        private readonly long strayReferenceCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult"/> class.
        /// </summary>
        /// <param name="codeCount">The number of county codes examined.</param>
        /// <param name="partCount">The number of county polygon parts the buildings were read from.</param>
        /// <param name="referenceCount">The number of building references read from <c>building_2d</c>.</param>
        /// <param name="strayReferenceCount">The number of references with at least one row held under a part other than the one their building sits on, summed over the tables.</param>
        /// <param name="movableReferenceCount">The number of references the probe would have moved. The live run's moved plus blocked references.</param>
        /// <param name="blockedReferenceCount">The number of moves the destination part refused because it already holds the stored object. On a dry run, what the probe reported as blocked.</param>
        /// <param name="movedReferenceCount">The number of references that had at least one row moved. Zero on a dry run.</param>
        /// <param name="failedCodeCount">The number of counties stepped over after a failure, which the run reports rather than ending on.</param>
        /// <param name="cancelled">Whether the run was cancelled before reaching the end.</param>
        public PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult(long codeCount, long partCount, long referenceCount, long strayReferenceCount, long movableReferenceCount, long blockedReferenceCount, long movedReferenceCount, long failedCodeCount, bool cancelled)
        {
            this.codeCount = codeCount;
            this.partCount = partCount;
            this.referenceCount = referenceCount;
            this.strayReferenceCount = strayReferenceCount;
            this.movableReferenceCount = movableReferenceCount;
            this.blockedReferenceCount = blockedReferenceCount;
            this.movedReferenceCount = movedReferenceCount;
            this.failedCodeCount = failedCodeCount;
            this.cancelled = cancelled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult"/> class by copying an existing instance.
        /// </summary>
        /// <param name="postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult">The <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult"/> instance to copy from.</param>
        public PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult(PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult? postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult)
            : base(postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult)
        {
            if (postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult is not null)
            {
                codeCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.codeCount;
                partCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.partCount;
                referenceCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.referenceCount;
                strayReferenceCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.strayReferenceCount;
                movableReferenceCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.movableReferenceCount;
                blockedReferenceCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.blockedReferenceCount;
                movedReferenceCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.movedReferenceCount;
                failedCodeCount = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.failedCodeCount;
                cancelled = postgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult.cancelled;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing serialized properties.</param>
        public PostgreSQLBuilding2DReferencedObjectsCountyPartRefreshResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of moves the destination part refused because it already holds the stored object.
        /// <para>Those rows are left exactly where they are - deleting either copy is a decision for a person - so the figure is the count of references a person still has to settle.</para>
        /// </summary>
        [JsonIgnore]
        public long BlockedReferenceCount => blockedReferenceCount;

        /// <summary>
        /// Gets a value indicating whether the run was cancelled before reaching the end.
        /// </summary>
        [JsonIgnore]
        public bool Cancelled => cancelled;

        /// <summary>
        /// Gets the number of county codes examined.
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
        /// Gets the number of references that had at least one row moved. Zero on a dry run.
        /// </summary>
        [JsonIgnore]
        public long MovedReferenceCount => movedReferenceCount;

        /// <summary>
        /// Gets the number of references the probe found to have at least one movable stray row, summed over the tables.
        /// </summary>
        [JsonIgnore]
        public long MovableReferenceCount => movableReferenceCount;

        /// <summary>
        /// Gets the number of county polygon parts the buildings were read from.
        /// </summary>
        [JsonIgnore]
        public long PartCount => partCount;

        /// <summary>
        /// Gets the number of building references read from <c>building_2d</c>, the sweep the run carries rows for.
        /// </summary>
        [JsonIgnore]
        public long ReferenceCount => referenceCount;

        /// <summary>
        /// Gets the number of references with at least one row held under a part other than the one their building sits on, summed over the tables. Movable plus blocked.
        /// </summary>
        [JsonIgnore]
        public long StrayReferenceCount => strayReferenceCount;
    }
}
