using DiGi.Core;
using DiGi.Core.IO.Table.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using DiGi.PostgreSQL.Table.Classes;
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
    public class Building2DTablePostgreSQLConverter : TablePostgreSQLConverter<Column>, IGISPostgreSQLConverter
    {
        public Building2DTablePostgreSQLConverter(ConnectionData? connectionData) 
            : base(connectionData)
        {
        }

        public override string TableName => "building_2d_data";

        protected override TableConversionOptions<Column>? TableConversionOptions => new () 
        {
            PrimaryKeyColumns = [IO.Constants.Column.CountyId, IO.Constants.Column.Reference],
            PartitioningOptions = new PartitioningOptions<Column>()
            {
                Column = IO.Constants.Column.CountyId,
                PartitioningRule = new ValuePartitioningRule()
            }
        };
    }
}