using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        public static async Task<bool> TableAsync_AdministrativeArea2D(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Using timestamptz to ensure consistent time tracking across different time zones
            const string commandText = @"
                CREATE TABLE IF NOT EXISTS administrative_areal_2D (
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
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_AdministrativeArea2D)}): {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> TableAsync_Building2D(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            const string commandText = @"
                CREATE TABLE IF NOT EXISTS building_2D (
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

                -- This index handles both: subdivision_id = ANY(@ids) AND subdivision_id IS NULL
                CREATE INDEX IF NOT EXISTS index_building_2d_subdivision_id
                ON building_2D (subdivision_id);
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
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_Building2D)}): {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> TableAsync_Building2D_Partition(this NpgsqlConnection? npgsqlConnection, int countyId)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS building_2D_{countyId} PARTITION OF building_2D
                    FOR VALUES IN ({countyId});
                ";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            await npgsqlCommand.ExecuteNonQueryAsync();

            return true;
        }

        public static async Task<bool> TableAsync_OrtoDatas(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            const string commandText = @"
                CREATE TABLE IF NOT EXISTS orto_datas (
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
                CREATE INDEX IF NOT EXISTS idx_orto_datas_subdivision
                ON orto_datas (subdivision_id);

                -- Optimization: Composite index for County + Reference
                -- This is highly effective because of your partitioning strategy.
                CREATE UNIQUE INDEX IF NOT EXISTS idx_orto_datas_ref
                ON orto_datas (county_id, reference);
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
                Console.WriteLine($"Postgres Error (OrtoDatas): {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> TableAsync_OrtoDatas_Partition(this NpgsqlConnection? npgsqlConnection, int countyId)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS orto_datas_{countyId} PARTITION OF orto_datas
                    FOR VALUES IN ({countyId});
                ";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            await npgsqlCommand.ExecuteNonQueryAsync();

            return true;
        }

        public static async Task<bool> TableAsync_LocationReference(this NpgsqlConnection? npgsqlConnection, string? tableName, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(tableName))
            {
                return false;
            }

            // Added semicolons after each SQL statement to fix the 42601 syntax error
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

        public static async Task<bool> TableAsync_YearBuilt(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Combined command: Create partitioned table and the supporting index
            // The index on the parent table will be inherited by all child partitions.
            const string commandText = @"
                CREATE TABLE IF NOT EXISTS year_built (
                    id BIGINT GENERATED ALWAYS AS IDENTITY,
                    county_id INT NOT NULL,
                    reference TEXT NOT NULL,
                    source TEXT,
                    year SMALLINT,
                    object JSONB,
                    created_at timestamptz DEFAULT now(),
                    PRIMARY KEY (id, county_id)
                ) PARTITION BY LIST (county_id);

                -- Optimization: Composite index for County + Reference
                CREATE UNIQUE INDEX IF NOT EXISTS idx_year_built_ref
                ON year_built (county_id, reference);
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
                Console.WriteLine($"Postgres Error ({nameof(TableAsync_YearBuilt)}): {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> TableAsync_YearBuilt_Partition(this NpgsqlConnection? npgsqlConnection, int countyId)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            string commandText = $@"
                CREATE TABLE IF NOT EXISTS year_built_{countyId} PARTITION OF year_built
                    FOR VALUES IN ({countyId});
                ";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            await npgsqlCommand.ExecuteNonQueryAsync();

            return true;
        }
    }
}