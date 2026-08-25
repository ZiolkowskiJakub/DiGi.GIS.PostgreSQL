using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// How the subdivision a building is filed under in <c>building_2d</c> lines up with the one its orthophoto row carries, for one county.
    /// <para>The two tables live in different databases, so nothing in SQL can compare them and no constraint keeps them in step. The value is resolved against <c>building_2d</c> and pushed across by the refresh, which makes this the only place the two can be seen together.</para>
    /// <para>Read <see cref="OrtoDatasOnlyCount"/> and <see cref="Building2DOnlyCount"/> as a pair, and read them across a run rather than on their own:</para>
    /// <para><see cref="OrtoDatasOnlyCount"/> counts rows whose orthophoto knows a subdivision the building no longer does. It can only fall if something is clearing stored subdivisions - the defect of issues #23, #31 and #36 - so a refresh that lowers it is a refresh doing damage.</para>
    /// <para><see cref="Building2DOnlyCount"/> counts buildings whose subdivision has never reached the orthophoto row. A refresh should drive this towards zero; it climbing again afterwards means the download is writing rows without carrying the subdivision through, which is issue #36.</para>
    /// <para><see cref="DisagreeCount"/> is a different fault altogether: both sides name a subdivision and they differ, which happens when a building is refiled and only one table is told.</para>
    /// </summary>
    public class OrtoDatasSubdivisionResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(BothCount))]
        private readonly long bothCount;

        [JsonInclude, JsonPropertyName(nameof(Building2DCount))]
        private readonly long building2DCount;

        [JsonInclude, JsonPropertyName(nameof(Building2DOnlyCount))]
        private readonly long building2DOnlyCount;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(DisagreeCount))]
        private readonly long disagreeCount;

        [JsonInclude, JsonPropertyName(nameof(MatchedCount))]
        private readonly long matchedCount;

        [JsonInclude, JsonPropertyName(nameof(NeitherCount))]
        private readonly long neitherCount;

        [JsonInclude, JsonPropertyName(nameof(OrtoDatasCount))]
        private readonly long ortoDatasCount;

        [JsonInclude, JsonPropertyName(nameof(OrtoDatasOnlyCount))]
        private readonly long ortoDatasOnlyCount;

        [JsonInclude, JsonPropertyName(nameof(References_Building2DOnly))]
        private readonly List<string> references_Building2DOnly = [];

        [JsonInclude, JsonPropertyName(nameof(References_Disagree))]
        private readonly List<string> references_Disagree = [];

        [JsonInclude, JsonPropertyName(nameof(References_OrtoDatasOnly))]
        private readonly List<string> references_OrtoDatasOnly = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasSubdivisionResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county compared.</param>
        /// <param name="ortoDatasCount">How many orthophoto rows the county holds.</param>
        /// <param name="building2DCount">How many building references the county holds.</param>
        /// <param name="matchedCount">How many references are present on both sides.</param>
        /// <param name="bothCount">Of those, how many name a subdivision on both sides.</param>
        /// <param name="disagreeCount">Of those, how many name a different subdivision on each side.</param>
        /// <param name="ortoDatasOnlyCount">How many name a subdivision on the orthophoto side only.</param>
        /// <param name="building2DOnlyCount">How many name a subdivision on the building side only.</param>
        /// <param name="neitherCount">How many name one on neither side.</param>
        /// <param name="references_OrtoDatasOnly">A sample of the references counted by <paramref name="ortoDatasOnlyCount"/>, or null for none.</param>
        /// <param name="references_Building2DOnly">A sample of the references counted by <paramref name="building2DOnlyCount"/>, or null for none.</param>
        /// <param name="references_Disagree">A sample of the references counted by <paramref name="disagreeCount"/>, or null for none.</param>
        public OrtoDatasSubdivisionResult(int countyId, long ortoDatasCount, long building2DCount, long matchedCount, long bothCount, long disagreeCount, long ortoDatasOnlyCount, long building2DOnlyCount, long neitherCount, IEnumerable<string>? references_OrtoDatasOnly, IEnumerable<string>? references_Building2DOnly, IEnumerable<string>? references_Disagree)
        {
            this.countyId = countyId;
            this.ortoDatasCount = ortoDatasCount;
            this.building2DCount = building2DCount;
            this.matchedCount = matchedCount;
            this.bothCount = bothCount;
            this.disagreeCount = disagreeCount;
            this.ortoDatasOnlyCount = ortoDatasOnlyCount;
            this.building2DOnlyCount = building2DOnlyCount;
            this.neitherCount = neitherCount;

            // The samples are built by the comparison constructing this result and handed over whole, so the
            // strings are taken as they are - only the lists are copied, to stop the caller's collections
            // aliasing the result's.
            if (references_OrtoDatasOnly is not null)
            {
                this.references_OrtoDatasOnly = [.. references_OrtoDatasOnly];
            }

            if (references_Building2DOnly is not null)
            {
                this.references_Building2DOnly = [.. references_Building2DOnly];
            }

            if (references_Disagree is not null)
            {
                this.references_Disagree = [.. references_Disagree];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasSubdivisionResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="ortoDatasSubdivisionResult">The <see cref="OrtoDatasSubdivisionResult"/> to copy from.</param>
        public OrtoDatasSubdivisionResult(OrtoDatasSubdivisionResult? ortoDatasSubdivisionResult)
            : base(ortoDatasSubdivisionResult)
        {
            if (ortoDatasSubdivisionResult is not null)
            {
                countyId = ortoDatasSubdivisionResult.countyId;
                ortoDatasCount = ortoDatasSubdivisionResult.ortoDatasCount;
                building2DCount = ortoDatasSubdivisionResult.building2DCount;
                matchedCount = ortoDatasSubdivisionResult.matchedCount;
                bothCount = ortoDatasSubdivisionResult.bothCount;
                disagreeCount = ortoDatasSubdivisionResult.disagreeCount;
                ortoDatasOnlyCount = ortoDatasSubdivisionResult.ortoDatasOnlyCount;
                building2DOnlyCount = ortoDatasSubdivisionResult.building2DOnlyCount;
                neitherCount = ortoDatasSubdivisionResult.neitherCount;

                references_OrtoDatasOnly = new List<string>(ortoDatasSubdivisionResult.references_OrtoDatasOnly);
                references_Building2DOnly = new List<string>(ortoDatasSubdivisionResult.references_Building2DOnly);
                references_Disagree = new List<string>(ortoDatasSubdivisionResult.references_Disagree);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasSubdivisionResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public OrtoDatasSubdivisionResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets how many matched references name a subdivision on both sides, whether or not they agree.
        /// </summary>
        [JsonIgnore]
        public long BothCount => bothCount;

        /// <summary>
        /// Gets how many building references the county holds.
        /// </summary>
        [JsonIgnore]
        public long Building2DCount => building2DCount;

        /// <summary>
        /// Gets how many matched references name a subdivision on the building side but not on the orthophoto side.
        /// <para>What a refresh exists to fix. It should fall to near zero after one and stay there; climbing again once the download drains the queue is issue #36.</para>
        /// </summary>
        [JsonIgnore]
        public long Building2DOnlyCount => building2DOnlyCount;

        /// <summary>
        /// Gets the identifier of the county compared.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets how many matched references name a different subdivision on each side.
        /// <para>A subset of <see cref="BothCount"/>. Neither table is authoritative on its face - the building side is the one that is resolved from geometry, so it is usually the one to trust.</para>
        /// </summary>
        [JsonIgnore]
        public long DisagreeCount => disagreeCount;

        /// <summary>
        /// Gets how many references are present on both sides.
        /// <para><see cref="BothCount"/>, <see cref="OrtoDatasOnlyCount"/>, <see cref="Building2DOnlyCount"/> and <see cref="NeitherCount"/> partition this figure.</para>
        /// </summary>
        [JsonIgnore]
        public long MatchedCount => matchedCount;

        /// <summary>
        /// Gets how many matched references name a subdivision on neither side.
        /// </summary>
        [JsonIgnore]
        public long NeitherCount => neitherCount;

        /// <summary>
        /// Gets how many orthophoto rows the county holds.
        /// <para>Subtracting <see cref="MatchedCount"/> gives the rows filed under this county that no building of it accounts for - usually a building deleted since, or a row filed under the wrong polygon part.</para>
        /// </summary>
        [JsonIgnore]
        public long OrtoDatasCount => ortoDatasCount;

        /// <summary>
        /// Gets how many matched references name a subdivision on the orthophoto side but not on the building side.
        /// <para>The number that must not fall. Nothing legitimate removes a subdivision from a stored row, so a run that lowers this is clearing them.</para>
        /// </summary>
        [JsonIgnore]
        public long OrtoDatasOnlyCount => ortoDatasOnlyCount;

        /// <summary>
        /// Gets a bounded sample of the references counted by <see cref="Building2DOnlyCount"/>.
        /// </summary>
        [JsonIgnore]
        public List<string> References_Building2DOnly => references_Building2DOnly;

        /// <summary>
        /// Gets a bounded sample of the references counted by <see cref="DisagreeCount"/>.
        /// </summary>
        [JsonIgnore]
        public List<string> References_Disagree => references_Disagree;

        /// <summary>
        /// Gets a bounded sample of the references counted by <see cref="OrtoDatasOnlyCount"/>.
        /// </summary>
        [JsonIgnore]
        public List<string> References_OrtoDatasOnly => references_OrtoDatasOnly;
    }
}
