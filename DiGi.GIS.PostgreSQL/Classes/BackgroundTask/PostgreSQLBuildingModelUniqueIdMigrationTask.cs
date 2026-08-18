// TODO [BuildingModelRowIdentity]: this whole file is temporary and exists only for the one-off
// unique_id migration of issue ZiolkowskiJakub/DiGi.GIS.PostgreSQL#5. Delete it once every deployed
// database has run this task and it reports zero pending rows nationally, together with the
// migration members on BuildingModelPostgreSQLConverter and
// Building2DReferencedObjectPostgreSQLConverter, UniqueIdMigrationResult,
// PostgreSQLBuildingModelUniqueIdMigrationOptions and the registration in
// DiGi.GIS.PostgreSQL.UI Create.VisualBackgroundTasks.

using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// [TEMPORARY] Moves every stored building model's own identifier into the <c>unique_id</c> column of the row holding it.
    /// <para>The rows of <c>building_model</c> were keyed on the reference of the building they describe rather than on the identifier of the model they hold. That was the fix for issue #1 - a regeneration was inserting a second model per building instead of replacing one - but it changed what a row means, pinning the table to one row per building and making it the only referenced-object table not following the convention on <see cref="Building2DReferencedObject{TUniqueObject}"/>. This puts it back without regenerating anything: the identifier is already stored, it travels inside the <c>object</c> column, and the migration reads it from there.</para>
    /// <para><b>It has to run against a database before a build converting models with their own identifier is deployed against it.</b> In the other order the first upload matches no row and inserts a second model for every building, which is exactly the duplication issue #1 removed. Nothing enforces the order, so it is the operator's to keep.</para>
    /// <para>It destroys nothing. The value it overwrites is the building's reference, which the row still carries in its own <c>reference</c> column, so a migrated row can be read back either way round.</para>
    /// <para>The work is one statement per county row, not a read-modify-write per row: the <c>object</c> column holds a complete building model, so pulling the rows to the client to reach one value in each would move gigabytes for a value a few characters long.</para>
    /// <para><see cref="UniqueIdMigrationResults"/> keeps what every county row reported, so a caller can log or show the totals when the run ends - this project has no logging dependency and does not need one to surface them.</para>
    /// <para>Temporary - see the note at the top of this file.</para>
    /// </summary>
    public class PostgreSQLBuildingModelUniqueIdMigrationTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// The GIS PostgreSQL converter manager used to retrieve the converters this task works through.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// What each county row reported, in the order the county rows were visited, whether or not the task went on to write.
        /// </summary>
        private readonly List<UniqueIdMigrationResult> uniqueIdMigrationResults = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingModelUniqueIdMigrationTask"/> class.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The manager holding the PostgreSQL converters.</param>
        public PostgreSQLBuildingModelUniqueIdMigrationTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager;
        }

        /// <summary>
        /// Gets or sets the options used to configure the migration.
        /// </summary>
        public PostgreSQLBuildingModelUniqueIdMigrationOptions PostgreSQLBuildingModelUniqueIdMigrationOptions { get; set; } = new();

        /// <summary>
        /// Gets what each county row reported, in the order the county rows were visited.
        /// <para>Counted before anything was written, so a dry run fills this exactly as an armed run does. It is the record to review a completed run against: a non-zero <see cref="UniqueIdMigrationResult.Blocked"/> or <see cref="UniqueIdMigrationResult.Missing"/> names a part whose rows the migration could not move and which the converter change will therefore strand.</para>
        /// <para>Cleared at the start of every run, so it always describes the last one.</para>
        /// </summary>
        public IReadOnlyList<UniqueIdMigrationResult> UniqueIdMigrationResults => uniqueIdMigrationResults;

        /// <summary>
        /// Executes the migration over every county row in scope.
        /// </summary>
        /// <param name="progress">A progress reporter carrying the running total of rows migrated.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true when every county row in scope was visited; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            uniqueIdMigrationResults.Clear();

            if (gISPostgreSQLConverterManager is null)
            {
                return false;
            }

            PostgreSQLBuildingModelUniqueIdMigrationOptions ??= new PostgreSQLBuildingModelUniqueIdMigrationOptions();

            bool dryRun = PostgreSQLBuildingModelUniqueIdMigrationOptions.DryRun;

            // A county part holds tens of thousands of rows and the migration is one statement over all of
            // them, so the 30s default is not enough. Same figure as the other national tasks use.
            const int commandTimeout = 600;

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return false;
            }

            BuildingModelPostgreSQLConverter? buildingModelPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingModelPostgreSQLConverter>();
            if (buildingModelPostgreSQLConverter is null)
            {
                return false;
            }

            List<AdministrativeAreal2D>? administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.County, cancellationToken: cancellationToken);
            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                return false;
            }

            HashSet<int>? countyIds = PostgreSQLBuildingModelUniqueIdMigrationOptions.CountyIds is null ? null : [.. PostgreSQLBuildingModelUniqueIdMigrationOptions.CountyIds];
            HashSet<string>? voivodeshipCodes = PostgreSQLBuildingModelUniqueIdMigrationOptions.VoivodeshipCodes is null ? null : [.. PostgreSQLBuildingModelUniqueIdMigrationOptions.VoivodeshipCodes];

            List<int> countyIds_Migrated = [];
            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                int countyId = administrativeAreal2D.Id;
                if (Query.IsInScope(countyId, administrativeAreal2D.Code, countyIds, voivodeshipCodes))
                {
                    countyIds_Migrated.Add(countyId);
                }
            }

            if (countyIds_Migrated.Count == 0)
            {
                return false;
            }

            countyIds_Migrated.Sort();

            LongProgressWrapper? longProgressWrapper = Core.Create.LongProgressWrapper(progress);

            foreach (int countyId in countyIds_Migrated)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Counted before anything is written, so the pending figure is what the update should return
                // and the two can be compared afterwards. A dry run stops here.
                UniqueIdMigrationResult? uniqueIdMigrationResult = await buildingModelPostgreSQLConverter.GetUniqueIdMigrationResultAsync(countyId, commandTimeout, cancellationToken);
                if (uniqueIdMigrationResult is null)
                {
                    continue;
                }

                uniqueIdMigrationResults.Add(uniqueIdMigrationResult);

                if (uniqueIdMigrationResult.Pending == 0 || dryRun)
                {
                    continue;
                }

                cancellationToken.ThrowIfCancellationRequested();

                HashSet<long>? ids = await buildingModelPostgreSQLConverter.MigrateUniqueIdsAsync(countyId, commandTimeout, cancellationToken);

                longProgressWrapper?.Increment(ids?.Count ?? 0);
            }

            return true;
        }
    }
}
