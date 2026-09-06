using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Orders county polygon parts into one group per county code, each group widened to every part the caller has looked up for its code.
        /// <para>A county code is not a key - it names one row per polygon part - so a run driven per identifier would sample a multi-part county once per part. Driven per group instead, each code's territory is reached exactly once, and every point it produces can then be filed under the part containing it. The widening is handed in rather than looked up here: the parts of a code live in the database, and this method is pure.</para>
        /// <para>Groups are ordered by their lowest identifier and each group is sorted ascending, so a run that is stopped and started again walks the counties in the same order and its progress means the same thing. A part with no usable code groups with itself.</para>
        /// </summary>
        /// <param name="countyReferences">The county parts the caller named.</param>
        /// <param name="countyIds_ByCode">Every part of each code, as looked up from the database, keyed by code. Only codes present among the references are widened; a code the dictionary does not name keeps the parts supplied with it.</param>
        /// <returns>One group of identifiers per county code, ordered by lowest identifier, or an empty list when nothing was supplied.</returns>
        public static List<List<int>> CountyIdGroups(this IEnumerable<AdministrativeAreal2DReference>? countyReferences, IReadOnlyDictionary<string, HashSet<int>>? countyIds_ByCode = null)
        {
            List<List<int>> result = [];

            if (countyReferences is null)
            {
                return result;
            }

            Dictionary<string, List<int>> countyIds_ByCode_Supplied = [];
            List<int> countyIds_Codeless = [];

            foreach (AdministrativeAreal2DReference? countyReference in countyReferences)
            {
                if (countyReference is null)
                {
                    continue;
                }

                if (countyReference.Code is string code && !string.IsNullOrWhiteSpace(code))
                {
                    if (!countyIds_ByCode_Supplied.TryGetValue(code, out List<int>? countyIds))
                    {
                        countyIds = [];
                        countyIds_ByCode_Supplied[code] = countyIds;
                    }

                    countyIds.Add(countyReference.Id);
                }
                else
                {
                    countyIds_Codeless.Add(countyReference.Id);
                }
            }

            HashSet<int> countyIds_Grouped = [];

            foreach (KeyValuePair<string, List<int>> keyValuePair in countyIds_ByCode_Supplied)
            {
                List<int> countyIds_Group;
                if (countyIds_ByCode is not null && countyIds_ByCode.TryGetValue(keyValuePair.Key, out HashSet<int>? countyIds_Widened) && countyIds_Widened is not null && countyIds_Widened.Count > 0)
                {
                    countyIds_Group = [.. countyIds_Widened];
                }
                else
                {
                    countyIds_Group = [.. keyValuePair.Value];
                }

                countyIds_Group.Sort();

                // A widened group reaches parts that were never named, and two codes widening into a shared
                // identifier would have one part sampled twice. The first code here keeps it.
                countyIds_Group.RemoveAll(x => !countyIds_Grouped.Add(x));

                if (countyIds_Group.Count > 0)
                {
                    result.Add(countyIds_Group);
                }
            }

            foreach (int countyId in countyIds_Codeless)
            {
                if (countyIds_Grouped.Add(countyId))
                {
                    result.Add([countyId]);
                }
            }

            // A dictionary does not order, so the groups are placed by their lowest identifier: the first
            // county a run reaches is a property of the data, not of the enumeration.
            result.Sort((x, y) => x[0].CompareTo(y[0]));

            return result;
        }
    }
}
