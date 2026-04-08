using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
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