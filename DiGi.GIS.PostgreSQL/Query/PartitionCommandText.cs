namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Builds the statement that creates one county partition of a list-partitioned table and leaves it with statistics.
        /// <para>A partition PostgreSQL has never analysed reports <c>reltuples = -1</c>, and an empty one stays that way for good - autovacuum analyses on modifications, and nothing ever modifies it. Every estimated count then reads it as "not measured" rather than as zero, and one such partition voids the aggregate of the whole voivodeship it sits in. The statement therefore analyses the partition right after creating it, and only then: the check on <c>reltuples</c> keeps the analyse off the path of every later write, which creates nothing and would otherwise pay for a statistics pass over the whole partition on each batch.</para>
        /// </summary>
        /// <param name="tableName">The name of the partitioned table.</param>
        /// <param name="countyId">The identifier of the county the partition holds; it names the partition and is its single list value.</param>
        /// <returns>The SQL text creating the partition if it is absent and analysing it if it has never been analysed.</returns>
        public static string PartitionCommandText(string tableName, int countyId)
        {
            string partitionName = $"{tableName}_{countyId}";

            return $@"
                CREATE TABLE IF NOT EXISTS {partitionName} PARTITION OF {tableName}
                    FOR VALUES IN ({countyId});

                DO $$
                BEGIN
                    IF (SELECT reltuples FROM pg_class WHERE oid = to_regclass('{partitionName}')) < 0 THEN
                        ANALYZE {partitionName};
                    END IF;
                END $$;
                ";
        }
    }
}
