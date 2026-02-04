using DiGi.GIS.Classes;
using DiGi.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class GISModelPostgreSQLConverter : GISPartitionReferencePostgreSQLConverter<GISModel>
    {
        public GISModelPostgreSQLConverter(ConnectionData connectionData)
            : base(connectionData)
        {
            PartitionReferenceGenerating += GISModelPostgreSQLConverter_PartitionReferenceGenerating;
        }

        private void GISModelPostgreSQLConverter_PartitionReferenceGenerating(object sender, DiGi.PostgreSQL.PartitionReference.Classes.PartitionReferenceGeneratingEventArgs e)
        {
            if (e.Item is not GISModel gISModel)
            {
                return;
            }

            if (gISModel.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
            {
                return;
            }

            e.PartitionReference = Create.PartitionReference(gISModel);
        }
    }
}