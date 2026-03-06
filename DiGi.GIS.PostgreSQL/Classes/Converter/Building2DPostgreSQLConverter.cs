using DiGi.Geometry.Planar.Classes;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DPostgreSQLConverter : PostgreSQLConverter<Building2D>
    {
        public Building2DPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        public async Task<bool> Clear()
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync();

            return await DiGi.PostgreSQL.Modify.Clear(npgsqlConnection, "building_2D");
        }

        public async Task<HashSet<long>?> UpdateAsync(IEnumerable<Building2D>? building2Ds, double tolerance = Core.Constants.Tolerance.MacroDistance)
        {
            if (building2Ds is null)
            {
                return null;
            }

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync();

            bool succeded = await Create.TableAsync_Building2D(npgsqlConnection);
            if (!succeded)
            {
                return null;
            }

            HashSet<long> result = [];

            if (!building2Ds.Any())
            {
                return result;
            }

            Dictionary<int, List<Building2D>> dictionary_Building2D = [];

            Dictionary<string, int> dictionary_Code = [];

            foreach (Building2D building2D in building2Ds)
            {
                if (building2D is null)
                {
                    continue;
                }

                int? countyId = building2D.CountyId;

                if (countyId is null || !countyId.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(building2D.Code))
                    {
                        if (dictionary_Code.TryGetValue(building2D.Code, out int countyId_Dictionary))
                        {
                            countyId = countyId_Dictionary;
                        }
                        else
                        {
                            if (await AdministrativeAreal2DPostgreSQLConverter.GetIdByCode(npgsqlConnection, building2D.Code, Enums.AdministrativeArealType.County) is int countyId_Code)
                            {
                                countyId = countyId_Code;
                                dictionary_Code[building2D.Code] = countyId_Code;
                            }
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
                    BoundingBox2D? boundingBox2D = building2D.BoundingBox2D;

                    List<AdministrativeAreal2D>? administrativeAreal2Ds = await AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(npgsqlConnection, boundingBox2D, Enums.AdministrativeArealType.County, tolerance);
                    if (administrativeAreal2Ds != null)
                    {
                        int count = administrativeAreal2Ds.Count;
                        if (count == 1)
                        {
                            countyId = administrativeAreal2Ds[0].Id;
                        }
                        else if (count > 1)
                        {
                            GIS.Classes.Building2D? building2D_GIS = building2D.ToDiGi<GIS.Classes.Building2D>();

                            Geometry.Planar.Interfaces.IPolygonal2D? polygonal2D_Building2D = building2D_GIS?.PolygonalFace2D?.ExternalEdge;

                            List<Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>> tuples_AdministrativeAreal2D = administrativeAreal2Ds.ConvertAll(x => new Tuple<AdministrativeAreal2D, Geometry.Planar.Interfaces.IPolygonal2D?>(x, x.ToDiGi<GIS.Classes.AdministrativeAreal2D>()?.PolygonalFace2D?.ExternalEdge));
                            tuples_AdministrativeAreal2D.RemoveAll(x => x is null);

                            if (tuples_AdministrativeAreal2D.Count == 1)
                            {
                                countyId = tuples_AdministrativeAreal2D[0].Item1.Id;
                            }
                            else if (count > 1)
                            {
                                countyId = tuples_AdministrativeAreal2D[0].Item1.Id;
                            }
                        }
                    }
                }

                if (countyId is null || !countyId.HasValue)
                {
                    continue;
                }

                if (!dictionary_Building2D.TryGetValue(countyId.Value, out List<Building2D>? building2Ds_CountyId) || building2Ds_CountyId is null)
                {
                    building2Ds_CountyId = [];
                    dictionary_Building2D[countyId.Value] = building2Ds_CountyId;
                }

                building2Ds_CountyId.Add(building2D);
            }

            await using NpgsqlBatch npgsqlBatch = new(npgsqlConnection);

            foreach (KeyValuePair<int, List<Building2D>> keyValuePair in dictionary_Building2D)
            {
                int countyId = keyValuePair.Key;

                succeded = await Create.TableAsync_Building2D_Partition(npgsqlConnection, countyId);
                if (!succeded)
                {
                    continue;
                }

                foreach (Building2D building2D in keyValuePair.Value)
                {
                    // SQL with full update on conflict (excluding ID)
                    NpgsqlBatchCommand npgsqlBatchCommand = new($@"
                    INSERT INTO building_2D (county_id, reference, min_x, min_y, max_x, max_y, subdivision_id, object)
                    VALUES (@county_id, @reference, @min_x, @min_y, @max_x, @max_y, @subdivision_id, @object)
                    ON CONFLICT (reference)
                    DO UPDATE SET
                        county_id = EXCLUDED.county_id,
                        min_x = EXCLUDED.min_x,
                        min_y = EXCLUDED.min_y,
                        max_x = EXCLUDED.max_x,
                        max_y = EXCLUDED.max_y,
                        subdivision_id = EXCLUDED.subdivision_id,
                        object = EXCLUDED.object
                    RETURNING id;");

                    BoundingBox2D? boundingBox2D = building2D.BoundingBox2D;

                    // Adding parameters with explicit NpgsqlDbType
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("county_id", NpgsqlDbType.Integer) { Value = countyId });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("reference", NpgsqlDbType.Text) { Value = building2D.Reference });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.X });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("min_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Min.Y });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_x", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.X });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("max_y", NpgsqlDbType.Double) { Value = boundingBox2D is null ? double.NaN : boundingBox2D.Max.Y });
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("subdivision_id", NpgsqlDbType.Integer) { Value = (object?)building2D.SubdivisionId ?? DBNull.Value });

                    // Handling potential null for JSONB object
                    npgsqlBatchCommand.Parameters.Add(new NpgsqlParameter("object", NpgsqlDbType.Jsonb)
                    {
                        Value = (object?)building2D.Object?.ToJsonString() ?? DBNull.Value
                    });

                    npgsqlBatch.BatchCommands.Add(npgsqlBatchCommand);
                }
            }

            // Execute batch and collect IDs
            await using NpgsqlDataReader npgsqlDataReader = await npgsqlBatch.ExecuteReaderAsync();

            do
            {
                while (await npgsqlDataReader.ReadAsync())
                {
                    // The RETURNING id works for both INSERT and UPDATE cases
                    long id = npgsqlDataReader.GetInt64(0);
                    result.Add(id);
                }
            }
            while (await npgsqlDataReader.NextResultAsync());

            return result;
        }
    }
}