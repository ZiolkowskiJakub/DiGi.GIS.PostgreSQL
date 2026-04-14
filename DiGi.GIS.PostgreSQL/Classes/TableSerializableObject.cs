using DiGi.Core.Classes;
using DiGi.GIS.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class TableSerializableObject<TGISSerializableObject> : SerializableObject, ITableSerializableObject<TGISSerializableObject> where TGISSerializableObject : IGISSerializableObject
    {
        public TableSerializableObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public TableSerializableObject(TableSerializableObject<TGISSerializableObject>? tableSerializableObject)
            : base(tableSerializableObject)
        {
            if (tableSerializableObject is not null)
            {
                Object = tableSerializableObject.Object;
            }
        }

        public TableSerializableObject()
            : base()
        {
        }

        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        public TGISSerializableObject? ToDiGi()
        {
            if (Object is not JsonObject jsonObject)
            {
                return default;
            }

            return Core.Create.SerializableObject<TGISSerializableObject>(jsonObject);
        }
    }
}