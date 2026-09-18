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
        /// <para>The wall outward and roof upward normals are the normals of the shell faces of the model’s spaces, built with <see cref="DiGi.Geometry.Core.Enums.Side.External"/> so each face direction is resolved by the shell construction over the space’s face set instead of being guessed from the component’s stored geometry. Every shell face carries the <see cref="DiGi.Core.Interfaces.IUniqueReference"/> of the component it was built from, which is how a face is matched back to its component; a component in one shell face is classified from it, a component in two or more is an internal partition and is excluded from the external area and counted in the result, and a component no shell face carries is a defect in the model’s space structure, so it throws.</para>
        /// <para>A component the method cannot classify - a wall whose normal is vertical, so its azimuth is undefined - is skipped and counted in the result.</para>
        /// <para>A reference can arrive several times (several stored versions of the model); the first record of a given county and reference is the one that is written and the rest are stepped over, so the collection has to reach this method in the caller’s order of preference - the converter returns the newest record first.</para>
        /// </summary>
        /// <param name="table">The table to fill with the classification.</param>
        /// <param name="buildingModels">The envelopes of stored building models, most preferred record first.</param>
        /// <returns>The number of components skipped because they bound two or more spaces or their target bucket is undefined; 0 when every component was classified.</returns>
        public static long Update_ExternalComponentsArea(this Table? table, IEnumerable<BuildingModel>? buildingModels)
        {
            // Must stay in sync with the DiGi.GIS.IO column descriptions: flat is strictly below 5°,
            // the first tilted band is [5°, 20°], the second is (20°, 45°], the third above 45°.
            const double tilt_FlatDegrees = 5.0;
            const double tilt_UpTo20Degrees = 20.0;
            const double tilt_Between45Degrees = 45.0;

            double Area(string reference, int countyId, PolygonalFace3D face, string componentKind)
            {
                double area = face.GetArea();
                if (double.IsNaN(area) || area <= 0)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the {componentKind} face has no usable area, so it cannot be classified.");
                }

                return area;
            }

            Vector3D? NormalUnit(string reference, int countyId, DiGi.Analytical.Classes.Face face_Shell, string componentKind)
            {
                Vector3D? normal = face_Shell.Plane?.Normal;
                if (normal is null || normal.Length <= 0)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the {componentKind} face has no usable normal, so it cannot be classified.");
                }

                Vector3D? normal_Unit = normal.Unit;
                if (normal_Unit is null)
                {
                    throw new InvalidOperationException($"Building {reference} of county {countyId}: the {componentKind} face has no usable normal, so it cannot be classified.");
                }

                return normal_Unit;
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

                double[] areas = new double[count_Breakdown];

                bool hasComponent = (walls is not null && walls.Count > 0) || (roofs is not null && roofs.Count > 0) || (floors is not null && floors.Count > 0);
                if (hasComponent)
                {
                    // The external side of the space shells: the shell construction resolves every face direction over the space's face set,
                    // so a stored normal that points into the building is flipped by topology rather than by guesswork.
                    // A space with relations but no polygonal face throws here (the model's own throw); a model with no space at all answers null.
                    List<DiGi.Analytical.Classes.Shell>? shells = buildingModel.GetShells<DiGi.Analytical.Building.Classes.Space>(DiGi.Geometry.Core.Enums.Side.External);
                    if (shells is null)
                    {
                        throw new InvalidOperationException($"Building {reference} of county {countyId}: the stored model carries components but no space, so no shell face carries their outward normals and they cannot be classified.");
                    }

                    // Every shell face carries the GuidReference of the component it was built from.
                    // A component in exactly one shell face is classified from it; one in two or more is an internal partition.
                    Dictionary<Guid, DiGi.Analytical.Classes.Face> face_ByComponentGuid = [];
                    HashSet<Guid> sharedComponentGuids = [];

                    foreach (DiGi.Analytical.Classes.Shell shell in shells)
                    {
                        // The getter clones on access, so the list is read once per shell.
                        List<DiGi.Analytical.Classes.Face>? faces = shell.PolygonalFaces;
                        if (faces is null || faces.Count == 0)
                        {
                            throw new InvalidOperationException($"Building {reference} of county {countyId}: a space shell carries no face, so the model's space structure is defective and its components cannot be classified.");
                        }

                        foreach (DiGi.Analytical.Classes.Face face in faces)
                        {
                            // Face.UniqueReference is a fresh clone on every call - read it once, and match on the Guid the reference carries.
                            DiGi.Core.Interfaces.IUniqueReference? uniqueReference_Face = face.UniqueReference;
                            if (uniqueReference_Face is not DiGi.Core.Classes.GuidReference guidReference_Face)
                            {
                                continue;
                            }

                            Guid guid_Component = guidReference_Face.Guid;
                            if (face_ByComponentGuid.ContainsKey(guid_Component))
                            {
                                face_ByComponentGuid.Remove(guid_Component);
                                sharedComponentGuids.Add(guid_Component);
                            }
                            else
                            {
                                face_ByComponentGuid.Add(guid_Component, face);
                            }
                        }
                    }

                    if (walls is not null)
                    {
                        foreach (IWall? wall in walls)
                        {
                            if (wall is null)
                            {
                                continue;
                            }

                            Guid guid_Component = wall.Guid;
                            if (sharedComponentGuids.Contains(guid_Component))
                            {
                                // Bounds two or more spaces: an internal partition, not an external component.
                                skippedComponentCount++;
                                continue;
                            }

                            if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
                            {
                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the wall component {guid_Component} bounds no space, so no shell face carries its outward normal and it cannot be classified.");
                            }

                            double area = Area(reference, countyId, face_Shell, "wall");
                            Vector3D normal_Unit = NormalUnit(reference, countyId, face_Shell, "wall")!;

                            double azimuth = (Math.Atan2(normal_Unit.X, normal_Unit.Y) * 180.0 / Math.PI + 360.0) % 360.0;

                            int index = SectorIndex(DiGi.GIS.Query.CardinalDirection(azimuth));
                            if (index < 0)
                            {
                                // CardinalDirection answered Undefined: the normal is vertical, so the sector is not guessed at.
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

                            Guid guid_Component = roof.Guid;
                            if (sharedComponentGuids.Contains(guid_Component))
                            {
                                // Bounds two or more spaces: an internal partition, not an external component.
                                skippedComponentCount++;
                                continue;
                            }

                            if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
                            {
                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the roof component {guid_Component} bounds no space, so no shell face carries its upward normal and it cannot be classified.");
                            }

                            double area = Area(reference, countyId, face_Shell, "roof");
                            Vector3D normal_Unit = NormalUnit(reference, countyId, face_Shell, "roof")!;

                            // The shell normal is the roof's upward direction; no flip is applied.
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
                                // CardinalDirection answered Undefined: the normal is vertical, so the sector is not guessed at.
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

                            Guid guid_Component = floor_Temp.Guid;
                            if (sharedComponentGuids.Contains(guid_Component))
                            {
                                // Bounds two or more spaces: an internal partition, not an external component.
                                skippedComponentCount++;
                                continue;
                            }

                            if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
                            {
                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the floor component {guid_Component} bounds no space, so its shell face is not available and it cannot be classified.");
                            }

                            areas[index_Floor] += Area(reference, countyId, face_Shell, "floor");
                        }
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
