using DiGi.Core.Interfaces;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a collection of table-serializable objects (database row envelopes) to the DiGi serializable objects they wrap, skipping entries that cannot be converted.
        /// </summary>
        /// <typeparam name="TSerializableObject">The type of the wrapped DiGi serializable object.</typeparam>
        /// <param name="tableSerializableObjects">The collection of <see cref="ITableSerializableObject{TSerializableObject}"/> instances to convert. This value can be null.</param>
        /// <returns>A list of <typeparamref name="TSerializableObject"/> instances, or null if <paramref name="tableSerializableObjects"/> is null.</returns>
        public static List<TSerializableObject>? ToDiGi<TSerializableObject>(this IEnumerable<ITableSerializableObject<TSerializableObject>>? tableSerializableObjects) where TSerializableObject : ISerializableObject
        {
            if (tableSerializableObjects is null)
            {
                return null;
            }

            List<TSerializableObject> result = [];
            foreach (ITableSerializableObject<TSerializableObject> tableSerializableObject in tableSerializableObjects)
            {
                if (tableSerializableObject is not null && tableSerializableObject.ToDiGi() is TSerializableObject serializableObject)
                {
                    result.Add(serializableObject);
                }
            }

            return result;
        }
    }
}