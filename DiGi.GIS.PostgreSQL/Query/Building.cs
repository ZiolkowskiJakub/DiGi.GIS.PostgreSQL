using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Selects the single most relevant <see cref="Classes.Building"/> from a collection of candidates.
        /// <para>Candidates are ranked ascending by level of detail and then by year, with nulls treated as the lowest rank, and only the candidates sharing the highest rank are considered.</para>
        /// <para>When more than one candidate shares the highest rank and a point is provided, the candidate whose surface geometry is closest to that point wins; candidates without usable geometry are excluded from that comparison.</para>
        /// </summary>
        /// <param name="buildings">The collection of <see cref="Classes.Building"/> candidates to choose from.</param>
        /// <param name="point3D">The optional <see cref="Point3D"/> used to break ties between candidates of equal rank.</param>
        /// <param name="tolerance">The tolerance used for the closest point calculation.</param>
        /// <returns>The most relevant <see cref="Classes.Building"/>, or null if the collection is null, empty or no candidate could be resolved.</returns>
        public static Classes.Building? Building(this IEnumerable<Classes.Building>? buildings, Point3D? point3D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (buildings is null)
            {
                return null;
            }

            // LOD1 = 0 and LOD2 = 1, therefore ascending order already ranks LOD1 below LOD2.
            // OrderBy is stable and places nulls first, matching the required ranking.
            List<Classes.Building> buildings_Ordered = [.. buildings.Where(x => x is not null).OrderBy(x => x.LOD).ThenBy(x => x.Year)];

            int count = buildings_Ordered.Count;
            if (count == 0)
            {
                return null;
            }

            Classes.Building building_Last = buildings_Ordered[count - 1];

            int index = count - 1;
            while (index > 0 && buildings_Ordered[index - 1].LOD == building_Last.LOD && buildings_Ordered[index - 1].Year == building_Last.Year)
            {
                index--;
            }

            List<Classes.Building> buildings_Ranked = buildings_Ordered.GetRange(index, count - index);

            if (buildings_Ranked.Count == 1 || point3D is null)
            {
                return building_Last;
            }

            Classes.Building? result = null;
            double distance_Min = double.MaxValue;

            foreach (Classes.Building building_Temp in buildings_Ranked)
            {
                List<IPolygonalFace3D>? polygonalFace3Ds = Geometries(building_Temp);
                if (polygonalFace3Ds is null || polygonalFace3Ds.Count == 0)
                {
                    continue;
                }

                if (point3D.ClosestPoint(polygonalFace3Ds, out IPolygonalFace3D? _, out double distance, tolerance) is null)
                {
                    continue;
                }

                if (distance < distance_Min)
                {
                    distance_Min = distance;
                    result = building_Temp;
                }
            }

            return result;

            static List<IPolygonalFace3D>? Geometries(Classes.Building? building)
            {
                IEnumerable<CityGML.Interfaces.ISurface>? surfaces = building?.ToDiGi()?.Surfaces;
                if (surfaces is null)
                {
                    return null;
                }

                List<IPolygonalFace3D> result = [];
                foreach (CityGML.Interfaces.ISurface surface in surfaces)
                {
                    if (surface?.Geometry is IPolygonalFace3D polygonalFace3D)
                    {
                        result.Add(polygonalFace3D);
                    }
                }

                return result;
            }
        }
    }
}
