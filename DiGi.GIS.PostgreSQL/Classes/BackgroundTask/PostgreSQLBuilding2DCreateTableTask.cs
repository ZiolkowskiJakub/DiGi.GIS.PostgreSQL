using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that creates the PostgreSQL table for Building2D.
    /// </summary>
    public class PostgreSQLBuilding2DCreateTableTask : BackgroundTask, IGISPostgreSQLObject
    {
        /// <summary>
        /// Gets the Building2D PostgreSQL converter used to create the table.
        /// </summary>
        private readonly Building2DPostgreSQLConverter building2DPostgreSQLConverter;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        /// <param name="building2DPostgreSQLConverter">The Building2D PostgreSQL converter used to create the table.</param>
        public PostgreSQLBuilding2DCreateTableTask(Building2DPostgreSQLConverter building2DPostgreSQLConverter)
        {
            this.building2DPostgreSQLConverter = building2DPostgreSQLConverter ?? throw new ArgumentNullException(nameof(building2DPostgreSQLConverter));
        }

        /// <summary>
        /// Executes the background task to create the PostgreSQL table for Building2D.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns true if the table was created successfully; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync()
        {
            // We pass the token to your C# DLL to ensure the
            // PostgreSQL command can be aborted mid-execution.
            return await building2DPostgreSQLConverter.CreateTableAsync(0);
        }
    }
}