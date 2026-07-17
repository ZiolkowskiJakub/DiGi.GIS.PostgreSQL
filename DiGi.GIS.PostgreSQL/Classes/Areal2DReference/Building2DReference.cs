using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a reference to a 2D building within a subdivision.
    /// </summary>
    public class Building2DReference : Areal2DReference
    {
        /// <summary>
        /// Initializes a new instance of the Building2DReference class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Building2DReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReference class by copying data from another Building2DReference instance.
        /// </summary>
        /// <param name="building2DReference">The Building2DReference instance to copy data from.</param>
        public Building2DReference(Building2DReference? building2DReference)
            : base(building2DReference)
        {
            if (building2DReference is not null)
            {
                Id = building2DReference.Id;
                SubdivisionId = building2DReference.SubdivisionId;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReference class.
        /// </summary>
        public Building2DReference()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReference class from a Building2D instance.
        /// </summary>
        /// <param name="building2D">The Building2D instance to reference. This value can be null.</param>
        public Building2DReference(Building2D? building2D)
        {
            if (building2D is not null)
            {
                Id = building2D.Id;
                SubdivisionId = building2D.SubdivisionId;
                CountyId = building2D.CountyId;
                Reference = building2D.Reference;
            }
        }

        /// <summary>
        /// Gets or sets the unique ID of the building.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the subdivision to which this building belongs.
        /// </summary>
        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}