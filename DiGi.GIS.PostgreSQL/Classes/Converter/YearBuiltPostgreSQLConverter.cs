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
    public class YearBuiltPostgreSQLConverter : PostgreSQLConverter<YearBuilt>, IGISPostgreSQLConverter<YearBuilt>
    {
        public YearBuiltPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public static async Task<List<YearBuilt>?> GetYearBuiltsByReferencesAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string>? references, int? countyId, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || references is null)
            {
                return null;
            }

            List<YearBuilt>? result = [];

            if (!references.Any())
            {
                return result;
            }

            const string commandText = @"
                SELECT id, county_id, reference, source, year, object, created_at
                FROM year_built
                WHERE reference = ANY(@references)
                  AND (@countyId IS NULL OR county_id = @countyId);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.AddWithValue("references", references.ToArray());
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            result = await ReadAsync_YearBuilt(npgsqlCommand, cancellationToken);

            return result;
        }

        public async Task<List<YearBuilt>?> GetYearBuiltsByBuilding2DReferenceAsync(Building2DReference? building2DReference, CancellationToken cancellationToken = default)
        {
            if (building2DReference is null)
            {
                return null;
            }

            return await GetYearBuiltsByBuilding2DReferencesAsync([building2DReference], cancellationToken);
        }

        public async Task<List<YearBuilt>?> GetYearBuiltsByBuilding2DReferencesAsync(IEnumerable<Building2DReference>? building2DReferences, CancellationToken cancellationToken = default)
        {
            if (building2DReferences is null)
            {
                return null;
            }

            if (!building2DReferences.Any())
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            List<YearBuilt> result = [];

            IEnumerable<IGrouping<int, Building2DReference>> groupings = building2DReferences.Where(x => x.CountyId.HasValue && !string.IsNullOrEmpty(x.Reference)).GroupBy(x => x.CountyId!.Value);

            foreach (IGrouping<int, Building2DReference> grouping in groupings)
            {
                int countyId = grouping.Key;

                string[] references = [.. grouping.Select(lr => lr.Reference!).Distinct()];

                const string commandText = @"
                    SELECT id, county_id, reference, source, year, object, created_at
                    FROM year_built
                    WHERE county_id = @county_id
                      AND reference = ANY(@references);";

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
                npgsqlCommand.Parameters.AddWithValue("county_id", countyId);
                npgsqlCommand.Parameters.AddWithValue("references", references);

                List<YearBuilt>? yearBuilts = await ReadAsync_YearBuilt(npgsqlCommand, cancellationToken);
                if (yearBuilts is not null)
                {
                    result.AddRange(yearBuilts);
                }
            }

            return result;
        }

        public async Task<YearBuilt?> GetYearBuiltByReferenceAsync(string reference, int? countyId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return await GetYearBuiltsByReferencesAsync([reference], countyId, cancellationToken).ContinueWith(t => t.Result?.FirstOrDefault(), cancellationToken);
        }

        public async Task<List<YearBuilt>?> GetYearBuiltsByReferencesAsync(IEnumerable<string>? references, int? countyId, CancellationToken cancellationToken = default)
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

            return await GetYearBuiltsByReferencesAsync(npgsqlConnection, references, countyId, cancellationToken);
        }

        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<YearBuilt>? yearBuilts)
        {
            if (yearBuilts is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await npgsqlConnection.TableAsync_YearBuilt();
            if (!succeded)
            {
                return null;
            }

            HashSet<long> result = [];
            if (!yearBuilts.Any())
            {
                return result;
            }

            IEnumerable<IGrouping<int?, YearBuilt>> groupings = yearBuilts.GroupBy(x => x.CountyId);

            foreach (IGrouping<int?, YearBuilt> grouping in groupings)
            {
                if (!grouping.Key.HasValue)
                {
                    continue;
                }

                await npgsqlConnection.TableAsync_YearBuilt_Partition(grouping.Key.Value);

                const string commandText = @"
                    INSERT INTO year_built (county_id, reference, source, year, object)
                    VALUES (@county_id, @reference, @source, @year, @object)
                    ON CONFLICT (county_id, reference)
                    DO UPDATE SET
                        year = EXCLUDED.year,
                        source = EXCLUDED.source,
                        object = EXCLUDED.object,
                    RETURNING id;";

                foreach (YearBuilt? yearBuilt in grouping)
                {
                    await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                    npgsqlCommand.Parameters.AddWithValue("county_id", yearBuilt.CountyId!);
                    npgsqlCommand.Parameters.AddWithValue("reference", yearBuilt.Reference ?? (object)DBNull.Value);
                    npgsqlCommand.Parameters.AddWithValue("source", yearBuilt.Source ?? (object)DBNull.Value);
                    npgsqlCommand.Parameters.AddWithValue("year", yearBuilt.Year ?? (object)DBNull.Value);

                    npgsqlCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                    {
                        Value = (object?)yearBuilt.Object?.ToJsonString() ?? DBNull.Value
                    });

                    var returnedId = await npgsqlCommand.ExecuteScalarAsync();
                    if (returnedId is long id)
                    {
                        result.Add(id);
                        yearBuilt.Id = id;
                    }
                }
            }

            return result;
        }

        private static YearBuilt Create_YearBuilt(NpgsqlDataReader npgsqlDataReader)
        {
            return new YearBuilt
            {
                Id = npgsqlDataReader.GetInt64(0),
                CountyId = npgsqlDataReader.IsDBNull(1) ? null : npgsqlDataReader.GetInt32(1),
                Reference = npgsqlDataReader.IsDBNull(2) ? null : npgsqlDataReader.GetString(2),
                Source = npgsqlDataReader.IsDBNull(3) ? null : npgsqlDataReader.GetString(3),
                Year = npgsqlDataReader.IsDBNull(4) ? null : npgsqlDataReader.GetInt16(4),
                Object = JsonNode.Parse(npgsqlDataReader.GetString(5)) as JsonObject,
                CreatedAt = npgsqlDataReader.IsDBNull(6) ? null : npgsqlDataReader.GetDateTime(6)
            };
        }

        private static async Task<List<YearBuilt>?> ReadAsync_YearBuilt(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync_YearBuilt(npgsqlDataReader, cancellationToken);
        }

        private static async Task<List<YearBuilt>> ReadAsync_YearBuilt(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<YearBuilt> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_YearBuilt(npgsqlDataReader));
            }

            return result;
        }
    }
}