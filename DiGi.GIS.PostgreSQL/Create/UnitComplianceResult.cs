using DiGi.GIS.Classes;
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
        /// Asynchronously evaluates the matching compliance of administrative area references of the specified type against BDL territorial units.
        /// </summary>
        /// <param name="unitPostgreSQLConverter">The converter used to access stored BDL unit data and extract the statistical unit hierarchy.</param>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter used to retrieve administrative area references.</param>
        /// <param name="administrativeArealType">The administrative area type to evaluate.</param>
        /// <param name="commandTimeout">The timeout in seconds for database commands.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UnitComplianceResult"/> if successful; otherwise, null.</returns>
        public static async Task<UnitComplianceResult?> UnitComplianceResultAsync(this UnitPostgreSQLConverter? unitPostgreSQLConverter, AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, AdministrativeArealType administrativeArealType, int commandTimeout = 60, CancellationToken cancellationToken = default)
        {
            if (unitPostgreSQLConverter is null || administrativeAreal2DPostgreSQLConverter is null || administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            StatisticalUnit? rootStatisticalUnit = await unitPostgreSQLConverter.GetStatisticalUnitAsync(commandTimeout, cancellationToken);
            if (rootStatisticalUnit is null)
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(administrativeArealType, null, uniqueCode: true, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return null;
            }

            long matchedCount = 0;
            List<AdministrativeAreal2DReference> unmatched = [];

            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                if (administrativeAreal2DReference is null)
                {
                    continue;
                }

                StatisticalUnit? matched = Query.Match(rootStatisticalUnit, administrativeAreal2DReference);
                if (matched is not null)
                {
                    matchedCount++;
                }
                else
                {
                    unmatched.Add(administrativeAreal2DReference);
                }
            }

            return new UnitComplianceResult(administrativeArealType, administrativeAreal2DReferences.Count, matchedCount, unmatched);
        }
    }
}
