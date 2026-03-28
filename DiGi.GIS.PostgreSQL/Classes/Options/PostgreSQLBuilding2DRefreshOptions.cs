using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLBuilding2DRefreshOptions : SerializableOptions
    {
        public PostgreSQLBuilding2DRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public PostgreSQLBuilding2DRefreshOptions()
            : base()
        {
        }

        public PostgreSQLBuilding2DRefreshOptions(PostgreSQLBuilding2DRefreshOptions postgreSQLBuilding2DRefreshOptions)
            : base(postgreSQLBuilding2DRefreshOptions)
        {
            if (postgreSQLBuilding2DRefreshOptions is not null)
            {
                BatchSize = postgreSQLBuilding2DRefreshOptions.BatchSize;
                Tolerance = postgreSQLBuilding2DRefreshOptions.Tolerance;
                OverrideExistingSubdivisionIds = postgreSQLBuilding2DRefreshOptions.OverrideExistingSubdivisionIds;
            }
        }

        [JsonInclude, JsonPropertyName("BatchSize")]
        public int BatchSize { get; set; } = 1000;

        [JsonInclude, JsonPropertyName("OverrideExistingSubdivisionIds")]
        public bool OverrideExistingSubdivisionIds { get; set; } = false;

        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.MacroDistance;
    }
}