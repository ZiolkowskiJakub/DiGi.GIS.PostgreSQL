using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides a converter for YearBuiltData objects when interacting with a PostgreSQL database.
    /// </summary>
    public class YearBuiltDataPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<YearBuiltData, IYearBuiltData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltDataPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to establish a connection to the PostgreSQL database.</param>
        public YearBuiltDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the database table associated with year built data.
        /// </summary>
        public override string TableName => Constants.TableName.YearBuiltData;

        /// <summary>
        /// Creates an instance of <see cref="YearBuiltData"/> based on the provided database values.
        /// </summary>
        /// <param name="id">The unique identifier of the record.</param>
        /// <param name="countyId">The county identifier associated with the record.</param>
        /// <param name="uniqueId">The unique external identifier for the record.</param>
        /// <param name="reference">The reference string for the record.</param>
        /// <param name="object">The JSON object containing the data attributes.</param>
        /// <param name="createdAt">The timestamp when the record was created.</param>
        /// <returns>A new <see cref="YearBuiltData"/> instance.</returns>
        protected override YearBuiltData Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new YearBuiltData()
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