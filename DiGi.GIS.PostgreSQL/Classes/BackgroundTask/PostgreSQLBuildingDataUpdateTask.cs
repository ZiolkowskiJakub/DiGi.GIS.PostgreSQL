using DiGi.Core.Classes;
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
        /// Gets the GIS PostgreSQL converter manager used to retrieve converters and execute operations.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Gets or sets the options used to configure the PostgreSQL building data update process.
        /// </summary>
        public PostgreSQLBuildingDataUpdateOptions uIBuildingDataUpdateOptions { get; set; } = new PostgreSQLBuildingDataUpdateOptions();

        /// <summary>
        /// Constructor with Dependency Injection.
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
        /// <returns>A task representing the asynchronous operation. Returns true if the update was successful; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
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

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.Subdivison, cancellationToken: cancellationToken);
            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return false;
            }

            uIBuildingDataUpdateOptions ??= new PostgreSQLBuildingDataUpdateOptions();

            if (uIBuildingDataUpdateOptions.BuildingDataUpdateTypes is not IEnumerable<BuildingDataUpdateType> buildingDataUpdateTypes || !buildingDataUpdateTypes.Any())
            {
                return false;
            }

            long totalUpdated = 0;

            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                if (administrativeAreal2DReference.CountyId is not int countyId)
                {
                    continue;
                }

                List<Building2D>? building2Ds = null;
                AdministrativeAreal2DReference? administrativeAreal2DReference_Subdivision = null;
                List<AdministrativeAreal2D>? administrativeAreal2Ds = null;

                List<Building2DReference>? building2DReferences = null;

                try
                {
                    AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(administrativeAreal2DReference, cancellationToken);

                    administrativeAreal2DReference_Subdivision = administrativeAreal2DReferencePath?[AdministrativeArealType.Subdivison];

                    administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(administrativeAreal2DReferencePath?.AdministrativeAreal2DReferences?.ConvertAll(x => x.Id));

                    building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(administrativeAreal2DReference.CountyId.Value, administrativeAreal2DReference_Subdivision?.Id, null, cancellationToken);
                    if (building2DReferences is null || building2DReferences.Count == 0)
                    {
                        continue;
                    }

                    building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(building2DReferences);
                }
                catch (Exception)
                {
                }

                if (building2Ds is null || building2Ds.Count == 0)
                {
                    continue;
                }

                Core.IO.Table.Classes.Table table = new();

                try
                {
                    if (buildingDataUpdateTypes.Contains(BuildingDataUpdateType.General))
                    {
                        IO.Modify.Update(table, countyId, administrativeAreal2DReference_Subdivision?.Id, building2Ds?.ConvertAll(x => x.ToDiGi()!), administrativeAreal2Ds: administrativeAreal2Ds?.ConvertAll(x => x.ToDiGi()!));
                    }

                    if (buildingDataUpdateTypes.Contains(BuildingDataUpdateType.Occupancy))
                    {
                        IO.Modify.Update_Building2D_Occupancy(table, countyId, building2Ds?.ConvertAll(x => x.ToDiGi()!));

                        Building2DOccupancyDataPostgreSQLConverter? building2DOccupancyDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DOccupancyDataPostgreSQLConverter>();
                        if (building2DOccupancyDataPostgreSQLConverter is not null)
                        {
                            List<Building2DOccupancyData>? building2DOccupancyDatas = await building2DOccupancyDataPostgreSQLConverter.GetItemsByReferencesAsync(building2DReferences?.ConvertAll(x => x.Reference!), countyId, cancellationToken: cancellationToken);
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
                }
                catch (Exception)
                {
                }

                if (table is not null)
                {
                    int count = table.RowCount;

                    if (count != 0)
                    {
                        bool updated = false;
                        try
                        {
                            updated = await buildingDataPostgreSQLConverter.PushAsync(table);
                        }
                        catch (Exception)
                        {
                        }

                        if (updated)
                        {
                            totalUpdated += count;
                            progress.Report(totalUpdated);
                        }
                    }
                }
            }

            return true;
        }
    }
}