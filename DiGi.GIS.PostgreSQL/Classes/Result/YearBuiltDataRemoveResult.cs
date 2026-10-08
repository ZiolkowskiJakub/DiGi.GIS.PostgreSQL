using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of deleting stored year built data objects of explicit county parts - see <c>YearBuiltDataPostgreSQLConverter.RemoveItemsAsync</c>.
    /// <para><see cref="Matched"/> is what the scope selected and <see cref="Removed"/> what was actually deleted. They differ only when nothing was deleted: on a dry run, and when more rows matched than <see cref="Limit"/> allows, in which case the delete is refused as a whole rather than cut short - a partial delete would leave a caller unable to tell which rows went.</para>
    /// </summary>
    public class YearBuiltDataRemoveResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(DryRun))]
        private readonly bool dryRun;

        [JsonInclude, JsonPropertyName(nameof(Limit))]
        private readonly int limit;

        [JsonInclude, JsonPropertyName(nameof(Matched))]
        private readonly int matched;

        [JsonInclude, JsonPropertyName(nameof(Removed))]
        private readonly int removed;

        [JsonInclude, JsonPropertyName(nameof(UnmatchedReferences))]
        private readonly HashSet<string> unmatchedReferences = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltDataRemoveResult"/> class.
        /// </summary>
        /// <param name="dryRun">A value indicating whether the call only counted, leaving every row in place.</param>
        /// <param name="limit">The largest number of rows the call was allowed to delete.</param>
        /// <param name="matched">The number of rows the scope selected.</param>
        /// <param name="removed">The number of rows deleted.</param>
        /// <param name="unmatchedReferences">The requested references no selected row holds, or null for none.</param>
        public YearBuiltDataRemoveResult(bool dryRun, int limit, int matched, int removed, IEnumerable<string>? unmatchedReferences)
        {
            this.dryRun = dryRun;
            this.limit = limit;
            this.matched = matched;
            this.removed = removed;

            if (unmatchedReferences is not null)
            {
                this.unmatchedReferences = [.. unmatchedReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltDataRemoveResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="yearBuiltDataRemoveResult">The <see cref="YearBuiltDataRemoveResult"/> to copy from.</param>
        public YearBuiltDataRemoveResult(YearBuiltDataRemoveResult? yearBuiltDataRemoveResult)
            : base(yearBuiltDataRemoveResult)
        {
            if (yearBuiltDataRemoveResult is not null)
            {
                dryRun = yearBuiltDataRemoveResult.dryRun;
                limit = yearBuiltDataRemoveResult.limit;
                matched = yearBuiltDataRemoveResult.matched;
                removed = yearBuiltDataRemoveResult.removed;
                unmatchedReferences = [.. yearBuiltDataRemoveResult.unmatchedReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltDataRemoveResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YearBuiltDataRemoveResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the call only counted, leaving every row in place.
        /// </summary>
        [JsonIgnore]
        public bool DryRun => dryRun;

        /// <summary>
        /// Gets the largest number of rows the call was allowed to delete.
        /// </summary>
        [JsonIgnore]
        public int Limit => limit;

        /// <summary>
        /// Gets the number of rows the scope selected.
        /// </summary>
        [JsonIgnore]
        public int Matched => matched;

        /// <summary>
        /// Gets the number of rows deleted. Zero on a dry run and when <see cref="Matched"/> exceeds <see cref="Limit"/>.
        /// </summary>
        [JsonIgnore]
        public int Removed => removed;

        /// <summary>
        /// Gets the requested references no selected row holds - either no row is stored for them under the given parts, or, when only empty objects were selected, every row they hold still carries an entry.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> UnmatchedReferences => unmatchedReferences;
    }
}
