using DiGi.Core.Classes;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.PointCloud.Spatial.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that fills the terrain point table by sampling elevations on a regular grid.
    /// <para>The work is driven county by county rather than by one grid over the whole area. Each county's subdivisions are read once, their outlines are derived once, and the points of that county are then decided against them in memory - so a point costs no database round trip at all, where deciding it through the database costs six of them plus the deserializing of an outline of thousands of vertices.</para>
    /// <para>Every county is sampled on tiles cut from one grid shared by all of them, anchored by <see cref="PostgreSQLTerrainPointCreateTableOptions.OriginX"/> and <see cref="PostgreSQLTerrainPointCreateTableOptions.OriginY"/>. Neighbouring counties therefore produce the same coordinates for a shared point instead of two grids that do not line up, and a tile that was already sampled is recognised and skipped - so a run that was stopped resumes, and a county sampled coarsely can later be sampled finely without paying for the points it already holds.</para>
    /// <para>What remains is one request to the elevation service per point, which is the whole of the running time. <see cref="PostgreSQLTerrainPointCreateTableOptions.MaxConcurrentRequests"/> governs it.</para>
    /// <para>A run reports success unless it was cancelled. A county or a tile that cannot be sampled is counted in <see cref="FailedBatchCount"/>, logged with the exception that caused it, and stepped over - so a run of many hours is not reduced to a single word by one batch out of hundreds of thousands, and what did go wrong is named in the log rather than inferred from a count nothing reads.</para>
    /// </summary>
    public class PostgreSQLTerrainPointCreateTableTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// Bulk reads over a whole county exceed the 30 second default; allow up to 10 minutes per statement.
        /// </summary>
        private const int commandTimeout = 600;

        protected readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        protected readonly HttpClient? httpClient;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="httpClient">The HTTP client used to reach the elevation service.</param>
        /// <param name="gISPostgreSQLConverterManager">The GIS PostgreSQL converter manager used to read the areas and write the points.</param>
        public PostgreSQLTerrainPointCreateTableTask(HttpClient? httpClient, GISPostgreSQLConverterManager? gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets the number of counties and tiles that failed outright and were stepped over.
        /// <para>Each one is logged with its county, its tile and the exception that caused it, so this figure is a count of entries to go and read rather than the whole of what is known.</para>
        /// </summary>
        public long FailedBatchCount { get; private set; }

        /// <summary>
        /// Gets the number of points the database accepted.
        /// <para>Points already stored are not counted - the write leaves them as they are - so a run over a county that is already complete reports zero.</para>
        /// </summary>
        public long PointCount { get; private set; }

        /// <summary>
        /// Gets the configuration for the PostgreSQL operation.
        /// These options will be used when the task is started.
        /// </summary>
        public PostgreSQLTerrainPointCreateTableOptions PostgreSQLTerrainPointCreateTableOptions { get; set; } = new PostgreSQLTerrainPointCreateTableOptions();
        
        /// <summary>
        /// Gets the number of points the database declined to store.
        /// </summary>
        public long RejectionCount { get; private set; }

        /// <summary>
        /// Gets the number of points that lie inside a subdivision but for which the elevation service returned nothing, even after the retries allowed.
        /// <para>These are gaps in the terrain rather than points outside the sampled area. They are reported rather than gated on: a run of tens of millions of requests to a public service will meet a few, and treating that as a failed run says nothing about which points were missed. The log names the tile each one belongs to.</para>
        /// </summary>
        public long UnresolvedPointCount { get; private set; }
        
        /// <summary>
        /// Executes the background task, sampling elevations county by county and writing them to the terrain point table.
        /// </summary>
        /// <param name="progress">A progress reporter carrying the running total of points stored.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true unless the run was cancelled.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            PointCount = 0;
            UnresolvedPointCount = 0;
            RejectionCount = 0;
            FailedBatchCount = 0;

            if (httpClient is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no elevation service client - nothing can be sampled", nameof(PostgreSQLTerrainPointCreateTableTask));
                return false;
            }

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the administrative areas cannot be read", nameof(PostgreSQLTerrainPointCreateTableTask), nameof(AdministrativeAreal2DPostgreSQLConverter));
                return false;
            }

            TerrainPointPostgreSQLConverter? terrainPointPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<TerrainPointPostgreSQLConverter>();
            if (terrainPointPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the points cannot be written", nameof(PostgreSQLTerrainPointCreateTableTask), nameof(TerrainPointPostgreSQLConverter));
                return false;
            }

            double gridSize = PostgreSQLTerrainPointCreateTableOptions.GridSize;
            int tileSize = PostgreSQLTerrainPointCreateTableOptions.TileSize;
            if (double.IsNaN(gridSize) || double.IsInfinity(gridSize) || gridSize <= 0 || tileSize < 1)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: grid size {GridSize} and tile size {TileSize} do not describe a lattice", nameof(PostgreSQLTerrainPointCreateTableTask), gridSize, tileSize);
                return false;
            }

            // Capped at half a step. Neighbouring tiles are one step apart, so anything larger would let a point of one
            // tile be taken for a point of the next, both when deciding what is already stored and when deciding what contains it.
            double tolerance = PostgreSQLTerrainPointCreateTableOptions.Tolerance;
            if (double.IsNaN(tolerance) || tolerance < 0)
            {
                tolerance = 0;
            }
            else if (tolerance > gridSize / 2)
            {
                tolerance = gridSize / 2;
            }

            Point2D origin = new(PostgreSQLTerrainPointCreateTableOptions.OriginX, PostgreSQLTerrainPointCreateTableOptions.OriginY);

            int maxConcurrentRequests = PostgreSQLTerrainPointCreateTableOptions.MaxConcurrentRequests < 1 ? 1 : PostgreSQLTerrainPointCreateTableOptions.MaxConcurrentRequests;
            int retryCount = PostgreSQLTerrainPointCreateTableOptions.RetryCount < 0 ? 0 : PostgreSQLTerrainPointCreateTableOptions.RetryCount;
            double retryDelayMilliseconds = PostgreSQLTerrainPointCreateTableOptions.RetryDelayMilliseconds;
            TimeSpan retryDelay = TimeSpan.FromMilliseconds(double.IsNaN(retryDelayMilliseconds) || retryDelayMilliseconds < 0 ? 0 : retryDelayMilliseconds);

            bool overrideExisting = PostgreSQLTerrainPointCreateTableOptions.OverrideExisting;

            // The table is created up front so that a problem with the schema stops the run now rather than being
            // discovered hours later as a batch that would not write.
            await using (NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(terrainPointPostgreSQLConverter.ConnectionData))
            {
                if (npgsqlConnection is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no connection to the terrain point database could be built", nameof(PostgreSQLTerrainPointCreateTableTask));
                    return false;
                }

                await npgsqlConnection.OpenAsync(cancellationToken);

                if (!await Create.TableAsync_TerrainPoint(npgsqlConnection, commandTimeout, cancellationToken))
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: the terrain point table could not be created - the run would have no place to write to", nameof(PostgreSQLTerrainPointCreateTableTask));
                    return false;
                }
            }

            // Identifiers rather than codes: a county held in several pieces is one row per piece, each with its own
            // subdivisions, so walking the identifiers reaches all of its territory exactly once.
            HashSet<int>? countyIds = PostgreSQLTerrainPointCreateTableOptions.CountyIds;
            countyIds ??= await administrativeAreal2DPostgreSQLConverter.GetIdsAsync(Enums.AdministrativeArealType.County, cancellationToken);
            if (countyIds is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: the counties could not be read - the run cannot be scoped", nameof(PostgreSQLTerrainPointCreateTableTask));
                return false;
            }

            if (countyIds.Count == 0)
            {
                Serilog.Modify.Log("{Type}: no county was named - nothing to sample", nameof(PostgreSQLTerrainPointCreateTableTask));
                return true;
            }

            // Sorted so that a run stopped and started again covers the counties in the same order and its progress means the same thing.
            List<int> countyIds_Sorted = [.. countyIds];
            countyIds_Sorted.Sort();

            Serilog.Modify.Log(
                "{Type} started: {CountyCount} counties, grid {GridSize}, tile {TileSize}, origin {OriginX}/{OriginY}, tolerance {Tolerance}, {MaxConcurrentRequests} requests in flight, {RetryCount} retries, override existing {OverrideExisting}",
                nameof(PostgreSQLTerrainPointCreateTableTask), countyIds_Sorted.Count, gridSize, tileSize, origin.X, origin.Y, tolerance, maxConcurrentRequests, retryCount, overrideExisting);

            foreach (int countyId in countyIds_Sorted)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Read before the county rather than accumulated inside it, so that the county's own contribution
                // survives the batches that fail: the running totals are what the tallies below are subtracted from.
                long pointCount_County = PointCount;
                long unresolvedPointCount_County = UnresolvedPointCount;
                long rejectionCount_County = RejectionCount;
                long failedBatchCount_County = FailedBatchCount;

                try
                {
                    List<AdministrativeAreal2D>? administrativeAreal2Ds;

                    await using (NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(administrativeAreal2DPostgreSQLConverter.ConnectionData))
                    {
                        if (npgsqlConnection is null)
                        {
                            FailedBatchCount++;
                            Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain county skipped - county {CountyId}, no connection to the administrative database could be built", countyId);
                            continue;
                        }

                        await npgsqlConnection.OpenAsync(cancellationToken);

                        administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(npgsqlConnection, Enums.AdministrativeArealType.Subdivision, countyId, cancellationToken);
                    }

                    if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
                    {
                        Serilog.Modify.Log("Terrain county skipped - county {CountyId} has no subdivisions, so nothing decides which of its points are on land", countyId);
                        continue;
                    }

                    // Derived once for the whole county. Reaching through the rows again per point would deserialize
                    // and clone an outline of thousands of vertices every time, which is the cost this task exists to remove.
                    Dictionary<int, PolygonalFace2D> polygonalFace2Ds_ById = administrativeAreal2Ds.PolygonalFace2DsById();
                    if (polygonalFace2Ds_ById.Count == 0)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain county skipped - county {CountyId} has {Count} subdivisions but none of them carries an outline", countyId, administrativeAreal2Ds.Count);
                        continue;
                    }

                    List<BoundingBox2D> boundingBox2Ds_Subdivision = [];
                    foreach (PolygonalFace2D polygonalFace2D in polygonalFace2Ds_ById.Values)
                    {
                        if (polygonalFace2D.GetBoundingBox() is BoundingBox2D boundingBox2D)
                        {
                            boundingBox2Ds_Subdivision.Add(boundingBox2D);
                        }
                    }

                    if (boundingBox2Ds_Subdivision.Count == 0)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain county skipped - county {CountyId} has outlines but none of them bounds an area", countyId);
                        continue;
                    }

                    BoundingBox2D boundingBox2D_County = new(boundingBox2Ds_Subdivision);

                    double index_X_Min_Double = Math.Ceiling((boundingBox2D_County.Min.X - origin.X - tolerance) / gridSize);
                    double index_X_Max_Double = Math.Floor((boundingBox2D_County.Max.X - origin.X + tolerance) / gridSize);
                    double index_Y_Min_Double = Math.Ceiling((boundingBox2D_County.Min.Y - origin.Y - tolerance) / gridSize);
                    double index_Y_Max_Double = Math.Floor((boundingBox2D_County.Max.Y - origin.Y + tolerance) / gridSize);

                    if (double.IsNaN(index_X_Min_Double) || double.IsNaN(index_X_Max_Double) || double.IsNaN(index_Y_Min_Double) || double.IsNaN(index_Y_Max_Double))
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain county skipped - county {CountyId} has an outline that does not place on the lattice", countyId);
                        continue;
                    }

                    if (index_X_Max_Double < index_X_Min_Double || index_Y_Max_Double < index_Y_Min_Double)
                    {
                        Serilog.Modify.Log("Terrain county skipped - county {CountyId} is smaller than one step of the {GridSize} lattice, so it holds no node", countyId, gridSize);
                        continue;
                    }

                    if (index_X_Min_Double < int.MinValue || index_X_Max_Double > int.MaxValue || index_Y_Min_Double < int.MinValue || index_Y_Max_Double > int.MaxValue)
                    {
                        FailedBatchCount++;
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain county skipped - county {CountyId} places outside the range of the lattice index", countyId);
                        continue;
                    }

                    // Qualified: an unqualified Convert binds to this project's own Convert class, not to System's.
                    int index_X_Min = System.Convert.ToInt32(index_X_Min_Double);
                    int index_X_Max = System.Convert.ToInt32(index_X_Max_Double);
                    int index_Y_Min = System.Convert.ToInt32(index_Y_Min_Double);
                    int index_Y_Max = System.Convert.ToInt32(index_Y_Max_Double);

                    int block_X_Min = FloorDivide(index_X_Min, tileSize);
                    int block_X_Max = FloorDivide(index_X_Max, tileSize);
                    int block_Y_Min = FloorDivide(index_Y_Min, tileSize);
                    int block_Y_Max = FloorDivide(index_Y_Max, tileSize);

                    for (int block_X = block_X_Min; block_X <= block_X_Max; block_X++)
                    {
                        for (int block_Y = block_Y_Min; block_Y <= block_Y_Max; block_Y++)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            try
                            {
                                // Held in index space and closed at both ends, so a tile and its neighbour are a whole
                                // step apart and no point can fall in both.
                                int index_X_Low = Math.Max(index_X_Min, block_X * tileSize);
                                int index_X_High = Math.Min(index_X_Max, ((block_X + 1) * tileSize) - 1);
                                int index_Y_Low = Math.Max(index_Y_Min, block_Y * tileSize);
                                int index_Y_High = Math.Min(index_Y_Max, ((block_Y + 1) * tileSize) - 1);

                                if (index_X_High < index_X_Low || index_Y_High < index_Y_Low)
                                {
                                    continue;
                                }

                                BoundingBox2D boundingBox2D_Tile = new(
                                    new Point2D(origin.X + (index_X_Low * gridSize), origin.Y + (index_Y_Low * gridSize)),
                                    new Point2D(origin.X + (index_X_High * gridSize), origin.Y + (index_Y_High * gridSize)));

                                // A county's outline is rarely a rectangle, so a good share of its tiles hold no land at
                                // all. Those cost nothing here rather than a read and a write that find nothing.
                                bool intersects = false;
                                foreach (BoundingBox2D boundingBox2D_Subdivision in boundingBox2Ds_Subdivision)
                                {
                                    if (boundingBox2D_Tile.InRange(boundingBox2D_Subdivision, tolerance))
                                    {
                                        intersects = true;
                                        break;
                                    }
                                }

                                if (!intersects)
                                {
                                    continue;
                                }

                                HashSet<(int, int)> indexes_Stored = [];
                                if (!overrideExisting)
                                {
                                    PointCloud3D? pointCloud3D = await terrainPointPostgreSQLConverter.GetPointCloud3DByBoundingBox2DAsync(boundingBox2D_Tile, countyId, null, tolerance, commandTimeout, cancellationToken);
                                    if (pointCloud3D is not null)
                                    {
                                        for (int i = 0; i < pointCloud3D.Count; i++)
                                        {
                                            if (!pointCloud3D.TryGetPoint(i, out double x, out double y, out double _))
                                            {
                                                continue;
                                            }

                                            // Points that are not nodes of this grid are left out rather than rounded onto
                                            // one. The table is shared with imports whose coordinates follow no grid, and
                                            // rounding those in would suppress nodes that were never sampled.
                                            if (new Point2D(x, y).TryGetGridIndex(origin, gridSize, gridSize, out int index_X_Stored, out int index_Y_Stored, tolerance))
                                            {
                                                indexes_Stored.Add((index_X_Stored, index_Y_Stored));
                                            }
                                        }
                                    }
                                }

                                List<Point2D>? point2Ds_Tile = boundingBox2D_Tile.Point2Ds(origin, gridSize, gridSize, tolerance);
                                if (point2Ds_Tile is null || point2Ds_Tile.Count == 0)
                                {
                                    continue;
                                }

                                List<Point2D> point2Ds_Remaining;
                                if (indexes_Stored.Count == 0)
                                {
                                    point2Ds_Remaining = point2Ds_Tile;
                                }
                                else
                                {
                                    point2Ds_Remaining = [];
                                    foreach (Point2D point2D in point2Ds_Tile)
                                    {
                                        if (point2D.TryGetGridIndex(origin, gridSize, gridSize, out int index_X_Point, out int index_Y_Point, tolerance) && indexes_Stored.Contains((index_X_Point, index_Y_Point)))
                                        {
                                            continue;
                                        }

                                        point2Ds_Remaining.Add(point2D);
                                    }
                                }

                                if (point2Ds_Remaining.Count == 0)
                                {
                                    continue;
                                }

                                int?[]? subdivisionIds = polygonalFace2Ds_ById.IdsByPoint2Ds(point2Ds_Remaining, tolerance);
                                if (subdivisionIds is null)
                                {
                                    continue;
                                }

                                List<Point2D> point2Ds_Kept = [];
                                List<int> subdivisionIds_Kept = [];
                                for (int i = 0; i < point2Ds_Remaining.Count; i++)
                                {
                                    // A point in no subdivision is outside the county's land. A rectangle laid over an
                                    // irregular outline always holds some, so this is expected rather than a failure.
                                    if (subdivisionIds[i] is not int subdivisionId)
                                    {
                                        continue;
                                    }

                                    point2Ds_Kept.Add(point2Ds_Remaining[i]);
                                    subdivisionIds_Kept.Add(subdivisionId);
                                }

                                if (point2Ds_Kept.Count == 0)
                                {
                                    continue;
                                }

                                List<Point3D?>? point3Ds = await GIS.Query.ElevationsAsync(httpClient, point2Ds_Kept, maxConcurrentRequests, retryCount, retryDelay, cancellationToken);
                                if (point3Ds is null)
                                {
                                    FailedBatchCount++;
                                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain tile not sampled - county {CountyId}, block {BlockX}/{BlockY}, the elevation service returned nothing for {PointCount} points", countyId, block_X, block_Y, point2Ds_Kept.Count);
                                    continue;
                                }

                                List<TerrainPoint> terrainPoints = [];
                                for (int i = 0; i < point3Ds.Count; i++)
                                {
                                    if (point3Ds[i] is not Point3D point3D)
                                    {
                                        UnresolvedPointCount++;
                                        continue;
                                    }

                                    terrainPoints.Add(new TerrainPoint(countyId, point3D, subdivisionIds_Kept[i]));
                                }

                                if (terrainPoints.Count == 0)
                                {
                                    continue;
                                }

                                TerrainPointUpdateResult? terrainPointUpdateResult = await terrainPointPostgreSQLConverter.UpdateAsync(terrainPoints, binaryInsert: false, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                                if (terrainPointUpdateResult is null)
                                {
                                    FailedBatchCount++;
                                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Terrain tile not written - county {CountyId}, block {BlockX}/{BlockY}, {PointCount} points sampled but the write returned nothing", countyId, block_X, block_Y, terrainPoints.Count);
                                    continue;
                                }

                                PointCount += terrainPointUpdateResult.Count;
                                RejectionCount += terrainPointUpdateResult.Rejections.Count;

                                progress.Report(PointCount);
                            }
                            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                            {
                                throw;
                            }
                            catch (Exception exception)
                            {
                                // One tile that will not sample should not end the run: it is counted and stepped over,
                                // and the points it holds are picked up whenever the county is sampled again. It is
                                // logged rather than only counted, because a count says a tile failed and nothing about
                                // why - and a run of hours produces the count long after the condition has passed.
                                FailedBatchCount++;
                                Serilog.Modify.Log(exception, "Terrain tile failed - county {CountyId}, block {BlockX}/{BlockY}", countyId, block_X, block_Y);
                                continue;
                            }
                        }
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    FailedBatchCount++;
                    Serilog.Modify.Log(exception, "Terrain county failed - county {CountyId}", countyId);
                    continue;
                }

                Serilog.Modify.Log(
                    "Terrain county {CountyId} done: {PointCount} points stored, {UnresolvedPointCount} unresolved, {RejectionCount} rejected, {FailedBatchCount} batches stepped over",
                    countyId, PointCount - pointCount_County, UnresolvedPointCount - unresolvedPointCount_County, RejectionCount - rejectionCount_County, FailedBatchCount - failedBatchCount_County);
            }

            bool cancelled = cancellationToken.IsCancellationRequested;

            Serilog.Modify.Log(
                cancelled ? Serilog.Enums.LogEventLevel.Warning : Serilog.Enums.LogEventLevel.Information,
                "{Type} finished{Cancelled}: {PointCount} points stored, {UnresolvedPointCount} unresolved, {RejectionCount} rejected, {FailedBatchCount} batches stepped over, over {CountyCount} counties",
                nameof(PostgreSQLTerrainPointCreateTableTask), cancelled ? " after being cancelled" : string.Empty, PointCount, UnresolvedPointCount, RejectionCount, FailedBatchCount, countyIds_Sorted.Count);

            // Cancellation is the only thing that makes a run unsuccessful. The three tallies are the record of what
            // went wrong, not the gate on whether anything did: one point out of tens of millions that the elevation
            // service would not answer for is a statistic, and gating on it reported a run of thirty hours as a
            // failure while saying nothing about which point, which county or which hour.
            return !cancelled;
        }

        /// <summary>
        /// Divides rounding towards negative infinity, so that indexes below the origin fall into blocks the same way indexes above it do.
        /// </summary>
        /// <param name="value">The value to divide.</param>
        /// <param name="divisor">The divisor, which has to be greater than zero.</param>
        /// <returns>The quotient rounded towards negative infinity.</returns>
        private static int FloorDivide(int value, int divisor)
        {
            int quotient = value / divisor;

            return value % divisor != 0 && value < 0 ? quotient - 1 : quotient;
        }
    }
}
