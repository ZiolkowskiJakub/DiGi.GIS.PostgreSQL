using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Decides whether a county row is in scope for a task that runs over a subset of the country.
        /// <para>A county code is not a key - it names one row per polygon part - so scope is expressed two ways at once: by county row identifier, and by the two-digit voivodeship code a county code starts with. Both filters must admit the row, so a task can be pointed at one voivodeship, at a handful of parts, or at the parts of one voivodeship named by identifier.</para>
        /// <para>A null filter admits everything, which is what makes a national pass the default. A row without a code cannot be placed in a voivodeship, so it is out of scope whenever <paramref name="voivodeshipCodes"/> is given.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row. A negative value is never in scope.</param>
        /// <param name="code">The county code, whose leading digits name the voivodeship.</param>
        /// <param name="countyIds">The county row identifiers in scope, or <see langword="null"/> for every row.</param>
        /// <param name="voivodeshipCodes">The voivodeship codes in scope, or <see langword="null"/> for every voivodeship.</param>
        /// <returns><see langword="true"/> when the county row is in scope, otherwise <see langword="false"/>.</returns>
        public static bool IsInScope(int countyId, string? code, ICollection<int>? countyIds, ICollection<string>? voivodeshipCodes)
        {
            if (countyId < 0)
            {
                return false;
            }

            if (countyIds is not null && !countyIds.Contains(countyId))
            {
                return false;
            }

            if (voivodeshipCodes is null)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }

            foreach (string voivodeshipCode in voivodeshipCodes)
            {
                if (string.IsNullOrWhiteSpace(voivodeshipCode))
                {
                    continue;
                }

                if (code.StartsWith(voivodeshipCode, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
