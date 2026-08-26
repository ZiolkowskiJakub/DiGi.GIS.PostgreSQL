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
        /// Asynchronously measures, for one county, how much of each of its subdivisions' buildings the orthophoto store holds.
        /// <para>What the estimated partition counts cannot answer. Both tables are partitioned by <c>county_id</c>, so <c>reltuples</c> describes a whole county and there is no subdivision-level figure to be had from it - reporting the county's own factor for a subdivision is <see href="https://github.com/ZiolkowskiJakub/DiGi.GIS.WebAPI/issues/8">DiGi.GIS.WebAPI issue #8</see>. This counts instead of estimating, and costs one read per side however many subdivisions are asked about.</para>
        /// <para><b>The orthophoto side's own <c>subdivision_id</c> is deliberately not used, and grouping by it would be wrong.</b> That column has never been written: not one of the 8 384 055 rows stored across 225 counties carries a value, measured 2026-08-26 through <c>gis/ortodatas/summariesbycountyids</c>. Grouping the orthophoto side by it answers zero for every subdivision in the country - a different wrong number, not an honest one. Populating it is an unfinished migration, and even once it runs the value is a copy of the building's, which is the defect class issues #23, #31 and #36 exist for. <c>building_2d</c> is the side that knows which subdivision a building belongs to, so a building is attributed there and the orthophoto side is asked only whether it holds that reference.</para>
        /// <para>The two tables live in different databases - <c>building_2d</c> in the main store, <c>orto_datas</c> in the storage one - so this cannot be a join and is not one. Each side is read once, cheaply, and matched in memory, the same way <see cref="SubdivisionLinksAsync(OrtoDatasPostgreSQLConverter?, Building2DPostgreSQLConverter?, int, int, int, CancellationToken)"/> does.</para>
        /// </summary>
        /// <param name="ortoDatasPostgreSQLConverter">The converter reading the orthophoto store.</param>
        /// <param name="building2DPostgreSQLConverter">The converter reading the building store.</param>
        /// <param name="countyId">The identifier of the county to measure. One polygon part, not a code - a multi-part county is measured a part at a time.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of each command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains one <see cref="OrtoDatasCoverageResult"/> per subdivision the county's buildings name, plus one carrying a null <see cref="OrtoDatasCoverageResult.SubdivisionId"/> for the buildings that name none when there are any; or null when either converter is missing, either side could not be read, or the county holds no orthophoto row at all.</returns>
        public static async Task<List<OrtoDatasCoverageResult>?> SubdivisionCoveragesAsync(this OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter, Building2DPostgreSQLConverter? building2DPostgreSQLConverter, int countyId, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (ortoDatasPostgreSQLConverter is null || building2DPostgreSQLConverter is null)
            {
                return null;
            }

            // Only the keys are wanted here - the reference is the whole of what the orthophoto side can say.
            // The read projects two columns and never object, which is what makes a whole-county comparison
            // affordable at all: that column holds the imagery for every year the row carries.
            Dictionary<string, int?>? subdivisionIds_OrtoDatas = await ortoDatasPostgreSQLConverter.GetSubdivisionIdsByCountyIdAsync(countyId, commandTimeout, cancellationToken);
            // An empty result is not an empty answer. A county holding no orthophoto row at all is a county
            // nothing has ever been downloaded for, which is a different fact from one that was downloaded and
            // covers none of its buildings - and the caller has to be able to tell them apart, because the
            // second is a measurement of nought per cent and the first is no measurement. Answered as nothing
            // here so that it reaches a caller the same way an absent partition does at county level, rather
            // than as a set of subdivisions each reporting an authoritative-looking zero.
            if (subdivisionIds_OrtoDatas is null || subdivisionIds_OrtoDatas.Count == 0)
            {
                return null;
            }

            List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, subdivisionId: null, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (building2DReferences is null)
            {
                return null;
            }

            Dictionary<int, long> building2DCounts = [];
            Dictionary<int, long> ortoDatasCounts = [];

            long building2DCount_Unassigned = 0;
            long ortoDatasCount_Unassigned = 0;

            // A reference is unique only per county_id and no constraint enforces it, so a repeat inside one
            // county would otherwise be counted twice on the building side while the orthophoto side, keyed by
            // a unique index, counts it once - and the factor would climb above what the county actually holds.
            HashSet<string> references = [];

            foreach (Building2DReference building2DReference in building2DReferences)
            {
                if (building2DReference?.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
                {
                    continue;
                }

                if (!references.Add(reference))
                {
                    continue;
                }

                bool hasOrtoDatas = subdivisionIds_OrtoDatas.ContainsKey(reference);

                if (building2DReference.SubdivisionId is not int subdivisionId)
                {
                    building2DCount_Unassigned++;

                    if (hasOrtoDatas)
                    {
                        ortoDatasCount_Unassigned++;
                    }

                    continue;
                }

                building2DCounts.TryGetValue(subdivisionId, out long building2DCount);
                building2DCounts[subdivisionId] = building2DCount + 1;

                if (hasOrtoDatas)
                {
                    ortoDatasCounts.TryGetValue(subdivisionId, out long ortoDatasCount);
                    ortoDatasCounts[subdivisionId] = ortoDatasCount + 1;
                }
            }

            List<OrtoDatasCoverageResult> result = [];

            foreach (int subdivisionId in building2DCounts.Keys.OrderBy(x => x))
            {
                ortoDatasCounts.TryGetValue(subdivisionId, out long ortoDatasCount);
                result.Add(new OrtoDatasCoverageResult(countyId, subdivisionId, building2DCounts[subdivisionId], ortoDatasCount));
            }

            // Kept apart rather than folded into a neighbour, and absent altogether when the county has none.
            // These buildings belong to no subdivision and to no municipality, so nothing below county level
            // may count them, but leaving them out of the result entirely would hide a county whose
            // subdivisions have not been resolved behind a set of subdivision figures that look complete.
            if (building2DCount_Unassigned > 0)
            {
                result.Add(new OrtoDatasCoverageResult(countyId, null, building2DCount_Unassigned, ortoDatasCount_Unassigned));
            }

            return result;
        }
    }
}
