using DiGi.Geometry.Planar.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class Areal2D<TAreal2D> : ReferencedObject<TAreal2D> where TAreal2D : GIS.Classes.Areal2D
    {
        public Areal2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public Areal2D(Areal2D<TAreal2D>? areal2D)
            : base(areal2D)
        {
            if (areal2D is not null)
            {
                BoundingBox2D = areal2D.BoundingBox2D;
                Code = areal2D.Code;
            }
        }

        public Areal2D()
        {
        }

        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        [JsonInclude, JsonPropertyName("Code")]
        public string? Code { get; set; } = null;
    }
}