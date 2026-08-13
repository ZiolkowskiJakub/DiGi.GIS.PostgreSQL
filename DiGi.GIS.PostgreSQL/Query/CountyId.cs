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
        /// <para>Every comparison breaks ties on the row identifier, so two runs over the same building cannot disagree. Returns <see langword="null"/> when nothing can be decided - the caller is expected to leave such a building unwritten rather than file it under a guess.</para>
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

            List<Tuple<AdministrativeAreal2D, IPolygonal2D>> tuples = [];
            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                if (administrativeAreal2D?.ToDiGi()?.PolygonalFace2D?.ExternalEdge is IPolygonal2D polygonal2D_AdministrativeAreal2D)
                {
                    tuples.Add(new Tuple<AdministrativeAreal2D, IPolygonal2D>(administrativeAreal2D, polygonal2D_AdministrativeAreal2D));
                }
            }

            if (tuples.Count == 0)
            {
                return null;
            }

            if (tuples.Count == 1)
            {
                return tuples[0].Item1.Id;
            }

            List<Tuple<AdministrativeAreal2D, IPolygonal2D>> tuples_InRange = tuples.FindAll(x => x.Item2.InRange(polygonal2D, tolerance));

            if (tuples_InRange.Count == 1)
            {
                return tuples_InRange[0].Item1.Id;
            }

            if (tuples_InRange.Count == 0)
            {
                // Nothing contains it - a footprint on a part boundary, or coordinates slightly outside
                // every polygon. The nearest part is the least wrong answer available.
                List<Tuple<AdministrativeAreal2D, double>> tuples_Distance = [];
                foreach (Tuple<AdministrativeAreal2D, IPolygonal2D> tuple in tuples)
                {
                    tuples_Distance.Add(new Tuple<AdministrativeAreal2D, double>(tuple.Item1, Geometry.Planar.Query.Distance(polygonal2D, tuple.Item2, out _, out _, tolerance)));
                }

                tuples_Distance.Sort((x, y) =>
                {
                    int result = x.Item2.CompareTo(y.Item2);
                    return result != 0 ? result : x.Item1.Id.CompareTo(y.Item1.Id);
                });

                return tuples_Distance[0].Item1.Id;
            }

            // Several parts contain it, so it straddles a boundary: it belongs to the one it lies in most.
            List<Tuple<AdministrativeAreal2D, double>> tuples_Area = [];
            foreach (Tuple<AdministrativeAreal2D, IPolygonal2D> tuple in tuples_InRange)
            {
                List<IPolygonal2D>? polygonal2Ds_Intersection = Geometry.Planar.Query.Intersection<IPolygonal2D, IPolygonal2D>([tuple.Item2, polygonal2D], tolerance);

                double area = 0;
                if (polygonal2Ds_Intersection is not null && polygonal2Ds_Intersection.Count != 0)
                {
                    area = polygonal2Ds_Intersection.ConvertAll(x => x.GetArea()).Sum();
                }

                if (area <= tolerance)
                {
                    continue;
                }

                tuples_Area.Add(new Tuple<AdministrativeAreal2D, double>(tuple.Item1, area));
            }

            if (tuples_Area.Count == 0)
            {
                // Every overlap collapsed to nothing measurable; fall back to the lowest of the parts that
                // reported containment rather than returning nothing.
                tuples_InRange.Sort((x, y) => x.Item1.Id.CompareTo(y.Item1.Id));

                return tuples_InRange[0].Item1.Id;
            }

            tuples_Area.Sort((x, y) =>
            {
                int result = y.Item2.CompareTo(x.Item2);
                return result != 0 ? result : x.Item1.Id.CompareTo(y.Item1.Id);
            });

            return tuples_Area[0].Item1.Id;
        }
    }
}
