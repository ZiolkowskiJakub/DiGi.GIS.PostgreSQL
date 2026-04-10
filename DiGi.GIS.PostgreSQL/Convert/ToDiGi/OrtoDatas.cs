using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        public static GIS.Classes.OrtoDatas? ToDiGi(this OrtoDatas? ortoDatas)
        {
            if (ortoDatas is null)
            {
                return null;
            }

            GIS.Classes.OrtoDatas? result = Core.Create.SerializableObject<GIS.Classes.OrtoDatas>(ortoDatas.Object);

            return result;
        }
    }
}