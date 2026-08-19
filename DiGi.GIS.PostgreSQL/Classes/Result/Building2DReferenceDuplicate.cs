using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents duplicate building reference collisions across counties.
    /// </summary>
    public class Building2DReferenceDuplicate : SerializableResult, IGISPostgreSQLSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Reference))]
        private readonly string? reference;

        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly long count;

        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        private readonly List<int>? countyIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferenceDuplicate"/> class.
        /// </summary>
        /// <param name="reference">The duplicated reference identifier.</param>
        /// <param name="count">The total number of occurrences across counties.</param>
        /// <param name="countyIds">The collection of county identifiers where this reference appears.</param>
        public Building2DReferenceDuplicate(string? reference, long count, IEnumerable<int>? countyIds)
        {
            this.reference = reference;
            this.count = count;
            this.countyIds = countyIds == null ? null : [.. countyIds];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferenceDuplicate"/> class by copying an existing one.
        /// </summary>
        /// <param name="building2DReferenceDuplicate">The <see cref="Building2DReferenceDuplicate"/> instance to copy.</param>
        public Building2DReferenceDuplicate(Building2DReferenceDuplicate? building2DReferenceDuplicate)
            : base(building2DReferenceDuplicate)
        {
            if (building2DReferenceDuplicate is not null)
            {
                reference = building2DReferenceDuplicate.reference;
                count = building2DReferenceDuplicate.count;
                countyIds = building2DReferenceDuplicate.countyIds == null ? null : [.. building2DReferenceDuplicate.countyIds];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferenceDuplicate"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the serialized data.</param>
        public Building2DReferenceDuplicate(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the duplicated reference identifier.
        /// </summary>
        [JsonIgnore]
        public string? Reference => reference;

        /// <summary>
        /// Gets the total number of occurrences across counties.
        /// </summary>
        [JsonIgnore]
        public long Count => count;

        /// <summary>
        /// Gets the collection of county identifiers where this reference appears.
        /// </summary>
        [JsonIgnore]
        public List<int>? CountyIds => countyIds == null ? null : [.. countyIds];
    }
}
