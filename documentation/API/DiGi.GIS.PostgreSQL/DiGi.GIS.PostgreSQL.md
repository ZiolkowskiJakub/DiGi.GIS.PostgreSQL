#### [DiGi\.GIS\.PostgreSQL](index.md 'index')

## DiGi\.GIS\.PostgreSQL Namespace
### Classes

<a name='DiGi.GIS.PostgreSQL.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

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

<a name='DiGi.GIS.PostgreSQL.Create.GISPostgreSQLConverterManager()'></a>

## Create\.GISPostgreSQLConverterManager\(\) Method

Initializes and returns a new instance of the [GISPostgreSQLConverterManager\(\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.GISPostgreSQLConverterManager() 'DiGi\.GIS\.PostgreSQL\.Create\.GISPostgreSQLConverterManager\(\)'),
configured using local configuration files from the executing assembly's directory\.

```csharp
public static DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager? GISPostgreSQLConverterManager();
```

#### Returns
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')  
A configured [GISPostgreSQLConverterManager\(\)](DiGi.GIS.PostgreSQL.md#DiGi.GIS.PostgreSQL.Create.GISPostgreSQLConverterManager() 'DiGi\.GIS\.PostgreSQL\.Create\.GISPostgreSQLConverterManager\(\)') if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int)'></a>

## Create\.TableAsync\_AdministrativeArea2D\(this NpgsqlConnection, int\) Method

Asynchronously creates the AdministrativeArea2D table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_AdministrativeArea2D(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2D(thisNpgsql.NpgsqlConnection,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string)'></a>

## Create\.TableAsync\_AdministrativeArea2DReferencedObject\(this NpgsqlConnection, string\) Method

Asynchronously creates the AdministrativeArea2DReferencedObject table for the specified table name\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_AdministrativeArea2DReferencedObject(this Npgsql.NpgsqlConnection? npgsqlConnection, string tableName);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_AdministrativeArea2DReferencedObject(thisNpgsql.NpgsqlConnection,string).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the table associated with the administrative area 2D referenced object\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int)'></a>

## Create\.TableAsync\_Building2D\(this NpgsqlConnection, int\) Method

Asynchronously creates the Building2D table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2D(this Npgsql.NpgsqlConnection? npgsqlConnection, int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The Npgsql connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D(thisNpgsql.NpgsqlConnection,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,System.Threading.CancellationToken)'></a>

## Create\.TableAsync\_Building2DReference\(this NpgsqlConnection, string, CancellationToken\) Method

Asynchronously creates the Building 2D reference table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2DReference(this Npgsql.NpgsqlConnection? npgsqlConnection, string? tableName, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,System.Threading.CancellationToken).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the table to be created for Building 2D references\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReference(thisNpgsql.NpgsqlConnection,string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used by other methods as a token for cancelling the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string)'></a>

## Create\.TableAsync\_Building2DReferencedObject\(this NpgsqlConnection, string\) Method

Asynchronously creates the Building 2D Referenced Object table for the specified table name\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2DReferencedObject(this Npgsql.NpgsqlConnection? npgsqlConnection, string tableName);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject(thisNpgsql.NpgsqlConnection,string).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') representing the name of the table to be created\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A [System\.Threading\.Tasks\.Task&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1') that represents the asynchronous operation\. The task result is a [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean') value indicating whether the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int)'></a>

## Create\.TableAsync\_Building2DReferencedObject\_Partition\(this NpgsqlConnection, string, int\) Method

Asynchronously creates a partition for the Building2DReferencedObject table based on the specified table name and county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2DReferencedObject_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, string tableName, int countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int).tableName'></a>

`tableName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the parent table that is being partitioned\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2DReferencedObject_Partition(thisNpgsql.NpgsqlConnection,string,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county for which the partition is created\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int)'></a>

## Create\.TableAsync\_Building2D\_Partition\(this NpgsqlConnection, int\) Method

Asynchronously creates a partition for the Building2D table associated with the specified county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_Building2D_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, int countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_Building2D_Partition(thisNpgsql.NpgsqlConnection,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county for which the partition is being created\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the Building2D partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_EPWFile(thisNpgsql.NpgsqlConnection)'></a>

## Create\.TableAsync\_EPWFile\(this NpgsqlConnection\) Method

Asynchronously creates the epw\_file table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_EPWFile(this Npgsql.NpgsqlConnection? npgsqlConnection);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_EPWFile(thisNpgsql.NpgsqlConnection).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas(thisNpgsql.NpgsqlConnection)'></a>

## Create\.TableAsync\_OrtoDatas\(this NpgsqlConnection\) Method

Asynchronously creates the OrtoDatas table in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_OrtoDatas(this Npgsql.NpgsqlConnection? npgsqlConnection);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas(thisNpgsql.NpgsqlConnection).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the OrtoDatas table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int)'></a>

## Create\.TableAsync\_OrtoDatas\_Partition\(this NpgsqlConnection, int\) Method

Asynchronously creates a partition for the OrtoDatas table based on the specified county identifier\.

```csharp
public static System.Threading.Tasks.Task<bool> TableAsync_OrtoDatas_Partition(this Npgsql.NpgsqlConnection? npgsqlConnection, int countyId);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Create.TableAsync_OrtoDatas_Partition(thisNpgsql.NpgsqlConnection,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the county for which the partition is being created\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the partition was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatas(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken)'></a>

## Modify\.RefreshOrtoDatas\(this GISPostgreSQLConverterManager, PostgreSQLOrtoDatasRefreshOptions, IProgress\<long\>, CancellationToken\) Method

Asynchronously refreshes orthodata in the PostgreSQL database based on the specified options\.

```csharp
public static System.Threading.Tasks.Task<bool> RefreshOrtoDatas(this DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager? gISPostgreSQLConverterManager, DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions, System.IProgress<long>? progress=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatas(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The manager used to retrieve the necessary PostgreSQL converters\.

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatas(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).PostgreSQLOrtoDatasRefreshOptions'></a>

`PostgreSQLOrtoDatasRefreshOptions` [PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions')

The options specifying how the orthodata should be refreshed\.

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatas(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

An optional progress reporter to track the number of processed building references\.

<a name='DiGi.GIS.PostgreSQL.Modify.RefreshOrtoDatas(thisDiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager,DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the refresh succeeded; otherwise, false\.

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

```csharp
public static void Update_Occupancy(this DiGi.Core.IO.Table.Classes.Table? table, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData>? building2DOccupancyDatas);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Occupancy(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData_).table'></a>

`table` [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')

The PostgreSQL table to be updated\.

<a name='DiGi.GIS.PostgreSQL.Modify.Update_Occupancy(thisDiGi.Core.IO.Table.Classes.Table,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData_).building2DOccupancyDatas'></a>

`building2DOccupancyDatas` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData') objects containing the new occupancy information\.

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