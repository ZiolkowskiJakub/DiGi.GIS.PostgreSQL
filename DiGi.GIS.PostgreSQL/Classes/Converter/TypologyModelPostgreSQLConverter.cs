using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class TypologyModelPostgreSQLConverter : AdministrativeAreal2DReferencedObjectPostgreSQLConverter<TypologyModel, Typology.Classes.TypologyModel>
    {
        public TypologyModelPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => Constants.TableName.TypologyModel;

        protected override TypologyModel Create(int id, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new TypologyModel()
            {
                Id = id,
                UniqueId = uniqueId,
                Reference = reference,
                Object = @object,
                CreatedAt = createdAt
            };
        }
    }
}