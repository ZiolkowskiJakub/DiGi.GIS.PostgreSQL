using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static UAdministrativeAreal2D? ToDiGi<UAdministrativeAreal2D>(this AdministrativeAreal2D? administrativeAreal2D) where UAdministrativeAreal2D : GIS.Classes.AdministrativeAreal2D
        {
            if (administrativeAreal2D is null)
            {
                return null;
            }

            UAdministrativeAreal2D? result = Core.Create.SerializableObject<UAdministrativeAreal2D>(administrativeAreal2D.Object);

            return result;
        }
    }
}