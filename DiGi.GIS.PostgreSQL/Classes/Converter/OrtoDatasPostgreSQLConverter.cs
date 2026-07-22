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
        /// <param name="npgsqlConnection">The <see cref="Npgsql.NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier for the county; if null, the estimate is calculated across all counties.</param>
        /// <param name="analyze">A value indicating whether to perform an ANALYZE operation on the database table to update statistics before retrieving the count.</param>
        /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
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
        /// <param name="npgsqlConnection">The <see cref="Npgsql.NpgsqlConnection" /> to use for the query.</param>
        /// <param name="countyIds">A collection of integers representing the county identifiers to estimate counts for.</param>
        /// <param name="analyze">A boolean indicating whether to run an analysis operation before fetching the estimated count.</param>
        /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
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
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Building2DReference"/> objects, or null if the connection or input collection is null.</returns>
        public static async Task<List<Building2DReference>?> GetExistingBuilding2DReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<Building2DReference>? building2DReferences, bool inverted = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || building2DReferences is null)
            {
                return null;
            }

            if (!building2DReferences.Any())
            {
                return [];
            }

            // 1. Prepare data for UNNEST
            int[] countyIds = [.. building2DReferences.Select(l => l.CountyId ?? 0)];
            string[] references = [.. building2DReferences.Select(l => l.Reference ?? string.Empty)];

            // 2. We use a LEFT JOIN between the input list (UNNEST) and the actual table.
            // If u.reference IS NULL, it means the item does NOT exist in the database.
            const string commandText = $@"
                SELECT input.c, input.r
                FROM UNNEST(@counties, @refs) AS input(c, r)
                LEFT JOIN {TableName.OrtoDatas} u ON u.county_id = input.c AND u.reference = input.r
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

            return results;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="OrtoDatas" /> based on the specified references and optional county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="Npgsql.NpgsqlConnection" /> used to connect to the database.</param>
        /// <param name="references">A collection of strings representing the references to search for.</param>
        /// <param name="countyId">The optional integer identifier of the county to filter the results.</param>
        /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken" /> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrtoDatas" /> objects, or null if the connection is null or no matching data is found.</returns>
        public static async Task<List<OrtoDatas>?> GetOrtoDatasByReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? references, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || references is null)
            {
                return null;
            }

            List<OrtoDatas>? result = [];

            if (!references.Any())
            {
                return result;
            }

            const string commandText = $@"
                SELECT id, county_id, reference, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM {TableName.OrtoDatas}
                WHERE reference = ANY(@references)
                  AND (@countyId IS NULL OR county_id = @countyId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            result = await ReadAsync_OrtoDatas(npgsqlCommand, cancellationToken);

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
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="HashSet{T}"/> of strings containing the filtered references, or null if the operation fails or no results are found.</returns>
        public async Task<HashSet<string>?> ContainsByReferencesAsync(IEnumerable<string> references, int? countyId, bool inverted = false, CancellationToken cancellationToken = default)
        {
            if (references is null)
            {
                return null;
            }

            if (!references.Any())
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

                string[] referenceArray = [.. references.Distinct()];

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

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves the count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="countyId">The optional integer identifier of the county to filter the count; if null, the count is retrieved for all counties.</param>
        /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
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
        /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
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
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> of matching references, or null if the input collection is null.</returns>
        public async Task<List<Building2DReference>?> GetExistingBuilding2DReferencesAsync(IEnumerable<Building2DReference>? building2DReferences, bool inverted = false, CancellationToken cancellationToken = default)
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

            return await GetExistingBuilding2DReferencesAsync(npgsqlConnection, building2DReferences, inverted, cancellationToken);
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
        /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OrtoDatas"/> object if found; otherwise, null.</returns>
        public async Task<OrtoDatas?> GetOrtoDatasByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetOrtoDatasByReferencesAsync([reference], countyId, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="OrtoDatas"/> based on the specified references and county identifier.
        /// </summary>
        /// <param name="references">An optional collection of strings representing the references to filter by.</param>
        /// <param name="countyId">An optional integer representing the unique identifier of the county.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrtoDatas"/> objects, or null if no matching data is found.</returns>
        public async Task<List<OrtoDatas>?> GetOrtoDatasByReferencesAsync(IEnumerable<string>? references, int? countyId, CancellationToken cancellationToken = default)
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

            return await GetOrtoDatasByReferencesAsync(npgsqlConnection, references, countyId, cancellationToken);
        }

        /// <summary>
        /// Asynchronously updates the data based on the provided orthodata and tolerance.
        /// </summary>
        /// <param name="ortoDatas">A nullable enumerable collection of <see cref="OrtoDatas"/> to be processed for the update.</param>
        /// <param name="tolerance">A double-precision floating-point number representing the distance tolerance used during the update process. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a nullable long, which may represent a unique identifier or the number of updated records.</returns>
        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<OrtoDatas>? ortoDatas, double tolerance = Core.Constants.Tolerance.MacroDistance)
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

            HashSet<long> result = [];

            if (!ortoDatas.Any())
            {
                return result;
            }

            Dictionary<int, List<OrtoDatas>> dictionary_OrtoDatas = [];

            foreach (OrtoDatas ortoDatas_Temp in ortoDatas)
            {
                if (ortoDatas_Temp is null)
                {
                    continue;
                }

                int? countyId = ortoDatas_Temp.CountyId;

                if (countyId is null || !countyId.HasValue)
                {
                    BoundingBox2D? boundingBox2D = ortoDatas_Temp.BoundingBox2D;
                    if (boundingBox2D is null)
                    {
                        continue;
                    }

                    List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, Enums.AdministrativeArealType.County, tolerance);
                    if (administrativeAreal2Ds != null)
                    {
                        int count = administrativeAreal2Ds.Count;
                        if (count == 1)
                        {
                            countyId = administrativeAreal2Ds[0].Id;
                        }
                        else if (count > 1)
                        {
                            List<Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>> tuples_AdministrativeAreal2D = administrativeAreal2Ds.ConvertAll(x => new Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>(x, x.ToDiGi()?.PolygonalFace2D?.ExternalEdge));
                            tuples_AdministrativeAreal2D.RemoveAll(x => x?.Item2 is null);

                            if (tuples_AdministrativeAreal2D.Count == 1)
                            {
                                countyId = tuples_AdministrativeAreal2D[0].Item1.Id;
                            }
                            else if (count > 1)
                            {
                                List<Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>> tuples_AdministrativeAreal2_Temp = tuples_AdministrativeAreal2D.FindAll(x => x.Item2!.InRange(boundingBox2D, tolerance));
                                if (tuples_AdministrativeAreal2_Temp.Count == 0)
                                {
                                    List<Tuple<AdministrativeAreal2D, double>> tuples_Distance = [];
                                    foreach (Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?> tuple in tuples_AdministrativeAreal2D)
                                    {
                                        Geometry.Planar.Interfaces.IPolygonal2D polygonal2D_AdministrativeAreal2D = tuple.Item2!;

                                        double distance = Geometry.Planar.Query.Distance(boundingBox2D, polygonal2D_AdministrativeAreal2D, out _, out _, tolerance);

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
                                    if ((Polygon2D?)boundingBox2D is Polygon2D polygon2D)
                                    {
                                        List<Tuple<AdministrativeAreal2D, double>> tuples_Area = [];
                                        foreach (Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?> tuple in tuples_AdministrativeAreal2_Temp)
                                        {
                                            Geometry.Planar.Interfaces.IPolygonal2D polygonal2D_AdministrativeAreal2D = tuple.Item2!;

                                            List<Geometry.Planar.Interfaces.IPolygonal2D>? polygonal2Ds_Intersection = Geometry.Planar.Query.Intersection<Geometry.Planar.Interfaces.IPolygonal2D, Geometry.Planar.Interfaces.IPolygonal2D>([polygonal2D_AdministrativeAreal2D, polygon2D], tolerance);

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
                }

                if (countyId is null || !countyId.HasValue)
                {
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
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{Building2DReference}"/> of the updated references, or null if the input collection was null or an error occurred.</returns>
        public async Task<List<Building2DReference>?> UpdateSubdivisionIds(IEnumerable<Building2DReference>? building2DReferences, CancellationToken cancellationToken = default)
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

            // Rozdzielamy dane na te, które mają CountyId i te, które go nie mają
            List<Building2DReference> withCounty = [.. building2DReferences.Where(x => x.CountyId.HasValue)];
            List<Building2DReference> withoutCounty = [.. building2DReferences.Where(x => !x.CountyId.HasValue)];

            // Jeśli brakuje CountyId, musimy je dociągnąć z bazy danych przed aktualizacją
            if (withoutCounty.Count > 0)
            {
                string[] missingRefs = [.. withoutCounty.Select(x => x.Reference ?? string.Empty)];

                // Szukamy county_id dla podanych referencji
                const string findSql = @"
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
            const string updateSql = $@"
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
                Console.WriteLine($"Database error in GetUpdateSubdivisionIds: {ex.Message}");
                throw;
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