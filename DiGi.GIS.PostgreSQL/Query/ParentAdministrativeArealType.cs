using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the parent administrative areal type for the specified administrative areal type based on the administrative hierarchy.
        /// </summary>
        /// <param name="administrativeArealType">The current administrative areal type.</param>
        /// <returns>The parent administrative areal type, or <c>null</c> if no parent exists (e.g., for Country or Undefined).</returns>
        public static AdministrativeArealType? ParentAdministrativeArealType(this AdministrativeArealType administrativeArealType)
        {
            return administrativeArealType switch
            {
                Enums.AdministrativeArealType.Undefined or Enums.AdministrativeArealType.Country => null,
                Enums.AdministrativeArealType.Voivodeship => Enums.AdministrativeArealType.Country,
                Enums.AdministrativeArealType.County => Enums.AdministrativeArealType.Voivodeship,
                Enums.AdministrativeArealType.Municipality => Enums.AdministrativeArealType.County,
                Enums.AdministrativeArealType.Subdivison => Enums.AdministrativeArealType.Municipality,
                _ => null,
            };
        }
    }
}