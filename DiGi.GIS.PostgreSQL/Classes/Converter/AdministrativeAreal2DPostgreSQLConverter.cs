using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.PostgreSQL.Constants;
using DiGi.GIS.PostgreSQL.Enums;
using DiGi.GIS.PostgreSQL.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class AdministrativeAreal2DPostgreSQLConverter : PostgreSQLConverter<AdministrativeAreal2D>, IGISPostgreSQLConverter<AdministrativeAreal2D>
    {
        private static readonly IEnumerable<AdministrativeArealType> administrativeArealTypes = Enum.GetValues<AdministrativeArealType>().Cast<AdministrativeArealType>().Where(t => t != AdministrativeArealType.Undefined).OrderBy(t => t);

        public AdministrativeAreal2DPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public static async Task<AdministrativeAreal2D?> GetAdministrativeAreal2DByIdAsync(NpgsqlConnection? npgsqlConnection, int id, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            // Added LIMIT 1 for optimization, although ID should be unique (PK)
            string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE id = @id
                LIMIT 1;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Using strongly typed parameter to prevent SQL injection and ensure correct DB type mapping
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = id });

            return (await ReadAsync_AdministrativeAreal2D(npgsqlCommand, cancellationToken))?.FirstOrDefault();
        }

        public static async Task<AdministrativeAreal2DReference?> GetAdministrativeAreal2DReferenceByIdAsync(NpgsqlConnection? npgsqlConnection, int id, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            return (await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, [id], cancellationToken))?.FirstOrDefault();
        }

        public static async Task<AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(NpgsqlConnection? npgsqlConnection, AdministrativeAreal2DReference administrativeAreal2DReference, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || administrativeAreal2DReference is null)
            {
                return null;
            }

            List<int> ids = administrativeAreal2DReference.GetIds();
            if (ids is null || ids.Count == 0)
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids, cancellationToken);
            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return null;
            }

            return new AdministrativeAreal2DReferencePath(administrativeAreal2DReferences);
        }

        public static async Task<AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(NpgsqlConnection? npgsqlConnection, int id, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            AdministrativeAreal2DReference? administrativeAreal2DReference = await GetAdministrativeAreal2DReferenceByIdAsync(npgsqlConnection, id, cancellationToken);
            if (administrativeAreal2DReference is null)
            {
                return null;
            }

            return await GetAdministrativeAreal2DReferencePathAsync(npgsqlConnection, administrativeAreal2DReference, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2DReferencePath>?> GetAdministrativeAreal2DReferencePathsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<AdministrativeAreal2DReference> administrativeAreal2DReferences, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || administrativeAreal2DReferences is null)
            {
                return null;
            }

            Dictionary<int, AdministrativeAreal2DReference?> dictionary = [];
            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                if (administrativeAreal2DReference?.GetIds() is not List<int> ids)
                {
                    continue;
                }

                for (int i = 0; i < ids.Count; i++)
                {
                    int id = ids[i];

                    if (!dictionary.ContainsKey(id))
                    {
                        dictionary[id] = null;
                    }
                }

                dictionary[administrativeAreal2DReference.Id] = administrativeAreal2DReference;
            }

            List<int> ids_Temp = [];
            foreach (KeyValuePair<int, AdministrativeAreal2DReference?> keyValuePair in dictionary)
            {
                if (keyValuePair.Value is null)
                {
                    ids_Temp.Add(keyValuePair.Key);
                }
            }

            if (ids_Temp.Count != 0)
            {
                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids_Temp, cancellationToken);
                if (administrativeAreal2DReferences_Temp is not null)
                {
                    foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences_Temp)
                    {
                        dictionary[administrativeAreal2DReference.Id] = administrativeAreal2DReference;
                    }
                }
            }

            List<AdministrativeAreal2DReferencePath> result = [];
            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                List<int> ids = administrativeAreal2DReference.GetIds();
                if (ids is null)
                {
                    continue;
                }

                List<AdministrativeAreal2DReference> administrativeAreal2DReferences_Temp = [];

                foreach (int id in ids)
                {
                    administrativeAreal2DReferences_Temp.Add(dictionary[id] ?? new AdministrativeAreal2DReference());
                }

                result.Add(new AdministrativeAreal2DReferencePath(administrativeAreal2DReferences_Temp));
            }

            return result;
        }

        /// <summary>
        /// Searches for administrative areas by name (case-insensitive) and returns a list of references.
        /// </summary>
        /// <param name="npgsqlConnection">Existing Npgsql connection.</param>
        /// <param name="text">The text to search for within the name column.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of AdministrativeAreal2DReference objects matching the search criteria.</returns>
        public static async Task<List<AdministrativeAreal2DReferencePath>?> GetAdministrativeAreal2DReferencePathsByNameAsync(NpgsqlConnection? npgsqlConnection, string text, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await GetAdministrativeAreal2DReferencesByNameAsync(npgsqlConnection, text, cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return null;
            }

            return await GetAdministrativeAreal2DReferencePathsAsync(npgsqlConnection, administrativeAreal2DReferences, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(NpgsqlConnection? npgsqlConnection, AdministrativeArealType administrativeArealType, IEnumerable<int> parentIds, bool uniqueCode = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || parentIds is null)
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, parentIds, cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return null;
            }

            List<AdministrativeAreal2DReference>? result = [];
            foreach (IGrouping<AdministrativeArealType, AdministrativeAreal2DReference> grouping in administrativeAreal2DReferences.GroupBy(x => x.AdministrativeArealType))
            {
                AdministrativeArealType administrativeArealType_Parent = grouping.Key;
                if (administrativeArealType_Parent >= administrativeArealType)
                {
                    continue;
                }

                HashSet<int> parentIds_AdministrativeArealType = [];
                foreach (AdministrativeAreal2DReference administrativeAreal2DReference in grouping)
                {
                    parentIds_AdministrativeArealType.Add(administrativeAreal2DReference.Id);
                }

                string? columnName = administrativeArealType_Parent switch
                {
                    AdministrativeArealType.Country => "country_id",
                    AdministrativeArealType.Voivodeship => "voivodeship_id",
                    AdministrativeArealType.County => "county_id",
                    AdministrativeArealType.Municipality => "municipality_id",
                    _ => null
                };

                if (columnName is null)
                {
                    continue;
                }

                string distinctClause = uniqueCode ? "DISTINCT ON (code)" : string.Empty;
                string orderByClause = uniqueCode ? "ORDER BY code, id ASC" : "ORDER BY id ASC";

                string commandText = $@"
                SELECT {distinctClause}
                    id,              -- index 0
                    reference,       -- index 1
                    code,            -- index 2
                    name,            -- index 3
                    type_id,         -- index 4
                    country_id,      -- index 5
                    voivodeship_id,  -- index 6
                    county_id,       -- index 7
                    municipality_id  -- index 8
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId AND {columnName} = ANY(@parentIds)
                {orderByClause};";

                await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

                npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("parentIds", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = parentIds_AdministrativeArealType!.ToArray() });

                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_AdministrativeArealType = await ReadAsync_AdministrativeAreal2DReference(npgsqlCommand, cancellationToken);
                if (administrativeAreal2DReferences_AdministrativeArealType is not null)
                {
                    result.AddRange(administrativeAreal2DReferences_AdministrativeArealType);
                }
            }

            return result;
        }

        public static async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(NpgsqlConnection? npgsqlConnection, AdministrativeArealType administrativeArealType, int? parentId = null, bool uniqueCode = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string additionalCondition = string.Empty;

            if (parentId.HasValue)
            {
                AdministrativeAreal2DReference? administrativeAreal2DReference = await GetAdministrativeAreal2DReferenceByIdAsync(npgsqlConnection, parentId.Value, cancellationToken);

                if (administrativeAreal2DReference is null)
                {
                    return null;
                }

                AdministrativeArealType administrativeArealType_Parent = administrativeAreal2DReference.AdministrativeArealType;
                if (administrativeArealType_Parent >= administrativeArealType)
                {
                    return null;
                }

                string? columnName = administrativeArealType_Parent switch
                {
                    AdministrativeArealType.Country => "country_id",
                    AdministrativeArealType.Voivodeship => "voivodeship_id",
                    AdministrativeArealType.County => "county_id",
                    AdministrativeArealType.Municipality => "municipality_id",
                    _ => null
                };

                if (columnName is not null)
                {
                    additionalCondition = $"AND {columnName} = @parentId";
                }
            }

            string distinctClause = uniqueCode ? "DISTINCT ON (code)" : string.Empty;
            string orderByClause = uniqueCode ? "ORDER BY code, id ASC" : "ORDER BY id ASC";

            string commandText = $@"
                SELECT {distinctClause}
                    id,              -- index 0
                    reference,       -- index 1
                    code,            -- index 2
                    name,            -- index 3
                    type_id,         -- index 4
                    country_id,      -- index 5
                    voivodeship_id,  -- index 6
                    county_id,       -- index 7
                    municipality_id  -- index 8
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId {additionalCondition}
                {orderByClause};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            if (!string.IsNullOrEmpty(additionalCondition))
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("parentId", NpgsqlDbType.Integer) { Value = parentId!.Value });
            }

            return await ReadAsync_AdministrativeAreal2DReference(npgsqlCommand, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(NpgsqlConnection? npgsqlConnection, string code, CancellationToken cancellationToken = default)
        {
            HashSet<int>? ids = await GetIdsByCodeAsync(npgsqlConnection, code, null, null, cancellationToken);
            if (ids is null)
            {
                return null;
            }

            if (ids.Count == 0)
            {
                return [];
            }

            return await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(NpgsqlConnection? npgsqlConnection, string code, AdministrativeArealType administrativeArealType, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code) || npgsqlConnection is null)
            {
                return null;
            }

            HashSet<int>? ids = await GetIdsByCodeAsync(npgsqlConnection, code, null, null, cancellationToken);
            if (ids is null)
            {
                return null;
            }

            if (ids.Count == 0)
            {
                return [];
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids, cancellationToken);
            if (administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return [];
            }

            List<AdministrativeAreal2DReference> result = [];

            IEnumerable<IGrouping<AdministrativeArealType, AdministrativeAreal2DReference>> groupings = administrativeAreal2DReferences.GroupBy(x => x.AdministrativeArealType);
            foreach (IGrouping<AdministrativeArealType, AdministrativeAreal2DReference> grouping in groupings)
            {
                List<AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(npgsqlConnection, administrativeArealType, grouping.ToList().ConvertAll(x => x.Id), false, cancellationToken);
                if (administrativeAreal2DReferences_Temp is null)
                {
                    continue;
                }

                result.AddRange(administrativeAreal2DReferences_Temp);
            }

            return result;
        }

        public static async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string commandText = $@"
                SELECT
                    id,              -- index 0
                    reference,       -- index 1
                    code,            -- index 2
                    name,            -- index 3
                    type_id,         -- index 4
                    country_id,      -- index 5
                    voivodeship_id,  -- index 6
                    county_id,       -- index 7
                    municipality_id  -- index 8
                FROM {TableName.AdministrativeAreal2D}
                WHERE id = ANY(@ids)";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Using strongly typed parameter to prevent SQL injection and ensure correct DB type mapping
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = ids!.ToArray() });

            return await ReadAsync_AdministrativeAreal2DReference(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Searches for administrative areas by name (case-insensitive) and returns a list of references.
        /// </summary>
        /// <param name="npgsqlConnection">Existing Npgsql connection.</param>
        /// <param name="text">The text to search for within the name column.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of AdministrativeAreal2DReference objects matching the search criteria.</returns>
        public static async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByNameAsync(NpgsqlConnection? npgsqlConnection, string text, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            // Using ILIKE for case-insensitive search and % placeholders for "contains" logic
            const string commandText = $@"
                SELECT
                    id,              -- index 0
                    reference,       -- index 1
                    code,            -- index 2
                    name,            -- index 3
                    type_id,         -- index 4
                    country_id,      -- index 5
                    voivodeship_id,  -- index 6
                    county_id,       -- index 7
                    municipality_id  -- index 8
                FROM {TableName.AdministrativeAreal2D}
                WHERE name ILIKE @text
                ORDER BY name ASC;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Adding wildcards to the parameter value to match text at any position
            string formattedSearch = $"%{text}%";
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("text", NpgsqlDbType.Text) { Value = formattedSearch });

            return await ReadAsync_AdministrativeAreal2DReference(npgsqlCommand, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByAdministrativeArealType(NpgsqlConnection? npgsqlConnection, AdministrativeArealType administrativeArealType, int? parentId = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            string additionalCondition = string.Empty;

            if (parentId.HasValue)
            {
                AdministrativeAreal2DReference? administrativeAreal2DReference = await GetAdministrativeAreal2DReferenceByIdAsync(npgsqlConnection, parentId.Value, cancellationToken);

                if (administrativeAreal2DReference is null)
                {
                    return null;
                }

                AdministrativeArealType administrativeArealType_Parent = administrativeAreal2DReference.AdministrativeArealType;
                if (administrativeArealType_Parent >= administrativeArealType)
                {
                    return null;
                }

                // Determine column name based on parent type
                string? columnName = administrativeArealType_Parent switch
                {
                    AdministrativeArealType.Country => "country_id",
                    AdministrativeArealType.Voivodeship => "voivodeship_id",
                    AdministrativeArealType.County => "county_id",
                    AdministrativeArealType.Municipality => "municipality_id",
                    _ => null
                };

                if (columnName is not null)
                {
                    additionalCondition = $"AND {columnName} = @parentId";
                }
            }

            // Fixed query string and removed unused HashSet
            string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId {additionalCondition};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Explicit typing for parameters
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            if (!string.IsNullOrEmpty(additionalCondition))
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("parentId", NpgsqlDbType.Integer) { Value = parentId!.Value });
            }

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Gets AdministrativeAreal2D for given AdministrativeArealType (not iterative way). It will check all records with given AdministrativeArealType
        /// </summary>
        /// <param name="npgsqlConnection">NpgsqlConnection</param>
        /// <param name="boundingBox2D">BoundingBox2D</param>
        /// <param name="administrativeArealType">AdministrativeArealType</param>
        /// <param name="tolerance">Tolerance</param>
        /// <param name="cancellationToken"></param>
        /// <returns>AdministrativeAreal2D list</returns>
        public static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, AdministrativeArealType administrativeArealType, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || boundingBox2D is null)
            {
                return [];
            }

            double searchMinX = boundingBox2D.Min.X - tolerance;
            double searchMinY = boundingBox2D.Min.Y - tolerance;
            double searchMaxX = boundingBox2D.Max.X + tolerance;
            double searchMaxY = boundingBox2D.Max.Y + tolerance;

            // Applying tolerance:
            // The point is considered within range if its tolerance buffer intersects with the bounding box
            // (x + tolerance) >= min_x AND (x - tolerance) <= max_x (apply the same logic for the Y axis)
            string commandText = new($@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId
                    AND box(point(min_x, min_y), point(max_x, max_y)) && box(point(@searchMinX, @searchMinY), point(@searchMaxX, @searchMaxY))");

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMinX", NpgsqlDbType.Double) { Value = searchMinX });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMinY", NpgsqlDbType.Double) { Value = searchMinY });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMaxX", NpgsqlDbType.Double) { Value = searchMaxX });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("searchMaxY", NpgsqlDbType.Double) { Value = searchMaxY });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand, cancellationToken);
        }

        /// <summary>
        /// Gets AdministrativeAreal2D for given AdministrativeArealTypes (Iterative way). It will iterate in order through Country, Voivodeship, County, Municipality to reduce number of objects. BoundingBox2D in range Country check, then Voivodeship in this specific Country etc..
        /// </summary>
        /// <param name="npgsqlConnection">NpgsqlConnection</param>
        /// <param name="boundingBox2D">BoundingBox2D</param>
        /// <param name="administrativeArealTypes">AdministrativeArealTypes</param>
        /// <param name="tolerance">Tolerance</param>
        /// <param name="cancellationToken"></param>
        /// <returns>AdministrativeAreal2D list</returns>
        public static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, IEnumerable<AdministrativeArealType>? administrativeArealTypes, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || boundingBox2D is null)
            {
                return null;
            }

            HashSet<AdministrativeArealType>? administrativeArealTypes_Temp = administrativeArealTypes is not null ? [.. administrativeArealTypes] : null;

            int maxIndex = (administrativeArealTypes_Temp is not null && administrativeArealTypes_Temp.Count > 0) ? (int)administrativeArealTypes_Temp.Max() : int.MaxValue;

            List<AdministrativeAreal2D>? result = [];

            HashSet<int> excludedIds = [];
            HashSet<int> parentIds = [];
            foreach (AdministrativeArealType administrativeArealType in AdministrativeAreal2DPostgreSQLConverter.administrativeArealTypes)
            {
                if ((int)administrativeArealType > maxIndex)
                {
                    break;
                }

                List<AdministrativeAreal2D>? administrativeAreal2Ds = await GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, administrativeArealType, parentIds, excludedIds, tolerance, cancellationToken);
                if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
                {
                    return result;
                }

                parentIds.Clear();
                foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
                {
                    if (administrativeAreal2D is null)
                    {
                        continue;
                    }

                    excludedIds.Add(administrativeAreal2D.Id);
                    parentIds.Add(administrativeAreal2D.Id);

                    if (administrativeArealTypes_Temp is not null && !administrativeArealTypes_Temp.Contains(administrativeAreal2D.AdministrativeArealType))
                    {
                        continue;
                    }

                    GIS.Classes.AdministrativeAreal2D? administrativeAreal2D_GIS = administrativeAreal2D.ToDiGi();
                    if (administrativeAreal2D_GIS is null)
                    {
                        continue;
                    }

                    if (administrativeAreal2D_GIS.PolygonalFace2D is PolygonalFace2D polygonalFace2D && boundingBox2D.InRange(polygonalFace2D.ExternalEdge, tolerance))
                    {
                        result.Add(administrativeAreal2D);
                    }
                }
            }

            return result;
        }

        public static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCodeAsync(NpgsqlConnection? npgsqlConnection, string code, AdministrativeArealType? administrativeArealType, CancellationToken cancellationToken = default)
        {
            HashSet<int>? ids = await GetIdsByCodeAsync(npgsqlConnection, code, null, administrativeArealType, cancellationToken);
            if (ids is null)
            {
                return null;
            }

            if (ids.Count == 0)
            {
                return [];
            }

            return await GetAdministrativeAreal2DsByIdsAsync(npgsqlConnection, ids, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByIdsAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || ids is null)
            {
                return null;
            }

            if (!ids.Any())
            {
                return [];
            }

            const string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE id = ANY(@ids);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Using strongly typed parameter to prevent SQL injection and ensure correct DB type mapping
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = ids!.ToArray() });

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand, cancellationToken);
        }

        public static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(NpgsqlConnection? npgsqlConnection, Point2D? point2D, AdministrativeArealType administrativeArealType, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (npgsqlConnection is null || point2D is null)
            {
                return [];
            }

            // Applying tolerance:
            // The point is considered within range if its tolerance buffer intersects with the bounding box
            // (x + tolerance) >= min_x AND (x - tolerance) <= max_x (apply the same logic for the Y axis)
            string commandText = new($@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId
                    AND (@x + @tolerance) >= min_x AND (@x - @tolerance) <= max_x
                    AND (@y + @tolerance) >= min_y AND (@y - @tolerance) <= max_y;");

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("x", NpgsqlDbType.Double) { Value = point2D.X });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("y", NpgsqlDbType.Double) { Value = point2D.Y });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("tolerance", NpgsqlDbType.Double) { Value = tolerance });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand);
        }

        public static async Task<long> GetCountAsync(NpgsqlConnection? npgsqlConnection, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, TableName.AdministrativeAreal2D, cancellationToken);
        }

        public static async Task<long> GetEstimatedCountAsync(NpgsqlConnection? npgsqlConnection, bool analyze = false, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return -1;
            }

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, TableName.AdministrativeAreal2D, analyze, cancellationToken);
        }

        public static async Task<int?> GetIdByCodeAsync(NpgsqlConnection? npgsqlConnection, string? code, AdministrativeArealType? administrativeArealType = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            HashSet<int>? ids = await GetIdsByCodeAsync(npgsqlConnection, code, 1, administrativeArealType, cancellationToken);
            if (ids is null || ids.Count == 0)
            {
                return null;
            }

            return ids.ElementAt(0);
        }

        public static async Task<HashSet<int>?> GetIdsByCodeAsync(NpgsqlConnection? npgsqlConnection, string? code, int? limit = null, AdministrativeArealType? administrativeArealType = null, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            // Base query
            string commandText = $@"
                SELECT id
                FROM {TableName.AdministrativeAreal2D}
                WHERE (@typeId IS NULL OR type_id = @typeId)
                  AND code = @code";

            // Dynamically append LIMIT if provided
            if (limit.HasValue)
            {
                commandText += $" LIMIT {limit.Value}";
            }

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Handling the nullable parameter for the SQL query
            object typeValue = administrativeArealType is not null ? (short)administrativeArealType.Value : DBNull.Value;

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint)
            {
                Value = typeValue
            });

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("code", NpgsqlDbType.Text)
            {
                Value = (object?)code ?? DBNull.Value
            });

            // If limit is provided, we can pre-allocate the HashSet capacity to improve performance
            HashSet<int> results = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                if (!await npgsqlDataReader.IsDBNullAsync(0, cancellationToken))
                {
                    results.Add(npgsqlDataReader.GetInt32(0));
                }
            }

            return results;
        }

        public async Task<bool> ClearAsync(CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await DiGi.PostgreSQL.Modify.ClearAsync(npgsqlConnection, TableName.AdministrativeAreal2D, cancellationToken);
        }

        public async Task<bool> CreateTableAsync(int commandTimeout = 30)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync();

            bool result = await Create.TableAsync_AdministrativeArea2D(npgsqlConnection, commandTimeout);
            if (result)
            {
                await DiGi.PostgreSQL.Modify.Analyze(npgsqlConnection, TableName.AdministrativeAreal2D, commandTimeout);
            }

            return result;
        }

        public async Task<AdministrativeAreal2D?> GetAdministrativeAreal2DByCodeAsync(string code)
        {
            List<AdministrativeAreal2D>? administrativeAreal2Ds = await GetAdministrativeAreal2DsByCodesAsync([code]);
            if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
            {
                return null;
            }

            return administrativeAreal2Ds[0];
        }

        public async Task<AdministrativeAreal2D?> GetAdministrativeAreal2DByIdAsync(int id)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            return await GetAdministrativeAreal2DByIdAsync(npgsqlConnection, id);
        }

        public async Task<AdministrativeAreal2DReference?> GetAdministrativeAreal2DReferenceByCodeAsync(string code, AdministrativeArealType? administrativeArealType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            HashSet<int>? ids = await GetIdsByCodeAsync(npgsqlConnection, code, 1, administrativeArealType, cancellationToken);
            if (ids is null || ids.Count == 0)
            {
                return null;
            }

            return (await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids, cancellationToken))?.FirstOrDefault();
        }

        public async Task<AdministrativeAreal2DReference?> GetAdministrativeAreal2DReferenceByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferenceByIdAsync(npgsqlConnection, id, cancellationToken);
        }

        public async Task<AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(AdministrativeAreal2DReference administrativeAreal2DReference, CancellationToken cancellationToken = default)
        {
            if (administrativeAreal2DReference is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferencePathAsync(npgsqlConnection, administrativeAreal2DReference, cancellationToken);
        }

        public async Task<AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(int id, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferencePathAsync(npgsqlConnection, id, cancellationToken);
        }

        /// <summary>
        /// Searches for administrative areas by name (case-insensitive) and returns a list of references.
        /// </summary>
        /// <param name="text">The text to search for within the name column.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of AdministrativeAreal2DReference objects matching the search criteria.</returns>
        public async Task<List<AdministrativeAreal2DReferencePath>?> GetAdministrativeAreal2DReferencePathsByNameAsync(string text, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await GetAdministrativeAreal2DReferencesByNameAsync(npgsqlConnection, text, cancellationToken);
            if (administrativeAreal2DReferences is null)
            {
                return null;
            }

            return await GetAdministrativeAreal2DReferencePathsAsync(npgsqlConnection, administrativeAreal2DReferences, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType administrativeArealType, int? parentId = null, bool uniqueCode = false, CancellationToken cancellationToken = default)
        {
            if (administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            if (administrativeArealType == AdministrativeArealType.Country && parentId.HasValue)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(npgsqlConnection, administrativeArealType, parentId, uniqueCode, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            HashSet<int>? ids = await GetIdsByCodeAsync(npgsqlConnection, code, null, null, cancellationToken);
            if (ids is null)
            {
                return null;
            }

            if (ids.Count == 0)
            {
                return [];
            }

            return await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(string code, AdministrativeArealType administrativeArealType, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferencesByCodeAsync(npgsqlConnection, code, administrativeArealType, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            if (ids is null)
            {
                return null;
            }

            if (!ids.Any())
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferencesByIdsAsync(npgsqlConnection, ids, cancellationToken);
        }

        /// <summary>
        /// Searches for administrative areas by name using the internal connection data.
        /// </summary>
        public async Task<List<AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByNameAsync(string text, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DReferencesByNameAsync(npgsqlConnection, text, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByAdministrativeArealType(AdministrativeArealType administrativeArealType, int? parentId = null, CancellationToken cancellationToken = default)
        {
            if (administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            if (administrativeArealType == AdministrativeArealType.Country && parentId.HasValue)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DsByAdministrativeArealType(npgsqlConnection, administrativeArealType, parentId, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByAdministrativeArealTypeAsync(AdministrativeArealType administrativeArealType)
        {
            if (administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            // Creating connection using your infrastructure
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            // Simple, high-performance query filtering only by type_id
            const string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // Adding the mandatory parameter
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, AdministrativeArealType administrativeArealType, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (boundingBox2D is null || administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            return await GetAdministrativeAreal2DsByBoundingBox2DAsync(boundingBox2D, [administrativeArealType], tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, IEnumerable<AdministrativeArealType>? administrativeArealTypes, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            if (boundingBox2D is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, administrativeArealTypes, tolerance, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(BoundingBox2D? boundingBox2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            return await GetAdministrativeAreal2DsByBoundingBox2DAsync(boundingBox2D, null, tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCircle2DAsync(Circle2D? circle2D, IEnumerable<AdministrativeArealType>? administrativeArealTypes, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (circle2D is null)
            {
                return [];
            }

            return await GetAdministrativeAreal2DsByPoint2DAsync(circle2D.Center, administrativeArealTypes, circle2D.Radius + tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCircle2DAsync(Circle2D? circle2D, AdministrativeArealType administrativeArealType, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (circle2D is null)
            {
                return [];
            }

            return await GetAdministrativeAreal2DsByPoint2DAsync(circle2D.Center, administrativeArealType, circle2D.Radius + tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCircle2DAsync(Circle2D? circle2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (circle2D is null)
            {
                return [];
            }

            return await GetAdministrativeAreal2DsByPoint2DAsync(circle2D.Center, circle2D.Radius + tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCodeAsync(string code, AdministrativeArealType? administrativeArealType, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetAdministrativeAreal2DsByCodeAsync(npgsqlConnection, code, administrativeArealType, cancellationToken);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCodesAsync(IEnumerable<string>? codes = null)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            List<AdministrativeAreal2D> result = [];

            // 1. Dynamic SQL construction
            // If ids is null or empty, the WHERE clause is effectively ignored or skipped
            bool noFilter = codes is null || !codes.Any();

            string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                {(noFilter ? "" : "WHERE code = ANY(@codes)")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            if (!noFilter)
            {
                // Passing the whole collection as a PostgreSQL array
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("codes", NpgsqlDbType.Array | NpgsqlDbType.Text)
                {
                    Value = codes!.ToArray()
                });
            }

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByIdsAsync(IEnumerable<int>? ids = null)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            List<AdministrativeAreal2D> result = [];

            // 1. Dynamic SQL construction
            // If ids is null or empty, the WHERE clause is effectively ignored or skipped
            bool noFilter = ids is null || !ids.Any();

            string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                {(noFilter ? "" : "WHERE id = ANY(@ids)")};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            if (!noFilter)
            {
                // Passing the whole collection as a PostgreSQL array
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("ids", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = ids!.ToArray() });
            }

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(Point2D? point2D, AdministrativeArealType administrativeArealType, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (point2D is null || administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            return await GetAdministrativeAreal2DsByPoint2DAsync(point2D, [administrativeArealType], tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(Point2D? point2D, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            return await GetAdministrativeAreal2DsByPoint2DAsync(point2D, null, tolerance);
        }

        public async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(Point2D? point2D, IEnumerable<AdministrativeArealType>? administrativeArealTypes, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (point2D is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return [];
            }

            await npgsqlConnection.OpenAsync();

            List<AdministrativeArealType> administrativeArealTypes_All = [.. Enum.GetValues<AdministrativeArealType>().Cast<AdministrativeArealType>()];
            administrativeArealTypes_All.Remove(AdministrativeArealType.Undefined);
            administrativeArealTypes_All.Sort();

            int maxIndex = int.MaxValue;
            if (administrativeArealTypes is not null && administrativeArealTypes.Any())
            {
                maxIndex = (int)administrativeArealTypes.Max();
            }

            List<AdministrativeAreal2D>? result = [];

            HashSet<int> excludedIds = [];
            HashSet<int> parentIds = [];
            foreach (AdministrativeArealType administrativeArealType in administrativeArealTypes_All)
            {
                if ((int)administrativeArealType > maxIndex)
                {
                    break;
                }

                List<AdministrativeAreal2D>? administrativeAreal2Ds = await GetAdministrativeAreal2DsByPoint2DAsync(npgsqlConnection, point2D, administrativeArealType, parentIds, excludedIds, tolerance);
                if (administrativeAreal2Ds is null || administrativeAreal2Ds.Count == 0)
                {
                    return result;
                }

                parentIds = [];
                foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
                {
                    if (administrativeAreal2D is null)
                    {
                        continue;
                    }

                    excludedIds.Add(administrativeAreal2D.Id);
                    parentIds.Add(administrativeAreal2D.Id);

                    if (administrativeArealTypes is not null && !administrativeArealTypes.Contains(administrativeAreal2D.AdministrativeArealType))
                    {
                        continue;
                    }

                    GIS.Classes.AdministrativeAreal2D? administrativeAreal2D_GIS = administrativeAreal2D.ToDiGi();
                    if (administrativeAreal2D_GIS is null)
                    {
                        continue;
                    }

                    if (administrativeAreal2D_GIS.PolygonalFace2D is PolygonalFace2D polygonalFace2D && polygonalFace2D.InRange(point2D, tolerance))
                    {
                        result.Add(administrativeAreal2D);
                    }
                }
            }

            return result;
        }

        public async Task<HashSet<string>?> GetCodesAsync()
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            HashSet<string> codes = [];

            // We only select the 'id' column to minimize data transfer
            const string commandText = $"SELECT code FROM {TableName.AdministrativeAreal2D};";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                // Reading only the first column (index 0)
                string code = reader.GetString(0);
                codes.Add(code);
            }

            return codes;
        }

        public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await DiGi.PostgreSQL.Query.CountAsync(npgsqlConnection, TableName.AdministrativeAreal2D, cancellationToken);
        }

        public async Task<long> GetEstimatedCountAsync(bool analyze = false, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return -1;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await DiGi.PostgreSQL.Query.EstimatedCountAsync(npgsqlConnection, TableName.AdministrativeAreal2D, analyze, cancellationToken);
        }

        public async Task<int?> GetIdByCodeAsync(string? code, AdministrativeArealType? administrativeArealType = null)
        {
            if (code is null || administrativeArealType == AdministrativeArealType.Undefined)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            return await GetIdByCodeAsync(npgsqlConnection, code, administrativeArealType);
        }

        public async Task<HashSet<int>?> GetIdsAsync()
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            HashSet<int> ids = [];

            // We only select the 'id' column to minimize data transfer
            string query = $"SELECT id FROM {TableName.AdministrativeAreal2D};";

            await using NpgsqlCommand npgsqlCommand = new(query, npgsqlConnection);
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                // Reading only the first column (index 0)
                int id = reader.GetInt32(0);
                ids.Add(id);
            }

            return ids;
        }

        public async Task<HashSet<int>?> GetIdsAsync(AdministrativeArealType administrativeArealType, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            HashSet<int> ids = [];

            // We only select the 'id' column to minimize data transfer
            string query = $"SELECT id FROM {TableName.AdministrativeAreal2D} WHERE type_id = @typeId;";

            await using NpgsqlCommand npgsqlCommand = new(query, npgsqlConnection);
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });
            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                // Reading only the first column (index 0)
                int id = reader.GetInt32(0);
                ids.Add(id);
            }

            return ids;
        }

        public async Task<HashSet<string>?> GetSubCodesAsync(string code)
        {
            // Basic validation of the input
            if (string.IsNullOrWhiteSpace(code))
            {
                return [];
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            HashSet<string> result = [];

            // SQL with LIKE operator. The '%' sign is a wildcard for any character sequence.
            // We use a parameterized query to prevent SQL Injection.
            string query = $"SELECT code FROM {TableName.AdministrativeAreal2D} WHERE code LIKE @code;";

            await using NpgsqlCommand npgsqlCommand = new(query, npgsqlConnection);

            // We append the wildcard '%' to the prefix in C# to look for strings starting with it
            npgsqlCommand.Parameters.AddWithValue("code", $"{code}%");

            await using NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                // Handling potential NULL in the database, even if column is usually populated
                if (!reader.IsDBNull(0))
                {
                    string code_Temp = reader.GetString(0);
                    result.Add(code_Temp);
                }
            }

            return result;
        }

        public async Task<bool> RefreshAsync(PostgreSQLAdministrativeAreal2DRefreshOptions? postgreSQLAdministrativeAreal2DRefreshOptions = default, IProgress<long>? progress = default, CancellationToken cancellationToken = default)
        {
            postgreSQLAdministrativeAreal2DRefreshOptions ??= new PostgreSQLAdministrativeAreal2DRefreshOptions();

            Dictionary<AdministrativeArealType, List<AdministrativeAreal2D>?> dictionary = [];

            List<AdministrativeArealType> administrativeArealTypes = [.. Enum.GetValues<AdministrativeArealType>().Cast<AdministrativeArealType>()];
            administrativeArealTypes.Remove(AdministrativeArealType.Undefined);
            administrativeArealTypes.Sort();

            int count = administrativeArealTypes.Count;

            int totalUpdated = 0;

            for (int i = 0; i < count; i++)
            {
                AdministrativeArealType administrativeArealType = administrativeArealTypes[i];

                List<AdministrativeAreal2D>? administrativeAreal2Ds_Current = await GetAdministrativeAreal2DsByAdministrativeArealTypeAsync(administrativeArealType);
                administrativeAreal2Ds_Current?.RemoveAll(x => x.BoundingBox2D == null);

                if (administrativeAreal2Ds_Current != null && administrativeAreal2Ds_Current.Count != 0)
                {
                    foreach (AdministrativeAreal2D administrativeAreal2D_Current in administrativeAreal2Ds_Current)
                    {
                        administrativeAreal2D_Current.ResetIds();
                    }

                    if (i != 0)
                    {
                        AdministrativeArealType administrativeArealType_Previous = (AdministrativeArealType)(i - 1);

                        List<AdministrativeAreal2D>? administrativeAreal2Ds_Previous = dictionary[administrativeArealType_Previous];
                        if (administrativeAreal2Ds_Previous != null && administrativeAreal2Ds_Previous.Count != 0)
                        {
                            foreach (AdministrativeAreal2D administrativeAreal2D_Current in administrativeAreal2Ds_Current)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                {
                                    break;
                                }

                                totalUpdated++;
                                progress?.Report(totalUpdated);

                                if (administrativeAreal2D_Current.BoundingBox2D?.GetCentroid() is not Point2D point2D)
                                {
                                    continue;
                                }

                                GIS.Classes.AdministrativeAreal2D? administrativeAreal2D_GIS_Current = null;

                                List<AdministrativeAreal2D>? administrativeAreal2Ds_Filtered = administrativeAreal2Ds_Previous.FindAll(x => x.BoundingBox2D!.InRange(point2D, postgreSQLAdministrativeAreal2DRefreshOptions.Tolerance));
                                if (administrativeAreal2Ds_Filtered is null || administrativeAreal2Ds_Filtered.Count == 0)
                                {
                                    administrativeAreal2D_GIS_Current = administrativeAreal2D_Current.ToDiGi();
                                    if (administrativeAreal2D_GIS_Current is null)
                                    {
                                        continue;
                                    }

                                    point2D = administrativeAreal2D_GIS_Current?.PolygonalFace2D?.GetInternalPoint() ?? point2D;

                                    administrativeAreal2Ds_Filtered = administrativeAreal2Ds_Previous.FindAll(x => x.BoundingBox2D!.InRange(point2D, postgreSQLAdministrativeAreal2DRefreshOptions.Tolerance));
                                }

                                if (administrativeAreal2Ds_Filtered is null || administrativeAreal2Ds_Filtered.Count == 0)
                                {
                                    continue;
                                }

                                if (administrativeAreal2Ds_Filtered.Count == 1)
                                {
                                    Modify.UpdateIds(administrativeAreal2D_Current, administrativeAreal2Ds_Filtered[0]);
                                    continue;
                                }

                                if (administrativeAreal2D_Current.BoundingBox2D?.GetPoints() is not List<Point2D> point2Ds || point2Ds.Count == 0)
                                {
                                    continue;
                                }

                                List<AdministrativeAreal2D>? administrativeAreal2Ds_Filtered_Temp = administrativeAreal2Ds_Filtered.FindAll(x => point2Ds.TrueForAll(y => x.BoundingBox2D!.InRange(y, postgreSQLAdministrativeAreal2DRefreshOptions.Tolerance)));
                                if (administrativeAreal2Ds_Filtered_Temp is not null)
                                {
                                    if (administrativeAreal2Ds_Filtered_Temp.Count == 1)
                                    {
                                        Modify.UpdateIds(administrativeAreal2D_Current, administrativeAreal2Ds_Filtered_Temp[0]);
                                        continue;
                                    }

                                    administrativeAreal2Ds_Filtered_Temp = administrativeAreal2Ds_Filtered;
                                }

                                if (administrativeAreal2D_GIS_Current is null)
                                {
                                    administrativeAreal2D_GIS_Current = administrativeAreal2D_Current.ToDiGi();
                                    if (administrativeAreal2D_GIS_Current is null)
                                    {
                                        continue;
                                    }
                                }

                                point2D = administrativeAreal2D_GIS_Current?.PolygonalFace2D?.GetInternalPoint() ?? point2D;

                                List<GIS.Classes.AdministrativeAreal2D?> administrativeAreal2Ds_GIS_Filtered = administrativeAreal2Ds_Filtered_Temp!.ConvertAll(x => x.ToDiGi());

                                List<GIS.Classes.AdministrativeAreal2D?>? administrativeAreal2Ds_GIS_Filtered_Temp = administrativeAreal2Ds_GIS_Filtered.FindAll(x => x?.PolygonalFace2D is PolygonalFace2D polygonalFace2D && polygonalFace2D.InRange(point2D, postgreSQLAdministrativeAreal2DRefreshOptions.Tolerance));
                                administrativeAreal2Ds_GIS_Filtered_Temp?.RemoveAll(x => x?.PolygonalFace2D is null);

                                if (administrativeAreal2Ds_GIS_Filtered_Temp is null || administrativeAreal2Ds_GIS_Filtered_Temp.Count == 0)
                                {
                                    continue;
                                }

                                if (administrativeAreal2Ds_GIS_Filtered_Temp.Count == 1)
                                {
                                    Modify.UpdateIds(administrativeAreal2D_Current, administrativeAreal2Ds_Filtered_Temp.Find(x => x.Reference == administrativeAreal2Ds_GIS_Filtered_Temp[0]?.Reference));
                                    continue;
                                }

                                administrativeAreal2Ds_GIS_Filtered_Temp.Sort((x, y) => x!.PolygonalFace2D!.GetArea().CompareTo(y!.PolygonalFace2D!.GetArea()));

                                Modify.UpdateIds(administrativeAreal2D_Current, administrativeAreal2Ds_Filtered_Temp.Find(x => x.Reference == administrativeAreal2Ds_GIS_Filtered_Temp[0]?.Reference));
                            }
                        }
                    }
                }

                dictionary[administrativeArealType] = administrativeAreal2Ds_Current;
            }

            List<AdministrativeAreal2D> administrativeAreal2Ds = [];
            foreach (KeyValuePair<AdministrativeArealType, List<AdministrativeAreal2D>?> keyValuePair in dictionary)
            {
                if (keyValuePair.Value is not null && keyValuePair.Value.Count > 0)
                {
                    administrativeAreal2Ds.AddRange(keyValuePair.Value);
                }
            }

            HashSet<int>? indexes = await UpdateAsync(administrativeAreal2Ds);

            return indexes is not null && indexes.Count != 0;
        }

        public async Task<HashSet<int>?> RemoveAsync(IEnumerable<int>? ids)
        {
            if (ids is null)
            {
                return null;
            }

            // Creating connection using your existing infrastructure
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            HashSet<int> result = [];

            if (!ids.Any())
            {
                return result;
            }

            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (int id in ids)
            {
                // Using RETURNING id to confirm which record was actually deleted
                NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    DELETE FROM {TableName.AdministrativeAreal2D}
                    WHERE id = @id
                    RETURNING id;");

                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = id });

                npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
            }

            // Execute the batch
            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync();

            do
            {
                while (await npgsqlDataReader.ReadAsync())
                {
                    // If the record existed and was deleted, its ID is returned here
                    int id = npgsqlDataReader.GetInt32(0);
                    result.Add(id);
                }
            }
            while (await npgsqlDataReader.NextResultAsync());

            return result;
        }

        public async Task<HashSet<int>?> UpdateAsync(IEnumerable<AdministrativeAreal2D>? administrativeAreal2Ds)
        {
            if (administrativeAreal2Ds is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await Create.TableAsync_AdministrativeArea2D(npgsqlConnection);
            if (!succeded)
            {
                return null;
            }

            HashSet<int> result = [];

            if (!administrativeAreal2Ds.Any())
            {
                return result;
            }

            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                if (administrativeAreal2D is null)
                {
                    continue;
                }

                // SQL with full update on conflict (excluding ID)
                NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO {TableName.AdministrativeAreal2D} (reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object)
                    VALUES (@reference, @code, @name, @type_id, @min_x, @min_y, @max_x, @max_y, @country_id, @voivodeship_id, @county_id, @municipality_id, @object)
                    ON CONFLICT (reference)
                    DO UPDATE SET
                        code = EXCLUDED.code,
                        name = EXCLUDED.name,
                        type_id = EXCLUDED.type_id,
                        min_x = EXCLUDED.min_x,
                        min_y = EXCLUDED.min_y,
                        max_x = EXCLUDED.max_x,
                        max_y = EXCLUDED.max_y,
                        country_id = EXCLUDED.country_id,
                        voivodeship_id = EXCLUDED.voivodeship_id,
                        county_id = EXCLUDED.county_id,
                        municipality_id = EXCLUDED.municipality_id,
                        object = EXCLUDED.object
                    RETURNING id;");

                BoundingBox2D? boundingBox2D = administrativeAreal2D.BoundingBox2D;

                // Adding parameters with explicit NpgsqlDbType
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("reference", NpgsqlDbType.Text) { Value = administrativeAreal2D.Reference });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("code", NpgsqlDbType.Text) { Value = (object?)administrativeAreal2D.Code ?? DBNull.Value });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Text) { Value = (object?)administrativeAreal2D.Name ?? DBNull.Value });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("type_id", NpgsqlDbType.Smallint) { Value = (short)administrativeAreal2D.AdministrativeArealType });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.X });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.Y });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.X });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.Y });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("country_id", NpgsqlDbType.Integer) { Value = (object?)administrativeAreal2D.CountryId ?? DBNull.Value });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("voivodeship_id", NpgsqlDbType.Integer) { Value = (object?)administrativeAreal2D.VoivodeshipId ?? DBNull.Value });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = (object?)administrativeAreal2D.CountyId ?? DBNull.Value });
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("municipality_id", NpgsqlDbType.Integer) { Value = (object?)administrativeAreal2D.MunicipalityId ?? DBNull.Value });

                // Handling potential null for JSONB object
                npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                {
                    Value = (object?)administrativeAreal2D.Object?.ToJsonString() ?? DBNull.Value
                });

                npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
            }

            // Execute batch and collect IDs
            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync();

            do
            {
                while (await npgsqlDataReader.ReadAsync())
                {
                    // The RETURNING id works for both INSERT and UPDATE cases
                    int id = npgsqlDataReader.GetInt32(0);
                    result.Add(id);
                }
            }
            while (await npgsqlDataReader.NextResultAsync());

            return result;
        }

        private static AdministrativeAreal2D Create_AdministrativeAreal2D(NpgsqlDataReader npgsqlDataReader)
        {
            return new AdministrativeAreal2D
            {
                Id = npgsqlDataReader.GetInt32(0),
                Reference = npgsqlDataReader.GetString(1),
                Code = npgsqlDataReader.GetString(2),
                Name = npgsqlDataReader.GetString(3),
                AdministrativeArealType = (AdministrativeArealType)npgsqlDataReader.GetInt32(4),
                BoundingBox2D = new BoundingBox2D(
                        new Point2D(npgsqlDataReader.IsDBNull(5) ? double.NaN : npgsqlDataReader.GetDouble(5), npgsqlDataReader.IsDBNull(6) ? double.NaN : npgsqlDataReader.GetDouble(6)),
                        new Point2D(npgsqlDataReader.IsDBNull(7) ? double.NaN : npgsqlDataReader.GetDouble(7), npgsqlDataReader.IsDBNull(8) ? double.NaN : npgsqlDataReader.GetDouble(8))),
                CountryId = npgsqlDataReader.IsDBNull(9) ? null : npgsqlDataReader.GetInt32(9),
                VoivodeshipId = npgsqlDataReader.IsDBNull(10) ? null : npgsqlDataReader.GetInt32(10),
                CountyId = npgsqlDataReader.IsDBNull(11) ? null : npgsqlDataReader.GetInt32(11),
                MunicipalityId = npgsqlDataReader.IsDBNull(12) ? null : npgsqlDataReader.GetInt32(12),
                Object = JsonNode.Parse(npgsqlDataReader.GetString(13)) as JsonObject,
                CreatedAt = npgsqlDataReader.IsDBNull(14) ? null : npgsqlDataReader.GetDateTime(14),
            };
        }

        private static AdministrativeAreal2DReference Create_AdministrativeAreal2DReference(NpgsqlDataReader npgsqlDataReader)
        {
            return new AdministrativeAreal2DReference
            {
                Id = npgsqlDataReader.GetInt32(0),
                Reference = npgsqlDataReader.GetString(1),
                Code = npgsqlDataReader.GetString(2),
                Name = npgsqlDataReader.GetString(3),
                AdministrativeArealType = (AdministrativeArealType)npgsqlDataReader.GetInt32(4),
                CountryId = npgsqlDataReader.IsDBNull(5) ? null : npgsqlDataReader.GetInt32(5),
                VoivodeshipId = npgsqlDataReader.IsDBNull(6) ? null : npgsqlDataReader.GetInt32(6),
                CountyId = npgsqlDataReader.IsDBNull(7) ? null : npgsqlDataReader.GetInt32(7),
                MunicipalityId = npgsqlDataReader.IsDBNull(8) ? null : npgsqlDataReader.GetInt32(8),
            };
        }

        private static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(NpgsqlConnection? npgsqlConnection, BoundingBox2D? boundingBox2D, AdministrativeArealType administrativeArealType, HashSet<int> parentIds, HashSet<int> excludedIds, double tolerance = Core.Constants.Tolerance.MacroDistance, CancellationToken cancellationToken = default)
        {
            // Check if point2D or the list of IDs is null/empty
            if (npgsqlConnection is null || boundingBox2D is null)
            {
                return [];
            }

            if (administrativeArealType == AdministrativeArealType.Undefined)
            {
                return [];
            }

            if (administrativeArealType == AdministrativeArealType.Country)
            {
                if (parentIds != null && parentIds.Count != 0)
                {
                    return [];
                }

                return await GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, administrativeArealType, tolerance, cancellationToken);
            }

            if (parentIds is null || parentIds.Count == 0)
            {
                return [];
            }

            string? parentIdColumnName = Query.ParentIdColumnName(administrativeArealType);
            if (string.IsNullOrWhiteSpace(parentIdColumnName))
            {
                return [];
            }

            string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId
                    AND id != ALL(@excludedIds)
                    AND ({parentIdColumnName} = ANY(@parentIds) OR {parentIdColumnName} IS NULL)
                    AND (@minX - @tolerance) <= max_x AND (@maxX + @tolerance) >= min_x
                    AND (@minY - @tolerance) <= max_y AND (@maxY + @tolerance) >= min_y;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("minX", NpgsqlDbType.Double) { Value = boundingBox2D.Min.X });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("minY", NpgsqlDbType.Double) { Value = boundingBox2D.Min.Y });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("maxX", NpgsqlDbType.Double) { Value = boundingBox2D.Max.X });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("maxY", NpgsqlDbType.Double) { Value = boundingBox2D.Max.Y });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("tolerance", NpgsqlDbType.Double) { Value = tolerance });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            // Passing the IEnumerable<int> as an array parameter
            // Npgsql automatically maps C# arrays/collections to PostgreSQL arrays
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("parentIds", NpgsqlDbType.Array | NpgsqlDbType.Integer)
            {
                Value = parentIds.ToArray()
            });

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("excludedIds", NpgsqlDbType.Array | NpgsqlDbType.Integer)
            {
                Value = excludedIds?.ToArray() ?? []
            });

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand, cancellationToken);
        }

        private static async Task<List<AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(NpgsqlConnection? npgsqlConnection, Point2D? point2D, AdministrativeArealType administrativeArealType, HashSet<int> parentIds, HashSet<int> excludedIds, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            // Check if point2D or the list of IDs is null/empty
            if (npgsqlConnection is null || point2D is null)
            {
                return [];
            }

            if (administrativeArealType == AdministrativeArealType.Undefined)
            {
                return [];
            }

            if (administrativeArealType == AdministrativeArealType.Country)
            {
                if (parentIds != null && parentIds.Count != 0)
                {
                    return [];
                }

                return await GetAdministrativeAreal2DsByPoint2DAsync(npgsqlConnection, point2D, administrativeArealType, tolerance);
            }

            if (parentIds is null || parentIds.Count == 0)
            {
                return [];
            }

            string? parentIdColumnName = Query.ParentIdColumnName(administrativeArealType);
            if (string.IsNullOrWhiteSpace(parentIdColumnName))
            {
                return [];
            }

            // 1. Prepare the dynamic part of the query for Excluded IDs
            // We check if we actually have any IDs to exclude to avoid the ALL() trap
            bool hasExclusions = excludedIds != null && excludedIds.Count > 0;
            string excludedFilter = hasExclusions ? "AND id != ALL(@excludedIds)" : "";

            // 2. Build the command text using the safe fragment
            string commandText = $@"
                SELECT id, reference, code, name, type_id, min_x, min_y, max_x, max_y, country_id, voivodeship_id, county_id, municipality_id, object, created_at
                FROM {TableName.AdministrativeAreal2D}
                WHERE type_id = @typeId
                    {excludedFilter}
                    AND ({parentIdColumnName} = ANY(@parentIds) OR {parentIdColumnName} IS NULL)
                    AND (@x + @tolerance) >= min_x AND (@x - @tolerance) <= max_x
                    AND (@y + @tolerance) >= min_y AND (@y - @tolerance) <= max_y;";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);

            // 3. Add parameters - we only add excludedIds if it's actually used in the query
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("x", NpgsqlDbType.Double) { Value = point2D.X });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("y", NpgsqlDbType.Double) { Value = point2D.Y });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("tolerance", NpgsqlDbType.Double) { Value = tolerance });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("typeId", NpgsqlDbType.Smallint) { Value = (short)administrativeArealType });

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("parentIds", NpgsqlDbType.Array | NpgsqlDbType.Integer)
            {
                Value = parentIds.ToArray()
            });

            if (hasExclusions)
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("excludedIds", NpgsqlDbType.Array | NpgsqlDbType.Integer)
                {
                    Value = excludedIds!.ToArray()
                });
            }

            return await ReadAsync_AdministrativeAreal2D(npgsqlCommand);
        }

        private static async Task<List<AdministrativeAreal2D>?> ReadAsync_AdministrativeAreal2D(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync_AdministrativeAreal2D(npgsqlDataReader, cancellationToken);
        }

        private static async Task<List<AdministrativeAreal2D>> ReadAsync_AdministrativeAreal2D(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<AdministrativeAreal2D> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_AdministrativeAreal2D(npgsqlDataReader));
            }

            return result;
        }

        private static async Task<List<AdministrativeAreal2DReference>?> ReadAsync_AdministrativeAreal2DReference(NpgsqlCommand npgsqlCommand, CancellationToken cancellationToken = default)
        {
            if (npgsqlCommand is null)
            {
                return null;
            }

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);

            return await ReadAsync_AdministrativeAreal2DReference(npgsqlDataReader, cancellationToken);
        }

        private static async Task<List<AdministrativeAreal2DReference>> ReadAsync_AdministrativeAreal2DReference(NpgsqlDataReader npgsqlDataReader, CancellationToken cancellationToken = default)
        {
            List<AdministrativeAreal2DReference> result = [];

            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(Create_AdministrativeAreal2DReference(npgsqlDataReader));
            }

            return result;
        }
    }
}