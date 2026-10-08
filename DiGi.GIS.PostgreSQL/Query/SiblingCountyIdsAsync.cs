using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Widens county rows to every polygon part of the codes they name.
        /// <para>A county whose territory is disconnected is stored as one row per part, and a building's data is filed under the part holding its <c>building_2d</c> row - so a caller naming one part, such as a visitor whose building view carries a single county id, may be looking at data stored under a sibling. The answer never crosses into a county the caller did not name.</para>
        /// </summary>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter used to read the codes of the rows and the parts of the codes.</param>
        /// <param name="countyIds">The county rows to widen.</param>
        /// <param name="commandTimeout">The timeout in seconds for the lookups. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>The given rows together with every part of their codes; an empty set when no row is given; <c>null</c> when a lookup could not run.</returns>
        public static async Task<HashSet<int>?> SiblingCountyIdsAsync(this AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, IEnumerable<int>? countyIds, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            HashSet<int> result = countyIds is null ? [] : [.. countyIds];
            if (result.Count == 0)
            {
                return result;
            }

            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(result, commandTimeout, cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return null;
            }

            HashSet<string> codes = [.. administrativeAreal2DReferences.Select(x => x?.Code).OfType<string>().Where(x => !string.IsNullOrWhiteSpace(x))];
            if (codes.Count == 0)
            {
                return result;
            }

            Dictionary<string, HashSet<int>>? countyIds_ByCode = await administrativeAreal2DPostgreSQLConverter.GetIdsByCodesAsync(codes, Enums.AdministrativeArealType.County, commandTimeout, cancellationToken);
            if (countyIds_ByCode is null)
            {
                return null;
            }

            foreach (HashSet<int>? countyIds_Code in countyIds_ByCode.Values)
            {
                if (countyIds_Code is not null)
                {
                    result.UnionWith(countyIds_Code);
                }
            }

            return result;
        }
    }
}
