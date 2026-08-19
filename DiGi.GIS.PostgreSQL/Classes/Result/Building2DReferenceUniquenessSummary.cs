using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents overall building reference uniqueness metrics across all partitions.
    /// </summary>
    public class Building2DReferenceUniquenessSummary : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(TotalCount))]
        private readonly long totalCount;

        [JsonInclude, JsonPropertyName(nameof(DistinctReferenceCount))]
        private readonly long distinctReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(DuplicateReferenceCount))]
        private readonly long duplicateReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(IsUnique))]
        private readonly bool isUnique;

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferenceUniquenessSummary"/> class.
        /// </summary>
        /// <param name="totalCount">The total number of building rows.</param>
        /// <param name="distinctReferenceCount">The number of distinct reference identifiers.</param>
        /// <param name="duplicateReferenceCount">The number of duplicate building rows across all counties.</param>
        /// <param name="isUnique">A boolean value indicating whether all building references are globally unique.</param>
        public Building2DReferenceUniquenessSummary(long totalCount, long distinctReferenceCount, long duplicateReferenceCount, bool isUnique)
        {
            this.totalCount = totalCount;
            this.distinctReferenceCount = distinctReferenceCount;
            this.duplicateReferenceCount = duplicateReferenceCount;
            this.isUnique = isUnique;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferenceUniquenessSummary"/> class by copying an existing one.
        /// </summary>
        /// <param name="building2DReferenceUniquenessSummary">The <see cref="Building2DReferenceUniquenessSummary"/> instance to copy.</param>
        public Building2DReferenceUniquenessSummary(Building2DReferenceUniquenessSummary? building2DReferenceUniquenessSummary)
            : base(building2DReferenceUniquenessSummary)
        {
            if (building2DReferenceUniquenessSummary is not null)
            {
                totalCount = building2DReferenceUniquenessSummary.totalCount;
                distinctReferenceCount = building2DReferenceUniquenessSummary.distinctReferenceCount;
                duplicateReferenceCount = building2DReferenceUniquenessSummary.duplicateReferenceCount;
                isUnique = building2DReferenceUniquenessSummary.isUnique;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferenceUniquenessSummary"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the serialized data.</param>
        public Building2DReferenceUniquenessSummary(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the total number of building rows.
        /// </summary>
        [JsonIgnore]
        public long TotalCount => totalCount;

        /// <summary>
        /// Gets the number of distinct reference identifiers.
        /// </summary>
        [JsonIgnore]
        public long DistinctReferenceCount => distinctReferenceCount;

        /// <summary>
        /// Gets the number of duplicate building rows across all counties.
        /// </summary>
        [JsonIgnore]
        public long DuplicateReferenceCount => duplicateReferenceCount;

        /// <summary>
        /// Gets a value indicating whether all building references are globally unique.
        /// </summary>
        [JsonIgnore]
        public bool IsUnique => isUnique;
    }
}
