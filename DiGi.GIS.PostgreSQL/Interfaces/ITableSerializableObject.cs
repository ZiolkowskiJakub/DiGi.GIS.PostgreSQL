using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Interfaces
{
    public interface ITableSerializableObject : ITableObject, IGISPostgreSQLSerializableObject
    {
        JsonObject? Object { get; set; }
    }

    public interface ITableSerializableObject<TGISSerializableObject> : ITableSerializableObject where TGISSerializableObject : IGISSerializableObject
    {
        TGISSerializableObject? ToDiGi();
    }
}