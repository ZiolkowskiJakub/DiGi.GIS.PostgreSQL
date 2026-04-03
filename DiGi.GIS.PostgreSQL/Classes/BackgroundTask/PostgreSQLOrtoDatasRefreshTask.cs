using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLOrtoDatasRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        public PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions { get; set; } = new PostgreSQLOrtoDatasRefreshOptions();

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        public PostgreSQLOrtoDatasRefreshTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            if (gISPostgreSQLConverterManager?.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>() is not AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter)
            {
                return false;
            }

            if (gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DPostgreSQLConverter>() is not Building2DPostgreSQLConverter building2DPostgreSQLConverter)
            {
                return false;
            }

            if (gISPostgreSQLConverterManager.GetPostgreSQLConverter<OrtoDatasPostgreSQLConverter>() is not OrtoDatasPostgreSQLConverter ortoDatasPostgreSQLConverter)
            {
                return false;
            }

            PostgreSQLOrtoDatasRefreshOptions ??= new PostgreSQLOrtoDatasRefreshOptions();

            HashSet<int>? countyIds = PostgreSQLOrtoDatasRefreshOptions.CountyIds;

            try
            {
                countyIds ??= await administrativeAreal2DPostgreSQLConverter.GetIdsAsync(Enums.AdministrativeArealType.County, cancellationToken);

                if (countyIds == null)
                {
                    return false;
                }

                if (countyIds.Count == 0)
                {
                    return true;
                }

                long count = 0;

                foreach (int countyId in countyIds)
                {
                    List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesAsync(countyId, cancellationToken: cancellationToken);
                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    count += building2DReferences.Count;

                    progress.Report(count);

                    if (PostgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds)
                    {
                        await ortoDatasPostgreSQLConverter.UpdateSubdivisionIds(building2DReferences, cancellationToken);
                    }

                    if (!PostgreSQLOrtoDatasRefreshOptions.OverrideExistsing)
                    {
                        building2DReferences = await ortoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(building2DReferences, true, cancellationToken);
                    }

                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    await ortoDatasPostgreSQLConverter.UpdateBuilding2DReferencesAsync(building2DReferences, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Transaction will rollback automatically on dispose if not committed
                return false;
            }
            catch (Exception)
            {
                // Log error here if necessary
                throw;
            }

            return !cancellationToken.IsCancellationRequested;
        }
    }
}