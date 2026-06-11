using DiGi.GIS.PostgreSQL.Interfaces;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Manages the conversion processes specifically for GIS data within a PostgreSQL database context.
    /// </summary>
    public class GISPostgreSQLConverterManager : DiGi.PostgreSQL.Classes.PostgreSQLConverterManager<IGISPostgreSQLConverter>
    {
    }
}