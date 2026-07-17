using DiGi.Analytical.Building.Enums;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides functionality to convert building model data for PostgreSQL database operations, scoped to a specific <see cref="DiGi.Analytical.Building.Enums.BuildingModelDetailLevel"/>.
    /// </summary>
    public class BuildingModelPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<BuildingModel, DiGi.Analytical.Building.Classes.BuildingModel>
    {
        /// <summary>
        /// Gets the detail level of the building models handled by this converter, which determines the target table.
        /// </summary>
        private BuildingModelDetailLevel BuildingModelDetailLevel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to connect to the PostgreSQL database.</param>
        /// <param name="buildingModelDetailLevel">The detail level of the building models handled by this converter.</param>
        public BuildingModelPostgreSQLConverter(ConnectionData? connectionData, BuildingModelDetailLevel buildingModelDetailLevel)
            : base(connectionData)
        {
            BuildingModelDetailLevel = buildingModelDetailLevel;
        }

        /// <summary>
        /// Gets the name of the table in the PostgreSQL database where building model data for the configured detail level is stored.
        /// </summary>
        public override string TableName => $"{Constants.TableName.BuildingModel}_{BuildingModelDetailLevel.ToString().ToLowerInvariant()}";

        protected override BuildingModel Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new BuildingModel()
            {
                Id = id,
                CountyId = countyId,
                UniqueId = uniqueId,
                Reference = reference,
                Object = @object,
                CreatedAt = createdAt
            };
        }
    }
}