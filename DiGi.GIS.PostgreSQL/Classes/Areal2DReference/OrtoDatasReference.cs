using DiGi.Geometry.Planar.Classes;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a lightweight reference and metadata projection for orthophoto data associated with a 2D building, omitting the heavy binary payload.
    /// </summary>
    public class OrtoDatasReference : Areal2DReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasReference"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public OrtoDatasReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasReference"/> class by copying data from another <see cref="OrtoDatasReference"/> instance.
        /// </summary>
        /// <param name="ortoDatasReference">The <see cref="OrtoDatasReference"/> instance to copy data from.</param>
        public OrtoDatasReference(OrtoDatasReference? ortoDatasReference)
            : base(ortoDatasReference)
        {
            if (ortoDatasReference is not null)
            {
                Id = ortoDatasReference.Id;
                BoundingBox2D = ortoDatasReference.BoundingBox2D is null ? null : new BoundingBox2D(ortoDatasReference.BoundingBox2D);
                SubdivisionId = ortoDatasReference.SubdivisionId;
                CreatedAt = ortoDatasReference.CreatedAt;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasReference"/> class.
        /// </summary>
        public OrtoDatasReference()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasReference"/> class from an <see cref="OrtoDatas"/> instance.
        /// </summary>
        /// <param name="ortoDatas">The <see cref="OrtoDatas"/> instance to reference. This value can be null.</param>
        public OrtoDatasReference(OrtoDatas? ortoDatas)
        {
            if (ortoDatas is not null)
            {
                Id = ortoDatas.Id;
                BoundingBox2D = ortoDatas.BoundingBox2D is null ? null : new BoundingBox2D(ortoDatas.BoundingBox2D);
                SubdivisionId = ortoDatas.SubdivisionId;
                CountyId = ortoDatas.CountyId;
                Reference = ortoDatas.Reference;
                CreatedAt = ortoDatas.CreatedAt;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasReference"/> class from a <see cref="Building2DReference"/> instance.
        /// </summary>
        /// <param name="building2DReference">The <see cref="Building2DReference"/> instance to reference. This value can be null.</param>
        public OrtoDatasReference(Building2DReference? building2DReference)
        {
            if (building2DReference is not null)
            {
                Id = building2DReference.Id;
                SubdivisionId = building2DReference.SubdivisionId;
                CountyId = building2DReference.CountyId;
                Reference = building2DReference.Reference;
            }
        }

        /// <summary>
        /// Gets or sets the unique ID of the orthophoto data record.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

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

        /// <summary>
        /// Gets or sets the UTC timestamp when this orthophoto record was created.
        /// </summary>
        [JsonInclude, JsonPropertyName("CreatedAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
