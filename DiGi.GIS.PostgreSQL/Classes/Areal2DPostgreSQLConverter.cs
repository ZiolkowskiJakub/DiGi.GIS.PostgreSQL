using DiGi.GIS.Classes;
using DiGi.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Areal2DPostgreSQLConverter : GISPartitionReferencePostgreSQLConverter<Areal2D>
    {
        private readonly Dictionary<string, string> dictionary = [];

        public Areal2DPostgreSQLConverter(ConnectionData connectionData)
            : base(connectionData)
        {
            PartitionReferenceGenerating += Areal2DPostgreSQLConverter_PartitionReferenceGenerating;
        }

        public bool SetName(Areal2D? areal2D, string? name)
        {
            if (areal2D?.Reference is not string reference || string.IsNullOrWhiteSpace(reference) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            dictionary[reference] = name;
            return true;
        }

        private void Areal2DPostgreSQLConverter_PartitionReferenceGenerating(object sender, DiGi.PostgreSQL.PartitionReference.Classes.PartitionReferenceGeneratingEventArgs e)
        {
            if (e.Item is not Areal2D areal2D)
            {
                return;
            }

            if (areal2D.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
            {
                return;
            }

            if (!dictionary.TryGetValue(reference, out string? name) || string.IsNullOrWhiteSpace(name))
            {
                name = Constants.Name.Partition.Unassigned;
            }

            e.PartitionReference = Create.PartitionReference(areal2D, name);
        }
    }
}