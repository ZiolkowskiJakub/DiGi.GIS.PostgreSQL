using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class YearBuilt : SerializableObject, ITableSerializableObject
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
                CountyId = yearBuilt.CountyId;
                CreatedAt = yearBuilt.CreatedAt;
                Id = yearBuilt.Id;
                Reference = yearBuilt.Reference;
                Source = yearBuilt.Source;
                Year = yearBuilt.Year;
                Object = yearBuilt.Object;
            }
        }

        public YearBuilt()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("CreatedAt")]
        public System.DateTime? CreatedAt { get; set; } = System.DateTime.UtcNow;

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }

        [JsonInclude, JsonPropertyName("Source")]
        public string? Source { get; set; }

        [JsonInclude, JsonPropertyName("Year")]
        public short? Year { get; set; }
    }
}