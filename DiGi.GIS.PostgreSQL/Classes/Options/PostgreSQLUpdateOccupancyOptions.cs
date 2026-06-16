using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLUpdateOccupancyOptions : SerializableOptions
    {
        public PostgreSQLUpdateOccupancyOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public PostgreSQLUpdateOccupancyOptions()
            : base()
        {
        }

        public PostgreSQLUpdateOccupancyOptions(PostgreSQLUpdateOccupancyOptions postgreSQLUpdateOccupancyOptions)
            : base(postgreSQLUpdateOccupancyOptions)
        {
            if (postgreSQLUpdateOccupancyOptions is not null)
            {
                IncludeBuilding2Ds = postgreSQLUpdateOccupancyOptions.IncludeBuilding2Ds;
                IncludeAdministrativeAreal2Ds = postgreSQLUpdateOccupancyOptions.IncludeAdministrativeAreal2Ds;
            }
        }

        [JsonInclude, JsonPropertyName(nameof(IncludeBuilding2Ds))]
        public bool IncludeBuilding2Ds { get; set; } = true;

        [JsonInclude, JsonPropertyName(nameof(IncludeAdministrativeAreal2Ds))]
        public bool IncludeAdministrativeAreal2Ds { get; set; } = true;
    }
}