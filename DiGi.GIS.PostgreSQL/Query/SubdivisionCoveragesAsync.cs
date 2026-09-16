using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Asynchronously measures, for one county, how much of each of its subdivisions' buildings the orthophoto store holds.
        /// <para>The subdivisions measured are those filed under the county part and under every sibling part sharing its code (the parent lookup widens by code on its own): a subdivision's <c>county_id</c> names one part, and the buildings it holds may be filed under another. Each is measured by its polygon through <see cref="CoveragesAsync(OrtoDatasPostgreSQLConverter?, Building2DPostgreSQLConverter?, int, IReadOnlyDictionary{int, PolygonalFace2D}?, double, int, CancellationToken)"/>, which is where the counting and its caveats live - in particular that where the layer nests, a building is counted for every subdivision containing it, so the results of a city and its districts overlap and must not be summed.</para>
        /// </summary>
        /// <param name="ortoDatasPostgreSQLConverter">The converter reading the orthophoto store.</param>
        /// <param name="building2DPostgreSQLConverter">The converter reading the building store.</param>
        /// <param name="administrativeAreal2DPostgreSQLConverter">The converter reading the subdivisions and the county parts.</param>
        /// <param name="countyId">The identifier of the county to measure. One polygon part, not a code - a multi-part county is measured a part at a time.</param>
        /// <param name="tolerance">The distance tolerance of the containment test.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of each command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains one <see cref="OrtoDatasCoverageResult"/> per subdivision holding at least one of the county's buildings, plus one carrying a null <see cref="OrtoDatasCoverageResult.AdministrativeAreal2DId"/> for the buildings inside no subdivision polygon when there are any; or null when a converter is missing, the subdivisions or either side could not be read, or the county holds no orthophoto row at all.</returns>
        public static async Task<List<OrtoDatasCoverageResult>?> SubdivisionCoveragesAsync(this OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter, Building2DPostgreSQLConverter? building2DPostgreSQLConverter, AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, int countyId, double tolerance = Core.Constants.Tolerance.MacroDistance, int commandTimeout = 600, CancellationToken cancellationToken = default)
        {
            if (ortoDatasPostgreSQLConverter is null || building2DPostgreSQLConverter is null || administrativeAreal2DPostgreSQLConverter is null)
            {
                return null;
            }

            // The parent lookup widens a county part to every part sharing its code on its own, so one read
            // brings the subdivisions filed under any sibling part as well.
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(Enums.AdministrativeArealType.Subdivision, countyId, commandTimeout, cancellationToken);
            if (administrativeAreal2Ds is null)
            {
                return null;
            }

            Dictionary<int, PolygonalFace2D> polygonalFace2Ds_ById = administrativeAreal2Ds.PolygonalFace2DsById();

            return await CoveragesAsync(ortoDatasPostgreSQLConverter, building2DPostgreSQLConverter, countyId, polygonalFace2Ds_ById, tolerance, commandTimeout, cancellationToken);
        }
    }
}
