using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLBuilding2DRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        private readonly Building2DPostgreSQLConverter building2DPostgreSQLConverter;

        /// <summary>
        /// Configuration for the PostgreSQL operation.
        /// These options will be used when RunAsync is triggered.
        /// </summary>
        public Building2DPostgreSQLRefreshOptions Building2DPostgreSQLRefreshOptions { get; set; } = new Building2DPostgreSQLRefreshOptions() { OverrideExistingSubdivisionIds = true };

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        public PostgreSQLBuilding2DRefreshTask(Building2DPostgreSQLConverter building2DPostgreSQLConverter)
        {
            this.building2DPostgreSQLConverter = building2DPostgreSQLConverter ?? throw new ArgumentNullException(nameof(building2DPostgreSQLConverter));
        }

        /// <summary>
        /// Concrete implementation of the background work.
        /// </summary>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            // We pass the token to your C# DLL to ensure the
            // PostgreSQL command can be aborted mid-execution.
            return await building2DPostgreSQLConverter.RefreshAsync(Building2DPostgreSQLRefreshOptions, progress, cancellationToken);
        }
    }
}