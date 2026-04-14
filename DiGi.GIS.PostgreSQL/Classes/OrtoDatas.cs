using DiGi.Geometry.Planar.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class OrtoDatas : CountyReferencedObject<GIS.Classes.OrtoDatas>
    {
        public OrtoDatas(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public OrtoDatas(OrtoDatas? ortoDatas)
            : base(ortoDatas)
        {
            if (ortoDatas is not null)
            {
                BoundingBox2D = ortoDatas.BoundingBox2D;
                SubdivisionId = ortoDatas.SubdivisionId;
            }
        }

        public OrtoDatas()
        {
        }

        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}