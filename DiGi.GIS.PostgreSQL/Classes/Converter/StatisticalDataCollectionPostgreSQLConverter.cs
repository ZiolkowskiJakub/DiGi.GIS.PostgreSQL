using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Interfaces;
using DiGi.GIS.PostgreSQL.Constants;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides functionality to convert and manage <see cref="StatisticalDataCollection"/> entities within a PostgreSQL database,
    /// implementing the <see cref="IGISPostgreSQLConverter"/> interface.
    /// </summary>
    public class StatisticalDataCollectionPostgreSQLConverter : PostgreSQLConverter<StatisticalDataCollection>, IGISPostgreSQLConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticalDataCollectionPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData"/> containing database connection settings.</param>
        public StatisticalDataCollectionPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the database table associated with statistical data collections.
        /// </summary>
        public static string TableName => Constants.TableName.StatisticalDataCollection;

        /// <summary>
        /// Asynchronously creates the statistical data collection table in the database if it does not already exist.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if table was created successfully; otherwise, false.</returns>
        public static async Task<bool> CreateTableAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            return await Create.TableAsync_StatisticalDataCollection(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously creates the statistical data collection table in the database if it does not already exist, managing the connection.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if table was created successfully; otherwise, false.</returns>
        public async Task<bool> CreateTableAsync(int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await CreateTableAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously clears all records from the statistical data collection table.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the table was cleared successfully; otherwise, false.</returns>
        public static async Task<bool> ClearAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously clears all records from the statistical data collection table, managing the connection.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the table was cleared successfully; otherwise, false.</returns>
        public async Task<bool> ClearAsync(int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await ClearAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously inserts or updates a collection of <see cref="StatisticalDataCollection"/> entities in the database in batches,
        /// merging any existing records with the incoming statistical data series.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="statisticalDataCollections">The collection of statistical data collections to insert or update.</param>
        /// <param name="batchSize">The maximum number of collections per batch command.</param>
        /// <param name="commandTimeout">The timeout in seconds for the command execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of identifiers successfully inserted or updated.</returns>
        public static async Task<List<string>> InsertAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<StatisticalDataCollection>? statisticalDataCollections, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || statisticalDataCollections is null)
            {
                return [];
            }

            Dictionary<string, StatisticalDataCollection> mergedIncoming = [];
            foreach (StatisticalDataCollection incoming in statisticalDataCollections)
            {
                if (incoming is null || string.IsNullOrWhiteSpace(incoming.Code))
                {
                    continue;
                }

                if (!mergedIncoming.TryGetValue(incoming.Code, out StatisticalDataCollection? target))
                {
                    mergedIncoming[incoming.Code] = incoming;
                }
                else if (!ReferenceEquals(target, incoming))
                {
                    IEnumerable<IStatisticalData> incomingDatas = incoming.GetStatisticalDatas<IStatisticalData>();
                    foreach (IStatisticalData incomingData in incomingDatas)
                    {
                        target.Add(incomingData);
                    }
                }
            }

            List<StatisticalDataCollection> collectionList = [.. mergedIncoming.Values];
            if (collectionList.Count == 0)
            {
                return [];
            }

            bool tableCreated = await CreateTableAsync(npgsqlConnection, commandTimeout, cancellationToken);
            if (!tableCreated)
            {
                return [];
            }

            List<string> insertedIds = [];

            for (int i = 0; i < collectionList.Count; i += batchSize)
            {
                cancellationToken.ThrowIfCancellationRequested();

                List<StatisticalDataCollection> chunk = collectionList.Skip(i).Take(batchSize).ToList();
                List<string> chunkIds = [.. chunk.Select(c => c.Code!).Distinct()];

                Dictionary<string, StatisticalDataCollection>? existingDictionary = await GetStatisticalDataCollectionDictionaryByIdsAsync(npgsqlConnection, chunkIds, batchSize, commandTimeout, cancellationToken);

                List<StatisticalDataCollection> collectionsToWrite = [];
                foreach (StatisticalDataCollection incoming in chunk)
                {
                    if (string.IsNullOrWhiteSpace(incoming.Code))
                    {
                        continue;
                    }

                    if (existingDictionary is not null && existingDictionary.TryGetValue(incoming.Code, out StatisticalDataCollection? existing) && existing is not null)
                    {
                        IEnumerable<IStatisticalData> incomingDatas = incoming.GetStatisticalDatas<IStatisticalData>();
                        foreach (IStatisticalData incomingData in incomingDatas)
                        {
                            existing.Add(incomingData);
                        }

                        collectionsToWrite.Add(existing);
                    }
                    else
                    {
                        collectionsToWrite.Add(incoming);
                    }
                }

                await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);
                npgsqlBatch.Timeout = commandTimeout;

                foreach (StatisticalDataCollection collection in collectionsToWrite)
                {
                    NpgsqlBatchCommand batchCommand = new($@"
                        INSERT INTO {TableName} (id, object)
                        VALUES (@id, @object)
                        ON CONFLICT (id)
                        DO UPDATE SET
                            object = EXCLUDED.object
                        RETURNING id;");

                    batchCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Text) { Value = collection.Code ?? string.Empty });
                    batchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                    {
                        Value = (object?)collection.ToJsonObject()?.ToJsonString() ?? DBNull.Value
                    });

                    npgsqlBatch.BatchCommands.Add(batchCommand);
                }

                await using NpgsqlDataReader reader = await npgsqlBatch.ExecuteReaderAsync(cancellationToken);
                do
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        insertedIds.Add(reader.GetString(0));
                    }
                }
                while (await reader.NextResultAsync(cancellationToken));
            }

            return insertedIds;
        }

        /// <summary>
        /// Asynchronously inserts or updates a collection of <see cref="StatisticalDataCollection"/> entities in the database, managing the connection.
        /// </summary>
        /// <param name="statisticalDataCollections">The collection of statistical data collections to insert or update.</param>
        /// <param name="batchSize">The maximum number of collections per batch command.</param>
        /// <param name="commandTimeout">The timeout in seconds for the command execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of identifiers successfully inserted or updated.</returns>
        public async Task<List<string>> InsertAsync(IEnumerable<StatisticalDataCollection>? statisticalDataCollections, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await InsertAsync(npgsqlConnection, statisticalDataCollections, batchSize, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="StatisticalDataCollection"/> by its territorial unit code or identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="id">The territorial unit identifier or code.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="StatisticalDataCollection"/> if found; otherwise, null.</returns>
        public static async Task<StatisticalDataCollection?> GetStatisticalDataCollectionByIdAsync(NpgsqlConnection? npgsqlConnection, string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            string commandText = $@"
                SELECT id, object
                FROM {TableName}
                WHERE id = @id
                LIMIT 1;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Text) { Value = id });

            List<StatisticalDataCollection>? collections = await ReadAsync_StatisticalDataCollection(npgsqlCommand, cancellationToken);
            return collections?.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="StatisticalDataCollection"/> by its territorial unit code or identifier, managing the connection.
        /// </summary>
        /// <param name="id">The territorial unit identifier or code.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="StatisticalDataCollection"/> if found; otherwise, null.</returns>
        public async Task<StatisticalDataCollection?> GetStatisticalDataCollectionByIdAsync(string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalDataCollectionByIdAsync(npgsqlConnection, id, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves statistical data collections matching a collection of unit identifiers in batched queries.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="ids">The collection of unit identifiers to retrieve.</param>
        /// <param name="batchSize">The maximum number of identifiers to query per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching <see cref="StatisticalDataCollection"/> entities, or null if connection is null.</returns>
        public static async Task<List<StatisticalDataCollection>?> GetStatisticalDataCollectionsByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? ids, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || ids is null)
            {
                return null;
            }

            List<string> idList = [.. ids.Where(id => !string.IsNullOrWhiteSpace(id))];
            if (idList.Count == 0)
            {
                return [];
            }

            List<StatisticalDataCollection> result = [];

            for (int i = 0; i < idList.Count; i += batchSize)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string[] idChunk = idList.Skip(i).Take(batchSize).ToArray();

                string commandText = $@"
                    SELECT id, object
                    FROM {TableName}
                    WHERE id = ANY(@ids)
                    ORDER BY id ASC;";

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.CommandTimeout = commandTimeout;
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("ids", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = idChunk });

                List<StatisticalDataCollection>? chunkResult = await ReadAsync_StatisticalDataCollection(npgsqlCommand, cancellationToken);
                if (chunkResult is not null)
                {
                    result.AddRange(chunkResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves statistical data collections matching a collection of unit identifiers, managing the connection.
        /// </summary>
        /// <param name="ids">The collection of unit identifiers to retrieve.</param>
        /// <param name="batchSize">The maximum number of identifiers to query per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching <see cref="StatisticalDataCollection"/> entities, or null if connection cannot be established.</returns>
        public async Task<List<StatisticalDataCollection>?> GetStatisticalDataCollectionsByIdsAsync(IEnumerable<string>? ids, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalDataCollectionsByIdsAsync(npgsqlConnection, ids, batchSize, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a dictionary of statistical data collections mapped by unit identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="ids">The collection of unit identifiers to retrieve.</param>
        /// <param name="batchSize">The maximum number of identifiers to query per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A dictionary mapping unit identifier to <see cref="StatisticalDataCollection"/>, or null if connection is null.</returns>
        public static async Task<Dictionary<string, StatisticalDataCollection>?> GetStatisticalDataCollectionDictionaryByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? ids, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || ids is null)
            {
                return null;
            }

            List<StatisticalDataCollection>? collections = await GetStatisticalDataCollectionsByIdsAsync(npgsqlConnection, ids, batchSize, commandTimeout, cancellationToken);
            if (collections is null)
            {
                return null;
            }

            Dictionary<string, StatisticalDataCollection> result = [];
            foreach (StatisticalDataCollection collection in collections)
            {
                if (collection?.Code is string code && !string.IsNullOrWhiteSpace(code))
                {
                    result[code] = collection;
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a dictionary of statistical data collections mapped by unit identifier, managing the connection.
        /// </summary>
        /// <param name="ids">The collection of unit identifiers to retrieve.</param>
        /// <param name="batchSize">The maximum number of identifiers to query per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A dictionary mapping unit identifier to <see cref="StatisticalDataCollection"/>, or null if connection cannot be established.</returns>
        public async Task<Dictionary<string, StatisticalDataCollection>?> GetStatisticalDataCollectionDictionaryByIdsAsync(IEnumerable<string>? ids, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalDataCollectionDictionaryByIdsAsync(npgsqlConnection, ids, batchSize, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the matching <see cref="StatisticalDataCollection"/> for the specified <see cref="StatisticalUnit"/>.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="statisticalUnit">The statistical unit to match.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching <see cref="StatisticalDataCollection"/> if found; otherwise, null.</returns>
        public static async Task<StatisticalDataCollection?> GetStatisticalDataCollectionAsync(NpgsqlConnection? npgsqlConnection, StatisticalUnit? statisticalUnit, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || statisticalUnit is null || string.IsNullOrWhiteSpace(statisticalUnit.Code))
            {
                return null;
            }

            return await GetStatisticalDataCollectionByIdAsync(npgsqlConnection, statisticalUnit.Code, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the matching <see cref="StatisticalDataCollection"/> for the specified <see cref="StatisticalUnit"/>, managing the connection.
        /// </summary>
        /// <param name="statisticalUnit">The statistical unit to match.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching <see cref="StatisticalDataCollection"/> if found; otherwise, null.</returns>
        public async Task<StatisticalDataCollection?> GetStatisticalDataCollectionAsync(StatisticalUnit? statisticalUnit, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (statisticalUnit is null || string.IsNullOrWhiteSpace(statisticalUnit.Code))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalDataCollectionAsync(npgsqlConnection, statisticalUnit, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a dictionary mapping <see cref="StatisticalUnit"/> entities to their matching <see cref="StatisticalDataCollection"/> records.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="statisticalUnits">The collection of statistical units to query.</param>
        /// <param name="batchSize">The maximum number of units per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A dictionary mapping each <see cref="StatisticalUnit"/> to its matching <see cref="StatisticalDataCollection"/>, or null if connection is null.</returns>
        public static async Task<Dictionary<StatisticalUnit, StatisticalDataCollection>?> GetStatisticalDataCollectionDictionaryAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<StatisticalUnit>? statisticalUnits, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || statisticalUnits is null)
            {
                return null;
            }

            Dictionary<string, List<StatisticalUnit>> unitsByCode = [];
            foreach (StatisticalUnit unit in statisticalUnits)
            {
                if (unit?.Code is string code && !string.IsNullOrWhiteSpace(code))
                {
                    if (!unitsByCode.TryGetValue(code, out List<StatisticalUnit>? unitList))
                    {
                        unitList = [];
                        unitsByCode[code] = unitList;
                    }

                    unitList.Add(unit);
                }
            }

            if (unitsByCode.Count == 0)
            {
                return [];
            }

            Dictionary<string, StatisticalDataCollection>? collectionsByCode = await GetStatisticalDataCollectionDictionaryByIdsAsync(npgsqlConnection, unitsByCode.Keys, batchSize, commandTimeout, cancellationToken);
            if (collectionsByCode is null)
            {
                return null;
            }

            Dictionary<StatisticalUnit, StatisticalDataCollection> result = [];
            foreach (KeyValuePair<string, List<StatisticalUnit>> pair in unitsByCode)
            {
                if (collectionsByCode.TryGetValue(pair.Key, out StatisticalDataCollection? collection) && collection is not null)
                {
                    foreach (StatisticalUnit unit in pair.Value)
                    {
                        result[unit] = collection;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a dictionary mapping <see cref="StatisticalUnit"/> entities to their matching <see cref="StatisticalDataCollection"/> records, managing the connection.
        /// </summary>
        /// <param name="statisticalUnits">The collection of statistical units to query.</param>
        /// <param name="batchSize">The maximum number of units per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A dictionary mapping each <see cref="StatisticalUnit"/> to its matching <see cref="StatisticalDataCollection"/>, or null if connection cannot be established.</returns>
        public async Task<Dictionary<StatisticalUnit, StatisticalDataCollection>?> GetStatisticalDataCollectionDictionaryAsync(IEnumerable<StatisticalUnit>? statisticalUnits, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalDataCollectionDictionaryAsync(npgsqlConnection, statisticalUnits, batchSize, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the total count of records in the statistical data collection table.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The total count of records, or -1 if the connection is null.</returns>
        public static async Task<long> GetCountAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, TableName, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the total count of records in the statistical data collection table, managing the connection.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The total count of records, or -1 if connection cannot be established.</returns>
        public async Task<long> GetCountAsync(int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetCountAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated count of records from the database table.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="analyze">Whether to run VACUUM ANALYZE before retrieving the estimate.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The estimated count of records, -1 when unanalysed, or null if the table does not exist or connection is null.</returns>
        public static async Task<long?> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, bool analyze = false, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, TableName, analyze, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated count of records from the database table, managing the connection.
        /// </summary>
        /// <param name="analyze">Whether to run VACUUM ANALYZE before retrieving the estimate.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The estimated count of records, -1 when unanalysed, or null if the table does not exist or connection cannot be established.</returns>
        public async Task<long?> GetEstimatedCountAsync(bool analyze = false, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetEstimatedCountAsync(npgsqlConnection, analyze, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves all territorial unit identifiers currently stored in the statistical data collection table.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of unit identifiers, or null if connection is null.</returns>
        public static async Task<List<string>?> GetIdsAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT id
                FROM {TableName}
                ORDER BY id ASC;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;

            List<string> result = [];
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves all territorial unit identifiers currently stored in the statistical data collection table, managing the connection.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of unit identifiers, or null if connection cannot be established.</returns>
        public async Task<List<string>?> GetIdsAsync(int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetIdsAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously checks whether a statistical data collection exists for the specified unit identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="id">The territorial unit identifier.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if a collection exists for the specified identifier; otherwise, false.</returns>
        public static async Task<bool> ContainsAsync(NpgsqlConnection? npgsqlConnection, string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            string commandText = $@"
                SELECT 1
                FROM {TableName}
                WHERE id = @id
                LIMIT 1;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Text) { Value = id });

            object? scalar = await npgsqlCommand.ExecuteScalarAsync(cancellationToken);
            return scalar is not null;
        }

        /// <summary>
        /// Asynchronously checks whether a statistical data collection exists for the specified unit identifier, managing the connection.
        /// </summary>
        /// <param name="id">The territorial unit identifier.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if a collection exists for the specified identifier; otherwise, false.</returns>
        public async Task<bool> ContainsAsync(string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await ContainsAsync(npgsqlConnection, id, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously checks whether a statistical data collection exists for the specified <see cref="StatisticalUnit"/>.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="statisticalUnit">The statistical unit to check.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if a collection exists for the specified statistical unit; otherwise, false.</returns>
        public static async Task<bool> ContainsAsync(NpgsqlConnection? npgsqlConnection, StatisticalUnit? statisticalUnit, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (statisticalUnit is null || string.IsNullOrWhiteSpace(statisticalUnit.Code))
            {
                return false;
            }

            return await ContainsAsync(npgsqlConnection, statisticalUnit.Code, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously checks whether a statistical data collection exists for the specified <see cref="StatisticalUnit"/>, managing the connection.
        /// </summary>
        /// <param name="statisticalUnit">The statistical unit to check.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if a collection exists for the specified statistical unit; otherwise, false.</returns>
        public async Task<bool> ContainsAsync(StatisticalUnit? statisticalUnit, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (statisticalUnit is null || string.IsNullOrWhiteSpace(statisticalUnit.Code))
            {
                return false;
            }

            return await ContainsAsync(statisticalUnit.Code, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the names of all statistical data series stored for the specified unit identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="id">The territorial unit identifier.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of statistical data item names, or null if the collection was not found or connection is null.</returns>
        public static async Task<List<string>?> GetStatisticalDataNamesAsync(NpgsqlConnection? npgsqlConnection, string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            StatisticalDataCollection? collection = await GetStatisticalDataCollectionByIdAsync(npgsqlConnection, id, commandTimeout, cancellationToken);
            if (collection is null)
            {
                return null;
            }

            return [.. collection.Names];
        }

        /// <summary>
        /// Asynchronously retrieves the names of all statistical data series stored for the specified unit identifier, managing the connection.
        /// </summary>
        /// <param name="id">The territorial unit identifier.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of statistical data item names, or null if the collection was not found or connection cannot be established.</returns>
        public async Task<List<string>?> GetStatisticalDataNamesAsync(string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            StatisticalDataCollection? collection = await GetStatisticalDataCollectionByIdAsync(id, commandTimeout, cancellationToken);
            if (collection is null)
            {
                return null;
            }

            return [.. collection.Names];
        }

        /// <summary>
        /// Asynchronously retrieves the names of all statistical data series stored for the specified <see cref="StatisticalUnit"/>.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="statisticalUnit">The statistical unit.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of statistical data item names, or null if the collection was not found or connection is null.</returns>
        public static async Task<List<string>?> GetStatisticalDataNamesAsync(NpgsqlConnection? npgsqlConnection, StatisticalUnit? statisticalUnit, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (statisticalUnit is null || string.IsNullOrWhiteSpace(statisticalUnit.Code))
            {
                return null;
            }

            return await GetStatisticalDataNamesAsync(npgsqlConnection, statisticalUnit.Code, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the names of all statistical data series stored for the specified <see cref="StatisticalUnit"/>, managing the connection.
        /// </summary>
        /// <param name="statisticalUnit">The statistical unit.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of statistical data item names, or null if the collection was not found or connection cannot be established.</returns>
        public async Task<List<string>?> GetStatisticalDataNamesAsync(StatisticalUnit? statisticalUnit, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (statisticalUnit is null || string.IsNullOrWhiteSpace(statisticalUnit.Code))
            {
                return null;
            }

            return await GetStatisticalDataNamesAsync(statisticalUnit.Code, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously populates statistical data collections into PostgreSQL by reading .sdcf files from a specified file path or directory.
        /// </summary>
        /// <param name="path">The file path to a single .sdcf file or a directory containing .sdcf files.</param>
        /// <param name="clear">Whether to clear existing records in the table before inserting.</param>
        /// <param name="batchSize">The maximum number of collections per database insert batch.</param>
        /// <param name="progress">Optional progress reporter carrying the count of inserted records.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if population succeeded; otherwise, false.</returns>
        public async Task<bool> PopulateAsync(string? path, bool clear = false, int batchSize = 1000, IProgress<long>? progress = null, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            List<string> filePaths = [];
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, $"*.{GIS.Constants.FileExtension.StatisticalDataCollectionFile}");
                if (files is not null && files.Length > 0)
                {
                    filePaths.AddRange(files);
                }
            }
            else if (File.Exists(path))
            {
                filePaths.Add(path);
            }
            else
            {
                return false;
            }

            if (filePaths.Count == 0)
            {
                return false;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            if (clear)
            {
                await ClearAsync(npgsqlConnection, commandTimeout, cancellationToken);
            }

            long totalInserted = 0;

            foreach (string filePath in filePaths)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using StatisticalDataCollectionFile file = new(filePath);
                IEnumerable<StatisticalDataCollection?>? values = file.Values;
                if (values is null)
                {
                    continue;
                }

                List<StatisticalDataCollection> collections = [.. values.OfType<StatisticalDataCollection>()];
                if (collections.Count == 0)
                {
                    continue;
                }

                List<string> inserted = await InsertAsync(npgsqlConnection, collections, batchSize, commandTimeout, cancellationToken);
                totalInserted += inserted.Count;
                progress?.Report(totalInserted);
            }

            return true;
        }

        private static async Task<List<StatisticalDataCollection>?> ReadAsync_StatisticalDataCollection(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken)
        {
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            List<StatisticalDataCollection> result = [];

            while (await reader.ReadAsync(cancellationToken))
            {
                if (reader.IsDBNull(1))
                {
                    continue;
                }

                string jsonString = reader.GetString(1);
                JsonObject? jsonObject = JsonNode.Parse(jsonString) as JsonObject;
                if (jsonObject is not null)
                {
                    StatisticalDataCollection collection = new(jsonObject);
                    result.Add(collection);
                }
            }

            return result;
        }
    }
}
