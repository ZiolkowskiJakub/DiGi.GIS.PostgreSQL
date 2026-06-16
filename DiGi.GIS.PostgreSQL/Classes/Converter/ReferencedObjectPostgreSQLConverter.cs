using DiGi.Core.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides a base implementation for converting PostgreSQL database records into referenced objects.
    /// </summary>
    /// <typeparam name="TReferencedObject">The type of the referenced object, which must inherit from <see cref="ReferencedObject{TUniqueObject}"/>.</typeparam>
    /// <typeparam name="TUniqueObject">The type of the unique object identifier, which must implement <see cref="IUniqueObject"/>.</typeparam>
    public abstract class ReferencedObjectPostgreSQLConverter<TReferencedObject, TUniqueObject> : PostgreSQLConverter<TReferencedObject>, IGISPostgreSQLConverter<TReferencedObject> where TReferencedObject : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedObjectPostgreSQLConverter{TReferencedObject, TUniqueObject}"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to establish a connection to the PostgreSQL database.</param>
        public ReferencedObjectPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the table in the PostgreSQL database that this converter operates on.
        /// </summary>
        public abstract string TableName { get; }

        protected abstract TReferencedObject Create(NpgsqlDataReader npgsqlDataReader);

        protected async Task<List<TReferencedObject>?> ReadAsync(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync(npgsqlDataReader, cancellationToken);
        }

        private async Task<List<TReferencedObject>> ReadAsync(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<TReferencedObject> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create(npgsqlDataReader));
            }

            return result;
        }

        /// <summary>
        /// Asynchronously clears all records from the referenced object table using the provided PostgreSQL connection.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database. This value can be null.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the clear operation was successful.</returns>
        public async Task<bool> ClearAsync(NpgsqlConnection? npgsqlConnection, CancellationToken cancellationToken = default)
        {
            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName, cancellationToken);
        }

        /// <summary>
        /// Asynchronously clears all records from the referenced object table in the PostgreSQL database.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the clear operation was successful.</returns>
        public async Task<bool> ClearAsync(CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);

            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await ClearAsync(npgsqlConnection, cancellationToken);
        }
    }
}