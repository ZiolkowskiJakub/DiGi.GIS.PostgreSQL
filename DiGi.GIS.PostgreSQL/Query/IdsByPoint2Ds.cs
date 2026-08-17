using DiGi.Geometry.Planar.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Decides, for each point, which face contains it, without touching the database.
        /// <para>The result has one entry per point, at the same position, holding null wherever a point lies in no face. That is an ordinary answer rather than a failure - a caller sampling a rectangle over an irregular area meets it at every corner.</para>
        /// <para>Faces are bucketed into a uniform cell grid first, so a point is tested against the few faces near it rather than against all of them. Without that a run over a whole area costs the number of points times the number of faces, each test walking a ring of thousands of vertices.</para>
        /// <para>Where faces overlap, the lowest identifier wins, so the same point decided twice gives the same answer.</para>
        /// </summary>
        /// <param name="polygonalFace2Ds_ById">The faces to decide against, keyed by identifier.</param>
        /// <param name="point2Ds">The points to decide.</param>
        /// <param name="tolerance">The distance a point may lie outside a face and still be counted as within it.</param>
        /// <returns>The identifier of the face containing each point, null at the position of every point that lies in none, or null when either argument is null.</returns>
        public static int?[]? IdsByPoint2Ds(this IDictionary<int, PolygonalFace2D>? polygonalFace2Ds_ById, IReadOnlyList<Point2D>? point2Ds, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (polygonalFace2Ds_ById is null || point2Ds is null)
            {
                return null;
            }

            int count_Point2Ds = point2Ds.Count;

            int?[] result = new int?[count_Point2Ds];
            if (count_Point2Ds == 0 || polygonalFace2Ds_ById.Count == 0)
            {
                return result;
            }

            // Held in ascending identifier order, so the first face found to contain a point is also the lowest numbered one that does.
            List<int> ids = [.. polygonalFace2Ds_ById.Keys];
            ids.Sort();

            List<int> ids_Valid = [];
            List<PolygonalFace2D> polygonalFace2Ds = [];
            List<BoundingBox2D> boundingBox2Ds = [];

            foreach (int id in ids)
            {
                PolygonalFace2D polygonalFace2D = polygonalFace2Ds_ById[id];

                // Computed once here. It walks the whole ring, and recomputing it per point is the cost this method exists to avoid.
                if (polygonalFace2D?.GetBoundingBox() is not BoundingBox2D boundingBox2D)
                {
                    continue;
                }

                ids_Valid.Add(id);
                polygonalFace2Ds.Add(polygonalFace2D);
                boundingBox2Ds.Add(boundingBox2D);
            }

            int count_Faces = ids_Valid.Count;
            if (count_Faces == 0)
            {
                return result;
            }

            BoundingBox2D boundingBox2D_Union = new(boundingBox2Ds);

            double x_Min = boundingBox2D_Union.Min.X;
            double y_Min = boundingBox2D_Union.Min.Y;
            double width = boundingBox2D_Union.Width;
            double height = boundingBox2D_Union.Height;

            // A cell about the size of an average face keeps the number of faces reaching the ring test at a handful,
            // whatever the area covered. Then widened until the grid itself is not the larger cost.
            double cellSize = 0;
            if (!double.IsNaN(width) && !double.IsInfinity(width) && !double.IsNaN(height) && !double.IsInfinity(height) && width > 0 && height > 0)
            {
                cellSize = System.Math.Sqrt(width * height / count_Faces);

                if (double.IsNaN(cellSize) || cellSize <= 0)
                {
                    cellSize = 0;
                }
                else
                {
                    while (((width / cellSize) + 1) * ((height / cellSize) + 1) > 4000000)
                    {
                        cellSize *= 2;
                    }
                }
            }

            // Positions within the lists above, not identifiers. Tuple keyed rather than packed into one number:
            // packing two indexes into a single key hands the dictionary a hash that collapses for the regular pairs a grid produces.
            Dictionary<(int, int), List<int>> indexes_ByCell = [];

            // Faces too large to bucket sensibly would otherwise be stamped into a cell for every point they span.
            List<int> indexes_Oversized = [];

            if (cellSize > 0)
            {
                for (int i = 0; i < count_Faces; i++)
                {
                    BoundingBox2D boundingBox2D = boundingBox2Ds[i];

                    int index_X_Min = CellIndex(boundingBox2D.Min.X - tolerance, x_Min);
                    int index_X_Max = CellIndex(boundingBox2D.Max.X + tolerance, x_Min);
                    int index_Y_Min = CellIndex(boundingBox2D.Min.Y - tolerance, y_Min);
                    int index_Y_Max = CellIndex(boundingBox2D.Max.Y + tolerance, y_Min);

                    long count_Cells = ((long)index_X_Max - index_X_Min + 1) * ((long)index_Y_Max - index_Y_Min + 1);
                    if (count_Cells > 1024)
                    {
                        indexes_Oversized.Add(i);
                        continue;
                    }

                    for (int index_X = index_X_Min; index_X <= index_X_Max; index_X++)
                    {
                        for (int index_Y = index_Y_Min; index_Y <= index_Y_Max; index_Y++)
                        {
                            if (!indexes_ByCell.TryGetValue((index_X, index_Y), out List<int>? indexes))
                            {
                                indexes = [];
                                indexes_ByCell[(index_X, index_Y)] = indexes;
                            }

                            indexes.Add(i);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < count_Faces; i++)
                {
                    indexes_Oversized.Add(i);
                }
            }

            for (int i = 0; i < count_Point2Ds; i++)
            {
                Point2D point2D = point2Ds[i];
                if (point2D is null)
                {
                    continue;
                }

                int index_Match = -1;

                if (cellSize > 0 && indexes_ByCell.TryGetValue((CellIndex(point2D.X, x_Min), CellIndex(point2D.Y, y_Min)), out List<int>? indexes_Cell))
                {
                    index_Match = FirstIndexInRange(indexes_Cell, point2D);
                }

                if (indexes_Oversized.Count != 0)
                {
                    int index_Match_Oversized = FirstIndexInRange(indexes_Oversized, point2D);
                    if (index_Match_Oversized != -1 && (index_Match == -1 || index_Match_Oversized < index_Match))
                    {
                        index_Match = index_Match_Oversized;
                    }
                }

                if (index_Match != -1)
                {
                    result[i] = ids_Valid[index_Match];
                }
            }

            return result;

            int CellIndex(double value, double value_Min)
            {
                double index = System.Math.Floor((value - value_Min) / cellSize);

                if (double.IsNaN(index) || index < int.MinValue)
                {
                    return int.MinValue;
                }

                return index > int.MaxValue ? int.MaxValue : System.Convert.ToInt32(index);
            }

            // The candidates are held in ascending order, so the first one containing the point is the lowest numbered one that does.
            int FirstIndexInRange(List<int> indexes, Point2D point2D)
            {
                foreach (int index in indexes)
                {
                    if (!boundingBox2Ds[index].InRange(point2D, tolerance))
                    {
                        continue;
                    }

                    if (polygonalFace2Ds[index].InRange(point2D, tolerance))
                    {
                        return index;
                    }
                }

                return -1;
            }
        }
    }
}
