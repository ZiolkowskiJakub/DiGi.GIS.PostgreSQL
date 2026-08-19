using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the name of the database column that stores the identifier of the parent administrative area for a given administrative areal type.
        /// <para>This is the column of the level <b>directly above</b>. A search that has to step over an empty level - m. Poznan holds no <c>gmina</c> feature, so its subdivisions hang off the county - knows which ancestor it matched and should use <see cref="IdColumnName(AdministrativeArealType)"/> with that type instead.</para>
        /// </summary>
        /// <param name="administrativeArealType">The type of the administrative area.</param>
        /// <returns>The name of the parent ID column as a string, or null if no parent column exists for the specified type.</returns>
        public static string? ParentIdColumnName(this AdministrativeArealType administrativeArealType)
        {
            return IdColumnName(ParentAdministrativeArealType(administrativeArealType) ?? Enums.AdministrativeArealType.Undefined);
        }
    }
}