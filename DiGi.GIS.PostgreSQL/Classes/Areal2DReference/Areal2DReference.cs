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
        /// <para>This is the identifier of an <c>administrative_areal_2d</c> row, which is one <b>polygon part</b> of a county rather than the whole county - a multi-part county has several such identifiers. It is the parent county, so it is <c>null</c> on a reference that already is a county.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the reference string associated with this item.
        /// <para>Not globally unique: the same building reference is stored once per county row it was imported under, so it is unique only in combination with <see cref="CountyId"/>. Roughly 86 000 <c>building_2d</c> rows are duplicated this way across sibling parts of multi-part counties.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }
    }
}