using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.PointCloud.Spatial.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides functionality for converting, querying, and managing <see cref="TerrainPoint"/> entities and <see cref="PointCloud3D"/> point cloud data within a PostgreSQL database.
    /// </summary>
    public class TerrainPointPostgreSQLConverter : PostgreSQLConverter<TerrainPoint>, IGISPostgreSQLConverter<TerrainPoint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPointPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData"/> containing the connection settings required to establish a connection to the PostgreSQL database. This value can be null.</param>
        public TerrainPointPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Asynchronously clears all records from the terrain point table or a specific county partition.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database.</param>
        /// <param name="countyId">The optional integer identifier of the county partition to clear. If null, the entire table is cleared.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the operation succeeded; otherwise, false.</returns>
        public static async Task<bool> ClearAsync(NpgsqlConnection? npgsqlConnection, int? countyId = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName(countyId), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously clears all records from the terrain point table or a specific county partition, automatically managing the connection.
        /// </summary>
        /// <param name="countyId">The optional integer identifier of the county partition to clear. If null, the entire table is cleared.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> ClearAsync(int? countyId = null, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await ClearAsync(npgsqlConnection, countyId, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the exact count of records from the database, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier of the county partition used to filter the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a <see cref="long"/>, or -1 when the table or partition does not exist.</returns>
        public static async Task<long> GetCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, TableName(countyId), cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the exact count of records from the database, optionally filtered by a specific county identifier, automatically managing the connection.
        /// </summary>
        /// <param name="countyId">The optional integer identifier of the county partition used to filter the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count as a <see cref="long"/>, or -1 when the table or partition does not exist.</returns>
        public async Task<long> GetCountAsync(int? countyId = null, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetCountAsync(npgsqlConnection, countyId, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated count of records, optionally filtered by a specific county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="countyId">The optional integer identifier for the county; if null, the estimate is read from the partitioned parent.</param>
        /// <param name="analyze">A value indicating whether to perform an ANALYZE operation on the database table to update statistics before retrieving the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated count as a <see cref="long"/>, or -1 when the table or partition does not exist.</returns>
        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, int? countyId = null, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, TableName(countyId), analyze, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves an estimated count of records, optionally filtered by a specific county identifier, automatically managing the connection.
        /// </summary>
        /// <param name="countyId">The optional integer identifier for the county; if null, the estimate is read from the partitioned parent.</param>
        /// <param name="analyze">A value indicating whether to perform an ANALYZE operation on the database table to update statistics before retrieving the count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the estimated count as a <see cref="long"/>, or -1 when the table or partition does not exist.</returns>
        public async Task<long> GetEstimatedCountAsync(int? countyId = null, bool analyze = false, CancellationToken cancellationToken = default)
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
        /// Asynchronously retrieves the estimated row count for the specified county identifiers in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> to use for the query.</param>
        /// <param name="countyIds">A collection of integers representing the county identifiers to estimate counts for. When null, the estimate is read from the partitioned parent instead.</param>
        /// <param name="analyze">A boolean indicating whether to run an analysis operation before fetching the estimated count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total estimated row count as a <see cref="long"/>. Counties with no partition contribute nothing rather than subtracting from the total.</returns>
        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int>? countyIds, bool analyze = false, CancellationToken cancellationToken = default)
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
                long count = await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, TableName(countyId), analyze, cancellationToken);

                // A county that has never been imported has no partition, and the query answers -1 for it.
                // Added to the running total that would silently subtract one row per missing county, so a
                // set of counties half of which are absent would report a plausible but wrong figure.
                if (count > 0)
                {
                    result += count;
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves the estimated row count for the specified county identifiers in the PostgreSQL database, automatically managing the connection.
        /// </summary>
        /// <param name="countyIds">A collection of integers representing the county identifiers to estimate counts for. When null, the estimate is read from the partitioned parent instead.</param>
        /// <param name="analyze">A boolean indicating whether to run an analysis operation before fetching the estimated count.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total estimated row count as a <see cref="long"/>.</returns>
        public async Task<long> GetEstimatedCountAsync(IEnumerable<int>? countyIds, bool analyze = false, CancellationToken cancellationToken = default)
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
        /// Asynchronously writes a <see cref="PointCloud3D"/> to the database for a specific county, creating the table and the county partition first.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="countyId">The integer identifier of the county partition.</param>
        /// <param name="pointCloud3D">The <see cref="PointCloud3D"/> containing the point coordinates to write.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision.</param>
        /// <param name="binaryInsert">Opts into a PostgreSQL binary COPY, which is much faster for large clouds but cannot skip points the table already holds - a repeated import, or two source tiles overlapping, fails on the primary key. Left at its default the points are sent as a single array-valued INSERT that leaves existing points as they are, so the write can be repeated safely. Set it true only for data known to carry no point already stored.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task whose result carries the number of points stored and any rejections, or null when the connection is null or the table could not be created.</returns>
        public static async Task<TerrainPointUpdateResult?> UpdateAsync(NpgsqlConnection? npgsqlConnection, int countyId, PointCloud3D? pointCloud3D, int? subdivisionId = null, bool binaryInsert = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            if (!await Create.TableAsync_TerrainPoint(npgsqlConnection, cancellationToken: cancellationToken))
            {
                return null;
            }

            if (pointCloud3D is null || pointCloud3D.Count == 0)
            {
                return new TerrainPointUpdateResult(0, null);
            }

            if (!await Create.TableAsync_TerrainPoint_Partition(npgsqlConnection, countyId, cancellationToken))
            {
                return new TerrainPointUpdateResult(0, [new Rejection(countyId.ToString(), UpdateRejectionReason.PartitionUnavailable)]);
            }

            long count;
            if (binaryInsert)
            {
                DateTime createdAt = DateTime.UtcNow;

                await using NpgsqlBinaryImporter npgsqlBinaryImporter = await npgsqlConnection.BeginBinaryImportAsync(CopyCommandText(countyId), cancellationToken);

                int count_Points = pointCloud3D.Count;
                for (int i = 0; i < count_Points; i++)
                {
                    if (!pointCloud3D.TryGetPoint(i, out double x, out double y, out double z))
                    {
                        continue;
                    }

                    await WriteRowAsync(npgsqlBinaryImporter, countyId, subdivisionId, x, y, z, createdAt, cancellationToken);
                }

                count = (long)await npgsqlBinaryImporter.CompleteAsync(cancellationToken);
            }
            else
            {
                double[]? x = pointCloud3D.GetX();
                double[]? y = pointCloud3D.GetY();
                double[]? z = pointCloud3D.GetZ();

                if (x is null || y is null || z is null)
                {
                    return new TerrainPointUpdateResult(0, null);
                }

                int[] countyIds = new int[x.Length];
                int?[] subdivisionIds = new int?[x.Length];
                DateTime[] createdAts = new DateTime[x.Length];

                DateTime createdAt = DateTime.UtcNow;
                for (int i = 0; i < x.Length; i++)
                {
                    countyIds[i] = countyId;
                    subdivisionIds[i] = subdivisionId;
                    createdAts[i] = createdAt;
                }

                count = await InsertArraysAsync(npgsqlConnection, countyIds, subdivisionIds, x, y, z, createdAts, cancellationToken);
            }

            return new TerrainPointUpdateResult(count, null);
        }

        /// <summary>
        /// Asynchronously writes a <see cref="PointCloud3D"/> to the database for a specific county, automatically managing the connection.
        /// </summary>
        /// <param name="countyId">The integer identifier of the county partition.</param>
        /// <param name="pointCloud3D">The <see cref="PointCloud3D"/> containing the point coordinates to write.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision.</param>
        /// <param name="binaryInsert">Opts into a PostgreSQL binary COPY, which is much faster for large clouds but cannot skip points the table already holds - a repeated import, or two source tiles overlapping, fails on the primary key. Left at its default the points are sent as a single array-valued INSERT that leaves existing points as they are, so the write can be repeated safely. Set it true only for data known to carry no point already stored.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task whose result carries the number of points stored and any rejections, or null when no connection could be opened.</returns>
        public async Task<TerrainPointUpdateResult?> UpdateAsync(int countyId, PointCloud3D? pointCloud3D, int? subdivisionId = null, bool binaryInsert = false, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await UpdateAsync(npgsqlConnection, countyId, pointCloud3D, subdivisionId, binaryInsert, cancellationToken);
        }

        /// <summary>
        /// Asynchronously writes a collection of <see cref="TerrainPoint"/> entities to the database, creating the table and every county partition first.
        /// <para>Points carrying no county or no geometry are recorded in <see cref="TerrainPointUpdateResult.Rejections"/> one by one; a county whose partition cannot be created contributes a single rejection naming the county, because a terrain batch runs to millions of points and one rejection each would cost more than the batch itself.</para>
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="terrainPoints">The collection of <see cref="TerrainPoint"/> objects to write.</param>
        /// <param name="binaryInsert">Opts into a PostgreSQL binary COPY, one per county, which is much faster for large collections but cannot skip points the table already holds - a repeated import, or two source tiles overlapping, fails on the primary key. Left at its default the points are sent as a single array-valued INSERT that leaves existing points as they are, so the write can be repeated safely. Set it true only for data known to carry no point already stored.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task whose result carries the number of points stored and any rejections, or null when the connection is null or the table could not be created.</returns>
        public static async Task<TerrainPointUpdateResult?> UpdateAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<TerrainPoint>? terrainPoints, bool binaryInsert = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            if (!await Create.TableAsync_TerrainPoint(npgsqlConnection, cancellationToken: cancellationToken))
            {
                return null;
            }

            List<Rejection> rejections = [];

            if (terrainPoints is null)
            {
                return new TerrainPointUpdateResult(0, rejections);
            }

            // Materialised once. The source is walked twice below - to sort the points into counties and
            // then to write them - and an IEnumerable that recomputes itself would do the work twice too.
            List<TerrainPoint> terrainPoints_Accepted = [];
            HashSet<int> countyIds_Required = [];

            foreach (TerrainPoint terrainPoint in terrainPoints)
            {
                if (terrainPoint is null)
                {
                    rejections.Add(new Rejection(null, UpdateRejectionReason.Undefined));
                    continue;
                }

                if (!terrainPoint.CountyId.HasValue)
                {
                    rejections.Add(new Rejection(null, UpdateRejectionReason.CountyUnresolved));
                    continue;
                }

                if (terrainPoint.Point3D is null)
                {
                    rejections.Add(new Rejection(terrainPoint.CountyId.Value.ToString(), UpdateRejectionReason.MissingGeometry));
                    continue;
                }

                terrainPoints_Accepted.Add(terrainPoint);
                countyIds_Required.Add(terrainPoint.CountyId.Value);
            }

            // The partitions are created before the transaction opens. Partition creation takes an exclusive
            // lock on the parent, and holding that for the length of a multi-million-point import would
            // block every reader of every county until the import finished.
            HashSet<int> countyIds_Unavailable = [];
            foreach (int countyId in countyIds_Required)
            {
                if (!await Create.TableAsync_TerrainPoint_Partition(npgsqlConnection, countyId, cancellationToken))
                {
                    countyIds_Unavailable.Add(countyId);
                    rejections.Add(new Rejection(countyId.ToString(), UpdateRejectionReason.PartitionUnavailable));
                }
            }

            if (countyIds_Unavailable.Count != 0)
            {
                terrainPoints_Accepted = [.. terrainPoints_Accepted.Where(x => !countyIds_Unavailable.Contains(x.CountyId!.Value))];
            }

            if (terrainPoints_Accepted.Count == 0)
            {
                return new TerrainPointUpdateResult(0, rejections);
            }

            long count = 0;
            if (binaryInsert)
            {
                DateTime createdAt_Default = DateTime.UtcNow;

                // One COPY per county: a binary import names a single table, and the points are streamed
                // into that county's partition rather than through the partitioned parent.
                foreach (IGrouping<int, TerrainPoint> grouping in terrainPoints_Accepted.GroupBy(x => x.CountyId!.Value))
                {
                    await using NpgsqlBinaryImporter npgsqlBinaryImporter = await npgsqlConnection.BeginBinaryImportAsync(CopyCommandText(grouping.Key), cancellationToken);

                    foreach (TerrainPoint terrainPoint in grouping)
                    {
                        Point3D point3D = terrainPoint.Point3D!;

                        await WriteRowAsync(
                            npgsqlBinaryImporter,
                            grouping.Key,
                            terrainPoint.SubdivisionId,
                            point3D.X,
                            point3D.Y,
                            point3D.Z,
                            terrainPoint.CreatedAt ?? createdAt_Default,
                            cancellationToken);
                    }

                    count += (long)await npgsqlBinaryImporter.CompleteAsync(cancellationToken);
                }
            }
            else
            {
                int count_Points = terrainPoints_Accepted.Count;

                int[] countyIds = new int[count_Points];
                int?[] subdivisionIds = new int?[count_Points];
                double[] x = new double[count_Points];
                double[] y = new double[count_Points];
                double[] z = new double[count_Points];
                DateTime[] createdAts = new DateTime[count_Points];

                DateTime createdAt_Default = DateTime.UtcNow;
                for (int i = 0; i < count_Points; i++)
                {
                    TerrainPoint terrainPoint = terrainPoints_Accepted[i];
                    Point3D point3D = terrainPoint.Point3D!;

                    countyIds[i] = terrainPoint.CountyId!.Value;
                    subdivisionIds[i] = terrainPoint.SubdivisionId;
                    x[i] = point3D.X;
                    y[i] = point3D.Y;
                    z[i] = point3D.Z;
                    createdAts[i] = terrainPoint.CreatedAt ?? createdAt_Default;
                }

                count = await InsertArraysAsync(npgsqlConnection, countyIds, subdivisionIds, x, y, z, createdAts, cancellationToken);
            }

            return new TerrainPointUpdateResult(count, rejections);
        }

        /// <summary>
        /// Asynchronously writes a collection of <see cref="TerrainPoint"/> entities to the database, automatically managing the connection.
        /// </summary>
        /// <param name="terrainPoints">The collection of <see cref="TerrainPoint"/> objects to write.</param>
        /// <param name="binaryInsert">Opts into a PostgreSQL binary COPY, one per county, which is much faster for large collections but cannot skip points the table already holds - a repeated import, or two source tiles overlapping, fails on the primary key. Left at its default the points are sent as a single array-valued INSERT that leaves existing points as they are, so the write can be repeated safely. Set it true only for data known to carry no point already stored.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task whose result carries the number of points stored and any rejections, or null when no connection could be opened.</returns>
        public async Task<TerrainPointUpdateResult?> UpdateAsync(IEnumerable<TerrainPoint>? terrainPoints, bool binaryInsert = false, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await UpdateAsync(npgsqlConnection, terrainPoints, binaryInsert, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> of the terrain points lying within a distance of the specified plan coordinate.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="point2D">The <see cref="Point2D"/> coordinate to search around. This value can be null.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The half-width, in model units, of the square searched around the coordinate. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>, which is a millimetre - well below the spacing of an elevation grid, so a caller wanting the points near a coordinate has to pass a distance of its own.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the matching <see cref="PointCloud3D"/>, or null if no points are found within the distance or if the provided point or county identifiers are null.</returns>
        public static async Task<PointCloud3D?> GetPointCloud3DByPoint2DAsync(NpgsqlConnection? npgsqlConnection, Point2D? point2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || point2D is null)
            {
                return null;
            }

            BoundingBox2D boundingBox2D = new(point2D, point2D);
            return await GetPointCloud3DByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyIds, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> of the terrain points lying within a distance of the specified plan coordinate, automatically managing the connection.
        /// </summary>
        /// <param name="point2D">The <see cref="Point2D"/> coordinate to search around. This value can be null.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The half-width, in model units, of the square searched around the coordinate. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>, which is a millimetre - well below the spacing of an elevation grid, so a caller wanting the points near a coordinate has to pass a distance of its own.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the matching <see cref="PointCloud3D"/>, or null if no points are found within the distance or if the provided point or county identifiers are null.</returns>
        public async Task<PointCloud3D?> GetPointCloud3DByPoint2DAsync(Point2D? point2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
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
            return await GetPointCloud3DByPoint2DAsync(npgsqlConnection, point2D, countyIds, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> within the specified 2D bounding box for a specific county partition.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyId">The integer identifier of the county partition to query.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision to filter by.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the <see cref="PointCloud3D"/> within the bounding box, or null if no points match.</returns>
        public static async Task<PointCloud3D?> GetPointCloud3DByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, int countyId, int? subdivisionId = null, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || boundingBox2D is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT x, y, z
                FROM {Constants.TableName.TerrainPoint}
                WHERE county_id = @countyId
                  AND point(x, y) <@ box(point(@minX, @minY), point(@maxX, @maxY))
                  AND (@subdivisionId IS NULL OR subdivision_id = @subdivisionId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            AddBoundingBox2DParameters(npgsqlCommand, boundingBox2D, tolerance);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivisionId", NpgsqlDbType.Integer) { Value = (object?)subdivisionId ?? DBNull.Value });

            return await ReadAsync_PointCloud3D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> within the specified 2D bounding box for a specific county partition, automatically managing the connection.
        /// </summary>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyId">The integer identifier of the county partition to query.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision to filter by.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the <see cref="PointCloud3D"/> within the bounding box, or null if no points match.</returns>
        public async Task<PointCloud3D?> GetPointCloud3DByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, int countyId, int? subdivisionId = null, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
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
            return await GetPointCloud3DByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyId, subdivisionId, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> within the specified 2D bounding box across the given counties.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the combined <see cref="PointCloud3D"/> across the given county partitions, or null if no county was given or no points match.</returns>
        public static async Task<PointCloud3D?> GetPointCloud3DByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || boundingBox2D is null || countyIds is null)
            {
                return null;
            }

            List<PointCloud3D?> pointCloud3Ds = [];
            foreach (int countyId in countyIds)
            {
                pointCloud3Ds.Add(await GetPointCloud3DByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyId, null, tolerance, cancellationToken));
            }

            // Concatenation is plain geometry, and the factory does it in one block copy per county.
            // Gathering the axes here through GetX/GetY/GetZ copied each county three times over.
            return DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(pointCloud3Ds);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> within the specified 2D bounding box across the given counties, automatically managing the connection.
        /// </summary>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the combined <see cref="PointCloud3D"/> across the given county partitions, or null if no county was given or no points match.</returns>
        public async Task<PointCloud3D?> GetPointCloud3DByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (boundingBox2D is null || countyIds is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetPointCloud3DByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyIds, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> of the terrain points lying inside the specified circle, for a specific county partition.
        /// <para>The circle is evaluated by the database, not by filtering a cloud that was fetched as a square: the points outside it are never read, never transferred and never allocated. PostgreSQL's point operator class supports containment in a circle on the same GiST index the bounding box queries use, so this is a narrower filter rather than a slower one.</para>
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area.</param>
        /// <param name="countyId">The integer identifier of the county partition to query.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision to filter by.</param>
        /// <param name="tolerance">The distance the search radius is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the <see cref="PointCloud3D"/> inside the circle, or null if the circle is null or degenerate, or no points match.</returns>
        public static async Task<PointCloud3D?> GetPointCloud3DByCircle2DAsync(NpgsqlConnection? npgsqlConnection, Circle2D? circle2D, int countyId, int? subdivisionId = null, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || circle2D is null || !IsQueryable(circle2D))
            {
                return null;
            }

            string commandText = $@"
                SELECT x, y, z
                FROM {Constants.TableName.TerrainPoint}
                WHERE county_id = @countyId
                  AND point(x, y) <@ circle(point(@centerX, @centerY), @radius)
                  AND (@subdivisionId IS NULL OR subdivision_id = @subdivisionId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            AddCircle2DParameters(npgsqlCommand, circle2D, tolerance);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivisionId", NpgsqlDbType.Integer) { Value = (object?)subdivisionId ?? DBNull.Value });

            return await ReadAsync_PointCloud3D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> of the terrain points lying inside the specified circle, for a specific county partition, automatically managing the connection.
        /// </summary>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area.</param>
        /// <param name="countyId">The integer identifier of the county partition to query.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision to filter by.</param>
        /// <param name="tolerance">The distance the search radius is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the <see cref="PointCloud3D"/> inside the circle, or null if the circle is null or degenerate, or no points match.</returns>
        public async Task<PointCloud3D?> GetPointCloud3DByCircle2DAsync(Circle2D? circle2D, int countyId, int? subdivisionId = null, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (circle2D is null || !IsQueryable(circle2D))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetPointCloud3DByCircle2DAsync(npgsqlConnection, circle2D, countyId, subdivisionId, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> of the terrain points lying inside the specified circle, across the given counties.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The distance the search radius is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the combined <see cref="PointCloud3D"/> across the given county partitions, or null if the circle is null or degenerate, no county was given, or no points match.</returns>
        public static async Task<PointCloud3D?> GetPointCloud3DByCircle2DAsync(NpgsqlConnection? npgsqlConnection, Circle2D? circle2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || circle2D is null || countyIds is null || !IsQueryable(circle2D))
            {
                return null;
            }

            List<PointCloud3D?> pointCloud3Ds = [];
            foreach (int countyId in countyIds)
            {
                pointCloud3Ds.Add(await GetPointCloud3DByCircle2DAsync(npgsqlConnection, circle2D, countyId, null, tolerance, cancellationToken));
            }

            return DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(pointCloud3Ds);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="PointCloud3D"/> of the terrain points lying inside the specified circle, across the given counties, automatically managing the connection.
        /// </summary>
        /// <param name="circle2D">The <see cref="Circle2D"/> defining the search area.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The distance the search radius is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task containing the combined <see cref="PointCloud3D"/> across the given county partitions, or null if the circle is null or degenerate, no county was given, or no points match.</returns>
        public async Task<PointCloud3D?> GetPointCloud3DByCircle2DAsync(Circle2D? circle2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (circle2D is null || countyIds is null || !IsQueryable(circle2D))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetPointCloud3DByCircle2DAsync(npgsqlConnection, circle2D, countyIds, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="TerrainPoint"/> records within the specified 2D bounding box for a specific county partition.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyId">The integer identifier of the county partition to query.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision to filter by.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="TerrainPoint"/> objects, or null if no records match.</returns>
        public static async Task<List<TerrainPoint>?> GetTerrainPointsByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, int countyId, int? subdivisionId = null, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || boundingBox2D is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT county_id, subdivision_id, x, y, z, created_at
                FROM {Constants.TableName.TerrainPoint}
                WHERE county_id = @countyId
                  AND point(x, y) <@ box(point(@minX, @minY), point(@maxX, @maxY))
                  AND (@subdivisionId IS NULL OR subdivision_id = @subdivisionId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            AddBoundingBox2DParameters(npgsqlCommand, boundingBox2D, tolerance);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivisionId", NpgsqlDbType.Integer) { Value = (object?)subdivisionId ?? DBNull.Value });

            return await ReadAsync_TerrainPoint(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="TerrainPoint"/> records within the specified 2D bounding box for a specific county partition, automatically managing the connection.
        /// </summary>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyId">The integer identifier of the county partition to query.</param>
        /// <param name="subdivisionId">The optional integer identifier of the subdivision to filter by.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="TerrainPoint"/> objects, or null if no records match.</returns>
        public async Task<List<TerrainPoint>?> GetTerrainPointsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, int countyId, int? subdivisionId = null, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
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
            return await GetTerrainPointsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyId, subdivisionId, tolerance, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="TerrainPoint"/> records within the specified 2D bounding box across the given counties.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all <see cref="TerrainPoint"/> objects across the given counties, or null if no county was given or no records match.</returns>
        public static async Task<List<TerrainPoint>?> GetTerrainPointsByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || boundingBox2D is null || countyIds is null)
            {
                return null;
            }

            List<TerrainPoint> result = [];

            foreach (int countyId in countyIds)
            {
                List<TerrainPoint>? terrainPoints = await GetTerrainPointsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyId, null, tolerance, cancellationToken);
                if (terrainPoints is not null && terrainPoints.Count != 0)
                {
                    result.AddRange(terrainPoints);
                }
            }

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a list of <see cref="TerrainPoint"/> records within the specified 2D bounding box across the given counties, automatically managing the connection.
        /// </summary>
        /// <param name="boundingBox2D">The <see cref="BoundingBox2D"/> defining the search area.</param>
        /// <param name="countyIds">The identifiers of the county partitions to search. The terrain table is partitioned by county and this database holds no administrative geometry to derive them from, so they are the caller's to supply.</param>
        /// <param name="tolerance">The distance the search bounding box is expanded by. Defaults to <see cref="Core.Constants.Tolerance.MacroDistance"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all <see cref="TerrainPoint"/> objects across the given counties, or null if no county was given or no records match.</returns>
        public async Task<List<TerrainPoint>?> GetTerrainPointsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, IEnumerable<int>? countyIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (boundingBox2D is null || countyIds is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetTerrainPointsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, countyIds, tolerance, cancellationToken);
        }

        private static string TableName(int? countyId)
        {
            if (countyId.HasValue)
            {
                return $"{Constants.TableName.TerrainPoint}_{countyId.Value}";
            }

            return Constants.TableName.TerrainPoint;
        }

        private static string CopyCommandText(int countyId)
        {
            return $@"
                COPY {TableName(countyId)} (county_id, subdivision_id, x, y, z, created_at)
                FROM STDIN (FORMAT BINARY)";
        }

        private static async Task WriteRowAsync(NpgsqlBinaryImporter npgsqlBinaryImporter, int countyId, int? subdivisionId, double x, double y, double z, DateTime createdAt, CancellationToken cancellationToken)
        {
            await npgsqlBinaryImporter.StartRowAsync(cancellationToken);
            await npgsqlBinaryImporter.WriteAsync(countyId, NpgsqlDbType.Integer, cancellationToken);

            if (subdivisionId.HasValue)
            {
                await npgsqlBinaryImporter.WriteAsync(subdivisionId.Value, NpgsqlDbType.Integer, cancellationToken);
            }
            else
            {
                await npgsqlBinaryImporter.WriteNullAsync(cancellationToken);
            }

            await npgsqlBinaryImporter.WriteAsync(x, NpgsqlDbType.Double, cancellationToken);
            await npgsqlBinaryImporter.WriteAsync(y, NpgsqlDbType.Double, cancellationToken);
            await npgsqlBinaryImporter.WriteAsync(z, NpgsqlDbType.Double, cancellationToken);
            await npgsqlBinaryImporter.WriteAsync(createdAt, NpgsqlDbType.TimestampTz, cancellationToken);
        }

        private static async Task<long> InsertArraysAsync(NpgsqlConnection npgsqlConnection, int[] countyIds, int?[] subdivisionIds, double[] x, double[] y, double[] z, DateTime[] createdAts, CancellationToken cancellationToken)
        {
            // One statement carrying six arrays, rather than one statement per point. A cloud of a million
            // points would otherwise become a million commands with five parameters each, all built and
            // held in memory before any of them reaches the server.
            string commandText = $@"
                INSERT INTO {Constants.TableName.TerrainPoint} (county_id, subdivision_id, x, y, z, created_at)
                SELECT t.county_id, t.subdivision_id, t.x, t.y, t.z, t.created_at
                FROM unnest(@countyIds, @subdivisionIds, @x, @y, @z, @createdAts)
                    AS t(county_id, subdivision_id, x, y, z, created_at)
                ON CONFLICT (county_id, x, y) DO NOTHING;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyIds", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = countyIds });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("subdivisionIds", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = subdivisionIds });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("x", NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = x });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("y", NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = y });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("z", NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = z });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("createdAts", NpgsqlDbType.Array | NpgsqlDbType.TimestampTz) { Value = createdAts });

            return await npgsqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        private static void AddBoundingBox2DParameters(NpgsqlCommand npgsqlCommand, BoundingBox2D boundingBox2D, double tolerance)
        {
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("minX", NpgsqlDbType.Double) { Value = boundingBox2D.Min.X - tolerance });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("minY", NpgsqlDbType.Double) { Value = boundingBox2D.Min.Y - tolerance });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("maxX", NpgsqlDbType.Double) { Value = boundingBox2D.Max.X + tolerance });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("maxY", NpgsqlDbType.Double) { Value = boundingBox2D.Max.Y + tolerance });
        }

        private static bool IsQueryable(Circle2D circle2D)
        {
            if (circle2D.Center is null)
            {
                return false;
            }

            double radius = circle2D.Radius;

            return !double.IsNaN(radius) && !double.IsInfinity(radius) && radius > 0;
        }

        private static void AddCircle2DParameters(NpgsqlCommand npgsqlCommand, Circle2D circle2D, double tolerance)
        {
            // Center hands back a fresh Point2D on every read, so it is taken once rather than three times.
            Point2D? center = circle2D.Center;

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("centerX", NpgsqlDbType.Double) { Value = center!.X });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("centerY", NpgsqlDbType.Double) { Value = center.Y });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("radius", NpgsqlDbType.Double) { Value = circle2D.Radius + tolerance });
        }

        private static TerrainPoint Create_TerrainPoint(NpgsqlDataReader npgsqlDataReader)
        {
            return new TerrainPoint
            {
                CountyId = npgsqlDataReader.IsDBNull(0) ? null : npgsqlDataReader.GetInt32(0),
                SubdivisionId = npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetInt32(1),
                Point3D = new Point3D(npgsqlDataReader.GetDouble(2), npgsqlDataReader.GetDouble(3), npgsqlDataReader.GetDouble(4)),
                CreatedAt = npgsqlDataReader.IsDBNull(5) ? null : npgsqlDataReader.GetDateTime(5)
            };
        }

        private static async Task<List<TerrainPoint>?> ReadAsync_TerrainPoint(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken)
        {
            List<TerrainPoint> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_TerrainPoint(npgsqlDataReader));
            }

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        private static async Task<PointCloud3D?> ReadAsync_PointCloud3D(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken)
        {
            List<double> xValues = [];
            List<double> yValues = [];
            List<double> zValues = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                xValues.Add(npgsqlDataReader.GetDouble(0));
                yValues.Add(npgsqlDataReader.GetDouble(1));
                zValues.Add(npgsqlDataReader.GetDouble(2));
            }

            if (xValues.Count == 0)
            {
                return null;
            }

            return new PointCloud3D([.. xValues], [.. yValues], [.. zValues]);
        }
    }
}
