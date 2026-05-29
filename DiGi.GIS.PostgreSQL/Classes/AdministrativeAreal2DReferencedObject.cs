using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract base class for referenced objects associated with 2D administrative areas, providing identification properties.
    /// </summary>
    /// <typeparam name="TUniqueObject">The type of the unique object this referenced object points to.</typeparam>
    public abstract class AdministrativeAreal2DReferencedObject<TUniqueObject> : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencedObject class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public AdministrativeAreal2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencedObject class by copying data from another AdministrativeAreal2DReferencedObject instance.
        /// </summary>
        /// <param name="administrativeAreal2DReferencedObject">The AdministrativeAreal2DReferencedObject instance to copy data from.</param>
        public AdministrativeAreal2DReferencedObject(AdministrativeAreal2DReferencedObject<TUniqueObject>? administrativeAreal2DReferencedObject)
            : base(administrativeAreal2DReferencedObject)
        {
            if (administrativeAreal2DReferencedObject is not null)
            {
                Id = administrativeAreal2DReferencedObject.Id;
            }
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencedObject class.
        /// </summary>
        public AdministrativeAreal2DReferencedObject()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the ID of the administrative area.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}