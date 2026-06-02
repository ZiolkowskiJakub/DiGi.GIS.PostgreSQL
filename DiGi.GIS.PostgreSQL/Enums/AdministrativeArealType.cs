using System.ComponentModel;

namespace DiGi.GIS.PostgreSQL.Enums
{
    /// <summary>
    /// Represents the type of administrative area.
    /// </summary>
    public enum AdministrativeArealType
    {
        /// <summary>
        /// Undefined administrative area type.
        /// </summary>
        [Description("Undefined")] Undefined = -1,

        /// <summary>
        /// Country level administrative area.
        /// </summary>
        [Description("Country")] Country = 0,

        /// <summary>
        /// Voivodeship (province) level administrative area.
        /// </summary>
        [Description("Voivodeship")] Voivodeship = 1,

        /// <summary>
        /// County (powiat) level administrative area.
        /// </summary>
        [Description("County")] County = 2,

        /// <summary>
        /// Municipality (gmina) level administrative area.
        /// </summary>
        [Description("Municipality")] Municipality = 3,

        /// <summary>
        /// Subdivision level administrative area.
        /// </summary>
        [Description("Subdivision")] Subdivison = 4,
    }
}