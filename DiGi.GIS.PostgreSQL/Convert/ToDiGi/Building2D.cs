using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static UBuilding2D? ToDiGi<UBuilding2D>(this Building2D? building2D) where UBuilding2D : GIS.Classes.Building2D
        {
            if (building2D is null)
            {
                return null;
            }

            UBuilding2D? result = Core.Create.SerializableObject<UBuilding2D>(building2D.Object);

            return result;
        }
    }
}