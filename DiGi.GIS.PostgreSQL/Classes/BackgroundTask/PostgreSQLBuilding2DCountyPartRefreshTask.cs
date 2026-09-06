// TODO [CountyPartAssignment]: this file is temporary and exists only for the county part repair of
// issue ZiolkowskiJakub/DiGi.GIS.PostgreSQL#68 - building_2d rows filed under a polygon part their
// footprint does not lie in, which the 2026-08-14 duplicate repair deliberately left alone because a
// building held by exactly one part was outside its remit. Delete it, together with its options, its
// result and the registration in DiGi.GIS.PostgreSQL.UI Create.VisualBackgroundTasks, once
// gis/building2d/countypartmismatches reports zero for every code and no importer bypasses
// Query.CountyId. Building2DPostgreSQLConverter.RefreshCountyIdsAsync, Modify.RefreshCountyIdsAsync
// and the mismatch endpoint are not temporary: they are how a county part is repaired at all.

using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Re-files every 2D building of a multi-part county under the polygon part its footprint lies in, and carries everything keyed on that building with it.
    /// <para>A county code names one <c>administrative_areal_2d</c> row per polygon part. Imports that resolved a code to a single part filed a whole county under it, and only geometry can say which part a building really belongs to - so a county can read back empty on the part holding its territory while every one of its buildings sits on a neighbouring exclave.</para>
    /// <para>Each building is decided with <see cref="Query.CountyId(System.Collections.Generic.IDictionary{int, Geometry.Planar.Interfaces.IPolygonal2D}, Geometry.Planar.Interfaces.IPolygonal2D, double)"/>, the same decision the import makes, and moved onto the part it belongs to. A building already sitting where it belongs is not touched, so a run over a healthy county does nothing.</para>
    /// <para><b>The rows keyed on a moved building move with it</b> - its 3D buildings, models, year built, occupancy, orthophotos and building data - unless <see cref="PostgreSQLBuilding2DCountyPartRefreshOptions.ReferencedObjects"/> says otherwise. Leaving them behind would make them unreachable, because every read of those tables filters on <c>county_id</c> first.</para>
    /// <para><b>Reports by default and writes nothing.</b> <see cref="PostgreSQLBuilding2DCountyPartRefreshOptions.DryRun"/> has to be turned off deliberately, and the report a dry run produces is what the move should be reviewed against.</para>
    /// <para>The report is written into <see cref="PostgreSQLBuilding2DCountyPartRefreshOptions.ReportDirectory"/> as well as to the log: one row per building in <c>Building2D_CountyPartRefresh.csv</c> and per-code totals in <c>Building2D_CountyPartRefresh_Summary.txt</c>. The row file is flushed per code, so a run interrupted late still leaves everything it had already decided.</para>
    /// <para>Nothing is deleted anywhere. A row the destination part will not take - it already holds that reference - stays where it is and is counted as blocked, for a person to settle.</para>
    /// </summary>
    public class PostgreSQLBuilding2DCountyPartRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// The converter manager the run draws its converters from.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The GIS PostgreSQL converter manager holding the converters the run reads and writes through.</param>
        public PostgreSQLBuilding2DCountyPartRefreshTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Gets the configuration for the run. These options are used when the task is started.
        /// </summary>
        public PostgreSQLBuilding2DCountyPartRefreshOptions PostgreSQLBuilding2DCountyPartRefreshOptions { get; set; } = new PostgreSQLBuilding2DCountyPartRefreshOptions();

        /// <summary>
        /// Gets what the last run read, decided and wrote.
        /// </summary>
        public PostgreSQLBuilding2DCountyPartRefreshResult? PostgreSQLBuilding2DCountyPartRefreshResult { get; private set; }

        /// <summary>
        /// Executes the background task, re-filing the buildings of every multi-part county under the polygon part they belong to.
        /// </summary>
        /// <param name="progress">A progress reporter carrying the running total of buildings decided.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true unless the run could not be attempted or was cancelled.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            PostgreSQLBuilding2DCountyPartRefreshResult = null;

            PostgreSQLBuilding2DCountyPartRefreshOptions postgreSQLBuilding2DCountyPartRefreshOptions = PostgreSQLBuilding2DCountyPartRefreshOptions ?? new PostgreSQLBuilding2DCountyPartRefreshOptions();

            bool dryRun = postgreSQLBuilding2DCountyPartRefreshOptions.DryRun;
            double tolerance = postgreSQLBuilding2DCountyPartRefreshOptions.Tolerance;
            int batchSize = postgreSQLBuilding2DCountyPartRefreshOptions.BatchSize;

            string directory = string.IsNullOrWhiteSpace(postgreSQLBuilding2DCountyPartRefreshOptions.ReportDirectory) ? AppContext.BaseDirectory : postgreSQLBuilding2DCountyPartRefreshOptions.ReportDirectory!;
            if (!Directory.Exists(directory))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "Report directory {Directory} does not exist", directory);
                return false;
            }

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager?.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager?.GetPostgreSQLConverter<Building2DPostgreSQLConverter>();

            if (administrativeAreal2DPostgreSQLConverter is null || building2DPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "PostgreSQL converters could not be resolved");
                return false;
            }

            List<AdministrativeAreal2D>? administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.County, cancellationToken: cancellationToken);
            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "County rows could not be retrieved");
                return false;
            }

            HashSet<string>? codes = postgreSQLBuilding2DCountyPartRefreshOptions.Codes is null ? null : [.. postgreSQLBuilding2DCountyPartRefreshOptions.Codes];

            Dictionary<string, List<AdministrativeAreal2D>> administrativeAreal2Ds_ByCode = [];
            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                string? code = administrativeAreal2D?.Code;
                if (string.IsNullOrWhiteSpace(code) || (codes is not null && !codes.Contains(code!)))
                {
                    continue;
                }

                if (!administrativeAreal2Ds_ByCode.TryGetValue(code!, out List<AdministrativeAreal2D>? administrativeAreal2Ds_Code) || administrativeAreal2Ds_Code is null)
                {
                    administrativeAreal2Ds_Code = [];
                    administrativeAreal2Ds_ByCode[code!] = administrativeAreal2Ds_Code;
                }

                administrativeAreal2Ds_Code.Add(administrativeAreal2D!);
            }

            LongProgressWrapper? longProgressWrapper = Core.Create.LongProgressWrapper(progress);

            Serilog.Modify.Log("{Type} started. DryRun: {DryRun}. Codes examined: {Count}. Referenced objects carried: {ReferencedObjects}", nameof(PostgreSQLBuilding2DCountyPartRefreshTask), dryRun, administrativeAreal2Ds_ByCode.Count, postgreSQLBuilding2DCountyPartRefreshOptions.ReferencedObjects);

            long codeCount = 0;
            long readCount = 0;
            long moveCount = 0;
            long movedCount = 0;
            long blockedCount = 0;
            long unresolvedCount = 0;
            long referencedObjectMovedCount = 0;
            bool cancelled = false;

            List<string> summaryLines =
            [
                "Building2D county part refresh",
                $"Started: {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                $"DryRun: {dryRun}",
                $"Referenced objects carried: {postgreSQLBuilding2DCountyPartRefreshOptions.ReferencedObjects}",
                string.Empty,
                "Code;Parts;Read;ToMove;Moved;Blocked;Unresolved"
            ];

            using StreamWriter streamWriter = new(System.IO.Path.Combine(directory, "Building2D_CountyPartRefresh.csv"), false, Encoding.UTF8);
            await streamWriter.WriteLineAsync("Code;Reference;Id;CountyId;CountyIdResolved;DecidedByGeometry");

            try
            {
                foreach (KeyValuePair<string, List<AdministrativeAreal2D>> keyValuePair in administrativeAreal2Ds_ByCode)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string code = keyValuePair.Key;

                    List<AdministrativeAreal2D> administrativeAreal2Ds_Code = keyValuePair.Value;
                    administrativeAreal2Ds_Code.Sort((x, y) => x.Id.CompareTo(y.Id));

                    // One part cannot hold a building under the wrong one of itself.
                    if (administrativeAreal2Ds_Code.Count < 2)
                    {
                        continue;
                    }

                    codeCount++;

                    List<int> countyIds_Code = administrativeAreal2Ds_Code.ConvertAll(x => x.Id);

                    long readCount_Code = 0;
                    foreach (int countyId in countyIds_Code)
                    {
                        long count = await building2DPostgreSQLConverter.CountAsync(countyId, cancellationToken: cancellationToken);
                        if (count > 0)
                        {
                            readCount_Code += count;
                        }
                    }

                    readCount += readCount_Code;

                    List<Building2DCountyPartMoveResult>? building2DCountyPartMoveResults = await building2DPostgreSQLConverter.GetCountyPartMovesAsync(administrativeAreal2Ds_Code, tolerance, batchSize, cancellationToken: cancellationToken);
                    if (building2DCountyPartMoveResults is null)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Code {Code}: the buildings could not be read, county stepped over", code);
                        continue;
                    }

                    Dictionary<int, List<string>> references_ByCountyId = [];
                    long unresolvedCount_Code = 0;

                    foreach (Building2DCountyPartMoveResult building2DCountyPartMoveResult in building2DCountyPartMoveResults)
                    {
                        await streamWriter.WriteLineAsync($"{code};{building2DCountyPartMoveResult.Reference};{building2DCountyPartMoveResult.Id};{building2DCountyPartMoveResult.CountyId};{building2DCountyPartMoveResult.CountyIdResolved};{building2DCountyPartMoveResult.DecidedByGeometry}");

                        if (building2DCountyPartMoveResult.CountyIdResolved <= 0 || string.IsNullOrWhiteSpace(building2DCountyPartMoveResult.Reference))
                        {
                            // No part could be decided for it. Left where it is, and reported rather than
                            // lost between nothing to do and moved.
                            unresolvedCount_Code++;
                            continue;
                        }

                        if (!references_ByCountyId.TryGetValue(building2DCountyPartMoveResult.CountyIdResolved, out List<string>? references) || references is null)
                        {
                            references = [];
                            references_ByCountyId[building2DCountyPartMoveResult.CountyIdResolved] = references;
                        }

                        references.Add(building2DCountyPartMoveResult.Reference!);
                    }

                    await streamWriter.FlushAsync(cancellationToken);

                    long moveCount_Code = 0;
                    foreach (KeyValuePair<int, List<string>> keyValuePair_Move in references_ByCountyId)
                    {
                        moveCount_Code += keyValuePair_Move.Value.Count;
                    }

                    moveCount += moveCount_Code;
                    unresolvedCount += unresolvedCount_Code;

                    Serilog.Modify.Log("Code {Code}: parts {Parts}, buildings {Read}, filed under the wrong part {Move}, undecidable {Unresolved}", code, string.Join(", ", countyIds_Code), readCount_Code, moveCount_Code, unresolvedCount_Code);

                    long movedCount_Code = 0;
                    long blockedCount_Code = 0;

                    if (!dryRun)
                    {
                        foreach (KeyValuePair<int, List<string>> keyValuePair_Move in references_ByCountyId)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            int countyId_Target = keyValuePair_Move.Key;
                            List<string> references_Move = keyValuePair_Move.Value;

                            // Naming the parts the rows can be sitting under is what prunes the move to this
                            // county instead of scanning every partition of the table.
                            List<int> countyIds_Source = countyIds_Code.FindAll(x => x != countyId_Target);

                            HashSet<string>? references_Moved = await building2DPostgreSQLConverter.RefreshCountyIdsAsync(references_Move, countyId_Target, countyIds_Source, cancellationToken: cancellationToken);
                            if (references_Moved is null)
                            {
                                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Code {Code}: the move onto part {CountyId} could not be attempted", code, countyId_Target);
                                continue;
                            }

                            movedCount_Code += references_Moved.Count;
                            blockedCount_Code += references_Move.Count - references_Moved.Count;

                            longProgressWrapper?.Increment(references_Moved.Count);

                            if (references_Moved.Count != references_Move.Count)
                            {
                                // The destination already held that reference. Deleting either copy is a
                                // decision for a person, so both are left and the reference is reported.
                                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Code {Code}: {Blocked} of {Requested} buildings could not move onto part {CountyId} - it already holds the reference", code, references_Move.Count - references_Moved.Count, references_Move.Count, countyId_Target);
                            }

                            if (postgreSQLBuilding2DCountyPartRefreshOptions.ReferencedObjects && references_Moved.Count != 0)
                            {
                                referencedObjectMovedCount += await RefreshReferencedObjectsAsync(gISPostgreSQLConverterManager, references_Moved, countyId_Target, countyIds_Source, cancellationToken);
                            }
                        }
                    }

                    movedCount += movedCount_Code;
                    blockedCount += blockedCount_Code;

                    if (dryRun)
                    {
                        longProgressWrapper?.Increment(moveCount_Code);
                    }

                    summaryLines.Add($"{code};{string.Join(" ", countyIds_Code)};{readCount_Code};{moveCount_Code};{movedCount_Code};{blockedCount_Code};{unresolvedCount_Code}");

                    foreach (KeyValuePair<int, List<string>> keyValuePair_Move in references_ByCountyId)
                    {
                        summaryLines.Add($"  part {keyValuePair_Move.Key}: {keyValuePair_Move.Value.Count} buildings {(dryRun ? "would be moved onto it" : "to move onto it")}");
                    }
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                cancelled = true;
            }

            summaryLines.Add(string.Empty);
            summaryLines.Add($"Codes examined: {codeCount}");
            summaryLines.Add($"Buildings read: {readCount}");
            summaryLines.Add($"Filed under the wrong part: {moveCount}");
            summaryLines.Add($"Moved: {movedCount}");
            summaryLines.Add($"Blocked by the destination: {blockedCount}");
            summaryLines.Add($"Undecidable: {unresolvedCount}");
            summaryLines.Add($"Referenced object references carried: {referencedObjectMovedCount}");
            summaryLines.Add($"Ended: {DateTime.Now:yyyy-MM-dd HH:mm:ss}{(cancelled ? " after being cancelled" : string.Empty)}");

            await streamWriter.FlushAsync(CancellationToken.None);

            await File.WriteAllLinesAsync(System.IO.Path.Combine(directory, "Building2D_CountyPartRefresh_Summary.txt"), summaryLines, CancellationToken.None);

            PostgreSQLBuilding2DCountyPartRefreshResult = new PostgreSQLBuilding2DCountyPartRefreshResult(codeCount, readCount, moveCount, movedCount, blockedCount, unresolvedCount, referencedObjectMovedCount, cancelled);

            Serilog.Modify.Log(
                cancelled ? Serilog.Enums.LogEventLevel.Warning : Serilog.Enums.LogEventLevel.Information,
                "{Type} ended{Cancelled}. DryRun: {DryRun}. Codes {Codes}, read {Read}, wrongly filed {Move}, moved {Moved}, blocked {Blocked}, undecidable {Unresolved}, referenced object references carried {Referenced}. Report written to {Directory}",
                nameof(PostgreSQLBuilding2DCountyPartRefreshTask), cancelled ? " after being cancelled" : string.Empty, dryRun, codeCount, readCount, moveCount, movedCount, blockedCount, unresolvedCount, referencedObjectMovedCount, directory);

            return !cancelled;
        }

        /// <summary>
        /// Carries every row keyed on the given buildings onto the county part those buildings have just been moved to.
        /// <para>A converter the manager does not hold is skipped rather than failing the run: an installation storing no orthophotos has nothing to carry, and that is not an error.</para>
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The manager holding the converters to move rows through.</param>
        /// <param name="references">The references of the buildings that have moved.</param>
        /// <param name="countyId">The county polygon part they have moved onto.</param>
        /// <param name="countyIds_Source">The parts their rows may still be sitting under.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the number of references that had at least one row carried, summed over the tables.</returns>
        private static async Task<long> RefreshReferencedObjectsAsync(GISPostgreSQLConverterManager? gISPostgreSQLConverterManager, IEnumerable<string> references, int countyId, IEnumerable<int> countyIds_Source, CancellationToken cancellationToken)
        {
            long result = 0;

            if (gISPostgreSQLConverterManager is null)
            {
                return result;
            }

            // The tables keyed on (county_id, reference). They carry no unique_id, so they are moved through
            // the statement in Modify.RefreshCountyIdsAsync, which is told the parts to look in.
            BuildingPostgreSQLConverter? buildingPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingPostgreSQLConverter>();
            if (buildingPostgreSQLConverter is not null)
            {
                HashSet<string>? references_Moved = await buildingPostgreSQLConverter.RefreshCountyIdsAsync(references, countyId, countyIds_Source, cancellationToken: cancellationToken);
                result += Count(references_Moved, Constants.TableName.Building, countyId);
            }

            BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingDataPostgreSQLConverter>();
            if (buildingDataPostgreSQLConverter is not null)
            {
                HashSet<string>? references_Moved = await buildingDataPostgreSQLConverter.RefreshCountyIdsAsync(references, countyId, countyIds_Source, cancellationToken: cancellationToken);
                result += Count(references_Moved, Constants.TableName.BuildingData, countyId);
            }

            OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<OrtoDatasPostgreSQLConverter>();
            if (ortoDatasPostgreSQLConverter is not null)
            {
                HashSet<string>? references_Moved = await ortoDatasPostgreSQLConverter.RefreshCountyIdsAsync(references, countyId, countyIds_Source, cancellationToken: cancellationToken);
                result += Count(references_Moved, Constants.TableName.OrtoDatas, countyId);
            }

            // The tables keyed on (county_id, unique_id). Their own converter already knows how to move a
            // row between partitions; it searches every partition rather than the parts named here, which is
            // the cost of an interface that predates this run.
            BuildingModelPostgreSQLConverter? buildingModelPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingModelPostgreSQLConverter>();
            if (buildingModelPostgreSQLConverter is not null)
            {
                HashSet<string>? references_Moved = await buildingModelPostgreSQLConverter.RefreshCountyIdsAsync(references, countyId, cancellationToken: cancellationToken);
                result += Count(references_Moved, Constants.TableName.BuildingModel, countyId);
            }

            YearBuiltDataPostgreSQLConverter? yearBuiltDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<YearBuiltDataPostgreSQLConverter>();
            if (yearBuiltDataPostgreSQLConverter is not null)
            {
                HashSet<string>? references_Moved = await yearBuiltDataPostgreSQLConverter.RefreshCountyIdsAsync(references, countyId, cancellationToken: cancellationToken);
                result += Count(references_Moved, Constants.TableName.YearBuiltData, countyId);
            }

            Building2DOccupancyDataPostgreSQLConverter? building2DOccupancyDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DOccupancyDataPostgreSQLConverter>();
            if (building2DOccupancyDataPostgreSQLConverter is not null)
            {
                HashSet<string>? references_Moved = await building2DOccupancyDataPostgreSQLConverter.RefreshCountyIdsAsync(references, countyId, cancellationToken: cancellationToken);
                result += Count(references_Moved, Constants.TableName.OccupancyData_Building2D, countyId);
            }

            return result;

            static long Count(HashSet<string>? references_Moved, string tableName, int countyId)
            {
                if (references_Moved is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{TableName}: the move onto part {CountyId} could not be attempted", tableName, countyId);
                    return 0;
                }

                if (references_Moved.Count != 0)
                {
                    Serilog.Modify.Log("{TableName}: {Count} references carried onto part {CountyId}", tableName, references_Moved.Count, countyId);
                }

                return references_Moved.Count;
            }
        }
    }
}
