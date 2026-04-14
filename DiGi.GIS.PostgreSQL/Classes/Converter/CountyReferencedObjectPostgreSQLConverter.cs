using DiGi.GIS.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
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
    public abstract class CountyReferencedObjectPostgreSQLConverter<TCountyReferencedObject, TGISSerializableObject> : PostgreSQLConverter<TCountyReferencedObject>, IGISPostgreSQLConverter<TCountyReferencedObject> where TCountyReferencedObject : CountyReferencedObject<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public CountyReferencedObjectPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {

        }

        public abstract string TableName { get; }
        
        public async Task<TCountyReferencedObject?> GetItemByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], countyId, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        public async Task<List<TCountyReferencedObject>?> GetItemsByReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? references, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || references is null)
            {
                return null;
            }

            List<TCountyReferencedObject>? result = [];

            if (!references.Any())
            {
                return result;
            }

            string commandText = $@"
                SELECT id, county_id, reference, object, created_at
                FROM {TableName}
                WHERE reference = ANY(@references)
                  AND (@countyId IS NULL OR county_id = @countyId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            result = await ReadAsync(npgsqlCommand, cancellationToken);

            return result;
        }

        public async Task<List<TCountyReferencedObject>?> GetItemsByReferencesAsync(IEnumerable<string>? references, int? countyId, CancellationToken cancellationToken = default)
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

            return await GetItemsByReferencesAsync(npgsqlConnection, references, countyId, cancellationToken);
        }
        
        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<TCountyReferencedObject>? countyReferencedObjects)
        {
            if (countyReferencedObjects is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await npgsqlConnection.TableAsync_CountyReferencedObject(TableName);
            if (!succeded)
            {
                return null;
            }

            HashSet<long> result = [];
            if (!countyReferencedObjects.Any())
            {
                return result;
            }

            IEnumerable<IGrouping<int?, TCountyReferencedObject>> groupings = countyReferencedObjects.GroupBy(x => x.CountyId);

            foreach (IGrouping<int?, TCountyReferencedObject> grouping in groupings)
            {
                if (!grouping.Key.HasValue)
                {
                    continue;
                }

                await npgsqlConnection.TableAsync_CountyReferencedObject_Partition(TableName, grouping.Key.Value);

                string commandText = $@"
                    INSERT INTO {TableName} (county_id, reference, object)
                    VALUES (@county_id, @reference, @object)
                    ON CONFLICT (county_id, reference)
                    DO UPDATE SET
                        object = EXCLUDED.object,
                    RETURNING id;";

                foreach (TCountyReferencedObject? countyReferencedObject in grouping)
                {
                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                    npgsqlCommand.Parameters.AddWithValue("county_id", countyReferencedObject.CountyId!);
                    npgsqlCommand.Parameters.AddWithValue("reference", countyReferencedObject.Reference ?? (object)DBNull.Value);

                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                    {
                        Value = (object?)countyReferencedObject.Object?.ToJsonString() ?? DBNull.Value
                    });

                    var returnedId = await npgsqlCommand.ExecuteScalarAsync();
                    if (returnedId is long id)
                    {
                        result.Add(id);
                        countyReferencedObject.Id = id;
                    }
                }
            }

            return result;
        }

        protected abstract TCountyReferencedObject Create(long id, int? countyId, string? reference, JsonObject? @object, DateTime? createdAt);

        private TCountyReferencedObject Create(NpgsqlDataReader npgsqlDataReader)
        {
            return Create(
                npgsqlDataReader.GetInt64(0),
                npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetInt32(1),
                npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                JsonNode.Parse(npgsqlDataReader.GetString(3)) as JsonObject,
                npgsqlDataReader.IsDBNull(4) ? null : npgsqlDataReader.GetDateTime(4)
                );
        }

        private async Task<List<TCountyReferencedObject>?> ReadAsync(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync(npgsqlDataReader, cancellationToken);
        }

        private async Task<List<TCountyReferencedObject>> ReadAsync(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<TCountyReferencedObject> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create(npgsqlDataReader));
            }

            return result;
        }
    }
}