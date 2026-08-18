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
    /// <para>Rows are addressed at two levels - <c>(county_id, reference)</c> for everything held for a building, <c>unique_id</c> for one stored object within that set - and a building may hold several rows. <see cref="Classes.Building2DReferencedObject{TUniqueObject}"/> describes the convention in full; it decides which of the read and remove methods below is the right one for a given job.</para>
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
        /// <para>A reference can hold several rows, one per stored object, so this returns the most recently stored of them. Use <see cref="GetItemsByReferenceAsync"/> when the whole set is wanted.</para>
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

            // The type has to be stated explicitly: an untyped DBNull leaves the server unable to resolve
            // the parameter type for the '@countyId IS NULL' occurrence, which fails the whole query.
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = (object?)countyId ?? DBNull.Value });

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

            // Base query with reference filter and optional countyId filter.
            // The ordering is not cosmetic: a reference can hold several rows - one per stored object -
            // so without it a limited read returns whichever row the plan happened to reach first, and
            // the answer changes with a vacuum or the heap ordering. created_at alone does not settle it
            // either, because now() is transaction start and a bulk write stamps every row of the batch
            // identically; id is what actually decides between them.
            string commandText = $@"
                SELECT id, county_id, unique_id, reference, object, created_at
                FROM {TableName}
                WHERE reference = ANY(@references)
                  AND (@countyId IS NULL OR county_id = @countyId)
                ORDER BY created_at DESC, id DESC";

            // Append LIMIT if provided
            if (limit.HasValue)
            {
                commandText += " LIMIT @limit";
            }

            commandText += ";";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Adding parameters with explicit handling of nulls for PostgreSQL
            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());

            // The type has to be stated explicitly: an untyped DBNull leaves the server unable to resolve
            // the parameter type for the '@countyId IS NULL' occurrence, which fails the whole query.
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = (object?)countyId ?? DBNull.Value });

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
        /// <para>The upsert targets <c>(county_id, unique_id)</c>, which is the identity of the stored <b>object</b>, not of the building. An object read back from the database keeps its identifier and so replaces its own row; an object built fresh carries a new one and is <b>added</b> alongside whatever the building already holds. That is the intended behaviour - see <see cref="Classes.Building2DReferencedObject{TUniqueObject}"/> - so a caller that means to replace a building's data has to remove it first, with <see cref="RemoveAsync"/> for the whole set or <c>RemoveByUniqueIdsAsync</c> for one object.</para>
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
        /// Asynchronously retrieves every reference held under a single county row.
        /// <para>The whole row is not read - only the reference column - so this stays usable on a county part holding tens of thousands of rows, which reading the objects would not be.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row to read.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the distinct references held under the county row, or null when the connection could not be created.</returns>
        public async Task<HashSet<string>?> GetReferencesAsync(int countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                SELECT DISTINCT reference
                FROM {TableName}
                WHERE county_id = @countyId
                  AND reference IS NOT NULL;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            HashSet<string> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(npgsqlDataReader.GetString(0));
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves the unique identifiers of every row held for the given references under a single county row.
        /// <para>Only the identifier column is read. It is the counterpart of <c>GetItemsByReferencesAsync</c> for a caller that wants to address the rows rather than the objects inside them - reading the objects to reach one column would move every stored object across the connection, which on a table of building models is gigabytes fetched for a value a few characters long.</para>
        /// <para>This is the read half of replacing what a building holds. Take the identifiers first, write the new rows, then delete the identifiers taken here with <c>RemoveByUniqueIdsAsync(uniqueIds, countyId)</c>. Ordered that way round a run interrupted between the write and the delete leaves the building holding both its old and its new object, which is recoverable, rather than holding neither.</para>
        /// </summary>
        /// <param name="references">The references whose rows are to be identified.</param>
        /// <param name="countyId">The identifier of the county row holding them.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unique identifiers held for the references, or null when no references were given or the connection could not be created.</returns>
        public async Task<HashSet<string>?> GetUniqueIdsByReferencesAsync(IEnumerable<string>? references, int countyId, CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            string[] references_Array = [.. references];
            if (references_Array.Length == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // ANY keeps this one statement rather than one per reference, and county_id leads the filter
            // because it is the partition key and the leading column of the index reads go through.
            string commandText = $@"
                SELECT unique_id
                FROM {TableName}
                WHERE county_id = @countyId
                  AND reference = ANY(@references);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);
            npgsqlCommand.Parameters.AddWithValue("references", references_Array);

            HashSet<string> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(npgsqlDataReader.GetString(0));
            }

            return result;
        }

        /// <summary>
        /// Deletes the rows holding the given references under a single county row.
        /// <para>A reference is unique only per <c>county_id</c>: the same building is held once per county row it was imported under, so a delete has to name the row as well as the reference. Deleting by reference alone would take the building out of every part of the county.</para>
        /// <para>It removes data and has no undo - read <c>AI Guidelines/Coding - GIS Administrative Data.md</c> before calling it.</para>
        /// </summary>
        /// <param name="references">The references to delete.</param>
        /// <param name="countyId">The identifier of the county row to delete them from.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers of the rows actually deleted, which is how many of the references were really there.</returns>
        public async Task<HashSet<long>?> RemoveAsync(IEnumerable<string>? references, int countyId, CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            string[] references_Array = [.. references];
            if (references_Array.Length == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // ANY keeps this one statement rather than one per reference, and RETURNING reports what was
            // really removed - the count is the only evidence that the delete matched what was intended.
            string commandText = $@"
                DELETE FROM {TableName}
                WHERE county_id = @countyId
                  AND reference = ANY(@references)
                RETURNING id;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);
            npgsqlCommand.Parameters.AddWithValue("references", references_Array);

            HashSet<long> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(npgsqlDataReader.GetInt64(0));
            }

            return result;
        }

        /// <summary>
        /// Deletes single stored objects from the set held for one building, naming each of them by its own unique identifier.
        /// <para>A building can hold several rows in this table - one per stored object - so <see cref="RemoveAsync"/>, which takes out everything held for a reference, is too blunt to correct one of them. This is the delete half of updating a single object: read the set with <see cref="GetItemsByReferenceAsync"/>, pick the one to change, remove it here, then write the replacement.</para>
        /// <para><c>county_id</c> and <c>unique_id</c> already identify the row on their own - the table declares <c>UNIQUE (county_id, unique_id)</c>. The reference is required as well and is matched as a guard, so a unique identifier belonging to a different building cannot silently take out that building's object.</para>
        /// <para>It removes data and has no undo - read <c>AI Guidelines/Coding - GIS Administrative Data.md</c> before calling it.</para>
        /// </summary>
        /// <param name="uniqueIds">The unique identifiers of the stored objects to delete.</param>
        /// <param name="reference">The reference of the building the objects belong to.</param>
        /// <param name="countyId">The identifier of the county row holding them.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers of the rows actually deleted, which is how many of the unique identifiers were really there.</returns>
        public async Task<HashSet<long>?> RemoveByUniqueIdsAsync(IEnumerable<string>? uniqueIds, string reference, int countyId, CancellationToken cancellationToken = default)
        {
            if (uniqueIds is null || string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            string[] uniqueIds_Array = [.. uniqueIds];
            if (uniqueIds_Array.Length == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // ANY keeps this one statement rather than one per identifier, and RETURNING reports what was
            // really removed - the count is the only evidence that the delete matched what was intended.
            string commandText = $@"
                DELETE FROM {TableName}
                WHERE county_id = @countyId
                  AND reference = @reference
                  AND unique_id = ANY(@uniqueIds)
                RETURNING id;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);
            npgsqlCommand.Parameters.AddWithValue("reference", reference);
            npgsqlCommand.Parameters.AddWithValue("uniqueIds", uniqueIds_Array);

            HashSet<long> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(npgsqlDataReader.GetInt64(0));
            }

            return result;
        }

        /// <summary>
        /// Deletes single stored objects from one county row, naming each of them by its own unique identifier without stating which building it belongs to.
        /// <para>The unguarded counterpart of <c>RemoveByUniqueIdsAsync(uniqueIds, reference, countyId)</c>. That overload matches the reference as well, so a unique identifier belonging to a different building cannot silently take out that building's object; it is the right one whenever the identifiers came from anywhere other than the rows being deleted, and it costs one statement per building.</para>
        /// <para>This one is for the case where the identifiers were read back from the very rows being replaced, with <see cref="GetUniqueIdsByReferencesAsync"/>. They already name exactly those rows, so the guard would only re-check what that read established, and a whole batch of buildings goes out in one statement instead of one each.</para>
        /// <para>It removes data and has no undo - read <c>AI Guidelines/Coding - GIS Administrative Data.md</c> before calling it.</para>
        /// </summary>
        /// <param name="uniqueIds">The unique identifiers of the stored objects to delete.</param>
        /// <param name="countyId">The identifier of the county row holding them.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers of the rows actually deleted, which is how many of the unique identifiers were really there.</returns>
        public async Task<HashSet<long>?> RemoveByUniqueIdsAsync(IEnumerable<string>? uniqueIds, int countyId, CancellationToken cancellationToken = default)
        {
            if (uniqueIds is null)
            {
                return null;
            }

            string[] uniqueIds_Array = [.. uniqueIds];
            if (uniqueIds_Array.Length == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // ANY keeps this one statement rather than one per identifier, and RETURNING reports what was
            // really removed - the count is the only evidence that the delete matched what was intended.
            string commandText = $@"
                DELETE FROM {TableName}
                WHERE county_id = @countyId
                  AND unique_id = ANY(@uniqueIds)
                RETURNING id;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);
            npgsqlCommand.Parameters.AddWithValue("uniqueIds", uniqueIds_Array);

            HashSet<long> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(npgsqlDataReader.GetInt64(0));
            }

            return result;
        }

        // TODO [BuildingModelRowIdentity]: everything from here to the end of this class is temporary and
        // exists only for the one-off unique_id migration of issue
        // ZiolkowskiJakub/DiGi.GIS.PostgreSQL#5. Delete it once every deployed database has run
        // PostgreSQLBuildingModelUniqueIdMigrationTask and it reports zero pending rows nationally.
        // Nothing above this line is temporary.

        /// <summary>
        /// [TEMPORARY] Gets the common table expression classifying every row of one county row against the convention that <c>unique_id</c> carries the identifier of the object the row stores.
        /// <para>It is a prefix rather than a statement: a caller appends the <c>SELECT</c> or <c>UPDATE</c> consuming <c>row_classified</c>, aliases it <c>r</c>, and supplies a <c>countyId</c> parameter. It exists so the query counting what a migration would do and the statement doing it classify a row identically - written out twice they would drift, and the count would stop being evidence about the update.</para>
        /// <para><c>unique_id_target</c> is the value the stored object's own <c>UniqueId</c> produces. Every object held in these tables derives from <see cref="DiGi.Core.Classes.GuidObject"/>, whose <c>UniqueId</c> is its guid formatted <c>N</c> - 32 hexadecimal characters, lower case, no separators - while the serialized form of that same guid is the ordinary dashed one. Stripping the dashes and lower-casing is therefore exactly the transformation between the two, and neither half of it may be dropped.</para>
        /// <para>That assumption is what limits where this is meaningful. It holds for <c>building_model</c>, <c>occupancy_data_building_2d</c> and <c>year_built_data</c>, whose stored objects all derive from <see cref="DiGi.Core.Classes.GuidObject"/>; it does not automatically hold for a sibling table added later.</para>
        /// <para>Temporary - see the note above.</para>
        /// </summary>
        protected string UniqueIdClassificationCommandText => $@"
                WITH row_classified AS (
                    SELECT t.id AS id,
                           t.unique_id AS unique_id,
                           lower(replace(t.object->>'Guid', '-', '')) AS unique_id_target,
                           count(*) OVER (PARTITION BY lower(replace(t.object->>'Guid', '-', ''))) AS target_count,
                           EXISTS (SELECT 1
                                   FROM {TableName} AS t_Held
                                   WHERE t_Held.county_id = @countyId
                                     AND t_Held.id <> t.id
                                     AND t_Held.unique_id = lower(replace(t.object->>'Guid', '-', ''))) AS target_held
                    FROM {TableName} AS t
                    WHERE t.county_id = @countyId
                )";

        /// <summary>
        /// [TEMPORARY] The condition selecting the rows whose stored object carries no identifier to migrate.
        /// <para>Expected to match nothing. It is counted rather than assumed away because a row it matches is one the migration can do nothing with, and that has to be reported instead of silently skipped.</para>
        /// <para>Temporary - see the note above <see cref="UniqueIdClassificationCommandText"/>.</para>
        /// </summary>
        protected const string UniqueIdMissingCondition = "(r.unique_id_target IS NULL OR r.unique_id_target = '')";

        /// <summary>
        /// [TEMPORARY] The condition selecting the rows already carrying the identifier of the object they store, which need nothing done to them.
        /// <para>Every row of a table that was written correctly from the start matches this, which is what makes the count usable as a check against a table needing no migration at all.</para>
        /// <para>Temporary - see the note above <see cref="UniqueIdClassificationCommandText"/>.</para>
        /// </summary>
        protected const string UniqueIdDoneCondition = "(r.unique_id_target IS NOT NULL AND r.unique_id_target <> '' AND r.unique_id = r.unique_id_target)";

        /// <summary>
        /// [TEMPORARY] The condition selecting the rows left alone because the identifier they would take is already held within the same county row.
        /// <para>Two rows carrying the same stored identifier cannot both be migrated: the table constrains <c>UNIQUE (county_id, unique_id)</c>, which PostgreSQL checks as each row is written, so one collision would roll back the update of the whole county row. Excluding them keeps the statement unable to abort, and turns a duplicate into a number to review rather than a failed run.</para>
        /// <para>Temporary - see the note above <see cref="UniqueIdClassificationCommandText"/>.</para>
        /// </summary>
        protected const string UniqueIdBlockedCondition = "(r.unique_id_target IS NOT NULL AND r.unique_id_target <> '' AND r.unique_id <> r.unique_id_target AND (r.target_count > 1 OR r.target_held))";

        /// <summary>
        /// [TEMPORARY] The condition selecting the rows the migration moves, being every row not already done, not missing an identifier and not blocked by one.
        /// <para>Used unchanged as the count of what a migration would do and as the <c>WHERE</c> of the statement doing it, so the two can never disagree.</para>
        /// <para>Temporary - see the note above <see cref="UniqueIdClassificationCommandText"/>.</para>
        /// </summary>
        protected const string UniqueIdPendingCondition = "(r.unique_id_target IS NOT NULL AND r.unique_id_target <> '' AND r.unique_id <> r.unique_id_target AND r.target_count = 1 AND NOT r.target_held)";

        /// <summary>
        /// [TEMPORARY] Asynchronously counts how the rows of one county row stand against the convention that <c>unique_id</c> carries the identifier of the object the row stores.
        /// <para>Counts and writes nothing, so it is safe against a deployed database. It is the half to run first: the numbers it reports are what the migration should be reviewed against, and comparing them with what the migration returns is the only evidence the update matched what was counted.</para>
        /// <para>It is on this class rather than on the one table that needs migrating so it can be run through <see cref="Building2DOccupancyDataPostgreSQLConverter"/> and <see cref="YearBuiltDataPostgreSQLConverter"/> as well. Those two tables have always been written the way <c>building_model</c> is being changed to be, so every one of their rows must come back done - which is how the expression computing the target identifier is checked against real stored data before it is pointed at the table it is meant to repair.</para>
        /// <para>Temporary - see the note above <see cref="UniqueIdClassificationCommandText"/>.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row to count in.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the counts for the county row, or null when the connection could not be created.</returns>
        public async Task<UniqueIdMigrationResult?> GetUniqueIdMigrationResultAsync(int countyId, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // The four classes are mutually exclusive and cover every row, so the four counts add up to the
            // total. A county row holding no rows at all still answers, with a row of zeros.
            string commandText = $@"
                {UniqueIdClassificationCommandText}
                SELECT count(*) AS total,
                       count(*) FILTER (WHERE {UniqueIdDoneCondition}) AS done,
                       count(*) FILTER (WHERE {UniqueIdPendingCondition}) AS pending,
                       count(*) FILTER (WHERE {UniqueIdBlockedCondition}) AS blocked,
                       count(*) FILTER (WHERE {UniqueIdMissingCondition}) AS missing
                FROM row_classified AS r;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            if (!await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                return null;
            }

            return new UniqueIdMigrationResult(
                countyId,
                npgsqlDataReader.GetInt64(0),
                npgsqlDataReader.GetInt64(1),
                npgsqlDataReader.GetInt64(2),
                npgsqlDataReader.GetInt64(3),
                npgsqlDataReader.GetInt64(4)
                );
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