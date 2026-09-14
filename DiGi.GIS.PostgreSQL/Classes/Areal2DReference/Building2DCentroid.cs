using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents the bounding-box centre (centroid) of a 2D building, keyed by its reference and county partition, for fast 2D dot rendering without loading the full geometry.
    /// <para><see cref="Areal2DReference.Reference"/> is unique only in combination with <see cref="Areal2DReference.CountyId"/> - a building reference is stored once per county part it was imported under, so the county identifier must travel with the reference for any caller-side join. Row order on the wire is not contractual.</para>
    /// </summary>
    public class Building2DCentroid : Areal2DReference
    {
        /// <summary>
        /// Initializes a new instance of the Building2DCentroid class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Building2DCentroid(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DCentroid class by copying data from another Building2DCentroid instance.
        /// </summary>
        /// <param name="building2DCentroid">The Building2DCentroid instance to copy data from.</param>
        public Building2DCentroid(Building2DCentroid? building2DCentroid)
            : base(building2DCentroid)
        {
            if (building2DCentroid is not null)
            {
                X = building2DCentroid.X;
                Y = building2DCentroid.Y;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Building2DCentroid class.
        /// </summary>
        public Building2DCentroid()
        {
        }

        /// <summary>
        /// Gets or sets the X coordinate of the centroid.
        /// </summary>
        [JsonInclude, JsonPropertyName("X")]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the centroid.
        /// </summary>
        [JsonInclude, JsonPropertyName("Y")]
        public double Y { get; set; }
    }
}
