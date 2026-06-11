using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        /// <summary>
        /// Resets the administrative identifiers of the specified administrative areal 2D object to null.
        /// </summary>
        /// <param name="administrativeAreal2D">The administrative areal 2D object whose identifiers are to be reset.</param>
        /// <returns>True if the identifiers were successfully reset; otherwise, false.</returns>
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