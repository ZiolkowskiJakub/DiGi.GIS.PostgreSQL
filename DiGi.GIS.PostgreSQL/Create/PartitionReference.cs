using DiGi.GIS.Classes;
using DiGi.PostgreSQL.PartitionReference.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        public static PartitionReference? PartitionReference(this GISModel? gISModel)
        {
            if (gISModel?.Reference is not string reference)
            {
                return null;
            }

            return new PartitionReference(typeof(GISModel).Name, reference);
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
    }
}