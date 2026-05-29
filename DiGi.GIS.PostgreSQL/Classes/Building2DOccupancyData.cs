using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents occupancy data associated with a 2D building.
    /// </summary>
    public class Building2DOccupancyData : Building2DReferencedObject<IOccupancyData>
    {
        /// <summary>
        /// Initializes a new instance of the Building2DOccupancyData class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Building2DOccupancyData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DOccupancyData class by copying data from another Building2DOccupancyData instance.
        /// </summary>
        /// <param name="building2DOccupancyData">The Building2DOccupancyData instance to copy data from.</param>
        public Building2DOccupancyData(Building2DOccupancyData? building2DOccupancyData)
            : base(building2DOccupancyData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DOccupancyData class.
        /// </summary>
        public Building2DOccupancyData()
            : base()
        {
        }
    }
}