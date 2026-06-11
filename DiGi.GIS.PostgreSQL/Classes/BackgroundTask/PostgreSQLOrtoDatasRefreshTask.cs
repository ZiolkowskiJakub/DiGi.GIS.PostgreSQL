using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that refreshes the PostgreSQL data for OrtoDatas.
    /// </summary>
    public class PostgreSQLOrtoDatasRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the GIS PostgreSQL converter manager used to refresh the data.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Gets the configuration for the PostgreSQL operation.
        /// These options will be used when RunAsync is triggered.
        /// </summary>
        public PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions { get; set; } = new PostgreSQLOrtoDatasRefreshOptions();

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="gISPostgreSQLConverterManager">The GIS PostgreSQL converter manager used to refresh the data.</param>
        public PostgreSQLOrtoDatasRefreshTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Executes the background task to refresh the PostgreSQL data for OrtoDatas.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of processed items.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the refresh was successful; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            return await Modify.RefreshOrtoDatas(gISPostgreSQLConverterManager, PostgreSQLOrtoDatasRefreshOptions, progress, cancellationToken);
        }
    }
}