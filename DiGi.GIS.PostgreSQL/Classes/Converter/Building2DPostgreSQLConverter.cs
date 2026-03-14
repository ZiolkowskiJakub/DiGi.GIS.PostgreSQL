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

        public async Task<bool> Clear()
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync();

            return await DiGi.PostgreSQL.Modify.Clear(npgsqlConnection, "building_2D");
        }

        public async Task<Building2D?> GetBuilding2DbyPoint2DAsync(Point2D? point2D, double tolerance = Core.Constans.Tolerance.MacroDistance)
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

        public async Task<List<Building2D>?> GetBuilding2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, double tolerance = Core.Constans.Tolerance.MacroDistance)
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

        public async Task<List<Building2D>?> GetBuilding2DsByCircle2DAsync(Circle2D? circle2D, double tolerance = Core.Constans.Tolerance.MacroDistance)
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

        public async Task<bool> RefreshAsync(Building2DPostgreSQLRefreshOptions? building2DPostgreSQLRefreshOptions = default, IProgress<long>? progress = default, CancellationToken cancellationToken = default)
        {
            building2DPostgreSQLRefreshOptions ??= new Building2DPostgreSQLRefreshOptions();

            int totalUpdated = 0;

            // Loop until all records are processed or cancellation is requested
            while (!cancellationToken.IsCancellationRequested)
            {
                await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
                if (npgsqlConnection is null)
                {
                    return false;
                }

                await npgsqlConnection.OpenAsync(cancellationToken);

                // Open a transaction to hold FOR UPDATE SKIP LOCKED locks
                await using NpgsqlTransaction npgsqlTransaction = await npgsqlConnection.BeginTransactionAsync(cancellationToken);

                // 1. Fetch a batch of records and lock them so other instances skip them
                string whereClause = building2DPostgreSQLRefreshOptions.OverrideExistingSubdivisionIds ? "" : "AND subdivision_id IS NULL ";
                string commandText_Select = $@"SELECT id, county_id, object FROM building_2D WHERE true {whereClause} FOR UPDATE SKIP LOCKED LIMIT @batchSize";

                List<(long Id, int CountyId, string Json)> ids = [];

                try
                {
                    await using (NpgsqlCommand npgsqlCommand = new(commandText_Select, npgsqlConnection, npgsqlTransaction))
                    {
                        npgsqlCommand.Parameters.AddWithValue("batchSize", building2DPostgreSQLRefreshOptions.BatchSize);

                        // Use SequentialAccess for high performance with large strings/json
                        await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(System.Data.CommandBehavior.SequentialAccess, cancellationToken);
                        while (await npgsqlDataReader.ReadAsync(cancellationToken))
                        {
                            ids.Add((npgsqlDataReader.GetInt64(0), npgsqlDataReader.GetInt32(1), npgsqlDataReader.GetString(2)));
                        }
                    } // Reader is disposed here, freeing the connection for the next command

                    // If no more records are available, exit the loop
                    if (ids.Count == 0)
                    {
                        break;
                    }

                    List<(long Id, int CountyId, int SubdivisionId)> updates = [];

                    // 2. Process data in memory using your C# DLL logic
                    foreach (var (Id, CountyId, Json) in ids)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }

                        GIS.Classes.Building2D? building = Core.Convert.ToDiGi<GIS.Classes.Building2D>(Json)?.FirstOrDefault();
                        if (building is null)
                        {
                            continue;
                        }

                        // Note: GetSubdivisionIdAsync should use the same connection if it needs DB access
                        int? subdivisionId = await GetSubdivisionIdAsync(npgsqlConnection, building, building2DPostgreSQLRefreshOptions.Tolerance);
                        if (subdivisionId.HasValue)
                        {
                            updates.Add((Id, CountyId, subdivisionId.Value));
                        }
                    }

                    // 3. Execute updates within the same transaction that holds the locks
                    if (updates.Count > 0 && !cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteUpdateBatchAsync(updates, cancellationToken);
                        totalUpdated += updates.Count;
                        progress?.Report(totalUpdated);
                    }

                    // 4. Commit releases the FOR UPDATE locks for the current batch
                    await npgsqlTransaction.CommitAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // Transaction will rollback automatically on dispose if not committed
                    return false;
                }
                catch (Exception)
                {
                    // Log error here if necessary
                    throw;
                }

                async Task ExecuteUpdateBatchAsync(List<(long Id, int CountyId, int SubdivisionId)> updates, CancellationToken cancellationToken)
                {
                    // We use NpgsqlBatch for optimal performance (sends all commands in one network roundtrip)
                    await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection, npgsqlTransaction);

                    foreach (var (Id, CountyId, SubdivisionId) in updates)
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

            return !cancellationToken.IsCancellationRequested;
        }

        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<Building2D>? building2Ds, double tolerance = Core.Constans.Tolerance.MacroDistance)
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
                            if (await AdministrativeAreal2DPostgreSQLConverter.GetIdByCode(npgsqlConnection, building2D.Code, Enums.AdministrativeArealType.County) is int countyId_Code)
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

        private static async Task<int?> GetSubdivisionIdAsync(NpgsqlConnection npgsqlConnection, GIS.Classes.Building2D? building2D, double tolerance = Core.Constans.Tolerance.MacroDistance)
        {
            if (npgsqlConnection is null || building2D?.PolygonalFace2D is not PolygonalFace2D polygonalFace2D)
            {
                return null;
            }

            List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, polygonalFace2D.GetBoundingBox(), [Enums.AdministrativeArealType.Subdivison], tolerance);
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