using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class YearBuilt : Building2DReferencedObject<IYearBuilt>
    {
        public YearBuilt(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public YearBuilt(YearBuilt? yearBuilt)
            : base(yearBuilt)
        {
        }

        public YearBuilt()
            : base()
        {
        }
    }
}