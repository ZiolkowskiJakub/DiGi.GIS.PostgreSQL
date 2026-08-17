using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Derives the face of each row once, keyed by the identifier of the row it came from.
        /// <para>Reading a row's geometry deserializes the whole stored object, and the property that exposes the face hands back a clone on every access - so a caller deciding many points against the same rows should derive the faces once through this and test against the result, rather than reaching through the rows again for each point.</para>
        /// <para>The whole face is kept, holes included, unlike <see cref="Polygonal2DsByCountyId(IEnumerable{AdministrativeAreal2D})"/>, which keeps only the outer ring. An area that excludes a town inside it is a face with a hole, and a point in that town belongs to the town rather than to the area around it - testing against the outer ring alone would claim it.</para>
        /// <para>A row whose geometry cannot be read is left out, so the result holds only rows that can actually be tested against.</para>
        /// </summary>
        /// <param name="administrativeAreal2Ds">The rows to derive faces from.</param>
        /// <returns>The face of each row that has one, keyed by row identifier. Empty when nothing could be derived.</returns>
        public static Dictionary<int, PolygonalFace2D> PolygonalFace2DsById(this IEnumerable<AdministrativeAreal2D>? administrativeAreal2Ds)
        {
            Dictionary<int, PolygonalFace2D> result = [];

            if (administrativeAreal2Ds is null)
            {
                return result;
            }

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                // Read into a local: the deserialize behind ToDiGi and the clone behind the property are both paid per access.
                if (administrativeAreal2D?.ToDiGi()?.PolygonalFace2D is PolygonalFace2D polygonalFace2D)
                {
                    result[administrativeAreal2D.Id] = polygonalFace2D;
                }
            }

            return result;
        }
    }
}
