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
    public class Building2DPostgreSQLConverter : PostgreSQLConverter<Building2D>, IGISPostgreSQLConverter<Building2D>
    {
        public Building2DPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public async Task<bool> ClearAsync()
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync();

            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, "building_2D");
        }

        public async Task<Building2D?> GetBuilding2DbyPoint2DAsync(Point2D? point2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
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

            string commandText = @"
                    SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                    FROM building_2d
                    WHERE county_id = @countyId 
                        AND (subdivision_id = @subdivisionId OR subdivision_id IS NULL)
                        AND (@x + @tolerance) >= min_x AND (@x - @tolerance) <= max_x
                        AND (@y + @tolerance) >= min_y AND (@y - @tolerance) <= max_y
                    LIMIT 1;";

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                int countyId = administrativeAreal2D.Id;
                int subdivisionId = administrativeAreal2D.Id;

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("countyId", countyId);
                npgsqlCommand.Parameters.AddWithValue("subdivisionId", subdivisionId);
                npgsqlCommand.Parameters.AddWithValue("x", point2D.X);
                npgsqlCommand.Parameters.AddWithValue("y", point2D.Y);
                npgsqlCommand.Parameters.AddWithValue("tolerance", tolerance);

                List<Building2D>? results = await ReadAsync(npgsqlCommand);

                if (results is not null && results.Count > 0)
                {
                    return results[0];
                }
            }

            return null;
        }

        public async Task<List<Building2D>?> GetBuilding2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
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

            await npgsqlConnection.OpenAsync();

            // 1. Get areas once
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, [AdministrativeArealType.Subdivison], tolerance);

            Dictionary<string, Building2D> dictionary = [];

            // Prepare pre-calculated boundaries for SQL performance
            double minX = boundingBox2D.Min.X - tolerance;
            double maxX = boundingBox2D.Max.X + tolerance;
            double minY = boundingBox2D.Min.Y - tolerance;
            double maxY = boundingBox2D.Max.Y + tolerance;

            // Use a single query for everything (Subdivisions OR NULLs)
            // This query uses ANY() to avoid the N+1 problem
            string commandText = @"
                SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM building_2d
                WHERE county_id = ANY(@county_ids)
                    AND (subdivision_id = ANY(@subdivision_ids) OR subdivision_id IS NULL)
                    AND @minX <= max_x AND @maxX >= min_x
                    AND @minY <= max_y AND @maxY >= min_y;";

            if (administrativeAreal2Ds is not null && administrativeAreal2Ds.Count != 0)
            {
                // Extract IDs into arrays for Npgsql
                int[] countyIds = [.. administrativeAreal2Ds.Select(a => a.Id).Distinct()];
                int[] subdivisionIds = [.. administrativeAreal2Ds.Select(a => a.Id).Distinct()];

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("county_ids", countyIds);
                npgsqlCommand.Parameters.AddWithValue("subdivision_ids", subdivisionIds);
                npgsqlCommand.Parameters.AddWithValue("minX", minX);
                npgsqlCommand.Parameters.AddWithValue("maxX", maxX);
                npgsqlCommand.Parameters.AddWithValue("minY", minY);
                npgsqlCommand.Parameters.AddWithValue("maxY", maxY);

                List<Building2D>? building2Ds = await ReadAsync(npgsqlCommand);

                if (building2Ds is not null)
                {
                    foreach (Building2D building in building2Ds)
                    {
                        // Efficiently build a unique key
                        string key = $"{building.CountyId}_{building.Reference}";
                        dictionary.TryAdd(key, building);
                    }
                }
            }

            return [.. dictionary.Values];
        }

        public async Task<List<Building2D>?> GetBuilding2DsByCircle2DAsync(Circle2D? circle2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (circle2D?.Center is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            // 1. Calculate the effective search radius (Radius + Tolerance)
            double effectiveRadius = circle2D.Radius + tolerance;

            // 2. Get administrative areas using the same logic (Point + Radius as Tolerance)
            // This reuses your existing Point-based logic for area identification
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, circle2D.GetBoundingBox(), [AdministrativeArealType.Subdivison], tolerance);

            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                return [];
            }

            Dictionary<string, Building2D> dictionary = [];

            // 3. Prepare parameters for the Point-in-BBox query
            double centerX = circle2D.Center.X;
            double centerY = circle2D.Center.Y;
            int[] countyIds = [.. administrativeAreal2Ds.Select(a => a.Id).Distinct()];
            int[] subdivisionIds = [.. administrativeAreal2Ds.Select(a => a.Id).Distinct()];

            // Using the same logic as your GetAdministrativeAreal2DsByPoint2DAsync
            // Search area: [CenterX - effectiveRadius, CenterX + effectiveRadius]
            string commandText = @"
                SELECT id, county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object, created_at
                FROM building_2d
                WHERE county_id = ANY(@county_ids)
                    AND (subdivision_id = ANY(@subdivision_ids) OR subdivision_id IS NULL)
                    AND (@x + @effectiveRadius) >= min_x AND (@x - @effectiveRadius) <= max_x
                    AND (@y + @effectiveRadius) >= min_y AND (@y - @effectiveRadius) <= max_y;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("county_ids", countyIds);
            npgsqlCommand.Parameters.AddWithValue("subdivision_ids", subdivisionIds);
            npgsqlCommand.Parameters.AddWithValue("x", centerX);
            npgsqlCommand.Parameters.AddWithValue("y", centerY);
            npgsqlCommand.Parameters.AddWithValue("effectiveRadius", effectiveRadius);

            List<Building2D>? results = await ReadAsync(npgsqlCommand);

            if (results is not null)
            {
                foreach (Building2D building in results)
                {
                    string key = $"{building.CountyId}_{building.Reference}";
                    dictionary.TryAdd(key, building);
                }
            }

            return [.. dictionary.Values];
        }

        public async Task<List<Building2D>?> GetBuilding2DsByLocationReferences(IEnumerable<LocationReference> locationReferences)
        {
            if (locationReferences is null)
            {
                return null;
            }

            if (!locationReferences.Any())
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            // 1. Prepare parameters for the batch query
            string[] references = [.. locationReferences.Select(l => l.Reference ?? string.Empty)];
            int?[] countyIds = [.. locationReferences.Select(l => l.CountyId)];

            // 2. We only fetch the 'object' column as a string.
            // DISTINCT ON ensures we don't get duplicates if the input has them.
            const string commandText = @"
                SELECT DISTINCT ON (b.reference, b.county_id) b.object
                FROM UNNEST(@refs, @counties) AS input(ref, cnty)
                INNER JOIN building_2d b ON b.reference = input.ref
                WHERE (input.cnty IS NULL OR b.county_id = input.cnty);";

            List<Building2D> result = [];

            try
            {
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("refs", references);
                npgsqlCommand.Parameters.AddWithValue("counties", countyIds);

                await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    // 3. Extract JSONB as a raw string
                    if (reader.IsDBNull(0))
                    {
                        continue;
                    }

                    // 4. Use your existing conversion logic
                    // Assuming Core.Convert.ToDiGi returns a collection or the object itself.
                    Building2D? building2D = Core.Convert.ToDiGi<Building2D>(reader.GetString(0))?.FirstOrDefault();
                    if (building2D is not null)
                    {
                        result.Add(building2D);
                    }
                }
            }
            catch (Exception)
            {
                // Log error if needed
                throw;
            }

            return result;
        }

        /// <summary>
        /// Retrieves all LocationReferences for a specific county, with an optional exclusion list.
        /// Optimized for partitioned tables using the partition key (county_id).
        /// </summary>
        /// <param name="countyId">The ID of the county (Partition Key).</param>
        /// <param name="excludedReferences">Optional collection of references to be excluded from the result.</param>
        /// <returns>A list of LocationReference objects, or null if connection fails.</returns>
        public async Task<List<LocationReference>?> GetLocationReferencesAsync(int countyId, IEnumerable<string>? excludedReferences = null, CancellationToken cancellationToken = default)
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

            string commandText = @"
                SELECT id, county_id, reference, subdivision_id
                FROM building_2d
                WHERE county_id = @countyId";

            bool hasExcluded = excludedReferences != null && excludedReferences.Any();

            if (hasExcluded)
            {
                commandText += " AND NOT (reference = ANY(@excluded))";
            }

            List<LocationReference> results = [];

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            if (hasExcluded)
            {
                // Using explicit typing and collection expression for performance
                string[] excludedArray = [.. excludedReferences!];
                npgsqlCommand.Parameters.AddWithValue("excluded", excludedArray);
            }

            // 3. Execution with token is correct
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            // 4. Reading loop with token is correct
            while (await reader.ReadAsync(cancellationToken))
            {
                LocationReference locationReference = new()
                {
                    Id = reader.GetInt64(0),
                    CountyId = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                    Reference = reader.IsDBNull(2) ? null : reader.GetString(2),
                    SubdivisionId = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3)
                };
                results.Add(locationReference);
            }

            return results;
        }

        /// <summary>
        /// Retrieves full LocationReference data from building_2d table based on input references.
        /// Performs batch processing using UNNEST to avoid N+1 query performance issues.
        /// </summary>
        /// <param name="locationReferences">Collection of partial references (must have Reference, optional CountyId).</param>
        /// <returns>A list of populated LocationReference objects found in the database.</returns>
        public async Task<List<LocationReference>?> GetLocationReferencesAsync(IEnumerable<LocationReference> locationReferences)
        {
            if (locationReferences == null)
            {
                return null;
            }

            if (!locationReferences.Any())
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
            string[] references = [.. locationReferences.Select(l => l.Reference ?? string.Empty)];
            int?[] countyIds = [.. locationReferences.Select(l => l.CountyId)];

            // SQL Logic:
            // 1. UNNEST creates a virtual table from the input arrays.
            // 2. INNER JOIN matches input with building_2d table.
            // 3. The WHERE clause handles optional CountyId (if null in input, matches only by reference).
            // 4. DISTINCT ON ensures we get only one result per input record even if multiple matches exist.
            const string commandText = @"
                SELECT DISTINCT ON (input.ref, input.cnty)
                       b.id, b.county_id, b.reference, b.subdivision_id
                FROM UNNEST(@refs, @counties) AS input(ref, cnty)
                INNER JOIN building_2d b ON b.reference = input.ref
                WHERE (input.cnty IS NULL OR b.county_id = input.cnty)
                ORDER BY input.ref, input.cnty, b.id ASC;";

            List<LocationReference> result = [];

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("refs", references);
            npgsqlCommand.Parameters.AddWithValue("counties", countyIds);

            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                // Mapping the flat row back to a LocationReference object
                LocationReference locationReference = new()
                {
                    Id = reader.GetInt64(0),
                    CountyId = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    Reference = reader.IsDBNull(2) ? null : reader.GetString(2),
                    SubdivisionId = reader.IsDBNull(3) ? null : reader.GetInt32(3)
                };
                result.Add(locationReference);
            }

            return result;
        }

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
                    FROM building_2D
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
                        if (building is null) continue;

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
                    NpgsqlBatchCommand npgsqlBatchCommand = new("UPDATE building_2D SET subdivision_id = @subdivision_id WHERE id = @id AND county_id = @county_id");
                    npgsqlBatchCommand.Parameters.AddWithValue("subdivision_id", SubdivisionId);
                    npgsqlBatchCommand.Parameters.AddWithValue("id", Id);
                    npgsqlBatchCommand.Parameters.AddWithValue("county_id", CountyId);
                    npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
                }
                await npgsqlBatch.ExecuteNonQueryAsync(cancellationToken);
            }
        }

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

            bool succeded = await PostgreSQL.Create.TableAsync_Building2D(npgsqlConnection);
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
                            if (await AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(npgsqlConnection, building2D.Code, Enums.AdministrativeArealType.County) is int countyId_Code)
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
                            GIS.Classes.Building2D? building2D_GIS = building2D.ToDiGi<GIS.Classes.Building2D>();

                            Geometry.Planar.Interfaces.IPolygonal2D? polygonal2D_Building2D = building2D_GIS?.PolygonalFace2D?.ExternalEdge;
                            if (polygonal2D_Building2D is null)
                            {
                                continue;
                            }

                            List<Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>> tuples_AdministrativeAreal2D = administrativeAreal2Ds.ConvertAll(x => new Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>(x, x.ToDiGi<GIS.Classes.AdministrativeAreal2D>()?.PolygonalFace2D?.ExternalEdge));
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

                succeded = await PostgreSQL.Create.TableAsync_Building2D_Partition(npgsqlConnection, countyId);
                if (!succeded)
                {
                    continue;
                }

                foreach (Building2D building2D in keyValuePair.Value)
                {
                    // SQL with full update on conflict (excluding ID)
                    NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO building_2D (county_id, reference, code, min_x, min_y, max_x, max_y, subdivision_id, object)
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

        private static Building2D Create(NpgsqlDataReader npgsqlDataReader)
        {
            return new Building2D
            {
                Id = npgsqlDataReader.GetInt32(0),
                CountyId = npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetInt32(1),
                Reference = npgsqlDataReader.GetString(2),
                Code = npgsqlDataReader.GetString(3),
                BoundingBox2D = new BoundingBox2D(
                        new Point2D(npgsqlDataReader.IsDBNull(4) ? double.NaN : npgsqlDataReader.GetDouble(4), npgsqlDataReader.IsDBNull(5) ? double.NaN : npgsqlDataReader.GetDouble(5)),
                        new Point2D(npgsqlDataReader.IsDBNull(6) ? double.NaN : npgsqlDataReader.GetDouble(6), npgsqlDataReader.IsDBNull(7) ? double.NaN : npgsqlDataReader.GetDouble(7))),
                SubdivisionId = npgsqlDataReader.IsDBNull(7) ? null : npgsqlDataReader.GetInt32(7),
                Object = JsonNode.Parse(npgsqlDataReader.GetString(8)) as JsonObject,
                CreatedAt = npgsqlDataReader.IsDBNull(9) ? null : npgsqlDataReader.GetDateTime(9),
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
                GIS.Classes.AdministrativeAreal2D? administrativeAreal2D_GIS = administrativeAreal2D.ToDiGi<GIS.Classes.AdministrativeAreal2D>();
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

        private static async Task<List<Building2D>?> ReadAsync(NpgsqlCommand npgsqlCommand)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();

            return await ReadAsync(npgsqlDataReader);
        }

        private static async Task<List<Building2D>> ReadAsync(NpgsqlDataReader npgsqlDataReader)
        {
            List<Building2D> result = [];

            while (await npgsqlDataReader.ReadAsync())
            {
                result.Add(Create(npgsqlDataReader));
            }

            return result;
        }
    }
}