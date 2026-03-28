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
                    List<LocationReference>? locationReferences = await building2DPostgreSQLConverter.GetLocationReferencesAsync(countyId, cancellationToken: cancellationToken);
                    if (locationReferences is null || locationReferences.Count == 0)
                    {
                        continue;
                    }

                    count += locationReferences.Count;

                    progress.Report(count);

                    if (!PostgreSQLOrtoDatasRefreshOptions.OverrideExistsing)
                    {
                        locationReferences = await ortoDatasPostgreSQLConverter.GetExistingLocationReferencesAsync(locationReferences, true, cancellationToken);
                    }

                    if (locationReferences is null || locationReferences.Count == 0)
                    {
                        continue;
                    }

                    await ortoDatasPostgreSQLConverter.UpdateLocationReferencesAsync(locationReferences, cancellationToken);
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