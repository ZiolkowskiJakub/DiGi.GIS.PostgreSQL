using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DOrtoDatasPostgreSQLConverter : PostgreSQLConverter<Building2DOrtoDatas>, IGISPostgreSQLConverter<Building2DOrtoDatas>
    {
        private ConnectionData? connectionData_Main;

        public Building2DOrtoDatasPostgreSQLConverter(ConnectionData? connectionData_Storage, ConnectionData? connectionData_Main)
            : base(connectionData_Storage)
        {
            this.connectionData_Main = connectionData_Main;
        }

        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<Building2DOrtoDatas>? building2DOrtoDatas, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            throw new NotImplementedException();
        }

    }
}