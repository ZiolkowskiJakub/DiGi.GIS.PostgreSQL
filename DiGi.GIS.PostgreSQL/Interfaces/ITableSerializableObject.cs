using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Interfaces
{
    public interface ITableSerializableObject : ITableObject, IGISPostgreSQLSerializableObject
    {
        JsonObject? Object { get; set; }
    }

    public interface ITableSerializableObject<TSerializableObject> : ITableSerializableObject where TSerializableObject : ISerializableObject
    {
        TSerializableObject? ToDiGi();
    }
}