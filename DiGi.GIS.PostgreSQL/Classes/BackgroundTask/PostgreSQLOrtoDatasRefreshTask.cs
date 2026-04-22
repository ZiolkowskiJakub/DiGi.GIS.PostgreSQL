using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLOrtoDatasRefreshTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        public PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions { get; set; } = new PostgreSQLOrtoDatasRefreshOptions();

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        public PostgreSQLOrtoDatasRefreshTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            return await Modify.RefreshOrtoDatas(gISPostgreSQLConverterManager, PostgreSQLOrtoDatasRefreshOptions, progress, cancellationToken);
        }
    }
}