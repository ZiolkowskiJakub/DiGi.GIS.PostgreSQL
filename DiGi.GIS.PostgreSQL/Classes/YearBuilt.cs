using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class YearBuilt : CountyReferencedObject<IYearBuilt>
    {
        public YearBuilt(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public YearBuilt(YearBuilt? yearBuilt)
            : base(yearBuilt)
        {
            if (yearBuilt is not null)
            {
                Source = yearBuilt.Source;
                Year = yearBuilt.Year;
            }
        }

        public YearBuilt()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("Source")]
        public string? Source { get; set; }

        [JsonInclude, JsonPropertyName("Year")]
        public short? Year { get; set; }
    }
}