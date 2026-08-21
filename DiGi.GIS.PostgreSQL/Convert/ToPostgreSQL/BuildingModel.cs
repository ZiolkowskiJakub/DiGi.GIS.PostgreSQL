using DiGi.Analytical.Building;
using DiGi.Analytical.Building.Interfaces;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the specified analytical building model to a PostgreSQL-compatible building model object, reading the reference from the building model parameters and taking the county identifier as an argument.
        /// <para>The row carries the identifier of the <b>model</b> in <c>UniqueId</c> and the reference of the 2D building it describes in <c>Reference</c>, which is the addressing convention every referenced-object table follows - see <see cref="Building2DReferencedObject{TUniqueObject}"/>. <c>(CountyId, Reference)</c> addresses everything held for the building; <c>UniqueId</c> addresses this one model within it.</para>
        /// <para>A model is handed a fresh <see cref="System.Guid"/> whenever one is created, so a regenerated model carries a new identifier and is stored <b>beside</b> the one the building already had rather than replacing it. That is the intended behaviour of the table, and it makes replacing a building's model the caller's job: remove what the building holds, then write. It is not a reason to key the row on the reference instead - that pins the table to one row per building and discards every record after the first.</para>
        /// </summary>
        /// <param name="buildingModel">The analytical building model to convert.</param>
        /// <param name="countyId">The identifier of the county the building model belongs to, resolved by the caller from the administrative area code.</param>
        /// <returns>A <see cref="BuildingModel" /> object if the provided building model is not null and carries both the <see cref="Analytical.Enums.BuildingModelParameter.Reference"/> parameter value and its own unique identifier; otherwise, null.</returns>
        public static BuildingModel? ToPostgreSQL(this DiGi.Analytical.Building.Classes.BuildingModel? buildingModel, int? countyId = null)
        {
            if (buildingModel is null)
            {
                return null;
            }

            if (!buildingModel.TryGetValue(Analytical.Enums.BuildingModelParameter.Reference, out string? reference) || string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            // The column is NOT NULL and is half of what addresses the row, so a model that cannot state its
            // own identifier has nowhere to be stored. It should not be reachable - the identifier comes from
            // GuidObject and is always there - which is exactly why it is worth refusing rather than writing
            // a row that nothing can address afterwards.
            string? uniqueId = buildingModel.UniqueId;
            if (string.IsNullOrWhiteSpace(uniqueId))
            {
                return null;
            }

            // Last gate before the database. A component sitting on a non-finite plane cannot be rendered
            // or measured, and once stored it is no longer traceable to the file it came from - the state
            // that filled the building model table with unusable rows. Such a model is not converted, so
            // the caller counts it as not written rather than persisting it.
            if (!buildingModel.IsValid())
            {
                return null;
            }

            BuildingModel result = new()
            {
                Reference = reference,
                Object = buildingModel.ToJsonObject(),
                UniqueId = uniqueId,
                CountyId = countyId
            };

            return result;
        }
    }
}
