using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// What one county's building data holds measured against the buildings that county actually has.
    /// <para>The question a row count cannot answer. A count says how much was written; this says how much was left out, and separates the two reasons a building can be missing - a run that failed on its subdivision, and a building that belongs to no subdivision and is therefore never reached by any run.</para>
    /// <para>The two sides live in different databases: <c>building_2d</c> is in the main one and <c>building_data</c> in the storage one, so the comparison is made on references read from each rather than by a join.</para>
    /// </summary>
    public class BuildingDataCoverageResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(Building2DCount))]
        private readonly long building2DCount;

        [JsonInclude, JsonPropertyName(nameof(BuildingDataCount))]
        private readonly long buildingDataCount;

        [JsonInclude, JsonPropertyName(nameof(MissingReferenceCount))]
        private readonly long missingReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(OrphanReferenceCount))]
        private readonly long orphanReferenceCount;

        [JsonInclude, JsonPropertyName(nameof(UnassignedSubdivisionCount))]
        private readonly long unassignedSubdivisionCount;

        [JsonInclude, JsonPropertyName(nameof(CrossCountySubdivisionCount))]
        private readonly long crossCountySubdivisionCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataCoverageResult"/> class.
        /// </summary>
        /// <param name="countyId">The identifier of the county the coverage describes.</param>
        /// <param name="building2DCount">The number of distinct building references the county holds.</param>
        /// <param name="buildingDataCount">The number of distinct references the county's building data holds.</param>
        /// <param name="missingReferenceCount">The number of buildings with no building data row.</param>
        /// <param name="orphanReferenceCount">The number of building data rows whose building is no longer there.</param>
        /// <param name="unassignedSubdivisionCount">The number of the county's buildings that name no subdivision.</param>
        /// <param name="crossCountySubdivisionCount">The number of the county's buildings whose subdivision belongs to a different county.</param>
        public BuildingDataCoverageResult(int countyId, long building2DCount, long buildingDataCount, long missingReferenceCount, long orphanReferenceCount, long unassignedSubdivisionCount, long crossCountySubdivisionCount = 0)
        {
            this.countyId = countyId;
            this.building2DCount = building2DCount;
            this.buildingDataCount = buildingDataCount;
            this.missingReferenceCount = missingReferenceCount;
            this.orphanReferenceCount = orphanReferenceCount;
            this.unassignedSubdivisionCount = unassignedSubdivisionCount;
            this.crossCountySubdivisionCount = crossCountySubdivisionCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataCoverageResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="buildingDataCoverageResult">The <see cref="BuildingDataCoverageResult"/> to copy from.</param>
        public BuildingDataCoverageResult(BuildingDataCoverageResult? buildingDataCoverageResult)
            : base(buildingDataCoverageResult)
        {
            if (buildingDataCoverageResult is not null)
            {
                countyId = buildingDataCoverageResult.countyId;
                building2DCount = buildingDataCoverageResult.building2DCount;
                buildingDataCount = buildingDataCoverageResult.buildingDataCount;
                missingReferenceCount = buildingDataCoverageResult.missingReferenceCount;
                orphanReferenceCount = buildingDataCoverageResult.orphanReferenceCount;
                unassignedSubdivisionCount = buildingDataCoverageResult.unassignedSubdivisionCount;
                crossCountySubdivisionCount = buildingDataCoverageResult.crossCountySubdivisionCount;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataCoverageResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public BuildingDataCoverageResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the identifier of the county the coverage describes.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the number of distinct building references the county holds.
        /// </summary>
        [JsonIgnore]
        public long Building2DCount => building2DCount;

        /// <summary>
        /// Gets the number of distinct references the county's building data holds.
        /// <para>Counted on references rather than on rows, so that it can be compared with <see cref="Building2DCount"/> directly.</para>
        /// </summary>
        [JsonIgnore]
        public long BuildingDataCount => buildingDataCount;

        /// <summary>
        /// Gets the number of the county's buildings that have no building data row at all.
        /// <para>This is the figure a run is judged on. <see cref="UnassignedSubdivisionCount"/> accounts for the part of it no run can currently reach; anything above that is a run that did not finish what it could have.</para>
        /// </summary>
        [JsonIgnore]
        public long MissingReferenceCount => missingReferenceCount;

        /// <summary>
        /// Gets the number of building data rows whose reference the county's buildings no longer carry.
        /// <para>Either a building that has since been removed, or a row written under the wrong county. Nothing deletes these, so they accumulate.</para>
        /// </summary>
        [JsonIgnore]
        public long OrphanReferenceCount => orphanReferenceCount;

        /// <summary>
        /// Gets the number of the county's buildings whose subdivision has not been resolved.
        /// <para>The building data update is driven by subdivisions, so a building that names none is never visited. This is the known and expected part of <see cref="MissingReferenceCount"/>.</para>
        /// </summary>
        [JsonIgnore]
        public long UnassignedSubdivisionCount => unassignedSubdivisionCount;

        /// <summary>
        /// Gets the number of the county's buildings whose subdivision belongs to a different county.
        /// <para>The building data update walks subdivisions by their parent county, so a building whose subdivision is filed under another county part is skipped by the subdivision loop and reached only by the fallback pass.</para>
        /// </summary>
        [JsonIgnore]
        public long CrossCountySubdivisionCount => crossCountySubdivisionCount;
    }
}
