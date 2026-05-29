using DiGi.Geometry.Planar.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents orthophoto data associated with a 2D building.
    /// </summary>
    public class OrtoDatas : Building2DReferencedObject<GIS.Classes.OrtoDatas>
    {
        /// <summary>
        /// Initializes a new instance of the OrtoDatas class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public OrtoDatas(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OrtoDatas class by copying data from another OrtoDatas instance.
        /// </summary>
        /// <param name="ortoDatas">The OrtoDatas instance to copy data from.</param>
        public OrtoDatas(OrtoDatas? ortoDatas)
            : base(ortoDatas)
        {
            if (ortoDatas is not null)
            {
                BoundingBox2D = ortoDatas.BoundingBox2D;
                SubdivisionId = ortoDatas.SubdivisionId;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OrtoDatas class.
        /// </summary>
        public OrtoDatas()
        {
        }

        /// <summary>
        /// Gets or sets the bounding box of the orthophoto data.
        /// </summary>
        [JsonInclude, JsonPropertyName("BoundingBox2D")]
        public BoundingBox2D? BoundingBox2D { get; set; }

        /// <summary>
        /// Gets or sets the ID of the subdivision to which this orthophoto data belongs.
        /// </summary>
        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}