using System.ComponentModel;

namespace DiGi.GIS.PostgreSQL.Enums
{
    /// <summary>
    /// Specifies the building data update type.
    /// </summary>
    [Description("BuildingDataUpdateType")]
    public enum BuildingDataUpdateType
    {
        /// <summary>
        /// General data update type, which mostly includes Building2D.
        /// </summary>
        [Description("General")] General,

        /// <summary>
        /// Database data update type.
        /// </summary>
        [Description("Database")] Database,

        /// <summary>
        /// Occupancy data update.
        /// </summary>
        [Description("Occupancy")] Occupancy,

    }
}