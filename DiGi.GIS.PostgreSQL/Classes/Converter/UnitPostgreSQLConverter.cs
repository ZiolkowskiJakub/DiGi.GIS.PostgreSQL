using DiGi.BDL.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Enums;
using DiGi.GIS.PostgreSQL.Constants;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides functionality to convert and manage BDL <see cref="Unit"/> entities within a PostgreSQL database,
    /// implementing the <see cref="IGISPostgreSQLConverter"/> interface.
    /// </summary>
    public class UnitPostgreSQLConverter : PostgreSQLConverter<Unit>, IGISPostgreSQLConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData"/> containing database connection settings.</param>
        public UnitPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the database table associated with territorial units.
        /// </summary>
        public static string TableName => Constants.TableName.Unit;

        /// <summary>
        /// Asynchronously creates the unit table in the database if it does not already exist.
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

            return await Create.TableAsync_Unit(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously creates the unit table in the database if it does not already exist, managing the connection.
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
        /// Asynchronously clears all records from the unit table.
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
        /// Asynchronously clears all records from the unit table, managing the connection.
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
        /// Asynchronously inserts or updates a collection of <see cref="Unit"/> entities in the database in batches.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="units">The collection of units to insert or update.</param>
        /// <param name="batchSize">The maximum number of units per batch command.</param>
        /// <param name="commandTimeout">The timeout in seconds for the command execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of unit identifiers successfully inserted or updated.</returns>
        public static async Task<List<string>> InsertAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<Unit>? units, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || units is null)
            {
                return [];
            }

            List<Unit> unitList = [.. units.Where(u => u is not null && !string.IsNullOrWhiteSpace(u.id))];
            if (unitList.Count == 0)
            {
                return [];
            }

            bool tableCreated = await CreateTableAsync(npgsqlConnection, commandTimeout, cancellationToken);
            if (!tableCreated)
            {
                return [];
            }

            List<string> insertedIds = [];

            for (int i = 0; i < unitList.Count; i += batchSize)
            {
                cancellationToken.ThrowIfCancellationRequested();

                List<Unit> chunk = unitList.Skip(i).Take(batchSize).ToList();

                await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);
                npgsqlBatch.Timeout = commandTimeout;

                foreach (Unit unit in chunk)
                {
                    NpgsqlBatchCommand batchCommand = new($@"
                        INSERT INTO {TableName} (id, name, level, has_description)
                        VALUES (@id, @name, @level, @has_description)
                        ON CONFLICT (id)
                        DO UPDATE SET
                            name = EXCLUDED.name,
                            level = EXCLUDED.level,
                            has_description = EXCLUDED.has_description
                        RETURNING id;");

                    batchCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Text) { Value = unit.id ?? string.Empty });
                    batchCommand.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Text) { Value = (object?)unit.name ?? DBNull.Value });
                    batchCommand.Parameters.Add(new NpgsqlParameter("level", NpgsqlDbType.Smallint) { Value = unit.level });
                    batchCommand.Parameters.Add(new NpgsqlParameter("has_description", NpgsqlDbType.Boolean) { Value = unit.hasDescription });

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
        /// Asynchronously inserts or updates a collection of <see cref="Unit"/> entities in the database, managing the connection.
        /// </summary>
        /// <param name="units">The collection of units to insert or update.</param>
        /// <param name="batchSize">The maximum number of units per batch command.</param>
        /// <param name="commandTimeout">The timeout in seconds for the command execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of unit identifiers successfully inserted or updated.</returns>
        public async Task<List<string>> InsertAsync(IEnumerable<Unit>? units, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await InsertAsync(npgsqlConnection, units, batchSize, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves all units or units filtered by level from the database.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="level">Optional level filter (0=country, 1=macroregion, 2=voivodeship, etc.).</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of <see cref="Unit"/> entities, or null if connection is null.</returns>
        public static async Task<List<Unit>?> GetUnitsAsync(NpgsqlConnection? npgsqlConnection, short? level = null, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string whereClause = level.HasValue ? "WHERE level = @level" : string.Empty;
            string commandText = $@"
                SELECT id, name, level, has_description
                FROM {TableName}
                {whereClause}
                ORDER BY level ASC, id ASC;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;

            if (level.HasValue)
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("level", NpgsqlDbType.Smallint) { Value = level.Value });
            }

            return await ReadAsync_Unit(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves all units or units filtered by level from the database, managing the connection.
        /// </summary>
        /// <param name="level">Optional level filter.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of <see cref="Unit"/> entities, or null if connection cannot be established.</returns>
        public async Task<List<Unit>?> GetUnitsAsync(short? level = null, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetUnitsAsync(npgsqlConnection, level, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a unit by its unique identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="id">The unique identifier of the unit.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Unit"/> if found; otherwise, null.</returns>
        public static async Task<Unit?> GetUnitByIdAsync(NpgsqlConnection? npgsqlConnection, string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            string commandText = $@"
                SELECT id, name, level, has_description
                FROM {TableName}
                WHERE id = @id
                LIMIT 1;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Text) { Value = id });

            List<Unit>? units = await ReadAsync_Unit(npgsqlCommand, cancellationToken);
            return units?.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously retrieves a unit by its unique identifier, managing the connection.
        /// </summary>
        /// <param name="id">The unique identifier of the unit.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Unit"/> if found; otherwise, null.</returns>
        public async Task<Unit?> GetUnitByIdAsync(string? id, int commandTimeout = 30, CancellationToken cancellationToken = default)
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
            return await GetUnitByIdAsync(npgsqlConnection, id, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves units matching a collection of identifiers in batched queries.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="ids">The collection of unit identifiers to retrieve.</param>
        /// <param name="batchSize">The maximum number of identifiers to query per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching <see cref="Unit"/> entities, or null if connection is null.</returns>
        public static async Task<List<Unit>?> GetUnitsByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? ids, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
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

            List<Unit> result = [];

            for (int i = 0; i < idList.Count; i += batchSize)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string[] idChunk = idList.Skip(i).Take(batchSize).ToArray();

                string commandText = $@"
                    SELECT id, name, level, has_description
                    FROM {TableName}
                    WHERE id = ANY(@ids)
                    ORDER BY level ASC, id ASC;";

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.CommandTimeout = commandTimeout;
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("ids", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = idChunk });

                List<Unit>? chunkResult = await ReadAsync_Unit(npgsqlCommand, cancellationToken);
                if (chunkResult is not null)
                {
                    result.AddRange(chunkResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves units matching a collection of identifiers, managing the connection.
        /// </summary>
        /// <param name="ids">The collection of unit identifiers to retrieve.</param>
        /// <param name="batchSize">The maximum number of identifiers to query per batch.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching <see cref="Unit"/> entities, or null if connection cannot be established.</returns>
        public async Task<List<Unit>?> GetUnitsByIdsAsync(IEnumerable<string>? ids, int batchSize = 1000, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetUnitsByIdsAsync(npgsqlConnection, ids, batchSize, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves units matching the specified name (case-insensitive search).
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="name">The name or part of the name to search for.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching <see cref="Unit"/> entities, or null if connection is null.</returns>
        public static async Task<List<Unit>?> GetUnitsByNameAsync(NpgsqlConnection? npgsqlConnection, string? name, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            string commandText = $@"
                SELECT id, name, level, has_description
                FROM {TableName}
                WHERE name ILIKE @name
                ORDER BY level ASC, name ASC, id ASC;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Text) { Value = $"%{name}%" });

            return await ReadAsync_Unit(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves units matching the specified name, managing the connection.
        /// </summary>
        /// <param name="name">The name or part of the name to search for.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching <see cref="Unit"/> entities, or null if connection cannot be established.</returns>
        public async Task<List<Unit>?> GetUnitsByNameAsync(string? name, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetUnitsByNameAsync(npgsqlConnection, name, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the distribution of unit counts grouped by level.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A dictionary mapping level to the count of units, or null if connection is null.</returns>
        public static async Task<Dictionary<short, int>?> GetCountsByLevelAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT level, COUNT(*)::int
                FROM {TableName}
                GROUP BY level
                ORDER BY level ASC;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;

            Dictionary<short, int> result = [];
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                short level = reader.GetInt16(0);
                int count = reader.GetInt32(1);
                result[level] = count;
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves the distribution of unit counts grouped by level, managing the connection.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A dictionary mapping level to the count of units, or null if connection cannot be established.</returns>
        public async Task<Dictionary<short, int>?> GetCountsByLevelAsync(int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetCountsByLevelAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously constructs the root <see cref="StatisticalUnit"/> hierarchy from all stored unit entities in the database.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The root <see cref="StatisticalUnit"/> if units are present; otherwise, null.</returns>
        public static async Task<StatisticalUnit?> GetStatisticalUnitAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            List<Unit>? units = await GetUnitsAsync(npgsqlConnection, level: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (units is null || units.Count == 0)
            {
                return null;
            }

            return GIS.Create.StatisticalUnit(units);
        }

        /// <summary>
        /// Asynchronously constructs the root <see cref="StatisticalUnit"/> hierarchy from all stored unit entities in the database, managing the connection.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The root <see cref="StatisticalUnit"/> if units are present; otherwise, null.</returns>
        public async Task<StatisticalUnit?> GetStatisticalUnitAsync(int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalUnitAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the matching <see cref="StatisticalUnit"/> for the specified <see cref="AdministrativeAreal2D"/>.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="administrativeAreal2D">The administrative area to match.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static async Task<StatisticalUnit?> GetStatisticalUnitAsync(NpgsqlConnection? npgsqlConnection, AdministrativeAreal2D? administrativeAreal2D, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || administrativeAreal2D is null)
            {
                return null;
            }

            StatisticalUnit? root = await GetStatisticalUnitAsync(npgsqlConnection, commandTimeout, cancellationToken);
            if (root is null)
            {
                return null;
            }

            return Query.Match(root, administrativeAreal2D);
        }

        /// <summary>
        /// Asynchronously retrieves the matching <see cref="StatisticalUnit"/> for the specified <see cref="AdministrativeAreal2DReference"/>.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="administrativeAreal2DReference">The administrative area reference to match.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static async Task<StatisticalUnit?> GetStatisticalUnitAsync(NpgsqlConnection? npgsqlConnection, AdministrativeAreal2DReference? administrativeAreal2DReference, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || administrativeAreal2DReference is null)
            {
                return null;
            }

            StatisticalUnit? root = await GetStatisticalUnitAsync(npgsqlConnection, commandTimeout, cancellationToken);
            if (root is null)
            {
                return null;
            }

            return Query.Match(root, administrativeAreal2DReference);
        }

        /// <summary>
        /// Asynchronously retrieves the matching <see cref="StatisticalUnit"/> for the specified <see cref="AdministrativeAreal2D"/>, managing the connection.
        /// </summary>
        /// <param name="administrativeAreal2D">The administrative area to match.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public async Task<StatisticalUnit?> GetStatisticalUnitAsync(AdministrativeAreal2D? administrativeAreal2D, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (administrativeAreal2D is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalUnitAsync(npgsqlConnection, administrativeAreal2D, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the matching <see cref="StatisticalUnit"/> for the specified <see cref="AdministrativeAreal2DReference"/>, managing the connection.
        /// </summary>
        /// <param name="administrativeAreal2DReference">The administrative area reference to match.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public async Task<StatisticalUnit?> GetStatisticalUnitAsync(AdministrativeAreal2DReference? administrativeAreal2DReference, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (administrativeAreal2DReference is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetStatisticalUnitAsync(npgsqlConnection, administrativeAreal2DReference, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously measures matching compliance of all administrative areas of the specified type against the BDL Unit tree.
        /// </summary>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter used to read administrative areal references.</param>
        /// <param name="administrativeArealType">The administrative area type to evaluate.</param>
        /// <param name="commandTimeout">The timeout in seconds for database commands.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation, returning the <see cref="UnitComplianceResult"/>.</returns>
        public async Task<UnitComplianceResult?> GetComplianceAsync(AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, AdministrativeArealType administrativeArealType, int commandTimeout = 60, CancellationToken cancellationToken = default)
        {
            return await Create.UnitComplianceResultAsync(this, administrativeAreal2DPostgreSQLConverter, administrativeArealType, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously populates the unit table from the central statistical office (BDL) API.
        /// </summary>
        /// <param name="pageSize">The page size used for BDL API requests.</param>
        /// <param name="clear">Whether to clear existing records before inserting.</param>
        /// <param name="batchSize">The maximum number of units per database insert batch.</param>
        /// <param name="clientId">Optional BDL API client identifier (API key).</param>
        /// <param name="progress">Progress reporter carrying the count of inserted records.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if population succeeded; otherwise, false.</returns>
        public async Task<bool> PopulateAsync(int pageSize = 100, bool clear = false, int batchSize = 1000, string? clientId = null, IProgress<long>? progress = null, int commandTimeout = 60, CancellationToken cancellationToken = default)
        {
            List<Unit>? units = await BDL.Create.Units(pageSize, clientId);
            if (units is null)
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

            List<string> inserted = await InsertAsync(npgsqlConnection, units, batchSize, commandTimeout, cancellationToken);
            progress?.Report(inserted.Count);

            return inserted.Count > 0 || units.Count == 0;
        }

        /// <summary>
        /// Asynchronously populates the unit table from a local JSON file or directory containing JSON files.
        /// </summary>
        /// <param name="npgsqlConnection">The active <see cref="NpgsqlConnection"/>.</param>
        /// <param name="path">The file path or directory path containing unit JSON files.</param>
        /// <param name="clear">Whether to clear existing records before inserting.</param>
        /// <param name="batchSize">The maximum number of units per database insert batch.</param>
        /// <param name="progress">Progress reporter carrying the count of inserted records.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if population succeeded; otherwise, false.</returns>
        public static async Task<bool> PopulateAsync(NpgsqlConnection? npgsqlConnection, string? path, bool clear = false, int batchSize = 1000, IProgress<long>? progress = null, int commandTimeout = 60, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            List<string> filePaths = [];
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
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

            if (clear)
            {
                await ClearAsync(npgsqlConnection, commandTimeout, cancellationToken);
            }

            long totalInserted = 0;

            foreach (string filePath in filePaths)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string json;
                try
                {
                    json = await File.ReadAllTextAsync(filePath, cancellationToken);
                }
                catch
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(json))
                {
                    continue;
                }

                List<Unit>? units;
                try
                {
                    units = JsonSerializer.Deserialize<List<Unit>>(json);
                }
                catch
                {
                    continue;
                }

                if (units is null || units.Count == 0)
                {
                    continue;
                }

                List<string> inserted = await InsertAsync(npgsqlConnection, units, batchSize, commandTimeout, cancellationToken);
                totalInserted += inserted.Count;
                progress?.Report(totalInserted);
            }

            return totalInserted > 0 || filePaths.Count > 0;
        }

        /// <summary>
        /// Asynchronously populates the unit table from a local JSON file or directory containing JSON files, managing the connection.
        /// </summary>
        /// <param name="path">The file path or directory path containing unit JSON files.</param>
        /// <param name="clear">Whether to clear existing records before inserting.</param>
        /// <param name="batchSize">The maximum number of units per database insert batch.</param>
        /// <param name="progress">Progress reporter carrying the count of inserted records.</param>
        /// <param name="commandTimeout">The timeout in seconds for database execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if population succeeded; otherwise, false.</returns>
        public async Task<bool> PopulateAsync(string? path, bool clear = false, int batchSize = 1000, IProgress<long>? progress = null, int commandTimeout = 60, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await PopulateAsync(npgsqlConnection, path, clear, batchSize, progress, commandTimeout, cancellationToken);
        }

        private static async Task<List<Unit>?> ReadAsync_Unit(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken)
        {
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            List<Unit> result = [];

            while (await reader.ReadAsync(cancellationToken))
            {
                Unit unit = new()
                {
                    id = reader.IsDBNull(0) ? null : reader.GetString(0),
                    name = reader.IsDBNull(1) ? null : reader.GetString(1),
                    level = reader.GetInt16(2),
                    hasDescription = reader.GetBoolean(3)
                };

                result.Add(unit);
            }

            return result;
        }
    }
}
