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
    public class PostgreSQLUpdateOccupancyTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the GIS PostgreSQL converter manager used to refresh the data.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        public PostgreSQLUpdateOccupancyOptions PostgreSQLUpdateOccupancyOptions { get; set; } = new PostgreSQLUpdateOccupancyOptions();

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

            if (includeAdministrativeAreal2Ds)
            {
                List<AdministrativeArealType> administrativeArealTypes = [.. Enum.GetValues<AdministrativeArealType>()];
                administrativeArealTypes.Remove(AdministrativeArealType.Undefined);
                administrativeArealTypes.Reverse();

                Dictionary<int, OccupancyData> dictionary = [];
                foreach (AdministrativeArealType administrativeArealType in administrativeArealTypes)
                {
                    List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(administrativeArealType, cancellationToken: cancellationToken);
                    if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
                    {
                        continue;
                    }

                    AdministrativeArealType? administrativeArealType_Child = administrativeArealType.ChildAdministrativeArealType();

                    foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                    {
                        AdministrativeAreal2D? administrativeAreal2D_PostgreSQL = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(administrativeAreal2DReference.Id);
                        if (administrativeAreal2D_PostgreSQL is null)
                        {
                            continue;
                        }

                        GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = administrativeAreal2D_PostgreSQL.ToDiGi();

                        if (administrativeAreal2D is AdministrativeSubdivision administrativeSubdivision)
                        {
                            dictionary[administrativeAreal2D_PostgreSQL.Id] = new OccupancyData(administrativeAreal2DReference.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, administrativeSubdivision?.Occupancy ?? 0);
                            continue;
                        }


                        if (administrativeArealType_Child is not null)
                        {
                            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Child = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(administrativeArealType_Child.Value, administrativeAreal2DReference.Id, false, cancellationToken: cancellationToken);
                            if (administrativeAreal2DReferences_Child is null || administrativeAreal2DReferences_Child.Count == 0)
                            {
                                continue;
                            }

                            uint occupancy = 0;

                            foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Child in administrativeAreal2DReferences_Child)
                            {
                                if (!dictionary.TryGetValue(administrativeAreal2DReference_Child.Id, out OccupancyData? occupancyData) || occupancyData?.Occupancy is null)
                                {
                                    continue;
                                }

                                occupancy += occupancyData.Occupancy.Value;
                            }

                            dictionary[administrativeAreal2DReference.Id] = new OccupancyData(administrativeAreal2DReference.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, occupancy);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (dictionary is not null && dictionary.Count != 0)
                {
                    List<AdministrativeAreal2DOccupancyData> administrativeAreal2DOccupancyDatas = [];
                    foreach (OccupancyData occupancyData in dictionary.Values)
                    {
                        if (occupancyData.ToPostgreSQL() is AdministrativeAreal2DOccupancyData administrativeAreal2DOccupancyData)
                        {
                            administrativeAreal2DOccupancyDatas.Add(administrativeAreal2DOccupancyData);

                        }
                    }

                    await administrativeAreal2DOccupancyDataPostgreSQLConverter.UpdateAsync(administrativeAreal2DOccupancyDatas);
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

                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivison, cancellationToken: cancellationToken);
                if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
                {
                    return false;
                }

                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                {
                    if (administrativeAreal2DReference?.Reference is not string reference || string.IsNullOrWhiteSpace(reference) || administrativeAreal2DReference.CountyId is not int countyId)
                    {
                        continue;
                    }

                    List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, administrativeAreal2DReference.Id, null, cancellationToken: cancellationToken);
                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    List<Building2D>? building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(building2DReferences);
                    if (building2Ds is null || building2Ds.Count == 0)
                    {
                        continue;
                    }

                    List<Tuple<GIS.Classes.Building2D, double>> tuples = [];

                    int count = building2Ds.Count;

                    double area = 0;

                    for (int i = 0; i < count; i++)
                    {
                        if (building2Ds[i]?.ToDiGi() is not GIS.Classes.Building2D building2D || !GIS.Query.IsOccupied(building2D) || building2D.PolygonalFace2D?.GetArea() is not double floorArea || floorArea is not > 0)
                        {
                            continue;
                        }

                        double buildingArea = floorArea * (building2D.Storeys is not > 0 ? 1 : building2D.Storeys);

                        tuples.Add(new Tuple<GIS.Classes.Building2D, double>(building2D, buildingArea));

                        area += buildingArea;
                    }

                    if (tuples.Count <= 0)
                    {
                        continue;
                    }

                    OccupancyData? occupancyData = (await administrativeAreal2DOccupancyDataPostgreSQLConverter.GetItemByReferenceAsync(reference, cancellationToken))?.ToDiGi() as OccupancyData;

                    uint occupancy = occupancyData?.Occupancy ?? 0;

                    double occupancyPerMeterSquared = occupancy / area;

                    List<OccupancyData> occupancyDatas = [];
                    foreach (Tuple<GIS.Classes.Building2D, double> tuple in tuples)
                    {
                        uint occupancy_Building2D = System.Convert.ToUInt32(Math.Floor(tuple.Item2 * occupancyPerMeterSquared));
                        if (occupancy_Building2D == 0)
                        {
                            occupancy_Building2D++;
                        }

                        occupancyDatas.Add(new OccupancyData(tuple.Item1.Reference, tuple.Item2, occupancy_Building2D));
                        occupancy -= occupancy_Building2D;
                    }

                    Random random = new(occupancyDatas.Count);

                    Range<int> range = new(0, occupancyDatas.Count - 1);

                    if (occupancy < 0)
                    {
                        occupancyDatas.Sort((x, y) => (y.Occupancy ?? 0).CompareTo(x.Occupancy ?? 0));

                        while (occupancy < 0 && (occupancyDatas[0].Occupancy ?? 0) > 0)
                        {
                            for (int i = 0; i < occupancyDatas.Count; i++)
                            {
                                uint occupancy_Building2D = occupancyDatas[i].Occupancy ?? 0;
                                if(occupancy_Building2D <= 0 )
                                {
                                    break;
                                }

                                occupancyDatas[i] = new OccupancyData(occupancyDatas[i].Reference, occupancyDatas[i].OccupancyArea, occupancy_Building2D - 1);
                                occupancy++;

                                if(occupancy >= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    while (occupancy > 0)
                    {
                        int index = Core.Query.Random(random, range);

                        occupancyDatas[index] = new OccupancyData(occupancyDatas[index].Reference, occupancyDatas[index].OccupancyArea, occupancyDatas[index].Occupancy + 1);
                        occupancy--;
                    }

                    List<Building2DOccupancyData> building2DOccupancyDatas = [];
                    foreach(OccupancyData occupancyData_Building in occupancyDatas)
                    {
                        if(occupancyData_Building.ToPostgreSQL(administrativeAreal2DReference.CountyId) is not Building2DOccupancyData building2DOccupancyData)
                        {
                            continue;
                        }

                        building2DOccupancyDatas.Add(building2DOccupancyData);
                    }

                    await building2DOccupancyDataPostgreSQLConverter.UpdateAsync(building2DOccupancyDatas);
                }
            }


            return true;
        }
    }
}