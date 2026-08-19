using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the name of the database column that stores the identifier of an administrative area of the given type.
        /// <para>This is the column to filter on when the ancestor being matched is known outright, which is not always the level directly above the rows being read - see <see cref="ParentIdColumnName(AdministrativeArealType)"/> for the relative form.</para>
        /// </summary>
        /// <param name="administrativeArealType">The type of the administrative area whose identifier is stored.</param>
        /// <returns>The name of the column holding an identifier of that type, or <see langword="null"/> when no column stores one - nothing references a Subdivision.</returns>
        public static string? IdColumnName(this AdministrativeArealType administrativeArealType)
        {
            return administrativeArealType switch
            {
                Enums.AdministrativeArealType.Country => "country_id",
                Enums.AdministrativeArealType.Voivodeship => "voivodeship_id",
                Enums.AdministrativeArealType.County => "county_id",
                Enums.AdministrativeArealType.Municipality => "municipality_id",
                _ => null,
            };
        }
    }
}
