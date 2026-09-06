using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Narrows the polygon parts of a county to those whose stored extent reaches the given bounding box.
        /// <para>A part whose bounding box does not reach a building cannot contain it, because the polygon lies inside its own box. This is therefore a deduction rather than an approximation: what it removes could not have been the answer, and handing what is left to <see cref="CountyId(IDictionary{int, Geometry.Planar.Interfaces.IPolygonal2D}, Geometry.Planar.Interfaces.IPolygonal2D, double)"/> gives what testing every part would have given.</para>
        /// <para>What it buys is the geometry it does not read. Deciding a county of a hundred thousand buildings by containment means deserializing every footprint and testing it against polygons of thousands of vertices; where one part is left, the boxes have already decided and no footprint is needed at all.</para>
        /// <para>A part storing no extent is always a candidate - nothing is known about it, so nothing can be ruled out - and a building with no extent leaves every part standing for the same reason. An empty result means no part reaches the building: it lies outside the county as stored, and the caller has to fall back to every part so that the nearest one can be found.</para>
        /// </summary>
        /// <param name="administrativeAreal2Ds">The candidate county rows, normally every polygon part of one code.</param>
        /// <param name="boundingBox2D">The stored extent of the building.</param>
        /// <param name="tolerance">The distance tolerance used for the extent comparison.</param>
        /// <returns>The identifiers of the parts that could contain the building, in the order the parts were given. Empty when none reaches it or there were no parts.</returns>
        public static List<int> CountyIds(this IEnumerable<AdministrativeAreal2D>? administrativeAreal2Ds, BoundingBox2D? boundingBox2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            List<int> result = [];

            if (administrativeAreal2Ds is null)
            {
                return result;
            }

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                if (administrativeAreal2D is null)
                {
                    continue;
                }

                if (boundingBox2D is not null && administrativeAreal2D.BoundingBox2D is BoundingBox2D boundingBox2D_Part && !boundingBox2D_Part.InRange(boundingBox2D, tolerance))
                {
                    continue;
                }

                result.Add(administrativeAreal2D.Id);
            }

            return result;
        }
    }
}
