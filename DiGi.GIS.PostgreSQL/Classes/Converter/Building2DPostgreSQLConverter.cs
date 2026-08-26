using DiGi.Core;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.PostgreSQL.Enums;
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
    /// Provides functionality for converting and managing <see cref="Building2D"/> entities within a PostgreSQL database, implementing the <see cref="IGISPostgreSQLConverter{T}"/> interface.
    /// <para><b><c>county_id</c> is a polygon part, not a county.</b> It points at an <c>administrative_areal_2d</c> row, and a county whose territory is disconnected has one such row per part - so the buildings of a single county can be spread over several <c>county_id</c> values, and querying one part never returns the whole county. See <see cref="AdministrativeAreal2DPostgreSQLConverter"/> for the storage model.</para>
    /// <para><b><c>reference</c> assures row uniqueness.</b> Each building reference uniquely identifies a <see cref="Building2D"/> record in the database. <c>county_id</c> can be used to scope queries to a specific partition for performance optimization (partition pruning), but is not required for uniqueness. Lookups keyed on reference alone or using fallback search by reference are safe and reliable.</para>
    /// </summary>
    public class Building2DPostgreSQLConverter : PostgreSQLConverter<Building2D>, IGISPostgreSQLConverter<Building2D>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData"/> containing the connection settings for the PostgreSQL database, or <see langword="null"/>.</param>
        public Building2DPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Asynchronously counts the number of 2D buildings for a specified county, with optional filtering by subdivision identifiers.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> used to connect to the PostgreSQL database.</param>
        /// <param name="countyId">The integer identifier of the county (Partition Key).</param>
        /// <param name="subdivisionIds">An optional collection of integers representing the subdivision identifiers to filter the results.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a long integer, or -1 if the connection is null.</returns>
        public static async Task<long> CountAsync(NpgsqlConnection? npgsqlConnection, int countyId, IEnumerable<int>? subdivisionIds = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection == null)
            {
                return -1;
            }

            // Check early if cancellation was already requested
            cancellationToken.ThrowIfCancellationRequested();

            bool hasSubdivisionIds = subdivisionIds != null && subdivisionIds.Any();

            // Build the base query using the mandatory county_id parameter
            string commandText = $@"
                SELECT COUNT(*)
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @countyId";

            // Dynamically append the subdivision_id filter if the collection has elements
            if (hasSubdivisionIds)
            {
                commandText += " AND (subdivision_id = ANY(@subdivisionIds) OR subdivision_id IS NULL)";
            }

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });

            if (hasSubdivisionIds)
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivisionIds", NpgsqlDbType.Array | NpgsqlDbType.Integer)
                {
                    Value = subdivisionIds!.ToArray()
                });
            }

            object? executeResult = await npgsqlCommand.ExecuteScalarAsync(cancellationToken);

            if (executeResult != null && executeResult != DBNull.Value)
            {
                return System.Convert.ToInt64(executeResult);
            }

            return 0;
        }

        /// <summary>
        /// Asynchronously retrieves a list of 2D building references for a specified county, with optional filtering by subdivision identifiers.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> used to connect to the PostgreSQL database.</param>
        /// <param name="countyId">The integer identifier of the county.</param>
        /// <param name="subdivisionIds">An optional collection of integers representing the subdivision identifiers to filter the results.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2DReference" /> objects, or null if no references are found or an error occurs.</returns>
        public static async Task<List<Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(NpgsqlConnection? npgsqlConnection, int countyId, IEnumerable<int>? subdivisionIds = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            int[]? subdivisionIds_Array = subdivisionIds?.ToArray();
            bool hasSubdivisionIds = subdivisionIds_Array != null && subdivisionIds_Array.Length > 0;

            string commandText = $@"
                SELECT id, county_id, reference, subdivision_id
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @county_id{(hasSubdivisionIds ? " AND (subdivision_id = ANY(@subdivision_ids) OR subdivision_id IS NULL)" : "")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = countyId });
            if (hasSubdivisionIds)
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivision_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = subdivisionIds_Array });
            }

            return await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of 2D buildings for a specified county, with optional filtering by subdivision identifiers.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> used to connect to the PostgreSQL database.</param>
        /// <param name="countyId">The integer identifier of the county.</param>
        /// <param name="subdivisionIds">An optional collection of integers representing the subdivision identifiers to filter the results.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D" /> objects, or null if no buildings are found or the connection is null.</returns>
        public static async Task<List<Building2D>?> GetBuilding2DsByCountyIdAsync(NpgsqlConnection? npgsqlConnection, int countyId, IEnumerable<int>? subdivisionIds = null, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            cancellationToken.ThrowIfCancellationRequested();

            int[]? subdivisionIds_Array = subdivisionIds?.ToArray();
            bool hasSubdivisionIds = subdivisionIds_Array != null && subdivisionIds_Array.Length > 0;

            string commandText = $@"
                SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @county_id{(hasSubdivisionIds ? " AND (subdivision_id = ANY(@subdivision_ids) OR subdivision_id IS NULL)" : "")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = countyId });
            if (hasSubdivisionIds)
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivision_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = subdivisionIds_Array });
            }

            return await ReadAsync_Building2D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the total count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the results.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other methods as part of cooperating cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a long integer.</returns>
        public static async Task<long> GetCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string tableName = Constants.TableName.Building2D;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, tableName, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated row count from the database, optionally filtered by a specific county identifier and with an optional statistics update.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier of the county for which the count is estimated; if null, the estimate may be calculated across all counties.</param>
        /// <param name="analyze">A boolean value indicating whether to perform an ANALYZE operation on the table before retrieving the count to ensure statistics are current.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated row count as a long integer.</returns>
        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string tableName = Constants.TableName.Building2D;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, tableName, analyze, cancellationToken);
        }

        /// <summary>
        /// Asynchronously gets the estimated row count across specified counties in a PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection" /> to use for the query.</param>
        /// <param name="countyIds">A collection of integers representing the county identifiers.</param>
        /// <param name="analyze">A boolean indicating whether to run an analysis before fetching the estimated count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated total row count as a long, or -1 if an error occurs.</returns>
        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int> countyIds, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            if (countyIds is null)
            {
                return await GetEstimatedCountAsync(npgsqlConnection, (int?)null, analyze, cancellationToken);
            }

            long result = 0;

            foreach (int countyId in countyIds)
            {
                string tableName = string.Format("{0}_{1}", Constants.TableName.Building2D, countyId);
                result += await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, tableName, analyze, cancellationToken);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Point2D"/> objects associated with the specified references and an optional county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to connect to the database.</param>
        /// <param name="references">An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the references to query.</param>
        /// <param name="countyId">An optional <see cref="int"/> representing the county identifier for filtering results.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone for any references not found in the initial search.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="Point2D"/> objects if matches are found; otherwise, null.</returns>
        public static async Task<List<Point2D>?> GetPoint2DsByReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? references, int? countyId, bool fallbackByReference = false, CancellationToken cancellationToken = default)
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

            string commandText = $@"
                    SELECT min_x, min_y, max_x, max_y, reference
                    FROM {Constants.TableName.Building2D}
                    WHERE reference = ANY(@references){(countyId is null ? "" : " AND county_id = @countyId")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("references", references_Array);
            if (countyId is not null)
            {
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId.Value);
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            List<Point2D> result = [];
            HashSet<string> foundReferences = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                double min_x = npgsqlDataReader.IsDBNull(0) ? double.NaN : npgsqlDataReader.GetDouble(0);
                if (double.IsNaN(min_x))
                {
                    continue;
                }

                double min_y = npgsqlDataReader.IsDBNull(1) ? double.NaN : npgsqlDataReader.GetDouble(1);
                if (double.IsNaN(min_y))
                {
                    continue;
                }

                double max_x = npgsqlDataReader.IsDBNull(2) ? double.NaN : npgsqlDataReader.GetDouble(2);
                if (double.IsNaN(max_x))
                {
                    continue;
                }

                double max_y = npgsqlDataReader.IsDBNull(3) ? double.NaN : npgsqlDataReader.GetDouble(3);
                if (double.IsNaN(max_y))
                {
                    continue;
                }

                if (!npgsqlDataReader.IsDBNull(4))
                {
                    foundReferences.Add(npgsqlDataReader.GetString(4));
                }

                result.Add(new Point2D((min_x + max_x) / 2, (min_y + max_y) / 2));
            }

            if (fallbackByReference && countyId is not null)
            {
                string[] missingReferences = [.. references_Array.Where(r => !foundReferences.Contains(r))];
                if (missingReferences.Length > 0)
                {
                    List<Point2D>? fallbackPoints = await GetPoint2DsByReferencesAsync(npgsqlConnection, missingReferences, null, false, cancellationToken);
                    if (fallbackPoints is not null && fallbackPoints.Count > 0)
                    {
                        result.AddRange(fallbackPoints);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves full building data based on a collection of references using optimized partition-pruned batching.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the PostgreSQL database.</param>
        /// <param name="building2DReferences">The collection of <see cref="Building2DReference"/> objects used to identify and retrieve the corresponding buildings from the database.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform an additional fallback query matching by reference only (without county identifier condition) for any references not found in the initial search.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects matching the provided references, an empty list if none match, or null if the input collection was null or the connection is null.</returns>
        public static async Task<List<Building2D>?> GetBuilding2DsByBuilding2DReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<Building2DReference>? building2DReferences, bool fallbackByReference = false, int commandTimeout = 30, CancellationToken cancellationToken = default)
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

            const int batchSize = 1000;
            List<Building2D> result = [];
            HashSet<long> existingIds = [];

            // 1. Group references with a known county ID for partition pruning
            IEnumerable<IGrouping<int, Building2DReference>> countyGroupings = building2DReferences_List
                .Where(r => r?.CountyId != null && !string.IsNullOrWhiteSpace(r.Reference))
                .GroupBy(r => r.CountyId!.Value);

            foreach (IGrouping<int, Building2DReference> countyGroup in countyGroupings)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int countyId = countyGroup.Key;
                string[] uniqueRefsInCounty = [.. countyGroup.Select(r => r.Reference!).Distinct()];

                for (int i = 0; i < uniqueRefsInCounty.Length; i += batchSize)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string[] chunk = uniqueRefsInCounty.Skip(i).Take(batchSize).ToArray();

                    const string commandText = $@"
                        SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                        FROM {Constants.TableName.Building2D}
                        WHERE county_id = @countyId AND reference = ANY(@references);";

                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                    npgsqlCommand.CommandTimeout = commandTimeout;
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("references", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = chunk });

                    List<Building2D>? chunkResults = await ReadAsync_Building2D(npgsqlCommand, cancellationToken);
                    if (chunkResults != null)
                    {
                        foreach (Building2D item in chunkResults)
                        {
                            if (existingIds.Add(item.Id))
                            {
                                result.Add(item);
                            }
                        }
                    }
                }
            }

            // 2. References with no CountyId specified
            string[] unassignedRefs = [.. building2DReferences_List
                .Where(r => r?.CountyId == null && !string.IsNullOrWhiteSpace(r?.Reference))
                .Select(r => r!.Reference!)
                .Distinct()];

            if (unassignedRefs.Length > 0)
            {
                for (int i = 0; i < unassignedRefs.Length; i += batchSize)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string[] chunk = unassignedRefs.Skip(i).Take(batchSize).ToArray();

                    const string commandText = $@"
                        SELECT DISTINCT ON (reference)
                               id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                        FROM {Constants.TableName.Building2D}
                        WHERE reference = ANY(@references)
                        ORDER BY reference, id ASC;";

                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                    npgsqlCommand.CommandTimeout = commandTimeout;
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("references", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = chunk });

                    List<Building2D>? chunkResults = await ReadAsync_Building2D(npgsqlCommand, cancellationToken);
                    if (chunkResults != null)
                    {
                        foreach (Building2D item in chunkResults)
                        {
                            if (existingIds.Add(item.Id))
                            {
                                result.Add(item);
                            }
                        }
                    }
                }
            }

            // 3. Fallback for missing references if requested
            if (fallbackByReference)
            {
                HashSet<string> foundCountyReferencePairs = [.. result.Where(b => !string.IsNullOrWhiteSpace(b.Reference) && b.CountyId.HasValue).Select(b => $"{b.CountyId!.Value}_{b.Reference}")];
                HashSet<string> foundReferences = [.. result.Where(b => !string.IsNullOrWhiteSpace(b.Reference)).Select(b => b.Reference!)];

                HashSet<string> missingReferences = [];
                foreach (Building2DReference building2DReference in building2DReferences_List)
                {
                    if (string.IsNullOrWhiteSpace(building2DReference?.Reference))
                    {
                        continue;
                    }

                    bool found = building2DReference.CountyId.HasValue
                        ? foundCountyReferencePairs.Contains($"{building2DReference.CountyId.Value}_{building2DReference.Reference}")
                        : foundReferences.Contains(building2DReference.Reference);

                    if (!found)
                    {
                        missingReferences.Add(building2DReference.Reference);
                    }
                }

                if (missingReferences.Count > 0)
                {
                    string[] missingRefs_Array = [.. missingReferences];
                    for (int i = 0; i < missingRefs_Array.Length; i += batchSize)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string[] chunk = missingRefs_Array.Skip(i).Take(batchSize).ToArray();

                        const string fallbackCommandText = $@"
                            SELECT DISTINCT ON (reference)
                                   id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                            FROM {Constants.TableName.Building2D}
                            WHERE reference = ANY(@missingRefs)
                            ORDER BY reference, id ASC;";

                        await using NpgsqlCommand fallbackCommand = new(fallbackCommandText, npgsqlConnection);
                        fallbackCommand.CommandTimeout = commandTimeout;
                        fallbackCommand.Parameters.Add(new NpgsqlParameter("missingRefs", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = chunk });

                        List<Building2D>? fallbackResults = await ReadAsync_Building2D(fallbackCommand, cancellationToken);
                        if (fallbackResults != null)
                        {
                            foreach (Building2D item in fallbackResults)
                            {
                                if (existingIds.Add(item.Id))
                                {
                                    result.Add(item);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Asynchronously clears all data from the specified table and restarts its identity sequence.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result is a <see cref="bool"/> indicating true if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> ClearAsync()
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync();

            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, Constants.TableName.Building2D);
        }

        /// <summary>
        /// Asynchronously counts the number of 2D buildings for a specified county, with optional filtering by subdivision identifiers.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county (Partition Key).</param>
        /// <param name="subdivisionIds">An optional collection of integers representing the subdivision identifiers to filter the results.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a long integer, or -1 if the connection is null.</returns>
        public async Task<long> CountAsync(int countyId, IEnumerable<int>? subdivisionIds = null, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await CountAsync(npgsqlConnection, countyId, subdivisionIds, cancellationToken);
        }

        /// <summary>
        /// Asynchronously counts the number of 2D buildings for a specified administrative areal 2D identifiers.
        /// </summary>
        /// <param name="administrativeAreal2DIds">A collection of integers representing the administrative areal 2D identifiers to filter by.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>Number of buidlings in the specified administrative areas.</returns>
        public async Task<long> CountAsync(IEnumerable<int> administrativeAreal2DIds, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, administrativeAreal2DIds, cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return -1;
            }

            administrativeAreal2DReferences.Filter(x => x?.AdministrativeArealType == AdministrativeArealType.Subdivision, out administrativeAreal2DReferences, out List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Out);

            administrativeAreal2DReferences ??= [];

            if (administrativeAreal2DReferences_Out is not null && administrativeAreal2DReferences_Out.Count != 0)
            {
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Out in administrativeAreal2DReferences_Out)
                {
                    List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(npgsqlConnection, AdministrativeArealType.Subdivision, administrativeAreal2DReference_Out.Id, false, cancellationToken: cancellationToken);
                    if (administrativeAreal2DReferences_Temp is not null)
                    {
                        administrativeAreal2DReferences.AddRange(administrativeAreal2DReferences_Temp);
                    }
                }
            }

            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return 0;
            }

            long result = 0;
            while (administrativeAreal2DReferences is not null && administrativeAreal2DReferences.Count > 0)
            {
                int? countyId = administrativeAreal2DReferences[0]?.CountyId;

                administrativeAreal2DReferences.Filter(x => x?.CountyId == countyId, out List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_CountyId, out administrativeAreal2DReferences);

                if (administrativeAreal2DReferences_CountyId is null || administrativeAreal2DReferences_CountyId.Count == 0)
                {
                    break;
                }

                if (countyId is null || !countyId.HasValue)
                {
                    continue;
                }

                long count = await CountAsync(npgsqlConnection, countyId.Value, administrativeAreal2DReferences_CountyId.ConvertAll(x => x.Id).Distinct(), cancellationToken);
                if (count > 0)
                {
                    result += count;
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously creates the table in the database.
        /// </summary>
        /// <param name="commandTimeout">The time interval, in seconds, to wait for the command to complete before timing out. The default value is 30 seconds.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the table was successfully created.</returns>
        public async Task<bool> CreateTableAsync(int commandTimeout = 30)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync();

            bool result = await Create.TableAsync_Building2D(npgsqlConnection, commandTimeout);
            if (result)
            {
                await DiGi.PostgreSQL.Modify.Analyze(npgsqlConnection, Constants.TableName.Building2D, commandTimeout);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="Building2D"/> instance by its unique identifier and an optional county identifier.
        /// </summary>
        /// <param name="id">The long unique identifier of the building to retrieve.</param>
        /// <param name="countyId">The optional integer identifier of the county used to scope the search.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Building2D"/> instance if found; otherwise, null.</returns>
        public async Task<Building2D?> GetBuilding2DByIdAsync(long id, int? countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                    SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                    FROM {Constants.TableName.Building2D}
                    WHERE id = @id{(countyId is null ? "" : " AND county_id = @countyId")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("id", id);
            if (countyId is not null)
            {
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId.Value);
            }

            List<Building2D>? results = await ReadAsync_Building2D(npgsqlCommand, cancellationToken);

            return results?.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="Building2D"/> located at or near the specified 2D point within a given tolerance.
        /// </summary>
        /// <param name="point2D">The <see cref="Point2D"/> coordinate to search for. This value can be null.</param>
        /// <param name="tolerance">The <see cref="double"/> distance tolerance used to determine if a building is associated with the given point. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Building2D"/> found at the specified location, or null if no building is found within the tolerance or if the provided point is null.</returns>
        public async Task<Building2D?> GetBuilding2DByPoint2DAsync(Point2D? point2D, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (point2D is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            BoundingBox2D boundingBox2D = new(point2D, point2D);

            // 1. First attempt: search within matching subdivisions
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, AdministrativeArealType.Subdivision, tolerance, cancellationToken);

            Building2D? building2D = await FindBuilding2DAsync(administrativeAreal2Ds);
            if (building2D is not null)
            {
                return building2D;
            }

            // 2. Fallback attempt: search within matching county partitions if no subdivision matched or contained the building
            administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, AdministrativeArealType.County, tolerance, cancellationToken);

            return await FindBuilding2DAsync(administrativeAreal2Ds);

            async Task<Building2D?> FindBuilding2DAsync(List<AdministrativeAreal2D>? administrativeAreal2Ds_Temp)
            {
                if (administrativeAreal2Ds_Temp is null || administrativeAreal2Ds_Temp.Count == 0)
                {
                    return null;
                }

                // Represent the point as a tolerance-sized search box so the GiST index on
                // box(point(min_x, min_y), point(max_x, max_y)) can serve the '&&' overlap operator.
                double searchMinX = point2D.X - tolerance;
                double searchMinY = point2D.Y - tolerance;
                double searchMaxX = point2D.X + tolerance;
                double searchMaxY = point2D.Y + tolerance;

                foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds_Temp)
                {
                    if (administrativeAreal2D is null)
                    {
                        continue;
                    }

                    int? countyId = administrativeAreal2D.AdministrativeArealType == AdministrativeArealType.County
                        ? administrativeAreal2D.Id
                        : administrativeAreal2D.CountyId;

                    if (countyId is null)
                    {
                        continue;
                    }

                    int? subdivisionId = administrativeAreal2D.AdministrativeArealType == AdministrativeArealType.Subdivision
                        ? administrativeAreal2D.Id
                        : null;

                    string commandText = $@"
                        SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                        FROM {Constants.TableName.Building2D}
                        WHERE county_id = @countyId
                            {(subdivisionId.HasValue ? "AND (subdivision_id = @subdivisionId OR subdivision_id IS NULL)" : "")}
                            AND box(point(min_x, min_y), point(max_x, max_y)) && box(point(@searchMinX, @searchMinY), point(@searchMaxX, @searchMaxY))
                        ORDER BY id ASC;";

                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId.Value });
                    if (subdivisionId.HasValue)
                    {
                        npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivisionId", NpgsqlDbType.Integer) { Value = subdivisionId.Value });
                    }
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMinX", NpgsqlDbType.Double) { Value = searchMinX });
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMinY", NpgsqlDbType.Double) { Value = searchMinY });
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMaxX", NpgsqlDbType.Double) { Value = searchMaxX });
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMaxY", NpgsqlDbType.Double) { Value = searchMaxY });

                    List<Building2D>? results = await ReadAsync_Building2D(npgsqlCommand, cancellationToken);
                    if (results is null || results.Count == 0)
                    {
                        continue;
                    }

                    foreach (Building2D building2D_Candidate in results)
                    {
                        if (building2D_Candidate.ToDiGi()?.PolygonalFace2D is PolygonalFace2D polygonalFace2D && polygonalFace2D.InRange(point2D, tolerance))
                        {
                            return building2D_Candidate;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="Building2D"/> instance based on the specified reference string and an optional county identifier.
        /// </summary>
        /// <param name="reference">The <see cref="string"/> reference used to identify the building.</param>
        /// <param name="countyId">The optional nullable integer identifier for the county.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone if the building is not found in the specified county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Building2D"/> instance if found; otherwise, null.</returns>
        public async Task<Building2D?> GetBuilding2DByReferenceAsync(string reference, int? countyId, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                    SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                    FROM {Constants.TableName.Building2D}
                    WHERE reference = @reference{(countyId is null ? "" : " AND county_id = @countyId")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("reference", reference);
            if (countyId is not null)
            {
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId.Value);
            }

            List<Building2D>? results = await ReadAsync_Building2D(npgsqlCommand, cancellationToken);

            Building2D? building2D = results?.FirstOrDefault();
            if (building2D is null && countyId is not null && fallbackByReference)
            {
                return await GetBuilding2DByReferenceAsync(reference, null, false, cancellationToken);
            }

            return building2D;
        }

        /// <summary>
        /// Asynchronously retrieves a building 2D reference by its unique identifier and an optional county identifier.
        /// </summary>
        /// <param name="id">The long integer representing the unique identifier of the building.</param>
        /// <param name="countyId">An optional integer representing the county identifier used to filter the search.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Building2DReference"/> if a match is found; otherwise, null.</returns>
        public async Task<Building2DReference?> GetBuilding2DReferenceByIdAsync(long id, int? countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                    SELECT id, county_id, reference, subdivision_id
                    FROM {Constants.TableName.Building2D}
                    WHERE id = @id{(countyId is null ? "" : " AND county_id = @countyId")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("id", id);
            if (countyId is not null)
            {
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId.Value);
            }

            List<Building2DReference>? results = await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);

            return results?.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously retrieves a building 2D reference based on the specified reference string and an optional county identifier.
        /// </summary>
        /// <param name="reference">The unique reference string of the building to retrieve.</param>
        /// <param name="countyId">The optional integer identifier of the county used to filter the search.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone if the reference is not found in the specified county.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="T:Building2DReference" /> if a match is found; otherwise, null.</returns>
        public async Task<Building2DReference?> GetBuilding2DReferenceByReferenceAsync(string reference, int? countyId, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                    SELECT id, county_id, reference, subdivision_id
                    FROM {Constants.TableName.Building2D}
                    WHERE reference = @reference{(countyId is null ? "" : " AND county_id = @countyId")}
                    ORDER BY id ASC;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("reference", reference);
            if (countyId is not null)
            {
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId.Value);
            }

            List<Building2DReference>? results = await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);

            Building2DReference? building2DReference = results?.FirstOrDefault();
            if (building2DReference is null && countyId is not null && fallbackByReference)
            {
                return await GetBuilding2DReferenceByReferenceAsync(reference, null, false, cancellationToken);
            }

            return building2DReference;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects for a specified county identifier, with optional filtering by subdivision and excluded references.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county used to filter the search.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision.</param>
        /// <param name="excludedReferences">Optional collection of references to be excluded from the result.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects if matches are found; otherwise, null.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByCountyIdAsync(int countyId, int? subdivisionId = null, IEnumerable<string>? excludedReferences = null, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @countyId";

            bool hasSubdivisionId = subdivisionId.HasValue;
            if (hasSubdivisionId)
            {
                commandText += " AND subdivision_id = @subdivisionId";
            }

            bool hasExcluded = excludedReferences != null && excludedReferences.Any();

            if (hasExcluded)
            {
                commandText += " AND NOT (reference = ANY(@excluded))";
            }

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            if (hasSubdivisionId)
            {
                npgsqlCommand.Parameters.AddWithValue("subdivisionId", subdivisionId!.Value);
            }

            if (hasExcluded)
            {
                string[] excludedArray = [.. excludedReferences!];
                npgsqlCommand.Parameters.AddWithValue("excluded", excludedArray);
            }

            return await ReadAsync_Building2D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects for a specified county identifier, with optional filtering by subdivision identifiers.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county used to filter the search.</param>
        /// <param name="subdivisionIds">An optional collection of integers representing the subdivision identifiers to filter the results.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects if matches are found; otherwise, null.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByCountyIdAsync(int countyId, IEnumerable<int>? subdivisionIds, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetBuilding2DsByCountyIdAsync(npgsqlConnection, countyId, subdivisionIds, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects for a specified county identifier.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county used to filter the search.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects if matches are found; otherwise, null.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByCountyIdAsync(int countyId, CancellationToken cancellationToken = default)
        {
            return await GetBuilding2DsByCountyIdAsync(countyId, (int?)null, null, 30, cancellationToken);
        }

        /// <summary>
        /// Retrieves full Building2DReference data from building_2d table based on input references.
        /// Performs batch processing using partition-pruned chunked queries.
        /// </summary>
        /// <param name="building2DReferences">Collection of partial references (must have Reference, optional CountyId).</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform an additional fallback query matching by reference only (without county identifier condition) for any references not found in the initial search.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A list of populated Building2DReference objects found in the database.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesAsync(IEnumerable<Building2DReference>? building2DReferences, bool fallbackByReference = false, int commandTimeout = 30, CancellationToken cancellationToken = default)
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

            const int batchSize = 1000;
            List<Building2DReference> result = [];
            HashSet<long> existingIds = [];

            // 1. Group references with a known county ID for partition pruning
            IEnumerable<IGrouping<int, Building2DReference>> countyGroupings = building2DReferences_List
                .Where(r => r?.CountyId != null && !string.IsNullOrWhiteSpace(r.Reference))
                .GroupBy(r => r.CountyId!.Value);

            foreach (IGrouping<int, Building2DReference> countyGroup in countyGroupings)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int countyId = countyGroup.Key;
                string[] uniqueRefsInCounty = [.. countyGroup.Select(r => r.Reference!).Distinct()];

                for (int i = 0; i < uniqueRefsInCounty.Length; i += batchSize)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string[] chunk = uniqueRefsInCounty.Skip(i).Take(batchSize).ToArray();

                    const string commandText = $@"
                        SELECT id, county_id, reference, subdivision_id
                        FROM {Constants.TableName.Building2D}
                        WHERE county_id = @countyId AND reference = ANY(@references);";

                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                    npgsqlCommand.CommandTimeout = commandTimeout;
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("references", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = chunk });

                    List<Building2DReference>? chunkResults = await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);
                    if (chunkResults != null)
                    {
                        foreach (Building2DReference item in chunkResults)
                        {
                            if (existingIds.Add(item.Id))
                            {
                                result.Add(item);
                            }
                        }
                    }
                }
            }

            // 2. References with no CountyId specified
            string[] unassignedRefs = [.. building2DReferences_List
                .Where(r => r?.CountyId == null && !string.IsNullOrWhiteSpace(r?.Reference))
                .Select(r => r!.Reference!)
                .Distinct()];

            if (unassignedRefs.Length > 0)
            {
                for (int i = 0; i < unassignedRefs.Length; i += batchSize)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string[] chunk = unassignedRefs.Skip(i).Take(batchSize).ToArray();

                    const string commandText = $@"
                        SELECT DISTINCT ON (reference)
                               id, county_id, reference, subdivision_id
                        FROM {Constants.TableName.Building2D}
                        WHERE reference = ANY(@references)
                        ORDER BY reference, id ASC;";

                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                    npgsqlCommand.CommandTimeout = commandTimeout;
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("references", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = chunk });

                    List<Building2DReference>? chunkResults = await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);
                    if (chunkResults != null)
                    {
                        foreach (Building2DReference item in chunkResults)
                        {
                            if (existingIds.Add(item.Id))
                            {
                                result.Add(item);
                            }
                        }
                    }
                }
            }

            // 3. Fallback for missing references if requested
            if (fallbackByReference)
            {
                HashSet<string> foundCountyReferencePairs = [.. result.Where(b => !string.IsNullOrWhiteSpace(b.Reference) && b.CountyId.HasValue).Select(b => $"{b.CountyId!.Value}_{b.Reference}")];
                HashSet<string> foundReferences = [.. result.Where(b => !string.IsNullOrWhiteSpace(b.Reference)).Select(b => b.Reference!)];

                HashSet<string> missingReferences = [];
                foreach (Building2DReference building2DReference in building2DReferences_List)
                {
                    if (string.IsNullOrWhiteSpace(building2DReference?.Reference))
                    {
                        continue;
                    }

                    bool found = building2DReference.CountyId.HasValue
                        ? foundCountyReferencePairs.Contains($"{building2DReference.CountyId.Value}_{building2DReference.Reference}")
                        : foundReferences.Contains(building2DReference.Reference);

                    if (!found)
                    {
                        missingReferences.Add(building2DReference.Reference);
                    }
                }

                if (missingReferences.Count > 0)
                {
                    string[] missingRefs_Array = [.. missingReferences];
                    for (int i = 0; i < missingRefs_Array.Length; i += batchSize)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string[] chunk = missingRefs_Array.Skip(i).Take(batchSize).ToArray();

                        const string fallbackCommandText = $@"
                            SELECT DISTINCT ON (reference)
                                   id, county_id, reference, subdivision_id
                            FROM {Constants.TableName.Building2D}
                            WHERE reference = ANY(@missingRefs)
                            ORDER BY reference, id ASC;";

                        await using NpgsqlCommand fallbackCommand = new(fallbackCommandText, npgsqlConnection);
                        fallbackCommand.CommandTimeout = commandTimeout;
                        fallbackCommand.Parameters.Add(new NpgsqlParameter("missingRefs", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = chunk });

                        List<Building2DReference>? fallbackResults = await ReadAsync_Building2DReference(fallbackCommand, cancellationToken);
                        if (fallbackResults != null)
                        {
                            foreach (Building2DReference item in fallbackResults)
                            {
                                if (existingIds.Add(item.Id))
                                {
                                    result.Add(item);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of building 2D references associated with the specified administrative areal 2D identifiers.
        /// <para>Resolution goes through <b>Subdivision children</b>, not geometry: each identifier is expanded to its <see cref="AdministrativeArealType.Subdivision"/> descendants and the buildings are then fetched per <c>county_id</c> plus <c>subdivision_id</c>. An identifier with no subdivisions therefore yields an empty list, which does <b>not</b> mean the area holds no buildings - compare with <c>GetBuilding2DReferencesByCountyIdAsync</c> before concluding anything about coverage.</para>
        /// </summary>
        /// <param name="administrativeAreal2DIds">A collection of integers representing the administrative areal 2D identifiers to filter by.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2DReference"/> objects, or null if no references are found or an error occurs.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync(IEnumerable<int> administrativeAreal2DIds, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, administrativeAreal2DIds, cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return null;
            }

            administrativeAreal2DReferences.Filter(x => x?.AdministrativeArealType == AdministrativeArealType.Subdivision, out administrativeAreal2DReferences, out List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Out);

            administrativeAreal2DReferences ??= [];

            if (administrativeAreal2DReferences_Out is not null && administrativeAreal2DReferences_Out.Count != 0)
            {
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Out in administrativeAreal2DReferences_Out)
                {
                    List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(npgsqlConnection, AdministrativeArealType.Subdivision, administrativeAreal2DReference_Out.Id, false, cancellationToken: cancellationToken);
                    if (administrativeAreal2DReferences_Temp is not null)
                    {
                        administrativeAreal2DReferences.AddRange(administrativeAreal2DReferences_Temp);
                    }
                }
            }

            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return [];
            }

            Dictionary<long, Building2DReference> dictionary = [];
            while (administrativeAreal2DReferences is not null && administrativeAreal2DReferences.Count > 0)
            {
                int? countyId = administrativeAreal2DReferences[0]?.CountyId;

                administrativeAreal2DReferences.Filter(x => x?.CountyId == countyId, out List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_CountyId, out administrativeAreal2DReferences);

                if (administrativeAreal2DReferences_CountyId is null || administrativeAreal2DReferences_CountyId.Count == 0)
                {
                    break;
                }

                if (countyId is null || !countyId.HasValue)
                {
                    continue;
                }

                List<Building2DReference>? building2DReferences = await GetBuilding2DReferencesByCountyIdAsync(npgsqlConnection, countyId.Value, administrativeAreal2DReferences_CountyId.ConvertAll(x => x.Id).Distinct(), cancellationToken);
                if (building2DReferences is not null)
                {
                    foreach (Building2DReference building2DReference in building2DReferences)
                    {
                        dictionary[building2DReference.Id] = building2DReference;
                    }
                }
            }

            return [.. dictionary.Values];
        }

        /// <summary>
        /// Retrieves all Building2DReferences for a specific county, with an optional exclusion list.
        /// Optimized for partitioned tables using the partition key (county_id).
        /// </summary>
        /// <param name="countyId">The ID of the county (Partition Key).</param>
        /// <param name="subdivisionId">The ID of the subdivision.</param>
        /// <param name="excludedReferences">Optional collection of references to be excluded from the result.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of Building2DReference objects, or null if connection fails.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(int countyId, int? subdivisionId = null, IEnumerable<string>? excludedReferences = null, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            // 1. Check early if cancellation was already requested to avoid unnecessary allocations
            cancellationToken.ThrowIfCancellationRequested();

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            // 2. Critical: Pass the token to the connection opening process
            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                SELECT id, county_id, reference, subdivision_id
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @countyId";

            bool hasSubdivisionId = subdivisionId.HasValue;
            if (hasSubdivisionId)
            {
                commandText += " AND subdivision_id = @subdivisionId";
            }

            bool hasExcluded = excludedReferences != null && excludedReferences.Any();

            if (hasExcluded)
            {
                commandText += " AND NOT (reference = ANY(@excluded))";
            }

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            if (hasSubdivisionId)
            {
                npgsqlCommand.Parameters.AddWithValue("subdivisionId", subdivisionId!.Value);
            }

            if (hasExcluded)
            {
                // Using explicit typing and collection expression for performance
                string[] excludedArray = [.. excludedReferences!];
                npgsqlCommand.Parameters.AddWithValue("excluded", excludedArray);
            }

            // 3. Reading loop with token is correct
            return await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a keyset-paginated list of Building2DReference objects for a specified county.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county (Partition Key).</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision.</param>
        /// <param name="lastReference">The last reference string from the previous page, used as the pagination cursor.</param>
        /// <param name="pageSize">The maximum number of references to return in a single page. Defaults to 250.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2DReference"/> objects, or null if connection fails.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(int countyId, int? subdivisionId = null, string? lastReference = null, int pageSize = 250, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // Initialize the base query with the mandatory county_id parameter
            string commandText = $@"
                SELECT id, county_id, reference, subdivision_id
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @countyId";

            // Dynamically append the subdivision_id filter if it was provided
            bool hasSubdivisionId = subdivisionId.HasValue;
            if (hasSubdivisionId)
            {
                commandText += " AND subdivision_id = @subdivisionId";
            }

            // Dynamically append the pagination cursor if it was provided
            bool hasCursor = !string.IsNullOrWhiteSpace(lastReference);
            if (hasCursor)
            {
                commandText += " AND reference > @lastReference";
            }

            // Append sorting and limiting to finalize the keyset pagination query
            commandText += @"
                ORDER BY reference
                LIMIT @pageSize;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);
            npgsqlCommand.Parameters.AddWithValue("pageSize", pageSize);

            // Add the optional parameter values to the command if they are part of the query
            if (hasSubdivisionId)
            {
                npgsqlCommand.Parameters.AddWithValue("subdivisionId", subdivisionId!.Value);
            }

            if (hasCursor)
            {
                npgsqlCommand.Parameters.AddWithValue("lastReference", lastReference!);
            }

            List<Building2DReference>? building2DReferences = await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);

            return building2DReferences;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects located within the specified bounding box, applying a distance tolerance.
        /// </summary>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the spatial area to search for buildings; may be null.</param>
        /// <param name="tolerance">The double value representing the distance tolerance used during the spatial query.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a <see cref="List{Building2D}"/> of buildings found within the specified area.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, double tolerance = Core.Constants.Tolerance.MacroDistance, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (boundingBox2D is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // 1. Get administrative areas to identify which partitions (counties) to hit
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, AdministrativeArealType.Subdivision, tolerance, cancellationToken);

            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                return [];
            }

            Dictionary<string, Building2D> dictionary = [];

            // 2. Prepare pre-calculated boundaries for the 'box' constructor
            double searchMinX = boundingBox2D.Min.X - tolerance;
            double searchMaxX = boundingBox2D.Max.X + tolerance;
            double searchMinY = boundingBox2D.Min.Y - tolerance;
            double searchMaxY = boundingBox2D.Max.Y + tolerance;

            // 3. Optimized Query:
            // - county_id = ANY(@county_ids) triggers Partition Pruning
            // - box && box triggers GiST index scan on those partitions
            const string commandText = $@"
                SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM {Constants.TableName.Building2D}
                WHERE county_id = ANY(@county_ids)
                    AND (subdivision_id = ANY(@subdivision_ids) OR subdivision_id IS NULL)
                    AND box(point(min_x, min_y), point(max_x, max_y)) && box(point(@sMinX, @sMinY), point(@sMaxX, @sMaxY));";

            int[] countyIds = [.. administrativeAreal2Ds.Select(a => a.CountyId).OfType<int>().Distinct()];
            int[] subdivisionIds = [.. administrativeAreal2Ds.Select(a => a.Id).Distinct()];

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("county_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = countyIds });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivision_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = subdivisionIds });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("sMinX", NpgsqlDbType.Double) { Value = searchMinX });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("sMaxX", NpgsqlDbType.Double) { Value = searchMaxX });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("sMinY", NpgsqlDbType.Double) { Value = searchMinY });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("sMaxY", NpgsqlDbType.Double) { Value = searchMaxY });

            List<Building2D>? building2Ds = await ReadAsync_Building2D(npgsqlCommand, cancellationToken);

            if (building2Ds is not null)
            {
                foreach (Building2D building in building2Ds)
                {
                    // Defensive de-duplication keyed on county + reference (unique per row, so duplicates are not expected)
                    string key = $"{building.CountyId}_{building.Reference}";
                    dictionary.TryAdd(key, building);
                }
            }

            return [.. dictionary.Values];
        }

        /// <summary>
        /// Asynchronously retrieves full building data based on a collection of references using optimized partition-pruned batching.
        /// </summary>
        /// <param name="building2DReferences">The collection of <see cref="Building2DReference"/> objects used to identify and retrieve the corresponding buildings from the database.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform an additional fallback query matching by reference only (without county identifier condition) for any references not found in the initial search.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects matching the provided references, an empty list if none match, or null if the input collection was null or the database connection could not be established.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByBuilding2DReferencesAsync(IEnumerable<Building2DReference>? building2DReferences, bool fallbackByReference = false, int commandTimeout = 30, CancellationToken cancellationToken = default)
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

            return await GetBuilding2DsByBuilding2DReferencesAsync(npgsqlConnection, building2DReferences_List, fallbackByReference, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects whose bounding box lies within or intersects the specified circular area (the radius expanded by the tolerance).
        /// </summary>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area; can be null.</param>
        /// <param name="tolerance">The double value representing the distance tolerance for the spatial query, defaulting to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2D}"/> of buildings found within the specified area, an empty list if none match, or null if the input is invalid or the connection could not be established.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByCircle2DAsync(Circle2D? circle2D, double tolerance = Core.Constants.Tolerance.MacroDistance, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (circle2D?.Center is null)
            {
                return null;
            }

            // Delegate the coarse spatial search to the bounding-box query: the circle's bounding box
            // expanded by the tolerance is exactly the effective search region (radius + tolerance).
            // This reuses the partition pruning and the GiST-indexed 'box && box' filter.
            List<Building2D>? building2Ds = await GetBuilding2DsByBoundingBox2DAsync(circle2D.GetBoundingBox(), tolerance, commandTimeout, cancellationToken);
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return building2Ds;
            }

            // Narrow the bounding-box superset to the true circular area (radius + tolerance)
            // using the exact rectangle-vs-circle test on each building's bounding box.
            List<Building2D> result = [];
            foreach (Building2D building2D in building2Ds)
            {
                if (circle2D.InRange(building2D.BoundingBox2D, tolerance))
                {
                    result.Add(building2D);
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2DReference"/> objects for the buildings whose bounding box lies within or intersects the specified circular area (the radius expanded by the tolerance).
        /// </summary>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area; can be null.</param>
        /// <param name="tolerance">The double value representing the distance tolerance for the spatial query, defaulting to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> of references to the buildings found within the specified area, an empty list if none match, or null if the input is invalid or the connection could not be established.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesByCircle2DAsync(Circle2D? circle2D, double tolerance = Core.Constants.Tolerance.MacroDistance, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (circle2D?.Center is null)
            {
                return null;
            }

            // Delegate the coarse spatial search to the bounding-box query: the circle's bounding box
            // expanded by the tolerance is exactly the effective search region (radius + tolerance).
            // This reuses the partition pruning and the GiST-indexed 'box && box' filter.
            List<Building2D>? building2Ds = await GetBuilding2DsByBoundingBox2DAsync(circle2D.GetBoundingBox(), tolerance, commandTimeout, cancellationToken);
            if (building2Ds is null)
            {
                return null;
            }

            if (building2Ds.Count == 0)
            {
                return [];
            }

            // Narrow the bounding-box superset to the true circular area (radius + tolerance)
            // using the exact rectangle-vs-circle test on each building's bounding box.
            List<Building2DReference> result = [];
            foreach (Building2D building2D in building2Ds)
            {
                if (circle2D.InRange(building2D.BoundingBox2D, tolerance))
                {
                    result.Add(new Building2DReference(building2D));
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves the count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="countyId">The optional integer identifier of the county to filter the results; if null, the total count is retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total row count as a long.</returns>
        public async Task<long> GetCountAsync(int? countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string tableName = Constants.TableName.Building2D;
            if (countyId != null && countyId.HasValue)
            {
                tableName = string.Format("{0}_{1}", tableName, countyId.Value);
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, tableName, cancellationToken);
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
        /// <param name="countyIds">A collection of integers representing the IDs of the counties to be counted.</param>
        /// <param name="analyze">A boolean value indicating whether to run a database analysis before fetching the estimate.</param>
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
        /// Asynchronously retrieves a list of <see cref="Point2D"/> objects associated with the specified references and optional county identifier.
        /// </summary>
        /// <param name="references">An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the reference identifiers for the points to retrieve.</param>
        /// <param name="countyId">An optional <see cref="int"/> representing the unique identifier of the county used to filter the search.</param>
        /// <param name="fallbackByReference">A boolean value indicating whether to perform a fallback search by reference alone for any references not found in the initial search.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="Point2D"/> objects if matches are found, an empty list if none match, or null if the input collection was null or the connection could not be established.</returns>
        public async Task<List<Point2D>?> GetPoint2DsByReferencesAsync(IEnumerable<string>? references, int? countyId, bool fallbackByReference = false, CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            List<string> references_List = [.. references];
            if (references_List.Count == 0)
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetPoint2DsByReferencesAsync(npgsqlConnection, references_List, countyId, fallbackByReference, cancellationToken);
        }

        /// <summary>
        /// Asynchronously refreshes the 2D building data in the PostgreSQL database.
        /// </summary>
        /// <param name="postgreSQLBuilding2DRefreshOptions">The options to configure the refresh process for PostgreSQL 2D buildings. Can be null to use default settings.</param>
        /// <param name="progress">The progress reporter used to report the current progress as a long value representing the count of updated buildings. Can be null if no progress reporting is required.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result carries a <see cref="PostgreSQLBuilding2DRefreshResult"/> with details of the operation, or <see langword="null"/> if the database connection could not be established.</returns>
        public async Task<PostgreSQLBuilding2DRefreshResult?> RefreshAsync(PostgreSQLBuilding2DRefreshOptions? postgreSQLBuilding2DRefreshOptions = default, IProgress<long>? progress = default, CancellationToken cancellationToken = default)
        {
            postgreSQLBuilding2DRefreshOptions ??= new PostgreSQLBuilding2DRefreshOptions();

            int batchSize = postgreSQLBuilding2DRefreshOptions.BatchSize < 1 ? 1 : postgreSQLBuilding2DRefreshOptions.BatchSize;
            double tolerance = postgreSQLBuilding2DRefreshOptions.Tolerance;
            bool overrideExistingSubdivisionIds = postgreSQLBuilding2DRefreshOptions.OverrideExistingSubdivisionIds;
            long lastProcessedId = postgreSQLBuilding2DRefreshOptions.StartId;

            long readCount = 0;
            long updatedCount = 0;
            long failedBatchCount = 0;
            bool cancelled = false;

            Serilog.Modify.Log(
                "{Type} refresh started: batch size {BatchSize}, start ID {StartId}, override existing subdivision IDs {OverrideExistingSubdivisionIds}, tolerance {Tolerance}",
                nameof(Building2DPostgreSQLConverter), batchSize, lastProcessedId, overrideExistingSubdivisionIds, tolerance);

            while (!cancellationToken.IsCancellationRequested)
            {
                await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
                if (npgsqlConnection is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type} refresh failed: unable to create database connection", nameof(Building2DPostgreSQLConverter));
                    return null;
                }

                await npgsqlConnection.OpenAsync(cancellationToken);

                // Open transaction to maintain FOR UPDATE SKIP LOCKED locks during the batch
                await using NpgsqlTransaction npgsqlTransaction = await npgsqlConnection.BeginTransactionAsync(cancellationToken);

                // We combine the ID anchor with the optional NULL check.
                // This ensures we always move forward in the table, regardless of update success.
                string filterClause = overrideExistingSubdivisionIds
                    ? "id > @lastId"
                    : "id > @lastId AND subdivision_id IS NULL";

                string commandText_Select = $@"
                    SELECT id, county_id, object
                    FROM {Constants.TableName.Building2D}
                    WHERE {filterClause}
                    ORDER BY id ASC
                    FOR UPDATE SKIP LOCKED
                    LIMIT @batchSize";

                List<(long Id, int CountyId, string Json)> records = [];

                try
                {
                    await using (NpgsqlCommand npgsqlCommand = new(commandText_Select, npgsqlConnection, npgsqlTransaction))
                    {
                        npgsqlCommand.Parameters.AddWithValue("batchSize", batchSize);
                        npgsqlCommand.Parameters.AddWithValue("lastId", lastProcessedId);

                        await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(System.Data.CommandBehavior.SequentialAccess, cancellationToken);
                        while (await npgsqlDataReader.ReadAsync(cancellationToken))
                        {
                            records.Add((npgsqlDataReader.GetInt64(0), npgsqlDataReader.GetInt32(1), npgsqlDataReader.GetString(2)));
                        }
                    }

                    // If the query returns no records matching (id > lastId AND condition), we are done.
                    if (records.Count == 0)
                    {
                        break;
                    }

                    readCount += records.Count;

                    List<(long Id, int CountyId, int SubdivisionId)> updates = [];

                    foreach ((long Id, int CountyId, string Json) in records)
                    {
                        if (cancellationToken.IsCancellationRequested) break;

                        GIS.Classes.Building2D? building = Core.Convert.ToDiGi<GIS.Classes.Building2D>(Json)?.FirstOrDefault();
                        if (building is null)
                        {
                            continue;
                        }

                        int? subdivisionId = await GetSubdivisionIdAsync(npgsqlConnection, building, tolerance);
                        if (subdivisionId.HasValue)
                        {
                            updates.Add((Id, CountyId, subdivisionId.Value));
                        }
                    }

                    // Execute batch update for successfully resolved records
                    if (updates.Count > 0 && !cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteUpdateBatchAsync(npgsqlConnection, npgsqlTransaction, updates, cancellationToken);
                        updatedCount += updates.Count;
                        progress?.Report(updatedCount);
                    }

                    // Commit releases the locks and confirms the batch processing
                    await npgsqlTransaction.CommitAsync(cancellationToken);

                    // Update anchor after successful batch transaction
                    lastProcessedId = records[^1].Id;
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    cancelled = true;
                    break;
                }
                catch (Exception exception)
                {
                    failedBatchCount++;
                    Serilog.Modify.Log(exception, "{Type} refresh batch failed around last ID {LastId}", nameof(Building2DPostgreSQLConverter), lastProcessedId);

                    if (records.Count > 0)
                    {
                        lastProcessedId = records[^1].Id;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            cancelled = cancelled || cancellationToken.IsCancellationRequested;

            Serilog.Modify.Log(
                cancelled || failedBatchCount != 0 ? Serilog.Enums.LogEventLevel.Warning : Serilog.Enums.LogEventLevel.Information,
                "{Type} refresh finished{Cancelled}: {ReadCount} records read, {UpdatedCount} subdivision IDs written, {FailedBatchCount} batches stepped over, last ID {LastId}",
                nameof(Building2DPostgreSQLConverter), cancelled ? " after being cancelled" : string.Empty, readCount, updatedCount, failedBatchCount, lastProcessedId);

            return new PostgreSQLBuilding2DRefreshResult(readCount, updatedCount, failedBatchCount, lastProcessedId, cancelled);

            async static Task ExecuteUpdateBatchAsync(NpgsqlConnection connection, NpgsqlTransaction transaction, List<(long Id, int CountyId, int SubdivisionId)> updates, CancellationToken cancellationToken)
            {
                await using NpgsqlBatch npgsqlBatch = new(connection, transaction);

                foreach ((long Id, int CountyId, int SubdivisionId) in updates)
                {
                    NpgsqlBatchCommand npgsqlBatchCommand = new($"UPDATE {Constants.TableName.Building2D} SET subdivision_id = @subdivision_id WHERE id = @id AND county_id = @county_id");
                    npgsqlBatchCommand.Parameters.AddWithValue("subdivision_id", SubdivisionId);
                    npgsqlBatchCommand.Parameters.AddWithValue("id", Id);
                    npgsqlBatchCommand.Parameters.AddWithValue("county_id", CountyId);
                    npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
                }
                await npgsqlBatch.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously updates the specified collection of 2D buildings using a defined distance tolerance.
        /// </summary>
        /// <param name="building2Ds">The enumerable collection of <see cref="Building2D"/> objects to be updated; may be null.</param>
        /// <param name="tolerance">The double precision value used as the distance tolerance for the update operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers written and the rows dropped before the database, or null when the update could not be attempted at all - no connection, or the table could not be created.</returns>
        public async Task<PostgreSQLUpdateResult?> UpdateAsync(IEnumerable<Building2D>? building2Ds, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (building2Ds is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await Create.TableAsync_Building2D(npgsqlConnection);
            if (!succeded)
            {
                return null;
            }

            HashSet<long> ids = [];
            List<Rejection> rejections = [];

            if (!building2Ds.Any())
            {
                return new PostgreSQLUpdateResult(ids, rejections);
            }

            Dictionary<int, List<Building2D>> dictionary_Building2D = [];

            Dictionary<string, List<AdministrativeAreal2D>> dictionary_Code = [];

            // The parts of a code are cached above; their polygons are cached here, because deriving one
            // deserializes a county-sized geometry and every building carrying the code is tested against
            // the same set.
            Dictionary<string, Dictionary<int, Geometry.Planar.Interfaces.IPolygonal2D>> dictionary_Polygonal2D = [];

            // County assignment runs in three tiers, in descending reliability:
            //   1. building2D.CountyId - already names the part, nothing to infer.
            //   2. building2D.Code     - names the county but not which of its parts. A code holding a
            //                            single part resolves outright; a multi-part code only narrows
            //                            the candidates and hands them to tier 3.
            //   3. geometry            - decides which candidate part really contains the building, and
            //                            is the only tier that can.
            // Tier 2 used to collapse a multi-part county onto one part by itself, which is why
            // building_2d holds ~86k rows duplicated across sibling parts: separate runs resolved the
            // same code to different parts back when that resolution had no ORDER BY. Narrowing instead
            // of choosing is what stops that recurring, and it also makes tier 3 cheaper - the candidate
            // set is the county's own parts rather than every county overlapping the bounding box.
            foreach (Building2D building2D in building2Ds)
            {
                if (building2D is null)
                {
                    // Recorded rather than skipped in silence, so the rejection count stays an exact
                    // account of the rows that never reached the database.
                    rejections.Add(new Rejection(null, UpdateRejectionReason.Undefined));
                    continue;
                }

                int? countyId = building2D.CountyId;

                List<AdministrativeAreal2D>? administrativeAreal2Ds_Candidate = null;
                string? code_Candidate = null;

                if (countyId is null || !countyId.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(building2D.Code))
                    {
                        if (!dictionary_Code.TryGetValue(building2D.Code, out List<AdministrativeAreal2D>? administrativeAreal2Ds_Code) || administrativeAreal2Ds_Code is null)
                        {
                            // Cached per batch: the parts of a code and their polygons are the same for
                            // every building carrying it, and re-reading them per row would put a query
                            // and a polygon parse on each one.
                            administrativeAreal2Ds_Code = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(npgsqlConnection, building2D.Code, AdministrativeArealType.County) ?? [];
                            dictionary_Code[building2D.Code] = administrativeAreal2Ds_Code;
                        }

                        if (administrativeAreal2Ds_Code.Count == 1)
                        {
                            countyId = administrativeAreal2Ds_Code[0].Id;
                        }
                        else if (administrativeAreal2Ds_Code.Count > 1)
                        {
                            administrativeAreal2Ds_Candidate = administrativeAreal2Ds_Code;
                            code_Candidate = building2D.Code;
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
                    BoundingBox2D? boundingBox2D = building2D.BoundingBox2D;

                    // Only a code that named nothing falls back to searching every county by bounding box.
                    List<AdministrativeAreal2D>? administrativeAreal2Ds = administrativeAreal2Ds_Candidate ?? await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, AdministrativeArealType.County, tolerance);
                    if (administrativeAreal2Ds is not null && administrativeAreal2Ds.Count != 0)
                    {
                        if (administrativeAreal2Ds.Count == 1)
                        {
                            countyId = administrativeAreal2Ds[0].Id;
                        }
                        else if (code_Candidate is not null)
                        {
                            if (!dictionary_Polygonal2D.TryGetValue(code_Candidate, out Dictionary<int, Geometry.Planar.Interfaces.IPolygonal2D>? polygonal2Ds_ByCountyId) || polygonal2Ds_ByCountyId is null)
                            {
                                polygonal2Ds_ByCountyId = administrativeAreal2Ds.Polygonal2DsByCountyId();
                                dictionary_Polygonal2D[code_Candidate] = polygonal2Ds_ByCountyId;
                            }

                            countyId = polygonal2Ds_ByCountyId.CountyId(building2D.ToDiGi()?.PolygonalFace2D?.ExternalEdge, tolerance);
                        }
                        else
                        {
                            // The bounding box path answers a different candidate set per building, so there
                            // is nothing to cache across them.
                            countyId = administrativeAreal2Ds.CountyId(building2D.ToDiGi()?.PolygonalFace2D?.ExternalEdge, tolerance);
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
                    // All three tiers ran and named no part. The building is dropped, and naming it here is
                    // the only place that can - nothing downstream knows which rows never made the batch.
                    rejections.Add(new Rejection(building2D.Reference, UpdateRejectionReason.CountyUnresolved));
                    continue;
                }

                if (!dictionary_Building2D.TryGetValue(countyId.Value, out List<Building2D>? building2Ds_CountyId) || building2Ds_CountyId is null)
                {
                    building2Ds_CountyId = [];
                    dictionary_Building2D[countyId.Value] = building2Ds_CountyId;
                }

                building2Ds_CountyId.Add(building2D);
            }

            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (KeyValuePair<int, List<Building2D>> keyValuePair in dictionary_Building2D)
            {
                int countyId = keyValuePair.Key;

                succeded = await Create.TableAsync_Building2D_Partition(npgsqlConnection, countyId);
                if (!succeded)
                {
                    // A whole county's worth of rows disappears here, which is the largest silent drop of
                    // the three and the one least likely to be the caller's doing.
                    foreach (Building2D building2D in keyValuePair.Value)
                    {
                        rejections.Add(new Rejection(building2D.Reference, UpdateRejectionReason.PartitionUnavailable));
                    }

                    continue;
                }

                foreach (Building2D building2D in keyValuePair.Value)
                {
                    // SQL with full update on conflict (excluding ID)
                    NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO {Constants.TableName.Building2D} (county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object)
                    VALUES (@county_id, @reference, @code, @min_x, @min_y, @max_x, @max_y, @subdivision_id, @object)
                    ON CONFLICT (reference, county_id)
                    DO UPDATE SET
                        code = EXCLUDED.code,
                        min_x = EXCLUDED.min_x,
                        min_y = EXCLUDED.min_y,
                        max_x = EXCLUDED.max_x,
                        max_y = EXCLUDED.max_y,
                        subdivision_id = COALESCE(EXCLUDED.subdivision_id, {Constants.TableName.Building2D}.subdivision_id),
                        object = EXCLUDED.object
                    RETURNING id;");

                    BoundingBox2D? boundingBox2D = building2D.BoundingBox2D;

                    // Adding parameters with explicit NpgsqlDbType
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = countyId });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("reference", NpgsqlDbType.Text) { Value = building2D.Reference });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("code", NpgsqlDbType.Text) { Value = building2D.Code });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.X });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.Y });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.X });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.Y });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("subdivision_id", NpgsqlDbType.Integer) { Value = (object?)building2D.SubdivisionId ?? DBNull.Value });

                    // Handling potential null for JSONB object
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                    {
                        Value = (object?)building2D.Object?.ToJsonString() ?? DBNull.Value
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

        private static Building2D Create_Building2D(NpgsqlDataReader npgsqlDataReader)
        {
            return new Building2D
            {
                // Changed to GetInt64 because of BIGINT in SQL
                Id = npgsqlDataReader.GetInt64(0),
                CountyId = npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetInt32(1),
                // Added DBNull checks for strings as they are nullable in schema
                Reference = npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                Code = npgsqlDataReader.IsDBNull(3) ? null : npgsqlDataReader.GetString(3),
                BoundingBox2D = new BoundingBox2D(
                        new Point2D(npgsqlDataReader.IsDBNull(4) ? double.NaN : npgsqlDataReader.GetDouble(4),
                                    npgsqlDataReader.IsDBNull(5) ? double.NaN : npgsqlDataReader.GetDouble(5)),
                        new Point2D(npgsqlDataReader.IsDBNull(6) ? double.NaN : npgsqlDataReader.GetDouble(6),
                                    npgsqlDataReader.IsDBNull(7) ? double.NaN : npgsqlDataReader.GetDouble(7))),
                SubdivisionId = npgsqlDataReader.IsDBNull(8) ? null : npgsqlDataReader.GetInt32(8),
                // Object is JSONB, so it can be null
                Object = npgsqlDataReader.IsDBNull(9) ? null : JsonNode.Parse(npgsqlDataReader.GetString(9)) as JsonObject,
                CreatedAt = npgsqlDataReader.IsDBNull(10) ? null : npgsqlDataReader.GetDateTime(10),
            };
        }

        private static Building2DReference Create_Building2DReference(NpgsqlDataReader npgsqlDataReader)
        {
            return new Building2DReference
            {
                Id = npgsqlDataReader.GetInt64(0),
                CountyId = npgsqlDataReader.IsDBNull(1) ? null : (int?)npgsqlDataReader.GetInt32(1),
                Reference = npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                SubdivisionId = npgsqlDataReader.IsDBNull(3) ? null : (int?)npgsqlDataReader.GetInt32(3)
            };
        }

        private static async Task<int?> GetSubdivisionIdAsync(NpgsqlConnection npgsqlConnection, GIS.Classes.Building2D? building2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (npgsqlConnection is null || building2D?.PolygonalFace2D is not PolygonalFace2D polygonalFace2D)
            {
                return null;
            }

            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, polygonalFace2D.GetBoundingBox(), [AdministrativeArealType.Subdivision], tolerance);
            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                return null;
            }

            List<Tuple<AdministrativeAreal2D, AdministrativeDivision>> tuples_AdministrativeDivision = [];
            List<Tuple<AdministrativeAreal2D, AdministrativeSubdivision>> tuples_AdministrativeSubdivision = [];

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                GIS.Classes.AdministrativeAreal2D? administrativeAreal2D_GIS = administrativeAreal2D.ToDiGi();
                if (administrativeAreal2D_GIS is null)
                {
                    continue;
                }

                if (administrativeAreal2D_GIS is AdministrativeDivision administrativeDivision)
                {
                    tuples_AdministrativeDivision.Add(new Tuple<AdministrativeAreal2D, AdministrativeDivision>(administrativeAreal2D, administrativeDivision));
                }
                else if (administrativeAreal2D_GIS is AdministrativeSubdivision administrativeSubdivision)
                {
                    tuples_AdministrativeSubdivision.Add(new Tuple<AdministrativeAreal2D, AdministrativeSubdivision>(administrativeAreal2D, administrativeSubdivision));
                }
            }

            List<Tuple<AdministrativeAreal2D, double>>? tuples_Area = null;

            if (tuples_AdministrativeSubdivision is null || tuples_AdministrativeSubdivision.Count == 0)
            {
                if (tuples_AdministrativeDivision is null || tuples_AdministrativeDivision.Count == 0)
                {
                    return null;
                }

                if (tuples_AdministrativeDivision.Count == 1)
                {
                    return tuples_AdministrativeDivision[0].Item1.Id;
                }

                tuples_Area = [];
                foreach (Tuple<AdministrativeAreal2D, AdministrativeDivision> tuple in tuples_AdministrativeDivision)
                {
                    if (tuple?.Item2?.PolygonalFace2D is not PolygonalFace2D polygonalFace2D_AdministrativeDivision)
                    {
                        continue;
                    }

                    List<PolygonalFace2D>? polygonal2Ds_Intersection = Geometry.Planar.Query.Intersection(polygonalFace2D, polygonalFace2D_AdministrativeDivision);

                    double area = 0;
                    if (polygonal2Ds_Intersection is not null && polygonal2Ds_Intersection.Count != 0)
                    {
                        area = polygonal2Ds_Intersection.ConvertAll(x => x.GetArea()).Sum();
                    }

                    if (area <= tolerance)
                    {
                        continue;
                    }

                    tuples_Area.Add(new Tuple<AdministrativeAreal2D, double>(tuple.Item1, area));
                }

                if (tuples_Area.Count != 0)
                {
                    tuples_Area.Sort((x, y) =>
                    {
                        int result = y.Item2.CompareTo(x.Item2);
                        return result != 0 ? result : x.Item1.Id.CompareTo(y.Item1.Id);
                    });

                    return tuples_Area[0].Item1.Id;
                }

                return null;
            }

            if (tuples_AdministrativeSubdivision.Count == 1)
            {
                return tuples_AdministrativeSubdivision[0].Item1.Id;
            }

            tuples_Area = [];
            foreach (Tuple<AdministrativeAreal2D, AdministrativeSubdivision> tuple in tuples_AdministrativeSubdivision)
            {
                if (tuple?.Item2?.PolygonalFace2D is not PolygonalFace2D polygonalFace2D_AdministrativeSubdivision)
                {
                    continue;
                }

                List<PolygonalFace2D>? polygonal2Ds_Intersection = Geometry.Planar.Query.Intersection(polygonalFace2D, polygonalFace2D_AdministrativeSubdivision);

                double area = 0;
                if (polygonal2Ds_Intersection is not null && polygonal2Ds_Intersection.Count != 0)
                {
                    area = polygonal2Ds_Intersection.ConvertAll(x => x.GetArea()).Sum();
                }

                if (area <= tolerance)
                {
                    continue;
                }

                tuples_Area.Add(new Tuple<AdministrativeAreal2D, double>(tuple.Item1, area));
            }

            if (tuples_Area.Count != 0)
            {
                tuples_Area.Sort((x, y) =>
                {
                    int result = y.Item2.CompareTo(x.Item2);
                    return result != 0 ? result : x.Item1.Id.CompareTo(y.Item1.Id);
                });

                return tuples_Area[0].Item1.Id;
            }

            return null;
        }

        private static async Task<List<Building2D>?> ReadAsync_Building2D(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync_Building2D(npgsqlDataReader, cancellationToken);
        }

        private static async Task<List<Building2D>> ReadAsync_Building2D(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<Building2D> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_Building2D(npgsqlDataReader));
            }

            return result;
        }

        private static async Task<List<Building2DReference>?> ReadAsync_Building2DReference(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync_Building2DReference(npgsqlDataReader, cancellationToken);
        }

        /// <summary>
        /// Deletes the rows holding the given references under a single county row.
        /// <para>A reference is unique only per <c>county_id</c>: the same building is held once per county row it was imported under, so a delete has to name the row as well as the reference. Deleting by reference alone would take the building out of every part of the county.</para>
        /// <para>Intended for repairing the parts a building was filed under by mistake. It removes data and has no undo - read <c>AI Guidelines/Coding - GIS Administrative Data.md</c> before calling it, and make sure the building survives under the part it belongs to first.</para>
        /// </summary>
        /// <param name="references">The references to delete.</param>
        /// <param name="countyId">The identifier of the county row to delete them from.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
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

            // UNNEST keeps this one statement rather than one per reference, and RETURNING reports what was
            // really removed - the count is the only evidence that the delete matched what was intended.
            string commandText = $@"
                DELETE FROM {Constants.TableName.Building2D}
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

        private static async Task<List<Building2DReference>> ReadAsync_Building2DReference(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<Building2DReference> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_Building2DReference(npgsqlDataReader));
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves duplicate building references that occur across multiple counties, ordered by collision count descending.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the query.</param>
        /// <param name="limit">The maximum number of duplicate references to return. Defaults to 100.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout. Defaults to 600 seconds.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, returning a list of <see cref="Building2DReferenceDuplicate"/> instances if any duplicates exist; otherwise, null.</returns>
        public static async Task<List<Building2DReferenceDuplicate>?> GetDuplicateReferencesAsync(NpgsqlConnection? npgsqlConnection, int limit = 100, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT reference, COUNT(*) AS count, ARRAY_AGG(county_id ORDER BY county_id) AS county_ids
                FROM {Constants.TableName.Building2D}
                GROUP BY reference
                HAVING COUNT(*) > 1
                ORDER BY count DESC
                LIMIT @limit;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.AddWithValue("limit", limit);

            List<Building2DReferenceDuplicate> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                string? reference = npgsqlDataReader.IsDBNull(0) ? null : npgsqlDataReader.GetString(0);
                long count = npgsqlDataReader.IsDBNull(1) ? 0 : npgsqlDataReader.GetInt64(1);
                int[]? countyIds = npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetFieldValue<int[]>(2);

                result.Add(new Building2DReferenceDuplicate(reference, count, countyIds));
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves duplicate building references that occur across multiple counties, ordered by collision count descending.
        /// </summary>
        /// <param name="limit">The maximum number of duplicate references to return. Defaults to 100.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout. Defaults to 600 seconds.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, returning a list of <see cref="Building2DReferenceDuplicate"/> instances if any duplicates exist; otherwise, null.</returns>
        public async Task<List<Building2DReferenceDuplicate>?> GetDuplicateReferencesAsync(int limit = 100, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetDuplicateReferencesAsync(npgsqlConnection, limit, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves overall building reference uniqueness metrics across all partitions in the database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the query.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout. Defaults to 600 seconds.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="Building2DReferenceUniquenessSummary"/> object containing total, distinct, and duplicate metrics; or null if the connection is null.</returns>
        public static async Task<Building2DReferenceUniquenessSummary?> GetReferenceUniquenessSummaryAsync(NpgsqlConnection? npgsqlConnection, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT
                    COUNT(*) AS total_count,
                    COUNT(DISTINCT reference) AS distinct_count
                FROM {Constants.TableName.Building2D};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            if (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                long totalCount = npgsqlDataReader.IsDBNull(0) ? 0 : npgsqlDataReader.GetInt64(0);
                long distinctReferenceCount = npgsqlDataReader.IsDBNull(1) ? 0 : npgsqlDataReader.GetInt64(1);
                long duplicateReferenceCount = totalCount - distinctReferenceCount;
                bool isUnique = duplicateReferenceCount == 0;

                return new Building2DReferenceUniquenessSummary(totalCount, distinctReferenceCount, duplicateReferenceCount, isUnique);
            }

            return null;
        }

        /// <summary>
        /// Asynchronously retrieves overall building reference uniqueness metrics across all partitions in the database.
        /// </summary>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout. Defaults to 600 seconds.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="Building2DReferenceUniquenessSummary"/> object containing total, distinct, and duplicate metrics; or null if connection fails.</returns>
        public async Task<Building2DReferenceUniquenessSummary?> GetReferenceUniquenessSummaryAsync(int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetReferenceUniquenessSummaryAsync(npgsqlConnection, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously counts the buildings of one county whose subdivision has not been resolved.
        /// <para>These buildings belong to no subdivision, so anything driven by subdivisions - the building data update among them - never visits them. The figure is therefore the part of a coverage shortfall that no run can currently close.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county to count.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count, or -1 when no connection could be built.</returns>
        public async Task<long> GetCountWithoutSubdivisionAsync(int countyId, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetCountWithoutSubdivisionAsync(npgsqlConnection, countyId, commandTimeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously counts the buildings of one county whose subdivision has not been resolved, over the given connection.
        /// </summary>
        /// <param name="npgsqlConnection">The Npgsql connection instance used to execute the query. This value can be null.</param>
        /// <param name="countyId">The identifier of the county to count.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count, or -1 when the connection is null.</returns>
        public static async Task<long> GetCountWithoutSubdivisionAsync(NpgsqlConnection? npgsqlConnection, int countyId, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            string commandText = $@"
                SELECT COUNT(*)
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @countyId
                  AND subdivision_id IS NULL;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });

            object? @object = await npgsqlCommand.ExecuteScalarAsync(cancellationToken);

            return @object is long @long ? @long : -1;
        }
    }
}
