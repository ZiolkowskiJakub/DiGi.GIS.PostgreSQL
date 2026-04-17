using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public abstract class TableSerializableObject<TSerializableObject> : SerializableObject, ITableSerializableObject<TSerializableObject> where TSerializableObject : ISerializableObject
    {
        public TableSerializableObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public TableSerializableObject(TableSerializableObject<TSerializableObject>? tableSerializableObject)
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

        public TSerializableObject? ToDiGi()
        {
            if (Object is not JsonObject jsonObject)
            {
                return default;
            }

            return Core.Create.SerializableObject<TSerializableObject>(jsonObject);
        }

        public void FromDiGi(TSerializableObject? gISSerializableObject)
        {
            Object = gISSerializableObject?.ToJsonObject();
        }
    }
}