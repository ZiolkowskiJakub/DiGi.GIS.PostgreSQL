using DiGi.Core.Classes;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides options for refreshing orthodata within a PostgreSQL database.
    /// </summary>
    public class PostgreSQLOrtoDatasRefreshOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshOptions" /> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public PostgreSQLOrtoDatasRefreshOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshOptions" /> class.
        /// </summary>
        public PostgreSQLOrtoDatasRefreshOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLOrtoDatasRefreshOptions" /> class by copying an existing options instance.
        /// </summary>
        /// <param name="postgreSQLOrtoDatasRefreshOptions">The source options instance to copy from.</param>
        public PostgreSQLOrtoDatasRefreshOptions(PostgreSQLOrtoDatasRefreshOptions postgreSQLOrtoDatasRefreshOptions)
            : base(postgreSQLOrtoDatasRefreshOptions)
        {
            if (postgreSQLOrtoDatasRefreshOptions is not null)
            {
                CountyIds = postgreSQLOrtoDatasRefreshOptions.CountyIds == null ? null : [.. postgreSQLOrtoDatasRefreshOptions.CountyIds];
                OverrideExistsing = postgreSQLOrtoDatasRefreshOptions.OverrideExistsing;
                UpdateSubdivisionIds = postgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds;
            }
        }

        /// <summary>
        /// Gets or sets the set of county identifiers for which data should be refreshed.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyIds")]
        public HashSet<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether existing data should be overridden during the refresh process.
        /// </summary>
        [JsonInclude, JsonPropertyName("OverrideExistsing")]
        public bool OverrideExistsing { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether subdivision identifiers should be updated during the refresh process.
        /// </summary>
        [JsonInclude, JsonPropertyName("UpdateSubdivisionIds")]
        public bool UpdateSubdivisionIds { get; set; } = true;
    }
}