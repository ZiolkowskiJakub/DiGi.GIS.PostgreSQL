using DiGi.Analytical.Building.Interfaces;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.IO;
using DiGi.GIS.PostgreSQL.Classes;
using DiGi.Geometry.Spatial.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Modify
    {
        /// <summary>
        /// Classifies the components of the stored <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/>s into the 35 “External Components Area” columns of the given table, keyed by county identifier and reference.
        /// <para>A wall is filed under the sector of its outward normal’s azimuth, a roof under its tilt band (flat below 5°, then [5°, 20°], (20°, 45°] and above 45°) crossed with the same sectors, and a floor under the floor column; the total column is the sum of the 34 breakdowns. A building that arrives with a stored model gets a row in which every empty bucket is 0, so a zero row and an absent row stay distinguishable.</para>
        /// <para>The outward direction of a wall is resolved against the building’s interior, and the interior is the internal point of the model’s first floor: a point on a floor face lies inside the building volume, which a bounding-box centre does not (an L-shaped footprint puts the centre in the notch). A component the method cannot classify - one that does not yield a <see cref="PolygonalFace3D"/>, a face without an internal point or a usable area, a face without a usable normal, or a model without a floor - is a defect in the stored model and throws <see cref="InvalidOperationException"/> naming the building, so the failure is loud rather than a silently missing area.</para>
        /// <para>A component that is geometrically valid but has no definable target bucket - a wall whose normal is vertical, so its azimuth is undefined, or whose orientation against the interior is degenerate - is skipped and counted in the result.</para>
        /// <para>A reference can arrive several times (several stored versions of the model); the first record of a given county and reference is the one that is written and the rest are stepped over, so the collection has to reach this method in the caller’s order of preference - the converter returns the newest record first.</para>
        /// </summary>
        /// <param name="table">The table to fill with the classification.</param>
        /// <param name="buildingModels">The envelopes of stored building models, most preferred record first.</param>
        /// <returns>The number of components skipped because their target bucket is undefined; 0 when every component was classified.</returns>
        public static long Update_ExternalComponentsArea(this Table? table, IEnumerable<BuildingModel>? buildingModels)
        {
            // Must stay in sync with the DiGi.GIS.IO column descriptions: flat is strictly below 5°,
            // the first tilted band is [5°, 20°], the second is (20°, 45°], the third above 45°.
            const double tilt_FlatDegrees = 5.0;
            const double tilt_UpTo20Degrees = 20.0;
            const double tilt_Between45Degrees = 45.0;

            PolygonalFace3D Face(string reference, int countyId, IComponent component, string componentKind)
            {
                PolygonalFace3D? face = DiGi.Analytical.Building.Query.Geometry3D<PolygonalFace3D>(component);
                if (face is null)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the {componentKind} component does not yield a PolygonalFace3D face, so it cannot be classified.");
                }

                return face;
            }

            double Area(string reference, int countyId, PolygonalFace3D face, string componentKind)
            {
                double area = face.GetArea();
                if (double.IsNaN(area) || area <= 0)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the {componentKind} face has no usable area, so it cannot be classified.");
                }

                return area;
            }

            // Wall sectors, in the order of the columns: north, northeast, east, southeast, south, southwest, west, northwest.
            int SectorIndex(DiGi.GIS.Enums.CardinalDirection direction)
            {
                int index = direction switch
                {
                    DiGi.GIS.Enums.CardinalDirection.North => 0,
                    DiGi.GIS.Enums.CardinalDirection.NorthEast => 1,
                    DiGi.GIS.Enums.CardinalDirection.East => 2,
                    DiGi.GIS.Enums.CardinalDirection.SouthEast => 3,
                    DiGi.GIS.Enums.CardinalDirection.South => 4,
                    DiGi.GIS.Enums.CardinalDirection.SouthWest => 5,
                    DiGi.GIS.Enums.CardinalDirection.West => 6,
                    DiGi.GIS.Enums.CardinalDirection.NorthWest => 7,
                    _ => -1
                };

                return index;
            }

            long skippedComponentCount = 0;

            if (table is null || buildingModels is null)
            {
                return skippedComponentCount;
            }

            Column? column_CountyId = table.UpdateColumn<Column>(IO.Constants.Column.CountyId);
            Column? column_Reference = table.UpdateColumn<Column>(IO.Constants.Column.Reference);
            if (column_CountyId is null || column_Reference is null)
            {
                return skippedComponentCount;
            }

            List<Column> columns_External = [];
            foreach (Column column in IO.Create.Columns_ExternalComponentsArea())
            {
                Column? column_Added = table.UpdateColumn<Column>(column);
                if (column_Added is not null)
                {
                    columns_External.Add(column_Added);
                }
            }

            // The 34 breakdown columns come first and the total column last, in the order Columns_ExternalComponentsArea declares them.
            int count_Breakdown = columns_External.Count - 1;
            if (count_Breakdown < 1)
            {
                return skippedComponentCount;
            }

            // Column indexes, in that declared order: 8 wall sectors, then the flat roof at 8,
            // then the 24 tilted-roof bands (8 sectors x 3 bands), then the floor last among the breakdowns.
            int index_FlatRoof = 8;
            int index_TiltedRoof = 9;
            int index_Floor = count_Breakdown - 1;

            Dictionary<(int, string), Row> dictionary_Row = Query.RowsByCountyIdAndReference(table, column_CountyId, column_Reference);
            HashSet<(int, string)> written = [];

            foreach (BuildingModel? buildingModel_Envelope in buildingModels)
            {
                if (buildingModel_Envelope is null)
                {
                    continue;
                }

                string? reference = buildingModel_Envelope.Reference;
                if (string.IsNullOrWhiteSpace(reference))
                {
                    continue;
                }

                if (buildingModel_Envelope.CountyId is not int countyId)
                {
                    continue;
                }

                if (!written.Add((countyId, reference)))
                {
                    continue;
                }

                DiGi.Analytical.Building.Classes.BuildingModel? buildingModel = buildingModel_Envelope.ToDiGi();
                if (buildingModel is null)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the stored model does not rehydrate into an analytical BuildingModel, so its components cannot be classified.");
                }

                // GetComponents hands out clones, so each list is fetched exactly once and reused.
                List<IWall>? walls = buildingModel.GetComponents<IWall>();
                List<IRoof>? roofs = buildingModel.GetComponents<IRoof>();
                List<IFloor>? floors = buildingModel.GetComponents<IFloor>();

                if (floors is null || floors.Count == 0)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the stored model carries no floor, so the interior reference point the wall normals are oriented against is not available.");
                }

                IFloor? floor = floors[0];

                PolygonalFace3D face_Floor = Face(reference, countyId, floor, "floor");
                Point3D? interiorPoint = face_Floor.GetInternalPoint();
                if (interiorPoint is null)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the floor face has no internal point, so the interior reference point the wall normals are oriented against is not available.");
                }

                double[] areas = new double[count_Breakdown];

                if (walls is not null)
                {
                    foreach (IWall? wall in walls)
                    {
                        if (wall is null)
                        {
                            continue;
                        }

                        PolygonalFace3D face_Wall = Face(reference, countyId, wall, "wall");

                        Point3D? facePoint = face_Wall.GetInternalPoint();
                        if (facePoint is null)
                        {
                            throw new InvalidOperationException($"Building {reference} of county {countyId}: the wall face has no internal point, so the wall normal cannot be oriented.");
                        }

                        double area = Area(reference, countyId, face_Wall, "wall");

                        Vector3D? normal = face_Wall.Plane?.Normal;
                        if (normal is null || normal.Length <= 0)
                        {
                            throw new InvalidOperationException($"Building {reference} of county {countyId}: the wall face has no usable normal, so the wall cannot be classified.");
                        }

                        Vector3D? normal_Unit = normal.Unit;
                        if (normal_Unit is null)
                        {
                            throw new InvalidOperationException($"Building {reference} of county {countyId}: the wall face has no usable normal, so the wall cannot be classified.");
                        }

                        Vector3D interior = new(interiorPoint.X - facePoint.X, interiorPoint.Y - facePoint.Y, interiorPoint.Z - facePoint.Z);
                        Vector3D? interior_Unit = interior.Unit;
                        if (interior_Unit is null)
                        {
                            // The wall point and the interior point coincide: the side of the wall the building is on is undefined.
                            skippedComponentCount++;
                            continue;
                        }

                        // A normal pointing into the interior is flipped so the azimuth is measured off the outward side.
                        double cosine = normal_Unit.DotProduct(interior_Unit);
                        if (Math.Abs(cosine) <= Core.Constants.Tolerance.MicroDistance)
                        {
                            // The interior point sits in the wall plane: either side is as defensible as the other, so the sector is not guessed at.
                            skippedComponentCount++;
                            continue;
                        }

                        if (cosine > 0)
                        {
                            normal_Unit = normal_Unit.GetInversed();
                        }

                        double azimuth = (Math.Atan2(normal_Unit.X, normal_Unit.Y) * 180.0 / Math.PI + 360.0) % 360.0;

                        int index = SectorIndex(DiGi.GIS.Query.CardinalDirection(azimuth));
                        if (index < 0)
                        {
                            // CardinalDirection answered Undefined: the azimuth is out of range, so the sector is not guessed at.
                            skippedComponentCount++;
                            continue;
                        }

                        areas[index] += area;
                    }
                }

                if (roofs is not null)
                {
                    foreach (IRoof? roof in roofs)
                    {
                        if (roof is null)
                        {
                            continue;
                        }

                        PolygonalFace3D face_Roof = Face(reference, countyId, roof, "roof");
                        double area = Area(reference, countyId, face_Roof, "roof");

                        Vector3D? normal = face_Roof.Plane?.Normal;
                        if (normal is null || normal.Length <= 0)
                        {
                            throw new InvalidOperationException($"Building {reference} of county {countyId}: the roof face has no usable normal, so the roof cannot be classified.");
                        }

                        Vector3D? normal_Unit = normal.Unit;
                        if (normal_Unit is null)
                        {
                            throw new InvalidOperationException($"Building {reference} of county {countyId}: the roof face has no usable normal, so the roof cannot be classified.");
                        }

                        // A roof normal pointing down is flipped so the tilt is measured against the upward side.
                        if (normal_Unit.Z < 0)
                        {
                            normal_Unit = normal_Unit.GetInversed();
                        }

                        // Angle answers in radians; convert explicitly rather than through Math.ToDegrees, which the target BCL does not carry.
                        double tilt = normal_Unit.Angle(DiGi.Geometry.Spatial.Constants.Vector3D.WorldZ) * 180.0 / Math.PI;
                        if (tilt < tilt_FlatDegrees)
                        {
                            areas[index_FlatRoof] += area;
                            continue;
                        }

                        double azimuth = (Math.Atan2(normal_Unit.X, normal_Unit.Y) * 180.0 / Math.PI + 360.0) % 360.0;

                        int index = SectorIndex(DiGi.GIS.Query.CardinalDirection(azimuth));
                        if (index < 0)
                        {
                            // CardinalDirection answered Undefined: the azimuth is out of range, so the sector is not guessed at.
                            skippedComponentCount++;
                            continue;
                        }

                        int band = tilt <= tilt_UpTo20Degrees ? 0 : tilt <= tilt_Between45Degrees ? 1 : 2;
                        areas[index_TiltedRoof + 8 * band + index] += area;
                    }
                }

                if (floors is not null)
                {
                    foreach (IFloor? floor_Temp in floors)
                    {
                        if (floor_Temp is null)
                        {
                            continue;
                        }

                        PolygonalFace3D face_FloorArea = Face(reference, countyId, floor_Temp, "floor");
                        areas[index_Floor] += Area(reference, countyId, face_FloorArea, "floor");
                    }
                }

                double totalArea = 0;
                foreach (double area in areas)
                {
                    totalArea += area;
                }

                Row? row = null;
                if (!dictionary_Row.TryGetValue((countyId, reference), out row) || row is null)
                {
                    row = table.AddRow();
                    IO.Modify.SetValue(row, column_CountyId, countyId);
                    IO.Modify.SetValue(row, column_Reference, reference);
                    dictionary_Row[(countyId, reference)] = row;
                }

                for (int i = 0; i < count_Breakdown; i++)
                {
                    IO.Modify.SetValue(row, columns_External[i], areas[i] > 0 ? (float)areas[i] : 0f);
                }

                IO.Modify.SetValue(row, columns_External[count_Breakdown], (float)totalArea);

                table.AddRow(row, false);
            }

            return skippedComponentCount;
        }
    }
}
