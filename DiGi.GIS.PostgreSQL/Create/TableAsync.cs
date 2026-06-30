using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        /// <summary>
        /// Asynchronously creates the AdministrativeArea2D table in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="Npgsql.NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_AdministrativeArea2D(this NpgsqlConnection? npgsqlConnection, int commandTimeout = 30)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Using timestamptz to ensure consistent time tracking across different time zones
            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {Constants.TableName.AdministrativeAreal2D} (
                    id SERIAL PRIMARY KEY,
                    reference TEXT NOT NULL UNIQUE,
                    code TEXT,
                    name TEXT,
                    type_id SMALLINT NOT NULL,
                    min_x DOUBLE PRECISION,
                    min_y DOUBLE PRECISION,
                    max_x DOUBLE PRECISION,
                    max_y DOUBLE PRECISION,
                    country_id INT,
                    voivodeship_id INT,
                    county_id INT,
                    municipality_id INT,
                    object JSONB,
                    created_at timestamptz DEFAULT now()
                );

                -- 1. Spatial index using GiST and box type for fast Bounding Box searches
                -- This index directly supports the '&&' operator used in GetAdministrativeAreal2DsByBoundingBox2DAsync
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.AdministrativeAreal2D}_bbox
                ON {Constants.TableName.AdministrativeAreal2D} USING gist (box(point(min_x, min_y), point(max_x, max_y)));

                -- 2. Index for type filtering (often used together with spatial queries)
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.AdministrativeAreal2D}_type_id
                ON {Constants.TableName.AdministrativeAreal2D} (type_id);

                -- 3. Composite indices for hierarchical administrative lookups
                -- These speed up filtering by voivodeship, county, etc.
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.AdministrativeAreal2D}_hierarchy
                ON {Constants.TableName.AdministrativeAreal2D} (voivodeship_id, county_id, municipality_id);";

            try
            {
                // Explicitly using NpgsqlCommand type instead of implicit typing
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.CommandTimeout = commandTimeout;

                await npgsqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Asynchronously creates the AdministrativeArea2DReferencedObject table for the specified table name.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="tableName">The name of the table associated with the administrative area 2D referenced object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_AdministrativeArea2DReferencedObject(this NpgsqlConnection? npgsqlConnection, string tableName)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {tableName} (
                    id SERIAL PRIMARY KEY,
                    unique_id TEXT NOT NULL UNIQUE,
                    reference TEXT NOT NULL,
                    object JSONB,
                    created_at timestamptz DEFAULT now()
                );";

            try
            {
                // Explicitly using NpgsqlCommand type instead of implicit typing
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                await npgsqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                // Logging the error to console - in ASP.NET Core we will later replace this with ILogger
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_AdministrativeArea2DReferencedObject)}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously creates the Building2D table in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The Npgsql connection instance used to execute the command.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_Building2D(this NpgsqlConnection? npgsqlConnection, int commandTimeout = 30)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {Constants.TableName.Building2D} (
                    id BIGINT GENERATED ALWAYS AS IDENTITY,
                    county_id INT NOT NULL,
                    reference TEXT NOT NULL,
                    code TEXT,
                    min_x DOUBLE PRECISION,
                    min_y DOUBLE PRECISION,
                    max_x DOUBLE PRECISION,
                    max_y DOUBLE PRECISION,
                    subdivision_id INT,
                    object JSONB,
                    created_at timestamptz DEFAULT now(),
                    PRIMARY KEY (id, county_id),
                    UNIQUE (reference, county_id)
                ) PARTITION BY LIST (county_id);

                -- 1. CRITICAL: Spatial index using GiST and box type.
                -- This is essential for Bounding Box searches at the scale of millions of buildings.
                -- It allows PostgreSQL to perform an R-Tree search instead of a Sequential Scan.
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.Building2D}_bbox
                ON {Constants.TableName.Building2D} USING gist (box(point(min_x, min_y), point(max_x, max_y)));

                -- 2. Hierarchy index: Subdivision filtering.
                -- Useful for grouping buildings within estates or specific technical zones.
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.Building2D}_subdivision_id
                ON {Constants.TableName.Building2D} (subdivision_id);
                ";

            try
            {
                // Explicitly using NpgsqlCommand type instead of implicit typing
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.CommandTimeout = commandTimeout;

                await npgsqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Asynchronously creates a partition for the Building2D table associated with the specified county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="countyId">The integer identifier of the county for which the partition is being created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the Building2D partition was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_Building2D_Partition(this NpgsqlConnection? npgsqlConnection, int countyId)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {Constants.TableName.Building2D}_{countyId} PARTITION OF {Constants.TableName.Building2D}
                    FOR VALUES IN ({countyId});
                ";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            await npgsqlCommand.ExecuteNonQueryAsync();

            return true;
        }

        /// <summary>
        /// Asynchronously creates the Building 2D reference table in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="tableName">The name of the table to be created for Building 2D references.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other methods as a token for cancelling the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_Building2DReference(this NpgsqlConnection? npgsqlConnection, string? tableName, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(tableName))
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {tableName} (
                    id BIGINT GENERATED ALWAYS AS IDENTITY,
                    county_id INT NOT NULL,
                    reference TEXT NOT NULL,
                    subdivision_id INT,
                    created_at timestamptz DEFAULT now(),
                    PRIMARY KEY (id, county_id)
                );

                CREATE UNIQUE INDEX IF NOT EXISTS idx_{tableName}_county_id_reference
                    ON {tableName} (county_id, reference);

                CREATE INDEX IF NOT EXISTS idx_{tableName}_created_at
                    ON {tableName} (created_at ASC);
                ";

            try
            {
                // Explicitly specifying NpgsqlCommand type
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                await npgsqlCommand.ExecuteNonQueryAsync(cancellationToken);
                return true;
            }
            catch (NpgsqlException ex)
            {
                // For engineering plugins (Revit/Rhino), logging to a dedicated console or file is key
                Console.WriteLine($"Postgres Error during table creation ({tableName}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously creates the Building 2D Referenced Object table for the specified table name.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="Npgsql.NpgsqlConnection"/> instance used to execute the command.</param>
        /// <param name="tableName">The <see cref="System.String"/> representing the name of the table to be created.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that represents the asynchronous operation. The task result is a <see cref="System.Boolean"/> value indicating whether the table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_Building2DReferencedObject(this NpgsqlConnection? npgsqlConnection, string tableName)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {tableName} (
                    id BIGINT GENERATED ALWAYS AS IDENTITY,
                    unique_id TEXT NOT NULL,
                    county_id INT NOT NULL,
                    reference TEXT NOT NULL,
                    object JSONB,
                    created_at timestamptz DEFAULT now(),
                    PRIMARY KEY (id, county_id),
                    UNIQUE (county_id, unique_id)
                ) PARTITION BY LIST (county_id);

                -- Note: The index name should be unique per database schema
                CREATE INDEX IF NOT EXISTS idx_{tableName}_unique_id_county
                ON {tableName} (county_id, unique_id);";

            try
            {
                // Explicitly using NpgsqlCommand type instead of implicit typing
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                await npgsqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                // Logging the error to console - in ASP.NET Core we will later replace this with ILogger
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_Building2DReferencedObject)}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously creates a partition for the Building2DReferencedObject table based on the specified table name and county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="tableName">The name of the parent table that is being partitioned.</param>
        /// <param name="countyId">The integer identifier of the county for which the partition is created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the partition was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_Building2DReferencedObject_Partition(this NpgsqlConnection? npgsqlConnection, string tableName, int countyId)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {tableName}_{countyId} PARTITION OF {tableName}
                    FOR VALUES IN ({countyId});
                ";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            await npgsqlCommand.ExecuteNonQueryAsync();

            return true;
        }

        /// <summary>
        /// Asynchronously creates the OrtoDatas table in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="Npgsql.NpgsqlConnection"/> instance used to execute the command.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the OrtoDatas table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_OrtoDatas(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {Constants.TableName.OrtoDatas} (
                    id BIGINT GENERATED ALWAYS AS IDENTITY,
                    county_id INT NOT NULL,
                    reference TEXT NOT NULL,
                    min_x DOUBLE PRECISION,
                    min_y DOUBLE PRECISION,
                    max_x DOUBLE PRECISION,
                    max_y DOUBLE PRECISION,
                    subdivision_id INT,
                    object JSONB,
                    created_at timestamptz DEFAULT now(),
                    PRIMARY KEY (id, county_id)
                ) PARTITION BY LIST (county_id);

                -- Index for subdivision (already in your code)
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.OrtoDatas}_subdivision
                ON {Constants.TableName.OrtoDatas} (subdivision_id);

                -- Optimization: Composite index for County + Reference
                -- This is highly effective because of your partitioning strategy.
                CREATE UNIQUE INDEX IF NOT EXISTS idx_{Constants.TableName.OrtoDatas}_ref
                ON {Constants.TableName.OrtoDatas} (county_id, reference);
                ";

            try
            {
                // Explicitly using NpgsqlCommand type instead of implicit typing
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                await npgsqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                // Logging the error to console - in ASP.NET Core we will later replace this with ILogger
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_OrtoDatas)}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously creates a partition for the OrtoDatas table based on the specified county identifier.
        /// </summary>
        /// <param name="npgsqlConnection">The PostgreSQL connection instance used to execute the command.</param>
        /// <param name="countyId">The unique identifier of the county for which the partition is being created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the partition was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_OrtoDatas_Partition(this NpgsqlConnection? npgsqlConnection, int countyId)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {Constants.TableName.OrtoDatas}_{countyId} PARTITION OF {Constants.TableName.OrtoDatas}
                    FOR VALUES IN ({countyId});
                ";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            await npgsqlCommand.ExecuteNonQueryAsync();

            return true;
        }

        /// <summary>
        /// Asynchronously creates the epw_file table in the PostgreSQL database.
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> instance used to execute the command.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the table was created successfully; otherwise, false.</returns>
        public static async Task<bool> TableAsync_EPWFile(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS {Constants.TableName.EPWFile} (
                    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                    name TEXT NOT NULL UNIQUE,
                    x DOUBLE PRECISION NOT NULL,
                    y DOUBLE PRECISION NOT NULL,
                    object JSONB NOT NULL,
                    created_at timestamptz DEFAULT now()
                );
                CREATE INDEX IF NOT EXISTS idx_{Constants.TableName.EPWFile}_location
                ON {Constants.TableName.EPWFile} USING gist ((point(x, y)));
            ";

            try
            {
                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                await npgsqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_EPWFile)}): {ex.Message}");
                return false;
            }
        }
    }
}