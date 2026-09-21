using System.Text.Json.Nodes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the stored form of a <c>byte[]</c> member back to bytes.
        /// <para>The DiGi serializer writes a <c>byte[]</c> as a JSON array of numbers (<c>Core.Create.JsonNode</c> treats it as an enumerable), so that is the shape a projection such as <c>v-&gt;'Bytes'</c> reads out of a stored object. A base64 string - the form System.Text.Json gives a <c>byte[]</c> - is accepted as well, so a row written that way decodes rather than fails. Any other shape answers null.</para>
        /// </summary>
        /// <param name="jsonNode">The <see cref="JsonNode"/> holding the member. This value can be null.</param>
        /// <returns>The bytes, or null when <paramref name="jsonNode"/> is null, an element is not a byte, or the node is neither an array nor a base64 string.</returns>
        public static byte[]? ToSystem_Bytes(this JsonNode? jsonNode)
        {
            if (jsonNode is JsonArray jsonArray)
            {
                byte[] result = new byte[jsonArray.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    if (jsonArray[i] is not JsonValue jsonValue || !jsonValue.TryGetValue(out byte @byte))
                    {
                        return null;
                    }

                    result[i] = @byte;
                }

                return result;
            }

            if (jsonNode is JsonValue jsonValue_String && jsonValue_String.TryGetValue(out string? @string) && @string is not null)
            {
                try
                {
                    return System.Convert.FromBase64String(@string);
                }
                catch (System.FormatException)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
