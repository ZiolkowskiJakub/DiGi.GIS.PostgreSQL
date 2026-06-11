using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the child administrative areal type for the specified administrative areal type based on the administrative hierarchy.
        /// </summary>
        /// <param name="administrativeArealType">The current administrative areal type.</param>
        /// <returns>The next level of administrative areal type in the hierarchy, or null if no child exists or the input is undefined.</returns>
        public static AdministrativeArealType? ChildAdministrativeArealType(this AdministrativeArealType administrativeArealType)
        {
            return administrativeArealType switch
            {
                Enums.AdministrativeArealType.Undefined => null,
                Enums.AdministrativeArealType.Country => Enums.AdministrativeArealType.Voivodeship,
                Enums.AdministrativeArealType.Voivodeship => Enums.AdministrativeArealType.County,
                Enums.AdministrativeArealType.County => Enums.AdministrativeArealType.Municipality,
                Enums.AdministrativeArealType.Municipality => Enums.AdministrativeArealType.Subdivison,
                Enums.AdministrativeArealType.Subdivison => null,
                _ => null,
            };
        }
    }
}