using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2DPostgreSQLRefreshOptions : SerializableOptions
    {
        public AdministrativeAreal2DPostgreSQLRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public AdministrativeAreal2DPostgreSQLRefreshOptions()
            : base()
        {
        }

        public AdministrativeAreal2DPostgreSQLRefreshOptions(AdministrativeAreal2DPostgreSQLRefreshOptions administrativeAreal2DPostgreSQLRefreshOptions)
            : base(administrativeAreal2DPostgreSQLRefreshOptions)
        {
            if (administrativeAreal2DPostgreSQLRefreshOptions is not null)
            {
                Tolerance = administrativeAreal2DPostgreSQLRefreshOptions.Tolerance;
            }
        }

        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}