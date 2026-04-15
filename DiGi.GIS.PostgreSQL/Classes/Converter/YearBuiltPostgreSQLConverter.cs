using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using System;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class YearBuiltPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<YearBuilt, IYearBuilt>
    {
        public YearBuiltPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public override string TableName => "year_built";

        protected override YearBuilt Create(long id, int? countyId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new YearBuilt()
            {
                Id = id,
                CountyId = countyId,
                Reference = reference,
                Object = @object,
                CreatedAt = createdAt
            };
        }
    }
}