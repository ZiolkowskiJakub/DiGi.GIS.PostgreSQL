using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class BuildingModelPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<BuildingModel, Analytical.Building.Classes.BuildingModel>
    {
        public BuildingModelPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => Constants.TableName.BuildingModel;

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