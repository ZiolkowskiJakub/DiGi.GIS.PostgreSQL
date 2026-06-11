using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that refreshes the PostgreSQL data for AdministrativeAreal2D.
    /// </summary>
    public class PostgreSQLAdministrativeAreal2DRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the AdministrativeAreal2D PostgreSQL converter used to refresh the data.
        /// </summary>
        private readonly AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter;

        /// <summary>
        /// Gets the configuration for the PostgreSQL operation.
        /// These options will be used when RunAsync is triggered.
        /// </summary>
        public PostgreSQLAdministrativeAreal2DRefreshOptions PostgreSQLAdministrativeAreal2DRefreshOptions { get; set; } = new PostgreSQLAdministrativeAreal2DRefreshOptions();

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The AdministrativeAreal2D PostgreSQL converter used to refresh the data.</param>
        public PostgreSQLAdministrativeAreal2DRefreshTask(AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter)
        {
            this.administrativeAreal2DPostgreSQLConverter = administrativeAreal2DPostgreSQLConverter ?? throw new ArgumentNullException(nameof(administrativeAreal2DPostgreSQLConverter));
        }

        /// <summary>
        /// Executes the background task to refresh the PostgreSQL data for AdministrativeAreal2D.
        /// </summary>
        /// <param name="progress">A progress reporter for reporting the number of processed items.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the refresh was successful; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            // We pass the token to your C# DLL to ensure the
            // PostgreSQL command can be aborted mid-execution.
            return await administrativeAreal2DPostgreSQLConverter.RefreshAsync(PostgreSQLAdministrativeAreal2DRefreshOptions, progress, cancellationToken);
        }
    }
}