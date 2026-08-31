#### [DiGi\.GIS\.PostgreSQL](DiGi.GIS.PostgreSQL.Overview.md 'DiGi\.GIS\.PostgreSQL\.Overview')

## DiGi\.GIS\.PostgreSQL\.Enums Namespace
### Enums

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType'></a>

## AdministrativeArealType Enum

Represents the type of administrative area\.

The value is the `type_id` column of `administrative_areal_2d` and travels the wire as an <b>integer</b>. One row is stored per polygon part of a unit, so a level holds more rows than there are real units - counties are 406 rows for 380 codes, and both country and voivodeship are 406 rows because every county part carries its own ancestor chain.

```csharp
public enum AdministrativeArealType
```
### Fields

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Undefined'></a>

`Undefined` -1

Undefined administrative area type\.

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Country'></a>

`Country` 0

Country level administrative area\.

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Voivodeship'></a>

`Voivodeship` 1

Voivodeship \(province\) level administrative area\.

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.County'></a>

`County` 2

County \(powiat\) level administrative area\.

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Municipality'></a>

`Municipality` 3

Municipality \(gmina\) level administrative area\.

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Subdivision'></a>

`Subdivision` 4

Subdivision level administrative area\.

Renamed from the misspelled `Subdivison` (missing the second `i`), which was the accepted wire token until this build. `Subdivison` is now <b>rejected</b> with HTTP 400 - send `Subdivision` or the integer `4`. The value is unchanged, so nothing stored under `type_id` is affected.

<a name='DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType'></a>

## BuildingDataUpdateType Enum

Specifies the building data update type\.

```csharp
public enum BuildingDataUpdateType
```
### Fields

<a name='DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType.General'></a>

`General` 0

General data update type, which mostly includes Building2D\.

<a name='DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType.Database'></a>

`Database` 1

Database data update type\.

<a name='DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType.Occupancy'></a>

`Occupancy` 2

Occupancy data update\.

<a name='DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType.RadialRatios'></a>

`RadialRatios` 3

Radial ratios \(Radial Building Coverage Ratio, Radial Floor Area Ratio\) update\.

<a name='DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType.Statistical'></a>

`Statistical` 4

Statistical demographic data update\.

<a name='DiGi.GIS.PostgreSQL.Enums.UpdateRejectionReason'></a>

## UpdateRejectionReason Enum

Names why a row handed to an update never reached the database\.

A row that cannot be filed under a county part is dropped rather than written, which used to leave no trace at all: the caller received only the identifiers of the rows that were stored, so a batch that stored one of five thousand looked exactly like a batch that stored all of them. The reason is carried alongside the reference because it decides what the caller should do next - a payload defect is worth correcting and reposting, a footprint that falls outside every candidate part is not.

```csharp
public enum UpdateRejectionReason
```
### Fields

<a name='DiGi.GIS.PostgreSQL.Enums.UpdateRejectionReason.Undefined'></a>

`Undefined` 0

The element itself was null, so there is nothing to name\.

<a name='DiGi.GIS.PostgreSQL.Enums.UpdateRejectionReason.MissingGeometry'></a>

`MissingGeometry` 1

The row carries no bounding box, so no county could even be attempted\. A defect in the posted payload\.

<a name='DiGi.GIS.PostgreSQL.Enums.UpdateRejectionReason.CountyUnresolved'></a>

`CountyUnresolved` 2

County resolution ran and named no part\. Not necessarily the caller's fault \- the last tier decides by geometry, and a footprint falling outside every candidate part lands here\.

<a name='DiGi.GIS.PostgreSQL.Enums.UpdateRejectionReason.PartitionUnavailable'></a>

`PartitionUnavailable` 3

The county resolved, but its partition could not be created, so every row filed under it was dropped\. Server\-side\.