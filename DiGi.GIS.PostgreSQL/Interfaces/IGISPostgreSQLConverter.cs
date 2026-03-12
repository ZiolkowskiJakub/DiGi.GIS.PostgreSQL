namespace DiGi.GIS.PostgreSQL.Interfaces
{
    public interface IGISPostgreSQLConverter : DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, IGISPostgreSQLObject
    {
    }

    public interface IGISPostgreSQLConverter<TTableObject> : IGISPostgreSQLConverter where TTableObject : ITableObject
    {
    }
}