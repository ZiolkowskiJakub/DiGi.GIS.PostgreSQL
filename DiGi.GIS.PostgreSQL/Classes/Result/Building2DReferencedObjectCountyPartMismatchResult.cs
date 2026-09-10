using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Reports how many rows held by one polygon part of a multi-part county name a building that <c>building_2d</c> does not hold under that part.
    /// <para>A county code names one <c>administrative_areal_2d</c> row per polygon part, and a referenced object is filed under one of them. A row mismatches when <c>building_2d</c> holds its reference under a different part, or under none at all - the read half of the part misfile the <see cref="Building2DReferencedObjectPostgreSQLConverter{TBuilding2DReferencedObject, TUniqueObject}"/> repair settles in the other direction.</para>
    /// <para>The mismatches split into two classes with different repairs, and they are reported separately: <see cref="CountHeldElsewhere"/> names a building <c>building_2d</c> holds under another part, so it has a destination and the move has somewhere to put it; <see cref="CountOrphan"/> names a building <c>building_2d</c> holds under no part, so there is no destination and the gap is a missing building row, not a misfile.</para>
    /// <para><see cref="Count"/> is <c>CountHeldElsewhere + CountOrphan</c>, the before and after number of a repair. Unlike the <see cref="Building2DCountyPartMismatchResult"/> of <c>building_2d</c> itself, nothing here is a geometry lower bound: a referenced object carries no box, so a non-zero count is a certain misfile, not a suspect, and a part absent from the result is one that held no mismatched row.</para>
    /// </summary>
    public class Building2DReferencedObjectCountyPartMismatchResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Code))]
        private readonly string? code;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        private readonly List<int>? countyIds;

        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(CountHeldElsewhere))]
        private readonly long countHeldElsewhere;

        [JsonInclude, JsonPropertyName(nameof(CountOrphan))]
        private readonly long countOrphan;

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectCountyPartMismatchResult"/> class.
        /// </summary>
        /// <param name="code">The county code the part belongs to.</param>
        /// <param name="countyId">The identifier of the county polygon part holding the mismatched rows.</param>
        /// <param name="countyIds">Every polygon part identifier the code holds, the part itself included.</param>
        /// <param name="count">The number of mismatched rows held by the part; <c>countHeldElsewhere + countOrphan</c>.</param>
        /// <param name="countHeldElsewhere">The number of those rows whose reference <c>building_2d</c> holds under at least one other part.</param>
        /// <param name="countOrphan">The number of those rows whose reference <c>building_2d</c> holds under no part.</param>
        public Building2DReferencedObjectCountyPartMismatchResult(string? code, int countyId, IEnumerable<int>? countyIds, long count, long countHeldElsewhere, long countOrphan)
        {
            this.code = code;
            this.countyId = countyId;
            this.countyIds = countyIds is null ? null : [.. countyIds];
            this.count = count;
            this.countHeldElsewhere = countHeldElsewhere;
            this.countOrphan = countOrphan;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectCountyPartMismatchResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="building2DReferencedObjectCountyPartMismatchResult">The <see cref="Building2DReferencedObjectCountyPartMismatchResult"/> instance to copy.</param>
        public Building2DReferencedObjectCountyPartMismatchResult(Building2DReferencedObjectCountyPartMismatchResult? building2DReferencedObjectCountyPartMismatchResult)
            : base(building2DReferencedObjectCountyPartMismatchResult)
        {
            if (building2DReferencedObjectCountyPartMismatchResult is not null)
            {
                code = building2DReferencedObjectCountyPartMismatchResult.code;
                countyId = building2DReferencedObjectCountyPartMismatchResult.countyId;
                countyIds = building2DReferencedObjectCountyPartMismatchResult.countyIds is null ? null : [.. building2DReferencedObjectCountyPartMismatchResult.countyIds];
                count = building2DReferencedObjectCountyPartMismatchResult.count;
                countHeldElsewhere = building2DReferencedObjectCountyPartMismatchResult.countHeldElsewhere;
                countOrphan = building2DReferencedObjectCountyPartMismatchResult.countOrphan;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectCountyPartMismatchResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the serialized data.</param>
        public Building2DReferencedObjectCountyPartMismatchResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the county code the part belongs to.
        /// </summary>
        [JsonIgnore]
        public string? Code => code;

        /// <summary>
        /// Gets the identifier of the county polygon part holding the mismatched rows.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets every polygon part identifier the code holds, the part itself included.
        /// </summary>
        [JsonIgnore]
        public List<int>? CountyIds => countyIds is null ? null : [.. countyIds];

        /// <summary>
        /// Gets the number of mismatched rows held by the part.
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the number of those rows whose reference <c>building_2d</c> holds under at least one other part.
        /// </summary>
        [JsonIgnore]
        public long CountHeldElsewhere => countHeldElsewhere;

        /// <summary>
        /// Gets the number of those rows whose reference <c>building_2d</c> holds under no part.
        /// </summary>
        [JsonIgnore]
        public long CountOrphan => countOrphan;
    }
}
