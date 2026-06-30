using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that creates the PostgreSQL table for AdministrativeAreal2D.
    /// </summary>
    public class PostgreSQLAdministrativeAreal2DCreateTableTask : BackgroundTask, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the AdministrativeAreal2D PostgreSQL converter used to create the table.
        /// </summary>
        private readonly AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The AdministrativeAreal2D PostgreSQL converter used to create the table.</param>
        public PostgreSQLAdministrativeAreal2DCreateTableTask(AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter)
        {
            this.administrativeAreal2DPostgreSQLConverter = administrativeAreal2DPostgreSQLConverter ?? throw new ArgumentNullException(nameof(administrativeAreal2DPostgreSQLConverter));
        }

        /// <summary>
        /// Executes the background task to create the PostgreSQL table for AdministrativeAreal2D.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns true if the table was created successfully; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync()
        {
            return await administrativeAreal2DPostgreSQLConverter.CreateTableAsync(600);
        }
    }
}