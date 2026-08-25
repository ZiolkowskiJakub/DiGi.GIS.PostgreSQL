using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that queues the orthophoto downloads a county is short of.
    /// <para>The task stores no orthophoto data itself. It reads each county's building references, drops the ones already stored, and appends the rest to <see cref="Constants.TableName.OrtoDatas_Building2DReference_Update"/> - the queue the download task drains. What a run leaves behind is therefore scheduled work, and the orthophotos appear only as that queue is worked through.</para>
    /// <para>With <see cref="PostgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds"/> set, which is the default, a run also pushes each building's own subdivision identifier onto its stored row.</para>
    /// <para>A county that fails is logged and stepped over rather than ending the run, so <see cref="BackgroundTask.IsSucceeded"/> alone does not say a run did everything it set out to do. <see cref="FailedCountyCount"/> is what tells the two apart.</para>
    /// </summary>
    public class PostgreSQLOrtoDatasRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// The converter manager the run draws its converters from.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The GIS PostgreSQL converter manager used to refresh the data.</param>
        public PostgreSQLOrtoDatasRefreshTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Gets the number of references the queue accepted during the last run.
        /// <para>A reference already waiting in the queue conflicts and is not counted, so a run repeated straight away reports far fewer than the first.</para>
        /// </summary>
        public long EnqueuedCount { get; private set; }

        /// <summary>
        /// Gets the number of counties that failed outright and were stepped over during the last run.
        /// <para>Each one is logged with the exception that caused it, so this figure is a count of entries to go and read rather than the whole of what is known.</para>
        /// </summary>
        public long FailedCountyCount { get; private set; }

        /// <summary>
        /// Gets the configuration for the PostgreSQL operation.
        /// These options will be used when the task is started.
        /// </summary>
        public PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions { get; set; } = new PostgreSQLOrtoDatasRefreshOptions();

        /// <summary>
        /// Gets the number of building references read out of the building table during the last run.
        /// </summary>
        public long ReadCount { get; private set; }

        /// <summary>
        /// Gets the number of stored rows that had a subdivision identifier written to them during the last run.
        /// </summary>
        public long SubdivisionIdCount { get; private set; }

        /// <summary>
        /// Executes the background task, queuing the orthophoto downloads each county is short of.
        /// </summary>
        /// <param name="progress">A progress reporter carrying the running total of references the queue has accepted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true unless the run could not be attempted or was cancelled; a county stepped over does not make it false, so read <see cref="FailedCountyCount"/> as well.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            EnqueuedCount = 0;
            FailedCountyCount = 0;
            ReadCount = 0;
            SubdivisionIdCount = 0;

            PostgreSQLOrtoDatasRefreshResult? postgreSQLOrtoDatasRefreshResult = await gISPostgreSQLConverterManager.RefreshOrtoDatasAsync(PostgreSQLOrtoDatasRefreshOptions, progress, cancellationToken);
            if (postgreSQLOrtoDatasRefreshResult is null)
            {
                return false;
            }

            EnqueuedCount = postgreSQLOrtoDatasRefreshResult.EnqueuedCount;
            FailedCountyCount = postgreSQLOrtoDatasRefreshResult.FailedCountyCount;
            ReadCount = postgreSQLOrtoDatasRefreshResult.ReadCount;
            SubdivisionIdCount = postgreSQLOrtoDatasRefreshResult.SubdivisionIdCount;

            return !postgreSQLOrtoDatasRefreshResult.Cancelled;
        }
    }
}
