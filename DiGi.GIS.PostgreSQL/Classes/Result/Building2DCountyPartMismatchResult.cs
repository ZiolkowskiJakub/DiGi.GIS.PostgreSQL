using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Reports how many 2D buildings held by one polygon part of a multi-part county lie outside that part.
    /// <para>A county code names one <c>administrative_areal_2d</c> row per polygon part, and a building is filed under one of them. This counts, for one part, the rows whose stored bounding box does not even touch the bounding box of the part holding them - a building that cannot be inside the part it is filed under, decided without deserializing a single geometry.</para>
    /// <para>The figure is a <b>lower bound</b>: a building sitting inside the part's bounding box but outside its polygon is not counted, because settling that needs the polygon. Nothing counted here is a false alarm, which is what makes it usable as the before and after measurement of a repair.</para>
    /// </summary>
    public class Building2DCountyPartMismatchResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Code))]
        private readonly string? code;

        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(CountOutsideBoundingBox))]
        private readonly long countOutsideBoundingBox;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        private readonly List<int>? countyIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DCountyPartMismatchResult"/> class.
        /// </summary>
        /// <param name="code">The county code the part belongs to.</param>
        /// <param name="countyId">The identifier of the county polygon part holding the buildings.</param>
        /// <param name="countyIds">Every polygon part identifier the code holds, the part itself included.</param>
        /// <param name="count">The number of buildings held by the part.</param>
        /// <param name="countOutsideBoundingBox">The number of those buildings whose bounding box does not intersect the bounding box of the part holding them.</param>
        public Building2DCountyPartMismatchResult(string? code, int countyId, IEnumerable<int>? countyIds, long count, long countOutsideBoundingBox)
        {
            this.code = code;
            this.countyId = countyId;
            this.countyIds = countyIds is null ? null : [.. countyIds];
            this.count = count;
            this.countOutsideBoundingBox = countOutsideBoundingBox;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DCountyPartMismatchResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="building2DCountyPartMismatchResult">The <see cref="Building2DCountyPartMismatchResult"/> instance to copy.</param>
        public Building2DCountyPartMismatchResult(Building2DCountyPartMismatchResult? building2DCountyPartMismatchResult)
            : base(building2DCountyPartMismatchResult)
        {
            if (building2DCountyPartMismatchResult is not null)
            {
                code = building2DCountyPartMismatchResult.code;
                countyId = building2DCountyPartMismatchResult.countyId;
                countyIds = building2DCountyPartMismatchResult.countyIds is null ? null : [.. building2DCountyPartMismatchResult.countyIds];
                count = building2DCountyPartMismatchResult.count;
                countOutsideBoundingBox = building2DCountyPartMismatchResult.countOutsideBoundingBox;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DCountyPartMismatchResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the serialized data.</param>
        public Building2DCountyPartMismatchResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the county code the part belongs to.
        /// </summary>
        [JsonIgnore]
        public string? Code => code;

        /// <summary>
        /// Gets the number of buildings held by the part.
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the number of buildings held by the part whose bounding box does not intersect the bounding box of that part.
        /// </summary>
        [JsonIgnore]
        public long CountOutsideBoundingBox => countOutsideBoundingBox;

        /// <summary>
        /// Gets the identifier of the county polygon part holding the buildings.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets every polygon part identifier the code holds, the part itself included.
        /// </summary>
        [JsonIgnore]
        public List<int>? CountyIds => countyIds is null ? null : [.. countyIds];
    }
}
