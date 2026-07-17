using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a database row envelope for a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> referenced by a 2D building.
    /// </summary>
    public class BuildingModel : Building2DReferencedObject<DiGi.Analytical.Building.Classes.BuildingModel>
    {
        /// <summary>
        /// Initializes a new instance of the BuildingModel class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public BuildingModel(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BuildingModel class by copying data from another BuildingModel instance.
        /// </summary>
        /// <param name="buildingModel">The BuildingModel instance to copy data from.</param>
        public BuildingModel(BuildingModel? buildingModel)
            : base(buildingModel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BuildingModel class.
        /// </summary>
        public BuildingModel()
            : base()
        {
        }
    }
}