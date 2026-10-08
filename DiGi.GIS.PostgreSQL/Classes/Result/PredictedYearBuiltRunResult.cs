using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// One prediction run as stored under one county part: the stamp, the model that produced it when recorded, and how many stored year built data objects carry an entry with it.
    /// <para>The stamp is carried as <see cref="System.DateTime.Ticks"/> - the same value the entry is keyed by - rather than as text, because a formatted date follows the culture and the kind of the value and is not an identity.</para>
    /// </summary>
    public class PredictedYearBuiltRunResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(Ticks))]
        private readonly long ticks;

        [JsonInclude, JsonPropertyName(nameof(ModelId))]
        private readonly string? modelId;

        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictedYearBuiltRunResult"/> class.
        /// </summary>
        /// <param name="countyId">The county part the objects are stored under.</param>
        /// <param name="ticks">The stamp of the run, as <see cref="System.DateTime.Ticks"/>.</param>
        /// <param name="modelId">The identifier of the model that produced the entries, or null when it was not recorded.</param>
        /// <param name="count">The number of stored objects carrying an entry with the stamp and model.</param>
        public PredictedYearBuiltRunResult(int countyId, long ticks, string? modelId, int count)
        {
            this.countyId = countyId;
            this.ticks = ticks;
            this.modelId = modelId;
            this.count = count;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictedYearBuiltRunResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="predictedYearBuiltRunResult">The <see cref="PredictedYearBuiltRunResult"/> to copy from.</param>
        public PredictedYearBuiltRunResult(PredictedYearBuiltRunResult? predictedYearBuiltRunResult)
            : base(predictedYearBuiltRunResult)
        {
            if (predictedYearBuiltRunResult is not null)
            {
                countyId = predictedYearBuiltRunResult.countyId;
                ticks = predictedYearBuiltRunResult.ticks;
                modelId = predictedYearBuiltRunResult.modelId;
                count = predictedYearBuiltRunResult.count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictedYearBuiltRunResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public PredictedYearBuiltRunResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the county part the objects are stored under.
        /// </summary>
        [JsonIgnore]
        public int CountyId => countyId;

        /// <summary>
        /// Gets the stamp of the run, as <see cref="System.DateTime.Ticks"/>.
        /// </summary>
        [JsonIgnore]
        public long Ticks => ticks;

        /// <summary>
        /// Gets the identifier of the model that produced the entries, or null when it was not recorded.
        /// </summary>
        [JsonIgnore]
        public string? ModelId => modelId;

        /// <summary>
        /// Gets the number of stored objects carrying an entry with the stamp and model.
        /// </summary>
        [JsonIgnore]
        public int Count => count;
    }
}
