using DiGi.Geometry.Spatial.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a building database row envelope that references a <see cref="CityGML.Classes.Building"/> object, extending <see cref="Building2DReferencedObject{TUniqueObject}"/> with bounding box, level of detail, and construction year information.
    /// </summary>
    public class Building : Building2DReferencedObject<CityGML.Classes.Building>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public Building(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class by copying data from another <see cref="Building"/> instance.
        /// </summary>
        /// <param name="building">The <see cref="Building"/> instance to copy data from.</param>
        public Building(Building? building)
            : base(building)
        {
            if (building is not null)
            {
                BoundingBox3D = building.BoundingBox3D;
                Year = building.Year;
                LOD = building.LOD;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        public Building()
        {
        }

        /// <summary>
        /// Gets or sets the 3D bounding box of the building.
        /// </summary>
        [JsonInclude, JsonPropertyName("BoundingBox3D")]
        public BoundingBox3D? BoundingBox3D { get; set; }

        /// <summary>
        /// Gets or sets the CityGML level of detail of the building.
        /// </summary>
        [JsonInclude, JsonPropertyName("LOD")]
        public CityGML.Enums.LOD? LOD { get; set; }

        /// <summary>
        /// Gets or sets the construction year of the building.
        /// </summary>
        [JsonInclude, JsonPropertyName("Year")]
        public short? Year { get; set; }
    }
}