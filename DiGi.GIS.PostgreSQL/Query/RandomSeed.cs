namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Combines a run seed with a county row identifier into a seed for that county alone.
        /// <para>A single generator advanced across counties makes each county's draw depend on how many items every preceding county held, so changing the scope of a run - or the population of one county - changes what every county after it draws. Seeding per county removes that: a county draws the same sample whether it is verified on its own, with its voivodeship, or nationally.</para>
        /// <para><b>Do not replace this with <see cref="System.HashCode.Combine{T1, T2}(T1, T2)"/>.</b> That mixes in a seed randomized per process, so it returns a different value on every run - the opposite of what this exists to provide.</para>
        /// </summary>
        /// <param name="randomSeed">The seed identifying the run.</param>
        /// <param name="countyId">The identifier of the county row.</param>
        /// <returns>The seed to draw that county's sample with.</returns>
        public static int RandomSeed(int randomSeed, int countyId)
        {
            return unchecked((randomSeed * 397) + countyId);
        }
    }
}
