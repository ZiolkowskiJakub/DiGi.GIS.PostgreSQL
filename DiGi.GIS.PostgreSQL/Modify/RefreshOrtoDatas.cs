using DiGi.GIS.PostgreSQL.Classes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        /// <summary>
        /// Asynchronously refreshes orthodata in the PostgreSQL database based on the specified options.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The manager used to retrieve the necessary PostgreSQL converters.</param>
        /// <param name="PostgreSQLOrtoDatasRefreshOptions">The options specifying how the orthodata should be refreshed.</param>
        /// <param name="progress">An optional progress reporter to track the number of processed building references.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the refresh succeeded; otherwise, false.</returns>
        public static async Task<bool> RefreshOrtoDatas(this GISPostgreSQLConverterManager? gISPostgreSQLConverterManager, PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions, IProgress<long>? progress = null, CancellationToken cancellationToken = default)
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
                    List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, null, null,  cancellationToken);
                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    count += building2DReferences.Count;

                    progress?.Report(count);

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