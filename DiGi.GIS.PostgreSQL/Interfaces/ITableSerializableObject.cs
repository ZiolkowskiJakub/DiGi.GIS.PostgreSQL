using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Interfaces
{
    public interface ITableSerializableObject : ITableObject, IGISPostgreSQLSerializableObject
    {
        JsonObject? Object { get; set; }
    }
}