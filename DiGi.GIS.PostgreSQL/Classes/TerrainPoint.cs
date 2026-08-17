using DiGi.Core.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// One row of the terrain point table: a single elevation point and the administrative identifiers it is filed under.
    /// <para>Named for what one row is, as the rest of this family is - it is a point, not a terrain. The terrain assembled from many of these is <see cref="DiGi.Analytical.Building.Classes.PointCloudTerrain"/>, which wraps a whole <see cref="Geometry.PointCloud.Spatial.Classes.PointCloud3D"/>.</para>
    /// </summary>
    public class TerrainPoint : SerializableObject, ITableObject, IGISPostgreSQLSerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPoint"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public TerrainPoint(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPoint"/> class by copying data from another <see cref="TerrainPoint"/> instance.
        /// </summary>
        /// <param name="terrainPoint">The <see cref="TerrainPoint"/> instance to copy data from.</param>
        public TerrainPoint(TerrainPoint? terrainPoint)
            : base(terrainPoint)
        {
            if (terrainPoint is not null)
            {
                CountyId = terrainPoint.CountyId;
                SubdivisionId = terrainPoint.SubdivisionId;
                Point3D = terrainPoint.Point3D;
                CreatedAt = terrainPoint.CreatedAt;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPoint"/> class with specified county, 3D point, and optional subdivision identifier.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county.</param>
        /// <param name="point3D">The <see cref="Point3D"/> representing the terrain point coordinates (X, Y, Z).</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision.</param>
        public TerrainPoint(int? countyId, Point3D? point3D, int? subdivisionId = null)
        {
            CountyId = countyId;
            Point3D = point3D;
            SubdivisionId = subdivisionId;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPoint"/> class.
        /// </summary>
        public TerrainPoint()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the county identifier associated with this terrain point.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the subdivision identifier associated with this terrain point.
        /// </summary>
        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }

        /// <summary>
        /// Gets or sets the 3D point coordinates (X, Y, Z) of the terrain elevation point.
        /// </summary>
        [JsonInclude, JsonPropertyName("Point3D")]
        public Point3D? Point3D { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp of the record.
        /// <para>Stamped by the primary constructor rather than a property initializer, so a point read back from the database or copied keeps the timestamp it was stored with instead of being restamped on every construction.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("CreatedAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
