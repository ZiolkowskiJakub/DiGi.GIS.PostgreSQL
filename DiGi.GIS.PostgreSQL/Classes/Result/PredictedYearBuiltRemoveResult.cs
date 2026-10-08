using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of removing one prediction run - the predicted year built entries carrying one stamp - from the stored year built data objects of explicit county parts. See <c>YearBuiltDataPostgreSQLConverter.RemovePredictedYearBuiltsAsync</c>.
    /// <para>An object holds at most one entry per stamp, because entries are keyed by source and a prediction's source is its stamp, so <see cref="Matched"/> counts objects and entries alike. An object the removal leaves without any entry is kept, and listed in <see cref="EmptiedReferences"/> so the caller can delete it explicitly.</para>
    /// </summary>
    public class PredictedYearBuiltRemoveResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(DryRun))]
        private readonly bool dryRun;

        [JsonInclude, JsonPropertyName(nameof(Limit))]
        private readonly int limit;

        [JsonInclude, JsonPropertyName(nameof(Ticks))]
        private readonly long ticks;

        [JsonInclude, JsonPropertyName(nameof(Matched))]
        private readonly int matched;

        [JsonInclude, JsonPropertyName(nameof(Removed))]
        private readonly int removed;

        [JsonInclude, JsonPropertyName(nameof(References))]
        private readonly HashSet<string> references = [];

        [JsonInclude, JsonPropertyName(nameof(EmptiedReferences))]
        private readonly HashSet<string> emptiedReferences = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictedYearBuiltRemoveResult"/> class.
        /// </summary>
        /// <param name="dryRun">A value indicating whether the call only counted, leaving every object as it was.</param>
        /// <param name="limit">The largest number of objects the call was allowed to rewrite.</param>
        /// <param name="ticks">The stamp of the removed run, as <see cref="System.DateTime.Ticks"/>.</param>
        /// <param name="matched">The number of objects carrying an entry with the stamp.</param>
        /// <param name="removed">The number of entries removed.</param>
        /// <param name="references">The references of the matched objects, or null for none. Left empty when <paramref name="matched"/> exceeds <paramref name="limit"/>.</param>
        /// <param name="emptiedReferences">The references of the matched objects the removal leaves without any entry, or null for none.</param>
        public PredictedYearBuiltRemoveResult(bool dryRun, int limit, long ticks, int matched, int removed, IEnumerable<string>? references, IEnumerable<string>? emptiedReferences)
        {
            this.dryRun = dryRun;
            this.limit = limit;
            this.ticks = ticks;
            this.matched = matched;
            this.removed = removed;

            if (references is not null)
            {
                this.references = [.. references];
            }

            if (emptiedReferences is not null)
            {
                this.emptiedReferences = [.. emptiedReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictedYearBuiltRemoveResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="predictedYearBuiltRemoveResult">The <see cref="PredictedYearBuiltRemoveResult"/> to copy from.</param>
        public PredictedYearBuiltRemoveResult(PredictedYearBuiltRemoveResult? predictedYearBuiltRemoveResult)
            : base(predictedYearBuiltRemoveResult)
        {
            if (predictedYearBuiltRemoveResult is not null)
            {
                dryRun = predictedYearBuiltRemoveResult.dryRun;
                limit = predictedYearBuiltRemoveResult.limit;
                ticks = predictedYearBuiltRemoveResult.ticks;
                matched = predictedYearBuiltRemoveResult.matched;
                removed = predictedYearBuiltRemoveResult.removed;
                references = [.. predictedYearBuiltRemoveResult.references];
                emptiedReferences = [.. predictedYearBuiltRemoveResult.emptiedReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictedYearBuiltRemoveResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public PredictedYearBuiltRemoveResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the call only counted, leaving every object as it was.
        /// </summary>
        [JsonIgnore]
        public bool DryRun => dryRun;

        /// <summary>
        /// Gets the largest number of objects the call was allowed to rewrite.
        /// </summary>
        [JsonIgnore]
        public int Limit => limit;

        /// <summary>
        /// Gets the stamp of the removed run, as <see cref="System.DateTime.Ticks"/>.
        /// </summary>
        [JsonIgnore]
        public long Ticks => ticks;

        /// <summary>
        /// Gets the number of objects carrying an entry with the stamp.
        /// </summary>
        [JsonIgnore]
        public int Matched => matched;

        /// <summary>
        /// Gets the number of entries removed. Zero on a dry run and when <see cref="Matched"/> exceeds <see cref="Limit"/>.
        /// </summary>
        [JsonIgnore]
        public int Removed => removed;

        /// <summary>
        /// Gets the references of the matched objects - the buildings whose derived year built columns a removal makes stale. Empty when <see cref="Matched"/> exceeds <see cref="Limit"/>.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> References => references;

        /// <summary>
        /// Gets the references of the matched objects the removal leaves without any entry. Those objects are kept; deleting them is a separate, explicit call.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> EmptiedReferences => emptiedReferences;
    }
}
