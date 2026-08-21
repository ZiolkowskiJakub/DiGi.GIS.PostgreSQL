using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Constants;
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
    /// <summary>
    /// Provides a specialized implementation for converting <see cref="OrtoDatas"/> objects to and from a PostgreSQL database format, incorporating GIS-specific conversion capabilities.
    /// </summary>
    public class OrtoDatasPostgreSQLConverter : PostgreSQLConverter<OrtoDatas>, IGISPostgreSQLConverter<OrtoDatas>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData"/> containing the connection settings required to establish a connection to the PostgreSQL database. This value can be null.</param>
        public OrtoDatasPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Asynchronously retrieves the count of records from the database, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a <see cref="long"/>.</returns>
        public static async Task<long> GetCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string tableName = TableName.OrtoDatas;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, tableName, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier for the county; if null, the estimate is calculated across all counties.</param>
        /// <param name="analyze">A value indicating whether to perform an ANALYZE operation on the database table to update statistics before retrieving the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated count as a long integer.</returns>
        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string tableName = TableName.OrtoDatas;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, tableName, analyze, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the estimated row count for the specified county identifiers in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> to use for the query.</param>
        /// <param name="countyIds">A collection of integers representing the county identifiers to estimate counts for.</param>
        /// <param name="analyze">A boolean indicating whether to run an analysis operation before fetching the estimated count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total estimated row count as a long, or -1 if an error occurs.</returns>
        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int> countyIds, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            long result = 0;
            foreach (int countyId in countyIds)
            {
                string tableName = string.Format("{0}_{1}", TableName.OrtoDatas, countyId);
                result += await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, tableName, analyze, cancellationToken);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves existing building 2D references from the database based on the provided collection.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the PostgreSQL database.</param>
        /// <param name="building2DReferences">A collection of <see cref="Building2DReference"/> objects to check for existence in the database.</param>
        /// <param name="inverted">A boolean value indicating whether to invert the search criteria, returning references that do not exist if set to true.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone across all partitions for references not matched by county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2DReference"/> objects, or null if the connection or input collection is null.</returns>
        public static async Task<List<Building2DReference>?> GetExistingBuilding2DReferencesAsync(
            NpgsqlConnection? npgsqlConnection,
            IEnumerable<Building2DReference>? building2DReferences,
            bool inverted = false,
            bool fallbackByReference = false,
            CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || building2DReferences is null)
            {
                return null;
            }

            List<Building2DReference> building2DReferences_List = [.. building2DReferences];
            if (building2DReferences_List.Count == 0)
            {
                return [];
            }

            // 1. Prepare data for UNNEST
            int[] countyIds = [.. building2DReferences_List.Select(l => l.CountyId ?? 0)];
            string[] references = [.. building2DReferences_List.Select(l => l.Reference ?? string.Empty)];

            // 2. We use a LEFT JOIN between the input list (UNNEST) and the actual table.
            // If u.reference IS NULL, it means the item does NOT exist in the database.
            const string commandText = $@"
                SELECT input.c, input.r
                FROM UNNEST(@counties, @refs) AS input(c, r)
                LEFT JOIN {TableName.OrtoDatas} u ON (input.c = 0 OR u.county_id = input.c) AND u.reference = input.r
                WHERE (@inverted = false AND u.reference IS NOT NULL)  -- Item exists
                   OR (@inverted = true AND u.reference IS NULL);     -- Item does not exist";

            List<Building2DReference> results = [];

            try
            {
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("counties", countyIds);
                npgsqlCommand.Parameters.AddWithValue("refs", references);
                npgsqlCommand.Parameters.AddWithValue("inverted", inverted);

                await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
                while (await npgsqlDataReader.ReadAsync(cancellationToken))
                {
                    results.Add(new Building2DReference { CountyId = npgsqlDataReader.GetInt32(0), Reference = npgsqlDataReader.GetString(1) });
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Postgres Error {nameof(GetExistingBuilding2DReferencesAsync)}: {ex.Message}");
                return null;
            }

            if (fallbackByReference)
            {
                if (!inverted)
                {
                    HashSet<string> foundReferences = [.. results.Where(r => !string.IsNullOrWhiteSpace(r.Reference)).Select(r => r.Reference!)];
                    string[] missingReferences = [.. references.Where(r => !string.IsNullOrWhiteSpace(r) && !foundReferences.Contains(r)).Distinct()];

                    if (missingReferences.Length > 0)
                    {
                        const string fallbackCommandText = $@"
                            SELECT u.county_id, u.reference
                            FROM UNNEST(@missingRefs) AS input(r)
                            INNER JOIN {TableName.OrtoDatas} u ON u.reference = input.r;";

                        try
                        {
                            await using NpgsqlCommand fallbackCommand = new(fallbackCommandText, npgsqlConnection);
                            fallbackCommand.Parameters.AddWithValue("missingRefs", missingReferences);

                            await using NpgsqlDataReader fallbackReader = await fallbackCommand.ExecuteReaderAsync(cancellationToken);
                            while (await fallbackReader.ReadAsync(cancellationToken))
                            {
                                results.Add(new Building2DReference
                                {
                                    CountyId = fallbackReader.GetInt32(0),
                                    Reference = fallbackReader.GetString(1)
                                });
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine($"Postgres Error {nameof(GetExistingBuilding2DReferencesAsync)} fallback: {ex.Message}");
                            return null;
                        }
                    }
                }
                else
                {
                    string[] candidateReferences = [.. results.Where(r => !string.IsNullOrWhiteSpace(r.Reference)).Select(r => r.Reference!).Distinct()];

                    if (candidateReferences.Length > 0)
                    {
                        const string checkExistCommandText = $@"
                            SELECT DISTINCT u.reference
                            FROM UNNEST(@candidateRefs) AS input(r)
                            INNER JOIN {TableName.OrtoDatas} u ON u.reference = input.r;";

                        HashSet<string> existingGlobally = [];

                        try
                        {
                            await using NpgsqlCommand checkCommand = new(checkExistCommandText, npgsqlConnection);
                            checkCommand.Parameters.AddWithValue("candidateRefs", candidateReferences);

                            await using NpgsqlDataReader checkReader = await checkCommand.ExecuteReaderAsync(cancellationToken);
                            while (await checkReader.ReadAsync(cancellationToken))
                            {
                                existingGlobally.Add(checkReader.GetString(0));
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine($"Postgres Error {nameof(GetExistingBuilding2DReferencesAsync)} inverted fallback: {ex.Message}");
                            return null;
                        }

                        if (existingGlobally.Count > 0)
                        {
                            results.RemoveAll(r => !string.IsNullOrWhiteSpace(r.Reference) && existingGlobally.Contains(r.Reference));
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="OrtoDatas" /> based on the specified references and optional county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> used to connect to the database.</param>
        /// <param name="references">A collection of strings representing the references to search for.</param>
        /// <param name="countyId">The optional integer identifier of the county to filter the results.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback query without county filtering for references not found in the initial search.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrtoDatas" /> objects, or null if the connection is null or no matching data is found.</returns>
        public static async Task<List<OrtoDatas>?> GetOrtoDatasByReferencesAsync(
            NpgsqlConnection? npgsqlConnection,
            IEnumerable<string>? references,
            int? countyId,
            bool fallbackByReference = false,
            CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || references is null)
            {
                return null;
            }

            string[] references_Array = [.. references];
            if (references_Array.Length == 0)
            {
                return [];
            }

            const string commandText = $@"
                SELECT id, county_id, reference, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM {TableName.OrtoDatas}
                WHERE reference = ANY(@references)
                  AND (@countyId IS NULL OR county_id = @countyId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("references", references_Array);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            List<OrtoDatas>? result = await ReadAsync_OrtoDatas(npgsqlCommand, cancellationToken);
            if (result is null)
            {
                return null;
            }

            if (fallbackByReference && countyId is not null)
            {
                HashSet<string> foundReferences = [.. result.Where(o => !string.IsNullOrWhiteSpace(o.Reference)).Select(o => o.Reference!)];
                string[] missingReferences = [.. references_Array.Where(r => !string.IsNullOrWhiteSpace(r) && !foundReferences.Contains(r)).Distinct()];

                if (missingReferences.Length > 0)
                {
                    List<OrtoDatas>? fallbackItems = await GetOrtoDatasByReferencesAsync(npgsqlConnection, missingReferences, null, false, cancellationToken);
                    if (fallbackItems is not null && fallbackItems.Count > 0)
                    {
                        result.AddRange(fallbackItems);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously clears all data and restarts the identity sequence.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> ClearAsync(CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            bool result_1 = await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName.OrtoDatas, cancellationToken: cancellationToken);
            bool result_2 = await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName.OrtoDatas_Building2DReference_Update, cancellationToken: cancellationToken);

            return result_1 || result_2;
        }

        /// <summary>
        /// Asynchronously checks for the existence of a collection of references, optionally filtered by a county identifier.
        /// </summary>
        /// <param name="references">An <see cref="IEnumerable{T}"/> of strings representing the references to be checked.</param>
        /// <param name="countyId">The optional integer identifier for the county; if null, the search is not filtered by county.</param>
        /// <param name="inverted">A boolean value indicating whether to return the set of references that do not exist (true) or those that do exist (false).</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback check without county filtering for references not matched by county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="HashSet{T}"/> of strings containing the filtered references, or null if the operation fails or no results are found.</returns>
        public async Task<HashSet<string>?> ContainsByReferencesAsync(
            IEnumerable<string> references,
            int? countyId,
            bool inverted = false,
            bool fallbackByReference = false,
            CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            string[] referenceArray = [.. references.Distinct()];
            if (referenceArray.Length == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            HashSet<string> result = [];

            const string commandText = $@"
                SELECT input_ref
                FROM UNNEST(@refs) AS input_ref
                LEFT JOIN {TableName.OrtoDatas} u ON u.reference = input_ref
                    AND (@county_id IS NULL OR u.county_id = @county_id)
                WHERE
                    (@inverted = false AND u.reference IS NOT NULL)
                    OR
                    (@inverted = true AND u.reference IS NULL);";

            try
            {
                await npgsqlConnection.OpenAsync(cancellationToken);

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                npgsqlCommand.Parameters.Add(new NpgsqlParameter("refs", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = referenceArray });
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = (object?)countyId ?? DBNull.Value });
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("inverted", NpgsqlDbType.Boolean) { Value = inverted });

                await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    result.Add(reader.GetString(0));
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Database error in {nameof(ContainsByReferencesAsync)}: {ex.Message}");
                throw;
            }

            if (fallbackByReference && countyId is not null)
            {
                if (!inverted)
                {
                    string[] missingReferences = [.. referenceArray.Where(r => !result.Contains(r))];
                    if (missingReferences.Length > 0)
                    {
                        const string fallbackCommandText = $@"
                            SELECT DISTINCT u.reference
                            FROM UNNEST(@missingRefs) AS input(r)
                            INNER JOIN {TableName.OrtoDatas} u ON u.reference = input.r;";

                        try
                        {
                            await using NpgsqlCommand fallbackCommand = new(fallbackCommandText, npgsqlConnection);
                            fallbackCommand.Parameters.Add(new NpgsqlParameter("missingRefs", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = missingReferences });

                            await using NpgsqlDataReader fallbackReader = await fallbackCommand.ExecuteReaderAsync(cancellationToken);
                            while (await fallbackReader.ReadAsync(cancellationToken))
                            {
                                result.Add(fallbackReader.GetString(0));
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine($"Database error in {nameof(ContainsByReferencesAsync)} fallback: {ex.Message}");
                            throw;
                        }
                    }
                }
                else
                {
                    string[] candidateReferences = [.. result];
                    if (candidateReferences.Length > 0)
                    {
                        const string checkExistCommandText = $@"
                            SELECT DISTINCT u.reference
                            FROM UNNEST(@candidateRefs) AS input(r)
                            INNER JOIN {TableName.OrtoDatas} u ON u.reference = input.r;";

                        HashSet<string> existingGlobally = [];

                        try
                        {
                            await using NpgsqlCommand checkCommand = new(checkExistCommandText, npgsqlConnection);
                            checkCommand.Parameters.Add(new NpgsqlParameter("candidateRefs", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = candidateReferences });

                            await using NpgsqlDataReader checkReader = await checkCommand.ExecuteReaderAsync(cancellationToken);
                            while (await checkReader.ReadAsync(cancellationToken))
                            {
                                existingGlobally.Add(checkReader.GetString(0));
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine($"Database error in {nameof(ContainsByReferencesAsync)} inverted fallback: {ex.Message}");
                            throw;
                        }

                        if (existingGlobally.Count > 0)
                        {
                            result.RemoveWhere(existingGlobally.Contains);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves the count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="countyId">The optional integer identifier of the county to filter the count; if null, the count is retrieved for all counties.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total row count as a long.</returns>
        public async Task<long> GetCountAsync(int? countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string tableName = TableName.OrtoDatas;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, tableName, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated row count, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="countyId">The optional <see cref="System.Int32"/> identifier of the county to filter the estimate; if null, the estimate is calculated for all counties.</param>
        /// <param name="analyze">A <see cref="System.Boolean"/> value indicating whether to run an analysis operation before fetching the count to improve accuracy.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated number of rows as a <see cref="System.Int64"/>, or -1 if an error occurs or the target does not exist.</returns>
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
        /// Asynchronously retrieves an estimated row count for the specified collection of county identifiers.
        /// </summary>
        /// <param name="countyIds">A collection of integers representing the IDs of the counties to estimate counts for.</param>
        /// <param name="analyze">A boolean value indicating whether to perform a database analysis before fetching the count.</param>
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
        /// Asynchronously retrieves existing building 2D references based on the provided collection and inversion criteria.
        /// </summary>
        /// <param name="building2DReferences">An <see cref="IEnumerable{Building2DReference}"/> of building 2D references to check for existence.</param>
        /// <param name="inverted">A boolean value indicating whether to invert the result; if set to <see langword="true"/>, retrieves references that do not exist.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone across all partitions for references not matched by county.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> of matching references, or null if the input collection is null.</returns>
        public async Task<List<Building2DReference>?> GetExistingBuilding2DReferencesAsync(
            IEnumerable<Building2DReference>? building2DReferences,
            bool inverted = false,
            bool fallbackByReference = false,
            CancellationToken cancellationToken = default)
        {
            if (building2DReferences == null)
            {
                return null;
            }

            if (!building2DReferences.Any())
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetExistingBuilding2DReferencesAsync(npgsqlConnection, building2DReferences, inverted, fallbackByReference, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of building 2D references.
        /// </summary>
        /// <param name="count">The maximum number of <see cref="Building2DReference"/> objects to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> if references are found; otherwise, null.</returns>
        public async Task<List<Building2DReference>?> GetNextBuilding2DReferencesAsync(int count = 100)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool exists = await DiGi.PostgreSQL.Query.TableExistsAsync(npgsqlConnection, TableName.OrtoDatas_Building2DReference_Update);
            if (!exists)
            {
                return null;
            }

            // FIX: Changed '=' to 'IN' to handle multiple rows from subquery
            string commandText = $@"
                DELETE FROM {TableName.OrtoDatas_Building2DReference_Update}
                WHERE id IN (
                    SELECT id FROM {TableName.OrtoDatas_Building2DReference_Update}
                    ORDER BY created_at ASC
                    FOR UPDATE SKIP LOCKED
                    LIMIT {count}
                )
                RETURNING id, county_id, reference, subdivision_id;";

            List<Building2DReference> result = [];

            try
            {
                await using NpgsqlCommand command = new(commandText, npgsqlConnection);
                await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Building2DReference building2DReference = new()
                    {
                        Id = reader.GetInt64(reader.GetOrdinal("id"))
                    };

                    int countyIdOrdinal = reader.GetOrdinal("county_id");
                    building2DReference.CountyId = reader.IsDBNull(countyIdOrdinal) ? null : (int?)reader.GetInt32(countyIdOrdinal);

                    int referenceOrdinal = reader.GetOrdinal("reference");
                    building2DReference.Reference = reader.IsDBNull(referenceOrdinal) ? null : reader.GetString(referenceOrdinal);

                    int subdivisionIdOrdinal = reader.GetOrdinal("subdivision_id");
                    building2DReference.SubdivisionId = reader.IsDBNull(subdivisionIdOrdinal) ? null : (int?)reader.GetInt32(subdivisionIdOrdinal);

                    result.Add(building2DReference);
                }
            }
            catch (NpgsqlException)
            {
                // Log error (using ILogger in Web API context)
                throw;
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves orthodata based on a specified reference and an optional county identifier.
        /// </summary>
        /// <param name="reference">The string reference used to identify the orthodata.</param>
        /// <param name="countyId">The optional integer identifier of the county.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback query without county filtering if not found in the specified county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OrtoDatas"/> object if found; otherwise, null.</returns>
        public async Task<OrtoDatas?> GetOrtoDatasByReferenceAsync(
            string reference,
            int? countyId,
            bool fallbackByReference = false,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetOrtoDatasByReferencesAsync([reference], countyId, fallbackByReference, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="OrtoDatas"/> based on the specified references and county identifier.
        /// </summary>
        /// <param name="references">An optional collection of strings representing the references to filter by.</param>
        /// <param name="countyId">An optional integer representing the unique identifier of the county.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback query without county filtering for references not found in the initial search.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrtoDatas"/> objects, or null if no matching data is found.</returns>
        public async Task<List<OrtoDatas>?> GetOrtoDatasByReferencesAsync(
            IEnumerable<string>? references,
            int? countyId,
            bool fallbackByReference = false,
            CancellationToken cancellationToken = default)
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

            return await GetOrtoDatasByReferencesAsync(npgsqlConnection, references, countyId, fallbackByReference, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="OrtoDatas"/> based on the specified building 2D references.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database.</param>
        /// <param name="building2DReferences">A collection of <see cref="Building2DReference"/> objects to search for.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone across all partitions for references not matched by county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrtoDatas"/> objects, or null if the connection is null or no matching data is found.</returns>
        public static async Task<List<OrtoDatas>?> GetOrtoDatasByBuilding2DReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<Building2DReference>? building2DReferences, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || building2DReferences is null)
            {
                return null;
            }

            List<Building2DReference> building2DReferences_List = [.. building2DReferences];
            if (building2DReferences_List.Count == 0)
            {
                return [];
            }

            int[] countyIds = [.. building2DReferences_List.Select(l => l.CountyId ?? 0)];
            string[] references = [.. building2DReferences_List.Select(l => l.Reference ?? string.Empty)];

            const string commandText = $@"
                SELECT u.id, u.county_id, u.reference, u.min_x, u.min_y, u.max_x, u.max_y, u.subdivision_id, u.object, u.created_at
                FROM UNNEST(@counties, @refs) AS input(c, r)
                INNER JOIN {TableName.OrtoDatas} u ON (input.c = 0 OR u.county_id = input.c) AND u.reference = input.r;";

            List<OrtoDatas>? result;

            await using (NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection))
            {
                npgsqlCommand.Parameters.AddWithValue("counties", countyIds);
                npgsqlCommand.Parameters.AddWithValue("refs", references);

                result = await ReadAsync_OrtoDatas(npgsqlCommand, cancellationToken);
            }

            if (result is null)
            {
                return null;
            }

            if (fallbackByReference)
            {
                HashSet<string> foundReferences = [.. result.Where(o => !string.IsNullOrWhiteSpace(o.Reference)).Select(o => o.Reference!)];
                string[] missingReferences = [.. references.Where(r => !string.IsNullOrWhiteSpace(r) && !foundReferences.Contains(r)).Distinct()];

                if (missingReferences.Length > 0)
                {
                    List<OrtoDatas>? fallbackItems = await GetOrtoDatasByReferencesAsync(npgsqlConnection, missingReferences, null, false, cancellationToken);
                    if (fallbackItems is not null && fallbackItems.Count > 0)
                    {
                        result.AddRange(fallbackItems);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="OrtoDatas"/> based on the specified building 2D references.
        /// </summary>
        /// <param name="building2DReferences">A collection of <see cref="Building2DReference"/> objects to search for.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone across all partitions for references not matched by county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrtoDatas"/> objects, or null if no matching data is found.</returns>
        public async Task<List<OrtoDatas>?> GetOrtoDatasByBuilding2DReferencesAsync(IEnumerable<Building2DReference>? building2DReferences, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            if (building2DReferences is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetOrtoDatasByBuilding2DReferencesAsync(npgsqlConnection, building2DReferences, fallbackByReference, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves orthodata based on a specified building 2D reference.
        /// </summary>
        /// <param name="building2DReference">The <see cref="Building2DReference"/> used to identify the orthodata.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone across all partitions if not matched by county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OrtoDatas"/> object if found; otherwise, null.</returns>
        public async Task<OrtoDatas?> GetOrtoDatasByBuilding2DReferenceAsync(Building2DReference? building2DReference, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            if (building2DReference is null || string.IsNullOrWhiteSpace(building2DReference.Reference))
            {
                return null;
            }

            List<OrtoDatas>? results = await GetOrtoDatasByBuilding2DReferencesAsync([building2DReference], fallbackByReference, cancellationToken);
            return results?.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously updates the data based on the provided orthodata and tolerance.
        /// <para>An entry that names no county row is resolved by geometry against every county its bounding box overlaps. When the county is known but which of its polygon parts is not, use <see cref="UpdateAsync(IEnumerable{OrtoDatas}, IEnumerable{int}, double)"/> and hand over the parts - it narrows the field before the geometry runs.</para>
        /// </summary>
        /// <param name="ortoDatas">A nullable enumerable collection of <see cref="OrtoDatas"/> to be processed for the update.</param>
        /// <param name="tolerance">A double-precision floating-point number representing the distance tolerance used during the update process. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers written and the rows dropped before the database, or null when the update could not be attempted at all - no connection, or the table could not be created.</returns>
        public async Task<PostgreSQLUpdateResult?> UpdateAsync(IEnumerable<OrtoDatas>? ortoDatas, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            return await UpdateAsync(ortoDatas, (IEnumerable<int>?)null, tolerance);
        }

        /// <summary>
        /// Asynchronously updates the data based on the provided orthodata and tolerance, resolving the county of an entry that names none from the given candidate rows.
        /// <para>County assignment runs in three tiers, in descending reliability:</para>
        /// <para>1. the entry's own county identifier - already names the row, nothing to infer.</para>
        /// <para>2. <paramref name="countyIds"/> - a county code names one row per polygon part, so the caller can state the county without stating the part. A single candidate resolves outright; several only narrow the field and are handed to tier 3.</para>
        /// <para>3. geometry - <see cref="Query.CountyId(IDictionary{int, Geometry.Planar.Interfaces.IPolygonal2D}, Geometry.Planar.Interfaces.IPolygonal2D, double)"/> decides which candidate the entry lies in, is nearest to, or overlaps most. With no candidates the field is every county its bounding box overlaps, which is both slower and wider than narrowing first.</para>
        /// </summary>
        /// <param name="ortoDatas">A nullable enumerable collection of <see cref="OrtoDatas"/> to be processed for the update.</param>
        /// <param name="countyIds">The candidate county rows an entry with no county of its own may be filed under. Null or empty searches every county overlapping the entry instead.</param>
        /// <param name="tolerance">A double-precision floating-point number representing the distance tolerance used during the update process. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers written and the rows dropped before the database, or null when the update could not be attempted at all - no connection, or the table could not be created.</returns>
        public async Task<PostgreSQLUpdateResult?> UpdateAsync(IEnumerable<OrtoDatas>? ortoDatas, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (ortoDatas is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await Create.TableAsync_OrtoDatas(npgsqlConnection);
            if (!succeded)
            {
                return null;
            }

            HashSet<long> ids = [];
            List<Rejection> rejections = [];

            if (!ortoDatas.Any())
            {
                return new PostgreSQLUpdateResult(ids, rejections);
            }

            List<int> countyIds_Candidate = countyIds is null ? [] : [.. new HashSet<int>(countyIds)];

            // Derived once for the whole batch: a part's polygon is the same for every entry tested against
            // it, and deriving one deserializes a county-sized geometry. Left null while there is nothing to
            // choose between, so a single candidate costs no polygon at all.
            Dictionary<int, Geometry.Planar.Interfaces.IPolygonal2D>? polygonal2Ds_ByCountyId = null;
            if (countyIds_Candidate.Count > 1)
            {
                polygonal2Ds_ByCountyId = (await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(npgsqlConnection, countyIds_Candidate)).Polygonal2DsByCountyId();
            }

            Dictionary<int, List<OrtoDatas>> dictionary_OrtoDatas = [];

            foreach (OrtoDatas ortoDatas_Temp in ortoDatas)
            {
                if (ortoDatas_Temp is null)
                {
                    // Recorded rather than skipped in silence, so the rejection count stays an exact
                    // account of the rows that never reached the database.
                    rejections.Add(new Rejection(null, Enums.UpdateRejectionReason.Undefined));
                    continue;
                }

                int? countyId = ortoDatas_Temp.CountyId;

                if (countyId is null || !countyId.HasValue)
                {
                    if (countyIds_Candidate.Count == 1)
                    {
                        countyId = countyIds_Candidate[0];
                    }
                    else
                    {
                        BoundingBox2D? boundingBox2D = ortoDatas_Temp.BoundingBox2D;
                        if (boundingBox2D is null)
                        {
                            // No county was named and there is no geometry to infer one from, so resolution
                            // never even starts. A defect in the posted payload, unlike the tiers below.
                            rejections.Add(new Rejection(ortoDatas_Temp.Reference, Enums.UpdateRejectionReason.MissingGeometry));
                            continue;
                        }

                        Geometry.Planar.Interfaces.IPolygonal2D? polygonal2D = (Polygon2D?)boundingBox2D;

                        if (polygonal2Ds_ByCountyId is not null)
                        {
                            countyId = polygonal2Ds_ByCountyId.CountyId(polygonal2D, tolerance);
                        }
                        else
                        {
                            // The bounding box path answers a different candidate set per entry, so there is
                            // nothing to cache across them.
                            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, Enums.AdministrativeArealType.County, tolerance);
                            if (administrativeAreal2Ds is not null && administrativeAreal2Ds.Count != 0)
                            {
                                // A single overlapping county is taken as it stands: the bounding box search
                                // already proved the overlap from the stored extents, so a row whose geometry
                                // cannot be deserialized must not cost the entry its only candidate.
                                countyId = administrativeAreal2Ds.Count == 1 ? administrativeAreal2Ds[0].Id : administrativeAreal2Ds.CountyId(polygonal2D, tolerance);
                            }
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
                    // Resolution ran and named no part. The row is dropped, and naming it here is the only
                    // place that can - nothing downstream knows which rows never made the batch.
                    rejections.Add(new Rejection(ortoDatas_Temp.Reference, Enums.UpdateRejectionReason.CountyUnresolved));
                    continue;
                }

                if (!dictionary_OrtoDatas.TryGetValue(countyId.Value, out List<OrtoDatas>? OrtoDatas_CountyId) || OrtoDatas_CountyId is null)
                {
                    OrtoDatas_CountyId = [];
                    dictionary_OrtoDatas[countyId.Value] = OrtoDatas_CountyId;
                }

                OrtoDatas_CountyId.Add(ortoDatas_Temp);
            }

            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (KeyValuePair<int, List<OrtoDatas>> keyValuePair in dictionary_OrtoDatas)
            {
                int countyId = keyValuePair.Key;

                succeded = await Create.TableAsync_OrtoDatas_Partition(npgsqlConnection, countyId);
                if (!succeded)
                {
                    // A whole county's worth of rows disappears here, and it is the one drop that is never
                    // the caller's doing.
                    foreach (OrtoDatas ortoDatas_Rejected in keyValuePair.Value)
                    {
                        rejections.Add(new Rejection(ortoDatas_Rejected.Reference, Enums.UpdateRejectionReason.PartitionUnavailable));
                    }

                    continue;
                }

                foreach (OrtoDatas ortoDatas_Temp in keyValuePair.Value)
                {
                    // SQL with full update on conflict (excluding ID)
                    NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO {TableName.OrtoDatas} (county_id, reference, min_x, min_y, max_x, max_y, subdivision_id, object)
                    VALUES (@county_id, @reference, @min_x, @min_y, @max_x, @max_y, @subdivision_id, @object)
                    ON CONFLICT (reference, county_id)
                    DO UPDATE SET
                        min_x = EXCLUDED.min_x,
                        min_y = EXCLUDED.min_y,
                        max_x = EXCLUDED.max_x,
                        max_y = EXCLUDED.max_y,
                        subdivision_id = EXCLUDED.subdivision_id,
                        object = EXCLUDED.object
                    RETURNING id;");

                    BoundingBox2D? boundingBox2D = ortoDatas_Temp.BoundingBox2D;

                    // Adding parameters with explicit NpgsqlDbType
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = countyId });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("reference", NpgsqlDbType.Text) { Value = ortoDatas_Temp.Reference });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.X });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.Y });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.X });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.Y });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("subdivision_id", NpgsqlDbType.Integer) { Value = (object?)ortoDatas_Temp.SubdivisionId ?? DBNull.Value });

                    // Handling potential null for JSONB object
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                    {
                        Value = (object?)ortoDatas_Temp.Object?.ToJsonString() ?? DBNull.Value
                    });

                    npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
                }
            }

            // Every row was dropped, so there is nothing to execute. Npgsql 10.0.2 answers a zero-command
            // batch with an empty reader rather than throwing, which is precisely why the all-dropped case
            // used to be indistinguishable from an unreachable database: both returned an empty id set. The
            // rejections tell them apart now, and the round trip buys nothing.
            if (npgsqlBatch.BatchCommands.Count == 0)
            {
                return new PostgreSQLUpdateResult(ids, rejections);
            }

            // Execute batch and collect IDs
            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync();

            do
            {
                while (await npgsqlDataReader.ReadAsync())
                {
                    // The RETURNING id works for both INSERT and UPDATE cases
                    long id = npgsqlDataReader.GetInt64(0);
                    ids.Add(id);
                }
            }
            while (await npgsqlDataReader.NextResultAsync());

            return new PostgreSQLUpdateResult(ids, rejections);
        }

        /// <summary>
        /// Asynchronously updates the specified collection of building 2D references.
        /// </summary>
        /// <param name="building2DReferences">An <see cref="IEnumerable{Building2DReference}"/> containing the building 2D references to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> of the updated references, or null if the operation failed or the input collection was null.</returns>
        public async Task<List<Building2DReference>?> UpdateBuilding2DReferencesAsync(IEnumerable<Building2DReference> building2DReferences, CancellationToken cancellationToken = default)
        {
            // Ensure we have a valid connection
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // Check if table exists before proceeding
            bool created = await Create.TableAsync_Building2DReference(npgsqlConnection, TableName.OrtoDatas_Building2DReference_Update, cancellationToken);
            if (!created)
            {
                return null;
            }

            // ON CONFLICT (county_id, reference) DO NOTHING ensures we don't add duplicates.
            // RETURNING * will only return rows that were actually inserted.
            string sql = $@"
                INSERT INTO {TableName.OrtoDatas_Building2DReference_Update} (county_id, reference, subdivision_id)
                SELECT * FROM UNNEST(@counties, @refs, @subs)
                ON CONFLICT (county_id, reference) DO NOTHING
                RETURNING id, county_id, reference, subdivision_id;";

            List<Building2DReference> result = [];

            try
            {
                await using NpgsqlCommand command = new(sql, npgsqlConnection);

                // Preparing arrays for PostgreSQL UNNEST to avoid multiple INSERT calls (optimization)
                int[] countyIds = [.. building2DReferences.Select(x => x.CountyId ?? 0)];
                string[] references = [.. building2DReferences.Select(x => x.Reference ?? string.Empty)];
                int?[] subdivisionIds = [.. building2DReferences.Select(x => x.SubdivisionId)];

                command.Parameters.AddWithValue("counties", countyIds);
                command.Parameters.AddWithValue("refs", references);
                command.Parameters.AddWithValue("subs", subdivisionIds);

                await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    Building2DReference building2DReference = new()
                    {
                        Id = reader.GetInt64(reader.GetOrdinal("id")),
                        CountyId = reader.IsDBNull(reader.GetOrdinal("county_id")) ? null : reader.GetInt32(reader.GetOrdinal("county_id")),
                        Reference = reader.IsDBNull(reader.GetOrdinal("reference")) ? null : reader.GetString(reader.GetOrdinal("reference")),
                        SubdivisionId = reader.IsDBNull(reader.GetOrdinal("subdivision_id")) ? null : reader.GetInt32(reader.GetOrdinal("subdivision_id"))
                    };

                    result.Add(building2DReference);
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Database error during {nameof(UpdateBuilding2DReferencesAsync)}: {ex.Message}");
                throw;
            }

            return result;
        }

        /// <summary>
        /// Asynchronously updates the subdivision identifiers for the specified collection of building 2D references.
        /// </summary>
        /// <param name="building2DReferences">An <see cref="IEnumerable{Building2DReference}"/> containing the building 2D references to be updated, or null.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search across partitions by reference alone for references whose county identifier was mismatched.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> of the updated references, or null if the input collection was null or an error occurred.</returns>
        public async Task<List<Building2DReference>?> UpdateSubdivisionIds(IEnumerable<Building2DReference>? building2DReferences, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            if (building2DReferences == null)
            {
                return null;
            }

            List<Building2DReference> building2DReferences_List = [.. building2DReferences];
            if (building2DReferences_List.Count == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // Rozdzielamy dane na te, które mają CountyId i te, które go nie mają
            List<Building2DReference> withCounty = [.. building2DReferences_List.Where(x => x.CountyId.HasValue)];
            List<Building2DReference> withoutCounty = [.. building2DReferences_List.Where(x => !x.CountyId.HasValue)];

            // Jeśli brakuje CountyId, musimy je dociągnąć z bazy danych przed aktualizacją
            if (withoutCounty.Count > 0)
            {
                string[] missingRefs = [.. withoutCounty.Select(x => x.Reference ?? string.Empty)];

                string findSql = $@"
                    SELECT reference, county_id
                    FROM {TableName.OrtoDatas}
                    WHERE reference = ANY(@refs)";

                await using NpgsqlCommand npgsqlCommand = new(findSql, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("refs", missingRefs);

                await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
                while (await npgsqlDataReader.ReadAsync(cancellationToken))
                {
                    string @ref = npgsqlDataReader.GetString(0);
                    int cid = npgsqlDataReader.GetInt32(1);

                    // Uzupełniamy brakujące ID w obiektach wejściowych
                    Building2DReference? match = withoutCounty.FirstOrDefault(x => x.Reference == @ref);
                    if (match != null)
                    {
                        match.CountyId = cid;
                        withCounty.Add(match);
                    }
                }
                await npgsqlDataReader.CloseAsync();
            }

            if (withCounty.Count == 0)
            {
                return [];
            }

            // Teraz wykonujemy właściwy UPDATE na kompletnych danych
            string updateSql = $@"
                UPDATE {TableName.OrtoDatas} u
                SET subdivision_id = data.new_sub_id
                FROM (
                    SELECT * FROM UNNEST(@counties, @refs, @subs) AS t(c_id, r_text, new_sub_id)
                ) AS data
                WHERE u.county_id = data.c_id  -- To gwarantuje Partition Pruning
                  AND u.reference = data.r_text
                RETURNING u.id, u.county_id, u.reference, u.subdivision_id;";

            List<Building2DReference> result = [];

            try
            {
                await using NpgsqlCommand command = new(updateSql, npgsqlConnection);

                command.Parameters.AddWithValue("counties", withCounty.Select(x => x.CountyId ?? 0).ToArray());
                command.Parameters.AddWithValue("refs", withCounty.Select(x => x.Reference ?? string.Empty).ToArray());
                command.Parameters.AddWithValue("subs", withCounty.Select(x => x.SubdivisionId).ToArray());

                await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    result.Add(new Building2DReference
                    {
                        Id = reader.GetInt64(0),
                        CountyId = reader.GetInt32(1),
                        Reference = reader.GetString(2),
                        SubdivisionId = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3)
                    });
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Database error in UpdateSubdivisionIds: {ex.Message}");
                throw;
            }

            if (fallbackByReference)
            {
                HashSet<string> updatedRefs = [.. result.Where(r => !string.IsNullOrWhiteSpace(r.Reference)).Select(r => r.Reference!)];
                List<Building2DReference> remaining = [.. withCounty.Where(b => !string.IsNullOrWhiteSpace(b.Reference) && !updatedRefs.Contains(b.Reference!))];

                if (remaining.Count > 0)
                {
                    string[] remainingRefs = [.. remaining.Select(b => b.Reference!)];

                    string fallbackFindSql = $@"
                        SELECT reference, county_id
                        FROM {TableName.OrtoDatas}
                        WHERE reference = ANY(@refs)";

                    List<Building2DReference> fallbackResolved = [];

                    await using (NpgsqlCommand fallbackFindCmd = new(fallbackFindSql, npgsqlConnection))
                    {
                        fallbackFindCmd.Parameters.AddWithValue("refs", remainingRefs);

                        await using NpgsqlDataReader fallbackFindReader = await fallbackFindCmd.ExecuteReaderAsync(cancellationToken);
                        while (await fallbackFindReader.ReadAsync(cancellationToken))
                        {
                            string @ref = fallbackFindReader.GetString(0);
                            int cid = fallbackFindReader.GetInt32(1);

                            Building2DReference? match = remaining.FirstOrDefault(x => x.Reference == @ref);
                            if (match != null)
                            {
                                fallbackResolved.Add(new Building2DReference
                                {
                                    Reference = match.Reference,
                                    CountyId = cid,
                                    SubdivisionId = match.SubdivisionId
                                });
                            }
                        }
                    }

                    if (fallbackResolved.Count > 0)
                    {
                        await using NpgsqlCommand fallbackUpdateCmd = new(updateSql, npgsqlConnection);

                        fallbackUpdateCmd.Parameters.AddWithValue("counties", fallbackResolved.Select(x => x.CountyId ?? 0).ToArray());
                        fallbackUpdateCmd.Parameters.AddWithValue("refs", fallbackResolved.Select(x => x.Reference ?? string.Empty).ToArray());
                        fallbackUpdateCmd.Parameters.AddWithValue("subs", fallbackResolved.Select(x => x.SubdivisionId).ToArray());

                        await using NpgsqlDataReader fallbackUpdateReader = await fallbackUpdateCmd.ExecuteReaderAsync(cancellationToken);
                        while (await fallbackUpdateReader.ReadAsync(cancellationToken))
                        {
                            result.Add(new Building2DReference
                            {
                                Id = fallbackUpdateReader.GetInt64(0),
                                CountyId = fallbackUpdateReader.GetInt32(1),
                                Reference = fallbackUpdateReader.GetString(2),
                                SubdivisionId = fallbackUpdateReader.IsDBNull(3) ? null : (int?)fallbackUpdateReader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return result;
        }

        private static OrtoDatas Create_OrtoDatas(NpgsqlDataReader npgsqlDataReader)
        {
            return new OrtoDatas
            {
                Id = npgsqlDataReader.GetInt64(0),
                CountyId = npgsqlDataReader.IsDBNull(1) ? null : (int?)npgsqlDataReader.GetInt32(1),
                Reference = npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                BoundingBox2D = new BoundingBox2D(
                        new Point2D(npgsqlDataReader.IsDBNull(3) ? double.NaN : npgsqlDataReader.GetDouble(3),
                                    npgsqlDataReader.IsDBNull(4) ? double.NaN : npgsqlDataReader.GetDouble(4)),
                        new Point2D(npgsqlDataReader.IsDBNull(5) ? double.NaN : npgsqlDataReader.GetDouble(5),
                                    npgsqlDataReader.IsDBNull(6) ? double.NaN : npgsqlDataReader.GetDouble(6))),
                SubdivisionId = npgsqlDataReader.IsDBNull(7) ? null : (int?)npgsqlDataReader.GetInt32(7),
                Object = npgsqlDataReader.IsDBNull(8) ? null : JsonNode.Parse(npgsqlDataReader.GetString(8)) as JsonObject,
                CreatedAt = npgsqlDataReader.IsDBNull(9) ? null : (DateTime?)npgsqlDataReader.GetDateTime(9)
            };
        }

        private static async Task<List<OrtoDatas>> ReadAsync_OrtoDatas(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<OrtoDatas> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_OrtoDatas(npgsqlDataReader));
            }

            return result;
        }

        private static async Task<List<OrtoDatas>?> ReadAsync_OrtoDatas(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync_OrtoDatas(npgsqlDataReader, cancellationToken);
        }
    }
}