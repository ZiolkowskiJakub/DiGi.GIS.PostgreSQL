using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2D : Areal2D
    {
        public Building2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Building2D(Building2D? building2D)
            : base(building2D)
        {
            if (building2D is not null)
            {
                CountyId = building2D.CountyId;
                Id = building2D.Id;
                SubdivisionId = building2D.SubdivisionId;
            }
        }

        public Building2D()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}