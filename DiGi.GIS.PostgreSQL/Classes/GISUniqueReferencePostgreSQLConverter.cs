using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class GISUniqueReferencePostgreSQLConverter<TGISSerializableObject> : DiGi.PostgreSQL.UniqueReference.Classes.UniqueReferencePostgreSQLConverter<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public GISUniqueReferencePostgreSQLConverter(ConnectionData connectionData)
            : base(connectionData)
        {
        }
    }
}