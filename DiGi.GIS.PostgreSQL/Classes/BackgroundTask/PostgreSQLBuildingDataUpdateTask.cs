using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.PostgreSQL.Constants;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that fills the building data table from Building2D and the other stored data sources.
    /// <para>The run is driven by subdivisions: for each one it reads that subdivision's buildings and, according to <see cref="PostgreSQLBuildingDataUpdateOptions.BuildingDataUpdateTypes"/>, derives the shape and administrative columns, the occupancy, the database identifier and the radial ratios, then upserts a row per building keyed on county and reference.</para>
    /// <para>Buildings the subdivision loop cannot reach - those without a <c>subdivision_id</c>, and those whose subdivision belongs to a neighbouring county - are updated in a final per-county pass, deriving their shape, occupancy, database identifier, radial ratios and predicted year built. The population columns are written per subdivision group by resolving the group's own subdivision through <c>administrative_areal_2d</c>; buildings with no subdivision, or whose subdivision matches no statistical unit or carries no population series, have their population columns left unwritten and are logged rather than filled with zeros.</para>
    /// <para>A subdivision that fails is logged and stepped over rather than ending the run, so <see cref="BackgroundTask.IsSucceeded"/> alone does not say a run did everything it set out to do. <see cref="FailedSubdivisionCount"/> and <see cref="SkippedSubdivisionCount"/> are what tell those apart. A selected update type whose prerequisite is missing writes nothing at all while the rest of the run carries on; <see cref="UnfulfilledUpdateTypeCount"/> counts those, and the run is reported as not succeeded while it is above zero.</para>
    /// </summary>
    public class PostgreSQLBuildingDataUpdateTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// The GIS PostgreSQL converter manager used to retrieve converters and execute operations.
        /// </summary>
        protected readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Gets or sets the options used to configure the PostgreSQL building data update process.
        /// </summary>
        public PostgreSQLBuildingDataUpdateOptions PostgreSQLBuildingDataUpdateOptions { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataUpdateTask"/> class.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The GIS PostgreSQL converter manager used to retrieve converters and execute operations.</param>
        public PostgreSQLBuildingDataUpdateTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Gets the number of subdivisions that failed outright and were stepped over during the last run.
        /// <para>Each one is logged with the exception that caused it, so this figure is a count of entries to go and read rather than the whole of what is known.</para>
        /// </summary>
        public long FailedSubdivisionCount { get; private set; }

        /// <summary>
        /// Gets the number of subdivisions that were read and written during the last run.
        /// </summary>
        public long ProcessedSubdivisionCount { get; private set; }

        /// <summary>
        /// Gets the number of subdivisions that were stepped over during the last run without being attempted, because the stored record does not say which county they belong to.
        /// <para>Unlike <see cref="FailedSubdivisionCount"/> this is a defect in the administrative data rather than in the run, and it does not clear itself: the same subdivisions are stepped over again next time until their parent chain is repaired.</para>
        /// </summary>
        public long SkippedSubdivisionCount { get; private set; }

        /// <summary>
        /// Gets the number of buildings without a subdivision (<c>subdivision_id IS NULL</c>) that were processed during the last run.
        /// </summary>
        public long UnassignedSubdivisionBuildingCount { get; private set; }

        /// <summary>
        /// Gets the number of buildings whose subdivision belongs to a neighbouring county that were processed during the last run.
        /// <para>Unlike <see cref="UnassignedSubdivisionBuildingCount"/> these buildings do name a subdivision; that subdivision simply sits under another county, so the subdivision loop cannot reach them.</para>
        /// </summary>
        public long CrossCountySubdivisionBuildingCount { get; private set; }

        /// <summary>
        /// Gets the number of building data rows written during the last run.
        /// <para>Rows rather than buildings: a building reached under more than one update type is written once, but the same building is counted again on a later run.</para>
        /// </summary>
        public long UpdatedRowCount { get; private set; }

        /// <summary>
        /// Gets the number of selected update types whose prerequisite was missing during the last run, so the type wrote nothing at all.
        /// <para>For instance <see cref="BuildingDataUpdateType.Statistical"/> when no statistical unit hierarchy could be loaded: both passes then skip the population columns instead of writing them. A warning-level gap that still leaves a type writing something - a missing occupancy converter, say, which only drops the stored occupancy while the Building2D-derived columns are still written - is not counted. Counted once per run rather than once per subdivision, and unlike <see cref="SkippedSubdivisionCount"/> it does make the run incomplete - a run that returned a selected update type unwritten is not a run that succeeded.</para>
        /// </summary>
        public long UnfulfilledUpdateTypeCount { get; private set; }

        /// <summary>
        /// Executes the background task to update building data from AdministrativeAreal2D and Building2D sources.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of processed items.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true when the run could be attempted, every subdivision in scope was updated without error and every selected update type was written; otherwise, false - including when a selected update type was counted against <see cref="UnfulfilledUpdateTypeCount"/>.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            FailedSubdivisionCount = 0;
            ProcessedSubdivisionCount = 0;
            SkippedSubdivisionCount = 0;
            UnassignedSubdivisionBuildingCount = 0;
            CrossCountySubdivisionBuildingCount = 0;
            UpdatedRowCount = 0;
            UnfulfilledUpdateTypeCount = 0;

            PostgreSQLBuildingDataUpdateOptions ??= new();

            if (PostgreSQLBuildingDataUpdateOptions.BuildingDataUpdateTypes is not HashSet<BuildingDataUpdateType> buildingDataUpdateTypes || buildingDataUpdateTypes.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Type}: no update type was named - there is nothing to write", nameof(PostgreSQLBuildingDataUpdateTask));
                return false;
            }

            bool update_General = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.General);
            bool update_Database = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Database);
            bool update_Occupancy = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Occupancy);
            bool update_RadialRatios = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.RadialRatios);
            bool update_Statistical = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Statistical);
            bool update_PredictedYearBuilt = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.PredictedYearBuilt);

            // The GIS conversion carries the outline of every building, so it is done only for the update types
            // that actually measure geometry or need domain models. A Database-only run reads identifiers and never touches an outline.
            bool convert_Building2Ds = update_General || update_Occupancy || update_RadialRatios || update_Statistical;

            int commandTimeout = PostgreSQLBuildingDataUpdateOptions.CommandTimeout;

            BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingDataPostgreSQLConverter>();
            if (buildingDataPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - there is nowhere to write to", nameof(PostgreSQLBuildingDataUpdateTask), nameof(BuildingDataPostgreSQLConverter));
                return false;
            }

            Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the buildings cannot be read", nameof(PostgreSQLBuildingDataUpdateTask), nameof(Building2DPostgreSQLConverter));
                return false;
            }

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the subdivisions that drive the run cannot be read", nameof(PostgreSQLBuildingDataUpdateTask), nameof(AdministrativeAreal2DPostgreSQLConverter));
                return false;
            }

            Building2DOccupancyDataPostgreSQLConverter? building2DOccupancyDataPostgreSQLConverter = null;
            if (update_Occupancy)
            {
                building2DOccupancyDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DOccupancyDataPostgreSQLConverter>();
                if (building2DOccupancyDataPostgreSQLConverter is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Type}: no {Converter} - the stored occupancy is not read and only what Building2D itself says about occupancy is written", nameof(PostgreSQLBuildingDataUpdateTask), nameof(Building2DOccupancyDataPostgreSQLConverter));
                }
            }

            YearBuiltDataPostgreSQLConverter? yearBuiltDataPostgreSQLConverter = null;
            if (update_PredictedYearBuilt)
            {
                yearBuiltDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<YearBuiltDataPostgreSQLConverter>();
                if (yearBuiltDataPostgreSQLConverter is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Type}: no {Converter} - the stored predicted year built data is not read", nameof(PostgreSQLBuildingDataUpdateTask), nameof(YearBuiltDataPostgreSQLConverter));
                }
            }

            UnitPostgreSQLConverter? unitPostgreSQLConverter = null;
            StatisticalDataCollectionPostgreSQLConverter? statisticalDataCollectionPostgreSQLConverter = null;
            StatisticalUnit? rootStatisticalUnit = null;
            if (update_Statistical)
            {
                unitPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<UnitPostgreSQLConverter>();
                if (unitPostgreSQLConverter is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the territorial units cannot be read", nameof(PostgreSQLBuildingDataUpdateTask), nameof(UnitPostgreSQLConverter));
                    return false;
                }

                statisticalDataCollectionPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<StatisticalDataCollectionPostgreSQLConverter>();
                if (statisticalDataCollectionPostgreSQLConverter is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the statistical data collections cannot be read", nameof(PostgreSQLBuildingDataUpdateTask), nameof(StatisticalDataCollectionPostgreSQLConverter));
                    return false;
                }

                rootStatisticalUnit = await unitPostgreSQLConverter.GetStatisticalUnitAsync(commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                if (rootStatisticalUnit is null)
                {
                    // The hierarchy is what every population write resolves through, so without it the statistical
                    // updates would be dropped in silence and the run would still report success. The other update
                    // types keep running; the missing one is counted and the result says the run was incomplete.
                    UnfulfilledUpdateTypeCount++;
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no statistical unit hierarchy could be loaded - the statistical updates write nothing; populate the {UnitTable} and {StatisticalDataTable} tables and run again", nameof(PostgreSQLBuildingDataUpdateTask), TableName.Unit, TableName.StatisticalDataCollection);
                }
            }

            List<double> radiuses = [.. (PostgreSQLBuildingDataUpdateOptions.Radiuses ?? []).Where(x => !double.IsNaN(x) && !double.IsInfinity(x) && x > 0)];
            if (update_RadialRatios && radiuses.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: the radial ratios were asked for but no usable radius was named", nameof(PostgreSQLBuildingDataUpdateTask));
                return false;
            }

            // The neighbour search has to reach the largest radius, and Update_RadialRatios works the radiuses
            // largest-first off the one set it is given.
            double radius_Max = radiuses.Count == 0 ? 0 : radiuses.Max();

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivision, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no subdivision could be read - the run cannot be scoped", nameof(PostgreSQLBuildingDataUpdateTask));
                return false;
            }

            List<AdministrativeAreal2DReference>? countyReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.County, commandTimeout: commandTimeout, cancellationToken: cancellationToken);

            Dictionary<int, HashSet<int>> siblingCountyGroups = countyReferences.SiblingCountyGroups();

            HashSet<int>? countyIds = PostgreSQLBuildingDataUpdateOptions.CountyIds;
            HashSet<int> processedCountyIds = [];
            Dictionary<string, StatisticalDataCollection?> cachedStatisticalDataCollections = [];
            Dictionary<int, HashSet<int>> inScopeSubdivisionIds_ByCountyId = Query.InScopeSubdivisionIds(administrativeAreal2DReferences, siblingCountyGroups);

            Serilog.Modify.Log(
                "{Type}: starting over {SubdivisionCount} subdivisions, counties {CountyScope}, update types {UpdateTypes}",
                nameof(PostgreSQLBuildingDataUpdateTask),
                administrativeAreal2DReferences.Count,
                countyIds is null ? "all" : string.Join(", ", countyIds),
                string.Join(", ", buildingDataUpdateTypes));

            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (administrativeAreal2DReference.CountyId is not int subdivisionCountyId)
                {
                    SkippedSubdivisionCount++;
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Building data subdivision skipped - subdivision {SubdivisionId} names no parent county, so its buildings cannot be addressed", administrativeAreal2DReference.Id);
                    continue;
                }

                if (!siblingCountyGroups.TryGetValue(subdivisionCountyId, out HashSet<int>? siblingCountyIds) || siblingCountyIds is null || siblingCountyIds.Count == 0)
                {
                    siblingCountyIds = [subdivisionCountyId];
                }

                if (countyIds is not null && !siblingCountyIds.Overlaps(countyIds))
                {
                    continue;
                }

                HashSet<int> candidateCountyIds = countyIds is null ? siblingCountyIds : [.. siblingCountyIds.Where(countyIds.Contains)];

                // The references were queried by AdministrativeArealType.Subdivision, so Id is the subdivision id.
                int subdivisionId = administrativeAreal2DReference.Id;

                AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath = null;
                GIS.Classes.AdministrativeSubdivision? administrativeSubdivision = null;

                if (update_General || update_Statistical)
                {
                    try
                    {
                        administrativeAreal2DReferencePath = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(administrativeAreal2DReference, commandTimeout, cancellationToken);

                        if (update_General)
                        {
                            // The subdivision is the one member of the chain still read whole, because its occupancy and
                            // its settlement type are not on a reference.
                            List<AdministrativeAreal2D>? administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync([subdivisionId], commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                            administrativeSubdivision = administrativeAreal2Ds?.Select(x => x.ToDiGi()).OfType<GIS.Classes.AdministrativeSubdivision>().FirstOrDefault();
                        }
                    }
                    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        FailedSubdivisionCount++;
                        Serilog.Modify.Log(exception, "Building data subdivision administrative metadata failed - subdivision {SubdivisionId}", subdivisionId);
                        continue;
                    }
                }

                StatisticalDataCollection? statisticalDataCollection = null;
                if (update_Statistical && rootStatisticalUnit is not null)
                {
                    StatisticalUnit? statisticalUnit = Query.Match(rootStatisticalUnit, administrativeAreal2DReference, administrativeAreal2DReferencePath);

                    if (statisticalUnit?.Code is string statisticalUnitCode && !string.IsNullOrWhiteSpace(statisticalUnitCode))
                    {
                        if (!cachedStatisticalDataCollections.TryGetValue(statisticalUnitCode, out statisticalDataCollection))
                        {
                            try
                            {
                                statisticalDataCollection = await statisticalDataCollectionPostgreSQLConverter!.GetStatisticalDataCollectionByIdAsync(statisticalUnitCode, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                                cachedStatisticalDataCollections[statisticalUnitCode] = statisticalDataCollection;
                            }
                            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                            {
                                throw;
                            }
                            catch (Exception exception)
                            {
                                Serilog.Modify.Log(exception, "Building data statistical data collection retrieval failed - unit code {Code}", statisticalUnitCode);
                            }
                        }
                    }
                }

                bool subdivisionHasBuildings = false;

                foreach (int targetCountyId in candidateCountyIds)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<Building2D>? building2Ds;

                    try
                    {
                        building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByCountyIdAsync(targetCountyId, subdivisionId, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    }
                    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        FailedSubdivisionCount++;
                        Serilog.Modify.Log(exception, "Building data subdivision failed - county {CountyId}, subdivision {SubdivisionId}, the buildings could not be read", targetCountyId, subdivisionId);
                        continue;
                    }

                    if (building2Ds is null || building2Ds.Count == 0)
                    {
                        continue;
                    }

                    subdivisionHasBuildings = true;
                    processedCountyIds.Add(targetCountyId);

                    List<GIS.Classes.Building2D> building2Ds_GIS = convert_Building2Ds ? [.. building2Ds.Select(x => x.ToDiGi()).OfType<GIS.Classes.Building2D>()] : [];

                    Table table = new();

                    try
                    {
                        if (update_General)
                        {
                            // The names come off the path references rather than off the boundary objects: the objects
                            // carry the outlines as well, and at the top of the chain that outline is the whole country.
                            string? countyName = administrativeAreal2DReferencePath?[AdministrativeArealType.County]?.Name;
                            string? municipalityName = administrativeAreal2DReferencePath?[AdministrativeArealType.Municipality]?.Name;
                            string? voivodeshipName = administrativeAreal2DReferencePath?[AdministrativeArealType.Voivodeship]?.Name;

                            IO.Modify.Update_Building2D(table, targetCountyId, building2Ds_GIS);
                            IO.Modify.Update_Building2D(table, targetCountyId, subdivisionId, building2Ds_GIS, countyName, municipalityName, voivodeshipName, administrativeSubdivision);
                        }

                        if (update_Occupancy)
                        {
                            // Update_Building2D has already written these two off Building2D itself when the general
                            // columns were asked for, so repeating that pass would recompute the same two values.
                            if (!update_General)
                            {
                                IO.Modify.Update_Building2D_Occupancy(table, targetCountyId, building2Ds_GIS);
                            }

                            if (building2DOccupancyDataPostgreSQLConverter is not null)
                            {
                                List<string> references = [.. building2Ds.Where(x => !string.IsNullOrWhiteSpace(x?.Reference)).Select(x => x.Reference!)];

                                // No fallback by reference: a reference the county does not hold is answered out of some
                                // other county, and the record that comes back carries that county's identifier. Writing
                                // it would file a building data row under a county this run is not processing.
                                List<Building2DOccupancyData>? building2DOccupancyDatas = await building2DOccupancyDataPostgreSQLConverter.GetItemsByReferencesAsync(references, targetCountyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                                if (building2DOccupancyDatas is not null)
                                {
                                    Modify.Update_Occupancy(table, building2DOccupancyDatas);
                                }
                            }
                        }

                        if (update_Database)
                        {
                            Modify.Update_Id(table, [.. building2Ds.Select(x => new Building2DReference(x))]);
                        }

                        if (update_RadialRatios)
                        {
                            // One neighbour read for the whole subdivision rather than one per building: the area the
                            // subdivision covers, grown by the largest radius, is exactly what every building in it can
                            // reach. Update_RadialRatios then indexes that set once and measures every building against it.
                            List<BoundingBox2D> boundingBox2Ds = [.. building2Ds_GIS.Select(x => x.PolygonalFace2D?.GetBoundingBox()).OfType<BoundingBox2D>()];

                            // An empty BoundingBox2D reads back as a point at the origin rather than as nothing, so
                            // measuring against one would quietly gather the surroundings of the wrong place.
                            if (boundingBox2Ds.Count == 0)
                            {
                                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Building data radial ratios not measured - county {CountyId}, subdivision {SubdivisionId}, none of its {BuildingCount} buildings carries an outline", targetCountyId, subdivisionId, building2Ds.Count);
                            }
                            else
                            {
                                BoundingBox2D boundingBox2D = new(boundingBox2Ds);
                                boundingBox2D.Offset(radius_Max);

                                List<Building2D>? building2Ds_Neighbour = await building2DPostgreSQLConverter.GetBuilding2DsByBoundingBox2DAsync(boundingBox2D, commandTimeout: commandTimeout, cancellationToken: cancellationToken);

                                // Distinct from an empty answer: the read did not happen, so the ratios would be measured
                                // against surroundings that are missing rather than absent.
                                if (building2Ds_Neighbour is null)
                                {
                                    throw new InvalidOperationException($"The buildings surrounding subdivision {subdivisionId} of county {targetCountyId} could not be read.");
                                }

                                List<GIS.Classes.Building2D> building2Ds_Neighbour_GIS = [.. building2Ds_Neighbour.Select(x => x.ToDiGi()).OfType<GIS.Classes.Building2D>()];

                                IO.Modify.Update_RadialRatios(table, radiuses, targetCountyId, building2Ds_GIS, building2Ds_Neighbour_GIS);
                            }
                        }

                        if (update_Statistical && statisticalDataCollection is not null)
                        {
                            StatisticalYearlyDoubleData? statisticalYearlyDoubleData = Query.Population(statisticalDataCollection);
                            if (statisticalYearlyDoubleData is not null)
                            {
                                IO.Modify.Update_Building2D_Population(table, targetCountyId, building2Ds_GIS, statisticalYearlyDoubleData, PostgreSQLBuildingDataUpdateOptions.Years);
                            }
                        }

                        if (update_PredictedYearBuilt && yearBuiltDataPostgreSQLConverter is not null)
                        {
                            List<string> references = [.. building2Ds.Where(x => !string.IsNullOrWhiteSpace(x?.Reference)).Select(x => x.Reference!)];
                            List<YearBuiltData>? yearBuiltDatas = await yearBuiltDataPostgreSQLConverter.GetItemsByReferencesAsync(references, targetCountyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                            if (yearBuiltDatas is not null)
                            {
                                List<GIS.Classes.YearBuiltData> yearBuiltDatas_GIS = [.. yearBuiltDatas.Select(x => x.ToDiGi()).OfType<GIS.Classes.YearBuiltData>()];
                                IO.Modify.Update_Building2D_PredictedYearBuilt(table, targetCountyId, yearBuiltDatas_GIS);
                            }
                        }
                    }
                    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        FailedSubdivisionCount++;
                        Serilog.Modify.Log(exception, "Building data subdivision failed - county {CountyId}, subdivision {SubdivisionId}, the {BuildingCount} buildings read could not be turned into rows", targetCountyId, subdivisionId, building2Ds.Count);
                        continue;
                    }

                    if (table.RowCount == 0)
                    {
                        continue;
                    }

                    bool updated;
                    try
                    {
                        updated = await buildingDataPostgreSQLConverter.PushAsync(table, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    }
                    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        FailedSubdivisionCount++;
                        Serilog.Modify.Log(exception, "Building data subdivision failed - county {CountyId}, subdivision {SubdivisionId}, the {RowCount} rows built could not be written", targetCountyId, subdivisionId, table.RowCount);
                        continue;
                    }

                    if (!updated)
                    {
                        FailedSubdivisionCount++;
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Building data subdivision failed - county {CountyId}, subdivision {SubdivisionId}, the write of {RowCount} rows was rolled back", targetCountyId, subdivisionId, table.RowCount);
                        continue;
                    }

                    UpdatedRowCount += table.RowCount;
                    progress.Report(UpdatedRowCount);
                }

                if (subdivisionHasBuildings)
                {
                    ProcessedSubdivisionCount++;
                }
            }

            IEnumerable<int> unassignedCountyScope = countyIds ?? (countyReferences is not null ? [.. countyReferences.Select(x => x.Id)] : processedCountyIds);
            foreach (int countyId in unassignedCountyScope)
            {
                cancellationToken.ThrowIfCancellationRequested();

                List<Building2D>? building2Ds_Unassigned;
                try
                {
                    inScopeSubdivisionIds_ByCountyId.TryGetValue(countyId, out HashSet<int>? inScopeSubdivisionIds);
                    building2Ds_Unassigned = await building2DPostgreSQLConverter.GetBuilding2DsUnreachedByCountyAsync(countyId, inScopeSubdivisionIds, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    FailedSubdivisionCount++;
                    Serilog.Modify.Log(exception, "Building data unassigned buildings query failed - county {CountyId}", countyId);
                    continue;
                }

                if (building2Ds_Unassigned is null || building2Ds_Unassigned.Count == 0)
                {
                    continue;
                }

                List<GIS.Classes.Building2D> building2Ds_Unassigned_GIS = convert_Building2Ds ? [.. building2Ds_Unassigned.Select(x => x.ToDiGi()).OfType<GIS.Classes.Building2D>()] : [];

                Table table = new();

                try
                {
                    if (update_General)
                    {
                        IO.Modify.Update_Building2D(table, countyId, building2Ds_Unassigned_GIS);
                    }

                    if (update_Occupancy)
                    {
                        if (!update_General)
                        {
                            IO.Modify.Update_Building2D_Occupancy(table, countyId, building2Ds_Unassigned_GIS);
                        }

                        if (building2DOccupancyDataPostgreSQLConverter is not null)
                        {
                            List<string> references = [.. building2Ds_Unassigned.Where(x => !string.IsNullOrWhiteSpace(x?.Reference)).Select(x => x.Reference!)];
                            List<Building2DOccupancyData>? building2DOccupancyDatas = await building2DOccupancyDataPostgreSQLConverter.GetItemsByReferencesAsync(references, countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                            if (building2DOccupancyDatas is not null)
                            {
                                Modify.Update_Occupancy(table, building2DOccupancyDatas);
                            }
                        }
                    }

                    if (update_Database)
                    {
                        Modify.Update_Id(table, [.. building2Ds_Unassigned.Select(x => new Building2DReference(x))]);
                    }

                    if (update_RadialRatios)
                    {
                        List<BoundingBox2D> boundingBox2Ds = [.. building2Ds_Unassigned_GIS.Select(x => x.PolygonalFace2D?.GetBoundingBox()).OfType<BoundingBox2D>()];

                        if (boundingBox2Ds.Count == 0)
                        {
                            Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Building data radial ratios not measured for unassigned buildings - county {CountyId}, none of its {BuildingCount} unassigned buildings carries an outline", countyId, building2Ds_Unassigned.Count);
                        }
                        else
                        {
                            BoundingBox2D boundingBox2D = new(boundingBox2Ds);
                            boundingBox2D.Offset(radius_Max);

                            List<Building2D>? building2Ds_Neighbour = await building2DPostgreSQLConverter.GetBuilding2DsByBoundingBox2DAsync(boundingBox2D, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                            if (building2Ds_Neighbour is null)
                            {
                                throw new InvalidOperationException($"The buildings surrounding unassigned buildings of county {countyId} could not be read.");
                            }

                            List<GIS.Classes.Building2D> building2Ds_Neighbour_GIS = [.. building2Ds_Neighbour.Select(x => x.ToDiGi()).OfType<GIS.Classes.Building2D>()];

                            IO.Modify.Update_RadialRatios(table, radiuses, countyId, building2Ds_Unassigned_GIS, building2Ds_Neighbour_GIS);
                        }
                    }

                    if (update_Statistical && rootStatisticalUnit is not null)
                    {
                        // These buildings either name no subdivision at all or name one the subdivision loop could
                        // not reach (a cross-county one), so no loop variable carries their statistical unit: each
                        // group is resolved through the building's own subdivision, exactly as the loop would have.
                        Dictionary<int, List<Building2D>> building2Ds_BySubdivisionId = [];
                        List<Building2D> building2Ds_WithoutSubdivision = [];

                        foreach (Building2D building2D_Temp in building2Ds_Unassigned)
                        {
                            if (building2D_Temp.SubdivisionId is int subdivisionId_Temp)
                            {
                                if (!building2Ds_BySubdivisionId.TryGetValue(subdivisionId_Temp, out List<Building2D>? building2Ds_Group))
                                {
                                    building2Ds_Group = [];
                                    building2Ds_BySubdivisionId[subdivisionId_Temp] = building2Ds_Group;
                                }

                                building2Ds_Group.Add(building2D_Temp);
                            }
                            else
                            {
                                building2Ds_WithoutSubdivision.Add(building2D_Temp);
                            }
                        }

                        int buildingCount_NoPopulation = building2Ds_WithoutSubdivision.Count;

                        if (building2Ds_BySubdivisionId.Count > 0)
                        {
                            List<int> subdivisionIds_CrossCounty = [.. building2Ds_BySubdivisionId.Keys];

                            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Subdivision = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(subdivisionIds_CrossCounty, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                            List<AdministrativeAreal2DReferencePath>? administrativeAreal2DReferencePaths = administrativeAreal2DReferences_Subdivision is null || administrativeAreal2DReferences_Subdivision.Count == 0 ? null : await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsAsync(administrativeAreal2DReferences_Subdivision, commandTimeout: commandTimeout, cancellationToken: cancellationToken);

                            Dictionary<int, AdministrativeAreal2DReference> administrativeAreal2DReferences_BySubdivisionId = [];
                            if (administrativeAreal2DReferences_Subdivision is not null)
                            {
                                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Subdivision in administrativeAreal2DReferences_Subdivision)
                                {
                                    administrativeAreal2DReferences_BySubdivisionId[administrativeAreal2DReference_Subdivision.Id] = administrativeAreal2DReference_Subdivision;
                                }
                            }

                            Dictionary<int, AdministrativeAreal2DReferencePath> administrativeAreal2DReferencePaths_BySubdivisionId = [];
                            if (administrativeAreal2DReferencePaths is not null)
                            {
                                foreach (AdministrativeAreal2DReferencePath administrativeAreal2DReferencePath_Temp in administrativeAreal2DReferencePaths)
                                {
                                    List<AdministrativeAreal2DReference> administrativeAreal2DReferences_Path = administrativeAreal2DReferencePath_Temp.AdministrativeAreal2DReferences;
                                    if (administrativeAreal2DReferences_Path.Count > 0)
                                    {
                                        administrativeAreal2DReferencePaths_BySubdivisionId[administrativeAreal2DReferences_Path[^1].Id] = administrativeAreal2DReferencePath_Temp;
                                    }
                                }
                            }

                            foreach (KeyValuePair<int, List<Building2D>> building2Ds_Group_Entry in building2Ds_BySubdivisionId)
                            {
                                List<Building2D> building2Ds_Group = building2Ds_Group_Entry.Value;

                                if (!administrativeAreal2DReferences_BySubdivisionId.TryGetValue(building2Ds_Group_Entry.Key, out AdministrativeAreal2DReference? administrativeAreal2DReference_Subdivision))
                                {
                                    buildingCount_NoPopulation += building2Ds_Group.Count;
                                    continue;
                                }

                                administrativeAreal2DReferencePaths_BySubdivisionId.TryGetValue(building2Ds_Group_Entry.Key, out AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath_Group);

                                StatisticalUnit? statisticalUnit = Query.Match(rootStatisticalUnit, administrativeAreal2DReference_Subdivision, administrativeAreal2DReferencePath_Group);
                                if (statisticalUnit?.Code is not string statisticalUnitCode || string.IsNullOrWhiteSpace(statisticalUnitCode))
                                {
                                    buildingCount_NoPopulation += building2Ds_Group.Count;
                                    continue;
                                }

                                if (!cachedStatisticalDataCollections.TryGetValue(statisticalUnitCode, out StatisticalDataCollection? statisticalDataCollection))
                                {
                                    try
                                    {
                                        statisticalDataCollection = await statisticalDataCollectionPostgreSQLConverter!.GetStatisticalDataCollectionByIdAsync(statisticalUnitCode, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                                        cachedStatisticalDataCollections[statisticalUnitCode] = statisticalDataCollection;
                                    }
                                    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                                    {
                                        throw;
                                    }
                                    catch (Exception exception)
                                    {
                                        Serilog.Modify.Log(exception, "Building data statistical data collection retrieval failed - unit code {Code}", statisticalUnitCode);
                                    }
                                }

                                StatisticalYearlyDoubleData? statisticalYearlyDoubleData = Query.Population(statisticalDataCollection);
                                if (statisticalYearlyDoubleData is null)
                                {
                                    buildingCount_NoPopulation += building2Ds_Group.Count;
                                    continue;
                                }

                                HashSet<string> references_Group = [.. building2Ds_Group.Where(x => !string.IsNullOrWhiteSpace(x?.Reference)).Select(x => x.Reference!)];
                                List<GIS.Classes.Building2D> building2Ds_Group_GIS = [.. building2Ds_Unassigned_GIS.Where(x => x.Reference is string reference_Group && references_Group.Contains(reference_Group))];

                                IO.Modify.Update_Building2D_Population(table, countyId, building2Ds_Group_GIS, statisticalYearlyDoubleData, PostgreSQLBuildingDataUpdateOptions.Years);
                            }
                        }

                        if (buildingCount_NoPopulation > 0)
                        {
                            Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Building data population columns left unwritten - county {CountyId}, {BuildingCount} unassigned buildings have no subdivision, no resolvable statistical unit or no population series", countyId, buildingCount_NoPopulation);
                        }
                    }

                    if (update_PredictedYearBuilt && yearBuiltDataPostgreSQLConverter is not null)
                    {
                        List<string> references = [.. building2Ds_Unassigned.Where(x => !string.IsNullOrWhiteSpace(x?.Reference)).Select(x => x.Reference!)];
                        List<YearBuiltData>? yearBuiltDatas = await yearBuiltDataPostgreSQLConverter.GetItemsByReferencesAsync(references, countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                        if (yearBuiltDatas is not null)
                        {
                            List<GIS.Classes.YearBuiltData> yearBuiltDatas_GIS = [.. yearBuiltDatas.Select(x => x.ToDiGi()).OfType<GIS.Classes.YearBuiltData>()];
                            IO.Modify.Update_Building2D_PredictedYearBuilt(table, countyId, yearBuiltDatas_GIS);
                        }
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    FailedSubdivisionCount++;
                    Serilog.Modify.Log(exception, "Building data unassigned building rows build failed - county {CountyId}, {BuildingCount} buildings read could not be turned into rows", countyId, building2Ds_Unassigned.Count);
                    continue;
                }

                if (table.RowCount == 0)
                {
                    continue;
                }

                bool updated;
                try
                {
                    updated = await buildingDataPostgreSQLConverter.PushAsync(table, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    FailedSubdivisionCount++;
                    Serilog.Modify.Log(exception, "Building data unassigned buildings write failed - county {CountyId}, {RowCount} rows built could not be written", countyId, table.RowCount);
                    continue;
                }

                if (!updated)
                {
                    FailedSubdivisionCount++;
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Building data unassigned buildings write failed - county {CountyId}, write of {RowCount} rows was rolled back", countyId, table.RowCount);
                    continue;
                }

                UnassignedSubdivisionBuildingCount += building2Ds_Unassigned.Count(x => x.SubdivisionId is null);
                CrossCountySubdivisionBuildingCount += building2Ds_Unassigned.Count(x => x.SubdivisionId is not null);
                UpdatedRowCount += table.RowCount;
                progress.Report(UpdatedRowCount);
            }

            Serilog.Modify.Log(
                "{Type}: finished - {ProcessedCount} subdivisions written, {UnassignedCount} unassigned buildings written, {CrossCountyCount} cross-county buildings written, {RowCount} total rows, {FailedCount} failed, {SkippedCount} skipped for want of a parent county, {UnfulfilledCount} update types unfulfilled",
                nameof(PostgreSQLBuildingDataUpdateTask),
                ProcessedSubdivisionCount,
                UnassignedSubdivisionBuildingCount,
                CrossCountySubdivisionBuildingCount,
                UpdatedRowCount,
                FailedSubdivisionCount,
                SkippedSubdivisionCount,
                UnfulfilledUpdateTypeCount);

            return FailedSubdivisionCount == 0 && UnfulfilledUpdateTypeCount == 0;
        }
    }
}
