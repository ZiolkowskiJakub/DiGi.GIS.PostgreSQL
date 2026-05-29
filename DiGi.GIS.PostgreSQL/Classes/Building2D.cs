using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a 2D building entity with county and subdivision identification.
    /// </summary>
    public class Building2D : Areal2D<GIS.Classes.Building2D>
    {
        /// <summary>
        /// Initializes a new instance of the Building2D class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Building2D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2D class by copying data from another Building2D instance.
        /// </summary>
        /// <param name="building2D">The Building2D instance to copy data from.</param>
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

        /// <summary>
        /// Initializes a new instance of the Building2D class.
        /// </summary>
        public Building2D()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the ID of the county associated with this building.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the building.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the subdivision to which this building belongs.
        /// </summary>
        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}