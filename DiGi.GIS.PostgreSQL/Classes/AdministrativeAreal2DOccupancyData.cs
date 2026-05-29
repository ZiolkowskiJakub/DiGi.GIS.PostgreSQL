using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents occupancy data for a 2D administrative area.
    /// </summary>
    public class AdministrativeAreal2DOccupancyData : AdministrativeAreal2DReferencedObject<IOccupancyData>
    {
        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DOccupancyData class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public AdministrativeAreal2DOccupancyData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DOccupancyData class by copying data from another AdministrativeAreal2DOccupancyData instance.
        /// </summary>
        /// <param name="administrativeAreal2DOccupancyData">The AdministrativeAreal2DOccupancyData instance to copy data from.</param>
        public AdministrativeAreal2DOccupancyData(AdministrativeAreal2DOccupancyData? administrativeAreal2DOccupancyData)
            : base(administrativeAreal2DOccupancyData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DOccupancyData class.
        /// </summary>
        public AdministrativeAreal2DOccupancyData()
            : base()
        {
        }
    }
}