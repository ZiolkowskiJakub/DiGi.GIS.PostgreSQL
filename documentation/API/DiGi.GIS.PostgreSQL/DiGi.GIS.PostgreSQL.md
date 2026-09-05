#### [DiGi\.GIS\.PostgreSQL](DiGi.GIS.PostgreSQL.Overview.md 'DiGi\.GIS\.PostgreSQL\.Overview')

## DiGi\.GIS\.PostgreSQL Namespace
### Classes

<a name='DiGi.GIS.PostgreSQL.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__)'></a>

## Convert\.ToDiGi\<TSerializableObject\>\(this IEnumerable\<ITableSerializableObject\<TSerializableObject\>\>\) Method

Converts a collection of table\-serializable objects \(database row envelopes\) to the DiGi serializable objects they wrap, skipping entries that cannot be converted\.

```csharp
public static System.Collections.Generic.List<TSerializableObject>? ToDiGi<TSerializableObject>(this System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject<TSerializableObject>>? tableSerializableObjects)
    where TSerializableObject : DiGi.Core.Interfaces.ISerializableObject;
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__).TSerializableObject'></a>

`TSerializableObject`

The type of the wrapped DiGi serializable object\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__).tableSerializableObjects'></a>

`tableSerializableObjects` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')[TSerializableObject](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__).TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Convert\.ToDiGi\<TSerializableObject\>\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\>\)\.TSerializableObject')[&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of [ITableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>') instances to convert\. This value can be null\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TSerializableObject](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__).TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Convert\.ToDiGi\<TSerializableObject\>\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\>\)\.TSerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [TSerializableObject](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__).TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Convert\.ToDiGi\<TSerializableObject\>\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\>\)\.TSerializableObject') instances, or null if [tableSerializableObjects](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Convert.ToDiGi_TSerializableObject_(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject__).tableSerializableObjects 'DiGi\.GIS\.PostgreSQL\.Convert\.ToDiGi\<TSerializableObject\>\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\>\)\.tableSerializableObjects') is null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.Analytical.Building.Classes.BuildingModel,System.Nullable_int_)'></a>

## Convert\.ToPostgreSQL\(this BuildingModel, Nullable\<int\>\) Method

Converts the specified analytical building model to a PostgreSQL\-compatible building model object, reading the reference from the building model parameters and taking the county identifier as an argument\.

The row carries the identifier of the <b>model</b> in `UniqueId` and the reference of the 2D building it describes in `Reference`, which is the addressing convention every referenced-object table follows - see [Building2DReferencedObject&lt;TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>'). `(CountyId, Reference)` addresses everything held for the building; `UniqueId` addresses this one model within it.

A model is handed a fresh [System\.Guid](https://learn.microsoft.com/en-us/dotnet/api/system.guid 'System\.Guid') whenever one is created, so a regenerated model carries a new identifier and is stored <b>beside</b> the one the building already had rather than replacing it. That is the intended behaviour of the table, and it makes replacing a building's model the caller's job: remove what the building holds, then write. It is not a reason to key the row on the reference instead - that pins the table to one row per building and discards every record after the first.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.BuildingModel? ToPostgreSQL(this DiGi.Analytical.Building.Classes.BuildingModel? buildingModel, System.Nullable<int> countyId=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.Analytical.Building.Classes.BuildingModel,System.Nullable_int_).buildingModel'></a>

`buildingModel` [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')

The analytical building model to convert\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.Analytical.Building.Classes.BuildingModel,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The identifier of the county the building model belongs to, resolved by the caller from the administrative area code\.

#### Returns
[BuildingModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingModel 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingModel')  
A [BuildingModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingModel 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingModel') object if the provided building model is not null and carries both the [DiGi\.GIS\.Analytical\.Enums\.BuildingModelParameter\.Reference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.analytical.enums.buildingmodelparameter.reference 'DiGi\.GIS\.Analytical\.Enums\.BuildingModelParameter\.Reference') parameter value and its own unique identifier; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.CityGML.Classes.Building,System.Nullable_int_)'></a>

## Convert\.ToPostgreSQL\(this Building, Nullable\<int\>\) Method

Converts a CityGML Building instance to a PostgreSQL\-compatible Building instance\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.Building? ToPostgreSQL(this DiGi.CityGML.Classes.Building? building, System.Nullable<int> countyId=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.CityGML.Classes.Building,System.Nullable_int_).building'></a>

`building` [DiGi\.CityGML\.Classes\.Building](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.building 'DiGi\.CityGML\.Classes\.Building')

The source CityGML building object to convert\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.CityGML.Classes.Building,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional county identifier associated with the building\.

#### Returns
[Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building')  
A converted [Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building') instance, or null if the input is null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.AdministrativeAreal2D)'></a>

## Convert\.ToPostgreSQL\(this AdministrativeAreal2D\) Method

Converts a GIS administrative areal 2D object to its PostgreSQL representation\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? ToPostgreSQL(this DiGi.GIS.Classes.AdministrativeAreal2D? administrativeAreal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.AdministrativeAreal2D).administrativeAreal2D'></a>

`administrativeAreal2D` [DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D')

The source administrative areal 2D object to convert\.

#### Returns
[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')  
The converted [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') object, or null if the input is null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.Building2D,string)'></a>

## Convert\.ToPostgreSQL\(this Building2D, string\) Method

Converts a GIS Building2D instance to a PostgreSQL\-compatible Building2D instance\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.Building2D? ToPostgreSQL(this DiGi.GIS.Classes.Building2D? building2D, string? code=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.Building2D,string).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The source building 2D object to convert\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.Building2D,string).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional code associated with the building\.

#### Returns
[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')  
A converted [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') instance, or null if the input is null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_)'></a>

## Convert\.ToPostgreSQL\(this OrtoDatas, Nullable\<int\>\) Method

Converts a GIS OrtoDatas instance to a PostgreSQL\-compatible OrtoDatas instance\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.OrtoDatas? ToPostgreSQL(this DiGi.GIS.Classes.OrtoDatas? ortoDatas, System.Nullable<int> countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_).ortoDatas'></a>

`ortoDatas` [DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')

The source GIS OrtoDatas object to convert\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional county identifier associated with the data\.

#### Returns
[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')  
A new PostgreSQL\-compatible [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') instance, or null if the input is null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_,System.Nullable_int_)'></a>

## Convert\.ToPostgreSQL\(this OrtoDatas, Nullable\<int\>, Nullable\<int\>\) Method

Converts a GIS OrtoDatas instance to a PostgreSQL\-compatible OrtoDatas instance with a specified subdivision identifier\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.OrtoDatas? ToPostgreSQL(this DiGi.GIS.Classes.OrtoDatas? ortoDatas, System.Nullable<int> countyId, System.Nullable<int> subdivisionId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_,System.Nullable_int_).ortoDatas'></a>

`ortoDatas` [DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')

The source GIS OrtoDatas object to convert\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional county identifier associated with the data\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Classes.OrtoDatas,System.Nullable_int_,System.Nullable_int_).subdivisionId'></a>

`subdivisionId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional subdivision identifier associated with the data\.

#### Returns
[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')  
A new PostgreSQL\-compatible [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') instance, or null if the input is null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IOccupancyData)'></a>

## Convert\.ToPostgreSQL\(this IOccupancyData\) Method

Converts the specified occupancy data to a PostgreSQL\-compatible administrative areal 2D occupancy data object\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData? ToPostgreSQL(this DiGi.GIS.Interfaces.IOccupancyData? occupancyData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IOccupancyData).occupancyData'></a>

`occupancyData` [DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')

The occupancy data to convert\.

#### Returns
[AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData')  
A [AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData') instance if the provided occupancy data is not null; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IOccupancyData,System.Nullable_int_)'></a>

## Convert\.ToPostgreSQL\(this IOccupancyData, Nullable\<int\>\) Method

Converts the specified occupancy data to a PostgreSQL\-compatible building 2D occupancy data object\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData? ToPostgreSQL(this DiGi.GIS.Interfaces.IOccupancyData? occupancyData, System.Nullable<int> countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IOccupancyData,System.Nullable_int_).occupancyData'></a>

`occupancyData` [DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')

The occupancy data to convert\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IOccupancyData,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional county identifier associated with the occupancy data\.

#### Returns
[Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')  
A [Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData') object if the provided occupancy data is not null; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IYearBuiltData,System.Nullable_int_)'></a>

## Convert\.ToPostgreSQL\(this IYearBuiltData, Nullable\<int\>\) Method

Converts the specified [DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData') instance to a [YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData') object for PostgreSQL storage\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.YearBuiltData? ToPostgreSQL(this DiGi.GIS.Interfaces.IYearBuiltData? yearBuiltData, System.Nullable<int> countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IYearBuiltData,System.Nullable_int_).yearBuiltData'></a>

`yearBuiltData` [DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData')

The source year built data\.

<a name='DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IYearBuiltData,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The county identifier associated with the data\.

#### Returns
[YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')  
A [YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData') instance if [yearBuiltData](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Convert.ToPostgreSQL(thisDiGi.GIS.Interfaces.IYearBuiltData,System.Nullable_int_).yearBuiltData 'DiGi\.GIS\.PostgreSQL\.Convert\.ToPostgreSQL\(this DiGi\.GIS\.Interfaces\.IYearBuiltData, System\.Nullable\<int\>\)\.yearBuiltData') is not null; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.GIS.PostgreSQL.Create.BuildingDataCoverageResultAsync(thisDiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken)'></a>

## Create\.BuildingDataCoverageResultAsync\(this BuildingDataPostgreSQLConverter, Building2DPostgreSQLConverter, int, int, CancellationToken\) Method

Asynchronously measures what one county's building data holds against the buildings that county actually has\.

The comparison is made on references read from each side rather than by a join, because the two tables are in different databases - `building_2d` in the main one and `building_data` in the storage one.

The reads run sequentially on their own connections without fanning out per building or per subdivision: a coverage read that opened a connection per item is what exhausted the pool the last time this shape was written.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.BuildingDataCoverageResult?> BuildingDataCoverageResultAsync(this DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter? buildingDataPostgreSQLConverter, DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter, int countyId, int commandTimeout=600, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.BuildingDataCoverageResultAsync(thisDiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).buildingDataPostgreSQLConverter'></a>

`buildingDataPostgreSQLConverter` [BuildingDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter')

The converter reading the building data side\.

<a name='DiGi.GIS.PostgreSQL.Create.BuildingDataCoverageResultAsync(thisDiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).building2DPostgreSQLConverter'></a>

`building2DPostgreSQLConverter` [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')

The converter reading the building side\.

<a name='DiGi.GIS.PostgreSQL.Create.BuildingDataCoverageResultAsync(thisDiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county to measure\.

<a name='DiGi.GIS.PostgreSQL.Create.BuildingDataCoverageResultAsync(thisDiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\. Defaults to 600 seconds\.

<a name='DiGi.GIS.PostgreSQL.Create.BuildingDataCoverageResultAsync(thisDiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[BuildingDataCoverageResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataCoverageResult 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataCoverageResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the coverage, or null when either converter is missing or either side could not be read\.

<a name='DiGi.GIS.PostgreSQL.Create.GISPostgreSQLConverterManager()'></a>

## Create\.GISPostgreSQLConverterManager\(\) Method

Creates a [GISPostgreSQLConverterManager\(\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.GISPostgreSQLConverterManager() 'DiGi\.GIS\.PostgreSQL\.Create\.GISPostgreSQLConverterManager\(\)') with all PostgreSQL converters registered\.
Reads connection configuration from `PostgreSQL_Main` and `PostgreSQL_Storage` files
in the executing assembly's directory\.

IMPORTANT: Every converter consumed by a GIS WebAPI controller (e.g. `BuildingController`,
`AdministrativeAreal2DController`) MUST be registered here. The WebAPI `InitializeAsync`
reads converters from the returned manager and adds them to the DI container. A missing
registration causes the controller's converter dependency to be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null'),
resulting in a 500 Internal Server Error at runtime.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager? GISPostgreSQLConverterManager();
```

#### Returns
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')  
A configured [GISPostgreSQLConverterManager\(\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.GISPostgreSQLConverterManager() 'DiGi\.GIS\.PostgreSQL\.Create\.GISPostgreSQLConverterManager\(\)') if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Create.Reference(DiGi.Analytical.Building.Classes.BuildingModel,DiGi.Analytical.Building.Interfaces.IBuildingGuidObject,System.Nullable_int_)'></a>

## Create\.Reference\(BuildingModel, IBuildingGuidObject, Nullable\<int\>\) Method

Creates a reference chain for the specified building model, optionally anchored to a county administrative division and a specific building element\.

The reference chain is ordered from the root of the containment hierarchy inwards: [DiGi\.GIS\.Classes\.AdministrativeDivision](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativedivision 'DiGi\.GIS\.Classes\.AdministrativeDivision') (if a county identifier is provided), [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') (by its [DiGi\.GIS\.Analytical\.Enums\.BuildingModelParameter\.Reference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.analytical.enums.buildingmodelparameter.reference 'DiGi\.GIS\.Analytical\.Enums\.BuildingModelParameter\.Reference') parameter value or a [DiGi\.Core\.Classes\.GuidReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidreference 'DiGi\.Core\.Classes\.GuidReference') fallback), and the optional [DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.ibuildingguidobject 'DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject') element. If the chain contains only a single entry, that entry is returned directly instead of wrapped in a [DiGi\.Core\.Classes\.ComplexReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.complexreference 'DiGi\.Core\.Classes\.ComplexReference').

```csharp
public static DiGi.Core.Interfaces.IReference? Reference(DiGi.Analytical.Building.Classes.BuildingModel buildingModel, DiGi.Analytical.Building.Interfaces.IBuildingGuidObject? buildingGuidObject=null, System.Nullable<int> countyId=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.Reference(DiGi.Analytical.Building.Classes.BuildingModel,DiGi.Analytical.Building.Interfaces.IBuildingGuidObject,System.Nullable_int_).buildingModel'></a>

`buildingModel` [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')

The analytical building model to create the reference for\.

<a name='DiGi.GIS.PostgreSQL.Create.Reference(DiGi.Analytical.Building.Classes.BuildingModel,DiGi.Analytical.Building.Interfaces.IBuildingGuidObject,System.Nullable_int_).buildingGuidObject'></a>

`buildingGuidObject` [DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.ibuildingguidobject 'DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject')

An optional specific building element \(e\.g\. component, space\) to include as the innermost reference in the chain\.

<a name='DiGi.GIS.PostgreSQL.Create.Reference(DiGi.Analytical.Building.Classes.BuildingModel,DiGi.Analytical.Building.Interfaces.IBuildingGuidObject,System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional county identifier that anchors the reference to an [DiGi\.GIS\.Classes\.AdministrativeDivision](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativedivision 'DiGi\.GIS\.Classes\.AdministrativeDivision') at the outermost level of the chain\.

#### Returns
[DiGi\.Core\.Interfaces\.IReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ireference 'DiGi\.Core\.Interfaces\.IReference')  
An [DiGi\.Core\.Interfaces\.IReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ireference 'DiGi\.Core\.Interfaces\.IReference') representing the containment chain, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if [buildingModel](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.Reference(DiGi.Analytical.Building.Classes.BuildingModel,DiGi.Analytical.Building.Interfaces.IBuildingGuidObject,System.Nullable_int_).buildingModel 'DiGi\.GIS\.PostgreSQL\.Create\.Reference\(DiGi\.Analytical\.Building\.Classes\.BuildingModel, DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject, System\.Nullable\<int\>\)\.buildingModel') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_AdministrativeArea2D\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the AdministrativeArea2D table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_AdministrativeArea2D(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_AdministrativeArea2DReferencedObject\(this NpgsqlConnection, string, int, CancellationToken\) Method

Asynchronously creates the AdministrativeArea2DReferencedObject table for the specified table name\.

`reference` is what every read of this table filters on, so it carries an index of its own. `unique_id` needs none: the `UNIQUE` constraint on it is already an index, and a second one on the same column would only cost storage and write time.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_AdministrativeArea2DReferencedObject(this Npgsql.NpgsqlConnection? npgsqlConnection, string tableName, int commandTimeout=600, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the table associated with the administrative area 2D referenced object\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\. TODO \[ReferencedObjectIndexes\]: the default is 600 rather than the 30 used elsewhere in this class, because on a table that predates the reference index the command has to build that index before it returns\. Once no deployed table needs a first build this is a catalog lookup again, and the default goes back to 30\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the partitioned [Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building') table along with its supporting composite index, if it does not already exist\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building2D\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the Building2D table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2D(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The Npgsql connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building2DReference\(this NpgsqlConnection, string, int, CancellationToken\) Method

Asynchronously creates the Building 2D reference table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2DReference(this Npgsql.NpgsqlConnection? npgsqlConnection, string? tableName, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the table to be created for Building 2D references\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used by other methods as a token for cancelling the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building2DReferencedObject\(this NpgsqlConnection, string, int, CancellationToken\) Method

Asynchronously creates the Building 2D Referenced Object table for the specified table name\.

The two constraints carry the addressing convention described on [Building2DReferencedObject&lt;TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>'). `UNIQUE (county_id, unique_id)` makes one <b>stored object</b> the unit of a row, and the absence of any constraint on `(county_id, reference)` is deliberate: a building may hold several rows here, so writes append rather than replace.

Do not add a unique constraint on `(county_id, reference)` to stop the table growing on re-runs. It would reduce the table to one row per building and discard every record after the first. The plain index created on that pair is not a constraint and places no such restriction on what may be stored.

Indexes: `(county_id, reference)` is the primary access path and every read filters on it, so it carries an index. `(county_id, unique_id)` carries none of its own, because the `UNIQUE` constraint is already an index on exactly those columns in that order.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2DReferencedObject(this Npgsql.NpgsqlConnection? npgsqlConnection, string tableName, int commandTimeout=600, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') representing the name of the table to be created\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\. TODO \[ReferencedObjectIndexes\]: the default is 600 rather than the 30 used elsewhere in this class, because on a table that predates the reference index the command has to build that index across every partition before it returns\. Once no deployed table needs a first build this is a catalog lookup again, and the default goes back to 30\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A [System\.Threading\.Tasks\.Task&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1') that represents the asynchronous operation\. The task result is a [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean') value indicating whether the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building2DReferencedObject\_Partition\(this NpgsqlConnection, string, int, int, CancellationToken\) Method

Asynchronously creates a partition for the Building2DReferencedObject table based on the specified table name and county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2DReferencedObject_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, string tableName, int countyId, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int,int,System.Threading.CancellationToken).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the parent table that is being partitioned\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county for which the partition is created\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building2D\_Partition\(this NpgsqlConnection, int, int, CancellationToken\) Method

Asynchronously creates a partition for the Building2D table associated with the specified county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2D_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, int countyId, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county for which the partition is being created\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the Building2D partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building\_Partition\(this NpgsqlConnection, int, int, CancellationToken\) Method

Asynchronously creates a partition for the [Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building') table based on the specified county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, int countyId, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the county for which the partition is being created\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_EPWFile(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_EPWFile\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the epw\_file table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_EPWFile(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_EPWFile(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_EPWFile(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_EPWFile(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_OrtoDatas\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the OrtoDatas table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_OrtoDatas(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the OrtoDatas table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_OrtoDatas\_Partition\(this NpgsqlConnection, int, int, CancellationToken\) Method

Asynchronously creates a partition for the OrtoDatas table based on the specified county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_OrtoDatas_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, int countyId, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the county for which the partition is being created\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_StatisticalDataCollection(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_StatisticalDataCollection\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the [StatisticalDataCollection](DiGi.GIS.PostgreSQL.Constants.md#DiGi.GIS.PostgreSQL.Constants.TableName.StatisticalDataCollection 'DiGi\.GIS\.PostgreSQL\.Constants\.TableName\.StatisticalDataCollection') table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_StatisticalDataCollection(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_StatisticalDataCollection(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_StatisticalDataCollection(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_StatisticalDataCollection(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_TerrainPoint\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the partitioned [TerrainPoint](DiGi.GIS.PostgreSQL.Constants.md#DiGi.GIS.PostgreSQL.Constants.TableName.TerrainPoint 'DiGi\.GIS\.PostgreSQL\.Constants\.TableName\.TerrainPoint') table along with its supporting indexes in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_TerrainPoint(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_TerrainPoint\_Partition\(this NpgsqlConnection, int, int, CancellationToken\) Method

Asynchronously creates a partition for the [TerrainPoint](DiGi.GIS.PostgreSQL.Constants.md#DiGi.GIS.PostgreSQL.Constants.TableName.TerrainPoint 'DiGi\.GIS\.PostgreSQL\.Constants\.TableName\.TerrainPoint') table based on the specified county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_TerrainPoint_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, int countyId, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county for which the partition is created\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_TerrainPoint_Partition(thisNpgsql.NpgsqlConnection,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Unit(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Unit\(this NpgsqlConnection, int, CancellationToken\) Method

Asynchronously creates the [Unit](DiGi.GIS.PostgreSQL.Constants.md#DiGi.GIS.PostgreSQL.Constants.TableName.Unit 'DiGi\.GIS\.PostgreSQL\.Constants\.TableName\.Unit') table along with its supporting indexes in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Unit(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Unit(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Unit(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Unit(thisNpgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_)'></a>

## Create\.TerrainPointDensityResult\(int, long, double, Nullable\<double\>\) Method

Works out how densely a county partition of the terrain point table is sampled and returns it as a [TerrainPointDensityResult\(int, long, double, Nullable&lt;double&gt;\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_) 'DiGi\.GIS\.PostgreSQL\.Create\.TerrainPointDensityResult\(int, long, double, System\.Nullable\<double\>\)')\.

Every figure the result carries beyond the count and the area is derived here rather than by the constructor, which assigns and nothing more.

A figure that cannot be derived is left null rather than filled with a not-a-number. Strict JSON has no token for one, so a not-a-number reaching a response body is a serialization failure rather than a value a reader can act on.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.TerrainPointDensityResult? TerrainPointDensityResult(int countyId, long count, double area, System.Nullable<double> gridSize=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county partition\.

<a name='DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_).count'></a>

`count` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of points stored for the county\.

<a name='DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_).area'></a>

`area` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The area the points were meant to cover, in square model units \- summed from the county's subdivisions, which is what a sampling run tests its points against\.

<a name='DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_).gridSize'></a>

`gridSize` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The spacing a sampling run used, when it is known\. Supplying it is what fills in [ExpectedDensity](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TerrainPointDensityResult.ExpectedDensity 'DiGi\.GIS\.PostgreSQL\.Classes\.TerrainPointDensityResult\.ExpectedDensity') and [Completeness](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TerrainPointDensityResult.Completeness 'DiGi\.GIS\.PostgreSQL\.Classes\.TerrainPointDensityResult\.Completeness')\.

#### Returns
[TerrainPointDensityResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TerrainPointDensityResult 'DiGi\.GIS\.PostgreSQL\.Classes\.TerrainPointDensityResult')  
The [TerrainPointDensityResult\(int, long, double, Nullable&lt;double&gt;\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.TerrainPointDensityResult(int,long,double,System.Nullable_double_) 'DiGi\.GIS\.PostgreSQL\.Create\.TerrainPointDensityResult\(int, long, double, System\.Nullable\<double\>\)'), or null when the count is negative or the area is not a usable measurement\.

<a name='DiGi.GIS.PostgreSQL.Create.UnitComplianceResultAsync(thisDiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,int,System.Threading.CancellationToken)'></a>

## Create\.UnitComplianceResultAsync\(this UnitPostgreSQLConverter, AdministrativeAreal2DPostgreSQLConverter, AdministrativeArealType, int, CancellationToken\) Method

Asynchronously evaluates the matching compliance of administrative area references of the specified type against BDL territorial units\.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.UnitComplianceResult?> UnitComplianceResultAsync(this DiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter? unitPostgreSQLConverter, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, int commandTimeout=60, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.UnitComplianceResultAsync(thisDiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,int,System.Threading.CancellationToken).unitPostgreSQLConverter'></a>

`unitPostgreSQLConverter` [UnitPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.UnitPostgreSQLConverter')

The converter used to access stored BDL unit data and extract the statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Create.UnitComplianceResultAsync(thisDiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,int,System.Threading.CancellationToken).administrativeAreal2DPostgreSQLConverter'></a>

`administrativeAreal2DPostgreSQLConverter` [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')

The converter used to retrieve administrative area references\.

<a name='DiGi.GIS.PostgreSQL.Create.UnitComplianceResultAsync(thisDiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,int,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The administrative area type to evaluate\.

<a name='DiGi.GIS.PostgreSQL.Create.UnitComplianceResultAsync(thisDiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for database commands\.

<a name='DiGi.GIS.PostgreSQL.Create.UnitComplianceResultAsync(thisDiGi.GIS.PostgreSQL.Classes.UnitPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[UnitComplianceResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.UnitComplianceResult 'DiGi\.GIS\.PostgreSQL\.Classes\.UnitComplianceResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [UnitComplianceResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.UnitComplianceResult 'DiGi\.GIS\.PostgreSQL\.Classes\.UnitComplianceResult') if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatasAsync(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken)'></a>

## Modify\.RefreshOrtoDatasAsync\(this GISPostgreSQLConverterManager, PostgreSQLOrtoDatasRefreshOptions, IProgress\<long\>, CancellationToken\) Method

Asynchronously queues the orthophoto downloads the given counties are short of\.

This writes no orthophoto data of its own. Each county's building references are read, the ones already stored are dropped unless [OverrideExisting](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.OverrideExisting 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions\.OverrideExisting') says otherwise, and the rest are appended to the queue the download task drains. What comes back is therefore work scheduled, not work done.

With [UpdateSubdivisionIds](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions\.UpdateSubdivisionIds') set, a second and unrelated thing happens first: each building's own subdivision identifier is pushed onto its stored row. A building whose subdivision has not been resolved is skipped rather than clearing the stored one.

A county that fails is logged with the exception that stopped it and stepped over, so one unreachable partition cannot cost the run the counties behind it. The counties are visited in ascending order, so a run started again covers them in the same order as the one it replaces.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshResult?> RefreshOrtoDatasAsync(this DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager? gISPostgreSQLConverterManager, DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions? postgreSQLOrtoDatasRefreshOptions=null, System.IProgress<long>? progress=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatasAsync(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The manager used to retrieve the necessary PostgreSQL converters\.

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatasAsync(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).postgreSQLOrtoDatasRefreshOptions'></a>

`postgreSQLOrtoDatasRefreshOptions` [PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions')

The options specifying how the orthophoto queue should be refreshed\. Null uses the defaults\.

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatasAsync(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

An optional progress reporter carrying the running total of references the queue has accepted\.

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatasAsync(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[PostgreSQLOrtoDatasRefreshResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshResult 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result carries what the run queued and what it stepped over, or null when the run could not be attempted at all \- a converter missing, or the counties unreadable\.

<a name='DiGi.GIS.PostgreSQL.Modify.ResetIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D)'></a>

## Modify\.ResetIds\(this AdministrativeAreal2D\) Method

Resets the administrative identifiers of the specified administrative areal 2D object to null\.

```csharp
public static bool ResetIds(this DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.ResetIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D'></a>

`administrativeAreal2D` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The administrative areal 2D object whose identifiers are to be reset\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the identifiers were successfully reset; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Modify.SetId(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D)'></a>

## Modify\.SetId\(this AdministrativeAreal2D, AdministrativeAreal2D\) Method

Sets the appropriate identifier on the destination administrative areal object based on the source's administrative areal type\.

```csharp
public static bool SetId(this DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D_Destination, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D_Source);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.SetId(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D_Destination'></a>

`administrativeAreal2D_Destination` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The destination administrative areal object where the ID will be set\.

<a name='DiGi.GIS.PostgreSQL.Modify.SetId(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D_Source'></a>

`administrativeAreal2D_Source` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The source administrative areal object providing the ID and type\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the ID was successfully set; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D)'></a>

## Modify\.UpdateIds\(this AdministrativeAreal2D, AdministrativeAreal2D\) Method

Updates the identification properties of the destination administrative areal object using values from the source object\.

```csharp
public static bool UpdateIds(this DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D_Destination, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D_Source);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D_Destination'></a>

`administrativeAreal2D_Destination` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The destination AdministrativeAreal2D object to be updated\.

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D_Source'></a>

`administrativeAreal2D_Source` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The source AdministrativeAreal2D object containing the new identification values\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the IDs were successfully updated; otherwise, false if either the destination or source object is null\.

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,double)'></a>

## Modify\.UpdateIds\(this AdministrativeAreal2D, IEnumerable\<AdministrativeAreal2D\>, double\) Method

Finds the parent of the destination administrative areal object among the given sources by geometry, and updates its identification properties from that parent\.

The sources are expected to be one administrative level, and all of it - the search is by geometry, not by an existing identifier. A source is chosen by containment of a sample point taken from the destination (its bounding box centroid, falling back to an internal point of its polygon); where several sources contain that point, the smallest of them wins as the most specific.

When no source contains the point, the destination is assigned to the source covering the <b>majority of its own area</b> instead. The BDOT10k settlement layer (`OT_ADMS_A`) and the administrative-division layer (`OT_ADJA_A`) are digitised independently, so a handful of settlements sit just outside every municipality polygon and their sample point lands in a gap. Requiring a majority - rather than any overlap - leaves a destination whose level genuinely holds no parent unassigned, so the caller can search the next level up instead of filing it under a neighbour it merely shares a border with. Full account: https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/14.

```csharp
public static bool UpdateIds(this DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D_Destination, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>? administrativeAreal2Ds_Source, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,double).administrativeAreal2D_Destination'></a>

`administrativeAreal2D_Destination` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The destination AdministrativeAreal2D object to be updated\.

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,double).administrativeAreal2Ds_Source'></a>

`administrativeAreal2Ds_Source` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The candidate source AdministrativeAreal2D objects, one administrative level, to be searched for a parent\.

<a name='DiGi.GIS.PostgreSQL.Modify.UpdateIds(thisDiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used by the containment checks\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if a parent was found and the IDs were updated; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Id(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_)'></a>

## Modify\.Update\_Id\(this Table, IEnumerable\<Building2DReference\>\) Method

Updates the Id column of the table based on the provided building2DReferences\. If a matching row is found \(based on CountyId and Reference\), it updates the Id value\. If no matching row is found, it adds a new row with the CountyId, Reference, and Id values\.

```csharp
public static void Update_Id(this DiGi.Core.IO.Table.Classes.Table? table, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference>? building2DReferences);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Id(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_).table'></a>

`table` [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')

The table to update

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Id(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The building2DReferences to use for updating

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Occupancy(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData_)'></a>

## Modify\.Update\_Occupancy\(this Table, IEnumerable\<Building2DOccupancyData\>\) Method

Updates the occupancy data in the specified table based on the provided collection of building occupancy records\.

The occupancy table holds one row per stored object rather than one per building, so a reference can arrive here several times. The first record of a given county and reference is the one that is written and the rest are stepped over, which means the collection has to reach this method in the caller's order of preference - the converter returns the newest record first, so passing its result straight through stores the newest.

```csharp
public static void Update_Occupancy(this DiGi.Core.IO.Table.Classes.Table? table, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData>? building2DOccupancyDatas);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Occupancy(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData_).table'></a>

`table` [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')

The PostgreSQL table to be updated\.

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Occupancy(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData_).building2DOccupancyDatas'></a>

`building2DOccupancyDatas` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData') objects containing the new occupancy information, most preferred record first\.

<a name='DiGi.GIS.PostgreSQL.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeArealType(thisDiGi.GIS.Classes.AdministrativeAreal2D)'></a>

## Query\.AdministrativeArealType\(this AdministrativeAreal2D\) Method

Determines the administrative areal type based on the provided administrative areal object\.

```csharp
public static DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType AdministrativeArealType(this DiGi.GIS.Classes.AdministrativeAreal2D? administrativeAreal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeArealType(thisDiGi.GIS.Classes.AdministrativeAreal2D).administrativeAreal2D'></a>

`administrativeAreal2D` [DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D')

The administrative areal object to evaluate\.

#### Returns
[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')  
The corresponding [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')\.

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeArealType(thisDiGi.GIS.Enums.AdministrativeDivisionType)'></a>

## Query\.AdministrativeArealType\(this AdministrativeDivisionType\) Method

Maps a GIS administrative division type to the PostgreSQL administrative areal type\.

```csharp
public static DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType AdministrativeArealType(this DiGi.GIS.Enums.AdministrativeDivisionType administrativeDivisionType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeArealType(thisDiGi.GIS.Enums.AdministrativeDivisionType).administrativeDivisionType'></a>

`administrativeDivisionType` [DiGi\.GIS\.Enums\.AdministrativeDivisionType](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.enums.administrativedivisiontype 'DiGi\.GIS\.Enums\.AdministrativeDivisionType')

The GIS administrative division type\.

#### Returns
[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')  
The corresponding [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')\.

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeCodeKey(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## Query\.AdministrativeCodeKey\(string, AdministrativeArealType\) Method

Gets the leading slice of an administrative code that names its ancestor at the given administrative areal type\.

A code carries the whole chain above it: the first 2 characters name the voivodeship, the first 4 the county, and the first 6 the municipality. The 7th character of a municipality code is the gmina <i>type</i> digit - a town and the rural area of one urban-rural gmina carry `4` and `5` against the gmina's own `3` - so the municipality slice is 6 characters, not the whole code. Verified against the stored table: every one of the 100 354 subdivision rows carries a 7-character code whose 4-character prefix is an existing county code, and 99.72% of the rows that already resolved agree with their municipality at 6 characters.

Returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') for [Country](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Country 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType\.Country'), which has no such relation - every country row's code is `10`, which is also the voivodeship code of łódzkie, so slicing 2 characters there would match one voivodeship's chain rather than the country. A caller that gets [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') has no code constraint to apply and must fall back to searching the whole level.

<b>A code is not a key.</b> 18 county codes and 64 municipality codes name several rows, one per polygon part, so a slice identifies a <i>set</i> of candidate rows and geometry still has to choose between them. See https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/1.

```csharp
public static string? AdministrativeCodeKey(string? code, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeCodeKey(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The administrative code to slice\.

<a name='DiGi.GIS.PostgreSQL.Query.AdministrativeCodeKey(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The administrative areal type of the ancestor being named\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The leading slice of [code](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.AdministrativeCodeKey(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).code 'DiGi\.GIS\.PostgreSQL\.Query\.AdministrativeCodeKey\(string, DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType\)\.code') naming the ancestor, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the type has no code relation or the code is too short to reach it\.

<a name='DiGi.GIS.PostgreSQL.Query.Building(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building_,DiGi.Geometry.Spatial.Classes.Point3D,double)'></a>

## Query\.Building\(this IEnumerable\<Building\>, Point3D, double\) Method

Selects the single most relevant [Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building') from a collection of candidates\.

Candidates are ranked ascending by level of detail and then by year, with nulls treated as the lowest rank, and only the candidates sharing the highest rank are considered.

When more than one candidate shares the highest rank and a point is provided, the candidate whose surface geometry is closest to that point wins; candidates without usable geometry are excluded from that comparison.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.Building? Building(this System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building>? buildings, DiGi.Geometry.Spatial.Classes.Point3D? point3D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Building(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building_,DiGi.Geometry.Spatial.Classes.Point3D,double).buildings'></a>

`buildings` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of [Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building') candidates to choose from\.

<a name='DiGi.GIS.PostgreSQL.Query.Building(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building_,DiGi.Geometry.Spatial.Classes.Point3D,double).point3D'></a>

`point3D` [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')

The optional [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') used to break ties between candidates of equal rank\.

<a name='DiGi.GIS.PostgreSQL.Query.Building(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building_,DiGi.Geometry.Spatial.Classes.Point3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The tolerance used for the closest point calculation\.

#### Returns
[Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building')  
The most relevant [Building](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building 'DiGi\.GIS\.PostgreSQL\.Classes\.Building'), or null if the collection is null, empty or no candidate could be resolved\.

<a name='DiGi.GIS.PostgreSQL.Query.ChildAdministrativeArealType(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## Query\.ChildAdministrativeArealType\(this AdministrativeArealType\) Method

Gets the child administrative areal type for the specified administrative areal type based on the administrative hierarchy\.

```csharp
public static System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> ChildAdministrativeArealType(this DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.ChildAdministrativeArealType(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The current administrative areal type\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The next level of administrative areal type in the hierarchy, or null if no child exists or the input is undefined\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double)'></a>

## Query\.CountyId\(this IDictionary\<int,IPolygonal2D\>, IPolygonal2D, double\) Method

Picks which of the candidate county rows a 2D building belongs to, from parts whose polygons the caller has already converted\.

The decision is the one described on the [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') overload; only where the polygons come from differs. Deriving a part's polygon means deserializing the stored geometry, and a county polygon carries thousands of vertices, so a caller deciding many buildings against the same parts should convert once with [Polygonal2DsByCountyId\(this IEnumerable&lt;AdministrativeAreal2D&gt;\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Polygonal2DsByCountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_) 'DiGi\.GIS\.PostgreSQL\.Query\.Polygonal2DsByCountyId\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D\>\)') and call this - the other overload converts every candidate again on every building.

```csharp
public static System.Nullable<int> CountyId(this System.Collections.Generic.IDictionary<int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D>? polygonal2Ds_ByCountyId, DiGi.Geometry.Planar.Interfaces.IPolygonal2D? polygonal2D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double).polygonal2Ds_ByCountyId'></a>

`polygonal2Ds_ByCountyId` [System\.Collections\.Generic\.IDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonal2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')

The candidate parts, keyed by the identifier of their county row\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double).polygonal2D'></a>

`polygonal2D` [DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonal2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D')

The external edge of the building footprint\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used for the containment and overlap tests\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The identifier of the county row the building belongs to, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when it cannot be decided\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double)'></a>

## Query\.CountyId\(this IEnumerable\<AdministrativeAreal2D\>, IPolygonal2D, double\) Method

Picks which of the candidate county rows a 2D building belongs to, by geometry\.

A county code names one row per polygon part, so a code can only narrow the field - this is what decides. Candidates are tried in three steps: the parts whose polygon the footprint lies in, else the nearest part, and where several parts contain it the one it overlaps most.

Where several parts cover the footprint <b>whole</b>, the smallest of them wins. Overlap cannot separate candidates that each hold every square metre of the building, so without this the answer would fall to the lowest identifier - a property of import order rather than of geography. The smallest is the most specific area containing it.

Every remaining comparison breaks ties on the row identifier, so two runs over the same building cannot disagree. Returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when nothing can be decided - the caller is expected to leave such a building unwritten rather than file it under a guess.

```csharp
public static System.Nullable<int> CountyId(this System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>? administrativeAreal2Ds, DiGi.Geometry.Planar.Interfaces.IPolygonal2D? polygonal2D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double).administrativeAreal2Ds'></a>

`administrativeAreal2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The candidate county rows\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double).polygonal2D'></a>

`polygonal2D` [DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonal2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D')

The external edge of the building footprint\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used for the containment and overlap tests\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The identifier of the county row the building belongs to, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when it cannot be decided\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyIdsByReferencesAsync(thisDiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_int_)'></a>

## Query\.CountyIdsByReferencesAsync\(this Building2DPostgreSQLConverter, IEnumerable\<string\>, IEnumerable\<int\>\) Method

Reads which county row each reference belongs to, from the `building_2d` row that holds it\.

A county code names one `administrative_areal_2d` row per polygon part, so a code cannot say which part an item belongs to. The 2D building already answers that - it was filed by geometry when it was imported - and reading it back keeps every table keyed by the same `(county_id, reference)` pair. Filing a whole batch under one part instead is what left sibling parts reading back empty while the upload reported success.

The parts are probed in ascending order, one batched lookup each, and a reference is taken by the first part that holds it. A reference held by more than one part therefore resolves to the same one on every run.

A reference no part holds is simply absent from the result: nothing states where it belongs, and the caller decides whether to drop it or resolve it some other way.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string,int>> CountyIdsByReferencesAsync(this DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter, System.Collections.Generic.IEnumerable<string?>? references, System.Collections.Generic.IEnumerable<int>? countyIds);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.CountyIdsByReferencesAsync(thisDiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_int_).building2DPostgreSQLConverter'></a>

`building2DPostgreSQLConverter` [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')

The converter used to look the references up\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyIdsByReferencesAsync(thisDiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_int_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The references to resolve\.

<a name='DiGi.GIS.PostgreSQL.Query.CountyIdsByReferencesAsync(thisDiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_int_).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The candidate county rows, normally every polygon part of one code\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The identifier of the county row holding each reference\. Empty when nothing could be resolved\.

<a name='DiGi.GIS.PostgreSQL.Query.IdColumnName(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## Query\.IdColumnName\(this AdministrativeArealType\) Method

Gets the name of the database column that stores the identifier of an administrative area of the given type\.

This is the column to filter on when the ancestor being matched is known outright, which is not always the level directly above the rows being read - see [ParentIdColumnName\(this AdministrativeArealType\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.ParentIdColumnName(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType) 'DiGi\.GIS\.PostgreSQL\.Query\.ParentIdColumnName\(this DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType\)') for the relative form.

```csharp
public static string? IdColumnName(this DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.IdColumnName(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The type of the administrative area whose identifier is stored\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The name of the column holding an identifier of that type, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when no column stores one \- nothing references a Subdivision\.

<a name='DiGi.GIS.PostgreSQL.Query.IdsByPoint2Ds(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Classes.PolygonalFace2D_,System.Collections.Generic.IReadOnlyList_DiGi.Geometry.Planar.Classes.Point2D_,double)'></a>

## Query\.IdsByPoint2Ds\(this IDictionary\<int,PolygonalFace2D\>, IReadOnlyList\<Point2D\>, double\) Method

Decides, for each point, which face contains it, without touching the database\.

The result has one entry per point, at the same position, holding null wherever a point lies in no face. That is an ordinary answer rather than a failure - a caller sampling a rectangle over an irregular area meets it at every corner.

Faces are bucketed into a uniform cell grid first, so a point is tested against the few faces near it rather than against all of them. Without that a run over a whole area costs the number of points times the number of faces, each test walking a ring of thousands of vertices.

Where faces overlap, the lowest identifier wins, so the same point decided twice gives the same answer.

```csharp
public static System.Nullable<int>[]? IdsByPoint2Ds(this System.Collections.Generic.IDictionary<int,DiGi.Geometry.Planar.Classes.PolygonalFace2D>? polygonalFace2Ds_ById, System.Collections.Generic.IReadOnlyList<DiGi.Geometry.Planar.Classes.Point2D>? point2Ds, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.IdsByPoint2Ds(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Classes.PolygonalFace2D_,System.Collections.Generic.IReadOnlyList_DiGi.Geometry.Planar.Classes.Point2D_,double).polygonalFace2Ds_ById'></a>

`polygonalFace2Ds_ById` [System\.Collections\.Generic\.IDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')

The faces to decide against, keyed by identifier\.

<a name='DiGi.GIS.PostgreSQL.Query.IdsByPoint2Ds(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Classes.PolygonalFace2D_,System.Collections.Generic.IReadOnlyList_DiGi.Geometry.Planar.Classes.Point2D_,double).point2Ds'></a>

`point2Ds` [System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')

The points to decide\.

<a name='DiGi.GIS.PostgreSQL.Query.IdsByPoint2Ds(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Classes.PolygonalFace2D_,System.Collections.Generic.IReadOnlyList_DiGi.Geometry.Planar.Classes.Point2D_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance a point may lie outside a face and still be counted as within it\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
The identifier of the face containing each point, null at the position of every point that lies in none, or null when either argument is null\.

<a name='DiGi.GIS.PostgreSQL.Query.InScopeSubdivisionIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IReadOnlyDictionary_int,System.Collections.Generic.HashSet_int__)'></a>

## Query\.InScopeSubdivisionIds\(IEnumerable\<AdministrativeAreal2DReference\>, IReadOnlyDictionary\<int,HashSet\<int\>\>\) Method

Computes, for every county polygon part, the set of subdivision identifiers the subdivision loop reaches under it\.

The building data update walks subdivisions and reaches a building only when the building's county part sits in the sibling group of the subdivision's parent county. This is the exact set the final per-county pass must leave untouched: a building whose part does not sit in its subdivision's parent group is invisible to the loop, so only the fallback can write it.

A county code is not a key - it names one row per polygon part - so the in-scope set is keyed by part identifier, and a subdivision filed under one part is in scope for every part of that part's code group. A subdivision without a parent county is out of scope everywhere.

```csharp
public static System.Collections.Generic.Dictionary<int,System.Collections.Generic.HashSet<int>> InScopeSubdivisionIds(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>? subdivisions, System.Collections.Generic.IReadOnlyDictionary<int,System.Collections.Generic.HashSet<int>>? siblingCountyGroups);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.InScopeSubdivisionIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IReadOnlyDictionary_int,System.Collections.Generic.HashSet_int__).subdivisions'></a>

`subdivisions` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The subdivision references, each carrying an identifier and its parent county identifier\. May be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Query.InScopeSubdivisionIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IReadOnlyDictionary_int,System.Collections.Generic.HashSet_int__).siblingCountyGroups'></a>

`siblingCountyGroups` [System\.Collections\.Generic\.IReadOnlyDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2 'System\.Collections\.Generic\.IReadOnlyDictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2 'System\.Collections\.Generic\.IReadOnlyDictionary\`2')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2 'System\.Collections\.Generic\.IReadOnlyDictionary\`2')

Each county part mapped to every part that shares its code\. May be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null'), in which case each subdivision is in scope for its parent part only\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A map from each in\-scope county part to the subdivision identifiers the subdivision loop reaches under it\. Parts with no in\-scope subdivisions are absent from the map\.

<a name='DiGi.GIS.PostgreSQL.Query.IsInScope(int,string,System.Collections.Generic.ICollection_int_,System.Collections.Generic.ICollection_string_)'></a>

## Query\.IsInScope\(int, string, ICollection\<int\>, ICollection\<string\>\) Method

Decides whether a county row is in scope for a task that runs over a subset of the country\.

A county code is not a key - it names one row per polygon part - so scope is expressed two ways at once: by county row identifier, and by the two-digit voivodeship code a county code starts with. Both filters must admit the row, so a task can be pointed at one voivodeship, at a handful of parts, or at the parts of one voivodeship named by identifier.

A null filter admits everything, which is what makes a national pass the default. A row without a code cannot be placed in a voivodeship, so it is out of scope whenever [voivodeshipCodes](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.IsInScope(int,string,System.Collections.Generic.ICollection_int_,System.Collections.Generic.ICollection_string_).voivodeshipCodes 'DiGi\.GIS\.PostgreSQL\.Query\.IsInScope\(int, string, System\.Collections\.Generic\.ICollection\<int\>, System\.Collections\.Generic\.ICollection\<string\>\)\.voivodeshipCodes') is given.

```csharp
public static bool IsInScope(int countyId, string? code, System.Collections.Generic.ICollection<int>? countyIds, System.Collections.Generic.ICollection<string>? voivodeshipCodes);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.IsInScope(int,string,System.Collections.Generic.ICollection_int_,System.Collections.Generic.ICollection_string_).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county row\. A negative value is never in scope\.

<a name='DiGi.GIS.PostgreSQL.Query.IsInScope(int,string,System.Collections.Generic.ICollection_int_,System.Collections.Generic.ICollection_string_).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The county code, whose leading digits name the voivodeship\.

<a name='DiGi.GIS.PostgreSQL.Query.IsInScope(int,string,System.Collections.Generic.ICollection_int_,System.Collections.Generic.ICollection_string_).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.ICollection&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1 'System\.Collections\.Generic\.ICollection\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1 'System\.Collections\.Generic\.ICollection\`1')

The county row identifiers in scope, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') for every row\.

<a name='DiGi.GIS.PostgreSQL.Query.IsInScope(int,string,System.Collections.Generic.ICollection_int_,System.Collections.Generic.ICollection_string_).voivodeshipCodes'></a>

`voivodeshipCodes` [System\.Collections\.Generic\.ICollection&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1 'System\.Collections\.Generic\.ICollection\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1 'System\.Collections\.Generic\.ICollection\`1')

The voivodeship codes in scope, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') for every voivodeship\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the county row is in scope, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.Classes.AdministrativeAreal2D)'></a>

## Query\.Match\(this StatisticalUnit, AdministrativeAreal2D\) Method

Finds the [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') matching the specified GIS Core [DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D') within the provided root statistical unit hierarchy\.

```csharp
public static DiGi.GIS.Classes.StatisticalUnit? Match(this DiGi.GIS.Classes.StatisticalUnit? rootStatisticalUnit, DiGi.GIS.Classes.AdministrativeAreal2D? administrativeAreal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.Classes.AdministrativeAreal2D).rootStatisticalUnit'></a>

`rootStatisticalUnit` [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')

The root statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.Classes.AdministrativeAreal2D).administrativeAreal2D'></a>

`administrativeAreal2D` [DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D')

The GIS administrative area to match\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')  
The matching [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D)'></a>

## Query\.Match\(this StatisticalUnit, AdministrativeAreal2D\) Method

Finds the [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') matching the specified PostgreSQL [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') within the provided root statistical unit hierarchy\.

```csharp
public static DiGi.GIS.Classes.StatisticalUnit? Match(this DiGi.GIS.Classes.StatisticalUnit? rootStatisticalUnit, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).rootStatisticalUnit'></a>

`rootStatisticalUnit` [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')

The root statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D'></a>

`administrativeAreal2D` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The administrative area to match\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')  
The matching [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference)'></a>

## Query\.Match\(this StatisticalUnit, AdministrativeAreal2DReference\) Method

Finds the [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') matching the specified [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') within the provided root statistical unit hierarchy\.

```csharp
public static DiGi.GIS.Classes.StatisticalUnit? Match(this DiGi.GIS.Classes.StatisticalUnit? rootStatisticalUnit, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference? administrativeAreal2DReference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference).rootStatisticalUnit'></a>

`rootStatisticalUnit` [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')

The root statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference).administrativeAreal2DReference'></a>

`administrativeAreal2DReference` [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')

The administrative area reference to match\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')  
The matching [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath)'></a>

## Query\.Match\(this StatisticalUnit, AdministrativeAreal2DReference, AdministrativeAreal2DReferencePath\) Method

Finds the [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') matching the subdivision reference within the hierarchy, falling back to the subdivision's parent municipality reference from the path when the subdivision itself does not match\.

```csharp
public static DiGi.GIS.Classes.StatisticalUnit? Match(this DiGi.GIS.Classes.StatisticalUnit? rootStatisticalUnit, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference? administrativeAreal2DReference, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath).rootStatisticalUnit'></a>

`rootStatisticalUnit` [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')

The root statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath).administrativeAreal2DReference'></a>

`administrativeAreal2DReference` [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')

The subdivision reference to match\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath).administrativeAreal2DReferencePath'></a>

`administrativeAreal2DReferencePath` [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')

The subdivision's territorial ancestor chain, consulted for the municipality fallback\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')  
The matching [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath)'></a>

## Query\.Match\(this StatisticalUnit, AdministrativeAreal2DReferencePath\) Method

Finds the [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') matching the leaf reference in the specified [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath') within the hierarchy\.

```csharp
public static DiGi.GIS.Classes.StatisticalUnit? Match(this DiGi.GIS.Classes.StatisticalUnit? rootStatisticalUnit, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath).rootStatisticalUnit'></a>

`rootStatisticalUnit` [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')

The root statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath).administrativeAreal2DReferencePath'></a>

`administrativeAreal2DReferencePath` [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')

The reference path containing the territorial ancestor chain\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')  
The matching [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,string,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## Query\.Match\(this StatisticalUnit, string, string, AdministrativeArealType\) Method

Finds the [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') matching the specified territorial name, code, and administrative type\.

```csharp
public static DiGi.GIS.Classes.StatisticalUnit? Match(this DiGi.GIS.Classes.StatisticalUnit? rootStatisticalUnit, string? name, string? code, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,string,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).rootStatisticalUnit'></a>

`rootStatisticalUnit` [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')

The root statistical unit hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,string,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The territorial entity name\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,string,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The territorial entity code\.

<a name='DiGi.GIS.PostgreSQL.Query.Match(thisDiGi.GIS.Classes.StatisticalUnit,string,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The administrative area type\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit')  
The matching [DiGi\.GIS\.Classes\.StatisticalUnit](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalunit 'DiGi\.GIS\.Classes\.StatisticalUnit') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Query.ParentAdministrativeArealType(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## Query\.ParentAdministrativeArealType\(this AdministrativeArealType\) Method

Gets the parent administrative areal type for the specified administrative areal type based on the administrative hierarchy\.

```csharp
public static System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> ParentAdministrativeArealType(this DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.ParentAdministrativeArealType(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The current administrative areal type\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The parent administrative areal type, or `null` if no parent exists \(e\.g\., for Country or Undefined\)\.

<a name='DiGi.GIS.PostgreSQL.Query.ParentIdColumnName(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## Query\.ParentIdColumnName\(this AdministrativeArealType\) Method

Gets the name of the database column that stores the identifier of the parent administrative area for a given administrative areal type\.

This is the column of the level <b>directly above</b>. A search that has to step over an empty level - m. Poznan holds no `gmina` feature, so its subdivisions hang off the county - knows which ancestor it matched and should use [IdColumnName\(this AdministrativeArealType\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.IdColumnName(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType) 'DiGi\.GIS\.PostgreSQL\.Query\.IdColumnName\(this DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType\)') with that type instead.

```csharp
public static string? ParentIdColumnName(this DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.ParentIdColumnName(thisDiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The type of the administrative area\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The name of the parent ID column as a string, or null if no parent column exists for the specified type\.

<a name='DiGi.GIS.PostgreSQL.Query.Polygonal2DsByCountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_)'></a>

## Query\.Polygonal2DsByCountyId\(this IEnumerable\<AdministrativeAreal2D\>\) Method

Derives the polygon of each county row once, keyed by the identifier of the row it came from\.

A row stores its geometry as JSON, so reading its polygon deserializes the whole object - and a county polygon carries thousands of vertices. Deciding many buildings against the same parts should derive them once through this and pass the result to [CountyId\(this IDictionary&lt;int,IPolygonal2D&gt;, IPolygonal2D, double\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.CountyId(thisSystem.Collections.Generic.IDictionary_int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D_,DiGi.Geometry.Planar.Interfaces.IPolygonal2D,double) 'DiGi\.GIS\.PostgreSQL\.Query\.CountyId\(this System\.Collections\.Generic\.IDictionary\<int,DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D\>, DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D, double\)'), rather than handing the rows themselves to the other overload, which repeats the conversion for every building.

A row whose geometry cannot be read is left out, so the result holds only parts that can actually be tested against.

```csharp
public static System.Collections.Generic.Dictionary<int,DiGi.Geometry.Planar.Interfaces.IPolygonal2D> Polygonal2DsByCountyId(this System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>? administrativeAreal2Ds);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Polygonal2DsByCountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_).administrativeAreal2Ds'></a>

`administrativeAreal2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The county rows to derive polygons from\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonal2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
The external edge of each row that has one, keyed by row identifier\. Empty when nothing could be derived\.

<a name='DiGi.GIS.PostgreSQL.Query.PolygonalFace2DsById(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_)'></a>

## Query\.PolygonalFace2DsById\(this IEnumerable\<AdministrativeAreal2D\>\) Method

Derives the face of each row once, keyed by the identifier of the row it came from\.

Reading a row's geometry deserializes the whole stored object, and the property that exposes the face hands back a clone on every access - so a caller deciding many points against the same rows should derive the faces once through this and test against the result, rather than reaching through the rows again for each point.

The whole face is kept, holes included, unlike [Polygonal2DsByCountyId\(this IEnumerable&lt;AdministrativeAreal2D&gt;\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Polygonal2DsByCountyId(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_) 'DiGi\.GIS\.PostgreSQL\.Query\.Polygonal2DsByCountyId\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D\>\)'), which keeps only the outer ring. An area that excludes a town inside it is a face with a hole, and a point in that town belongs to the town rather than to the area around it - testing against the outer ring alone would claim it.

A row whose geometry cannot be read is left out, so the result holds only rows that can actually be tested against.

```csharp
public static System.Collections.Generic.Dictionary<int,DiGi.Geometry.Planar.Classes.PolygonalFace2D> PolygonalFace2DsById(this System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>? administrativeAreal2Ds);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.PolygonalFace2DsById(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_).administrativeAreal2Ds'></a>

`administrativeAreal2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The rows to derive faces from\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
The face of each row that has one, keyed by row identifier\. Empty when nothing could be derived\.

<a name='DiGi.GIS.PostgreSQL.Query.Population(thisDiGi.GIS.Classes.StatisticalDataCollection)'></a>

## Query\.Population\(this StatisticalDataCollection\) Method

Retrieves the demographic population yearly data series from the statistical data collection, scaling BDL values to individual counts if applicable\.

```csharp
public static DiGi.GIS.Classes.StatisticalYearlyDoubleData? Population(this DiGi.GIS.Classes.StatisticalDataCollection? statisticalDataCollection);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Population(thisDiGi.GIS.Classes.StatisticalDataCollection).statisticalDataCollection'></a>

`statisticalDataCollection` [DiGi\.GIS\.Classes\.StatisticalDataCollection](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticaldatacollection 'DiGi\.GIS\.Classes\.StatisticalDataCollection')

The statistical data collection containing demographic information\.

#### Returns
[DiGi\.GIS\.Classes\.StatisticalYearlyDoubleData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalyearlydoubledata 'DiGi\.GIS\.Classes\.StatisticalYearlyDoubleData')  
A [DiGi\.GIS\.Classes\.StatisticalYearlyDoubleData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.statisticalyearlydoubledata 'DiGi\.GIS\.Classes\.StatisticalYearlyDoubleData') containing normalized yearly population counts, or null if no population series is found\.

<a name='DiGi.GIS.PostgreSQL.Query.RandomSeed(int,int)'></a>

## Query\.RandomSeed\(int, int\) Method

Combines a run seed with a county row identifier into a seed for that county alone\.

A single generator advanced across counties makes each county's draw depend on how many items every preceding county held, so changing the scope of a run - or the population of one county - changes what every county after it draws. Seeding per county removes that: a county draws the same sample whether it is verified on its own, with its voivodeship, or nationally.

<b>Do not replace this with <see cref="M:System.HashCode.Combine``2(``0,``1)"/>.</b> That mixes in a seed randomized per process, so it returns a different value on every run - the opposite of what this exists to provide.

```csharp
public static int RandomSeed(int randomSeed, int countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.RandomSeed(int,int).randomSeed'></a>

`randomSeed` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The seed identifying the run\.

<a name='DiGi.GIS.PostgreSQL.Query.RandomSeed(int,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county row\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The seed to draw that county's sample with\.

<a name='DiGi.GIS.PostgreSQL.Query.RowsByCountyIdAndReference(thisDiGi.Core.IO.Table.Classes.Table,DiGi.Core.IO.Table.Classes.Column,DiGi.Core.IO.Table.Classes.Column)'></a>

## Query\.RowsByCountyIdAndReference\(this Table, Column, Column\) Method

Indexes the rows of a table by their county identifier and reference so that a caller matching a collection of items against them does not rescan the table for each one\.

Rows are read from the back, so where several rows carry the same county and reference the one at the highest index wins - which is what a backwards scan stopping at its first hit did.

```csharp
public static System.Collections.Generic.Dictionary<(int,string),DiGi.Core.IO.Table.Classes.Row> RowsByCountyIdAndReference(this DiGi.Core.IO.Table.Classes.Table? table, DiGi.Core.IO.Table.Classes.Column? column_CountyId, DiGi.Core.IO.Table.Classes.Column? column_Reference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.RowsByCountyIdAndReference(thisDiGi.Core.IO.Table.Classes.Table,DiGi.Core.IO.Table.Classes.Column,DiGi.Core.IO.Table.Classes.Column).table'></a>

`table` [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')

The table whose rows are indexed\.

<a name='DiGi.GIS.PostgreSQL.Query.RowsByCountyIdAndReference(thisDiGi.Core.IO.Table.Classes.Table,DiGi.Core.IO.Table.Classes.Column,DiGi.Core.IO.Table.Classes.Column).column_CountyId'></a>

`column_CountyId` [DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column')

The county identifier column\.

<a name='DiGi.GIS.PostgreSQL.Query.RowsByCountyIdAndReference(thisDiGi.Core.IO.Table.Classes.Table,DiGi.Core.IO.Table.Classes.Column,DiGi.Core.IO.Table.Classes.Column).column_Reference'></a>

`column_Reference` [DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column')

The reference column\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.Core\.IO\.Table\.Classes\.Row](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.row 'DiGi\.Core\.IO\.Table\.Classes\.Row')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A dictionary of rows keyed by county identifier and reference\. Rows missing either value are not included\. Never null\.

<a name='DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random)'></a>

## Query\.Sample\<T\>\(this IEnumerable\<T\>, int, Random\) Method

Draws a reproducible sample of the given size from a collection\.

A partial Fisher-Yates shuffle over a copy: every item is equally likely to be drawn and none is drawn twice, without shuffling a list that can hold tens of thousands of entries in full.

The draw consumes exactly one value from [random](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).random 'DiGi\.GIS\.PostgreSQL\.Query\.Sample\<T\>\(this System\.Collections\.Generic\.IEnumerable\<T\>, int, System\.Random\)\.random') per item returned, so a generator shared across several calls hands each call a different stream depending on how large the preceding populations were. Seed a fresh generator per call with [RandomSeed\(int, int\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.RandomSeed(int,int) 'DiGi\.GIS\.PostgreSQL\.Query\.RandomSeed\(int, int\)') when the draws are meant to be independent of one another.

```csharp
public static System.Collections.Generic.List<T>? Sample<T>(this System.Collections.Generic.IEnumerable<T>? values, int sampleSize, System.Random? random);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).T'></a>

`T`

The item type\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).values'></a>

`values` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[T](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).T 'DiGi\.GIS\.PostgreSQL\.Query\.Sample\<T\>\(this System\.Collections\.Generic\.IEnumerable\<T\>, int, System\.Random\)\.T')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The items to draw from\.

<a name='DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).sampleSize'></a>

`sampleSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of items to draw\. A value of zero or less takes them all\.

<a name='DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).random'></a>

`random` [System\.Random](https://learn.microsoft.com/en-us/dotnet/api/system.random 'System\.Random')

The random source, seeded by the caller so the draw can be repeated\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[T](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).T 'DiGi\.GIS\.PostgreSQL\.Query\.Sample\<T\>\(this System\.Collections\.Generic\.IEnumerable\<T\>, int, System\.Random\)\.T')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The drawn items, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when [values](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).values 'DiGi\.GIS\.PostgreSQL\.Query\.Sample\<T\>\(this System\.Collections\.Generic\.IEnumerable\<T\>, int, System\.Random\)\.values') or [random](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.Sample_T_(thisSystem.Collections.Generic.IEnumerable_T_,int,System.Random).random 'DiGi\.GIS\.PostgreSQL\.Query\.Sample\<T\>\(this System\.Collections\.Generic\.IEnumerable\<T\>, int, System\.Random\)\.random') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Query.SiblingCountyGroups(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_)'></a>

## Query\.SiblingCountyGroups\(this IEnumerable\<AdministrativeAreal2DReference\>\) Method

Groups county polygon parts by code: every part is mapped to the full set of parts that share its code\.

A county code is not a key - it names one row per polygon part - so the result is keyed by part `Id`, and a part with no usable code groups with itself. This is the single definition the building data update uses to reach a building under every part of its subdivision's parent county.

```csharp
public static System.Collections.Generic.Dictionary<int,System.Collections.Generic.HashSet<int>> SiblingCountyGroups(this System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>? countyReferences);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.SiblingCountyGroups(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_).countyReferences'></a>

`countyReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

County references to group\. May be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null'), in which case the result is empty\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A map from each county part `Id` to the set of part `Id`s sharing its code; a code\-less part maps to itself\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionCoveragesAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken)'></a>

## Query\.SubdivisionCoveragesAsync\(this OrtoDatasPostgreSQLConverter, Building2DPostgreSQLConverter, int, int, CancellationToken\) Method

Asynchronously measures, for one county, how much of each of its subdivisions' buildings the orthophoto store holds\.

What the estimated partition counts cannot answer. Both tables are partitioned by `county_id`, so `reltuples` describes a whole county and there is no subdivision-level figure to be had from it - reporting the county's own factor for a subdivision is [DiGi\.GIS\.WebAPI issue \#8](https://github.com/ZiolkowskiJakub/DiGi.GIS.WebAPI/issues/8 'https://github\.com/ZiolkowskiJakub/DiGi\.GIS\.WebAPI/issues/8'). This counts instead of estimating, and costs one read per side however many subdivisions are asked about.

<b>The orthophoto side's own <c>subdivision_id</c> is deliberately not used, and grouping by it would be wrong.</b> That column has never been written: not one of the 8 384 055 rows stored across 225 counties carries a value, measured 2026-08-26 through `gis/ortodatas/summariesbycountyids`. Grouping the orthophoto side by it answers zero for every subdivision in the country - a different wrong number, not an honest one. Populating it is an unfinished migration, and even once it runs the value is a copy of the building's, which is the defect class issues #23, #31 and #36 exist for. `building_2d` is the side that knows which subdivision a building belongs to, so a building is attributed there and the orthophoto side is asked only whether it holds that reference.

The two tables live in different databases - `building_2d` in the main store, `orto_datas` in the storage one - so this cannot be a join and is not one. Each side is read once, cheaply, and matched in memory, the same way [SubdivisionLinksAsync\(this OrtoDatasPostgreSQLConverter, Building2DPostgreSQLConverter, int, int, int, CancellationToken\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken) 'DiGi\.GIS\.PostgreSQL\.Query\.SubdivisionLinksAsync\(this DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter, DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter, int, int, int, System\.Threading\.CancellationToken\)') does.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.OrtoDatasCoverageResult>?> SubdivisionCoveragesAsync(this DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter, DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter, int countyId, int commandTimeout=600, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionCoveragesAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).ortoDatasPostgreSQLConverter'></a>

`ortoDatasPostgreSQLConverter` [OrtoDatasPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter')

The converter reading the orthophoto store\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionCoveragesAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).building2DPostgreSQLConverter'></a>

`building2DPostgreSQLConverter` [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')

The converter reading the building store\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionCoveragesAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county to measure\. One polygon part, not a code \- a multi\-part county is measured a part at a time\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionCoveragesAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of each command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionCoveragesAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[OrtoDatasCoverageResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasCoverageResult 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasCoverageResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains one [OrtoDatasCoverageResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasCoverageResult 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasCoverageResult') per subdivision the county's buildings name, plus one carrying a null [SubdivisionId](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasCoverageResult.SubdivisionId 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasCoverageResult\.SubdivisionId') for the buildings that name none when there are any; or null when either converter is missing, either side could not be read, or the county holds no orthophoto row at all\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken)'></a>

## Query\.SubdivisionLinksAsync\(this OrtoDatasPostgreSQLConverter, Building2DPostgreSQLConverter, int, int, int, CancellationToken\) Method

Asynchronously compares, for one county, the subdivision each building is filed under against the one its orthophoto row carries\.

The two tables are in different databases - `building_2d` in the main store, `orto_datas` in the storage one - so this cannot be a join and is not one. Each side is read once, cheaply, and matched in memory: the orthophoto side through [GetSubdivisionIdsByCountyIdAsync\(int, int, CancellationToken\)](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetSubdivisionIdsByCountyIdAsync(int,int,System.Threading.CancellationToken) 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter\.GetSubdivisionIdsByCountyIdAsync\(int, int, System\.Threading\.CancellationToken\)'), which projects two columns and never the imagery, and the building side through [GetBuilding2DReferencesByCountyIdAsync\(int, Nullable&lt;int&gt;, IEnumerable&lt;string&gt;, int, CancellationToken\)](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int,System.Threading.CancellationToken) 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter\.GetBuilding2DReferencesByCountyIdAsync\(int, System\.Nullable\<int\>, System\.Collections\.Generic\.IEnumerable\<string\>, int, System\.Threading\.CancellationToken\)'), which already returns nothing heavier.

That separation is the whole reason the value has to be pushed across by a refresh in the first place, and the reason nothing keeps the two in step on its own.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.OrtoDatasSubdivisionResult?> SubdivisionLinksAsync(this DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter, DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter, int countyId, int sampleCount=20, int commandTimeout=600, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken).ortoDatasPostgreSQLConverter'></a>

`ortoDatasPostgreSQLConverter` [OrtoDatasPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter')

The converter reading the orthophoto store\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken).building2DPostgreSQLConverter'></a>

`building2DPostgreSQLConverter` [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')

The converter reading the building store\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county to compare\. One polygon part, not a code\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken).sampleCount'></a>

`sampleCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

How many references to name per disagreeing category, so a result stays a fixed size on a county with a hundred thousand buildings\. Zero names none\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of each command\. A value of 0 disables the timeout\.

<a name='DiGi.GIS.PostgreSQL.Query.SubdivisionLinksAsync(thisDiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter,DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter,int,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[OrtoDatasSubdivisionResult](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasSubdivisionResult 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasSubdivisionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result carries the comparison, or null when either converter is missing or either side could not be read\.

<a name='DiGi.GIS.PostgreSQL.Query.SubjectCount(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,int)'></a>

## Query\.SubjectCount\(IEnumerable\<Building2D\>, IEnumerable\<Building2D\>, int\) Method

Counts how many of the given buildings a spatial read of their own surroundings brought back\.

The surroundings of a set of buildings are read over the area those buildings cover, so every one of them is inside it and a correct read returns all of them. What comes back short says which of two different things went wrong, and they are not answered by the same fix: none of them back means the read is not reaching the partition they are filed under - a county whose territory is disconnected is one row per polygon part, and pruning to the wrong part answers an empty set rather than an error. Some of them back is per building instead: `min_x` to `max_y` are nullable and the overlap test is made on them, so a building whose stored box is missing drops out of its own neighbourhood.

Matched on county and reference together, because a reference is unique only within a county.

See https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/64.

```csharp
public static int SubjectCount(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2D>? building2Ds, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2D>? building2Ds_Neighbour, int countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.SubjectCount(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,int).building2Ds'></a>

`building2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The buildings the surroundings were read for\. May be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Query.SubjectCount(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,int).building2Ds_Neighbour'></a>

`building2Ds_Neighbour` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The surroundings that came back\. May be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Query.SubjectCount(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The county part the buildings are filed under\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of [building2Ds](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.SubjectCount(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,int).building2Ds 'DiGi\.GIS\.PostgreSQL\.Query\.SubjectCount\(System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.Building2D\>, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.Building2D\>, int\)\.building2Ds') present in [building2Ds\_Neighbour](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Query.SubjectCount(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,int).building2Ds_Neighbour 'DiGi\.GIS\.PostgreSQL\.Query\.SubjectCount\(System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.Building2D\>, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.Building2D\>, int\)\.building2Ds\_Neighbour')\.

<a name='DiGi.GIS.PostgreSQL.Query.TryParse(thisstring,string,System.Nullable_int_,DiGi.Core.Classes.GuidReference)'></a>

## Query\.TryParse\(this string, string, Nullable\<int\>, GuidReference\) Method

Attempts to parse a reference string into its constituent building model reference, optional county identifier, and optional building element GUID reference\.

The method delegates to [DiGi\.Core\.Query\.TryParse\(System\.String,DiGi\.Core\.Interfaces\.IReference@\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.query.tryparse#digi-core-query-tryparse(system-string-digi-core-interfaces-ireference@) 'DiGi\.Core\.Query\.TryParse\(System\.String,DiGi\.Core\.Interfaces\.IReference@\)') to deserialize the string. A plain non-parsable string is treated as a bare building model reference. For [DiGi\.Core\.Classes\.ComplexReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.complexreference 'DiGi\.Core\.Classes\.ComplexReference') values, the individual segments are extracted by matching [DiGi\.Core\.Classes\.TypeReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.typereference 'DiGi\.Core\.Classes\.TypeReference') discriminators for [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel'), [DiGi\.GIS\.Classes\.AdministrativeDivision](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativedivision 'DiGi\.GIS\.Classes\.AdministrativeDivision'), and [DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.ibuildingguidobject 'DiGi\.Analytical\.Building\.Interfaces\.IBuildingGuidObject')-assignable types.

```csharp
public static bool TryParse(this string? reference, out string buildingModelReference, out System.Nullable<int> countyId, out DiGi.Core.Classes.GuidReference? buildingObjectGuidReference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Query.TryParse(thisstring,string,System.Nullable_int_,DiGi.Core.Classes.GuidReference).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The reference string to parse\.

<a name='DiGi.GIS.PostgreSQL.Query.TryParse(thisstring,string,System.Nullable_int_,DiGi.Core.Classes.GuidReference).buildingModelReference'></a>

`buildingModelReference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

When this method returns, contains the extracted building model reference string\. Set to [System\.String\.Empty](https://learn.microsoft.com/en-us/dotnet/api/system.string.empty 'System\.String\.Empty') if parsing fails\.

<a name='DiGi.GIS.PostgreSQL.Query.TryParse(thisstring,string,System.Nullable_int_,DiGi.Core.Classes.GuidReference).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When this method returns, contains the county identifier if one was found in the reference chain; otherwise, [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Query.TryParse(thisstring,string,System.Nullable_int_,DiGi.Core.Classes.GuidReference).buildingObjectGuidReference'></a>

`buildingObjectGuidReference` [DiGi\.Core\.Classes\.GuidReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidreference 'DiGi\.Core\.Classes\.GuidReference')

When this method returns, contains the [DiGi\.Core\.Classes\.GuidReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidreference 'DiGi\.Core\.Classes\.GuidReference') for a building element if one was found in the reference chain; otherwise, [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the reference was successfully parsed or treated as a bare building model reference; [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the input is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null'), empty, or the parsed structure cannot be resolved\.