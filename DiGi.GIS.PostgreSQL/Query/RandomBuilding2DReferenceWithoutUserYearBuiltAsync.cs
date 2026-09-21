using DiGi.GIS.PostgreSQL.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Asynchronously draws one building that has orthophoto coverage and no user-provided year built yet, optionally confined to specific <c>building_2d</c> parts.
        /// <para>The orthophoto rows live in the storage database and the building, year-built and administrative rows in the main one, so this cannot be a join and is not one (the same split <see cref="SubdivisionLinksAsync(OrtoDatasPostgreSQLConverter, Building2DPostgreSQLConverter, int, int, int, CancellationToken)"/> works across). Stage 1 draws a county code from the parts that hold orthophotos, weighted by the estimated rows of the parts (<see cref="OrtoDatasPostgreSQLConverter.GetEstimatedCountsAsync(IEnumerable{int}, bool, int, int, CancellationToken)"/>, storage side) over the county references (<see cref="AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType, int?, bool, int, CancellationToken)"/>, main side). Stage 2 draws a batch of references from the code's parts without reading the imagery (<see cref="OrtoDatasPostgreSQLConverter.GetRandomReferencesByCountyIdsAsync(IEnumerable{int}, int, int, CancellationToken)"/>), keeps the ones that are buildings without a user entry (<see cref="YearBuiltDataPostgreSQLConverter.GetBuilding2DReferencesWithoutUserYearBuiltAsync(IEnumerable{int}, IEnumerable{string}, int, CancellationToken)"/>), and answers the first survivor that holds at least one photo year (<see cref="OrtoDatasPostgreSQLConverter.GetYearsByReferenceAsync(string, int?, bool, int, CancellationToken)"/>), so the drawn building always carries a card.</para>
        /// <para>A code whose parts yield nothing within <paramref name="maxBatchCount"/> batches is removed and redrawn, so the loop is bounded by codes times batches; <c>null</c> is the answer when nothing is left. <paramref name="countyIds"/> are <c>building_2d</c> part ids, never county codes; <c>null</c> or empty draws from every covered part, and an id that names no covered part simply falls out of the pool. A building holding only <c>PredictedYearBuilt</c> entries is eligible; a <c>UserYearBuilt</c> of any relation is not, because a bound is still a verification.</para>
        /// </summary>
        /// <param name="ortoDatasPostgreSQLConverter">The converter reading the orthophoto store.</param>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter reading the administrative areas, in the main store.</param>
        /// <param name="yearBuiltDataPostgreSQLConverter">The converter reading the buildings and their year-built rows, in the main store.</param>
        /// <param name="countyIds">The <c>building_2d</c> part ids to confine the draw to, or null/empty to draw from every covered part.</param>
        /// <param name="batchSize">How many references one storage-side draw takes before the main side filters them.</param>
        /// <param name="maxBatchCount">How many batches a drawn code is given before it is removed from the pool.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of each command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the drawn <see cref="Building2DReference"/>, or null when a converter is missing, either side could not be read, or no covered (and requested) county part yields a candidate.</returns>
        public static async Task<Building2DReference?> RandomBuilding2DReferenceWithoutUserYearBuiltAsync(this OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter, AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, YearBuiltDataPostgreSQLConverter? yearBuiltDataPostgreSQLConverter, IEnumerable<int>? countyIds = null, int batchSize = 64, int maxBatchCount = 4, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (ortoDatasPostgreSQLConverter is null || administrativeAreal2DPostgreSQLConverter is null || yearBuiltDataPostgreSQLConverter is null || batchSize <= 0 || maxBatchCount <= 0)
            {
                return null;
            }

            // Stage 1 - the codes that hold orthophotos, weighted by the estimate of what their parts hold.
            List<AdministrativeAreal2DReference>? countyReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Enums.AdministrativeArealType.County, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (countyReferences is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "RandomBuilding2DReferenceWithoutUserYearBuilt: county references could not be read (main store)");
                return null;
            }

            HashSet<int>? countyIds_Filter = countyIds is null ? null : [.. countyIds];
            if (countyIds_Filter is { Count: 0 })
            {
                countyIds_Filter = null;
            }

            Dictionary<string, List<int>> parts_ByCode = [];
            foreach (AdministrativeAreal2DReference? countyReference in countyReferences)
            {
                if (countyReference is null || string.IsNullOrWhiteSpace(countyReference.Code))
                {
                    continue;
                }

                // Filtering at the part level (not the code level) is what lets a caller draw from one part of
                // a multi-part code; the code is then weighted by what survives.
                if (countyIds_Filter is not null && !countyIds_Filter.Contains(countyReference.Id))
                {
                    continue;
                }

                string code = countyReference.Code!;
                if (!parts_ByCode.TryGetValue(code, out List<int>? parts))
                {
                    parts = [];
                    parts_ByCode[code] = parts;
                }

                parts.Add(countyReference.Id);
            }

            if (parts_ByCode.Count == 0)
            {
                return null;
            }

            List<int> countyIds_All = [.. parts_ByCode.Values.SelectMany(x => x)];

            Dictionary<int, long>? estimates = await ortoDatasPostgreSQLConverter.GetEstimatedCountsAsync(countyIds_All, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (estimates is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "RandomBuilding2DReferenceWithoutUserYearBuilt: orthophoto estimates could not be read (storage)");
                return null;
            }

            List<KeyValuePair<string, long>> candidates = [];
            foreach (KeyValuePair<string, List<int>> partGroup in parts_ByCode)
            {
                long weight = 0;
                foreach (int countyId in partGroup.Value)
                {
                    // count <= 0 covers the absent partition and the never-analysed one (-1): neither is
                    // evidence the part holds rows, so it stays out of the pool.
                    if (estimates.TryGetValue(countyId, out long estimate) && estimate > 0)
                    {
                        weight += estimate;
                    }
                }

                if (weight > 0)
                {
                    candidates.Add(new KeyValuePair<string, long>(partGroup.Key, weight));
                }
            }

            Serilog.Modify.Log("RandomBuilding2DReferenceWithoutUserYearBuilt: {CodeCount} codes over {PartCount} parts, {CandidateCount} with a positive estimate", parts_ByCode.Count, countyIds_All.Count, candidates.Count);

            // Stage 2 - one weighted draw per code; a code whose parts yield nothing is removed, so the loop is
            // bounded by the number of codes.
            while (candidates.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                long total = 0;
                foreach (KeyValuePair<string, long> candidate in candidates)
                {
                    total += candidate.Value;
                }

                long draw = Random.Shared.NextInt64(total);
                int index = candidates.Count - 1;
                for (int i = 0; i < candidates.Count; i++)
                {
                    if (draw < candidates[i].Value)
                    {
                        index = i;
                        break;
                    }

                    draw -= candidates[i].Value;
                }

                string code_Drawn = candidates[index].Key;
                candidates.RemoveAt(index);

                List<int> countyIds_Drawn = parts_ByCode[code_Drawn];

                for (int i = 0; i < maxBatchCount; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<string>? references = await ortoDatasPostgreSQLConverter.GetRandomReferencesByCountyIdsAsync(countyIds_Drawn, batchSize, commandTimeout, cancellationToken);
                    if (references is null || references.Count == 0)
                    {
                        Serilog.Modify.Log("RandomBuilding2DReferenceWithoutUserYearBuilt: code {Code} parts {Parts} batch {Batch}: {Outcome}", code_Drawn, string.Join(",", countyIds_Drawn), i, references is null ? "storage side answered null" : "no orthophoto rows");
                        break;
                    }

                    List<Building2DReference>? building2DReferences = await yearBuiltDataPostgreSQLConverter.GetBuilding2DReferencesWithoutUserYearBuiltAsync(countyIds_Drawn, references, commandTimeout, cancellationToken);
                    if (building2DReferences is null)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "RandomBuilding2DReferenceWithoutUserYearBuilt: code {Code} parts {Parts}: buildings without a user entry could not be read (main store)", code_Drawn, string.Join(",", countyIds_Drawn));
                        return null;
                    }

                    Serilog.Modify.Log("RandomBuilding2DReferenceWithoutUserYearBuilt: code {Code} parts {Parts} batch {Batch}: {ReferenceCount} references drawn, {SurvivorCount} without a user entry", code_Drawn, string.Join(",", countyIds_Drawn), i, references.Count, building2DReferences.Count);

                    foreach (Building2DReference building2DReference in building2DReferences)
                    {
                        if (building2DReference?.Reference is not string reference)
                        {
                            continue;
                        }

                        // The card guarantee, checked per survivor only: an orto_datas row with an empty Values array
                        // never produces a candidate. The photo may sit under a sibling part of a multi-part code,
                        // hence the fallback by reference.
                        List<short>? years = await ortoDatasPostgreSQLConverter.GetYearsByReferenceAsync(reference, building2DReference.CountyId, fallbackByReference: true, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                        if (years is { Count: > 0 })
                        {
                            return building2DReference;
                        }
                    }

                    // A batch smaller than asked is the whole part; no further batch can add anything.
                    if (references.Count < batchSize)
                    {
                        break;
                    }
                }
            }

            return null;
        }
    }
}
