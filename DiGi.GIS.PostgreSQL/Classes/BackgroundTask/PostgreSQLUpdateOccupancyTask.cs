using DiGi.Core.Classes;
using DiGi.GIS.Classes;
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

        /// <summary>
        /// Executes the background task to update occupancy data for administrative areal units and buildings.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of processed items.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the update was successful; otherwise, false.</returns>
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

                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Subdivisions = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivision, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Municipalities = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Municipality, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Counties = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.County, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Voivodeships = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Voivodeship, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Countries = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Country, cancellationToken: cancellationToken, commandTimeout: commandTimeout);

                ILookup<int?, AdministrativeAreal2DReference>? subdivisionsByMunicipalityId = administrativeAreal2DReferences_Subdivisions?.Where(s => s != null && s.MunicipalityId.HasValue).ToLookup(s => s.MunicipalityId);
                ILookup<int?, AdministrativeAreal2DReference>? directSubdivisionsByCountyId = administrativeAreal2DReferences_Subdivisions?.Where(s => s != null && s.MunicipalityId == null && s.CountyId.HasValue).ToLookup(s => s.CountyId);
                ILookup<int?, AdministrativeAreal2DReference>? municipalitiesByCountyId = administrativeAreal2DReferences_Municipalities?.Where(m => m != null && m.CountyId.HasValue).ToLookup(m => m.CountyId);
                ILookup<int?, AdministrativeAreal2DReference>? countiesByVoivodeshipId = administrativeAreal2DReferences_Counties?.Where(c => c != null && c.VoivodeshipId.HasValue).ToLookup(c => c.VoivodeshipId);
                ILookup<int?, AdministrativeAreal2DReference>? voivodeshipsByCountryId = administrativeAreal2DReferences_Voivodeships?.Where(v => v != null && v.CountryId.HasValue).ToLookup(v => v.CountryId);

                Dictionary<int, OccupancyData> occupancyDatas_ById = [];

                HashSet<int> countyIds_ForSubdivisions = [.. administrativeAreal2DReferences_Counties?.Select(c => c.Id) ?? []];
                if (administrativeAreal2DReferences_Subdivisions is not null)
                {
                    foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Subdivision in administrativeAreal2DReferences_Subdivisions)
                    {
                        if (administrativeAreal2DReference_Subdivision.CountyId.HasValue)
                        {
                            countyIds_ForSubdivisions.Add(administrativeAreal2DReference_Subdivision.CountyId.Value);
                        }
                    }
                }

                foreach (int countyId in countyIds_ForSubdivisions)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2D>? countySubdivisions = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.Subdivision, countyId, cancellationToken: cancellationToken);
                    if (countySubdivisions is null || countySubdivisions.Count == 0)
                    {
                        continue;
                    }

                    foreach (AdministrativeAreal2D countySubdivision in countySubdivisions)
                    {
                        GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = countySubdivision.ToDiGi();
                        if (administrativeAreal2D is AdministrativeSubdivision administrativeSubdivision)
                        {
                            occupancyDatas_ById[countySubdivision.Id] = new OccupancyData(countySubdivision.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, administrativeSubdivision?.Occupancy ?? 0);
                        }
                    }
                }

                if (administrativeAreal2DReferences_Municipalities is not null && administrativeAreal2DReferences_Municipalities.Count != 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2D>? municipalities = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.Municipality, cancellationToken: cancellationToken);
                    if (municipalities is not null)
                    {
                        foreach (AdministrativeAreal2D municipality in municipalities)
                        {
                            GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = municipality.ToDiGi();
                            uint occupancy = 0;

                            if (subdivisionsByMunicipalityId is not null)
                            {
                                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Subdivision in subdivisionsByMunicipalityId[municipality.Id])
                                {
                                    if (occupancyDatas_ById.TryGetValue(administrativeAreal2DReference_Subdivision.Id, out OccupancyData? occupancyData) && occupancyData?.Occupancy is not null)
                                    {
                                        occupancy += occupancyData.Occupancy.Value;
                                    }
                                }
                            }

                            occupancyDatas_ById[municipality.Id] = new OccupancyData(municipality.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, occupancy);
                        }
                    }
                }

                if (administrativeAreal2DReferences_Counties is not null && administrativeAreal2DReferences_Counties.Count != 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2D>? counties = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.County, cancellationToken: cancellationToken);
                    if (counties is not null)
                    {
                        foreach (AdministrativeAreal2D county in counties)
                        {
                            GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = county.ToDiGi();
                            uint occupancy = 0;

                            if (municipalitiesByCountyId is not null)
                            {
                                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Municipality in municipalitiesByCountyId[county.Id])
                                {
                                    if (occupancyDatas_ById.TryGetValue(administrativeAreal2DReference_Municipality.Id, out OccupancyData? occupancyData) && occupancyData?.Occupancy is not null)
                                    {
                                        occupancy += occupancyData.Occupancy.Value;
                                    }
                                }
                            }

                            if (directSubdivisionsByCountyId is not null)
                            {
                                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_DirectSubdivision in directSubdivisionsByCountyId[county.Id])
                                {
                                    if (occupancyDatas_ById.TryGetValue(administrativeAreal2DReference_DirectSubdivision.Id, out OccupancyData? occupancyData) && occupancyData?.Occupancy is not null)
                                    {
                                        occupancy += occupancyData.Occupancy.Value;
                                    }
                                }
                            }

                            occupancyDatas_ById[county.Id] = new OccupancyData(county.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, occupancy);
                        }
                    }
                }

                if (administrativeAreal2DReferences_Voivodeships is not null && administrativeAreal2DReferences_Voivodeships.Count != 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2D>? voivodeships = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.Voivodeship, cancellationToken: cancellationToken);
                    if (voivodeships is not null)
                    {
                        foreach (AdministrativeAreal2D voivodeship in voivodeships)
                        {
                            GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = voivodeship.ToDiGi();
                            uint occupancy = 0;

                            if (countiesByVoivodeshipId is not null)
                            {
                                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_County in countiesByVoivodeshipId[voivodeship.Id])
                                {
                                    if (occupancyDatas_ById.TryGetValue(administrativeAreal2DReference_County.Id, out OccupancyData? occupancyData) && occupancyData?.Occupancy is not null)
                                    {
                                        occupancy += occupancyData.Occupancy.Value;
                                    }
                                }
                            }

                            occupancyDatas_ById[voivodeship.Id] = new OccupancyData(voivodeship.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, occupancy);
                        }
                    }
                }

                if (administrativeAreal2DReferences_Countries is not null && administrativeAreal2DReferences_Countries.Count != 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2D>? countries = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType.Country, cancellationToken: cancellationToken);
                    if (countries is not null)
                    {
                        foreach (AdministrativeAreal2D country in countries)
                        {
                            GIS.Classes.AdministrativeAreal2D? administrativeAreal2D = country.ToDiGi();
                            uint occupancy = 0;

                            if (voivodeshipsByCountryId is not null)
                            {
                                foreach (AdministrativeAreal2DReference administrativeAreal2DReference_Voivodeship in voivodeshipsByCountryId[country.Id])
                                {
                                    if (occupancyDatas_ById.TryGetValue(administrativeAreal2DReference_Voivodeship.Id, out OccupancyData? occupancyData) && occupancyData?.Occupancy is not null)
                                    {
                                        occupancy += occupancyData.Occupancy.Value;
                                    }
                                }
                            }

                            occupancyDatas_ById[country.Id] = new OccupancyData(country.Reference, administrativeAreal2D?.PolygonalFace2D?.GetArea() ?? 0, occupancy);
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

                List<AdministrativeAreal2DReference>? countyReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.County, cancellationToken: cancellationToken, commandTimeout: commandTimeout);
                List<AdministrativeAreal2DReference>? subdivisionReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivision, cancellationToken: cancellationToken, commandTimeout: commandTimeout);

                if (subdivisionReferences is null || subdivisionReferences.Count == 0)
                {
                    return true;
                }

                ILookup<int?, AdministrativeAreal2DReference> subdivisionsByCountyId = subdivisionReferences.Where(s => s != null && s.CountyId.HasValue).ToLookup(s => s.CountyId);

                HashSet<int> countyIds = [.. countyReferences?.Select(c => c.Id) ?? []];
                foreach (AdministrativeAreal2DReference subdivisionReference in subdivisionReferences)
                {
                    if (subdivisionReference.CountyId.HasValue)
                    {
                        countyIds.Add(subdivisionReference.CountyId.Value);
                    }
                }

                foreach (int countyId in countyIds)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<AdministrativeAreal2DReference> countySubdivisions = [.. subdivisionsByCountyId[countyId]];
                    if (countySubdivisions.Count == 0)
                    {
                        continue;
                    }

                    List<Building2D>? countyBuildings = await building2DPostgreSQLConverter.GetBuilding2DsByCountyIdAsync(countyId, subdivisionId: null, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    if (countyBuildings is null || countyBuildings.Count == 0)
                    {
                        continue;
                    }

                    List<string> countySubdivisionReferences = [.. countySubdivisions.Where(s => !string.IsNullOrWhiteSpace(s.Reference)).Select(s => s.Reference!)];
                    Dictionary<string, OccupancyData> subdivisionOccupancyDatas_ByReference = [];

                    if (countySubdivisionReferences.Count > 0)
                    {
                        List<AdministrativeAreal2DOccupancyData>? countySubdivisionOccupancyDatas = await administrativeAreal2DOccupancyDataPostgreSQLConverter.GetItemsByReferencesAsync(countySubdivisionReferences, limit: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                        if (countySubdivisionOccupancyDatas is not null)
                        {
                            foreach (AdministrativeAreal2DOccupancyData countySubdivisionOccupancyData in countySubdivisionOccupancyDatas)
                            {
                                if (countySubdivisionOccupancyData?.Reference is not null && countySubdivisionOccupancyData.ToDiGi() is OccupancyData occupancyData)
                                {
                                    subdivisionOccupancyDatas_ByReference[countySubdivisionOccupancyData.Reference] = occupancyData;
                                }
                            }
                        }
                    }

                    ILookup<int?, Building2D> buildingsBySubdivisionId = countyBuildings.Where(b => b?.SubdivisionId != null).ToLookup(b => b.SubdivisionId);

                    List<Building2DOccupancyData> countyBuilding2DOccupancyDatas = [];

                    foreach (AdministrativeAreal2DReference subdivisionReference in countySubdivisions)
                    {
                        if (subdivisionReference.Reference is null)
                        {
                            continue;
                        }

                        List<Building2D> subdivisionBuildings = [.. buildingsBySubdivisionId[subdivisionReference.Id]];
                        if (subdivisionBuildings.Count == 0)
                        {
                            continue;
                        }

                        subdivisionOccupancyDatas_ByReference.TryGetValue(subdivisionReference.Reference, out OccupancyData? subdivisionOccupancyData);

                        List<Building2DOccupancyData> building2DOccupancyDatas = CalculateBuilding2DOccupancyDatas(countyId, subdivisionBuildings, subdivisionOccupancyData);
                        if (building2DOccupancyDatas.Count > 0)
                        {
                            countyBuilding2DOccupancyDatas.AddRange(building2DOccupancyDatas);
                        }
                    }

                    if (countyBuilding2DOccupancyDatas.Count > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await building2DOccupancyDataPostgreSQLConverter.UpdateAsync(countyBuilding2DOccupancyDatas, commandTimeout, cancellationToken);

                        totalUpdated += countyBuilding2DOccupancyDatas.Count;
                        progress.Report(totalUpdated);
                    }
                }
            }

            return true;

            static List<Building2DOccupancyData> CalculateBuilding2DOccupancyDatas(int countyId, List<Building2D> subdivisionBuildings, OccupancyData? subdivisionOccupancyData)
            {
                List<Tuple<GIS.Classes.Building2D, double>> tuples_BuildingArea = [];
                double totalArea = 0;

                foreach (Building2D building2D_Raw in subdivisionBuildings)
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
                    totalArea += buildingArea;
                }

                if (tuples_BuildingArea.Count == 0 || totalArea <= 0)
                {
                    return [];
                }

                int remainingOccupancy = (int)(subdivisionOccupancyData?.Occupancy ?? 0);
                double occupancyPerMeterSquared = (double)remainingOccupancy / totalArea;

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

                List<Building2DOccupancyData> result = [];
                foreach (OccupancyData occupancyData_Building in occupancyDatas)
                {
                    if (occupancyData_Building.ToPostgreSQL(countyId) is Building2DOccupancyData building2DOccupancyData)
                    {
                        result.Add(building2DOccupancyData);
                    }
                }

                return result;
            }
        }
    }
}