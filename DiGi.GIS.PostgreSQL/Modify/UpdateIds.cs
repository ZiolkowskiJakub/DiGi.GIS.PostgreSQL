using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        /// <summary>
        /// Updates the identification properties of the destination administrative areal object using values from the source object.
        /// </summary>
        /// <param name="administrativeAreal2D_Destination">The destination AdministrativeAreal2D object to be updated.</param>
        /// <param name="administrativeAreal2D_Source">The source AdministrativeAreal2D object containing the new identification values.</param>
        /// <returns>True if the IDs were successfully updated; otherwise, false if either the destination or source object is null.</returns>
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