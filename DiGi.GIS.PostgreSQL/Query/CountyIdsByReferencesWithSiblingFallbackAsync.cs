using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves each reference to the county part that holds its <c>building_2d</c> row, and - when the caller named only a subset of a county's parts - widens the unresolved ones to every part of the named code before giving up.
        /// <para><see cref="CountyIdsByReferencesAsync"/> probes only the county rows the caller sent. That is the safe answer for a caller who named every part, but it is lossy for one who named a single part of a multi-part county: a datum whose 2D building is filed under a sibling part comes back unresolved and is left unwritten, even though the county the caller named does hold it.</para>
        /// <para>This method keeps that first pass exactly, then adds one bounded widening. The references the first pass leaves unresolved are re-resolved against every part of the codes the candidate rows name, so a reference held by any part of a named county lands under that part. A reference held by no part of a named county stays unresolved - the widening never crosses into a county the caller did not name.</para>
        /// <para>The <see cref="CountyIdsByReferencesAsync"/> determinism contract is preserved: each pass probes its parts in ascending order and takes the first part that holds a reference, so a reference held by more than one part resolves to the same part on every run.</para>
        /// <para>It lives next to <see cref="CountyIdsByReferencesAsync"/> rather than in a host because the Web API, the desktop application and any background task have to answer "which part of this county holds that reference" the same way; answering it per controller is how a batch came to be filed under one part in the first place. A caller who already named every part sees no difference - the first pass resolves everything, nothing is left to widen, and the second pass is not reached.</para>
        /// </summary>
        /// <param name="building2DPostgreSQLConverter">The converter used to look the references up in <c>building_2d</c>.</param>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter used to widen the candidate rows to every part of their codes.</param>
        /// <param name="references">The references to resolve.</param>
        /// <param name="countyIds">The candidate county rows the caller named, normally one or more polygon parts of one county.</param>
        /// <param name="administrativeArealType">The level the candidate rows name; the widening reads the parts of their codes at this level only. <see cref="Enums.AdministrativeArealType.County"/> for the building-keyed tables this serves.</param>
        /// <param name="commandTimeout">The timeout in seconds for the widening lookups. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>The identifier of the county row holding each reference. A reference no part of a named county holds is absent from the result.</returns>
        public static async Task<Dictionary<string, int>> CountyIdsByReferencesWithSiblingFallbackAsync(this Building2DPostgreSQLConverter? building2DPostgreSQLConverter, AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, IEnumerable<string?>? references, IEnumerable<int>? countyIds, Enums.AdministrativeArealType administrativeArealType = Enums.AdministrativeArealType.County, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            Dictionary<string, int> result = await CountyIdsByReferencesAsync(building2DPostgreSQLConverter, references, countyIds);

            if (references is null || countyIds is null || administrativeAreal2DPostgreSQLConverter is null)
            {
                return result;
            }

            HashSet<string> references_Unresolved = [];
            foreach (string? reference in references)
            {
                if (reference is not null && !string.IsNullOrWhiteSpace(reference) && !result.ContainsKey(reference))
                {
                    references_Unresolved.Add(reference);
                }
            }

            if (references_Unresolved.Count == 0)
            {
                return result;
            }

            // The named rows stay candidates, and every part their codes name is added on: the widening is bounded to the counties the caller named.
            HashSet<int> countyIds_Expanded = [.. new HashSet<int>(countyIds)];

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(countyIds, commandTimeout, cancellationToken);
            if (administrativeAreal2DReferences is not null)
            {
                HashSet<string> codes = [];
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                {
                    if (administrativeAreal2DReference?.Code is string code && !string.IsNullOrWhiteSpace(code))
                    {
                        codes.Add(code);
                    }
                }

                if (codes.Count > 0)
                {
                    Dictionary<string, HashSet<int>>? countyIds_ByCode = await administrativeAreal2DPostgreSQLConverter.GetIdsByCodesAsync(codes, administrativeArealType, commandTimeout, cancellationToken);
                    if (countyIds_ByCode is not null)
                    {
                        foreach (HashSet<int>? parts in countyIds_ByCode.Values)
                        {
                            if (parts is not null)
                            {
                                countyIds_Expanded.UnionWith(parts);
                            }
                        }
                    }
                }
            }

            Dictionary<string, int> result_Fallback = await CountyIdsByReferencesAsync(building2DPostgreSQLConverter, references_Unresolved, countyIds_Expanded);

            foreach (KeyValuePair<string, int> keyValuePair in result_Fallback)
            {
                result[keyValuePair.Key] = keyValuePair.Value;
            }

            return result;
        }
    }
}
