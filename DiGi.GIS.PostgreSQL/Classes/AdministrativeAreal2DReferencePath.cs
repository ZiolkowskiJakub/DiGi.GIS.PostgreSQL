using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a collection of administrative 2D area references indexed by their administrative area type.
    /// </summary>
    public class AdministrativeAreal2DReferencePath : SerializableObject, IGISPostgreSQLSerializableObject
    {
        /// <summary>
        /// Gets or sets the dictionary of administrative 2D area references indexed by type.
        /// </summary>
        [JsonIgnore]
        private readonly Dictionary<AdministrativeArealType, AdministrativeAreal2DReference> dictionary = [];

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencePath class from a collection of administrative 2D area references.
        /// </summary>
        /// <param name="administrativeAreal2DReferences">The collection of administrative 2D area references to add.</param>
        public AdministrativeAreal2DReferencePath(IEnumerable<AdministrativeAreal2DReference> administrativeAreal2DReferences)
            : base()
        {
            if (administrativeAreal2DReferences is not null)
            {
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
                {
                    Add(administrativeAreal2DReference);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencePath class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public AdministrativeAreal2DReferencePath(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencePath class by copying data from another AdministrativeAreal2DReferencePath instance.
        /// </summary>
        /// <param name="administrativeAreal2DReferencePath">The AdministrativeAreal2DReferencePath instance to copy data from.</param>
        public AdministrativeAreal2DReferencePath(AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath)
            : base(administrativeAreal2DReferencePath)
        {
            if (administrativeAreal2DReferencePath is not null)
            {
                foreach (AdministrativeAreal2DReference AdministrativeAreal2DReference in administrativeAreal2DReferencePath.dictionary.Values)
                {
                    if (Core.Query.Clone(AdministrativeAreal2DReference) is AdministrativeAreal2DReference administrativeAreal2DReference_Clone)
                    {
                        dictionary[administrativeAreal2DReference_Clone.AdministrativeArealType] = administrativeAreal2DReference_Clone;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the AdministrativeAreal2DReferencePath class.
        /// </summary>
        public AdministrativeAreal2DReferencePath()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the list of administrative 2D area references, serialized in enum order.
        /// </summary>
        [JsonInclude, JsonPropertyName("AdministrativeAreal2DReferences")]
        public List<AdministrativeAreal2DReference> AdministrativeAreal2DReferences
        {
            get
            {
                List<AdministrativeAreal2DReference> result = [];
                foreach (AdministrativeArealType administrativeArealType in System.Enum.GetValues(typeof(AdministrativeArealType)))
                {
                    if (dictionary.TryGetValue(administrativeArealType, out AdministrativeAreal2DReference? administrativeAreal2DReference) && administrativeAreal2DReference is not null)
                    {
                        result.Add(administrativeAreal2DReference);
                    }
                }

                return result;
            }

            set
            {
                dictionary.Clear();

                if (value is null)
                {
                    return;
                }

                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in value)
                {
                    Add(administrativeAreal2DReference);
                }
            }
        }

        /// <summary>
        /// Gets the administrative 2D area reference for the specified administrative area type.
        /// </summary>
        /// <param name="administrativeArealType">The administrative area type to look up.</param>
        /// <returns>The AdministrativeAreal2DReference for the specified type, or null if not found.</returns>
        public AdministrativeAreal2DReference? this[AdministrativeArealType administrativeArealType]
        {
            get
            {
                if (!dictionary.TryGetValue(administrativeArealType, out AdministrativeAreal2DReference? result))
                {
                    return null;
                }

                return result;
            }
        }

        /// <summary>
        /// Adds an administrative 2D area reference to the collection.
        /// </summary>
        /// <param name="administrativeAreal2DReference">The administrative 2D area reference to add.</param>
        /// <returns>True if the reference was added successfully; otherwise, false.</returns>
        public bool Add(AdministrativeAreal2DReference? administrativeAreal2DReference)
        {
            if (administrativeAreal2DReference is null)
            {
                return false;
            }

            if (Core.Query.Clone(administrativeAreal2DReference) is not AdministrativeAreal2DReference administrativeAreal2DReference_Clone)
            {
                return false;
            }

            dictionary[administrativeAreal2DReference_Clone.AdministrativeArealType] = administrativeAreal2DReference_Clone;
            return true;
        }

        /// <summary>
        /// Removes the administrative 2D area reference for the specified administrative area type.
        /// </summary>
        /// <param name="administrativeArealType">The administrative area type to remove.</param>
        /// <returns>True if the reference was removed successfully; otherwise, false.</returns>
        public bool Remove(AdministrativeArealType administrativeArealType)
        {
            return dictionary.Remove(administrativeArealType);
        }
    }
}