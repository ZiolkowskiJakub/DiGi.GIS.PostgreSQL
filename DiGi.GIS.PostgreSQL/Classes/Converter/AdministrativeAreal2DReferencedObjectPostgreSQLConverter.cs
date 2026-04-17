using DiGi.Core.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class AdministrativeAreal2DReferencedObjectPostgreSQLConverter<TAdministrativeAreal2DReferencedObject, TUniqueObject> : ReferencedObjectPostgreSQLConverter<TAdministrativeAreal2DReferencedObject, TUniqueObject> where TAdministrativeAreal2DReferencedObject : AdministrativeAreal2DReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        public AdministrativeAreal2DReferencedObjectPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public async Task<TAdministrativeAreal2DReferencedObject?> GetItemByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetItemsByIdsAsync([id], cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        public async Task<TAdministrativeAreal2DReferencedObject?> GetItemByReferenceAsync(string reference, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        public async Task<List<TAdministrativeAreal2DReferencedObject>?> GetItemsByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int>? ids, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || ids is null)
            {
                return null;
            }

            List<TAdministrativeAreal2DReferencedObject>? result = [];

            if (!ids.Any())
            {
                return result;
            }

            string commandText = $@"
                SELECT id, unique_id, reference, object, created_at
                FROM {TableName}
                WHERE id = ANY(@ids);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("ids", ids.ToArray());

            result = await ReadAsync(npgsqlCommand, cancellationToken);

            return result;
        }

        public async Task<List<TAdministrativeAreal2DReferencedObject>?> GetItemsByIdsAsync(IEnumerable<int>? ids, CancellationToken cancellationToken = default)
        {
            if (ids is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetItemsByIdsAsync(npgsqlConnection, ids, cancellationToken);
        }

        public async Task<List<TAdministrativeAreal2DReferencedObject>?> GetItemsByReferenceAsync(string reference, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], cancellationToken);
        }

        public async Task<List<TAdministrativeAreal2DReferencedObject>?> GetItemsByReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? references, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || references is null)
            {
                return null;
            }

            List<TAdministrativeAreal2DReferencedObject>? result = [];

            if (!references.Any())
            {
                return result;
            }

            string commandText = $@"
                SELECT id, unique_id, reference, object, created_at
                FROM {TableName}
                WHERE reference = ANY(@references);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());

            result = await ReadAsync(npgsqlCommand, cancellationToken);

            return result;
        }

        public async Task<List<TAdministrativeAreal2DReferencedObject>?> GetItemsByReferencesAsync(IEnumerable<string>? references, CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetItemsByReferencesAsync(npgsqlConnection, references, cancellationToken);
        }

        public async Task<HashSet<int>?> UpdateAsync(IEnumerable<TAdministrativeAreal2DReferencedObject>? administrativeAreal2DReferencedObjects)
        {
            if (administrativeAreal2DReferencedObjects is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(npgsqlConnection, TableName);
            if (!succeded)
            {
                return null;
            }

            HashSet<int> result = [];

            if (!administrativeAreal2DReferencedObjects.Any())
            {
                return result;
            }

            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (TAdministrativeAreal2DReferencedObject administrativeAreal2DReferencedObject in administrativeAreal2DReferencedObjects)
            {
                if (administrativeAreal2DReferencedObject is null)
                {
                    continue;
                }

                // SQL with full update on conflict (excluding ID)
                NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO {TableName} (unique_id, reference, object)
                    VALUES (@unique_id, @reference, @object)
                    ON CONFLICT (unique_id)
                    DO UPDATE SET
                        object = EXCLUDED.object,
                        reference = EXCLUDED.reference
                    RETURNING id;");

                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("unique_id", NpgsqlDbType.Text)
                {
                    Value = administrativeAreal2DReferencedObject.UniqueId
                });

                // Adding parameters with explicit NpgsqlDbType
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("reference", NpgsqlDbType.Text) { Value = administrativeAreal2DReferencedObject.Reference });

                // Handling potential null for JSONB object
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                {
                    Value = (object?)administrativeAreal2DReferencedObject.Object?.ToJsonString() ?? DBNull.Value
                });

                npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
            }

            // Execute batch and collect IDs
            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync();

            do
            {
                while (await npgsqlDataReader.ReadAsync())
                {
                    // The RETURNING id works for both INSERT and UPDATE cases
                    int id = npgsqlDataReader.GetInt32(0);
                    result.Add(id);
                }
            }
            while (await npgsqlDataReader.NextResultAsync());

            return result;
        }

        protected abstract TAdministrativeAreal2DReferencedObject Create(int id, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt);

        protected override TAdministrativeAreal2DReferencedObject Create(NpgsqlDataReader npgsqlDataReader)
        {
            return Create(
                npgsqlDataReader.GetInt32(0),
                npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetString(1),
                npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                JsonNode.Parse(npgsqlDataReader.GetString(3)) as JsonObject,
                npgsqlDataReader.IsDBNull(4) ? null : npgsqlDataReader.GetDateTime(4)
                );
        }
    }
}