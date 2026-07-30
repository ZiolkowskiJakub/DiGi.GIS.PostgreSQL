using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
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
    /// Represents a background task that updates building data from AdministrativeAreal2D and Building2D sources.
    /// </summary>
    public class PostgreSQLBuildingDataUpdateTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// The GIS PostgreSQL converter manager used to retrieve converters and execute operations.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

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
        /// Executes the background task to update building data from AdministrativeAreal2D and Building2D sources.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of processed items.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true when every processed subdivision was updated without error; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            if (PostgreSQLBuildingDataUpdateOptions.BuildingDataUpdateTypes is not IEnumerable<BuildingDataUpdateType> buildingDataUpdateTypes_Temp || !buildingDataUpdateTypes_Temp.Any())
            {
                return false;
            }

            HashSet<BuildingDataUpdateType> buildingDataUpdateTypes = [.. buildingDataUpdateTypes_Temp];

            BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingDataPostgreSQLConverter>();
            if (buildingDataPostgreSQLConverter is null)
            {
                return false;
            }

            Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                return false;
            }

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return false;
            }

            Building2DOccupancyDataPostgreSQLConverter? building2DOccupancyDataPostgreSQLConverter = buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Occupancy) ? gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DOccupancyDataPostgreSQLConverter>() : null;

            // Bulk reads/writes over hundreds of thousands of records exceed the 30s default; allow up to 10 minutes per statement.
            const int commandTimeout = 600;

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivison, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return false;
            }

            // Update_RadialRatios evaluates the radiuses largest-first, so the spatial query must cover the largest one.
            List<double> radiuses = [200, 400, 600, 1000];
            double radius_Max = radiuses.Max();

            long totalUpdated = 0;
            long failedCount = 0;

            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (administrativeAreal2DReference.CountyId is not int countyId)
                {
                    continue;
                }

                // The references were queried by AdministrativeArealType.Subdivison, so Id is the subdivision id.
                int subdivisionId = administrativeAreal2DReference.Id;

                List<Building2D>? building2Ds = null;
                List<AdministrativeAreal2D>? administrativeAreal2Ds = null;
                List<Building2DReference>? building2DReferences = null;

                try
                {
                    AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(administrativeAreal2DReference, cancellationToken: cancellationToken);

                    // GetAdministrativeAreal2DsByIdsAsync treats a null or empty id collection as 'no filter' and reads the whole table.
                    if (administrativeAreal2DReferencePath?.AdministrativeAreal2DReferences?.ConvertAll(x => x.Id) is List<int> ids && ids.Count != 0)
                    {
                        administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(ids);
                    }

                    building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(countyId, subdivisionId, excludedReferences: null, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(building2DReferences, commandTimeout);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception)
                {
                    failedCount++;
                    continue;
                }

                if (building2Ds is null || building2Ds.Count == 0)
                {
                    continue;
                }

                List<GIS.Classes.Building2D> building2Ds_GIS = building2Ds.Select(x => x.ToDiGi()).OfType<GIS.Classes.Building2D>().ToList();
                List<GIS.Classes.AdministrativeAreal2D>? administrativeAreal2Ds_GIS = administrativeAreal2Ds?.Select(x => x.ToDiGi()).OfType<GIS.Classes.AdministrativeAreal2D>().ToList();

                Table table = new();

                try
                {
                    if (buildingDataUpdateTypes.Contains(BuildingDataUpdateType.General))
                    {
                        IO.Modify.Update(table, countyId, subdivisionId, building2Ds_GIS, administrativeAreal2Ds: administrativeAreal2Ds_GIS);
                    }

                    if (buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Occupancy))
                    {
                        IO.Modify.Update_Building2D_Occupancy(table, countyId, building2Ds_GIS);

                        if (building2DOccupancyDataPostgreSQLConverter is not null)
                        {
                            List<string> references = building2DReferences.Select(x => x.Reference).OfType<string>().ToList();

                            List<Building2DOccupancyData>? building2DOccupancyDatas = await building2DOccupancyDataPostgreSQLConverter.GetItemsByReferencesAsync(references, countyId, cancellationToken: cancellationToken);
                            if (building2DOccupancyDatas is not null)
                            {
                                Modify.Update_Occupancy(table, building2DOccupancyDatas);
                            }
                        }
                    }

                    if (buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Database))
                    {
                        Modify.Update_Id(table, building2DReferences);
                    }

                    if (buildingDataUpdateTypes.Contains(BuildingDataUpdateType.RadialRatios))
                    {
                        foreach (GIS.Classes.Building2D building2D_GIS in building2Ds_GIS)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            Point2D? point2D_Centroid = building2D_GIS.PolygonalFace2D?.Centroid();
                            if (point2D_Centroid is null)
                            {
                                continue;
                            }

                            Circle2D circle2D = new(point2D_Centroid, radius_Max);

                            List<Building2D>? building2Ds_Circle = await building2DPostgreSQLConverter.GetBuilding2DsByCircle2DAsync(circle2D, cancellationToken: cancellationToken);

                            List<GIS.Classes.Building2D> building2Ds_Circle_GIS = building2Ds_Circle?.Select(x => x.ToDiGi()).OfType<GIS.Classes.Building2D>().ToList() ?? [];

                            IO.Modify.Update_RadialRatios(table, radiuses, countyId, building2D_GIS, building2Ds_Circle_GIS);
                        }
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception)
                {
                    failedCount++;
                    continue;
                }

                if (table.RowCount == 0)
                {
                    continue;
                }

                bool updated;
                try
                {
                    updated = await buildingDataPostgreSQLConverter.PushAsync(table);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception)
                {
                    updated = false;
                }

                if (!updated)
                {
                    failedCount++;
                    continue;
                }

                totalUpdated += table.RowCount;
                progress.Report(totalUpdated);
            }

            return failedCount == 0;
        }
    }
}
