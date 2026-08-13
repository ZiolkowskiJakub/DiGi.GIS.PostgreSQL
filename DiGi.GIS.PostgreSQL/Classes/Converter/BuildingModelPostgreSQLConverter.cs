using DiGi.Analytical.Building.Enums;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Provides functionality to convert building model data for PostgreSQL database operations, scoped to a specific <see cref="DiGi.Analytical.Building.Enums.BuildingModelDetailLevel"/>.
    /// </summary>
    public class BuildingModelPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<BuildingModel, DiGi.Analytical.Building.Classes.BuildingModel>
    {
        /// <summary>
        /// Gets the detail level of the building models handled by this converter, which determines the target table.
        /// </summary>
        private BuildingModelDetailLevel BuildingModelDetailLevel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to connect to the PostgreSQL database.</param>
        /// <param name="buildingModelDetailLevel">The detail level of the building models handled by this converter.</param>
        public BuildingModelPostgreSQLConverter(ConnectionData? connectionData, BuildingModelDetailLevel buildingModelDetailLevel)
            : base(connectionData)
        {
            BuildingModelDetailLevel = buildingModelDetailLevel;
        }

        /// <summary>
        /// Gets the name of the table in the PostgreSQL database where building model data for the configured detail level is stored.
        /// </summary>
        public override string TableName => $"{Constants.TableName.BuildingModel}_{BuildingModelDetailLevel.ToString().ToLowerInvariant()}";

        /// <summary>
        /// Asynchronously counts the rows of a county row that a correctly keyed row has already superseded.
        /// <para>A building model row is keyed on the reference of the building it describes. Rows written before that were keyed on the model's own identifier, which <c>ParametrizedGuidObject</c> hands out fresh on every model created, so the <c>ON CONFLICT (county_id, unique_id)</c> upsert could not match one and inserted a second model for the same building instead of replacing it. Such a row is superseded once a row keyed on the reference exists beside it.</para>
        /// <para>This is the counterpart of <see cref="RemoveSupersededAsync"/> and reads nothing else - run it first and compare the two numbers.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row to count in.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of superseded rows, or -1 when the connection could not be created.</returns>
        public async Task<long> GetSupersededCountAsync(int countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                SELECT count(*)
                FROM {TableName} AS t
                WHERE t.county_id = @countyId
                  AND t.unique_id <> t.reference
                  AND EXISTS (SELECT 1
                              FROM {TableName} AS t_Keyed
                              WHERE t_Keyed.county_id = t.county_id
                                AND t_Keyed.reference = t.reference
                                AND t_Keyed.unique_id = t_Keyed.reference);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            object? result = await npgsqlCommand.ExecuteScalarAsync(cancellationToken);

            return result is long count ? count : -1;
        }

        /// <summary>
        /// Deletes the rows of a county row that a correctly keyed row has already superseded, as described on <see cref="GetSupersededCountAsync"/>.
        /// <para>A row is deleted only when a row keyed on the same building's reference exists beside it, so the building keeps a model either way. That makes the delete independent of when it is run: before a regeneration it removes nothing, after one it removes exactly what the regeneration replaced, and a part that has never been regenerated is left alone rather than emptied.</para>
        /// <para>It removes data and has no undo - run <see cref="GetSupersededCountAsync"/> first and review what it reports.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row to delete from.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers of the rows actually deleted, which is the only evidence the delete matched what was counted.</returns>
        public async Task<HashSet<long>?> RemoveSupersededAsync(int countyId, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            string commandText = $@"
                DELETE FROM {TableName} AS t
                WHERE t.county_id = @countyId
                  AND t.unique_id <> t.reference
                  AND EXISTS (SELECT 1
                              FROM {TableName} AS t_Keyed
                              WHERE t_Keyed.county_id = t.county_id
                                AND t_Keyed.reference = t.reference
                                AND t_Keyed.unique_id = t_Keyed.reference)
                RETURNING t.id;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.Parameters.AddWithValue("countyId", countyId);

            HashSet<long> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(npgsqlDataReader.GetInt64(0));
            }

            return result;
        }

        /// <inheritdoc />
        protected override BuildingModel Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new BuildingModel()
            {
                Id = id,
                CountyId = countyId,
                UniqueId = uniqueId,
                Reference = reference,
                Object = @object,
                CreatedAt = createdAt
            };
        }
    }
}