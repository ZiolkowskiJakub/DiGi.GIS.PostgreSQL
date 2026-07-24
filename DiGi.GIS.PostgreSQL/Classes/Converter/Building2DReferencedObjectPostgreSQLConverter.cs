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
    /// <summary>
    /// Provides a base implementation for a PostgreSQL converter specifically designed for building 2D referenced objects.
    /// </summary>
    /// <typeparam name="TBuilding2DReferencedObject">The type of the building 2D referenced object.</typeparam>
    /// <typeparam name="TUniqueObject">The type of the unique object used for identification, which must implement the <see cref="IUniqueObject"/> interface.</typeparam>
    public abstract class Building2DReferencedObjectPostgreSQLConverter<TBuilding2DReferencedObject, TUniqueObject> : ReferencedObjectPostgreSQLConverter<TBuilding2DReferencedObject, TUniqueObject> where TBuilding2DReferencedObject : Building2DReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DReferencedObjectPostgreSQLConverter{TBuilding2DReferencedObject, TUniqueObject}" /> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData" /> containing the connection settings required to establish a connection to the PostgreSQL database. This value can be null.</param>
        public Building2DReferencedObjectPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Asynchronously retrieves the total count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="countyId">The optional unique identifier of the county to filter the count; if null, the total count across all counties is retrieved.</param>
        /// <param name="cancellationToken">The cancellation token used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a long integer.</returns>
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

        /// <summary>
        /// Asynchronously retrieves an estimated count of records, optionally filtered by a specific county identifier and with the option to update table statistics before estimation.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the count.</param>
        /// <param name="analyze">A boolean value indicating whether an ANALYZE operation should be performed on the table to update statistics for a more accurate estimate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated record count as a long integer.</returns>
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

        /// <summary>
        /// Asynchronously gets the estimated row count for the specified county identifiers in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> to use for the query.</param>
        /// <param name="countyIds">A collection of integers representing the county identifiers to estimate counts for.</param>
        /// <param name="analyze">A boolean indicating whether to run a vacuum analyze operation before fetching the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated number of rows as a long, or -1 if an error occurs.</returns>
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

        /// <summary>
        /// Asynchronously retrieves an estimated row count, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="countyId">The optional integer identifier of the county to filter the estimate.</param>
        /// <param name="analyze">A boolean value indicating whether to run an analysis operation before fetching the count to ensure higher accuracy.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated row count as a <see cref="System.Int64"/>, or -1 if an error occurs.</returns>
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

        /// <summary>
        /// Asynchronously retrieves the estimated row count for the specified collection of county identifiers.
        /// </summary>
        /// <param name="countyIds">A collection of integers representing the unique identifiers of the counties to be counted.</param>
        /// <param name="analyze">A boolean value indicating whether to perform a database analysis operation before retrieving the estimate.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated total row count as a long, or -1 if an error occurs.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a building 2D referenced object by its unique identifier and an optional county identifier.
        /// </summary>
        /// <param name="id">The long integer unique identifier of the item to retrieve.</param>
        /// <param name="countyId">The optional integer identifier of the county associated with the item.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved <seeref name="TBuilding2DReferencedObject"/>, or null if no item with the specified identifier was found.</returns>
        public async Task<TBuilding2DReferencedObject?> GetItemByIdAsync(long id, int? countyId, CancellationToken cancellationToken = default)
        {
            return await GetItemsByIdsAsync([id], countyId, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <seeref name="TBuilding2DReferencedObject"/> using the specified reference and optional county identifier.
        /// </summary>
        /// <param name="reference">The string reference of the item to retrieve.</param>
        /// <param name="countyId">The optional integer identifier for the county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the matching <seeref name="TBuilding2DReferencedObject"/> if found; otherwise, null.</returns>
        public async Task<TBuilding2DReferencedObject?> GetItemByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], countyId, 1, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of items of type <typeparamref name="TBuilding2DReferencedObject"/> based on the specified identifiers and county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the PostgreSQL database.</param>
        /// <param name="ids">A collection of <see cref="System.Int64"/> identifiers for the items to retrieve.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the results.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{TBuilding2DReferencedObject}"/> of matching items, or null if the connection or identifiers are null.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a list of building 2D referenced objects based on the specified identifiers and an optional county identifier.
        /// </summary>
        /// <param name="ids">A collection of long identifiers of the items to retrieve.</param>
        /// <param name="countyId">The optional nullable integer identifier of the county used to filter the results.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{TBuilding2DReferencedObject}"/> of matching items, or null if no items are found or the provided identifiers are null.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a list of building 2D referenced objects based on the specified reference and optional filters.
        /// </summary>
        /// <param name="reference">The string reference used to identify the items.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the results.</param>
        /// <param name="limit">The optional maximum number of items to retrieve, specified as a long integer.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <typeparamref name="TBuilding2DReferencedObject"/> objects if matching items are found; otherwise, null.</returns>
        public async Task<List<TBuilding2DReferencedObject>?> GetItemsByReferenceAsync(string reference, int? countyId, long? limit = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetItemsByReferencesAsync([reference], countyId, limit, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of items that implement <typeparamref name="TBuilding2DReferencedObject" /> based on the provided references and optional filters.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> used to connect to the PostgreSQL database.</param>
        /// <param name="references">A collection of <see cref="System.String" /> representing the references of the items to be retrieved.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the results.</param>
        /// <param name="limit">The optional maximum number of items to retrieve as a <see cref="System.Int64" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{TBuilding2DReferencedObject}" /> of matching items, or null if the connection or references are null.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a list of items based on the provided references and county identifier.
        /// </summary>
        /// <param name="references">An optional collection of strings representing the unique references of the items to be retrieved.</param>
        /// <param name="countyId">An optional integer specifying the county identifier to filter the results.</param>
        /// <param name="limit">An optional long value that specifies the maximum number of items to return.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <typeparamref name="TBuilding2DReferencedObject"/> objects if matches are found; otherwise, null.</returns>
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

        /// <summary>
        /// Asynchronously updates the specified collection of building 2D referenced objects.
        /// </summary>
        /// <param name="building2DReferencedObjects">An <see cref="IEnumerable{TBuilding2DReferencedObject}"/> containing the referenced objects to be updated, or <c>null</c>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="HashSet{T}"/> of <see cref="long"/> identifiers for the objects that were updated, or <c>null</c> if no updates occurred.</returns>
        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<TBuilding2DReferencedObject>? building2DReferencedObjects, int commandTimeout = 30)
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
                npgsqlCommand.CommandTimeout = commandTimeout;

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

        /// <summary>
        /// Creates a new instance of a building referenced object using the specified identification and metadata.
        /// </summary>
        /// <param name="id">The unique <see cref="System.Int64"/> identifier for the record.</param>
        /// <param name="countyId">The optional <see cref="System.Int32"/> identifier for the county associated with the building.</param>
        /// <param name="uniqueId">The optional <see cref="System.String"/> representing a unique identification code.</param>
        /// <param name="reference">The optional <see cref="System.String"/> used as a reference for the record.</param>
        /// <param name="object">The optional <see cref="JsonObject"/> containing raw data for initialization.</param>
        /// <param name="createdAt">The optional <see cref="DateTime"/> indicating when the record was created.</param>
        /// <returns>A new instance of <seeref name="TBuilding2DReferencedObject"/>.</returns>
        protected abstract TBuilding2DReferencedObject Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt);

        /// <summary>
        /// Creates an instance of <seeref name="TBuilding2DReferencedObject"/> using the data provided by the <see cref="NpgsqlDataReader"/>.
        /// </summary>
        /// <param name="npgsqlDataReader">The <see cref="NpgsqlDataReader"/> containing the database record to be used for object creation.</param>
        /// <returns>A new instance of <seeref name="TBuilding2DReferencedObject"/> populated with data from the reader.</returns>
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