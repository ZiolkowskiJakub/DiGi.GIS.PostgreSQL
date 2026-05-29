using DiGi.Geometry.Planar.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract generic class for 2D area entities with bounding box and code information.
    /// </summary>
    /// <typeparam name="TAreal2D">The type of the underlying GIS 2D area object.</typeparam>
    public abstract class Areal2D<TAreal2D> : ReferencedObject<TAreal2D> where TAreal2D : GIS.Classes.Areal2D
    {
        /// <summary>
        /// Initializes a new instance of the Areal2D class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Areal2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Areal2D class by copying data from another Areal2D instance.
        /// </summary>
        /// <param name="areal2D">The Areal2D instance to copy data from.</param>
        public Areal2D(Areal2D<TAreal2D>? areal2D)
            : base(areal2D)
        {
            if (areal2D is not null)
            {
                BoundingBox2D = areal2D.BoundingBox2D;
                Code = areal2D.Code;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Areal2D class.
        /// </summary>
        public Areal2D()
        {
        }

        /// <summary>
        /// Gets or sets the bounding box of the 2D area.
        /// </summary>
        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        /// <summary>
        /// Gets or sets the code of the 2D area.
        /// </summary>
        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;
    }
}