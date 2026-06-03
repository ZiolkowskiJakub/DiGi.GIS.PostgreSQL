using DiGi.GIS.IO;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using DiGi.PostgreSQL.Table.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class BuildingDataPostgreSQLConverter : TablePostgreSQLConverter<Core.IO.Table.Classes.Column>, IGISPostgreSQLConverter
    {
        public BuildingDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => Constants.TableName.BuildingData;

        protected override TableConversionOptions<Core.IO.Table.Classes.Column>? TableConversionOptions => new()
        {
            PrimaryKeyColumns = [IO.Constants.Column.CountyId, IO.Constants.Column.Reference],
            PartitioningOptions = new PartitioningOptions<Core.IO.Table.Classes.Column>()
            {
                Column = IO.Constants.Column.CountyId,
                PartitioningRule = new ValuePartitioningRule()
            }
        };

        public async Task<IEnumerable<T?>?> GetUniqueValuesAsync<T>(string? uniqueId, int countyId)
        {
            if(string.IsNullOrWhiteSpace(uniqueId))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            return await GetUniqueValuesAsync<T>(npgsqlConnection, uniqueId, countyId);
        }

        public async Task<IEnumerable<T?>?> GetUniqueValuesAsync<T>(NpgsqlConnection? npgsqlConnection, string? uniqueId, int countyId)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(uniqueId))
            {
                return null;
            }

            string commandQuery = $@"
                SELECT DISTINCT {uniqueId} 
                FROM {TableName} 
                WHERE (@countyId IS NULL OR county_id = @countyId) 
                  AND {uniqueId} IS NOT NULL 
                ORDER BY {uniqueId}";

            HashSet<T?> result = [];

            using NpgsqlCommand npgsqlCommand = new(commandQuery, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId as object ?? DBNull.Value);

            using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();

            while (await npgsqlDataReader.ReadAsync())
            {
                if (npgsqlDataReader.IsDBNull(0) || !Core.Query.TryConvert(npgsqlDataReader.GetValue(0), out T? value))
                {
                    result.Add(default);
                }
                else
                {
                    result.Add(value);
                }
            }

            return result;
        }

        public async Task<Core.IO.Table.Classes.Table?> PullAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<string> references, int? countyId, IEnumerable<string>? columnUniqueIds = null, int batchSize = 1000)
        {
            if (npgsqlConnection is null || references is null || !references.Any())
            {
                return null;
            }

            HashSet<string>? columnUniqueIds_Temp = columnUniqueIds == null ? null : [.. columnUniqueIds];

            List<Core.IO.Table.Classes.Column> columns = await GetColumnsByUniqueIdsAsync(npgsqlConnection, columnUniqueIds_Temp) ?? [];

            Core.IO.Table.Classes.Table table = new(columns);

            Core.IO.Table.Classes.Column? column_Reference = table.UpdateColumn<Core.IO.Table.Classes.Column>(IO.Constants.Column.Reference);
            if (column_Reference is null)
            {
                return null;
            }

            Core.IO.Table.Classes.Column? column_CountyId = countyId is null ? null : table.UpdateColumn<Core.IO.Table.Classes.Column>(IO.Constants.Column.CountyId);

            foreach (string reference in references)
            {
                Dictionary<int, object?> values = [];
                values[column_Reference.Index] = reference;
                if (column_CountyId is not null)
                {
                    values[column_CountyId.Index] = countyId;
                }

                table.AddRow(values);
            }

            await PullAsync(npgsqlConnection, table, batchSize);

            return table;
        }

        public async Task<Core.IO.Table.Classes.Table?> PullAsync(IEnumerable<string> references, int? countyId, IEnumerable<string>? columnUniqueIds = null, int batchSize = 1000)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            return await PullAsync(npgsqlConnection, references, countyId, columnUniqueIds, batchSize);
        }
    }
}