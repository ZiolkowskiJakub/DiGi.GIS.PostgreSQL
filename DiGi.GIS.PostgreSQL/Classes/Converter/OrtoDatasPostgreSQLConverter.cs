using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class OrtoDatasPostgreSQLConverter : PostgreSQLConverter<OrtoDatas>, IGISPostgreSQLConverter<OrtoDatas>
    {
        private ConnectionData? connectionData_Main;

        public OrtoDatasPostgreSQLConverter(ConnectionData? connectionData_Storage, ConnectionData? connectionData_Main)
            : base(connectionData_Storage)
        {
            this.connectionData_Main = connectionData_Main;
        }

        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<OrtoDatas>? ortoDatas, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            throw new NotImplementedException();
        }

    }
}