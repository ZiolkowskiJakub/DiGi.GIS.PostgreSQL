using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Converter class for handling PostgreSQL database operations for <see cref="TypologyModel"/> objects.
    /// </summary>
    public class TypologyModelPostgreSQLConverter : AdministrativeAreal2DReferencedObjectPostgreSQLConverter<TypologyModel, Typology.Classes.TypologyModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypologyModelPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to connect to the PostgreSQL database.</param>
        public TypologyModelPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the table in the PostgreSQL database associated with typology models.
        /// </summary>
        public override string TableName => Constants.TableName.TypologyModel;

        /// <summary>
        /// Creates a new instance of <see cref="TypologyModel"/> using the provided data from the database.
        /// </summary>
        /// <param name="id">The primary key identifier.</param>
        /// <param name="uniqueId">The unique string identifier.</param>
        /// <param name="reference">The reference string.</param>
        /// <param name="object">The JSON object containing the model's properties.</param>
        /// <param name="createdAt">The timestamp of when the record was created.</param>
        /// <returns>A newly constructed <see cref="TypologyModel"/> instance.</returns>
        protected override TypologyModel Create(int id, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new TypologyModel()
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