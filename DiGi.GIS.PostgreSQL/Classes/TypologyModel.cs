using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class TypologyModel : AdministrativeAreal2DReferencedObject<Typology.Classes.TypologyModel>
    {
        public TypologyModel(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public TypologyModel(TypologyModel? typologyModel)
            : base(typologyModel)
        {
        }

        public TypologyModel()
            : base()
        {
        }
    }
}