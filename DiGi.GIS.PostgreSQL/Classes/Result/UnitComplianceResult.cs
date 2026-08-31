using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents the result of an administrative area matching compliance check against BDL statistical units.
    /// </summary>
    public class UnitComplianceResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(AdministrativeArealType))]
        private readonly AdministrativeArealType administrativeArealType;

        [JsonInclude, JsonPropertyName(nameof(TotalCount))]
        private readonly long totalCount;

        [JsonInclude, JsonPropertyName(nameof(MatchedCount))]
        private readonly long matchedCount;

        [JsonInclude, JsonPropertyName(nameof(UnmatchedCount))]
        private readonly long unmatchedCount;

        [JsonInclude, JsonPropertyName(nameof(ComplianceRate))]
        private readonly double complianceRate;

        [JsonInclude, JsonPropertyName(nameof(UnmatchedReferences))]
        private readonly List<AdministrativeAreal2DReference>? unmatchedReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitComplianceResult"/> class.
        /// </summary>
        /// <param name="administrativeArealType">The administrative areal type evaluated.</param>
        /// <param name="totalCount">The total number of administrative areal references evaluated.</param>
        /// <param name="matchedCount">The number of administrative areal references successfully matched to a statistical unit.</param>
        /// <param name="unmatchedReferences">The collection of administrative areal references that could not be matched.</param>
        public UnitComplianceResult(AdministrativeArealType administrativeArealType, long totalCount, long matchedCount, IEnumerable<AdministrativeAreal2DReference>? unmatchedReferences)
        {
            this.administrativeArealType = administrativeArealType;
            this.totalCount = totalCount;
            this.matchedCount = matchedCount;
            unmatchedCount = totalCount - matchedCount;
            complianceRate = totalCount > 0 ? (double)matchedCount / totalCount : 1.0;
            this.unmatchedReferences = unmatchedReferences is null ? null : [.. unmatchedReferences];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitComplianceResult"/> class by copying another instance.
        /// </summary>
        /// <param name="unitComplianceResult">The source instance to copy from.</param>
        public UnitComplianceResult(UnitComplianceResult? unitComplianceResult)
            : base(unitComplianceResult)
        {
            if (unitComplianceResult is not null)
            {
                administrativeArealType = unitComplianceResult.administrativeArealType;
                totalCount = unitComplianceResult.totalCount;
                matchedCount = unitComplianceResult.matchedCount;
                unmatchedCount = unitComplianceResult.unmatchedCount;
                complianceRate = unitComplianceResult.complianceRate;

                if (unitComplianceResult.unmatchedReferences is not null)
                {
                    unmatchedReferences = [];
                    foreach (AdministrativeAreal2DReference administrativeAreal2DReference in unitComplianceResult.unmatchedReferences)
                    {
                        if (Core.Query.Clone(administrativeAreal2DReference) is AdministrativeAreal2DReference administrativeAreal2DReference_Clone)
                        {
                            unmatchedReferences.Add(administrativeAreal2DReference_Clone);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitComplianceResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the serialized data.</param>
        public UnitComplianceResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the administrative areal type evaluated.
        /// </summary>
        [JsonIgnore]
        public AdministrativeArealType AdministrativeArealType => administrativeArealType;

        /// <summary>
        /// Gets the total number of administrative areal references evaluated.
        /// </summary>
        [JsonIgnore]
        public long TotalCount => totalCount;

        /// <summary>
        /// Gets the number of administrative areal references successfully matched to a statistical unit.
        /// </summary>
        [JsonIgnore]
        public long MatchedCount => matchedCount;

        /// <summary>
        /// Gets the number of administrative areal references that could not be matched.
        /// </summary>
        [JsonIgnore]
        public long UnmatchedCount => unmatchedCount;

        /// <summary>
        /// Gets the compliance rate as a value between 0.0 and 1.0.
        /// </summary>
        [JsonIgnore]
        public double ComplianceRate => complianceRate;

        /// <summary>
        /// Gets the collection of administrative areal references that could not be matched.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<AdministrativeAreal2DReference>? UnmatchedReferences => unmatchedReferences;
    }
}
