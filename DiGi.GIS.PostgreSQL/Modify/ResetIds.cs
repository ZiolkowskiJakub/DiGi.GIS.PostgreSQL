using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        public static bool ResetIds(this AdministrativeAreal2D? administrativeAreal2D)
        {
            if (administrativeAreal2D is null)
            {
                return false;
            }

            administrativeAreal2D.CountryId = null;
            administrativeAreal2D.CountyId = null;
            administrativeAreal2D.MunicipalityId = null;
            administrativeAreal2D.VoivodeshipId = null;

            return true;
        }
    }
}