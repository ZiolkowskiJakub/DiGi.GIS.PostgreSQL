using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract base class for 2D area references, providing serialization and common properties for area-based data.
    /// </summary>
    public abstract class Areal2DReference : SerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the Areal2DReference class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Areal2DReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Areal2DReference class by copying data from another Areal2DReference instance.
        /// </summary>
        /// <param name="areal2DReference">The Areal2DReference instance to copy data from.</param>
        public Areal2DReference(Areal2DReference? areal2DReference)
            : base(areal2DReference)
        {
            if (areal2DReference is not null)
            {
                CountyId = areal2DReference.CountyId;
                Reference = areal2DReference.Reference;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Areal2DReference class.
        /// </summary>
        public Areal2DReference()
        {
        }

        /// <summary>
        /// Gets or sets the county ID associated with this item.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the reference string associated with this item.
        /// </summary>
        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}