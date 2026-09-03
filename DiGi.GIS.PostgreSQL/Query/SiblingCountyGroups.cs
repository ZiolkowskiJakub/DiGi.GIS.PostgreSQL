using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Groups county polygon parts by code: every part is mapped to the full set of parts that share its code.
        /// <para>A county code is not a key - it names one row per polygon part - so the result is keyed by part <c>Id</c>, and a part with no usable code groups with itself. This is the single definition the building data update uses to reach a building under every part of its subdivision's parent county.</para>
        /// </summary>
        /// <param name="countyReferences">County references to group. May be <see langword="null"/>, in which case the result is empty.</param>
        /// <returns>A map from each county part <c>Id</c> to the set of part <c>Id</c>s sharing its code; a code-less part maps to itself.</returns>
        public static Dictionary<int, HashSet<int>> SiblingCountyGroups(this IEnumerable<AdministrativeAreal2DReference>? countyReferences)
        {
            if (countyReferences is null)
            {
                return [];
            }

            Dictionary<string, HashSet<int>> siblingIds_ByCode = [];
            Dictionary<int, HashSet<int>> siblingIds_ByCountyId = [];

            foreach (AdministrativeAreal2DReference? countyReference in countyReferences)
            {
                if (countyReference is null)
                {
                    continue;
                }

                if (countyReference.Code is { } code && !string.IsNullOrWhiteSpace(code))
                {
                    if (!siblingIds_ByCode.TryGetValue(code, out HashSet<int>? siblingIds))
                    {
                        siblingIds = [];
                        siblingIds_ByCode[code] = siblingIds;
                    }

                    siblingIds.Add(countyReference.Id);
                    siblingIds_ByCountyId[countyReference.Id] = siblingIds;
                }
                else
                {
                    siblingIds_ByCountyId[countyReference.Id] = [countyReference.Id];
                }
            }

            return siblingIds_ByCountyId;
        }
    }
}
