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
        /// <para>The wall outward and roof upward normals are the normals of the faces of the model’s external envelope, built by <see cref="DiGi.Analytical.Building.Classes.BuildingModel.GetExternalShell(DiGi.Geometry.Core.Enums.Side?, DiGi.Geometry.Core.Enums.Orientation?, DiGi.Geometry.Core.Enums.Orientation?, double)"/> with <see cref="DiGi.Geometry.Core.Enums.Side.External"/>, so each face direction is resolved once over the envelope instead of being guessed from the component’s stored geometry. That method states the selection rule for every consumer: a component bounding exactly one space is external, one bounding two is an internal partition, one bounding none is part of no envelope. Every envelope face carries the <see cref="DiGi.Core.Interfaces.IUniqueReference"/> of the component it was built from, which is how a face is matched back to its component; a component the envelope carries is classified from its face, one it does not carry is excluded from the external area and counted in the result when it bounds two spaces, and is a defect in the model’s space structure otherwise, so it throws - a component bounding one space that the envelope still leaves out has no polygonal face, or belongs to a model with fewer than the four external faces a closed solid needs.</para>
        /// <para>A component the method cannot classify - a wall whose normal is vertical, so its azimuth is undefined - is skipped and counted in the result.</para>
        /// <para>A reference can arrive several times (several stored versions of the model); the first record of a given county and reference is the one that is written and the rest are stepped over, so the collection has to reach this method in the caller’s order of preference - the converter returns the newest record first.</para>
        /// </summary>
        /// <param name="table">The table to fill with the classification.</param>
        /// <param name="buildingModels">The envelopes of stored building models, most preferred record first.</param>
        /// <returns>The number of components skipped because they bound two spaces or their target bucket is undefined; 0 when every component was classified.</returns>
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

            // The number of spaces a component bounds - the same count the selection rule of GetExternalShell reads.
            // Only reached for a component the envelope does not carry, so the clone GetRelation returns is paid a few times per model at most.
            int SpaceCount(DiGi.Analytical.Building.Classes.BuildingModel buildingModel, IComponent component)
            {
                DiGi.Analytical.Building.Classes.SpaceRelation? spaceRelation = buildingModel.GetRelation<DiGi.Analytical.Building.Classes.SpaceRelation>(component);

                // The getter clones the reference list, so it is read once.
                List<DiGi.Core.Interfaces.IUniqueReference>? uniqueReferences = spaceRelation?.UniqueReferences_To;

                return uniqueReferences?.Count ?? 0;
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
                    // The external envelope of the whole model: every component bounding exactly one space, oriented once over the envelope by ray parity,
                    // so a stored normal that points into the building is flipped by topology rather than by guesswork (GetExternalShell states the selection rule).
                    // Null when the model has no space relation at all or fewer than four external faces; either way every component is then absent from the envelope.
                    DiGi.Analytical.Classes.Shell? shell = buildingModel.GetExternalShell(DiGi.Geometry.Core.Enums.Side.External);

                    // Every envelope face carries the GuidReference of the component it was built from; a component appears at most once.
                    Dictionary<Guid, DiGi.Analytical.Classes.Face> face_ByComponentGuid = [];

                    // The getter clones on access, so the list is read once.
                    List<DiGi.Analytical.Classes.Face>? faces = shell?.PolygonalFaces;
                    if (faces is not null)
                    {
                        foreach (DiGi.Analytical.Classes.Face face in faces)
                        {
                            // Face.UniqueReference is a fresh clone on every call - read it once, and match on the Guid the reference carries.
                            DiGi.Core.Interfaces.IUniqueReference? uniqueReference_Face = face.UniqueReference;
                            if (uniqueReference_Face is not DiGi.Core.Classes.GuidReference guidReference_Face)
                            {
                                continue;
                            }

                            if (!face_ByComponentGuid.TryAdd(guidReference_Face.Guid, face))
                            {
                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the component {guidReference_Face.Guid} appears twice in the external envelope, so the model's space structure is defective and its components cannot be classified.");
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
                            if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
                            {
                                int spaceCount = SpaceCount(buildingModel, wall);
                                if (spaceCount == 2)
                                {
                                    // Bounds two spaces: an internal partition, not an external component.
                                    skippedComponentCount++;
                                    continue;
                                }

                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the wall component {guid_Component} bounds {spaceCount} space(s) and is not part of the external envelope, so its outward normal is not available and it cannot be classified.");
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
                            if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
                            {
                                int spaceCount = SpaceCount(buildingModel, roof);
                                if (spaceCount == 2)
                                {
                                    // Bounds two spaces: an internal partition, not an external component.
                                    skippedComponentCount++;
                                    continue;
                                }

                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the roof component {guid_Component} bounds {spaceCount} space(s) and is not part of the external envelope, so its upward normal is not available and it cannot be classified.");
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
                            if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
                            {
                                int spaceCount = SpaceCount(buildingModel, floor_Temp);
                                if (spaceCount == 2)
                                {
                                    // Bounds two spaces: an internal partition, not an external component.
                                    skippedComponentCount++;
                                    continue;
                                }

                                throw new InvalidOperationException($"Building {reference} of county {countyId}: the floor component {guid_Component} bounds {spaceCount} space(s) and is not part of the external envelope, so its envelope face is not available and it cannot be classified.");
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
