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

<a name='DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Subdivison'></a>

`Subdivison` 4

Subdivision level administrative area\.

The member name is misspelled (`Subdivison`, missing the second `i`) and the misspelling reaches the wire: a request carrying the correctly spelled `Subdivision` is rejected with HTTP 400. Pass the integer `4`, or the exact misspelling. Renaming this member is a breaking API change.

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