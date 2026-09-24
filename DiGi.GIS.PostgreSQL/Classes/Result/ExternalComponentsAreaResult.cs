using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of an external components area classification: what it classified, what it stepped over, how much of it rests on an open envelope, and which models it could not classify.
    /// <para><see cref="SkippedComponentCount"/> counts components that bound two spaces (internal partitions) or whose target bucket is undefined - the same count this method has returned as a bare number since the classification was introduced.</para>
    /// <para><see cref="OpenEnvelopeCount"/> counts models whose external envelope exists but does not close on the tolerance ladder. The orientation of an envelope face is decided by ray parity, which is sound only for a closed face set, so the sector and tilt values of those models may rest on an arbitrary face side; the count is the share of a run's rows a reader should treat with that caution, and each such row carries a null closing tolerance in the table.</para>
    /// <para><see cref="DegenerateReferences"/> names the models that carry components but no external envelope at all - fewer than the four external faces a closed solid needs, typically a sliver footprint with walls and no roof or floor. No row is written for them and they are not a failure.</para>
    /// <para><see cref="FailedReferences"/> names the models the classification refused as a defect in their space structure, each with the reason; no row is written for them, and the other models of the same call are classified regardless.</para>
    /// </summary>
    public class ExternalComponentsAreaResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(DegenerateReferences))]
        private readonly List<string>? degenerateReferences;

        [JsonInclude, JsonPropertyName(nameof(FailedReferences))]
        private readonly List<string>? failedReferences;

        [JsonInclude, JsonPropertyName(nameof(OpenEnvelopeCount))]
        private readonly long openEnvelopeCount;

        [JsonInclude, JsonPropertyName(nameof(SkippedComponentCount))]
        private readonly long skippedComponentCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalComponentsAreaResult"/> class.
        /// </summary>
        /// <param name="skippedComponentCount">The number of components skipped because they bound two spaces or their target bucket is undefined.</param>
        /// <param name="openEnvelopeCount">The number of models whose external envelope does not close on the tolerance ladder.</param>
        /// <param name="degenerateReferences">The references of the models that carry components but no external envelope; null or empty when there were none.</param>
        /// <param name="failedReferences">The models the classification refused, each as <c>"reference: reason"</c>; null or empty when there were none.</param>
        public ExternalComponentsAreaResult(long skippedComponentCount, long openEnvelopeCount, IEnumerable<string>? degenerateReferences = null, IEnumerable<string>? failedReferences = null)
        {
            this.skippedComponentCount = skippedComponentCount;
            this.openEnvelopeCount = openEnvelopeCount;
            this.degenerateReferences = degenerateReferences is null ? null : [.. degenerateReferences];
            this.failedReferences = failedReferences is null ? null : [.. failedReferences];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalComponentsAreaResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="externalComponentsAreaResult">The <see cref="ExternalComponentsAreaResult"/> to copy from.</param>
        public ExternalComponentsAreaResult(ExternalComponentsAreaResult? externalComponentsAreaResult)
            : base(externalComponentsAreaResult)
        {
            if (externalComponentsAreaResult is not null)
            {
                skippedComponentCount = externalComponentsAreaResult.skippedComponentCount;
                openEnvelopeCount = externalComponentsAreaResult.openEnvelopeCount;
                degenerateReferences = externalComponentsAreaResult.degenerateReferences is null ? null : new List<string>(externalComponentsAreaResult.degenerateReferences);
                failedReferences = externalComponentsAreaResult.failedReferences is null ? null : new List<string>(externalComponentsAreaResult.failedReferences);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalComponentsAreaResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public ExternalComponentsAreaResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of models that carry components but no external envelope - the count of <see cref="DegenerateReferences"/>.
        /// </summary>
        [JsonIgnore]
        public long DegenerateModelCount => degenerateReferences?.Count ?? 0;

        /// <summary>
        /// Gets a copy of the references of the models that carry components but no external envelope, so no row was written for them.
        /// <para><see cref="DiGi.Analytical.Building.Classes.BuildingModel.GetExternalShell(DiGi.Geometry.Core.Enums.Side?, DiGi.Geometry.Core.Enums.Orientation?, DiGi.Geometry.Core.Enums.Orientation?, double)"/> answers null for such a model - fewer than four external faces - so no component has an outward normal to classify by. It is a property of the stored data, not a defect of the run.</para>
        /// </summary>
        [JsonIgnore]
        public List<string>? DegenerateReferences => degenerateReferences is null ? null : new List<string>(degenerateReferences);

        /// <summary>
        /// Gets the number of models the classification refused - the count of <see cref="FailedReferences"/>.
        /// </summary>
        [JsonIgnore]
        public long FailedModelCount => failedReferences?.Count ?? 0;

        /// <summary>
        /// Gets a copy of the models the classification refused as a defect in their space structure, each as <c>"reference: reason"</c>; no row was written for them.
        /// </summary>
        [JsonIgnore]
        public List<string>? FailedReferences => failedReferences is null ? null : new List<string>(failedReferences);

        /// <summary>
        /// Gets the number of models whose external envelope does not close on the tolerance ladder.
        /// <para>An open envelope is not a failure: the model is still classified and its row written, but the orientation of a face whose ray leaves through the gap is arbitrary, so the sector and tilt values of these rows carry a null closing tolerance and this count.</para>
        /// </summary>
        [JsonIgnore]
        public long OpenEnvelopeCount => openEnvelopeCount;

        /// <summary>
        /// Gets the number of components skipped because they bound two spaces or their target bucket is undefined.
        /// </summary>
        [JsonIgnore]
        public long SkippedComponentCount => skippedComponentCount;
    }
}
