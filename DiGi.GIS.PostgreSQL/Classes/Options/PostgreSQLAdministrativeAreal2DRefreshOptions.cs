using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLAdministrativeAreal2DRefreshOptions : SerializableOptions
    {
        public PostgreSQLAdministrativeAreal2DRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public PostgreSQLAdministrativeAreal2DRefreshOptions()
            : base()
        {
        }

        public PostgreSQLAdministrativeAreal2DRefreshOptions(PostgreSQLAdministrativeAreal2DRefreshOptions postgreSQLAdministrativeAreal2DRefreshOptions)
            : base(postgreSQLAdministrativeAreal2DRefreshOptions)
        {
            if (postgreSQLAdministrativeAreal2DRefreshOptions is not null)
            {
                Tolerance = postgreSQLAdministrativeAreal2DRefreshOptions.Tolerance;
            }
        }

        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}