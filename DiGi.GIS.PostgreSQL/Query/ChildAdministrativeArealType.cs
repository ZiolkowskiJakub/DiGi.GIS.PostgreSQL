using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
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