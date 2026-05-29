using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Represents a typology model associated with an administrative 2D area reference.
    /// </summary>
    public class TypologyModel : AdministrativeAreal2DReferencedObject<Typology.Classes.TypologyModel>
    {
        /// <summary>
        /// Initializes a new instance of the TypologyModel class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public TypologyModel(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TypologyModel class by copying data from another TypologyModel instance.
        /// </summary>
        /// <param name="typologyModel">The TypologyModel instance to copy data from.</param>
        public TypologyModel(TypologyModel? typologyModel)
            : base(typologyModel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TypologyModel class.
        /// </summary>
        public TypologyModel()
            : base()
        {
        }
    }
}