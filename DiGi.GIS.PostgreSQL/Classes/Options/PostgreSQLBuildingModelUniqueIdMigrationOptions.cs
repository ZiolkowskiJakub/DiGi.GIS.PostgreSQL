// TODO [BuildingModelRowIdentity]: this whole file is temporary and exists only for the one-off
// unique_id migration of issue ZiolkowskiJakub/DiGi.GIS.PostgreSQL#5. Delete it once every deployed
// database has run PostgreSQLBuildingModelUniqueIdMigrationTask and it reports zero pending rows
// nationally.

using DiGi.Core.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// [TEMPORARY] Provides the options for <see cref="PostgreSQLBuildingModelUniqueIdMigrationTask"/>.
    /// <para>Scope is expressed the same two ways as every other national task - by county row identifier and by voivodeship code - and both filters have to admit a row. A null filter admits everything, which is what makes an unscoped national pass the default.</para>
    /// <para>Temporary - see the note at the top of this file.</para>
    /// </summary>
    public class PostgreSQLBuildingModelUniqueIdMigrationOptions : SerializableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLBuildingModelUniqueIdMigrationOptions"/> class.
        /// </summary>
        public PostgreSQLBuildingModelUniqueIdMigrationOptions()
        {
        }

        /// <summary>
        /// Gets or sets the identifiers of the county rows to migrate. When null every county row is migrated.
        /// <para>These are polygon parts, not counties - a multi-part county holds one row per part and each is migrated on its own.</para>
        /// </summary>
        public IEnumerable<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether the task only reports what it would do. Defaults to <see langword="false"/>, so it writes.
        /// <para>Armed by default because it destroys nothing: the value it overwrites is the building's reference, which the row still carries in its own <c>reference</c> column. Turning it on gives a report-only pass, which is worth doing once before a national run to see the blocked and missing counts.</para>
        /// </summary>
        public bool DryRun { get; set; } = false;

        /// <summary>
        /// Gets or sets the two-digit voivodeship codes to migrate. A county row is in scope when its code starts with one of them. When null every voivodeship is migrated.
        /// </summary>
        public IEnumerable<string>? VoivodeshipCodes { get; set; } = null;
    }
}
