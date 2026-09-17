using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that fills the external components area columns of the building data table from the stored building models.
    /// <para>The run is driven by counties: for each one it reads the stored building models, classifies the components of every model into the wall, roof and floor buckets, and upserts one building data row per building keyed on county and reference.</para>
    /// <para>A county whose buildings carry no stored model is processed, not failed: there is simply nothing to classify there, and the buildings keep their current values. A county whose stored models cannot be classified - a component without a planar face, a model without a floor - is failed and logged with the exception, and the run keeps going over the other counties. <see cref="FailedCountyCount"/> is what tells the two apart, and a run with a failed county reports itself as not succeeded.</para>
    /// <para>A component that is valid but has no definable bucket - a wall whose normal is vertical - is skipped and counted in <see cref="SkippedComponentCount"/> rather than failing the county.</para>
    /// <para>The run is idempotent: the read is deterministic (the latest stored version of a model wins), the classification is pure, and the push upserts on county and reference, so a re-run writes the same values.</para>
    /// </summary>
    public class PostgreSQLBuildingDataExternalComponentsUpdateTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// The GIS PostgreSQL converter manager used to retrieve converters and execute operations.
        /// </summary>
        protected readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Gets or sets the options used to configure the run.
        /// </summary>
        public PostgreSQLBuildingDataExternalComponentsUpdateOptions PostgreSQLBuildingDataExternalComponentsUpdateOptions { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingDataExternalComponentsUpdateTask"/> class.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The GIS PostgreSQL converter manager used to retrieve converters and execute operations.</param>
        public PostgreSQLBuildingDataExternalComponentsUpdateTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Gets the number of counties that failed outright and were stepped over during the last run.
        /// <para>Each one is logged with the exception that caused it, so this figure is a count of entries to go and read rather than the whole of what is known.</para>
        /// </summary>
        public long FailedCountyCount { get; private set; }

        /// <summary>
        /// Gets the number of counties that were processed during the last run.
        /// <para>A county whose buildings carry no stored model is processed, not failed: there is simply nothing to classify there.</para>
        /// </summary>
        public long ProcessedCountyCount { get; private set; }

        /// <summary>
        /// Gets the number of building models read and classified during the last run.
        /// <para>Models rather than records: a reference with several stored versions is read as the latest one only, and that one is what is counted.</para>
        /// </summary>
        public long ProcessedModelCount { get; private set; }

        /// <summary>
        /// Gets the number of components skipped because their target bucket is undefined during the last run.
        /// <para>A wall whose normal is vertical, so its azimuth is undefined, or whose orientation against the building interior is degenerate. A component that cannot be classified at all does not count here - that is a data defect and fails the county.</para>
        /// </summary>
        public long SkippedComponentCount { get; private set; }

        /// <summary>
        /// Gets the number of building data rows written during the last run.
        /// <para>Rows rather than buildings: the same building is counted again on a later run.</para>
        /// </summary>
        public long UpdatedRowCount { get; private set; }

        /// <summary>
        /// Executes the background task that fills the external components area columns of the building data table from the stored building models.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of rows written.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true when every county in scope was processed without error; otherwise, false - including when a county’s stored models cannot be classified.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            FailedCountyCount = 0;
            ProcessedCountyCount = 0;
            ProcessedModelCount = 0;
            SkippedComponentCount = 0;
            UpdatedRowCount = 0;

            PostgreSQLBuildingDataExternalComponentsUpdateOptions ??= new();

            int commandTimeout = PostgreSQLBuildingDataExternalComponentsUpdateOptions.CommandTimeout;

            BuildingModelPostgreSQLConverter? buildingModelPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingModelPostgreSQLConverter>();
            if (buildingModelPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the stored building models cannot be read", nameof(PostgreSQLBuildingDataExternalComponentsUpdateTask), nameof(BuildingModelPostgreSQLConverter));
                return false;
            }

            BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<BuildingDataPostgreSQLConverter>();
            if (buildingDataPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - there is nowhere to write to", nameof(PostgreSQLBuildingDataExternalComponentsUpdateTask), nameof(BuildingDataPostgreSQLConverter));
                return false;
            }

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no {Converter} - the counties that scope the run cannot be read", nameof(PostgreSQLBuildingDataExternalComponentsUpdateTask), nameof(AdministrativeAreal2DPostgreSQLConverter));
                return false;
            }

            List<AdministrativeAreal2DReference>? countyReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.County, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (countyReferences is null || countyReferences.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Type}: no county could be read - the run cannot be scoped", nameof(PostgreSQLBuildingDataExternalComponentsUpdateTask));
                return false;
            }

            HashSet<int>? countyIds = PostgreSQLBuildingDataExternalComponentsUpdateOptions.CountyIds;

            Serilog.Modify.Log(
                "{Type}: starting over {CountyCount} counties, scope {CountyScope}",
                nameof(PostgreSQLBuildingDataExternalComponentsUpdateTask),
                countyReferences.Count,
                countyIds is null ? "all" : string.Join(", ", countyIds));

            const int batchSize = 1000;

            foreach (AdministrativeAreal2DReference? countyReference in countyReferences)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (countyReference is null)
                {
                    continue;
                }

                // Id is the county row identifier - the key building_model_component is filed under - never the code,
                // which names several rows for a multi-part county.
                int countyId = countyReference.Id;

                if (countyIds is not null && !countyIds.Contains(countyId))
                {
                    continue;
                }

                bool countyFailed = false;

                List<BuildingModel> models_Deduped = [];

                List<string> references_List = [];
                try
                {
                    HashSet<string>? references = await buildingModelPostgreSQLConverter.GetReferencesAsync(countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    if (references is not null)
                    {
                        references_List = [.. references];
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    countyFailed = true;
                    Serilog.Modify.Log(exception, "External components county failed - county {CountyId}, the stored model references could not be read", countyId);
                }

                if (references_List.Count == 0 && !countyFailed)
                {
                    // No stored models in this county: nothing to classify, and that is not a failure.
                    ProcessedCountyCount++;
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Information, "External components county processed - county {CountyId} carries no stored building models", countyId);
                    continue;
                }

                if (references_List.Count > 0)
                {
                    for (int offset = 0; offset < references_List.Count && !countyFailed; offset += batchSize)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        List<string> references_Batch = references_List.GetRange(offset, Math.Min(batchSize, references_List.Count - offset));

                        List<BuildingModel>? models_Batch = null;
                        try
                        {
                            // No fallback by reference: a reference the county does not hold is answered out of some
                            // other county, and the record that comes back carries that county's identifier. Writing
                            // it would file a building data row under a county this run is not processing.
                            models_Batch = await buildingModelPostgreSQLConverter.GetItemsByReferencesAsync(references_Batch, countyId, fallbackByReference: false, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                            if (models_Batch is null)
                            {
                                countyFailed = true;
                                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "External components county failed - county {CountyId}, the read of the {ReferenceCount} references came back empty", countyId, references_Batch.Count);
                            }
                        }
                        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                        {
                            throw;
                        }
                        catch (Exception exception)
                        {
                            countyFailed = true;
                            Serilog.Modify.Log(exception, "External components county failed - county {CountyId}, the stored models of {ReferenceCount} references could not be read", countyId, references_Batch.Count);
                        }

                        if (countyFailed || models_Batch is null)
                        {
                            break;
                        }

                        models_Deduped.AddRange(models_Batch);
                    }
                }

                if (countyFailed)
                {
                    FailedCountyCount++;
                    continue;
                }

                // The read is ordered created_at DESC, id DESC, so the first record of a reference is the latest stored version.
                HashSet<string> references_Written = [];
                List<BuildingModel> models_Latest = [];
                foreach (BuildingModel? model in models_Deduped)
                {
                    if (model is null)
                    {
                        continue;
                    }

                    if (model.Reference is string reference_Model && !string.IsNullOrWhiteSpace(reference_Model) && references_Written.Add(reference_Model))
                    {
                        models_Latest.Add(model);
                    }
                }

                Table table = new();

                long countySkippedComponentCount = 0;
                try
                {
                    countySkippedComponentCount = Modify.Update_ExternalComponentsArea(table, models_Latest);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    // A model the classification refuses is a defect in the stored data, and the method names the
                    // building in the exception. The county fails visibly instead of writing partial rows.
                    FailedCountyCount++;
                    Serilog.Modify.Log(exception, "External components county failed - county {CountyId}, the {ModelCount} stored models could not be classified", countyId, models_Latest.Count);
                    continue;
                }

                if (table.RowCount == 0)
                {
                    ProcessedCountyCount++;
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Information, "External components county processed - county {CountyId}, {ModelCount} models read, none of them produced a row", countyId, models_Latest.Count);
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
                    FailedCountyCount++;
                    Serilog.Modify.Log(exception, "External components county failed - county {CountyId}, the {RowCount} rows built could not be written", countyId, table.RowCount);
                    continue;
                }

                if (!updated)
                {
                    FailedCountyCount++;
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "External components county failed - county {CountyId}, the write of {RowCount} rows was rolled back", countyId, table.RowCount);
                    continue;
                }

                SkippedComponentCount += countySkippedComponentCount;
                ProcessedModelCount += models_Latest.Count;
                UpdatedRowCount += table.RowCount;
                ProcessedCountyCount++;
                progress.Report(UpdatedRowCount);

                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Information, "External components county processed - county {CountyId}, {ModelCount} models, {RowCount} rows, {SkippedCount} components skipped", countyId, models_Latest.Count, table.RowCount, countySkippedComponentCount);
            }

            Serilog.Modify.Log(
                "{Type}: finished - {ProcessedCount} counties, {FailedCount} counties failed, {ModelCount} models, {SkippedCount} components skipped, {RowCount} rows",
                nameof(PostgreSQLBuildingDataExternalComponentsUpdateTask),
                ProcessedCountyCount,
                FailedCountyCount,
                ProcessedModelCount,
                SkippedComponentCount,
                UpdatedRowCount);

            return FailedCountyCount == 0;
        }
    }
}
