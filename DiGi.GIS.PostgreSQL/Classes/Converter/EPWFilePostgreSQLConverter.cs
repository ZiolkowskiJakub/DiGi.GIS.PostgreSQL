using DiGi.EPW;
using DiGi.EPW.Classes;
using DiGi.Geometry.Planar.Classes;
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
    /// Provides functionality for converting and managing <see cref="EPWFile"/> entities within a PostgreSQL database.
    /// </summary>
    public class EPWFilePostgreSQLConverter : PostgreSQLConverter<EPWFile>, IGISPostgreSQLConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EPWFilePostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The <see cref="ConnectionData"/> containing the connection settings required to establish a connection to the PostgreSQL database. This value can be null.</param>
        public EPWFilePostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the database table associated with EPW files.
        /// </summary>
        public static string TableName => Constants.TableName.EPWFile;

        /// <summary>
        /// Asynchronously clears all records from the epw_file table.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the operation succeeded; otherwise, false.</returns>
        public static async Task<bool> ClearAsync(NpgsqlConnection? npgsqlConnection, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the closest EPWFile to the given coordinate (x, y).
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database.</param>
        /// <param name="x">The X coordinate (longitude).</param>
        /// <param name="y">The Y coordinate (latitude).</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the closest <see cref="EPWFile"/>, or null if not found.</returns>
        public static async Task<EPWFile?> GetEPWFileAsync(NpgsqlConnection? npgsqlConnection, double x, double y, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT object
                FROM {TableName}
                ORDER BY point(x, y) <-> point(@x, @y)
                LIMIT 1;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("x", NpgsqlDbType.Double) { Value = x });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("y", NpgsqlDbType.Double) { Value = y });

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            if (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                string jsonString = npgsqlDataReader.GetString(0);
                JsonObject? jsonObject = JsonNode.Parse(jsonString) as JsonObject;
                EPWFile ePWFile = new(jsonObject);
                return ePWFile;
            }

            return null;
        }

        /// <summary>
        /// Asynchronously retrieves the closest EPWFile to the given Point2D.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database.</param>
        /// <param name="point2D">The coordinate point.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the closest <see cref="EPWFile"/>, or null if not found.</returns>
        public static async Task<EPWFile?> GetEPWFileAsync(NpgsqlConnection? npgsqlConnection, Point2D point2D, CancellationToken cancellationToken = default)
        {
            if (point2D is null)
            {
                return null;
            }

            return await GetEPWFileAsync(npgsqlConnection, point2D.X, point2D.Y, cancellationToken);
        }

        /// <summary>
        /// Asynchronously inserts or updates a collection of EPWFile objects in the database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the database.</param>
        /// <param name="ePWFiles">The collection of EPW files to insert or update.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of database identifiers for the inserted or updated records.</returns>
        public static async Task<List<int>> InsertAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<EPWFile> ePWFiles, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || ePWFiles is null || !ePWFiles.Any())
            {
                return [];
            }

            bool created = await Create.TableAsync_EPWFile(npgsqlConnection);
            if (!created)
            {
                return [];
            }

            List<int> ids = [];
            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (EPWFile ePWFile in ePWFiles)
            {
                if (ePWFile.Location is not Location location)
                {
                    continue;
                }

                string name = ePWFile.Name();
                double x = location.Longitude;
                double y = location.Latitude;

                NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO {TableName} (name, x, y, object)
                    VALUES (@name, @x, @y, @object)
                    ON CONFLICT (name)
                    DO UPDATE SET
                        x = EXCLUDED.x,
                        y = EXCLUDED.y,
                        object = EXCLUDED.object
                    RETURNING id;");

                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Text) { Value = name });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("x", NpgsqlDbType.Double) { Value = x });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("y", NpgsqlDbType.Double) { Value = y });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                {
                    Value = (object?)ePWFile.ToJsonObject()?.ToJsonString() ?? DBNull.Value
                });

                npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync(cancellationToken);
            do
            {
                while (await npgsqlDataReader.ReadAsync(cancellationToken))
                {
                    ids.Add(npgsqlDataReader.GetInt32(0));
                }
            }
            while (await npgsqlDataReader.NextResultAsync(cancellationToken));

            return ids;
        }

        /// <summary>
        /// Asynchronously clears all records from the epw_file table, automatically managing the connection.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> ClearAsync(CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await ClearAsync(npgsqlConnection, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the closest EPWFile to the given coordinate (x, y), automatically managing the connection.
        /// </summary>
        /// <param name="x">The X coordinate (longitude).</param>
        /// <param name="y">The Y coordinate (latitude).</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the closest <see cref="EPWFile"/>, or null if not found.</returns>
        public async Task<EPWFile?> GetEPWFileAsync(double x, double y, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await GetEPWFileAsync(npgsqlConnection, x, y, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the closest EPWFile to the given Point2D, automatically managing the connection.
        /// </summary>
        /// <param name="point2D">The coordinate point.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the closest <see cref="EPWFile"/>, or null if not found.</returns>
        public async Task<EPWFile?> GetEPWFileAsync(Point2D point2D, CancellationToken cancellationToken = default)
        {
            if (point2D is null)
            {
                return null;
            }

            return await GetEPWFileAsync(point2D.X, point2D.Y, cancellationToken);
        }

        /// <summary>
        /// Asynchronously inserts or updates a collection of EPWFile objects in the database, automatically managing the connection.
        /// </summary>
        /// <param name="ePWFiles">The collection of EPW files to insert or update.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of database identifiers for the inserted or updated records.</returns>
        public async Task<List<int>> InsertAsync(IEnumerable<EPWFile> ePWFiles, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);
            return await InsertAsync(npgsqlConnection, ePWFiles, cancellationToken);
        }
    }
}