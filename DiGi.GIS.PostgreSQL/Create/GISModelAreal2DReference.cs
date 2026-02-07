using DiGi.GIS.Classes;
using DiGi.PostgreSQL.PartitionReference.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        public static GISModelAreal2DReference? GISModelAreal2DReference(this PartitionReference? partitionReference)
        {
            if (partitionReference is null)
            {
                return null;
            }

            return new GISModelAreal2DReference(partitionReference.Name, partitionReference.UniqueId);
        }
    }
}