using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Computes, for every county polygon part, the set of subdivision identifiers the subdivision loop reaches under it.
        /// <para>The building data update walks subdivisions and reaches a building only when the building's county part sits in the sibling group of the subdivision's parent county. This is the exact set the final per-county pass must leave untouched: a building whose part does not sit in its subdivision's parent group is invisible to the loop, so only the fallback can write it.</para>
        /// <para>A county code is not a key - it names one row per polygon part - so the in-scope set is keyed by part identifier, and a subdivision filed under one part is in scope for every part of that part's code group. A subdivision without a parent county is out of scope everywhere.</para>
        /// </summary>
        /// <param name="subdivisions">The subdivision references, each carrying an identifier and its parent county identifier. May be <see langword="null"/>.</param>
        /// <param name="siblingCountyGroups">Each county part mapped to every part that shares its code. May be <see langword="null"/>, in which case each subdivision is in scope for its parent part only.</param>
        /// <returns>A map from each in-scope county part to the subdivision identifiers the subdivision loop reaches under it. Parts with no in-scope subdivisions are absent from the map.</returns>
        public static Dictionary<int, HashSet<int>> InScopeSubdivisionIds(IEnumerable<AdministrativeAreal2DReference>? subdivisions, IReadOnlyDictionary<int, HashSet<int>>? siblingCountyGroups)
        {
            Dictionary<int, HashSet<int>> inScopeSubdivisionIds_ByCountyId = [];

            if (subdivisions is null)
            {
                return inScopeSubdivisionIds_ByCountyId;
            }

            foreach (AdministrativeAreal2DReference? subdivision in subdivisions)
            {
                if (subdivision?.CountyId is not int parentCountyId)
                {
                    continue;
                }

                IEnumerable<int> parts;
                if (siblingCountyGroups is not null && siblingCountyGroups.TryGetValue(parentCountyId, out HashSet<int>? siblingCountyIds) && siblingCountyIds.Count > 0)
                {
                    parts = siblingCountyIds;
                }
                else
                {
                    parts = [parentCountyId];
                }

                foreach (int part in parts)
                {
                    if (!inScopeSubdivisionIds_ByCountyId.TryGetValue(part, out HashSet<int>? inScopeSubdivisionIds_Part))
                    {
                        inScopeSubdivisionIds_Part = [];
                        inScopeSubdivisionIds_ByCountyId[part] = inScopeSubdivisionIds_Part;
                    }

                    inScopeSubdivisionIds_Part.Add(subdivision.Id);
                }
            }

            return inScopeSubdivisionIds_ByCountyId;
        }
    }
}
