using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of a database update: the identifiers that were written, and the rows that were not.
    /// <para><see cref="Ids"/> alone cannot report a shortfall. It is a set, and every table written here uses <c>INSERT ... ON CONFLICT (reference, county_id) DO UPDATE ... RETURNING id</c>, so two rows of one batch colliding on the conflict key return the same identifier and the set keeps one. Comparing its count against the number of rows sent therefore proves nothing in either direction - <see cref="Rejections"/> is the exact figure.</para>
    /// </summary>
    public class PostgreSQLUpdateResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Ids))]
        private readonly HashSet<long> ids = [];

        [JsonInclude, JsonPropertyName(nameof(Rejections))]
        private readonly List<Rejection> rejections = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateResult"/> class.
        /// </summary>
        /// <param name="ids">The identifiers returned by the update, or null for none.</param>
        /// <param name="rejections">The rows dropped before the database, or null for none.</param>
        public PostgreSQLUpdateResult(IEnumerable<long>? ids, IEnumerable<Rejection>? rejections)
        {
            if (ids is not null)
            {
                this.ids = [.. ids];
            }

            if (rejections is not null)
            {
                // The rejections are built by the update that is constructing this result and handed over
                // whole, so the items are taken as they are - only the list itself is copied, to stop the
                // caller's collection aliasing the result's.
                this.rejections = [.. rejections];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="postgreSQLUpdateResult">The <see cref="PostgreSQLUpdateResult"/> to copy from.</param>
        public PostgreSQLUpdateResult(PostgreSQLUpdateResult? postgreSQLUpdateResult)
            : base(postgreSQLUpdateResult)
        {
            if (postgreSQLUpdateResult is not null)
            {
                ids = [.. postgreSQLUpdateResult.ids];

                foreach (Rejection rejection in postgreSQLUpdateResult.rejections)
                {
                    if (Core.Query.Clone(rejection) is Rejection rejection_Clone)
                    {
                        rejections.Add(rejection_Clone);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public PostgreSQLUpdateResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the identifiers of the rows written by the update, from the <c>RETURNING id</c> of both the insert and the update branch.
        /// </summary>
        [JsonIgnore]
        public HashSet<long> Ids => ids;

        /// <summary>
        /// Gets the rows that were dropped before the database, each named with the reason it was dropped.
        /// </summary>
        [JsonIgnore]
        public List<Rejection> Rejections => rejections;
    }
}
