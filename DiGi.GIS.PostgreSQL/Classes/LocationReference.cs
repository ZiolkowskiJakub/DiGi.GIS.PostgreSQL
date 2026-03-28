using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class LocationReference : SerializableObject
    {
        public LocationReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public LocationReference(LocationReference? locationReference)
            : base(locationReference)
        {
            if (locationReference is not null)
            {
                CountyId = locationReference.CountyId;
                Id = locationReference.Id;
                Reference = locationReference.Reference;
                SubdivisionId = locationReference.SubdivisionId;
            }
        }

        public LocationReference()
        {
        }

        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonInclude, JsonPropertyName("Reference")]
        public string? Reference { get; set; }

        [JsonInclude, JsonPropertyName("SubdivisionId")]
        public int? SubdivisionId { get; set; }
    }
}