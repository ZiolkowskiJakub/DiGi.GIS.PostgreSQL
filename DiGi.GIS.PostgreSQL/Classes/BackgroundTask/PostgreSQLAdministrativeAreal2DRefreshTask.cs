using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLAdministrativeAreal2DRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        private readonly AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter;

        /// <summary>
        /// Configuration for the PostgreSQL operation.
        /// These options will be used when RunAsync is triggered.
        /// </summary>
        public AdministrativeAreal2DPostgreSQLRefreshOptions AdministrativeAreal2DPostgreSQLRefreshOptions { get; set; } = new AdministrativeAreal2DPostgreSQLRefreshOptions();

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        public PostgreSQLAdministrativeAreal2DRefreshTask(AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter)
        {
            this.administrativeAreal2DPostgreSQLConverter = administrativeAreal2DPostgreSQLConverter ?? throw new ArgumentNullException(nameof(administrativeAreal2DPostgreSQLConverter));
        }

        /// <summary>
        /// Concrete implementation of the background work.
        /// </summary>
        protected override async Task<bool> RunAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            // We pass the token to your C# DLL to ensure the
            // PostgreSQL command can be aborted mid-execution.
            return await administrativeAreal2DPostgreSQLConverter.RefreshAsync(AdministrativeAreal2DPostgreSQLRefreshOptions, progress, cancellationToken);
        }
    }
}