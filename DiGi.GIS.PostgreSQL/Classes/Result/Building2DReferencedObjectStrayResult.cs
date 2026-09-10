using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of probing one <c>unique_id</c>-keyed table for the rows it holds for the given buildings under a county part other than <see cref="CountyId"/>.
    /// <para>A reference in <see cref="MovableReferences"/> has at least one stray row the move would take: the destination part does not yet hold its stored object, so the move - <c>RefreshCountyIdsAsync</c> on the same converter - reports it. A reference in <see cref="BlockedReferences"/> has stray rows but none of them can move - the destination already holds the stored object - and is left where it is, for a person to settle.</para>
    /// <para>The two sets are disjoint and together hold every reference with at least one stray row, so their union is the exact answer to "what would this move touch and what would it refuse".</para>
    /// </summary>
    public class Building2DReferencedObjectStrayResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(MovableReferences))]
        private readonly HashSet<string> movableReferences = [];

        [JsonInclude, JsonPropertyName(nameof(BlockedReferences))]
        private readonly HashSet<string> blockedReferences = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectStrayResult"/> class.
        /// </summary>
        /// <param name="countyId">The county part the rows belong under, the destination of a move.</param>
        /// <param name="movableReferences">The references that would have at least one stray row moved, or null for none.</param>
        /// <param name="blockedReferences">The references whose stray rows cannot move because the destination already holds the stored object, or null for none.</param>
        public Building2DReferencedObjectStrayResult(int countyId, IEnumerable<string>? movableReferences, IEnumerable<string>? blockedReferences)
        {
            this.countyId = countyId;

            if (movableReferences is not null)
            {
                this.movableReferences = [.. movableReferences];
            }

            if (blockedReferences is not null)
            {
                this.blockedReferences = [.. blockedReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectStrayResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="building2DReferencedObjectStrayResult">The <see cref="Building2DReferencedObjectStrayResult"/> to copy from.</param>
        public Building2DReferencedObjectStrayResult(Building2DReferencedObjectStrayResult? building2DReferencedObjectStrayResult)
            : base(building2DReferencedObjectStrayResult)
        {
            if (building2DReferencedObjectStrayResult is not null)
            {
                countyId = building2DReferencedObjectStrayResult.countyId;
                movableReferences = [.. building2DReferencedObjectStrayResult.movableReferences];
                blockedReferences = [.. building2DReferencedObjectStrayResult.blockedReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectStrayResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public Building2DReferencedObjectStrayResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the county part the rows belong under, the destination of a move.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the references that would have at least one stray row moved onto <see cref="CountyId"/>, the same set the move reports.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> MovableReferences => movableReferences;

        /// <summary>
        /// Gets the references with stray rows none of which can move, because <see cref="CountyId"/> already holds the stored object. Left where they are, for a person to settle.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> BlockedReferences => blockedReferences;
    }
}
