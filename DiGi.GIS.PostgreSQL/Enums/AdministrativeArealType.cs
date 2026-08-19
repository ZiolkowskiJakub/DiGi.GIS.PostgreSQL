using System.ComponentModel;

namespace DiGi.GIS.PostgreSQL.Enums
{
    /// <summary>
    /// Represents the type of administrative area.
    /// <para>The value is the <c>type_id</c> column of <c>administrative_areal_2d</c> and travels the wire as an <b>integer</b>. One row is stored per polygon part of a unit, so a level holds more rows than there are real units - counties are 406 rows for 380 codes, and both country and voivodeship are 406 rows because every county part carries its own ancestor chain.</para>
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
        /// <para>Renamed from the misspelled <c>Subdivison</c> (missing the second <c>i</c>), which was the accepted wire token until this build. <c>Subdivison</c> is now <b>rejected</b> with HTTP 400 - send <c>Subdivision</c> or the integer <c>4</c>. The value is unchanged, so nothing stored under <c>type_id</c> is affected.</para>
        /// </summary>
        [Description("Subdivision")] Subdivision = 4,
    }
}
