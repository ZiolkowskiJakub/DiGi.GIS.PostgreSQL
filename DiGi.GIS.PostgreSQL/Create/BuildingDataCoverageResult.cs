using DiGi.GIS.PostgreSQL.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        /// <summary>
        /// Asynchronously measures what one county's building data holds against the buildings that county actually has.
        /// <para>The comparison is made on references read from each side rather than by a join, because the two tables are in different databases - <c>building_2d</c> in the main one and <c>building_data</c> in the storage one.</para>
        /// <para>The reads run sequentially on their own connections without fanning out per building or per subdivision: a coverage read that opened a connection per item is what exhausted the pool the last time this shape was written.</para>
        /// </summary>
        /// <param name="buildingDataPostgreSQLConverter">The converter reading the building data side.</param>
        /// <param name="building2DPostgreSQLConverter">The converter reading the building side.</param>
        /// <param name="countyId">The identifier of the county to measure.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout. Defaults to 600 seconds.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the coverage, or null when either converter is missing or either side could not be read.</returns>
        public static async Task<BuildingDataCoverageResult?> BuildingDataCoverageResultAsync(this BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter, Building2DPostgreSQLConverter? building2DPostgreSQLConverter, int countyId, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (buildingDataPostgreSQLConverter is null || building2DPostgreSQLConverter is null)
            {
                return null;
            }

            List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, subdivisionId: null, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (building2DReferences is null)
            {
                return null;
            }

            HashSet<string>? references_BuildingData = await buildingDataPostgreSQLConverter.GetReferencesByCountyIdAsync(countyId, commandTimeout, cancellationToken);
            if (references_BuildingData is null)
            {
                return null;
            }

            HashSet<string> references_Building2D = [];
            foreach (Building2DReference building2DReference in building2DReferences)
            {
                if (building2DReference?.Reference is string reference && !string.IsNullOrWhiteSpace(reference))
                {
                    references_Building2D.Add(reference);
                }
            }

            long missingReferenceCount = 0;
            foreach (string reference in references_Building2D)
            {
                if (!references_BuildingData.Contains(reference))
                {
                    missingReferenceCount++;
                }
            }

            long orphanReferenceCount = 0;
            foreach (string reference in references_BuildingData)
            {
                if (!references_Building2D.Contains(reference))
                {
                    orphanReferenceCount++;
                }
            }

            long unassignedSubdivisionCount = await building2DPostgreSQLConverter.GetCountWithoutSubdivisionAsync(countyId, commandTimeout, cancellationToken);

            AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter = new(building2DPostgreSQLConverter.ConnectionData);
            List<AdministrativeAreal2DReference>? subdivisions_County = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivision, parentId: countyId, uniqueCode: false, commandTimeout: commandTimeout, cancellationToken: cancellationToken);

            long crossCountySubdivisionCount = 0;
            if (subdivisions_County is not null)
            {
                HashSet<int> inScopeSubdivisionIds = [];
                foreach (AdministrativeAreal2DReference subdivision_County in subdivisions_County)
                {
                    inScopeSubdivisionIds.Add(subdivision_County.Id);
                }

                foreach (Building2DReference building2DReference in building2DReferences)
                {
                    if (building2DReference?.SubdivisionId is int subdivisionId && !inScopeSubdivisionIds.Contains(subdivisionId))
                    {
                        crossCountySubdivisionCount++;
                    }
                }
            }

            return new BuildingDataCoverageResult(countyId, references_Building2D.Count, references_BuildingData.Count, missingReferenceCount, orphanReferenceCount, unassignedSubdivisionCount, crossCountySubdivisionCount);
        }
    }
}
