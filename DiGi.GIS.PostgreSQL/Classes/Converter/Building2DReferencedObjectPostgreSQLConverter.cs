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
    public abstract class Building2DReferencedObjectPostgreSQLConverter<TBuilding2DReferencedObject, TUniqueObject> : ReferencedObjectPostgreSQLConverter<TBuilding2DReferencedObject, TUniqueObject> where TBuilding2DReferencedObject : Building2DReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        public Building2DReferencedObjectPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public async Task<long> GetCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string tableName = TableName;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, tableName, cancellationToken);
        }

        public async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string tableName = TableName;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, tableName, analyze, cancellationToken);
        }

        public async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int> countyIds, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            long result = 0;
            foreach (int countyId in countyIds)
            {
                string tableName = string.Format("{0}_{1}", TableName, countyId);
                result += await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, tableName, analyze, cancellationToken);
            }

            return result;
        }

        public async Task<long> GetEstimatedCountAsync(int? countyId, bool analyze = false, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetEstimatedCountAsync(npgsqlConnection, countyId, analyze, cancellationToken);
        }

        public async Task<long> GetEstimatedCountAsync(IEnumerable<int> countyIds, bool analyze = false, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetEstimatedCountAsync(npgsqlConnection, countyIds, analyze, cancellationToken);
        }

        public async Task<TBuilding2DReferencedObject?> GetItemByIdAsync(long id, int? countyId, CancellationToken cancellationToken = default)
        {
            return await GetItemsByIdsAsync([id], countyId, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        public async Task<TBuilding2DReferencedObject?> GetItemByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], countyId, 1, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        public async Task<List<TBuilding2DReferencedObject>?> GetItemsByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<long>? ids, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || ids is null)
            {
                return null;
            }

            List<TBuilding2DReferencedObject>? result = [];

            if (!ids.Any())
            {
                return result;
            }

            string commandText = $@"
                SELECT id, county_id, unique_id, reference, object, created_at
                FROM {TableName}
                WHERE id = ANY(@ids)
                  AND (@countyId IS NULL OR county_id = @countyId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("ids", ids.ToArray());
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            result = await ReadAsync(npgsqlCommand, cancellationToken);

            return result;
        }

        public async Task<List<TBuilding2DReferencedObject>?> GetItemsByIdsAsync(IEnumerable<long>? ids, int? countyId, CancellationToken cancellationToken = default)
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

            return await GetItemsByIdsAsync(npgsqlConnection, ids, countyId, cancellationToken);
        }

        public async Task<List<TBuilding2DReferencedObject>?> GetItemsByReferenceAsync(string reference, int? countyId, long? limit = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], countyId, limit, cancellationToken);
        }

        public async Task<List<TBuilding2DReferencedObject>?> GetItemsByReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? references, int? countyId, long? limit = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || references is null)
            {
                return null;
            }

            if (!references.Any())
            {
                return [];
            }

            // Base query with reference filter and optional countyId filter
            string commandText = $@"
                SELECT id, county_id, unique_id, reference, object, created_at
                FROM {TableName}
                WHERE reference = ANY(@references)
                  AND (@countyId IS NULL OR county_id = @countyId)";

            // Append LIMIT if provided
            if (limit.HasValue)
            {
                commandText += " LIMIT @limit";
            }

            commandText += ";";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Adding parameters with explicit handling of nulls for PostgreSQL
            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());
            npgsqlCommand.Parameters.AddWithValue("countyId", (object?)countyId ?? DBNull.Value);

            if (limit.HasValue)
            {
                npgsqlCommand.Parameters.AddWithValue("limit", limit.Value);
            }

            return await ReadAsync(npgsqlCommand, cancellationToken);
        }

        public async Task<List<TBuilding2DReferencedObject>?> GetItemsByReferencesAsync(IEnumerable<string>? references, int? countyId, long? limit = null, CancellationToken cancellationToken = default)
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

            return await GetItemsByReferencesAsync(npgsqlConnection, references, countyId, limit, cancellationToken);
        }

        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<TBuilding2DReferencedObject>? building2DReferencedObjects)
        {
            if (building2DReferencedObjects is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await npgsqlConnection.TableAsync_Building2DReferencedObject(TableName);
            if (!succeded)
            {
                return null;
            }

            HashSet<long> result = [];
            if (!building2DReferencedObjects.Any())
            {
                return result;
            }

            IEnumerable<IGrouping<int?, TBuilding2DReferencedObject>> groupings = building2DReferencedObjects.GroupBy(x => x.CountyId);

            foreach (IGrouping<int?, TBuilding2DReferencedObject> grouping in groupings)
            {
                if (!grouping.Key.HasValue)
                {
                    continue;
                }

                await npgsqlConnection.TableAsync_Building2DReferencedObject_Partition(TableName, grouping.Key.Value);

                string commandText = $@"
                    INSERT INTO {TableName} (county_id, unique_id, reference, object)
                    VALUES (@county_id, @unique_id, @reference, @object)
                    ON CONFLICT (county_id, unique_id)
                    DO UPDATE SET
                        object = EXCLUDED.object,
                        reference = EXCLUDED.reference
                    RETURNING id;";

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                // Define parameters once
                NpgsqlParameter npgsqlParameter_UniqueId = npgsqlCommand.Parameters.Add("unique_id", NpgsqlDbType.Text);
                NpgsqlParameter npgsqlParameter_CountyId = npgsqlCommand.Parameters.Add("county_id", NpgsqlDbType.Integer);
                NpgsqlParameter npgsqlParameter_Reference = npgsqlCommand.Parameters.Add("reference", NpgsqlDbType.Text);
                NpgsqlParameter npgsqlParameter_Object = npgsqlCommand.Parameters.Add("object", NpgsqlDbType.Jsonb);

                foreach (TBuilding2DReferencedObject? countyReferencedObject in grouping)
                {
                    npgsqlParameter_UniqueId.Value = countyReferencedObject.UniqueId;
                    npgsqlParameter_CountyId.Value = countyReferencedObject.CountyId;
                    npgsqlParameter_Reference.Value = countyReferencedObject.Reference ?? (object)DBNull.Value;
                    npgsqlParameter_Object.Value = (object?)countyReferencedObject.Object?.ToJsonString() ?? DBNull.Value;

                    object? returnedId = await npgsqlCommand.ExecuteScalarAsync();
                    if (returnedId is long id)
                    {
                        result.Add(id);
                        countyReferencedObject.Id = id;
                    }
                }
            }

            return result;
        }

        protected abstract TBuilding2DReferencedObject Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt);

        protected override TBuilding2DReferencedObject Create(NpgsqlDataReader npgsqlDataReader)
        {
            return Create(
                npgsqlDataReader.GetInt64(0),
                npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetInt32(1),
                npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                npgsqlDataReader.IsDBNull(3) ? null : npgsqlDataReader.GetString(3),
                JsonNode.Parse(npgsqlDataReader.GetString(4)) as JsonObject,
                npgsqlDataReader.IsDBNull(5) ? null : npgsqlDataReader.GetDateTime(5)
                );
        }
    }
}