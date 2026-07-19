using DiGi.Analytical.Building.Interfaces;
using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.GIS.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Attempts to parse a reference string into its constituent building model reference, optional county identifier, and optional building element GUID reference.
        /// <para>The method delegates to <see cref="Core.Query.TryParse(string, out IReference)"/> to deserialize the string. A plain non-parsable string is treated as a bare building model reference. For <see cref="ComplexReference"/> values, the individual segments are extracted by matching <see cref="TypeReference"/> discriminators for <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/>, <see cref="AdministrativeDivision"/>, and <see cref="IBuildingGuidObject"/>-assignable types.</para>
        /// </summary>
        /// <param name="reference">The reference string to parse.</param>
        /// <param name="buildingModelReference">When this method returns, contains the extracted building model reference string. Set to <see cref="string.Empty"/> if parsing fails.</param>
        /// <param name="countyId">When this method returns, contains the county identifier if one was found in the reference chain; otherwise, <see langword="null"/>.</param>
        /// <param name="buildingObjectGuidReference">When this method returns, contains the <see cref="GuidReference"/> for a building element if one was found in the reference chain; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the reference was successfully parsed or treated as a bare building model reference; <see langword="false"/> if the input is <see langword="null"/>, empty, or the parsed structure cannot be resolved.</returns>
        public static bool TryParse(this string? reference, out string buildingModelReference, out int? countyId, out GuidReference? buildingObjectGuidReference)
        {
            buildingModelReference = string.Empty;
            countyId = null;
            buildingObjectGuidReference = null;

            if (string.IsNullOrWhiteSpace(reference))
            {
                return false;
            }

            if(!Core.Query.TryParse(reference, out IReference? reference_Temp) || reference_Temp is null)
            {
                buildingModelReference = reference;
                return true;
            }

            if(reference_Temp is IUniqueReference uniqueReference)
            {
                if(uniqueReference.UniqueId is string uniqueId && !string.IsNullOrWhiteSpace(uniqueId))
                {
                    buildingModelReference = uniqueId;
                    return true;
                }

                return false;
            }

            if(reference_Temp is not ComplexReference complexReference)
            {
                // Core.Query.TryParse accepts a bare, discriminator-less string by reading it as a legacy
                // TypeReference (see Core.Query.TryParseLegacy), so a plain building reference parses to a
                // non-unique, non-complex reference. Treat such a string as the plain building reference it is.
                buildingModelReference = reference;
                return true;
            }

            if (complexReference.References is not List<ISerializableReference> serializableReferences || serializableReferences.Count == 0)
            {
                return false;
            }

            var uniqueReference_BuildingModel = serializableReferences.Find(x => x is UniqueIdReference uniqueIdReference && uniqueIdReference.TypeReference == new TypeReference(typeof(DiGi.Analytical.Building.Classes.BuildingModel))) as UniqueIdReference;
            if (uniqueReference_BuildingModel is null || uniqueReference_BuildingModel.UniqueId is not string uniqueId_BuildingModel || string.IsNullOrWhiteSpace(uniqueId_BuildingModel))
            {
                return false;
            }

            buildingModelReference = uniqueId_BuildingModel;

            UniqueIdReference? uniqueReference_CountyId = serializableReferences.Find(x => x is UniqueIdReference uniqueIdReference && uniqueIdReference.TypeReference == new TypeReference(typeof(AdministrativeDivision))) as UniqueIdReference;
            if (uniqueReference_CountyId is not null && uniqueReference_CountyId.UniqueId is string uniqueId_CountyId && !string.IsNullOrWhiteSpace(uniqueId_CountyId) && int.TryParse(uniqueId_CountyId, out int countId_Temp))
            {
                countyId = countId_Temp;
            }

            buildingObjectGuidReference = serializableReferences.Find(x => x is GuidReference guidReference && guidReference.TypeReference is not null && typeof(IBuildingGuidObject).IsAssignableFrom(guidReference.TypeReference.Type())) as GuidReference;

            return true;
        }

    }
}
