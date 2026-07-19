using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task responsible for updating occupancy data within a PostgreSQL GIS database.
    /// <para>
    /// This class leverages the <see cref="GISPostgreSQLConverterManager"/> to execute the update process based on the provided <see cref="PostgreSQLUpdateOccupancyOptions"/>.
    /// </para>
    /// </summary>
    public class PostgreSQLUpdateOccupancyTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the GIS PostgreSQL converter manager used to refresh the data.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Gets or sets the options used to configure the PostgreSQL occupancy update process.
        /// </summary>
        public PostgreSQLUpdateOccupancyOptions PostgreSQLUpdateOccupancyOptions { get; set; } = new PostgreSQLUpdateOccupancyOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUpdateOccupancyTask"/> class.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The <see cref="GISPostgreSQLConverterManager"/> used to refresh the occupancy data.</param>
        public PostgreSQLUpdateOccupancyTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager;
        }

        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            if (gISPostgreSQLConverterManager is null)
            {
                return false;
            }

            PostgreSQLUpdateOccupancyOptions ??= new PostgreSQLUpdateOccupancyOptions();

            bool includeBuilding2Ds = PostgreSQLUpdateOccupancyOptions.IncludeBuilding2Ds;
            bool includeAdministrativeAreal2Ds = PostgreSQLUpdateOccupancyOptions.IncludeAdministrativeAreal2Ds;
            bool clear = PostgreSQLUpdateOccupancyOptions.Clear;

            // Bulk reads/writes over hundreds of thousands of records exceed the 30s default; allow up to 10 minutes per statement.
            const int commandTimeout = 600;

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return false;
            }

            AdministrativeAreal2DOccupancyDataPostgreSQLConverter? administrativeAreal2DOccupancyDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DOccupancyDataPostgreSQLConverter>();
            if (administrativeAreal2DOccupancyDataPostgreSQLConverter is null)
            {
                return false;
            }

            long totalUpdated = 0;

            if (includeAdministrativeAreal2Ds)
            {
                if (clear)
                {
                    await administrativeAreal2DOccupancyDataPostgreSQLConverter.ClearAsync(commandTimeout, cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();
                }

                List<AdministrativeArealType> administrativeArealTypes = [.. Enum.GetValues<AdministrativeArealType>()];
                administrativeArealTypes.Remove(AdministrativeArealType.Undefined);
                administrativeArealTypes.Reverse();

                Dictionary<int, OccupancyData> occupancyDatas_ById = [];
                foreach (AdministrativeArealType administrativeArealType in administrativeArealTypes)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(administrativeArealType, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                    if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
                    {
                        continue;
                    }

                    AdministrativeArealType? administrativeArealType_Child = administrativeArealType.ChildAdministrativeArealType();

                    foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        AdministrativeAreal2D? administrativeAreal2D_PostgreSQL = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(administrativeAreal2DReference.Id, commandTimeout);
                        if (administrativeAreal2D_PostgreSQL is null)
                        {
                            continue;
                        }

                        GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = administrativeAreal2D_PostgreSQL.ToDiGi();

                        if (administrativeAreal2D is AdministrativeSubdivision administrativeSubdivision)
                        {
                            occupancyDatas_ById[administrativeAreal2D_PostgreSQL.Id] = new OccupancyData(administrativeAreal2DReference.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, administrativeSubdivision?.Occupancy ?? 0);
                            continue;
                        }

                        if (administrativeArealType_Child is not null)
                        {
                            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Child = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(administrativeArealType_Child.Value, administrativeAreal2DReference.Id, false, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                            if (administrativeAreal2DReferences_Child is null || administrativeAreal2DReferences_Child.Count == 0)
                            {
                                continue;
                            }

                            uint occupancy = 0;

                            foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Child in administrativeAreal2DReferences_Child)
                            {
                                if (!occupancyDatas_ById.TryGetValue(administrativeAreal2DReference_Child.Id, out OccupancyData? occupancyData) || occupancyData?.Occupancy is null)
                                {
                                    continue;
                                }

                                occupancy += occupancyData.Occupancy.Value;
                            }

                            occupancyDatas_ById[administrativeAreal2DReference.Id] = new OccupancyData(administrativeAreal2DReference.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, occupancy);
                        }
                    }
                }

                if (occupancyDatas_ById.Count != 0)
                {
                    List<AdministrativeAreal2DOccupancyData> administrativeAreal2DOccupancyDatas = [];
                    foreach (OccupancyData occupancyData in occupancyDatas_ById.Values)
                    {
                        if (occupancyData.ToPostgreSQL() is AdministrativeAreal2DOccupancyData administrativeAreal2DOccupancyData)
                        {
                            administrativeAreal2DOccupancyDatas.Add(administrativeAreal2DOccupancyData);
                        }
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                    await administrativeAreal2DOccupancyDataPostgreSQLConverter.UpdateAsync(administrativeAreal2DOccupancyDatas, commandTimeout);

                    totalUpdated += administrativeAreal2DOccupancyDatas.Count;
                    progress.Report(totalUpdated);
                }
            }

            if (includeBuilding2Ds)
            {
                Building2DOccupancyDataPostgreSQLConverter? building2DOccupancyDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DOccupancyDataPostgreSQLConverter>();
                if (building2DOccupancyDataPostgreSQLConverter is null)
                {
                    return false;
                }

                Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DPostgreSQLConverter>();
                if (building2DPostgreSQLConverter is null)
                {
                    return false;
                }

                if (clear)
                {
                    await building2DOccupancyDataPostgreSQLConverter.ClearAsync(commandTimeout, cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();
                }

                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivison, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
                {
                    return true;
                }

                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (administrativeAreal2DReference?.Reference is not string reference || string.IsNullOrWhiteSpace(reference) || administrativeAreal2DReference.CountyId is not int countyId)
                    {
                        continue;
                    }

                    List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, administrativeAreal2DReference.Id, null, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    List<Building2D>? building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(building2DReferences, commandTimeout);
                    if (building2Ds is null || building2Ds.Count == 0)
                    {
                        continue;
                    }

                    List<Tuple<GIS.Classes.Building2D, double>> tuples_BuildingArea = [];
                    double area = 0;

                    foreach (Building2D building2D_Raw in building2Ds)
                    {
                        if (building2D_Raw?.ToDiGi() is not GIS.Classes.Building2D building2D ||
                            !GIS.Query.IsOccupied(building2D) ||
                            building2D.PolygonalFace2D?.GetArea() is not double floorArea ||
                            floorArea <= 0)
                        {
                            continue;
                        }

                        double buildingArea = floorArea * (building2D.Storeys is not > 0 ? 1 : building2D.Storeys);
                        tuples_BuildingArea.Add(new Tuple<GIS.Classes.Building2D, double>(building2D, buildingArea));
                        area += buildingArea;
                    }

                    if (tuples_BuildingArea.Count == 0)
                    {
                        continue;
                    }

                    OccupancyData? occupancyData = (await administrativeAreal2DOccupancyDataPostgreSQLConverter.GetItemByReferenceAsync(reference, cancellationToken, commandTimeout))?.ToDiGi() as OccupancyData;
                    int remainingOccupancy = (int)(occupancyData?.Occupancy ?? 0);
                    double occupancyPerMeterSquared = (double)remainingOccupancy / area;

                    List<OccupancyData> occupancyDatas = [];
                    bool canEnforceMin1 = remainingOccupancy >= tuples_BuildingArea.Count;

                    foreach (Tuple<GIS.Classes.Building2D, double> tuple_BuildingArea in tuples_BuildingArea)
                    {
                        uint occupancy_Building2D = (uint)Math.Floor(tuple_BuildingArea.Item2 * occupancyPerMeterSquared);
                        if (canEnforceMin1 && occupancy_Building2D == 0)
                        {
                            occupancy_Building2D = 1;
                        }

                        occupancyDatas.Add(new OccupancyData(tuple_BuildingArea.Item1.Reference, tuple_BuildingArea.Item2, occupancy_Building2D));
                        remainingOccupancy -= (int)occupancy_Building2D;
                    }

                    if (remainingOccupancy < 0)
                    {
                        occupancyDatas.Sort((x, y) => (y.Occupancy ?? 0).CompareTo(x.Occupancy ?? 0));
                        for (int i = 0; i < occupancyDatas.Count && remainingOccupancy < 0; i++)
                        {
                            uint currentOccupancy = occupancyDatas[i].Occupancy ?? 0;
                            if (currentOccupancy > 0)
                            {
                                occupancyDatas[i] = new OccupancyData(occupancyDatas[i].Reference, occupancyDatas[i].OccupancyArea, currentOccupancy - 1);
                                remainingOccupancy++;
                            }
                        }
                    }

                    if (remainingOccupancy > 0)
                    {
                        Random random = new(occupancyDatas.Count);
                        Range<int> range = new(0, occupancyDatas.Count - 1);

                        while (remainingOccupancy > 0)
                        {
                            int index = Core.Query.Random(random, range);
                            uint currentOccupancy = occupancyDatas[index].Occupancy ?? 0;

                            occupancyDatas[index] = new OccupancyData(occupancyDatas[index].Reference, occupancyDatas[index].OccupancyArea, currentOccupancy + 1);
                            remainingOccupancy--;
                        }
                    }

                    List<Building2DOccupancyData> building2DOccupancyDatas = [];
                    foreach (OccupancyData occupancyData_Building in occupancyDatas)
                    {
                        if (occupancyData_Building.ToPostgreSQL(administrativeAreal2DReference.CountyId) is Building2DOccupancyData building2DOccupancyData)
                        {
                            building2DOccupancyDatas.Add(building2DOccupancyData);
                        }
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                    await building2DOccupancyDataPostgreSQLConverter.UpdateAsync(building2DOccupancyDatas, commandTimeout);

                    totalUpdated += building2DOccupancyDatas.Count;
                    progress.Report(totalUpdated);
                }
            }

            return true;
        }
    }
}