using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents year built data associated with a 2D building.
    /// </summary>
    public class YearBuiltData : Building2DReferencedObject<IYearBuiltData>
    {
        /// <summary>
        /// Initializes a new instance of the YearBuiltData class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public YearBuiltData(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the YearBuiltData class by copying data from another YearBuiltData instance.
        /// </summary>
        /// <param name="yearBuiltData">The YearBuiltData instance to copy data from.</param>
        public YearBuiltData(YearBuiltData? yearBuiltData)
            : base(yearBuiltData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the YearBuiltData class.
        /// </summary>
        public YearBuiltData()
            : base()
        {
        }
    }
}