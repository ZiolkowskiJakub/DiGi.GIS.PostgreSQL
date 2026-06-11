using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides functionality to convert building 2D occupancy data for PostgreSQL database operations.
    /// </summary>
    public class Building2DOccupancyDataPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<Building2DOccupancyData, IOccupancyData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DOccupancyDataPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to connect to the PostgreSQL database.</param>
        public Building2DOccupancyDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the table in the PostgreSQL database where building 2D occupancy data is stored.
        /// </summary>
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