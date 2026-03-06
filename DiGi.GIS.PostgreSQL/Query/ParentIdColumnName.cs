using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        public static string? ParentIdColumnName(this AdministrativeArealType administrativeArealType)
        {
            return administrativeArealType switch
            {
                Enums.AdministrativeArealType.Undefined or Enums.AdministrativeArealType.Country => null,
                Enums.AdministrativeArealType.Voivodeship => "country_id",
                Enums.AdministrativeArealType.County => "voivodeship_id",
                Enums.AdministrativeArealType.Municipality => "county_id",
                Enums.AdministrativeArealType.Subdivison => "municipality_id",
                _ => null,
            };
        }
    }
}