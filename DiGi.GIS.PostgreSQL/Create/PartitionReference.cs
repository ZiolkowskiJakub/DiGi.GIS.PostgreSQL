using DiGi.GIS.Classes;
using DiGi.PostgreSQL.PartitionReference.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        public static PartitionReference? PartitionReference(this GISModel? gISModel)
        {
            return PartitionReference_GISModel(gISModel?.Reference);
        }

        public static PartitionReference? PartitionReference(this OrtoDatas? ortoDatas, string? name)
        {
            if (ortoDatas is null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            if (ortoDatas.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return new PartitionReference(name!, reference);
        }

        public static PartitionReference? PartitionReference(this GISModelAreal2DReference? gISModelAreal2DReference)
        {
            if (gISModelAreal2DReference is null)
            {
                return null;
            }

            return new PartitionReference(gISModelAreal2DReference.GISModelReference, gISModelAreal2DReference.Areal2DReference);
        }

        public static PartitionReference? PartitionReference_GISModel(string? reference)
        {
            if(string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            return new PartitionReference(typeof(GISModel).Name, reference);
        }
    }
}