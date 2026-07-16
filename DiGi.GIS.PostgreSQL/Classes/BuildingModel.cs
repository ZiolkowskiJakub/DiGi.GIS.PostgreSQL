using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class BuildingModel : Building2DReferencedObject<Analytical.Building.Classes.BuildingModel>
    {
        public BuildingModel(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

         public BuildingModel(BuildingModel? buildingModel)
            : base(buildingModel)
        {
        }

        public BuildingModel()
            : base()
        {
        }
    }
}