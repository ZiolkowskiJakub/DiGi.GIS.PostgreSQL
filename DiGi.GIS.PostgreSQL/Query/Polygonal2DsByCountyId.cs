using DiGi.Geometry.Planar.Interfaces;
using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Derives the polygon of each county row once, keyed by the identifier of the row it came from.
        /// <para>A row stores its geometry as JSON, so reading its polygon deserializes the whole object - and a county polygon carries thousands of vertices. Deciding many buildings against the same parts should derive them once through this and pass the result to <see cref="CountyId(IDictionary{int, IPolygonal2D}, IPolygonal2D, double)"/>, rather than handing the rows themselves to the other overload, which repeats the conversion for every building.</para>
        /// <para>A row whose geometry cannot be read is left out, so the result holds only parts that can actually be tested against.</para>
        /// </summary>
        /// <param name="administrativeAreal2Ds">The county rows to derive polygons from.</param>
        /// <returns>The external edge of each row that has one, keyed by row identifier. Empty when nothing could be derived.</returns>
        public static Dictionary<int, IPolygonal2D> Polygonal2DsByCountyId(this IEnumerable<AdministrativeAreal2D>? administrativeAreal2Ds)
        {
            Dictionary<int, IPolygonal2D> result = [];

            if (administrativeAreal2Ds is null)
            {
                return result;
            }

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                if (administrativeAreal2D?.ToDiGi()?.PolygonalFace2D?.ExternalEdge is IPolygonal2D polygonal2D)
                {
                    result[administrativeAreal2D.Id] = polygonal2D;
                }
            }

            return result;
        }
    }
}
