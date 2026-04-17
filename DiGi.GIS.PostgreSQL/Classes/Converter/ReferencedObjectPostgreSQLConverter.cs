using DiGi.Core.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class ReferencedObjectPostgreSQLConverter<TReferencedObject, TUniqueObject> : PostgreSQLConverter<TReferencedObject>, IGISPostgreSQLConverter<TReferencedObject> where TReferencedObject : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        public ReferencedObjectPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

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
    }
}