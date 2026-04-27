using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2DOccupancyDataPostgreSQLConverter : AdministrativeAreal2DReferencedObjectPostgreSQLConverter<AdministrativeAreal2DOccupancyData, IOccupancyData>
    {
        public AdministrativeAreal2DOccupancyDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => Constants.TableName.OccupancyData_AdministrativeAreal2D;

        protected override AdministrativeAreal2DOccupancyData Create(int id, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new AdministrativeAreal2DOccupancyData()
            {
                Id = id,
                UniqueId = uniqueId,
                Reference = reference,
                Object = @object,
                CreatedAt = createdAt
            };
        }
    }
}