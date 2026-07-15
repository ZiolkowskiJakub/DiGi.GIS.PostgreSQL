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
        public static async Task<long> CountAsync(NpgsqlConnection npgsqlConnection, int countyId, IEnumerable<int>? subdivisionIds = null, CancellationToken cancellationToken = default)
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
                commandText += " AND subdivision_id = ANY(@subdivisionIds)";
            }

            commandText += ";";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            if (hasSubdivisionIds)
            {
                // Explicitly converting to an array for the ANY() operator in PostgreSQL
                int[] subdivisionIdsArray = [.. subdivisionIds!];
                npgsqlCommand.Parameters.AddWithValue("subdivisionIds", subdivisionIdsArray);
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
        public static async Task<List<Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(NpgsqlConnection npgsqlConnection, int countyId, IEnumerable<int>? subdivisionIds = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            // Filter references by the county partition key, optionally narrowing to the given subdivisions.
            const string commandText = $@"
                SELECT id, county_id, reference, subdivision_id
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @county_id
                    AND (subdivision_id = ANY(@subdivision_ids) OR subdivision_id IS NULL);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("county_id", countyId);
            npgsqlCommand.Parameters.AddWithValue("subdivision_ids", subdivisionIds?.ToArray() as object ?? DBNull.Value);

            return await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);
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
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="Point2D"/> objects if matches are found; otherwise, null.</returns>
        public static async Task<List<Point2D>?> GetPoint2DsByReferences(NpgsqlConnection npgsqlConnection, IEnumerable<string> references, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                    SELECT min_x, min_y, max_x, max_y
                    FROM {Constants.TableName.Building2D}
                    WHERE reference = ANY(@references) AND county_id = @countyId;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            List<Point2D> result = [];

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

                result.Add(new Point2D((min_x + max_x) / 2, (min_y + max_y) / 2));
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

            administrativeAreal2DReferences.Filter(x => x?.AdministrativeArealType == AdministrativeArealType.Subdivison, out administrativeAreal2DReferences, out List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Out);

            administrativeAreal2DReferences ??= [];

            if (administrativeAreal2DReferences_Out is not null && administrativeAreal2DReferences_Out.Count != 0)
            {
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Out in administrativeAreal2DReferences_Out)
                {
                    List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(npgsqlConnection, AdministrativeArealType.Subdivison, administrativeAreal2DReference_Out.Id, false, cancellationToken);
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
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Building2D"/> found at the specified location, or null if no building is found within the tolerance or if the provided point is null.</returns>
        public async Task<Building2D?> GetBuilding2DByPoint2DAsync(Point2D? point2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
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

            await npgsqlConnection.OpenAsync();

            // 1. Identify the administrative area (subdivision) for this specific point.
            // We only need one area to get the county_id (partition key) and subdivision_id.
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, new BoundingBox2D(point2D, point2D), [AdministrativeArealType.Subdivison], tolerance);
            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                return null;
            }

            // Represent the point as a tolerance-sized search box so the GiST index on
            // box(point(min_x, min_y), point(max_x, max_y)) can serve the '&&' overlap operator.
            double searchMinX = point2D.X - tolerance;
            double searchMinY = point2D.Y - tolerance;
            double searchMaxX = point2D.X + tolerance;
            double searchMaxY = point2D.Y + tolerance;

            const string commandText = $@"
                    SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                    FROM {Constants.TableName.Building2D}
                    WHERE county_id = @countyId
                        AND (subdivision_id = @subdivisionId OR subdivision_id IS NULL)
                        AND box(point(min_x, min_y), point(max_x, max_y)) && box(point(@searchMinX, @searchMinY), point(@searchMaxX, @searchMaxY))
                    LIMIT 1;";

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("countyId", administrativeAreal2D.CountyId as object ?? DBNull.Value);
                npgsqlCommand.Parameters.AddWithValue("subdivisionId", administrativeAreal2D.Id as object ?? DBNull.Value);
                npgsqlCommand.Parameters.AddWithValue("searchMinX", searchMinX);
                npgsqlCommand.Parameters.AddWithValue("searchMinY", searchMinY);
                npgsqlCommand.Parameters.AddWithValue("searchMaxX", searchMaxX);
                npgsqlCommand.Parameters.AddWithValue("searchMaxY", searchMaxY);

                List<Building2D>? results = await ReadAsync_Building2D(npgsqlCommand);

                if (results is not null && results.Count > 0)
                {
                    return results[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="Building2D"/> instance based on the specified reference string and an optional county identifier.
        /// </summary>
        /// <param name="reference">The <see cref="string"/> reference used to identify the building.</param>
        /// <param name="countyId">The optional nullable integer identifier for the county.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Building2D"/> instance if found; otherwise, null.</returns>
        public async Task<Building2D?> GetBuilding2DByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
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

            return results?.FirstOrDefault();
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
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="T:Building2DReference" /> if a match is found; otherwise, null.</returns>
        public async Task<Building2DReference?> GetBuilding2DReferenceByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
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
                    WHERE reference = @reference{(countyId is null ? "" : " AND county_id = @countyId")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("reference", reference);
            if (countyId is not null)
            {
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId.Value);
            }

            List<Building2DReference>? results = await ReadAsync_Building2DReference(npgsqlCommand, cancellationToken);

            return results?.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects for a specified county identifier.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county used to filter the search.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects if matches are found; otherwise, null.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByCountyIdAsync(int countyId, CancellationToken cancellationToken = default)
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
                SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM {Constants.TableName.Building2D}
                WHERE county_id = @countyId;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            return await ReadAsync_Building2D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Retrieves full Building2DReference data from building_2d table based on input references.
        /// Performs batch processing using UNNEST to avoid N+1 query performance issues.
        /// </summary>
        /// <param name="building2DReferences">Collection of partial references (must have Reference, optional CountyId).</param>
        /// <returns>A list of populated Building2DReference objects found in the database.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesAsync(IEnumerable<Building2DReference> building2DReferences)
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

            await npgsqlConnection.OpenAsync();

            // Prepare arrays to send as parameters for the UNNEST function
            string[] references = [.. building2DReferences.Select(l => l.Reference ?? string.Empty)];
            int?[] countyIds = [.. building2DReferences.Select(l => l.CountyId)];

            // SQL Logic:
            // 1. UNNEST creates a virtual table from the input arrays.
            // 2. INNER JOIN matches input with building_2d table.
            // 3. The WHERE clause handles optional CountyId (if null in input, matches only by reference).
            // 4. DISTINCT ON ensures we get only one result per input record even if multiple matches exist.
            const string commandText = $@"
                SELECT DISTINCT ON (input.ref, input.cnty)
                       b.id, b.county_id, b.reference, b.subdivision_id
                FROM UNNEST(@refs, @counties) AS input(ref, cnty)
                INNER JOIN {Constants.TableName.Building2D} b ON b.reference = input.ref
                WHERE (input.cnty IS NULL OR b.county_id = input.cnty)
                ORDER BY input.ref, input.cnty, b.id ASC;";

            List<Building2DReference> result = [];

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("refs", references);
            npgsqlCommand.Parameters.AddWithValue("counties", countyIds);

            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                // Mapping the flat row back to a Building2DReference object
                Building2DReference building2DReference = new()
                {
                    Id = reader.GetInt64(0),
                    CountyId = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    Reference = reader.IsDBNull(2) ? null : reader.GetString(2),
                    SubdivisionId = reader.IsDBNull(3) ? null : reader.GetInt32(3)
                };
                result.Add(building2DReference);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of building 2D references associated with the specified administrative areal 2D identifiers.
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

            administrativeAreal2DReferences.Filter(x => x?.AdministrativeArealType == AdministrativeArealType.Subdivison, out administrativeAreal2DReferences, out List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Out);

            administrativeAreal2DReferences ??= [];

            if (administrativeAreal2DReferences_Out is not null && administrativeAreal2DReferences_Out.Count != 0)
            {
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Out in administrativeAreal2DReferences_Out)
                {
                    List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(npgsqlConnection, AdministrativeArealType.Subdivison, administrativeAreal2DReference_Out.Id, false, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <returns>A list of Building2DReference objects, or null if connection fails.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(int countyId, int? subdivisionId = null, IEnumerable<string>? excludedReferences = null, CancellationToken cancellationToken = default, int commandTimeout = 30)
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
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a <see cref="List{Building2D}"/> of buildings found within the specified area.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
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
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, AdministrativeArealType.Subdivison, tolerance, cancellationToken);

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
        /// Retrieves full building data based on a collection of references using optimized UNNEST batching.
        /// </summary>
        /// <param name="building2DReferences">The collection of <see cref="Building2DReference"/> objects used to identify and retrieve the corresponding buildings from the database.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2D"/> objects matching the provided references, or null if the input collection was null.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByBuilding2DReferences(IEnumerable<Building2DReference> building2DReferences, int commandTimeout = 30)
        {
            if (building2DReferences == null || !building2DReferences.Any())
            {
                return building2DReferences == null ? null : [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection == null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            string[] references = [.. building2DReferences.Select(l => l.Reference ?? string.Empty)];
            int?[] countyIds = [.. building2DReferences.Select(l => l.CountyId)];

            // Optimized query using UNNEST to perform a join against the input collection
            const string commandText = $@"
                SELECT b.id, b.county_id, b.reference, b.code, b.min_x, b.min_y, b.max_x, b.max_y, b.subdivision_id, b.object, b.created_at
                FROM UNNEST(@refs, @counties) AS input(ref, cnty)
                INNER JOIN {Constants.TableName.Building2D} b ON b.reference = input.ref
                WHERE (input.cnty IS NULL OR b.county_id = input.cnty);";

            List<Building2D> result = [];

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.AddWithValue("refs", references);
            npgsqlCommand.Parameters.AddWithValue("counties", countyIds);

            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                // Using the central mapping method to ensure consistency
                result.Add(Create_Building2D(reader));
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="Building2D"/> objects whose bounding box lies within or intersects the specified circular area (the radius expanded by the tolerance).
        /// </summary>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area; can be null.</param>
        /// <param name="tolerance">The double value representing the distance tolerance for the spatial query, defaulting to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2D}"/> of buildings found within the specified area, an empty list if none match, or null if the input is invalid or the connection could not be established.</returns>
        public async Task<List<Building2D>?> GetBuilding2DsByCircle2DAsync(Circle2D? circle2D, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (circle2D?.Center is null)
            {
                return null;
            }

            // Delegate the coarse spatial search to the bounding-box query: the circle's bounding box
            // expanded by the tolerance is exactly the effective search region (radius + tolerance).
            // This reuses the partition pruning and the GiST-indexed 'box && box' filter.
            List<Building2D>? building2Ds = await GetBuilding2DsByBoundingBox2DAsync(circle2D.GetBoundingBox(), tolerance, cancellationToken);
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
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="Point2D"/> objects if matches are found; otherwise, null.</returns>
        public async Task<List<Point2D>?> GetPoint2DsByReferences(IEnumerable<string> references, int? countyId, CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetPoint2DsByReferences(npgsqlConnection, references, countyId, cancellationToken);
        }

        /// <summary>
        /// Asynchronously refreshes the 2D building data in the PostgreSQL database.
        /// </summary>
        /// <param name="postgreSQLBuilding2DRefreshOptions">The options to configure the refresh process for PostgreSQL 2D buildings. Can be null to use default settings.</param>
        /// <param name="progress">The progress reporter used to report the current progress as a long value. Can be null if no progress reporting is required.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the refresh succeeded; otherwise, false.</returns>
        public async Task<bool> RefreshAsync(PostgreSQLBuilding2DRefreshOptions? postgreSQLBuilding2DRefreshOptions = default, IProgress<long>? progress = default, CancellationToken cancellationToken = default)
        {
            postgreSQLBuilding2DRefreshOptions ??= new PostgreSQLBuilding2DRefreshOptions();

            int totalUpdated = 0;
            // We use -1 or 0 as the initial anchor point for Keyset Pagination
            long lastProcessedId = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
                if (npgsqlConnection is null)
                {
                    return false;
                }

                await npgsqlConnection.OpenAsync(cancellationToken);

                // Open transaction to maintain FOR UPDATE SKIP LOCKED locks during the batch
                await using NpgsqlTransaction npgsqlTransaction = await npgsqlConnection.BeginTransactionAsync(cancellationToken);

                // We combine the ID anchor with the optional NULL check.
                // This ensures we always move forward in the table, regardless of update success.
                string filterClause = postgreSQLBuilding2DRefreshOptions.OverrideExistingSubdivisionIds
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
                        npgsqlCommand.Parameters.AddWithValue("batchSize", postgreSQLBuilding2DRefreshOptions.BatchSize);
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

                    List<(long Id, int CountyId, int SubdivisionId)> updates = [];

                    foreach ((long Id, int CountyId, string Json) in records)
                    {
                        if (cancellationToken.IsCancellationRequested) break;

                        // CRITICAL: Always update the anchor to the current ID.
                        // This prevents the loop from getting stuck on records that fail processing.
                        if (Id > lastProcessedId)
                        {
                            lastProcessedId = Id;
                        }

                        GIS.Classes.Building2D? building = Core.Convert.ToDiGi<GIS.Classes.Building2D>(Json)?.FirstOrDefault();
                        if (building is null)
                        {
                            continue;
                        }

                        int? subdivisionId = await GetSubdivisionIdAsync(npgsqlConnection, building, postgreSQLBuilding2DRefreshOptions.Tolerance);
                        if (subdivisionId.HasValue)
                        {
                            updates.Add((Id, CountyId, subdivisionId.Value));
                        }
                    }

                    // Execute batch update for successfully resolved records
                    if (updates.Count > 0 && !cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteUpdateBatchAsync(npgsqlConnection, npgsqlTransaction, updates, cancellationToken);
                        totalUpdated += updates.Count;
                        progress?.Report(totalUpdated);
                    }

                    // Commit releases the locks and confirms the batch processing
                    await npgsqlTransaction.CommitAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
                catch (Exception)
                {
                    // Log exception here
                    throw;
                }
            }

            return !cancellationToken.IsCancellationRequested;

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
        /// <returns>A task that represents the asynchronous operation. The task result contains a nullable long indicating the outcome of the update, or null if the operation could not be completed.</returns>
        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<Building2D>? building2Ds, double tolerance = Core.Constants.Tolerance.MacroDistance)
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

            HashSet<long> result = [];

            if (!building2Ds.Any())
            {
                return result;
            }

            Dictionary<int, List<Building2D>> dictionary_Building2D = [];

            Dictionary<string, int> dictionary_Code = [];

            foreach (Building2D building2D in building2Ds)
            {
                if (building2D is null)
                {
                    continue;
                }

                int? countyId = building2D.CountyId;

                if (countyId is null || !countyId.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(building2D.Code))
                    {
                        if (dictionary_Code.TryGetValue(building2D.Code, out int countyId_Dictionary))
                        {
                            countyId = countyId_Dictionary;
                        }
                        else
                        {
                            if (await AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(npgsqlConnection, building2D.Code, AdministrativeArealType.County) is int countyId_Code)
                            {
                                countyId = countyId_Code;
                                dictionary_Code[building2D.Code] = countyId_Code;
                            }
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
                    BoundingBox2D? boundingBox2D = building2D.BoundingBox2D;

                    List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, AdministrativeArealType.County, tolerance);
                    if (administrativeAreal2Ds != null)
                    {
                        int count = administrativeAreal2Ds.Count;
                        if (count == 1)
                        {
                            countyId = administrativeAreal2Ds[0].Id;
                        }
                        else if (count > 1)
                        {
                            GIS.Classes.Building2D? building2D_GIS = building2D.ToDiGi();

                            Geometry.Planar.Interfaces.IPolygonal2D? polygonal2D_Building2D = building2D_GIS?.PolygonalFace2D?.ExternalEdge;
                            if (polygonal2D_Building2D is null)
                            {
                                continue;
                            }

                            List<Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>> tuples_AdministrativeAreal2D = administrativeAreal2Ds.ConvertAll(x => new Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>(x, x.ToDiGi()?.PolygonalFace2D?.ExternalEdge));
                            tuples_AdministrativeAreal2D.RemoveAll(x => x?.Item2 is null);

                            if (tuples_AdministrativeAreal2D.Count == 1)
                            {
                                countyId = tuples_AdministrativeAreal2D[0].Item1.Id;
                            }
                            else if (count > 1)
                            {
                                List<Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>> tuples_AdministrativeAreal2_Temp = tuples_AdministrativeAreal2D.FindAll(x => x.Item2!.InRange(polygonal2D_Building2D, tolerance));
                                if (tuples_AdministrativeAreal2_Temp.Count == 0)
                                {
                                    List<Tuple<AdministrativeAreal2D, double>> tuples_Distance = [];
                                    foreach (Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?> tuple in tuples_AdministrativeAreal2D)
                                    {
                                        Geometry.Planar.Interfaces.IPolygonal2D polygonal2D_AdministrativeAreal2D = tuple.Item2!;

                                        double distance = Geometry.Planar.Query.Distance(polygonal2D_Building2D, polygonal2D_AdministrativeAreal2D, out _, out _, tolerance);

                                        tuples_Distance.Add(new Tuple<AdministrativeAreal2D, double>(tuple.Item1, distance));
                                    }

                                    if (tuples_Distance.Count != 0)
                                    {
                                        tuples_Distance.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                                        countyId = tuples_Distance[0].Item1.Id;
                                    }
                                }
                                else if (tuples_AdministrativeAreal2_Temp.Count == 1)
                                {
                                    countyId = tuples_AdministrativeAreal2_Temp[0].Item1.Id;
                                }
                                else
                                {
                                    List<Tuple<AdministrativeAreal2D, double>> tuples_Area = [];
                                    foreach (Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?> tuple in tuples_AdministrativeAreal2_Temp)
                                    {
                                        Geometry.Planar.Interfaces.IPolygonal2D polygonal2D_AdministrativeAreal2D = tuple.Item2!;

                                        List<Geometry.Planar.Interfaces.IPolygonal2D>? polygonal2Ds_Intersection = Geometry.Planar.Query.Intersection<Geometry.Planar.Interfaces.IPolygonal2D, Geometry.Planar.Interfaces.IPolygonal2D>([polygonal2D_AdministrativeAreal2D, polygonal2D_Building2D], tolerance);

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
                                        tuples_Area.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                                        countyId = tuples_Area[0].Item1.Id;
                                    }
                                }
                            }
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
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
                        subdivision_id = EXCLUDED.subdivision_id,
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

            // Execute batch and collect IDs
            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync();

            do
            {
                while (await npgsqlDataReader.ReadAsync())
                {
                    // The RETURNING id works for both INSERT and UPDATE cases
                    long id = npgsqlDataReader.GetInt64(0);
                    result.Add(id);
                }
            }
            while (await npgsqlDataReader.NextResultAsync());

            return result;
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

            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, polygonalFace2D.GetBoundingBox(), [AdministrativeArealType.Subdivison], tolerance);
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
                    tuples_Area.Sort((x, y) => x.Item2.CompareTo(y.Item2));

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
                tuples_Area.Sort((x, y) => x.Item2.CompareTo(y.Item2));

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

        private static async Task<List<Building2DReference>> ReadAsync_Building2DReference(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<Building2DReference> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_Building2DReference(npgsqlDataReader));
            }

            return result;
        }
    }
}