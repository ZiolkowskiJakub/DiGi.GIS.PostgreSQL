using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Reads which county row each reference belongs to, from the <c>building_2d</c> row that holds it.
        /// <para>A county code names one <c>administrative_areal_2d</c> row per polygon part, so a code cannot say which part an item belongs to. The 2D building already answers that - it was filed by geometry when it was imported - and reading it back keeps every table keyed by the same <c>(county_id, reference)</c> pair. Filing a whole batch under one part instead is what left sibling parts reading back empty while the upload reported success.</para>
        /// <para>The parts are probed in ascending order, one batched lookup each, and a reference is taken by the first part that holds it. A reference held by more than one part therefore resolves to the same one on every run.</para>
        /// <para>A reference no part holds is simply absent from the result: nothing states where it belongs, and the caller decides whether to drop it or resolve it some other way.</para>
        /// </summary>
        /// <param name="building2DPostgreSQLConverter">The converter used to look the references up.</param>
        /// <param name="references">The references to resolve.</param>
        /// <param name="countyIds">The candidate county rows, normally every polygon part of one code.</param>
        /// <para>It lives here rather than in a host because it is a question about <c>building_2d</c> and nothing else: the Web API, the desktop application and any background task all have to answer it the same way, and each answering it for itself is how a batch came to be filed under one part in the first place.</para>
        /// <returns>The identifier of the county row holding each reference. Empty when nothing could be resolved.</returns>
        public static async Task<Dictionary<string, int>> CountyIdsByReferencesAsync(this Building2DPostgreSQLConverter? building2DPostgreSQLConverter, IEnumerable<string?>? references, IEnumerable<int>? countyIds)
        {
            Dictionary<string, int> result = [];

            if (building2DPostgreSQLConverter is null || references is null || countyIds is null)
            {
                return result;
            }

            HashSet<string> references_Unresolved = [.. references.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x!)];
            if (references_Unresolved.Count == 0)
            {
                return result;
            }

            List<int> countyIds_Sorted = [.. new HashSet<int>(countyIds).OrderBy(x => x)];

            foreach (int countyId in countyIds_Sorted)
            {
                if (references_Unresolved.Count == 0)
                {
                    break;
                }

                List<Building2DReference> building2DReferences_Requested = [.. references_Unresolved.Select(x => new Building2DReference() { Reference = x, CountyId = countyId })];

                List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesAsync(building2DReferences_Requested);
                if (building2DReferences is null)
                {
                    continue;
                }

                foreach (Building2DReference building2DReference in building2DReferences)
                {
                    string? reference = building2DReference?.Reference;
                    if (string.IsNullOrWhiteSpace(reference) || !references_Unresolved.Remove(reference!))
                    {
                        continue;
                    }

                    result[reference!] = countyId;
                }
            }

            return result;
        }
    }
}
