using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of an external components area classification: what it classified, what it stepped over, and how much of it rests on an open envelope.
    /// <para><see cref="SkippedComponentCount"/> counts components that bound two spaces (internal partitions) or whose target bucket is undefined - the same count this method has returned as a bare number since the classification was introduced.</para>
    /// <para><see cref="OpenEnvelopeCount"/> counts models whose external envelope exists but does not close on the tolerance ladder. The orientation of an envelope face is decided by ray parity, which is sound only for a closed face set, so the sector and tilt values of those models may rest on an arbitrary face side; the count is the share of a run's rows a reader should treat with that caution, and each such row carries a null closing tolerance in the table.</para>
    /// </summary>
    public class ExternalComponentsAreaResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(OpenEnvelopeCount))]
        private readonly long openEnvelopeCount;

        [JsonInclude, JsonPropertyName(nameof(SkippedComponentCount))]
        private readonly long skippedComponentCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalComponentsAreaResult"/> class.
        /// </summary>
        /// <param name="skippedComponentCount">The number of components skipped because they bound two spaces or their target bucket is undefined.</param>
        /// <param name="openEnvelopeCount">The number of models whose external envelope does not close on the tolerance ladder.</param>
        public ExternalComponentsAreaResult(long skippedComponentCount, long openEnvelopeCount)
        {
            this.skippedComponentCount = skippedComponentCount;
            this.openEnvelopeCount = openEnvelopeCount;
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
