using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Converter for administrative areal 2D occupancy data used in PostgreSQL database operations.
    /// </summary>
    public class AdministrativeAreal2DOccupancyDataPostgreSQLConverter : AdministrativeAreal2DReferencedObjectPostgreSQLConverter<AdministrativeAreal2DOccupancyData, IOccupancyData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrativeAreal2DOccupancyDataPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data for the PostgreSQL database.</param>
        public AdministrativeAreal2DOccupancyDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the table associated with administrative areal 2D occupancy data.
        /// </summary>
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