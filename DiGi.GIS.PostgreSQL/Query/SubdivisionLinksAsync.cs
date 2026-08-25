using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Asynchronously compares, for one county, the subdivision each building is filed under against the one its orthophoto row carries.
        /// <para>The two tables are in different databases - <c>building_2d</c> in the main store, <c>orto_datas</c> in the storage one - so this cannot be a join and is not one. Each side is read once, cheaply, and matched in memory: the orthophoto side through <see cref="OrtoDatasPostgreSQLConverter.GetSubdivisionIdsByCountyIdAsync(int, int, CancellationToken)"/>, which projects two columns and never the imagery, and the building side through <see cref="Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int, int?, IEnumerable{string}?, int, CancellationToken)"/>, which already returns nothing heavier.</para>
        /// <para>That separation is the whole reason the value has to be pushed across by a refresh in the first place, and the reason nothing keeps the two in step on its own.</para>
        /// </summary>
        /// <param name="ortoDatasPostgreSQLConverter">The converter reading the orthophoto store.</param>
        /// <param name="building2DPostgreSQLConverter">The converter reading the building store.</param>
        /// <param name="countyId">The identifier of the county to compare. One polygon part, not a code.</param>
        /// <param name="sampleCount">How many references to name per disagreeing category, so a result stays a fixed size on a county with a hundred thousand buildings. Zero names none.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of each command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result carries the comparison, or null when either converter is missing or either side could not be read.</returns>
        public static async Task<OrtoDatasSubdivisionResult?> SubdivisionLinksAsync(this OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter, Building2DPostgreSQLConverter? building2DPostgreSQLConverter, int countyId, int sampleCount = 20, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (ortoDatasPostgreSQLConverter is null || building2DPostgreSQLConverter is null)
            {
                return null;
            }

            Dictionary<string, int?>? subdivisionIds_OrtoDatas = await ortoDatasPostgreSQLConverter.GetSubdivisionIdsByCountyIdAsync(countyId, commandTimeout, cancellationToken);
            if (subdivisionIds_OrtoDatas is null)
            {
                return null;
            }

            List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, subdivisionId: null, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (building2DReferences is null)
            {
                return null;
            }

            long building2DCount = 0;
            long matchedCount = 0;
            long bothCount = 0;
            long disagreeCount = 0;
            long ortoDatasOnlyCount = 0;
            long building2DOnlyCount = 0;
            long neitherCount = 0;

            List<string> references_OrtoDatasOnly = [];
            List<string> references_Building2DOnly = [];
            List<string> references_Disagree = [];

            // Walked from the building side and struck off the orthophoto side as it goes, so what is left in
            // the dictionary afterwards is exactly the rows no building of this county accounts for. A
            // reference repeated in the building list is therefore counted once, which matches the unique
            // index the orthophoto side is keyed by.
            foreach (Building2DReference building2DReference in building2DReferences)
            {
                if (building2DReference?.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
                {
                    continue;
                }

                building2DCount++;

                if (!subdivisionIds_OrtoDatas.Remove(reference, out int? subdivisionId_OrtoDatas))
                {
                    continue;
                }

                matchedCount++;

                int? subdivisionId_Building2D = building2DReference.SubdivisionId;

                if (subdivisionId_OrtoDatas.HasValue && subdivisionId_Building2D.HasValue)
                {
                    bothCount++;

                    if (subdivisionId_OrtoDatas.Value != subdivisionId_Building2D.Value)
                    {
                        disagreeCount++;
                        Sample(references_Disagree, reference);
                    }

                    continue;
                }

                if (subdivisionId_OrtoDatas.HasValue)
                {
                    ortoDatasOnlyCount++;
                    Sample(references_OrtoDatasOnly, reference);
                    continue;
                }

                if (subdivisionId_Building2D.HasValue)
                {
                    building2DOnlyCount++;
                    Sample(references_Building2DOnly, reference);
                    continue;
                }

                neitherCount++;
            }

            // Whatever survived the strike-off, plus what was matched, is the whole of the orthophoto side.
            long ortoDatasCount = matchedCount + subdivisionIds_OrtoDatas.Count;

            return new OrtoDatasSubdivisionResult(countyId, ortoDatasCount, building2DCount, matchedCount, bothCount, disagreeCount, ortoDatasOnlyCount, building2DOnlyCount, neitherCount, references_OrtoDatasOnly, references_Building2DOnly, references_Disagree);

            void Sample(List<string> references, string reference)
            {
                if (references.Count >= sampleCount)
                {
                    return;
                }

                references.Add(reference);
            }
        }
    }
}
