using DiGi.CityGML.Classes;
using DiGi.Core.Parameter.Classes;
using System.ComponentModel;

namespace DiGi.GIS.PostgreSQL.Enums
{
    /// <summary>
    /// Parameters applicable to a Building.
    /// </summary>
    [AssociatedTypes(typeof(Building)), Description("Building Parameter")]
    public enum BuildingParameter
    {
        /// <summary>
        /// Level of Detail.
        /// </summary>
        [ParameterProperties("eecae0d8-b699-4613-9cd2-9bb94078c9e8", "LOD", "Level Of Detail", Core.Parameter.Enums.AccessType.Read), StringParameterValue()] LOD,

        /// <summary>
        /// Model year.
        /// </summary>
        [ParameterProperties("3ec1627e-001c-4a19-a227-eaf57a85f87b", "Year", "Year", Core.Parameter.Enums.AccessType.Read), IntegerParameterValue()] Year,

        /// <summary>
        /// Area code.
        /// </summary>
        [ParameterProperties("14030ee5-681e-4f5d-ab0e-4c2d13185230", "Code", "Code", Core.Parameter.Enums.AccessType.Read), StringParameterValue()] Code,

        /// <summary>
        /// Source information.
        /// </summary>
        [ParameterProperties("1c8aa27e-230e-4c36-9399-62f4dd393a74", "Source", "Source", Core.Parameter.Enums.AccessType.Read), StringParameterValue()] Source,
    }
}