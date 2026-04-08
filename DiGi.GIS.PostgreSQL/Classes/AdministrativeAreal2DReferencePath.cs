using DiGi.GIS.PostgreSQL.Enums;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2DReferencePath : Areal2D
    {
        [JsonIgnore]
        private readonly Dictionary<AdministrativeArealType, AdministrativeAreal2DReference> dictionary = [];

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

        public AdministrativeAreal2DReferencePath(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

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

        public AdministrativeAreal2DReferencePath()
            : base()
        {

        }

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

        public bool Remove(AdministrativeArealType administrativeArealType)
        {
            return dictionary.Remove(administrativeArealType);
        }
    }
}