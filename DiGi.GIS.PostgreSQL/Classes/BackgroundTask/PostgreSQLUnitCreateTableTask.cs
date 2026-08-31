using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a background task that creates the PostgreSQL table and supporting indexes for territorial units.
    /// </summary>
    public class PostgreSQLUnitCreateTableTask : BackgroundTask, IGISPostgreSQLObject
    {
        private readonly UnitPostgreSQLConverter unitPostgreSQLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUnitCreateTableTask"/> class.
        /// </summary>
        /// <param name="unitPostgreSQLConverter">The unit PostgreSQL converter used to create the table.</param>
        public PostgreSQLUnitCreateTableTask(UnitPostgreSQLConverter unitPostgreSQLConverter)
        {
            this.unitPostgreSQLConverter = unitPostgreSQLConverter ?? throw new ArgumentNullException(nameof(unitPostgreSQLConverter));
        }

        /// <summary>
        /// Executes the background task to create the unit table and indexes in PostgreSQL.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns true if the table was created successfully; otherwise, false.</returns>
        protected override async Task<bool> ExecuteAsync()
        {
            return await unitPostgreSQLConverter.CreateTableAsync(600);
        }
    }
}
