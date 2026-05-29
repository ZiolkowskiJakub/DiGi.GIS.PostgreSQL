using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract base class for referenced objects, providing common properties for serialization and identification.
    /// </summary>
    /// <typeparam name="TUniqueObject">The type of the unique object this referenced object points to.</typeparam>
    public abstract class ReferencedObject<TUniqueObject> : TableSerializableObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        /// <summary>
        /// Initializes a new instance of the ReferencedObject class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public ReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReferencedObject class by copying data from another ReferencedObject instance.
        /// </summary>
        /// <param name="referencedObject">The ReferencedObject instance to copy data from.</param>
        public ReferencedObject(ReferencedObject<TUniqueObject>? referencedObject)
            : base(referencedObject)
        {
            if (referencedObject is not null)
            {
                Reference = referencedObject.Reference;
                CreatedAt = referencedObject.CreatedAt;
                UniqueId = referencedObject.UniqueId;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ReferencedObject class.
        /// </summary>
        public ReferencedObject()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the reference string associated with this object.
        /// </summary>
        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the referenced object.
        /// </summary>
        [JsonInclude, JsonPropertyName("UniqueId")]
        public string? UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when this object was created.
        /// </summary>
        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;
    }
}