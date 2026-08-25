using DiGi.GIS.PostgreSQL.Classes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        /// <summary>
        /// Asynchronously queues the orthophoto downloads the given counties are short of.
        /// <para>This writes no orthophoto data of its own. Each county's building references are read, the ones already stored are dropped unless <see cref="PostgreSQLOrtoDatasRefreshOptions.OverrideExisting"/> says otherwise, and the rest are appended to the queue the download task drains. What comes back is therefore work scheduled, not work done.</para>
        /// <para>With <see cref="PostgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds"/> set, a second and unrelated thing happens first: each building's own subdivision identifier is pushed onto its stored row. A building whose subdivision has not been resolved is skipped rather than clearing the stored one.</para>
        /// <para>A county that fails is logged with the exception that stopped it and stepped over, so one unreachable partition cannot cost the run the counties behind it. The counties are visited in ascending order, so a run started again covers them in the same order as the one it replaces.</para>
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The manager used to retrieve the necessary PostgreSQL converters.</param>
        /// <param name="postgreSQLOrtoDatasRefreshOptions">The options specifying how the orthophoto queue should be refreshed. Null uses the defaults.</param>
        /// <param name="progress">An optional progress reporter carrying the running total of references the queue has accepted.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result carries what the run queued and what it stepped over, or null when the run could not be attempted at all - a converter missing, or the counties unreadable.</returns>
        public static async Task<PostgreSQLOrtoDatasRefreshResult?> RefreshOrtoDatasAsync(this GISPostgreSQLConverterManager? gISPostgreSQLConverterManager, PostgreSQLOrtoDatasRefreshOptions? postgreSQLOrtoDatasRefreshOptions = null, IProgress<long>? progress = null, CancellationToken cancellationToken = default)
        {
            if (gISPostgreSQLConverterManager?.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>() is not AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no {Converter} - the counties cannot be read", nameof(RefreshOrtoDatasAsync), nameof(AdministrativeAreal2DPostgreSQLConverter));
                return null;
            }

            if (gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DPostgreSQLConverter>() is not Building2DPostgreSQLConverter building2DPostgreSQLConverter)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no {Converter} - the building references cannot be read", nameof(RefreshOrtoDatasAsync), nameof(Building2DPostgreSQLConverter));
                return null;
            }

            if (gISPostgreSQLConverterManager.GetPostgreSQLConverter<OrtoDatasPostgreSQLConverter>() is not OrtoDatasPostgreSQLConverter ortoDatasPostgreSQLConverter)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no {Converter} - nothing can be queued", nameof(RefreshOrtoDatasAsync), nameof(OrtoDatasPostgreSQLConverter));
                return null;
            }

            postgreSQLOrtoDatasRefreshOptions ??= new PostgreSQLOrtoDatasRefreshOptions();

            int batchSize = postgreSQLOrtoDatasRefreshOptions.BatchSize < 1 ? 1 : postgreSQLOrtoDatasRefreshOptions.BatchSize;
            int commandTimeout = postgreSQLOrtoDatasRefreshOptions.CommandTimeout < 0 ? 0 : postgreSQLOrtoDatasRefreshOptions.CommandTimeout;
            bool overrideExisting = postgreSQLOrtoDatasRefreshOptions.OverrideExisting;
            bool updateSubdivisionIds = postgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds;

            // Identifiers rather than codes: a county held in several pieces is one row per piece, and each
            // piece is its own partition, so walking the identifiers reaches all of its territory exactly once.
            HashSet<int>? countyIds = postgreSQLOrtoDatasRefreshOptions.CountyIds;
            countyIds ??= await administrativeAreal2DPostgreSQLConverter.GetIdsAsync(Enums.AdministrativeArealType.County, cancellationToken);
            if (countyIds is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: the counties could not be read - the run cannot be scoped", nameof(RefreshOrtoDatasAsync));
                return null;
            }

            // Sorted so that a run stopped and started again covers the counties in the same order.
            List<int> countyIds_Sorted = [.. countyIds];
            countyIds_Sorted.Sort();

            long countyCount = countyIds_Sorted.Count;
            long readCount = 0;
            long enqueuedCount = 0;
            long subdivisionIdCount = 0;
            long failedCountyCount = 0;
            bool cancelled = false;

            if (countyIds_Sorted.Count == 0)
            {
                Serilog.Modify.Log("{Method}: no county was named - nothing to queue", nameof(RefreshOrtoDatasAsync));
                return new PostgreSQLOrtoDatasRefreshResult(countyCount, readCount, enqueuedCount, subdivisionIdCount, failedCountyCount, cancelled);
            }

            Serilog.Modify.Log(
                "{Method} started: {CountyCount} counties, batch {BatchSize}, timeout {CommandTimeout} s, override existing {OverrideExisting}, update subdivision identifiers {UpdateSubdivisionIds}",
                nameof(RefreshOrtoDatasAsync), countyIds_Sorted.Count, batchSize, commandTimeout, overrideExisting, updateSubdivisionIds);

            foreach (int countyId in countyIds_Sorted)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancelled = true;
                    break;
                }

                // Held against the running totals rather than accumulated separately, so a county left part
                // way through - cancelled, or thrown out of - still reports what it managed to write. The
                // writes are committed by then either way, and a tally that forgot them would understate a
                // queue somebody else is about to drain.
                long enqueuedCount_County = enqueuedCount;
                long readCount_County = readCount;
                long subdivisionIdCount_County = subdivisionIdCount;

                // At most one per county however many of its batches fail, because the county is the unit
                // somebody goes and reads the log for.
                bool failed = false;

                try
                {
                    List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, subdivisionId: null, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    if (building2DReferences is null)
                    {
                        failed = true;
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "OrtoDatas refresh skipped - county {CountyId}, its building references could not be read", countyId);
                    }
                    else if (building2DReferences.Count != 0)
                    {
                        readCount += building2DReferences.Count;

                        // A county is tens of thousands of references and the largest are over a hundred
                        // thousand. Each chunk is one statement per stage, addressed to this county's partition
                        // alone.
                        for (int index = 0; index < building2DReferences.Count; index += batchSize)
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                cancelled = true;
                                break;
                            }

                            List<Building2DReference> building2DReferences_Batch = building2DReferences.GetRange(index, Math.Min(batchSize, building2DReferences.Count - index));

                            if (updateSubdivisionIds)
                            {
                                List<Building2DReference>? building2DReferences_Updated = await ortoDatasPostgreSQLConverter.UpdateSubdivisionIdsAsync(building2DReferences_Batch, fallbackByReference: false, countyId: countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                                subdivisionIdCount += building2DReferences_Updated?.Count ?? 0;
                            }

                            List<Building2DReference>? building2DReferences_Queue = building2DReferences_Batch;

                            if (!overrideExisting)
                            {
                                building2DReferences_Queue = await ortoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(building2DReferences_Batch, inverted: true, fallbackByReference: false, countyId: countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                                if (building2DReferences_Queue is null)
                                {
                                    failed = true;
                                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "OrtoDatas refresh batch skipped - county {CountyId}, {Count} references could not be checked against what is already stored", countyId, building2DReferences_Batch.Count);
                                    continue;
                                }
                            }

                            if (building2DReferences_Queue.Count == 0)
                            {
                                continue;
                            }

                            List<Building2DReference>? building2DReferences_Enqueued = await ortoDatasPostgreSQLConverter.UpdateBuilding2DReferencesAsync(building2DReferences_Queue, commandTimeout, cancellationToken);
                            if (building2DReferences_Enqueued is null)
                            {
                                failed = true;
                                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "OrtoDatas refresh batch not queued - county {CountyId}, {Count} references were accepted by nothing", countyId, building2DReferences_Queue.Count);
                                continue;
                            }

                            enqueuedCount += building2DReferences_Enqueued.Count;

                            // Reported after the write and from what the queue accepted, so the figure is work
                            // actually scheduled rather than buildings looked at.
                            progress?.Report(enqueuedCount);
                        }
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    // Stopping is not a failure of this county, and the tallies gathered so far are still worth
                    // handing back, so the loop is left rather than the exception propagated.
                    cancelled = true;
                }
                catch (Exception exception)
                {
                    failed = true;
                    Serilog.Modify.Log(exception, "OrtoDatas refresh county failed - county {CountyId}", countyId);
                }

                if (failed)
                {
                    failedCountyCount++;
                }

                // A county holding no buildings at all says nothing worth a line of its own, and there are
                // enough of them to bury the counties that do.
                if (readCount != readCount_County || failed)
                {
                    Serilog.Modify.Log(
                        "OrtoDatas refresh - county {CountyId} {Outcome}: {ReadCount} references read, {EnqueuedCount} queued, {SubdivisionIdCount} subdivision identifiers written",
                        countyId, failed ? "incomplete" : (cancelled ? "stopped" : "done"), readCount - readCount_County, enqueuedCount - enqueuedCount_County, subdivisionIdCount - subdivisionIdCount_County);
                }

                if (cancelled)
                {
                    break;
                }
            }

            cancelled = cancelled || cancellationToken.IsCancellationRequested;

            Serilog.Modify.Log(
                cancelled || failedCountyCount != 0 ? Serilog.Enums.LogEventLevel.Warning : Serilog.Enums.LogEventLevel.Information,
                "{Method} finished{Cancelled}: {ReadCount} references read, {EnqueuedCount} queued, {SubdivisionIdCount} subdivision identifiers written, {FailedCountyCount} counties stepped over, over {CountyCount} counties",
                nameof(RefreshOrtoDatasAsync), cancelled ? " after being cancelled" : string.Empty, readCount, enqueuedCount, subdivisionIdCount, failedCountyCount, countyIds_Sorted.Count);

            return new PostgreSQLOrtoDatasRefreshResult(countyCount, readCount, enqueuedCount, subdivisionIdCount, failedCountyCount, cancelled);
        }
    }
}
