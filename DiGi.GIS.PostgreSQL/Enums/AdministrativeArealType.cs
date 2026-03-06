using System.ComponentModel;

namespace DiGi.GIS.PostgreSQL.Enums
{
    public enum AdministrativeArealType
    {
        [Description("Undefined")] Undefined = -1,
        [Description("Country")] Country = 0,
        [Description("Voivodeship")] Voivodeship = 1,
        [Description("County")] County = 2,
        [Description("Municipality")] Municipality = 3,
        [Description("Subdivision")] Subdivison = 4,
    }
}