using System;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Draws a reproducible sample of the given size from a collection.
        /// <para>A partial Fisher-Yates shuffle over a copy: every item is equally likely to be drawn and none is drawn twice, without shuffling a list that can hold tens of thousands of entries in full.</para>
        /// <para>The draw consumes exactly one value from <paramref name="random"/> per item returned, so a generator shared across several calls hands each call a different stream depending on how large the preceding populations were. Seed a fresh generator per call with <see cref="RandomSeed(int, int)"/> when the draws are meant to be independent of one another.</para>
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="values">The items to draw from.</param>
        /// <param name="sampleSize">The number of items to draw. A value of zero or less takes them all.</param>
        /// <param name="random">The random source, seeded by the caller so the draw can be repeated.</param>
        /// <returns>The drawn items, or <see langword="null"/> when <paramref name="values"/> or <paramref name="random"/> is <see langword="null"/>.</returns>
        public static List<T>? Sample<T>(this IEnumerable<T>? values, int sampleSize, Random? random)
        {
            if (values is null || random is null)
            {
                return null;
            }

            List<T> values_Temp = [.. values];

            if (sampleSize < 1 || sampleSize >= values_Temp.Count)
            {
                return values_Temp;
            }

            List<T> result = new(sampleSize);
            for (int i = 0; i < sampleSize; i++)
            {
                int index = random.Next(i, values_Temp.Count);

                result.Add(values_Temp[index]);

                values_Temp[index] = values_Temp[i];
                values_Temp[i] = result[i];
            }

            return result;
        }
    }
}
