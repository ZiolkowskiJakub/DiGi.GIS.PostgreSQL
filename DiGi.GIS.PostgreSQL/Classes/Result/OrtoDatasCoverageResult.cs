using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// How much of one subdivision's buildings the orthophoto store holds, measured over that subdivision's own buildings rather than over its county's.
    /// <para>The figure the estimated county-level counts cannot give. <c>orto_datas</c> and <c>building_2d</c> are partitioned by <c>county_id</c>, so a partition estimate describes a whole county and says nothing about any area inside it; this is counted, not estimated.</para>
    /// <para>Counted from the building side alone. <c>orto_datas</c> carries a <c>subdivision_id</c> column of its own, but it has never been written - not one of the 8 384 055 rows stored across 225 counties carries a value - so grouping the orthophoto side by it answers zero for every subdivision in the country. <c>building_2d</c> is the side that knows which subdivision a building belongs to, and it is the side this is measured from.</para>
    /// </summary>
    public class OrtoDatasCoverageResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(SubdivisionId))]
        private readonly int? subdivisionId;

        [JsonInclude, JsonPropertyName(nameof(Building2DCount))]
        private readonly long building2DCount;

        [JsonInclude, JsonPropertyName(nameof(OrtoDatasCount))]
        private readonly long ortoDatasCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasCoverageResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county the coverage was measured in.</param>
        /// <param name="subdivisionId">The identifier of the subdivision the coverage describes, or null for the county's buildings that name no subdivision.</param>
        /// <param name="building2DCount">The number of buildings the subdivision holds.</param>
        /// <param name="ortoDatasCount">The number of those buildings that have an orthophoto row.</param>
        public OrtoDatasCoverageResult(int countyId, int? subdivisionId, long building2DCount, long ortoDatasCount)
        {
            this.countyId = countyId;
            this.subdivisionId = subdivisionId;
            this.building2DCount = building2DCount;
            this.ortoDatasCount = ortoDatasCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasCoverageResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="ortoDatasCoverageResult">The <see cref="OrtoDatasCoverageResult"/> to copy from.</param>
        public OrtoDatasCoverageResult(OrtoDatasCoverageResult? ortoDatasCoverageResult)
            : base(ortoDatasCoverageResult)
        {
            if (ortoDatasCoverageResult is not null)
            {
                countyId = ortoDatasCoverageResult.countyId;
                subdivisionId = ortoDatasCoverageResult.subdivisionId;
                building2DCount = ortoDatasCoverageResult.building2DCount;
                ortoDatasCount = ortoDatasCoverageResult.ortoDatasCount;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasCoverageResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public OrtoDatasCoverageResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the identifier of the county the coverage was measured in.
        /// <para>One polygon part, not a code - a multi-part county is measured a part at a time, because that is how both tables are partitioned.</para>
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the identifier of the subdivision the coverage describes.
        /// <para>Null is not a missing value: it is the county's buildings that name no subdivision. Those belong to no subdivision and to no municipality, so nothing below county level should ever count them, and they are kept apart rather than folded into a neighbour.</para>
        /// </summary>
        [JsonIgnore]
        public int? SubdivisionId => subdivisionId;

        /// <summary>
        /// Gets the number of buildings the subdivision holds. The denominator of the coverage.
        /// </summary>
        [JsonIgnore]
        public long Building2DCount => building2DCount;

        /// <summary>
        /// Gets the number of the subdivision's buildings that have an orthophoto row. The numerator of the coverage.
        /// <para>Counted on references present in <c>orto_datas</c> for the same county, not on that table's own subdivision column, which has never been written.</para>
        /// </summary>
        [JsonIgnore]
        public long OrtoDatasCount => ortoDatasCount;
    }
}
