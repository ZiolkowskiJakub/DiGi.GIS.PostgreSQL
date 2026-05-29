using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract base class for table-serializable objects, providing conversion between JSON and GIS serializable objects.
    /// </summary>
    /// <typeparam name="TSerializableObject">The type of the GIS serializable object.</typeparam>
    public abstract class TableSerializableObject<TSerializableObject> : SerializableObject, ITableSerializableObject<TSerializableObject> where TSerializableObject : ISerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the TableSerializableObject class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public TableSerializableObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TableSerializableObject class by copying data from another TableSerializableObject instance.
        /// </summary>
        /// <param name="tableSerializableObject">The TableSerializableObject instance to copy data from.</param>
        public TableSerializableObject(TableSerializableObject<TSerializableObject>? tableSerializableObject)
            : base(tableSerializableObject)
        {
            if (tableSerializableObject is not null)
            {
                Object = tableSerializableObject.Object;
            }
        }

        /// <summary>
        /// Initializes a new instance of the TableSerializableObject class.
        /// </summary>
        public TableSerializableObject()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the JSON object containing the serialized GIS serializable object data.
        /// </summary>
        [JsonInclude, JsonPropertyName("Object")]
        public JsonObject? Object { get; set; }

        /// <summary>
        /// Converts the serialized Object property to a DiGi GIS serializable object.
        /// </summary>
        /// <returns>The deserialized TSerializableObject, or default if conversion fails.</returns>
        public TSerializableObject? ToDiGi()
        {
            if (Object is not JsonObject jsonObject)
            {
                return default;
            }

            return Core.Create.SerializableObject<TSerializableObject>(jsonObject);
        }

        /// <summary>
        /// Sets the Object property from a DiGi GIS serializable object.
        /// </summary>
        /// <param name="gISSerializableObject">The GIS serializable object to serialize.</param>
        public void FromDiGi(TSerializableObject? gISSerializableObject)
        {
            Object = gISSerializableObject?.ToJsonObject();
        }
    }
}