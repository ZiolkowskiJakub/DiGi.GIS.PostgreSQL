using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLOrtoDatasRefreshOptions : SerializableOptions
    {
        public PostgreSQLOrtoDatasRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public PostgreSQLOrtoDatasRefreshOptions()
            : base()
        {
        }

        public PostgreSQLOrtoDatasRefreshOptions(PostgreSQLOrtoDatasRefreshOptions postgreSQLOrtoDatasRefreshOptions)
            : base(postgreSQLOrtoDatasRefreshOptions)
        {
            if (postgreSQLOrtoDatasRefreshOptions is not null)
            {
                CountyIds = postgreSQLOrtoDatasRefreshOptions.CountyIds == null ? null : [.. postgreSQLOrtoDatasRefreshOptions.CountyIds];
                OverrideExistsing = postgreSQLOrtoDatasRefreshOptions.OverrideExistsing;
            }
        }

        [JsonInclude, JsonPropertyName("CountyIds")]
        public HashSet<int>? CountyIds { get; set; } = null;

        [JsonInclude, JsonPropertyName("OverrideExistsing")]
        public bool OverrideExistsing { get; set; } = false;
    }
}