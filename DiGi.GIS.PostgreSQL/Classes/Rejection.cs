using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// One row that was dropped before the database, and why.
    /// <para>A row that cannot be filed under a county part is not written, and naming it is the only way the caller learns which rows are missing - the identifiers returned by an update describe what was stored, never what was lost.</para>
    /// </summary>
    public class Rejection : SerializableObject, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Reference))]
        private readonly string? reference;

        [JsonInclude, JsonPropertyName(nameof(UpdateRejectionReason))]
        private readonly UpdateRejectionReason updateRejectionReason;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rejection"/> class.
        /// </summary>
        /// <param name="reference">The reference of the dropped row, or null when it carried none.</param>
        /// <param name="updateRejectionReason">The reason the row was dropped.</param>
        public Rejection(string? reference, UpdateRejectionReason updateRejectionReason)
        {
            this.reference = reference;
            this.updateRejectionReason = updateRejectionReason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rejection"/> class by copying an existing one.
        /// </summary>
        /// <param name="rejection">The <see cref="Rejection"/> to copy from.</param>
        public Rejection(Rejection? rejection)
            : base(rejection)
        {
            if (rejection is not null)
            {
                reference = rejection.reference;
                updateRejectionReason = rejection.updateRejectionReason;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rejection"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public Rejection(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the reference of the dropped row.
        /// <para>Null when the row carried none. A row reaching an update has not been inserted yet, so its identifier is still zero and there is no second identity to fall back on.</para>
        /// </summary>
        [JsonIgnore]
        public string? Reference => reference;

        /// <summary>
        /// Gets the reason the row was dropped.
        /// <para>It decides what the caller should do next: a payload defect is worth correcting and reposting, a footprint falling outside every candidate county part is not.</para>
        /// </summary>
        [JsonIgnore]
        public UpdateRejectionReason UpdateRejectionReason => updateRejectionReason;
    }
}
