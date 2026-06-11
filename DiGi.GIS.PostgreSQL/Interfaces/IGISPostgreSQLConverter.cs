namespace DiGi.GIS.PostgreSQL.Interfaces
{
    /// <summary>
    /// Defines the contract for a GIS-specific PostgreSQL converter.
    /// </summary>
    public interface IGISPostgreSQLConverter : DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, IGISPostgreSQLObject
    {
    }

    /// <summary>
    /// Defines the contract for a generic GIS-specific PostgreSQL converter for a specific table object type.
    /// </summary>
    /// <typeparam name="TTableObject">The type of the table object that this converter handles.</typeparam>
    public interface IGISPostgreSQLConverter<TTableObject> : IGISPostgreSQLConverter where TTableObject : ITableObject
    {
    }
}