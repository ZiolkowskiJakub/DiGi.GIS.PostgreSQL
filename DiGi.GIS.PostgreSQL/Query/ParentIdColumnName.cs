using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the name of the database column that stores the identifier of the parent administrative area for a given administrative areal type.
        /// </summary>
        /// <param name="administrativeArealType">The type of the administrative area.</param>
        /// <returns>The name of the parent ID column as a string, or null if no parent column exists for the specified type.</returns>
        public static string? ParentIdColumnName(this AdministrativeArealType administrativeArealType)
        {
            AdministrativeArealType administrativeArealType_Parent = ParentAdministrativeArealType(administrativeArealType) ?? Enums.AdministrativeArealType.Undefined;

            return administrativeArealType_Parent switch
            {
                Enums.AdministrativeArealType.Undefined => null,
                Enums.AdministrativeArealType.Country => "country_id",
                Enums.AdministrativeArealType.Voivodeship => "voivodeship_id",
                Enums.AdministrativeArealType.County => "county_id",
                Enums.AdministrativeArealType.Municipality => "municipality_id",
                Enums.AdministrativeArealType.Subdivison => null,
                _ => null,
            };
        }
    }
}