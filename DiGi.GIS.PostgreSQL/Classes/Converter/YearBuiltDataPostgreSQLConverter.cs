using DiGi.GIS.Interfaces;
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
    /// <summary>
    /// Provides a converter for YearBuiltData objects when interacting with a PostgreSQL database.
    /// </summary>
    public class YearBuiltDataPostgreSQLConverter : Building2DReferencedObjectPostgreSQLConverter<YearBuiltData, IYearBuiltData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltDataPostgreSQLConverter"/> class.
        /// </summary>
        /// <param name="connectionData">The connection data used to establish a connection to the PostgreSQL database.</param>
        public YearBuiltDataPostgreSQLConverter(ConnectionData? connectionData)
            : base(connectionData)
        {
        }

        /// <summary>
        /// Gets the name of the table in the PostgreSQL database.
        /// </summary>
        public override string TableName => Constants.TableName.YearBuiltData;

        /// <summary>
        /// Creates an instance of <see cref="YearBuiltData"/> based on the provided database values.
        /// </summary>
        /// <param name="id">The unique identifier of the record.</param>
        /// <param name="countyId">The county identifier associated with the record.</param>
        /// <param name="uniqueId">The unique external identifier for the record.</param>
        /// <param name="reference">The reference string for the record.</param>
        /// <param name="object">The JSON object containing the data attributes.</param>
        /// <param name="createdAt">The timestamp when the record was created.</param>
        /// <returns>A new <see cref="YearBuiltData"/> instance.</returns>
        protected override YearBuiltData Create(long id, int? countyId, string? uniqueId, string? reference, JsonObject? @object, DateTime? createdAt)
        {
            return new YearBuiltData()
            {
                Id = id,
                CountyId = countyId,
                UniqueId = uniqueId,
                Reference = reference,
                Object = @object,
                CreatedAt = createdAt
            };
        }

        /// <summary>
        /// Asynchronously replaces the user-provided year built of one building - every stored object of the building keeps exactly the given user entry, and a building that holds no row gets one - committed as a single transaction.
        /// <para>The county part the write lands under is resolved through <c>building_2d</c> first: the caller's county identifier may name a sibling polygon part, and the <c>building_2d</c> row's part is the one every referenced-object table is filed under. A reference <c>building_2d</c> does not hold answers <c>null</c>.</para>
        /// <para>Each row read back keeps its own <c>unique_id</c>, so the upsert replaces the row it came from; a building with no rows gets one fresh object and therefore one new row. Predicted and other entries are left untouched - the object's entries are keyed by source, so setting the user entry replaces exactly the previous user entry.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row the caller names; the building's own part is resolved through <c>building_2d</c>.</param>
        /// <param name="reference">The reference of the building to write the year for.</param>
        /// <param name="userYearBuilt">The user-provided year built entry to store, relation and provenance included.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <c>true</c> when the write committed, <c>false</c> when it failed and rolled back, and <c>null</c> when the building is unknown or the write could not be attempted.</returns>
        public async Task<bool?> UpdateUserYearBuiltAsync(int countyId, string reference, GIS.Classes.UserYearBuilt userYearBuilt, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference) || userYearBuilt is null)
            {
                return null;
            }

            cancellationToken.ThrowIfCancellationRequested();

            // The part the building is filed under is the key every referenced-object table uses; writing under the
            // caller's identifier verbatim would file a second row under a sibling part.
            Building2DPostgreSQLConverter building2DPostgreSQLConverter = new(ConnectionData);
            Building2DReference? building2DReference = await building2DPostgreSQLConverter.GetBuilding2DReferenceByReferenceAsync(reference, countyId, fallbackByReference: true, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            if (building2DReference is null || building2DReference.CountyId is null)
            {
                return null;
            }

            int countyId_Part = building2DReference.CountyId.Value;

            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return false;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            if (!await npgsqlConnection.TableAsync_Building2DReferencedObject(TableName, commandTimeout, cancellationToken: cancellationToken)
             || !await npgsqlConnection.TableAsync_Building2DReferencedObject_Partition(TableName, countyId_Part, commandTimeout, cancellationToken: cancellationToken))
            {
                return false;
            }

            await using NpgsqlTransaction npgsqlTransaction = await npgsqlConnection.BeginTransactionAsync(cancellationToken);

            try
            {
                List<YearBuiltData>? yearBuiltDatas = await GetItemsByReferencesAsync(npgsqlConnection, [reference], countyId_Part, fallbackByReference: true, commandTimeout: commandTimeout, cancellationToken: cancellationToken);

                List<YearBuiltData> yearBuiltDatas_ToWrite = [];
                if (yearBuiltDatas is not null && yearBuiltDatas.Count > 0)
                {
                    foreach (YearBuiltData yearBuiltData in yearBuiltDatas)
                    {
                        if (yearBuiltData.ToDiGi() is not GIS.Classes.YearBuiltData yearBuiltData_GIS)
                        {
                            continue;
                        }

                        // Keyed by source: replaces exactly the previous user entry, leaves the others untouched.
                        yearBuiltData_GIS.SetUserYearBuilt(userYearBuilt);

                        if (yearBuiltData_GIS.ToPostgreSQL(countyId_Part) is YearBuiltData yearBuiltData_PostgreSQL)
                        {
                            yearBuiltDatas_ToWrite.Add(yearBuiltData_PostgreSQL);
                        }
                    }

                    // Every row the building held was unreadable - committing success over them would read as a clean
                    // write while dropping their entries, so the failure stands and the prior state is left intact.
                    if (yearBuiltDatas_ToWrite.Count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // Random mode's usual case: the building holds no row at all. A fresh object mints a new
                    // unique_id, so the upsert adds a row rather than replacing one.
                    GIS.Classes.YearBuiltData yearBuiltData_New = new(reference);
                    yearBuiltData_New.SetUserYearBuilt(userYearBuilt);

                    if (yearBuiltData_New.ToPostgreSQL(countyId_Part) is not YearBuiltData yearBuiltData_PostgreSQL)
                    {
                        return false;
                    }

                    yearBuiltDatas_ToWrite.Add(yearBuiltData_PostgreSQL);
                }

                PostgreSQLUpdateResult? result = await UpdateAsync(npgsqlConnection, npgsqlTransaction, yearBuiltDatas_ToWrite, commandTimeout, cancellationToken: cancellationToken);
                if (result is null)
                {
                    return false;
                }

                await npgsqlTransaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "{Type} user year built write for reference {Reference} of county part {CountyId} failed and rolled back", nameof(YearBuiltDataPostgreSQLConverter), reference, countyId_Part);
                return false;
            }
        }

        /// <summary>
        /// Asynchronously keeps, of the named references, the buildings of the named county parts that hold no user-provided year built yet.
        /// <para>The main-database half of the random unverified-building draw (<c>Query.RandomBuilding2DReferenceWithoutUserYearBuiltAsync</c>): the references arrive from the orthophoto store, which lives in the other database, and are matched here against <c>building_2d</c> and anti-joined against <c>year_built_data</c>. A reference with no <c>building_2d</c> row under the parts drops out.</para>
        /// <para>Eligibility is strict on the user entries: a building holding a <c>PredictedYearBuilt</c> entry stays eligible - predictions never affect it - while a <c>UserYearBuilt</c> of any relation excludes it, because a bound is still a verification.</para>
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to execute the command.</param>
        /// <param name="countyIds">The <c>building_2d</c> part ids the references are filed under.</param>
        /// <param name="references">The references to keep or drop.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the eligible buildings in the order the references were given, or null when the connection, the parts or the references are missing or either table has never been created.</returns>
        public static async Task<List<Building2DReference>?> GetBuilding2DReferencesWithoutUserYearBuiltAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int>? countyIds, IEnumerable<string>? references, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null || countyIds is null || references is null)
            {
                return null;
            }

            int[] countyIds_Array = [.. countyIds.Distinct()];
            string[] references_Array = [.. references.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct()];
            if (countyIds_Array.Length == 0 || references_Array.Length == 0)
            {
                return null;
            }

            if (!await DiGi.PostgreSQL.Query.TableExistsAsync(npgsqlConnection, Constants.TableName.Building2D)
             || !await DiGi.PostgreSQL.Query.TableExistsAsync(npgsqlConnection, Constants.TableName.YearBuiltData))
            {
                return null;
            }

            string? userType = Core.Query.FullTypeName(typeof(DiGi.GIS.Classes.UserYearBuilt));
            if (string.IsNullOrWhiteSpace(userType))
            {
                return null;
            }

            // array_position keeps the caller's (random) order, so the first row is the first drawn survivor.
            string commandText = $@"
                SELECT b.id, b.county_id, b.reference, b.subdivision_id
                FROM {Constants.TableName.Building2D} b
                WHERE b.county_id = ANY(@countyIds)
                  AND b.reference = ANY(@references)
                  AND NOT EXISTS (
                      SELECT 1
                      FROM {Constants.TableName.YearBuiltData} y
                      WHERE y.county_id = b.county_id
                        AND y.reference = b.reference
                        AND EXISTS (
                            SELECT 1
                            FROM jsonb_array_elements(y.object->'YearBuilts') AS entry(value)
                            WHERE entry->>'_type' = @userType))
                ORDER BY array_position(@references, b.reference);";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyIds", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = countyIds_Array });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("references", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = references_Array });
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("userType", NpgsqlDbType.Text) { Value = userType });

            List<Building2DReference> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result.Add(new Building2DReference
                {
                    Id = npgsqlDataReader.GetInt64(0),
                    CountyId = npgsqlDataReader.GetInt32(1),
                    Reference = npgsqlDataReader.GetString(2),
                    SubdivisionId = npgsqlDataReader.IsDBNull(3) ? null : npgsqlDataReader.GetInt32(3)
                });
            }

            return result;
        }

        /// <summary>
        /// Asynchronously keeps, of the named references, the buildings of the named county parts that hold no user-provided year built yet.
        /// </summary>
        /// <param name="countyIds">The <c>building_2d</c> part ids the references are filed under.</param>
        /// <param name="references">The references to keep or drop.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the eligible buildings in the order the references were given, or null when no connection could be built, the parts or the references are missing or either table has never been created.</returns>
        public async Task<List<Building2DReference>?> GetBuilding2DReferencesWithoutUserYearBuiltAsync(IEnumerable<int>? countyIds, IEnumerable<string>? references, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetBuilding2DReferencesWithoutUserYearBuiltAsync(npgsqlConnection, countyIds, references, commandTimeout, cancellationToken);
        }
    }
}