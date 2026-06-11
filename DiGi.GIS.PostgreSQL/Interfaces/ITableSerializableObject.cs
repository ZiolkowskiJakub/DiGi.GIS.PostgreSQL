using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Interfaces
{
    /// <summary>
    /// Defines an object that can be serialized as part of a database table.
    /// </summary>
    public interface ITableSerializableObject : ITableObject, IGISPostgreSQLSerializableObject
    {
        /// <summary>
        /// Gets or sets the JSON representation of the object.
        /// </summary>
        JsonObject? Object { get; set; }
    }

    /// <summary>
    /// Defines a generic object that can be serialized as part of a database table and converted to a specific serializable object type.
    /// </summary>
    /// <typeparam name="TSerializableObject">The type of the serializable object this interface converts to.</typeparam>
    public interface ITableSerializableObject<TSerializableObject> : ITableSerializableObject where TSerializableObject : ISerializableObject
    {
        /// <summary>
        /// Converts the current instance to a DiGi serializable object.
        /// </summary>
        /// <returns>A <typeparamref name="TSerializableObject" /> representation of the object, or null if conversion fails.</returns>
        TSerializableObject? ToDiGi();
    }
}