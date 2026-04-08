using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
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