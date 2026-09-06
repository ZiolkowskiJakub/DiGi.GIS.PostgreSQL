using DiGi.GIS.PostgreSQL.Classes;
using Npgsql;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves county part identifiers into one group per county code, each group widened to every part the database holds for its code.
        /// <para>A county code names one <c>administrative_areal_2d</c> row per polygon part, so a run driven per identifier would sample or repair a multi-part county once per part. The groups this returns are the scope a run walks instead: one group per code, each covering the whole county, so its territory is reached exactly once and every point it produces can be filed under the part containing it.</para>
        /// <para>It lives here rather than in a host because it is the same question for every terrain caller: the sampling task, the gap-fill task and any future one have to mean the same thing by a county, and each answering it for itself is how a whole county came to be stored once per part in the first place.</para>
        /// </summary>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter used to read the parts and their codes.</param>
        /// <param name="countyIds">The county part identifiers the run is scoped to.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of each command.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by the caller to cancel the asynchronous operation.</param>
        /// <returns>One sorted group of part identifiers per county code, ordered by lowest identifier, or null when the parts or their codes could not be read - an unreadable scope must not silently narrow to the named parts and file a whole county under one of them.</returns>
        public static async Task<List<List<int>>?> CountyIdGroupsAsync(this AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, IEnumerable<int>? countyIds, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (administrativeAreal2DPostgreSQLConverter is null || countyIds is null)
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences;

            await using (NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(administrativeAreal2DPostgreSQLConverter.ConnectionData))
            {
                if (npgsqlConnection is null)
                {
                    return null;
                }

                await npgsqlConnection.OpenAsync(cancellationToken);

                administrativeAreal2DReferences = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, countyIds, commandTimeout, cancellationToken);
                if (administrativeAreal2DReferences is null)
                {
                    return null;
                }

                HashSet<string> codes = [];
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                {
                    if (administrativeAreal2DReference?.Code is string code && !string.IsNullOrWhiteSpace(code))
                    {
                        codes.Add(code);
                    }
                }

                if (codes.Count == 0)
                {
                    return administrativeAreal2DReferences.CountyIdGroups();
                }

                // One batched lookup for every code named, widened to the parts the table holds for each.
                Dictionary<string, HashSet<int>>? countyIds_ByCode = await AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodesAsync(npgsqlConnection, codes, Enums.AdministrativeArealType.County, commandTimeout, cancellationToken);
                if (countyIds_ByCode is null)
                {
                    return null;
                }

                return administrativeAreal2DReferences.CountyIdGroups(countyIds_ByCode);
            }
        }
    }
}
