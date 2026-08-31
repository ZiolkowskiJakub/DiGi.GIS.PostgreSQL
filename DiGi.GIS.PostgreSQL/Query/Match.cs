using DiGi.GIS.Classes;
using DiGi.GIS.Enums;
using DiGi.GIS.PostgreSQL.Classes;
using DiGi.GIS.PostgreSQL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Finds the <see cref="StatisticalUnit"/> matching the specified PostgreSQL <see cref="Classes.AdministrativeAreal2D"/> within the provided root statistical unit hierarchy.
        /// </summary>
        /// <param name="rootStatisticalUnit">The root statistical unit hierarchy.</param>
        /// <param name="administrativeAreal2D">The administrative area to match.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static StatisticalUnit? Match(this StatisticalUnit? rootStatisticalUnit, Classes.AdministrativeAreal2D? administrativeAreal2D)
        {
            if (rootStatisticalUnit is null || administrativeAreal2D is null)
            {
                return null;
            }

            return Match(rootStatisticalUnit, administrativeAreal2D.Name, administrativeAreal2D.Code, administrativeAreal2D.AdministrativeArealType);
        }

        /// <summary>
        /// Finds the <see cref="StatisticalUnit"/> matching the specified <see cref="AdministrativeAreal2DReference"/> within the provided root statistical unit hierarchy.
        /// </summary>
        /// <param name="rootStatisticalUnit">The root statistical unit hierarchy.</param>
        /// <param name="administrativeAreal2DReference">The administrative area reference to match.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static StatisticalUnit? Match(this StatisticalUnit? rootStatisticalUnit, AdministrativeAreal2DReference? administrativeAreal2DReference)
        {
            if (rootStatisticalUnit is null || administrativeAreal2DReference is null)
            {
                return null;
            }

            return Match(rootStatisticalUnit, administrativeAreal2DReference.Name, administrativeAreal2DReference.Code, administrativeAreal2DReference.AdministrativeArealType);
        }

        /// <summary>
        /// Finds the <see cref="StatisticalUnit"/> matching the specified GIS Core <see cref="GIS.Classes.AdministrativeAreal2D"/> within the provided root statistical unit hierarchy.
        /// </summary>
        /// <param name="rootStatisticalUnit">The root statistical unit hierarchy.</param>
        /// <param name="administrativeAreal2D">The GIS administrative area to match.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static StatisticalUnit? Match(this StatisticalUnit? rootStatisticalUnit, GIS.Classes.AdministrativeAreal2D? administrativeAreal2D)
        {
            if (rootStatisticalUnit is null || administrativeAreal2D is null)
            {
                return null;
            }

            AdministrativeArealType? administrativeArealType = administrativeAreal2D switch
            {
                GIS.Classes.AdministrativeDivision administrativeDivision => (AdministrativeArealType?)administrativeDivision.AdministrativeDivisionType,
                GIS.Classes.AdministrativeSubdivision => Enums.AdministrativeArealType.Subdivision,
                _ => null
            };

            if (administrativeArealType is null)
            {
                return null;
            }

            return Match(rootStatisticalUnit, administrativeAreal2D.Name, administrativeAreal2D.Code, administrativeArealType.Value);
        }

        /// <summary>
        /// Finds the <see cref="StatisticalUnit"/> matching the leaf reference in the specified <see cref="AdministrativeAreal2DReferencePath"/> within the hierarchy.
        /// </summary>
        /// <param name="rootStatisticalUnit">The root statistical unit hierarchy.</param>
        /// <param name="administrativeAreal2DReferencePath">The reference path containing the territorial ancestor chain.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static StatisticalUnit? Match(this StatisticalUnit? rootStatisticalUnit, AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath)
        {
            if (rootStatisticalUnit is null || administrativeAreal2DReferencePath is null)
            {
                return null;
            }

            List<AdministrativeAreal2DReference> references = administrativeAreal2DReferencePath.AdministrativeAreal2DReferences;
            if (references.Count == 0)
            {
                return null;
            }

            AdministrativeAreal2DReference leaf = references[references.Count - 1];
            return Match(rootStatisticalUnit, leaf);
        }

        /// <summary>
        /// Finds the <see cref="StatisticalUnit"/> matching the specified territorial name, code, and administrative type.
        /// </summary>
        /// <param name="rootStatisticalUnit">The root statistical unit hierarchy.</param>
        /// <param name="name">The territorial entity name.</param>
        /// <param name="code">The territorial entity code.</param>
        /// <param name="administrativeArealType">The administrative area type.</param>
        /// <returns>The matching <see cref="StatisticalUnit"/> if found; otherwise, null.</returns>
        public static StatisticalUnit? Match(this StatisticalUnit? rootStatisticalUnit, string? name, string? code, Enums.AdministrativeArealType administrativeArealType)
        {
            if (rootStatisticalUnit is null)
            {
                return null;
            }

            if (administrativeArealType == Enums.AdministrativeArealType.Country)
            {
                return rootStatisticalUnit;
            }

            string cleanName = name?.Trim().ToUpperInvariant() ?? string.Empty;
            string cleanCode = code?.Trim() ?? string.Empty;

            if (administrativeArealType == Enums.AdministrativeArealType.Voivodeship)
            {
                IEnumerable<StatisticalUnit>? voivodeships = rootStatisticalUnit.GetStatisticalUnits(includeNested: true, x => x?.GetStatisticalUnitType() == StatisticalUnitType.voivedships);
                if (voivodeships is null)
                {
                    return null;
                }

                foreach (StatisticalUnit voivodeship in voivodeships)
                {
                    string vName = voivodeship.Name?.Trim().ToUpperInvariant() ?? string.Empty;
                    string? vPrefix = voivodeship.UnitCode?.GetPrefix();
                    string? vFullCode = voivodeship.UnitCode?.Code;

                    if (!string.IsNullOrWhiteSpace(cleanCode))
                    {
                        if (vPrefix is not null && (cleanCode.StartsWith(vPrefix) || vPrefix.StartsWith(cleanCode) || vPrefix.EndsWith(cleanCode)))
                        {
                            return voivodeship;
                        }

                        if (vFullCode is not null && vFullCode.Length >= 4 && cleanCode.Length == 2 && vFullCode.Substring(2, 2) == cleanCode)
                        {
                            return voivodeship;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(cleanName) && (vName == cleanName || vName == "WOJEWÓDZTWO " + cleanName || cleanName.EndsWith(vName) || vName.EndsWith(cleanName)))
                    {
                        return voivodeship;
                    }
                }

                return null;
            }

            if (administrativeArealType == Enums.AdministrativeArealType.County)
            {
                IEnumerable<StatisticalUnit>? counties = rootStatisticalUnit.GetStatisticalUnits(includeNested: true, x => x?.GetStatisticalUnitType() == StatisticalUnitType.counties);
                if (counties is null)
                {
                    return null;
                }

                foreach (StatisticalUnit county in counties)
                {
                    string cName = county.Name?.Trim().ToUpperInvariant() ?? string.Empty;
                    int indexOD = cName.IndexOf(" OD ", StringComparison.Ordinal);
                    int indexDO = cName.IndexOf(" DO ", StringComparison.Ordinal);
                    if (indexOD > 0 || indexDO > 0)
                    {
                        indexOD = indexOD == -1 ? indexDO : indexOD;
                        indexDO = indexDO == -1 ? indexOD : indexDO;
                        cName = cName.Substring(0, Math.Min(indexOD, indexDO)).Trim();
                    }

                    string expectedPowiat = "POWIAT " + cleanName;
                    string expectedPowiatM = "POWIAT M. " + cleanName;

                    if (cName == expectedPowiat || cName == expectedPowiatM || cName == cleanName || cName == "M. " + cleanName)
                    {
                        return county;
                    }

                    string? cFullCode = county.UnitCode?.Code;
                    if (!string.IsNullOrWhiteSpace(cleanCode))
                    {
                        if (county.UnitCode?.GetPrefix() is string cPrefix && (cleanCode.StartsWith(cPrefix) || cPrefix.StartsWith(cleanCode)))
                        {
                            return county;
                        }

                        // Map TERYT 4-digit code (Voivodeship 2 digits + County 2 digits) to BDL 9-digit unit code
                        if (cFullCode is not null && cFullCode.Length >= 9 && cleanCode.Length == 4)
                        {
                            string terytSlice = cFullCode.Substring(2, 2) + cFullCode.Substring(7, 2);
                            if (terytSlice == cleanCode)
                            {
                                return county;
                            }
                        }
                    }
                }

                foreach (StatisticalUnit county in counties)
                {
                    string cName = county.Name?.Trim().ToUpperInvariant() ?? string.Empty;
                    int indexOD = cName.IndexOf(" OD ", StringComparison.Ordinal);
                    int indexDO = cName.IndexOf(" DO ", StringComparison.Ordinal);
                    if (indexOD > 0 || indexDO > 0)
                    {
                        indexOD = indexOD == -1 ? indexDO : indexOD;
                        indexDO = indexDO == -1 ? indexOD : indexDO;
                        cName = cName.Substring(0, Math.Min(indexOD, indexDO)).Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(cleanName) && (cName.EndsWith(cleanName) || cleanName.EndsWith(cName.Replace("POWIAT ", "").Trim())))
                    {
                        return county;
                    }
                }

                return null;
            }

            if (administrativeArealType == Enums.AdministrativeArealType.Municipality)
            {
                IEnumerable<StatisticalUnit>? municipalities = rootStatisticalUnit.GetStatisticalUnits(includeNested: true, x => x?.GetStatisticalUnitType() == StatisticalUnitType.municipalities);
                if (municipalities is null)
                {
                    return null;
                }

                string normalizedAdminName = cleanName;
                if (normalizedAdminName.StartsWith("M. ST."))
                {
                    normalizedAdminName = normalizedAdminName.Replace(" ", string.Empty) + " OD 2002";
                }
                else if (normalizedAdminName.StartsWith("M."))
                {
                    normalizedAdminName = normalizedAdminName.Substring(2).Trim();
                }

                foreach (StatisticalUnit municipality in municipalities)
                {
                    string mName = municipality.Name?.Trim().ToUpperInvariant() ?? string.Empty;
                    MunicipalityType? municipalityType = municipality.MunicipalityType();

                    if (municipalityType.HasValue)
                    {
                        if (normalizedAdminName.EndsWith("(GM. MIEJSKA)") && GIS.Query.IsUrban(municipalityType.Value))
                        {
                            mName = $"{mName} (GM. MIEJSKA)";
                        }
                        else if (normalizedAdminName.EndsWith("(GM. WIEJSKA)") && !GIS.Query.IsUrban(municipalityType.Value))
                        {
                            mName = $"{mName} (GM. WIEJSKA)";
                        }
                    }

                    if (mName == normalizedAdminName || mName == cleanName)
                    {
                        return municipality;
                    }

                    string? mFullCode = municipality.UnitCode?.Code;
                    if (!string.IsNullOrWhiteSpace(cleanCode))
                    {
                        if (municipality.UnitCode?.GetPrefix() is string mPrefix && (cleanCode.StartsWith(mPrefix) || mPrefix.StartsWith(cleanCode)))
                        {
                            return municipality;
                        }

                        // Map TERYT 7-digit code (Voivodeship 2 + County 2 + Municipality 3) to BDL 12-digit unit code
                        if (mFullCode is not null && mFullCode.Length == 12 && cleanCode.Length == 7)
                        {
                            string terytSlice = mFullCode.Substring(2, 2) + mFullCode.Substring(7, 2) + mFullCode.Substring(9, 3);
                            if (terytSlice == cleanCode)
                            {
                                return municipality;
                            }
                        }
                    }
                }

                int parenStart = normalizedAdminName.LastIndexOf('(');
                if (parenStart > 0)
                {
                    string baseName = normalizedAdminName.Substring(0, parenStart).Trim();
                    foreach (StatisticalUnit municipality in municipalities)
                    {
                        string mName = municipality.Name?.Trim().ToUpperInvariant() ?? string.Empty;
                        if (mName == baseName)
                        {
                            return municipality;
                        }
                    }
                }

                return null;
            }

            if (administrativeArealType == Enums.AdministrativeArealType.Subdivision)
            {
                IEnumerable<StatisticalUnit>? candidates = rootStatisticalUnit.GetStatisticalUnits(includeNested: true, x =>
                {
                    string cName = x?.Name?.Trim().ToUpperInvariant() ?? string.Empty;
                    return !string.IsNullOrWhiteSpace(cleanName) && (cName == cleanName || cName.StartsWith(cleanName) || cleanName.StartsWith(cName));
                });

                return candidates?.FirstOrDefault();
            }

            return null;
        }
    }
}
