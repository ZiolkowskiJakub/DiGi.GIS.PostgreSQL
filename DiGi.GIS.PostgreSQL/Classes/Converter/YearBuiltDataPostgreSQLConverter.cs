using DiGi.GIS.Interfaces;
using DiGi.PostgreSQL.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
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
        /// Retrieves the training labels held under the specified county identifier, projected on the server so the full year history never crosses the connection.
        /// <para>The label of a record is the year of its user entry where one exists, otherwise the year of its first non-prediction entry - the selection <c>DiGi.GIS.ML.Query.YearBuiltLabels</c> makes on the same object. The object holds its entries as a dictionary keyed by source, so of a source the last entry in stored order is what it answers and its values walk in first-seen order of the sources; this query reproduces that record by record, which is also why a record holding nothing but predictions contributes nothing rather than a defaulted year.</para>
        /// <para>A reference holding several rows answers with the year of the <b>oldest</b> row that carries a label, in <c>(created_at, id)</c> order: the bulk read of the same rows orders <c>created_at DESC, id DESC</c> and the label selection overwrites by reference while walking it, so the last usable record - the oldest one - is what the incumbent path keeps. Reusing that order here is what keeps a projected read and the full read on every sampled county reporting the same dictionary.</para>
        /// <para>The read is pruned to the one county row named, like the rest of this converter: a row filed under a sibling polygon part of a multi-part county is out of scope for it and is read by naming that part. The parts of a county are its own identifiers, and a caller that wants every part names them.</para>
        /// <para>The entry is recognised by its stored <c>_type</c> discriminator, not by a <c>Source</c> member - that one is a key of the in-memory dictionary the object builds on deserialization and is not a field of the stored JSON, so it is absent from this table. The type names are bound as parameters from <c>DiGi.Core.Query.FullTypeName</c> at call time, so a rename of the entry class or its assembly changes the parameters and the query with it instead of matching nothing in silence.</para>
        /// </summary>
        /// <param name="npgsqlConnection">The <see cref="NpgsqlConnection"/> used to connect to the PostgreSQL database.</param>
        /// <param name="countyId">The identifier of the county row to read; if null, labels across all counties are retrieved.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the year of each labelled reference held, empty when the county holds no label, or null when the connection is null.</returns>
        public async Task<Dictionary<string, short>?> GetUserYearBuiltsByCountyIdAsync(NpgsqlConnection? npgsqlConnection, int? countyId, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            if (npgsqlConnection is null)
            {
                return null;
            }

            // The stored discriminator of the two entry kinds. A user entry is the label wherever there is
            // one; anything not a prediction is taken only when there is not - the non-prediction branch is
            // a negation rather than an equality on the user type, so an entry source added after these two
            // counts as ground truth without this query having to be revisited.
            string? userType = Core.Query.FullTypeName(typeof(DiGi.GIS.Classes.UserYearBuilt));
            string? predictionType = Core.Query.FullTypeName(typeof(DiGi.GIS.Classes.PredictedYearBuilt));
            if (string.IsNullOrWhiteSpace(userType) || string.IsNullOrWhiteSpace(predictionType))
            {
                return null;
            }

            // The selection is per row, then per reference, and it reproduces the incumbent one record by
            // record. The incumbent reads the object and its entries form a dictionary keyed by source, so
            // of a source the last entry in stored order is what the object holds, and its values walk in
            // first-seen order of the sources: the label is the user entry where the object holds one, else
            // the first non-prediction entry it holds. Per reference, the oldest usable row wins - the
            // created_at ASC, id ASC rank, the reverse of the bulk read's created_at DESC, id DESC, whose
            // last usable record the incumbent selection keeps.
            // The named county prunes the CTE to its own partition; without one the same query answers every part.
            string countyFilter = countyId.HasValue ? " WHERE county_id = @countyId" : string.Empty;

            string commandText = $@"
                WITH labeled AS (
                    SELECT reference,
                           created_at,
                           id,
                           COALESCE(
                               (SELECT (value->>'Year')::smallint
                                  FROM jsonb_array_elements(object->'YearBuilts') WITH ORDINALITY AS entry(value)
                                 WHERE value->>'_type' = @userType
                                 ORDER BY ordinality DESC
                                 LIMIT 1),
                               (SELECT w.year
                                  FROM (
                                      SELECT e.year,
                                             min(e.ordinality) OVER (PARTITION BY e.type) AS first_seen,
                                             row_number() OVER (PARTITION BY e.type ORDER BY e.ordinality DESC) AS last_in_source
                                         FROM (
                                             SELECT (value->>'Year')::smallint AS year,
                                                    value->>'_type' AS type,
                                                    ordinality AS ordinality
                                                FROM jsonb_array_elements(object->'YearBuilts') WITH ORDINALITY AS entry(value)
                                               WHERE value->>'_type' <> @predictionType
                                         ) e
                                  ) w
                                 WHERE w.last_in_source = 1
                                 ORDER BY w.first_seen
                                 LIMIT 1)
                           ) AS year
                    FROM {TableName}
                    {countyFilter}
                )
                SELECT reference, year
                FROM (
                    SELECT reference,
                           year,
                           row_number() OVER (PARTITION BY reference ORDER BY created_at ASC, id ASC) AS rank
                    FROM labeled
                    WHERE year IS NOT NULL
                ) ranked
                WHERE rank = 1";

            await using NpgsqlCommand npgsqlCommand = new(commandText, npgsqlConnection);
            npgsqlCommand.CommandTimeout = commandTimeout;

            npgsqlCommand.Parameters.Add(new NpgsqlParameter("userType", userType));
            npgsqlCommand.Parameters.Add(new NpgsqlParameter("predictionType", predictionType));

            if (countyId.HasValue)
            {
                npgsqlCommand.Parameters.Add(new NpgsqlParameter("countyId", NpgsqlDbType.Integer) { Value = countyId.Value });
            }

            Dictionary<string, short> result = [];

            await using NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken);
            while (await npgsqlDataReader.ReadAsync(cancellationToken))
            {
                result[npgsqlDataReader.GetString(0)] = npgsqlDataReader.GetInt16(1);
            }

            return result;
        }

        /// <summary>
        /// Retrieves the training labels held under the specified county identifier, projected on the server so the full year history never crosses the connection.
        /// <para>See <see cref="GetUserYearBuiltsByCountyIdAsync(NpgsqlConnection, int?, int, CancellationToken)"/> for the label selection and its order, which is what keeps a projected read and the full read reporting the same dictionary.</para>
        /// </summary>
        /// <param name="countyId">The identifier of the county row to read; if null, labels across all counties are retrieved.</param>
        /// <param name="commandTimeout">The timeout in seconds for the execution of the command. A value of 0 disables the timeout.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the year of each labelled reference held, empty when the county holds no label, or null when the connection could not be created.</returns>
        public async Task<Dictionary<string, short>?> GetUserYearBuiltsByCountyIdAsync(int? countyId, int commandTimeout = 30, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection? npgsqlConnection = DiGi.PostgreSQL.Create.NpgsqlConnection(ConnectionData);
            if (npgsqlConnection is null)
            {
                return null;
            }

            await npgsqlConnection.OpenAsync(cancellationToken);

            return await GetUserYearBuiltsByCountyIdAsync(npgsqlConnection, countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
        }
    }
}