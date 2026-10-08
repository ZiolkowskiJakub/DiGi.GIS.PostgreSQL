using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of withdrawing user-provided year built entries from the stored year built data objects of explicit county parts. See <c>YearBuiltDataPostgreSQLConverter.RemoveUserYearBuiltsAsync</c>.
    /// <para>Every requested reference lands in exactly one of the three sets. A building is decided as a whole: when an owner is required and any of its objects holds a user entry of someone else, none of its objects is touched.</para>
    /// </summary>
    public class UserYearBuiltRemoveResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(DryRun))]
        private readonly bool dryRun;

        [JsonInclude, JsonPropertyName(nameof(RemovedReferences))]
        private readonly HashSet<string> removedReferences = [];

        [JsonInclude, JsonPropertyName(nameof(NotOwnedReferences))]
        private readonly HashSet<string> notOwnedReferences = [];

        [JsonInclude, JsonPropertyName(nameof(NotFoundReferences))]
        private readonly HashSet<string> notFoundReferences = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="UserYearBuiltRemoveResult"/> class.
        /// </summary>
        /// <param name="dryRun">A value indicating whether the call only classified the references, leaving every object as it was.</param>
        /// <param name="removedReferences">The references whose user entry was withdrawn, or would be on a dry run; null for none.</param>
        /// <param name="notOwnedReferences">The references holding a user entry of another user, left untouched; null for none.</param>
        /// <param name="notFoundReferences">The references holding no user entry under the given parts; null for none.</param>
        public UserYearBuiltRemoveResult(bool dryRun, IEnumerable<string>? removedReferences, IEnumerable<string>? notOwnedReferences, IEnumerable<string>? notFoundReferences)
        {
            this.dryRun = dryRun;

            if (removedReferences is not null)
            {
                this.removedReferences = [.. removedReferences];
            }

            if (notOwnedReferences is not null)
            {
                this.notOwnedReferences = [.. notOwnedReferences];
            }

            if (notFoundReferences is not null)
            {
                this.notFoundReferences = [.. notFoundReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserYearBuiltRemoveResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="userYearBuiltRemoveResult">The <see cref="UserYearBuiltRemoveResult"/> to copy from.</param>
        public UserYearBuiltRemoveResult(UserYearBuiltRemoveResult? userYearBuiltRemoveResult)
            : base(userYearBuiltRemoveResult)
        {
            if (userYearBuiltRemoveResult is not null)
            {
                dryRun = userYearBuiltRemoveResult.dryRun;
                removedReferences = [.. userYearBuiltRemoveResult.removedReferences];
                notOwnedReferences = [.. userYearBuiltRemoveResult.notOwnedReferences];
                notFoundReferences = [.. userYearBuiltRemoveResult.notFoundReferences];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserYearBuiltRemoveResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public UserYearBuiltRemoveResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the call only classified the references, leaving every object as it was.
        /// </summary>
        [JsonIgnore]
        public bool DryRun => dryRun;

        /// <summary>
        /// Gets the references whose user entry was withdrawn, or would be on a dry run.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> RemovedReferences => removedReferences;

        /// <summary>
        /// Gets the references holding a user entry of another user. None of their objects was touched.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> NotOwnedReferences => notOwnedReferences;

        /// <summary>
        /// Gets the references holding no user entry under the given parts.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> NotFoundReferences => notFoundReferences;
    }
}
