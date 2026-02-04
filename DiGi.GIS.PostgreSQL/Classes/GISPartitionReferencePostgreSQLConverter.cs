using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class GISPartitionReferencePostgreSQLConverter<TGISSerializableObject> : DiGi.PostgreSQL.PartitionReference.Classes.PartitionReferencePostgreSQLConverter<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public GISPartitionReferencePostgreSQLConverter(ConnectionData connectionData)
            : base(connectionData)
        {
        }
    }
}