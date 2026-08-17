using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of a terrain point update: how many points were stored, and the points that were not.
    /// <para>Separate from <see cref="PostgreSQLUpdateResult"/> because the terrain point table has no identity column - its key is the county and the coordinates - so there are no identifiers to return. <see cref="Count"/> takes their place and is the figure that matters here: every write goes through <c>ON CONFLICT DO NOTHING</c>, so a re-import of unchanged data reports zero without failing, and the number distinguishes that from a batch that genuinely stored nothing.</para>
    /// </summary>
    public class TerrainPointUpdateResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(Rejections))]
        private readonly List<Rejection> rejections = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointUpdateResult"/> class.
        /// </summary>
        /// <param name="count">The number of points written to the database.</param>
        /// <param name="rejections">The points dropped before the database, or null for none.</param>
        public TerrainPointUpdateResult(long count, IEnumerable<Rejection>? rejections)
        {
            this.count = count;

            if (rejections is not null)
            {
                // The rejections are built by the update that is constructing this result and handed over
                // whole, so the items are taken as they are - only the list itself is copied, to stop the
                // caller's collection aliasing the result's.
                this.rejections = [.. rejections];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointUpdateResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="terrainPointUpdateResult">The <see cref="TerrainPointUpdateResult"/> to copy from.</param>
        public TerrainPointUpdateResult(TerrainPointUpdateResult? terrainPointUpdateResult)
            : base(terrainPointUpdateResult)
        {
            if (terrainPointUpdateResult is not null)
            {
                count = terrainPointUpdateResult.count;

                foreach (Rejection rejection in terrainPointUpdateResult.rejections)
                {
                    if (Core.Query.Clone(rejection) is Rejection rejection_Clone)
                    {
                        rejections.Add(rejection_Clone);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointUpdateResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public TerrainPointUpdateResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of points written by the update.
        /// <para>Points already present are not counted - the write conflicts on the county and the coordinates and does nothing - so a repeated import of the same data reports zero.</para>
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the points that were dropped before the database, each named with the reason it was dropped.
        /// </summary>
        [JsonIgnore]
        public List<Rejection> Rejections => rejections;
    }
}
