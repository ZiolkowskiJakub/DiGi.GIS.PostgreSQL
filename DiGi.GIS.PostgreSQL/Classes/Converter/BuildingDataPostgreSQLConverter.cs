using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using DiGi.PostgreSQL.Table.Classes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class BuildingDataPostgreSQLConverter : TablePostgreSQLConverter<Column>, IGISPostgreSQLConverter
    {
        public BuildingDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => Constants.TableName.BuildingData;

        protected override TableConversionOptions<Column>? TableConversionOptions => new()
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