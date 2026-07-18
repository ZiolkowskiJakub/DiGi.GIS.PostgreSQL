using DiGi.GIS.PostgreSQL.Classes;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the specified analytical building model to a PostgreSQL-compatible building model object, reading the reference and county identifier from the building model parameters.
        /// </summary>
        /// <param name="buildingModel">The analytical building model to convert.</param>
        /// <returns>A <see cref="BuildingModel" /> object if the provided building model is not null and carries the <see cref="Analytical.Enums.BuildingModelParameter.Reference"/> and <see cref="Analytical.Enums.BuildingModelParameter.CountyId"/> parameter values; otherwise, null.</returns>
        public static BuildingModel? ToPostgreSQL(this DiGi.Analytical.Building.Classes.BuildingModel? buildingModel)
        {
            if (buildingModel is null)
            {
                return null;
            }

            //TODO: Use ComplexReference instead CountyId

            if (!buildingModel.TryGetValue<string>(Analytical.Enums.BuildingModelParameter.Reference, out string? reference))
            {
                return null;
            }

            if (!buildingModel.TryGetValue<int?>(Analytical.Enums.BuildingModelParameter.CountyId, out int? countyId))
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