using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// The outcome of recomputing the three derived year built columns of <c>building_data</c> - predicted, user and calculated - from the stored year built history. See <c>Modify.UpdateBuildingDataYearBuiltAsync</c>.
    /// <para><see cref="Updated"/> and <see cref="Cleared"/> are the buildings written; the rest of <see cref="Matched"/> were not written at all, because they hold no <c>building_data</c> row and their history holds no value, so writing them would only add empty rows.</para>
    /// </summary>
    public class BuildingDataYearBuiltUpdateResult : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Matched))]
        private readonly int matched;

        [JsonInclude, JsonPropertyName(nameof(Updated))]
        private readonly int updated;

        [JsonInclude, JsonPropertyName(nameof(Cleared))]
        private readonly int cleared;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataYearBuiltUpdateResult"/> class.
        /// </summary>
        /// <param name="matched">The number of buildings considered.</param>
        /// <param name="updated">The number of buildings written with at least one of the three values.</param>
        /// <param name="cleared">The number of buildings written with none, so all three columns were set to NULL.</param>
        public BuildingDataYearBuiltUpdateResult(int matched, int updated, int cleared)
        {
            this.matched = matched;
            this.updated = updated;
            this.cleared = cleared;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataYearBuiltUpdateResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="buildingDataYearBuiltUpdateResult">The <see cref="BuildingDataYearBuiltUpdateResult"/> to copy from.</param>
        public BuildingDataYearBuiltUpdateResult(BuildingDataYearBuiltUpdateResult? buildingDataYearBuiltUpdateResult)
            : base(buildingDataYearBuiltUpdateResult)
        {
            if (buildingDataYearBuiltUpdateResult is not null)
            {
                matched = buildingDataYearBuiltUpdateResult.matched;
                updated = buildingDataYearBuiltUpdateResult.updated;
                cleared = buildingDataYearBuiltUpdateResult.cleared;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingDataYearBuiltUpdateResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public BuildingDataYearBuiltUpdateResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of buildings considered.
        /// </summary>
        [JsonIgnore]
        public int Matched => matched;

        /// <summary>
        /// Gets the number of buildings written with at least one of the three values.
        /// </summary>
        [JsonIgnore]
        public int Updated => updated;

        /// <summary>
        /// Gets the number of buildings written with none of the three values, so their three columns were set to NULL.
        /// </summary>
        [JsonIgnore]
        public int Cleared => cleared;
    }
}
