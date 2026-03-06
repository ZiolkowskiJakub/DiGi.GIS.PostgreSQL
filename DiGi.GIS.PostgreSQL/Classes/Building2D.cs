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

        public Building2D(Building2D? postgreSQLBuilding2D)
            : base(postgreSQLBuilding2D)
        {
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