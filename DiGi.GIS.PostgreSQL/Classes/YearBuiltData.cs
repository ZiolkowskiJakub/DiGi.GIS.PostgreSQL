using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class YearBuiltData : Building2DReferencedObject<IYearBuiltData>
    {
        public YearBuiltData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public YearBuiltData(YearBuiltData? yearBuiltData)
            : base(yearBuiltData)
        {
        }

        public YearBuiltData()
            : base()
        {
        }
    }
}