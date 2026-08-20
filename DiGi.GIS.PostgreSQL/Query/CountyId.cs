using DiGi.Geometry.Planar.Interfaces;
using DiGi.GIS.PostgreSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Picks which of the candidate county rows a 2D building belongs to, by geometry.
        /// <para>A county code names one row per polygon part, so a code can only narrow the field - this is what decides. Candidates are tried in three steps: the parts whose polygon the footprint lies in, else the nearest part, and where several parts contain it the one it overlaps most.</para>
        /// <para>Where several parts cover the footprint <b>whole</b>, the smallest of them wins. Overlap cannot separate candidates that each hold every square metre of the building, so without this the answer would fall to the lowest identifier - a property of import order rather than of geography. The smallest is the most specific area containing it.</para>
        /// <para>Every remaining comparison breaks ties on the row identifier, so two runs over the same building cannot disagree. Returns <see langword="null"/> when nothing can be decided - the caller is expected to leave such a building unwritten rather than file it under a guess.</para>
        /// </summary>
        /// <param name="administrativeAreal2Ds">The candidate county rows.</param>
        /// <param name="polygonal2D">The external edge of the building footprint.</param>
        /// <param name="tolerance">The distance tolerance used for the containment and overlap tests.</param>
        /// <returns>The identifier of the county row the building belongs to, or <see langword="null"/> when it cannot be decided.</returns>
        public static int? CountyId(this IEnumerable<AdministrativeAreal2D>? administrativeAreal2Ds, IPolygonal2D? polygonal2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (administrativeAreal2Ds is null || polygonal2D is null)
            {
                return null;
            }

            return Polygonal2DsByCountyId(administrativeAreal2Ds).CountyId(polygonal2D, tolerance);
        }

        /// <summary>
        /// Picks which of the candidate county rows a 2D building belongs to, from parts whose polygons the caller has already converted.
        /// <para>The decision is the one described on the <see cref="AdministrativeAreal2D"/> overload; only where the polygons come from differs. Deriving a part's polygon means deserializing the stored geometry, and a county polygon carries thousands of vertices, so a caller deciding many buildings against the same parts should convert once with <see cref="Polygonal2DsByCountyId"/> and call this - the other overload converts every candidate again on every building.</para>
        /// </summary>
        /// <param name="polygonal2Ds_ByCountyId">The candidate parts, keyed by the identifier of their county row.</param>
        /// <param name="polygonal2D">The external edge of the building footprint.</param>
        /// <param name="tolerance">The distance tolerance used for the containment and overlap tests.</param>
        /// <returns>The identifier of the county row the building belongs to, or <see langword="null"/> when it cannot be decided.</returns>
        public static int? CountyId(this IDictionary<int, IPolygonal2D>? polygonal2Ds_ByCountyId, IPolygonal2D? polygonal2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (polygonal2Ds_ByCountyId is null || polygonal2Ds_ByCountyId.Count == 0 || polygonal2D is null)
            {
                return null;
            }

            List<KeyValuePair<int, IPolygonal2D>> keyValuePairs = [.. polygonal2Ds_ByCountyId];

            if (keyValuePairs.Count == 1)
            {
                return keyValuePairs[0].Key;
            }

            List<KeyValuePair<int, IPolygonal2D>> keyValuePairs_InRange = keyValuePairs.FindAll(x => x.Value.InRange(polygonal2D, tolerance));

            if (keyValuePairs_InRange.Count == 1)
            {
                return keyValuePairs_InRange[0].Key;
            }

            if (keyValuePairs_InRange.Count == 0)
            {
                // Nothing contains it - a footprint on a part boundary, or coordinates slightly outside
                // every polygon. The nearest part is the least wrong answer available.
                List<Tuple<int, double>> tuples_Distance = [];
                foreach (KeyValuePair<int, IPolygonal2D> keyValuePair in keyValuePairs)
                {
                    tuples_Distance.Add(new Tuple<int, double>(keyValuePair.Key, Geometry.Planar.Query.Distance(polygonal2D, keyValuePair.Value, out _, out _, tolerance)));
                }

                tuples_Distance.Sort((x, y) =>
                {
                    int result = x.Item2.CompareTo(y.Item2);
                    return result != 0 ? result : x.Item1.CompareTo(y.Item1);
                });

                return tuples_Distance[0].Item1;
            }

            // Several parts contain it, so it straddles a boundary: it belongs to the one it lies in most.
            double area_Polygonal2D = polygonal2D.GetArea();

            List<Tuple<int, double>> tuples_Area = [];

            // Parts holding the whole building rather than a share of it, against their own area. Their
            // overlaps are all equal to the building, so the overlap comparison below cannot separate them.
            List<Tuple<int, double>> tuples_Area_Covering = [];

            foreach (KeyValuePair<int, IPolygonal2D> keyValuePair in keyValuePairs_InRange)
            {
                List<IPolygonal2D>? polygonal2Ds_Intersection = Geometry.Planar.Query.Intersection<IPolygonal2D, IPolygonal2D>([keyValuePair.Value, polygonal2D], tolerance);

                double area = 0;
                if (polygonal2Ds_Intersection is not null && polygonal2Ds_Intersection.Count != 0)
                {
                    area = polygonal2Ds_Intersection.ConvertAll(x => x.GetArea()).Sum();
                }

                if (area <= tolerance)
                {
                    continue;
                }

                if (Core.Query.AlmostEquals(area, area_Polygonal2D, tolerance))
                {
                    tuples_Area_Covering.Add(new Tuple<int, double>(keyValuePair.Key, keyValuePair.Value.GetArea()));
                }

                tuples_Area.Add(new Tuple<int, double>(keyValuePair.Key, area));
            }

            // Nesting is not hypothetical here: a part's polygon is its external edge, so an area with a
            // hole punched in it arrives solid and contains whatever sits in the hole, and the bounding box
            // search feeding this can answer with areas that overlap rather than sibling parts of one code.
            // Overlap is capped at the building's own area, so a covering part is by definition one of the
            // parts this would otherwise pick between - refining that tie can never overrule a part that
            // genuinely holds more of the building.
            if (tuples_Area_Covering.Count > 1)
            {
                tuples_Area_Covering.Sort((x, y) =>
                {
                    int result = x.Item2.CompareTo(y.Item2);
                    return result != 0 ? result : x.Item1.CompareTo(y.Item1);
                });

                return tuples_Area_Covering[0].Item1;
            }

            if (tuples_Area.Count == 0)
            {
                // Every overlap collapsed to nothing measurable; fall back to the lowest of the parts that
                // reported containment rather than returning nothing.
                keyValuePairs_InRange.Sort((x, y) => x.Key.CompareTo(y.Key));

                return keyValuePairs_InRange[0].Key;
            }

            tuples_Area.Sort((x, y) =>
            {
                int result = y.Item2.CompareTo(x.Item2);
                return result != 0 ? result : x.Item1.CompareTo(y.Item1);
            });

            return tuples_Area[0].Item1;
        }
    }
}
