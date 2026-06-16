using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents the options for updating building data in a PostgreSQL database, specifying which types of building data updates should be performed.
    /// </summary>
    public class PostgreSQLBuildingDataUpdateOptions : SerializableOptions
    {
        /// <summary>
        /// Gets or sets the collection of building data update types that specify which types of building data updates should be performed.
        /// </summary>
        public IEnumerable<BuildingDataUpdateType>? BuildingDataUpdateTypes { get; set; } = [BuildingDataUpdateType.General, BuildingDataUpdateType.Database];

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataUpdateOptions"/> class.
        /// </summary>
        public PostgreSQLBuildingDataUpdateOptions()
        {

        }
    }
}
