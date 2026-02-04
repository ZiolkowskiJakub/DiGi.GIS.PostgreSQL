using DiGi.GIS.Classes;
using DiGi.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class OrtoDatasPostgreSQLConverter : GISPartitionReferencePostgreSQLConverter<OrtoDatas>
    {
        private readonly Dictionary<string, string> dictionary = [];

        public OrtoDatasPostgreSQLConverter(ConnectionData connectionData)
            : base(connectionData)
        {
            PartitionReferenceGenerating += OrtoDatasPostgreSQLConverter_PartitionReferenceGenerating;
        }

        private void OrtoDatasPostgreSQLConverter_PartitionReferenceGenerating(object sender, DiGi.PostgreSQL.PartitionReference.Classes.PartitionReferenceGeneratingEventArgs e)
        {
            if (e.Item is not OrtoDatas ortoDatas)
            {
                return;
            }

            if (ortoDatas.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
            {
                return;
            }

            if (!dictionary.TryGetValue(reference, out string? name) || string.IsNullOrWhiteSpace(name))
            {
                name = Constans.Name.Partition.Unassigned;
            }

            e.PartitionReference = Create.PartitionReference(ortoDatas, name);
        }

        public bool SetName(OrtoDatas? ortoDatas, string? name)
        {
            if (ortoDatas?.Reference is not string reference || string.IsNullOrWhiteSpace(reference) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            dictionary[reference] = name!;
            return true;
        }
    }
}