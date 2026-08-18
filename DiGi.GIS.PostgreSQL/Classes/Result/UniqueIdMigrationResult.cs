// TODO [BuildingModelRowIdentity]: this whole file is temporary and exists only for the one-off
// unique_id migration of issue ZiolkowskiJakub/DiGi.GIS.PostgreSQL#5. Delete it once every deployed
// database has run PostgreSQLBuildingModelUniqueIdMigrationTask and it reports zero pending rows
// nationally.

using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// [TEMPORARY] How the rows of one county row stand against the convention that <c>unique_id</c> holds the identifier of the stored object itself.
    /// <para>Every row falls into exactly one of four classes, so <see cref="Done"/> + <see cref="Pending"/> + <see cref="Blocked"/> + <see cref="Missing"/> always equals <see cref="Total"/>. A table that has always been written correctly reports <see cref="Done"/> equal to <see cref="Total"/> and nothing else, which is what makes this usable as a check on tables that need no migration at all.</para>
    /// <para>Not named after <c>building_model</c> because it is produced for three tables: the one being migrated, and <c>occupancy_data_building_2d</c> and <c>year_built_data</c>, which were written this way from the start and are read as the evidence that the migration computes the right value.</para>
    /// <para>Temporary - see the note at the top of this file.</para>
    /// </summary>
    public class UniqueIdMigrationResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(Total))]
        private readonly long total;

        [JsonInclude, JsonPropertyName(nameof(Done))]
        private readonly long done;

        [JsonInclude, JsonPropertyName(nameof(Pending))]
        private readonly long pending;

        [JsonInclude, JsonPropertyName(nameof(Blocked))]
        private readonly long blocked;

        [JsonInclude, JsonPropertyName(nameof(Missing))]
        private readonly long missing;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIdMigrationResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county row the counts were taken from.</param>
        /// <param name="total">The number of rows the county row holds.</param>
        /// <param name="done">The number of rows already carrying the identifier of their stored object.</param>
        /// <param name="pending">The number of rows that would be, or were, migrated.</param>
        /// <param name="blocked">The number of rows whose target identifier is already taken within the county row.</param>
        /// <param name="missing">The number of rows whose stored object carries no identifier to migrate.</param>
        public UniqueIdMigrationResult(int countyId, long total, long done, long pending, long blocked, long missing)
        {
            this.countyId = countyId;
            this.total = total;
            this.done = done;
            this.pending = pending;
            this.blocked = blocked;
            this.missing = missing;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIdMigrationResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="uniqueIdMigrationResult">The <see cref="UniqueIdMigrationResult"/> to copy from.</param>
        public UniqueIdMigrationResult(UniqueIdMigrationResult? uniqueIdMigrationResult)
            : base(uniqueIdMigrationResult)
        {
            if (uniqueIdMigrationResult is not null)
            {
                countyId = uniqueIdMigrationResult.countyId;
                total = uniqueIdMigrationResult.total;
                done = uniqueIdMigrationResult.done;
                pending = uniqueIdMigrationResult.pending;
                blocked = uniqueIdMigrationResult.blocked;
                missing = uniqueIdMigrationResult.missing;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIdMigrationResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public UniqueIdMigrationResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the identifier of the county row the counts were taken from.
        /// <para>This is a polygon part, not a county - a multi-part county holds one row per part and each is counted on its own.</para>
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the number of rows the county row holds.
        /// </summary>
        [JsonIgnore]
        public long Total => total;

        /// <summary>
        /// Gets the number of rows already carrying the identifier of the object they store, which need nothing done to them.
        /// </summary>
        [JsonIgnore]
        public long Done => done;

        /// <summary>
        /// Gets the number of rows that would be, or were, migrated.
        /// </summary>
        [JsonIgnore]
        public long Pending => pending;

        /// <summary>
        /// Gets the number of rows left alone because the identifier they would take is already held by another row of the same county row.
        /// <para>Two rows carrying the same stored identifier is the duplicate case the migration cannot resolve on its own: writing both would breach <c>UNIQUE (county_id, unique_id)</c>, so they are reported rather than migrated and stay keyed as they are.</para>
        /// </summary>
        [JsonIgnore]
        public long Blocked => blocked;

        /// <summary>
        /// Gets the number of rows whose stored object carries no identifier to migrate.
        /// <para>Expected to be zero: every object stored in these tables derives from <see cref="Core.Classes.GuidObject"/> and is serialized with its guid. A non-zero count means the column holds something these tables were not built for.</para>
        /// </summary>
        [JsonIgnore]
        public long Missing => missing;
    }
}
