using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DOccupancyDataPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<Building2DOccupancyData, IOccupancyData>
    {
        public Building2DOccupancyDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => Constants.TableName.OccupancyData_Building2D;

        protected override Building2DOccupancyData Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new Building2DOccupancyData()
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