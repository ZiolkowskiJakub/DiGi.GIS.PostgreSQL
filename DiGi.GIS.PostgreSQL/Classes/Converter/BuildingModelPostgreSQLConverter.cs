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
        /// <para>[TEMPORARY] It describes the keying this table used before the unique_id migration of issue ZiolkowskiJakub/DiGi.GIS.PostgreSQL#5 and is inert against a migrated county row: after the migration no row is keyed on its reference, so nothing supersedes anything and this answers zero. It is kept because it is still the right tool if rows written by an un-migrated build reappear. TODO [BuildingModelRowIdentity]: remove it with the rest of that migration.</para>
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
        /// <para>[TEMPORARY] It describes the keying this table used before the unique_id migration of issue ZiolkowskiJakub/DiGi.GIS.PostgreSQL#5 and is inert against a migrated county row: after the migration no row is keyed on its reference, so nothing supersedes anything and this answers zero. It is kept because it is still the right tool if rows written by an un-migrated build reappear. TODO [BuildingModelRowIdentity]: remove it with the rest of that migration.</para>
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

        /// <summary>
        /// [TEMPORARY] Promotes each row's stored identifier into its <c>unique_id</c> column, for one county row.
        /// <para>The rows of this table were keyed on the reference of the building they describe rather than on the identifier of the model they hold - the emergency fix for issue #1, which stopped a regeneration inserting a second model per building by changing what a row means. This puts the table back on the convention every other referenced-object table follows, without regenerating anything: the identifier is already stored, it travels inside the <c>object</c> column, and this reads it from there.</para>
        /// <para>It is a single statement per county row rather than a read-modify-write per row. The <c>object</c> column holds a complete building model, so pulling the rows to the client to reach one value in each would move gigabytes for a value a few characters long.</para>
        /// <para>Run <see cref="Building2DReferencedObjectPostgreSQLConverter{TBuilding2DReferencedObject, TUniqueObject}.GetUniqueIdMigrationResultAsync"/> first. It classifies rows exactly as this does, so its pending count is what this should return, and its blocked and missing counts are the rows this deliberately leaves alone.</para>
        /// <para>Unlike the deletes on this class it destroys nothing - the value it overwrites is the building's reference, which the row still carries in its own <c>reference</c> column - but it must be run against a database <b>before</b> a build converting models with their own identifier is deployed against it. In the other order the first upload matches no row and inserts a second model for every building, which is the duplication issue #1 removed.</para>
        /// <para>Temporary - see the note above <see cref="Building2DReferencedObjectPostgreSQLConverter{TBuilding2DReferencedObject, TUniqueObject}.UniqueIdClassificationCommandText"/> on the base class.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row to migrate.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the identifiers of the rows actually updated, which is the only evidence the update matched what was counted.</returns>
        public async Task<HashSet<long>?> MigrateUniqueIdsAsync(int countyId, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            // county_id is repeated on the UPDATE itself rather than left to the join, so the write is still
            // pruned to the one partition instead of being planned across every one of them.
            string commandText = $@"
                {UniqueIdClassificationCommandText}
                UPDATE {TableName} AS t
                SET unique_id = r.unique_id_target
                FROM row_classified AS r
                WHERE t.id = r.id
                  AND t.county_id = @countyId
                  AND {UniqueIdPendingCondition}
                RETURNING t.id;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
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