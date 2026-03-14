using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DPostgreSQLRefreshOptions : SerializableOptions
    {
        public Building2DPostgreSQLRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public Building2DPostgreSQLRefreshOptions()
            : base()
        {
        }

        public Building2DPostgreSQLRefreshOptions(Building2DPostgreSQLRefreshOptions building2DPostgreSQLRefreshOptions)
            : base(building2DPostgreSQLRefreshOptions)
        {
            if (building2DPostgreSQLRefreshOptions is not null)
            {
                this.BatchSize = building2DPostgreSQLRefreshOptions.BatchSize;
                this.Tolerance = building2DPostgreSQLRefreshOptions.Tolerance;
                this.OverrideExistingSubdivisionIds = building2DPostgreSQLRefreshOptions.OverrideExistingSubdivisionIds;
            }
        }

        [JsonInclude, JsonPropertyName("BatchSize")]
        public int BatchSize { get; set; } = 100;

        [JsonInclude, JsonPropertyName("OverrideExistingSubdivisionIds")]
        public bool OverrideExistingSubdivisionIds { get; set; } = false;

        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constans.Tolerance.MacroDistance;
    }
}