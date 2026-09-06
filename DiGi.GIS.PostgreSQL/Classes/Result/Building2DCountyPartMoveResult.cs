using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Names one 2D building that is filed under a county polygon part its footprint does not lie in, and the part it belongs to.
    /// <para>Produced by the decision half of the county part repair and consumed by the write half, so the two can be run apart: a dry run reports these and writes nothing, and the same set of records drives the move when it is run for real.</para>
    /// <para><see cref="DecidedByGeometry"/> says how the answer was reached. When only one part of the county has a bounding box reaching the building, the boxes settle it and the footprint is never deserialized; otherwise the footprint is read and the full containment decision runs. Both answers are the same decision the import makes - the boxes only ever narrow the candidates.</para>
    /// </summary>
    public class Building2DCountyPartMoveResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Code))]
        private readonly string? code;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(CountyIdResolved))]
        private readonly int countyIdResolved;

        [JsonInclude, JsonPropertyName(nameof(DecidedByGeometry))]
        private readonly bool decidedByGeometry;

        [JsonInclude, JsonPropertyName(nameof(Id))]
        private readonly long id;

        [JsonInclude, JsonPropertyName(nameof(Reference))]
        private readonly string? reference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DCountyPartMoveResult"/> class.
        /// </summary>
        /// <param name="code">The county code both parts belong to.</param>
        /// <param name="reference">The reference of the building.</param>
        /// <param name="id">The identifier of the row holding the building.</param>
        /// <param name="countyId">The identifier of the county polygon part the row is filed under.</param>
        /// <param name="countyIdResolved">The identifier of the county polygon part the footprint lies in.</param>
        /// <param name="decidedByGeometry">Whether the footprint had to be read to decide, rather than the bounding boxes settling it.</param>
        public Building2DCountyPartMoveResult(string? code, string? reference, long id, int countyId, int countyIdResolved, bool decidedByGeometry)
        {
            this.code = code;
            this.reference = reference;
            this.id = id;
            this.countyId = countyId;
            this.countyIdResolved = countyIdResolved;
            this.decidedByGeometry = decidedByGeometry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DCountyPartMoveResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="building2DCountyPartMoveResult">The <see cref="Building2DCountyPartMoveResult"/> instance to copy.</param>
        public Building2DCountyPartMoveResult(Building2DCountyPartMoveResult? building2DCountyPartMoveResult)
            : base(building2DCountyPartMoveResult)
        {
            if (building2DCountyPartMoveResult is not null)
            {
                code = building2DCountyPartMoveResult.code;
                reference = building2DCountyPartMoveResult.reference;
                id = building2DCountyPartMoveResult.id;
                countyId = building2DCountyPartMoveResult.countyId;
                countyIdResolved = building2DCountyPartMoveResult.countyIdResolved;
                decidedByGeometry = building2DCountyPartMoveResult.decidedByGeometry;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DCountyPartMoveResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the serialized data.</param>
        public Building2DCountyPartMoveResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the county code both parts belong to.
        /// </summary>
        [JsonIgnore]
        public string? Code => code;

        /// <summary>
        /// Gets the identifier of the county polygon part the row is filed under.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the identifier of the county polygon part the footprint lies in.
        /// </summary>
        [JsonIgnore]
        public int CountyIdResolved => countyIdResolved;

        /// <summary>
        /// Gets a value indicating whether the footprint had to be read to decide, rather than the bounding boxes settling it.
        /// </summary>
        [JsonIgnore]
        public bool DecidedByGeometry => decidedByGeometry;

        /// <summary>
        /// Gets the identifier of the row holding the building.
        /// </summary>
        [JsonIgnore]
        public long Id => id;

        /// <summary>
        /// Gets the reference of the building.
        /// </summary>
        [JsonIgnore]
        public string? Reference => reference;
    }
}
