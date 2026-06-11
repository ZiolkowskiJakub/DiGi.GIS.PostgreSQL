using DiGi.GIS.PostgreSQL.Enums;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Determines the administrative areal type based on the provided administrative areal object.
        /// </summary>
        /// <param name="administrativeAreal2D">The administrative areal object to evaluate.</param>
        /// <returns>The corresponding <see cref="Enums.AdministrativeArealType"/>.</returns>
        public static AdministrativeArealType AdministrativeArealType(this GIS.Classes.AdministrativeAreal2D? administrativeAreal2D)
        {
            if (administrativeAreal2D is null)
            {
                return Enums.AdministrativeArealType.Undefined;
            }

            if (administrativeAreal2D is GIS.Classes.AdministrativeSubdivision)
            {
                return Enums.AdministrativeArealType.Subdivison;
            }

            if (administrativeAreal2D is GIS.Classes.AdministrativeDivision administrativeDivision)
            {
                return AdministrativeArealType(administrativeDivision.AdministrativeDivisionType);
            }

            return Enums.AdministrativeArealType.Undefined;
        }

        /// <summary>
        /// Maps a GIS administrative division type to the PostgreSQL administrative areal type.
        /// </summary>
        /// <param name="administrativeDivisionType">The GIS administrative division type.</param>
        /// <returns>The corresponding <see cref="Enums.AdministrativeArealType"/>.</returns>
        public static AdministrativeArealType AdministrativeArealType(this GIS.Enums.AdministrativeDivisionType administrativeDivisionType)
        {
            return administrativeDivisionType switch
            {
                GIS.Enums.AdministrativeDivisionType.country => Enums.AdministrativeArealType.Country,
                GIS.Enums.AdministrativeDivisionType.voivodeship => Enums.AdministrativeArealType.Voivodeship,
                GIS.Enums.AdministrativeDivisionType.municipality => Enums.AdministrativeArealType.Municipality,
                GIS.Enums.AdministrativeDivisionType.district_or_delegation => Enums.AdministrativeArealType.Subdivison,
                GIS.Enums.AdministrativeDivisionType.town_in_urban_rural_municipality => Enums.AdministrativeArealType.Subdivison,
                GIS.Enums.AdministrativeDivisionType.county => Enums.AdministrativeArealType.County,
                _ => Enums.AdministrativeArealType.Undefined,
            };
        }
    }
}