using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract base class for referenced objects associated with 2D buildings, providing county and identification properties.
    /// </summary>
    /// <typeparam name="TUniqueObject">The type of the unique object this referenced object points to.</typeparam>
    public abstract class Building2DReferencedObject<TUniqueObject> : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        /// <summary>
        /// Initializes a new instance of the Building2DReferencedObject class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Building2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReferencedObject class by copying data from another Building2DReferencedObject instance.
        /// </summary>
        /// <param name="building2DReferencedObject">The Building2DReferencedObject instance to copy data from.</param>
        public Building2DReferencedObject(Building2DReferencedObject<TUniqueObject>? building2DReferencedObject)
            : base(building2DReferencedObject)
        {
            if (building2DReferencedObject is not null)
            {
                CountyId = building2DReferencedObject.CountyId;
                Id = building2DReferencedObject.Id;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReferencedObject class.
        /// </summary>
        public Building2DReferencedObject()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the county ID associated with this referenced object.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of this referenced object.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }
    }
}