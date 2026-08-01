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
        /// </summary>
        /// <param name="buildingModel">The analytical building model to convert.</param>
        /// <param name="countyId">The identifier of the county the building model belongs to, resolved by the caller from the administrative area code.</param>
        /// <returns>A <see cref="BuildingModel" /> object if the provided building model is not null and carries the <see cref="Analytical.Enums.BuildingModelParameter.Reference"/> parameter value; otherwise, null.</returns>
        public static BuildingModel? ToPostgreSQL(this DiGi.Analytical.Building.Classes.BuildingModel? buildingModel, int? countyId = null)
        {
            if (buildingModel is null)
            {
                return null;
            }

            if (!buildingModel.TryGetValue(Analytical.Enums.BuildingModelParameter.Reference, out string? reference))
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
                UniqueId = buildingModel.UniqueId,
                CountyId = countyId
            };

            return result;
        }
    }
}