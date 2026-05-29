using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that creates the PostgreSQL database for AdministrativeAreal2D.
    /// </summary>
    public class PostgreSQLAdministrativeAreal2DCreateDatabaseTask : BackgroundTask, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the GIS PostgreSQL converter manager used to create the database.
        /// </summary>
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        public PostgreSQLAdministrativeAreal2DCreateDatabaseTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Executes the background task to create the PostgreSQL database for AdministrativeAreal2D.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns true if the database was created successfully; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync()
        {
            return await gISPostgreSQLConverterManager.TryCreateDatabase<AdministrativeAreal2DPostgreSQLConverter>();
        }
    }
}