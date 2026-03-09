using Npgsql;
using System;
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
                Console.WriteLine($"Postgres Error (AdministrativeArea2D): {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> TableAsync_Building2D(this NpgsqlConnection? npgsqlConnection)
        {
            if (npgsqlConnection is null)
            {
                return false;
            }

            // Using timestamptz to ensure consistent time tracking across different time zones
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
                    -- The unique constraint must include the partition key (county_id)
                    UNIQUE (reference, county_id)
                ) PARTITION BY LIST (county_id);";

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
                Console.WriteLine($"Postgres Error (Building2D): {ex.Message}");
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
    }
}