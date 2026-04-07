using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        public static bool UpdateIds(this AdministrativeAreal2D? administrativeAreal2D_Destination, AdministrativeAreal2D? administrativeAreal2D_Source)
        {
            if (administrativeAreal2D_Destination is null || administrativeAreal2D_Source is null)
            {
                return false;
            }

            administrativeAreal2D_Destination.CountryId = administrativeAreal2D_Source.CountryId;
            administrativeAreal2D_Destination.CountyId = administrativeAreal2D_Source.CountyId;
            administrativeAreal2D_Destination.MunicipalityId = administrativeAreal2D_Source.MunicipalityId;
            administrativeAreal2D_Destination.VoivodeshipId = administrativeAreal2D_Source.VoivodeshipId;

            SetId(administrativeAreal2D_Destination, administrativeAreal2D_Source);

            return true;
        }
    }
}