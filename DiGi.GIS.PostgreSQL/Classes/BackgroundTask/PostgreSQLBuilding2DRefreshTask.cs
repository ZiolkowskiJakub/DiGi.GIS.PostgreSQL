using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that refreshes the PostgreSQL data for Building2D.
    /// </summary>
    public class PostgreSQLBuilding2DRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the Building2D PostgreSQL converter used to refresh the data.
        /// </summary>
        private readonly Building2DPostgreSQLConverter building2DPostgreSQLConverter;

        /// <summary>
        /// Gets the configuration for the PostgreSQL operation.
        /// These options will be used when RunAsync is triggered.
        /// </summary>
        public PostgreSQLBuilding2DRefreshOptions PostgreSQLBuilding2DRefreshOptions { get; set; } = new PostgreSQLBuilding2DRefreshOptions() { OverrideExistingSubdivisionIds = false };

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="building2DPostgreSQLConverter">The Building2D PostgreSQL converter used to refresh the data.</param>
        public PostgreSQLBuilding2DRefreshTask(Building2DPostgreSQLConverter building2DPostgreSQLConverter)
        {
            this.building2DPostgreSQLConverter = building2DPostgreSQLConverter ?? throw new ArgumentNullException(nameof(building2DPostgreSQLConverter));
        }

        /// <summary>
        /// Executes the background task to refresh the PostgreSQL data for Building2D.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of processed items.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the refresh was successful; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            // We pass the token to your C# DLL to ensure the
            // PostgreSQL command can be aborted mid-execution.
            return await building2DPostgreSQLConverter.RefreshAsync(PostgreSQLBuilding2DRefreshOptions, progress, cancellationToken);
        }
    }
}