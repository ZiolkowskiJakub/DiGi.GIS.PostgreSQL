using DiGi.GIS.PostgreSQL.Classes;
using DiGi.PostgreSQL.Classes;
using System.IO;
using System.Reflection;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        public static GISPostgreSQLConverterManager? GISPostgreSQLConverterManager()
        {
            string? directory_ExecutingAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(directory_ExecutingAssembly) || !Directory.Exists(directory_ExecutingAssembly))
            {
                return null;
            }

            GISPostgreSQLConverterManager result = new();

            string path;

            path = Path.Combine(directory_ExecutingAssembly, Constants.FileName.PostgreSQL_Main);
            if (!string.IsNullOrWhiteSpace(path) && Path.Exists(path) && DiGi.PostgreSQL.Create.PostgreSQLConfigurationFile(path) is PostgreSQLConfigurationFile postgreSQLConfigurationFile_Main)
            {
                ConnectionData? connectionData = DiGi.PostgreSQL.Create.ConnectionData(postgreSQLConfigurationFile_Main);
                if (connectionData is not null)
                {
                    result.Add(new AdministrativeAreal2DPostgreSQLConverter(connectionData), postgreSQLConfigurationFile_Main);
                    result.Add(new Building2DPostgreSQLConverter(connectionData), postgreSQLConfigurationFile_Main);
                    result.Add(new YearBuiltDataPostgreSQLConverter(connectionData), postgreSQLConfigurationFile_Main);
                    result.Add(new Building2DOccupancyDataPostgreSQLConverter(connectionData), postgreSQLConfigurationFile_Main);
                    result.Add(new AdministrativeAreal2DOccupancyDataPostgreSQLConverter(connectionData), postgreSQLConfigurationFile_Main);
                }
            }

            path = Path.Combine(directory_ExecutingAssembly, Constants.FileName.PostgreSQL_Storage);
            if (!string.IsNullOrWhiteSpace(path) && Path.Exists(path) && DiGi.PostgreSQL.Create.PostgreSQLConfigurationFile(path) is PostgreSQLConfigurationFile postgreSQLConfigurationFile_Storage)
            {
                ConnectionData? connectionData = DiGi.PostgreSQL.Create.ConnectionData(postgreSQLConfigurationFile_Storage);
                if (connectionData is not null)
                {
                    result.Add(new OrtoDatasPostgreSQLConverter(connectionData), postgreSQLConfigurationFile_Storage);
                }
            }

            return result;
        }
    }
}