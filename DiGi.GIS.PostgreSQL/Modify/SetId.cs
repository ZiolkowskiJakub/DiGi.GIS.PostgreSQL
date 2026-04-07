using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        public static bool SetId(this AdministrativeAreal2D? administrativeAreal2D_Destination, AdministrativeAreal2D? administrativeAreal2D_Source)
        {
            if (administrativeAreal2D_Destination is null || administrativeAreal2D_Source is null)
            {
                return false;
            }

            if (administrativeAreal2D_Source.AdministrativeArealType == Enums.AdministrativeArealType.Country)
            {
                administrativeAreal2D_Destination.CountryId = administrativeAreal2D_Source.Id;
                return true;
            }

            if (administrativeAreal2D_Source.AdministrativeArealType == Enums.AdministrativeArealType.Voivodeship)
            {
                administrativeAreal2D_Destination.VoivodeshipId = administrativeAreal2D_Source.Id;
                return true;
            }

            if (administrativeAreal2D_Source.AdministrativeArealType == Enums.AdministrativeArealType.County)
            {
                administrativeAreal2D_Destination.CountyId = administrativeAreal2D_Source.Id;
                return true;
            }

            if (administrativeAreal2D_Source.AdministrativeArealType == Enums.AdministrativeArealType.Municipality)
            {
                administrativeAreal2D_Destination.MunicipalityId = administrativeAreal2D_Source.Id;
                return true;
            }

            return false;
        }
    }
}