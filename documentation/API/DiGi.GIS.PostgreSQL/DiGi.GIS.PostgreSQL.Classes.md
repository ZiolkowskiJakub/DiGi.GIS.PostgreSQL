#### [DiGi\.GIS\.PostgreSQL](DiGi.GIS.PostgreSQL.Overview.md 'DiGi\.GIS\.PostgreSQL\.Overview')

## DiGi\.GIS\.PostgreSQL\.Classes Namespace
### Classes

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D'></a>

## AdministrativeAreal2D Class

Represents a 2D administrative area within the Polish territorial division hierarchy \(country, voivodeship, county, municipality\)\.

```csharp
public class AdministrativeAreal2D : DiGi.GIS.PostgreSQL.Classes.Areal2D<DiGi.GIS.Classes.AdministrativeAreal2D>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>')[DiGi\.GIS\.Classes\.AdministrativeAreal2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.administrativeareal2d 'DiGi\.GIS\.Classes\.AdministrativeAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>') → AdministrativeAreal2D
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.AdministrativeAreal2D()'></a>

## AdministrativeAreal2D\(\) Constructor

Initializes a new instance of the AdministrativeAreal2D class\.

```csharp
public AdministrativeAreal2D();
```

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.AdministrativeAreal2D(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D)'></a>

## AdministrativeAreal2D\(AdministrativeAreal2D\) Constructor

Initializes a new instance of the AdministrativeAreal2D class by copying data from another AdministrativeAreal2D instance\.

```csharp
public AdministrativeAreal2D(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D? administrativeAreal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.AdministrativeAreal2D(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D).administrativeAreal2D'></a>

`administrativeAreal2D` [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')

The AdministrativeAreal2D instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.AdministrativeAreal2D(System.Text.Json.Nodes.JsonObject)'></a>

## AdministrativeAreal2D\(JsonObject\) Constructor

Initializes a new instance of the AdministrativeAreal2D class from a JsonObject\.

```csharp
public AdministrativeAreal2D(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.AdministrativeAreal2D(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.AdministrativeArealType'></a>

## AdministrativeAreal2D\.AdministrativeArealType Property

Gets or sets the type of the administrative area \(e\.g\., country, voivodeship, county, municipality\)\.

```csharp
public DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType AdministrativeArealType { get; set; }
```

#### Property Value
[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.CountryId'></a>

## AdministrativeAreal2D\.CountryId Property

Gets or sets the ID of the country\.

```csharp
public System.Nullable<int> CountryId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.CountyId'></a>

## AdministrativeAreal2D\.CountyId Property

Gets or sets the ID of the county\.

```csharp
public System.Nullable<int> CountyId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.Id'></a>

## AdministrativeAreal2D\.Id Property

Gets or sets the ID of the administrative area\.

```csharp
public int Id { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.MunicipalityId'></a>

## AdministrativeAreal2D\.MunicipalityId Property

Gets or sets the ID of the municipality\.

```csharp
public System.Nullable<int> MunicipalityId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.Name'></a>

## AdministrativeAreal2D\.Name Property

Gets or sets the name of the administrative area\.

```csharp
public string? Name { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D.VoivodeshipId'></a>

## AdministrativeAreal2D\.VoivodeshipId Property

Gets or sets the ID of the voivodeship\.

```csharp
public System.Nullable<int> VoivodeshipId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData'></a>

## AdministrativeAreal2DOccupancyData Class

Represents occupancy data for a 2D administrative area\.

```csharp
public class AdministrativeAreal2DOccupancyData : DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject<DiGi.GIS.Interfaces.IOccupancyData>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>') → AdministrativeAreal2DOccupancyData
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData.AdministrativeAreal2DOccupancyData()'></a>

## AdministrativeAreal2DOccupancyData\(\) Constructor

Initializes a new instance of the AdministrativeAreal2DOccupancyData class\.

```csharp
public AdministrativeAreal2DOccupancyData();
```

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData.AdministrativeAreal2DOccupancyData(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData)'></a>

## AdministrativeAreal2DOccupancyData\(AdministrativeAreal2DOccupancyData\) Constructor

Initializes a new instance of the AdministrativeAreal2DOccupancyData class by copying data from another AdministrativeAreal2DOccupancyData instance\.

```csharp
public AdministrativeAreal2DOccupancyData(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData? administrativeAreal2DOccupancyData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData.AdministrativeAreal2DOccupancyData(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData).administrativeAreal2DOccupancyData'></a>

`administrativeAreal2DOccupancyData` [AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData')

The AdministrativeAreal2DOccupancyData instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData.AdministrativeAreal2DOccupancyData(System.Text.Json.Nodes.JsonObject)'></a>

## AdministrativeAreal2DOccupancyData\(JsonObject\) Constructor

Initializes a new instance of the AdministrativeAreal2DOccupancyData class from a JsonObject\.

```csharp
public AdministrativeAreal2DOccupancyData(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData.AdministrativeAreal2DOccupancyData(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyDataPostgreSQLConverter'></a>

## AdministrativeAreal2DOccupancyDataPostgreSQLConverter Class

Converter for administrative areal 2D occupancy data used in PostgreSQL database operations\.

```csharp
public class AdministrativeAreal2DOccupancyDataPostgreSQLConverter : DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData, DiGi.GIS.Interfaces.IOccupancyData>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>')[AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>') → AdministrativeAreal2DOccupancyDataPostgreSQLConverter
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyDataPostgreSQLConverter.AdministrativeAreal2DOccupancyDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## AdministrativeAreal2DOccupancyDataPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [AdministrativeAreal2DOccupancyDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyDataPostgreSQLConverter') class\.

```csharp
public AdministrativeAreal2DOccupancyDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyDataPostgreSQLConverter.AdministrativeAreal2DOccupancyDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The connection data for the PostgreSQL database\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyDataPostgreSQLConverter.TableName'></a>

## AdministrativeAreal2DOccupancyDataPostgreSQLConverter\.TableName Property

Gets the name of the table associated with administrative areal 2D occupancy data\.

```csharp
public override string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter'></a>

## AdministrativeAreal2DPostgreSQLConverter Class

Provides functionality to convert and manage [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') entities within a PostgreSQL database, implementing the [IGISPostgreSQLConverter&lt;TTableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>') interface\.

```csharp
public class AdministrativeAreal2DPostgreSQLConverter : DiGi.PostgreSQL.Classes.PostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → AdministrativeAreal2DPostgreSQLConverter

Implements [DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>'), [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.AdministrativeAreal2DPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## AdministrativeAreal2DPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter') class\.

```csharp
public AdministrativeAreal2DPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.AdministrativeAreal2DPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings required to establish a connection to the PostgreSQL database\.
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.ClearAsync(System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.ClearAsync\(CancellationToken\) Method

Asynchronously clears all data from the administrative areal 2D table in the PostgreSQL database\.

```csharp
public System.Threading.Tasks.Task<bool> ClearAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.ClearAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the table was cleared successfully; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.CreateTableAsync(int)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.CreateTableAsync\(int\) Method

Asynchronously creates the PostgreSQL table for administrative areal 2D data and performs an analysis on the created table\.

```csharp
public System.Threading.Tasks.Task<bool> CreateTableAsync(int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.CreateTableAsync(int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The time interval, in seconds, to wait for the command to complete before timing out\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the table was created successfully; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByCodeAsync(string)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DByCodeAsync\(string\) Method

Asynchronously retrieves an administrative areal 2D based on the specified code\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D?> GetAdministrativeAreal2DByCodeAsync(string code);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByCodeAsync(string).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique string code of the administrative areal 2D\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(int,int)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DByIdAsync\(int, int\) Method

Asynchronously retrieves an administrative areal 2D based on the specified identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D?> GetAdministrativeAreal2DByIdAsync(int id, int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(int,int).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique integer identifier of the administrative areal 2D\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(int,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken,int)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DByIdAsync\(NpgsqlConnection, int, CancellationToken, int\) Method

Asynchronously retrieves an administrative areal 2D by its unique identifier\.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D?> GetAdministrativeAreal2DByIdAsync(Npgsql.NpgsqlConnection? npgsqlConnection, int id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken,int).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the administrative areal 2D to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferenceByCodeAsync\(string, Nullable\<AdministrativeArealType\>, CancellationToken\) Method

Asynchronously retrieves a reference to an administrative areal 2D based on the provided code and optional type\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference?> GetAdministrativeAreal2DReferenceByCodeAsync(string code, System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> administrativeArealType=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique code of the administrative areal 2D\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to filter the search results\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(int,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferenceByIdAsync\(int, CancellationToken\) Method

Asynchronously retrieves an [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') by its unique identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference?> GetAdministrativeAreal2DReferenceByIdAsync(int id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(int,System.Threading.CancellationToken).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the administrative areal 2D reference to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferenceByIdAsync\(NpgsqlConnection, int, CancellationToken\) Method

Asynchronously retrieves an administrative areal 2D reference by its unique identifier\.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference?> GetAdministrativeAreal2DReferenceByIdAsync(Npgsql.NpgsqlConnection? npgsqlConnection, int id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the administrative areal 2D reference to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByIdAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathAsync\(AdministrativeAreal2DReference, CancellationToken\) Method

Asynchronously retrieves the administrative areal 2D reference path for the specified administrative areal 2D reference\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference administrativeAreal2DReference, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken).administrativeAreal2DReference'></a>

`administrativeAreal2DReference` [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')

The administrative areal 2D reference used to retrieve the corresponding path\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(int,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathAsync\(int, CancellationToken\) Method

Asynchronously retrieves an administrative areal 2D reference path by its unique identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(int id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(int,System.Threading.CancellationToken).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the administrative areal 2D reference path\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathAsync\(NpgsqlConnection, AdministrativeAreal2DReference, CancellationToken\) Method

Asynchronously retrieves the reference path for the specified administrative areal 2D reference\.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference administrativeAreal2DReference, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken).administrativeAreal2DReference'></a>

`administrativeAreal2DReference` [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')

The [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') for which the reference path is retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathAsync\(NpgsqlConnection, int, CancellationToken\) Method

Asynchronously retrieves the reference path for the specified administrative areal 2D identifier\.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath?> GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection? npgsqlConnection, int id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The unique identifier of the administrative areal 2D entity\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(Npgsql.NpgsqlConnection,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath') if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathsAsync\(NpgsqlConnection, IEnumerable\<AdministrativeAreal2DReference\>, CancellationToken\) Method

Asynchronously retrieves a list of reference paths for the specified collection of administrative areal 2D references\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath>?> GetAdministrativeAreal2DReferencePathsAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference> administrativeAreal2DReferences, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Threading.CancellationToken).administrativeAreal2DReferences'></a>

`administrativeAreal2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects for which paths are retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathsByNameAsync\(NpgsqlConnection, string, CancellationToken\) Method

Searches for administrative areas by name \(case\-insensitive\) and returns a list of references\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath>?> GetAdministrativeAreal2DReferencePathsByNameAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string text, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

Existing Npgsql connection\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).text'></a>

`text` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The text to search for within the name column\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

Cancellation token\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A list of AdministrativeAreal2DReference objects matching the search criteria\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(string,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencePathsByNameAsync\(string, CancellationToken\) Method

Searches for administrative areas by name \(case\-insensitive\) and returns a list of references\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath>?> GetAdministrativeAreal2DReferencePathsByNameAsync(string text, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(string,System.Threading.CancellationToken).text'></a>

`text` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The text to search for within the name column\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathsByNameAsync(string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

Cancellation token\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A list of AdministrativeAreal2DReference objects matching the search criteria\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync\(AdministrativeArealType, Nullable\<int\>, bool, CancellationToken, int\) Method

Asynchronously retrieves a list of administrative areal 2D references based on the specified administrative areal type, an optional parent identifier, and a uniqueness flag\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Nullable<int> parentId=null, bool uniqueCode=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') that defines the category of administrative areals to be retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).parentId'></a>

`parentId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the parent administrative areal used to filter for child elements\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).uniqueCode'></a>

`uniqueCode` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether the retrieval should be filtered by unique codes\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync\(NpgsqlConnection, AdministrativeArealType, IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references based on the specified administrative area type and parent identifiers\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Collections.Generic.IEnumerable<int> parentIds, bool uniqueCode=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The target [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') of the references to be retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).parentIds'></a>

`parentIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integer identifiers for the parent administrative areas\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).uniqueCode'></a>

`uniqueCode` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether a unique code should be used during retrieval\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync\(NpgsqlConnection, AdministrativeArealType, Nullable\<int\>, bool, CancellationToken, int\) Method

Asynchronously retrieves a list of administrative areal 2D references filtered by the specified administrative areal type and optionally by a parent identifier\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Nullable<int> parentId=null, bool uniqueCode=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') that filters the administrative areal references\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).parentId'></a>

`parentId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional unique identifier of the parent administrative area\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).uniqueCode'></a>

`uniqueCode` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A value indicating whether to filter by a unique code\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,bool,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of references if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByCodeAsync\(NpgsqlConnection, string, AdministrativeArealType, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references based on the specified code and administrative areal type\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string code, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The identification code used to search for the administrative areal 2D references\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The type of administrative areal to filter by\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if found; otherwise, null or an empty list\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByCodeAsync\(NpgsqlConnection, string, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references based on the specified code\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string code, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The identification code used to search for the administrative areal 2D references\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if found; otherwise, null or an empty list\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByCodeAsync\(string, AdministrativeArealType, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references based on the specified code and administrative areal type\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(string code, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string code used to identify the administrative areals\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') specifying the category of the administrative areal\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if matches are found; otherwise, null or an empty list\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByCodeAsync\(string, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references associated with the specified code\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByCodeAsync(string code, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string code used to identify the administrative areals\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync(string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if found; otherwise, null or an empty list\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByIdsAsync\(NpgsqlConnection, IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references from the database based on the provided identifiers\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByIdsAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<int> ids, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integer identifiers for the records to be retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if successful; otherwise, null if the connection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByIdsAsync\(IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references associated with the specified identifiers using the internal connection data\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByIdsAsync(System.Collections.Generic.IEnumerable<int> ids, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of integer identifiers used to retrieve the administrative areal 2D references\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if matches are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByNameAsync\(NpgsqlConnection, string, CancellationToken\) Method

Searches for administrative areas by name \(case\-insensitive\) and returns a list of references\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByNameAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string text, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

Existing Npgsql connection\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).text'></a>

`text` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The text to search for within the name column\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(Npgsql.NpgsqlConnection,string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

Cancellation token\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A list of AdministrativeAreal2DReference objects matching the search criteria\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(string,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DReferencesByNameAsync\(string, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D references that match the specified search text using the internal connection data\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> GetAdministrativeAreal2DReferencesByNameAsync(string text, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(string,System.Threading.CancellationToken).text'></a>

`text` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The search string used to filter administrative areal 2D references by their name\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByNameAsync(string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference') objects if matches are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByAdministrativeArealType\(AdministrativeArealType, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D objects filtered by the specified administrative areal type and an optional parent identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByAdministrativeArealType(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Nullable<int> parentId=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to filter the administrative areal objects\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).parentId'></a>

`parentId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional unique identifier of the parent administrative areal object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByAdministrativeArealType\(NpgsqlConnection, AdministrativeArealType, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D entities based on the specified administrative areal type and an optional parent identifier\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByAdministrativeArealType(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Nullable<int> parentId=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') of the entities to be retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).parentId'></a>

`parentId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional unique identifier of the parent administrative areal entity\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealType(Npgsql.NpgsqlConnection,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of matching entities, or null if the connection is null or no valid entities are found based on the provided criteria\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByAdministrativeArealTypeAsync\(AdministrativeArealType\) Method

Asynchronously retrieves a list of administrative areal 2D objects filtered by the specified administrative areal type\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByAdministrativeArealTypeAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to filter the administrative areal objects\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects if successful; otherwise, null or an empty list\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByBoundingBox2DAsync\(BoundingBox2D, AdministrativeArealType, double\) Method

Asynchronously retrieves a list of administrative areal 2D objects that fall within the area defined by the specified bounding box and match the provided administrative areal type\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D') defining the search area\. If this value is null, the method returns null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to filter the administrative areal objects\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value representing the distance tolerance used for the search operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the bounding box is null or the administrative areal type is undefined\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByBoundingBox2DAsync\(BoundingBox2D, double\) Method

Asynchronously retrieves a list of administrative areal 2D objects that fall within the area defined by the specified bounding box\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D') defining the search area\. If this value is null, an empty list may be returned\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value representing the distance tolerance used for the search operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the retrieval fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByBoundingBox2DAsync\(BoundingBox2D, IEnumerable\<AdministrativeArealType\>, double, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D objects that fall within the area defined by the specified bounding box and match any of the provided administrative areal types\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType>? administrativeArealTypes, double tolerance=0.001, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D') defining the search area\. If this value is null, the method returns null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).administrativeArealTypes'></a>

`administrativeArealTypes` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') values used to filter the administrative areal objects\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value representing the distance tolerance used for the search operation\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the bounding box is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByBoundingBox2DAsync\(NpgsqlConnection, BoundingBox2D, AdministrativeArealType, double, CancellationToken\) Method

Gets AdministrativeAreal2D for given AdministrativeArealType \(not iterative way\)\. It will check all records with given AdministrativeArealType

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, double tolerance=0.001, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

NpgsqlConnection

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double,System.Threading.CancellationToken).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

BoundingBox2D

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

AdministrativeArealType

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double,System.Threading.CancellationToken).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

Tolerance

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
AdministrativeAreal2D list

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByBoundingBox2DAsync\(NpgsqlConnection, BoundingBox2D, IEnumerable\<AdministrativeArealType\>, double, CancellationToken\) Method

Gets AdministrativeAreal2D for given AdministrativeArealTypes \(Iterative way\)\. It will iterate in order through Country, Voivodeship, County, Municipality to reduce number of objects\. BoundingBox2D in range Country check, then Voivodeship in this specific Country etc\.\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType>? administrativeArealTypes, double tolerance=0.001, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

NpgsqlConnection

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

BoundingBox2D

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).administrativeArealTypes'></a>

`administrativeArealTypes` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

AdministrativeArealTypes

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

Tolerance

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByBoundingBox2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.BoundingBox2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
AdministrativeAreal2D list

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByCircle2DAsync\(Circle2D, AdministrativeArealType, double\) Method

Asynchronously retrieves a list of administrative areal 2D objects that fall within the area defined by the specified circle and match the given administrative areal type\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D? circle2D, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).circle2D'></a>

`circle2D` [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D')

The [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D') defining the search area\. If this value is null, an empty list is returned\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to filter the administrative areal objects\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value representing the distance tolerance added to the circle's radius for the search operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the retrieval fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByCircle2DAsync\(Circle2D, double\) Method

Asynchronously retrieves a list of administrative areal 2D objects that fall within the area defined by the specified circle\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D? circle2D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double).circle2D'></a>

`circle2D` [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D')

The [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D') defining the search area\. If this value is null, an empty list is returned\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value representing the distance tolerance added to the circle's radius for the search operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the retrieval fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByCircle2DAsync\(Circle2D, IEnumerable\<AdministrativeArealType\>, double\) Method

Asynchronously retrieves a list of administrative areal 2D objects that fall within the area defined by the specified circle and match any of the provided administrative areal types\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D? circle2D, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType>? administrativeArealTypes, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double).circle2D'></a>

`circle2D` [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D')

The [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D') defining the search area\. If this value is null, an empty list is returned\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double).administrativeArealTypes'></a>

`administrativeArealTypes` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') values used to filter the administrative areal objects\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value representing the distance tolerance added to the circle's radius for the search operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the retrieval fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByCodeAsync\(NpgsqlConnection, string, Nullable\<AdministrativeArealType\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D objects from the database based on the specified code and optional type\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCodeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string code, System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> administrativeArealType, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string code used to identify the administrative areal 2D objects\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to filter the results by a specific type\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByCodeAsync\(string, Nullable\<AdministrativeArealType\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D objects based on the specified code and an optional administrative areal type\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCodeAsync(string code, System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> administrativeArealType, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string representation of the code used to filter the administrative areal records\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to further refine the search results\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the database connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodesAsync(System.Collections.Generic.IEnumerable_string_)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByCodesAsync\(IEnumerable\<string\>\) Method

Asynchronously retrieves a list of administrative areal 2D objects based on the provided collection of codes\.

If the codes collection is null or empty, all records from the table are retrieved.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByCodesAsync(System.Collections.Generic.IEnumerable<string>? codes=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByCodesAsync(System.Collections.Generic.IEnumerable_string_).codes'></a>

`codes` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of strings representing the codes used to filter the results\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the database connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByIdsAsync\(NpgsqlConnection, IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D objects from the database based on the provided identifiers\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByIdsAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<int> ids, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of integer identifiers for the administrative areal 2D objects to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(System.Collections.Generic.IEnumerable_int_)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByIdsAsync\(IEnumerable\<int\>\) Method

Asynchronously retrieves a list of administrative areas based on the provided identifiers\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByIdsAsync(System.Collections.Generic.IEnumerable<int>? ids=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(System.Collections.Generic.IEnumerable_int_).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of integer identifiers used to filter the results\. If this parameter is null or empty, no ID filtering is applied\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByPoint2DAsync\(Point2D, AdministrativeArealType, double\) Method

Asynchronously retrieves a list of administrative areas of a specific type that contain or are near the specified 2D point within the given tolerance\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D? point2D, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The 2D point used to search for administrative areas\. If this value is null, the method returns null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The type of administrative area to retrieve\. If this value is [Undefined](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.Undefined 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType\.Undefined'), the method returns null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used for the spatial query\. The default value is [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of matching [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the provided point is null or the administrative area type is undefined\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByPoint2DAsync\(Point2D, double\) Method

Asynchronously retrieves a list of administrative areas that contain or are near the specified 2D point within the given tolerance\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D? point2D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,double).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The 2D point used to search for administrative areas\. If this value is null, the method returns null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used for the spatial query\. The default value is [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of matching [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the provided point is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByPoint2DAsync\(Point2D, IEnumerable\<AdministrativeArealType\>, double\) Method

Asynchronously retrieves a list of administrative areas that contain or are near the specified 2D point, filtered by the provided administrative area types and within the given tolerance\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D? point2D, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType>? administrativeArealTypes, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The 2D point used to search for administrative areas\. If this value is null, the method returns null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double).administrativeArealTypes'></a>

`administrativeArealTypes` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of administrative area types to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used for the spatial query\. The default value is [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of matching [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects, or null if the provided point is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetAdministrativeAreal2DsByPoint2DAsync\(NpgsqlConnection, Point2D, AdministrativeArealType, double\) Method

Asynchronously retrieves a list of administrative areal 2D records from the database that encompass or are within a specified tolerance of the provided 2D point\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>?> GetAdministrativeAreal2DsByPoint2DAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.Geometry.Planar.Classes.Point2D? point2D, DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') coordinates used to filter the administrative areals\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') specifying the category of administrative areal to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The double value representing the distance tolerance applied to the bounding box check\. Defaults to [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects if found; otherwise, an empty list or null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetCodesAsync()'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetCodesAsync\(\) Method

Asynchronously retrieves all codes of administrative areal 2D entities from the database\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<string>?> GetCodesAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of strings representing the codes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the database connection cannot be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetCountAsync\(NpgsqlConnection, CancellationToken\) Method

Asynchronously retrieves the total number of administrative areal 2D records from the database\.

```csharp
public static System.Threading.Tasks.Task<long> GetCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total number of records, or \-1 if the connection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetCountAsync(System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetCountAsync\(CancellationToken\) Method

Asynchronously retrieves the total count of administrative areal 2D entities from the database\.

```csharp
public System.Threading.Tasks.Task<long> GetCountAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetCountAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64'); returns \-1 if the database connection cannot be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(bool,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetEstimatedCountAsync\(bool, CancellationToken\) Method

Asynchronously retrieves an estimated count of the administrative areal 2D entities from the database\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to analyze the table before retrieving the estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated count as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64'); returns \-1 if the database connection cannot be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetEstimatedCountAsync\(NpgsqlConnection, bool, CancellationToken\) Method

Asynchronously retrieves an estimated count of the administrative areal 2D records from the database\.

```csharp
public static System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to perform a table analysis before retrieving the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated number of records, or \-1 if the connection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetIdByCodeAsync\(NpgsqlConnection, string, Nullable\<AdministrativeArealType\>, CancellationToken\) Method

Asynchronously retrieves the identifier of an administrative areal based on its code and optional type\.

```csharp
public static System.Threading.Tasks.Task<System.Nullable<int>> GetIdByCodeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string? code, System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> administrativeArealType=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string code identifying the administrative areal\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') to filter the search\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the identifier of the administrative areal if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetIdByCodeAsync\(string, Nullable\<AdministrativeArealType\>\) Method

Asynchronously retrieves the identifier of an administrative areal 2D entity based on the specified code and type\.

```csharp
public System.Threading.Tasks.Task<System.Nullable<int>> GetIdByCodeAsync(string? code, System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> administrativeArealType=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The identification code of the administrative areal entity\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(string,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_).administrativeArealType'></a>

`administrativeArealType` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The type of the administrative areal entity\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the identifier as an [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') if found; otherwise, `null`\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsAsync()'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetIdsAsync\(\) Method

Asynchronously retrieves all identifiers for administrative areal 2D entities from the database\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<int>?> GetIdsAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of integers containing the IDs, or `null` if the database connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetIdsAsync\(AdministrativeArealType, CancellationToken\) Method

Asynchronously retrieves a set of identifiers for administrative areal entities of the specified type from the database\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<int>?> GetIdsAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The type of administrative areal used to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsAsync(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used by other objects to signal that the asynchronous operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of integers containing the IDs, or `null` if the database connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_int_,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetIdsByCodeAsync\(NpgsqlConnection, string, Nullable\<int\>, Nullable\<AdministrativeArealType\>, CancellationToken\) Method

Asynchronously retrieves a set of identifiers for administrative areals that match the specified code and optional criteria\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.HashSet<int>?> GetIdsByCodeAsync(Npgsql.NpgsqlConnection? npgsqlConnection, string? code, System.Nullable<int> limit=null, System.Nullable<DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType> administrativeArealType=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_int_,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_int_,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string code used to filter the administrative areals\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_int_,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer specifying the maximum number of identifiers to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_int_,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).administrativeArealType'></a>

`administrativeArealType` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType') used to further filter the results by type\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetIdsByCodeAsync(Npgsql.NpgsqlConnection,string,System.Nullable_int_,System.Nullable_DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of integers representing the IDs if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetSubCodesAsync(string)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.GetSubCodesAsync\(string\) Method

Asynchronously retrieves a collection of sub\-codes that start with the specified code prefix from the database\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<string>?> GetSubCodesAsync(string code);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.GetSubCodesAsync(string).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The parent code or prefix used to filter and identify the associated sub\-codes\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of strings containing the matching codes, or `null` if the database connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.RefreshAsync\(PostgreSQLAdministrativeAreal2DRefreshOptions, IProgress\<long\>, CancellationToken\) Method

Asynchronously refreshes the administrative areal 2D data within the PostgreSQL database\.

```csharp
public System.Threading.Tasks.Task<bool> RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions? postgreSQLAdministrativeAreal2DRefreshOptions=null, System.IProgress<long>? progress=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).postgreSQLAdministrativeAreal2DRefreshOptions'></a>

`postgreSQLAdministrativeAreal2DRefreshOptions` [PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions')

The options used to configure the refresh process\. If null, a new instance of [PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions') is initialized\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

The provider for reporting the progress of the refresh operation as a long value\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while carrying out the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains true if the refresh was successful; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.RemoveAsync(System.Collections.Generic.IEnumerable_int_)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.RemoveAsync\(IEnumerable\<int\>\) Method

Asynchronously removes administrative areal 2D records from the database based on the provided identifiers\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<int>?> RemoveAsync(System.Collections.Generic.IEnumerable<int>? ids);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.RemoveAsync(System.Collections.Generic.IEnumerable_int_).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of integer identifiers for the records to be removed\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of IDs that were successfully deleted, or null if the input was null or a database connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_)'></a>

## AdministrativeAreal2DPostgreSQLConverter\.UpdateAsync\(IEnumerable\<AdministrativeAreal2D\>\) Method

Asynchronously updates a collection of administrative areal 2D records in the PostgreSQL database\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<int>?> UpdateAsync(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D>? administrativeAreal2Ds);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D_).administrativeAreal2Ds'></a>

`administrativeAreal2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D') objects to be updated\. This value can be null\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of integers representing the IDs of the records that were processed, or null if the update process failed or the input was null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference'></a>

## AdministrativeAreal2DReference Class

Represents a reference to a 2D administrative area within the Polish territorial division hierarchy \(country, voivodeship, county, municipality\)\.

```csharp
public class AdministrativeAreal2DReference : DiGi.GIS.PostgreSQL.Classes.Areal2DReference
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [Areal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2DReference') → AdministrativeAreal2DReference
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.AdministrativeAreal2DReference()'></a>

## AdministrativeAreal2DReference\(\) Constructor

Initializes a new instance of the AdministrativeAreal2DReference class\.

```csharp
public AdministrativeAreal2DReference();
```

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.AdministrativeAreal2DReference(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference)'></a>

## AdministrativeAreal2DReference\(AdministrativeAreal2DReference\) Constructor

Initializes a new instance of the AdministrativeAreal2DReference class by copying data from another AdministrativeAreal2DReference instance\.

```csharp
public AdministrativeAreal2DReference(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference? administrativeAreal2DReference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.AdministrativeAreal2DReference(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference).administrativeAreal2DReference'></a>

`administrativeAreal2DReference` [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')

The AdministrativeAreal2DReference instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.AdministrativeAreal2DReference(System.Text.Json.Nodes.JsonObject)'></a>

## AdministrativeAreal2DReference\(JsonObject\) Constructor

Initializes a new instance of the AdministrativeAreal2DReference class from a JsonObject\.

```csharp
public AdministrativeAreal2DReference(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.AdministrativeAreal2DReference(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.AdministrativeArealType'></a>

## AdministrativeAreal2DReference\.AdministrativeArealType Property

Gets or sets the type of the administrative area \(e\.g\., country, voivodeship, county, municipality\)\.

```csharp
public DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType AdministrativeArealType { get; set; }
```

#### Property Value
[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.Code'></a>

## AdministrativeAreal2DReference\.Code Property

Gets or sets the code of the administrative area\.

```csharp
public string? Code { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.CountryId'></a>

## AdministrativeAreal2DReference\.CountryId Property

Gets or sets the ID of the country\.

```csharp
public System.Nullable<int> CountryId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.Id'></a>

## AdministrativeAreal2DReference\.Id Property

Gets or sets the ID of the administrative area\.

```csharp
public int Id { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.MunicipalityId'></a>

## AdministrativeAreal2DReference\.MunicipalityId Property

Gets or sets the ID of the municipality\.

```csharp
public System.Nullable<int> MunicipalityId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.Name'></a>

## AdministrativeAreal2DReference\.Name Property

Gets or sets the name of the administrative area\.

```csharp
public string? Name { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.VoivodeshipId'></a>

## AdministrativeAreal2DReference\.VoivodeshipId Property

Gets or sets the ID of the voivodeship\.

```csharp
public System.Nullable<int> VoivodeshipId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference.GetIds()'></a>

## AdministrativeAreal2DReference\.GetIds\(\) Method

Gets a list of ids in the record including own Id \(in order: CountryId, VoivodeshipId, CountyId, MunicipalityId, Id\)\. Null values are skipped\.

```csharp
public System.Collections.Generic.List<int> GetIds();
```

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of integer IDs representing the territorial hierarchy\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_'></a>

## AdministrativeAreal2DReferencedObject\<TUniqueObject\> Class

Abstract base class for referenced objects associated with 2D administrative areas, providing identification properties\.

```csharp
public abstract class AdministrativeAreal2DReferencedObject<TUniqueObject> : DiGi.GIS.PostgreSQL.Classes.ReferencedObject<TUniqueObject>
    where TUniqueObject : DiGi.Core.Interfaces.IUniqueObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.TUniqueObject'></a>

`TUniqueObject`

The type of the unique object this referenced object points to\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → AdministrativeAreal2DReferencedObject\<TUniqueObject\>

Derived  
↳ [AdministrativeAreal2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyData')  
↳ [TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.AdministrativeAreal2DReferencedObject()'></a>

## AdministrativeAreal2DReferencedObject\(\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencedObject class\.

```csharp
public AdministrativeAreal2DReferencedObject();
```

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.AdministrativeAreal2DReferencedObject(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_)'></a>

## AdministrativeAreal2DReferencedObject\(AdministrativeAreal2DReferencedObject\<TUniqueObject\>\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencedObject class by copying data from another AdministrativeAreal2DReferencedObject instance\.

```csharp
public AdministrativeAreal2DReferencedObject(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject<TUniqueObject>? administrativeAreal2DReferencedObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.AdministrativeAreal2DReferencedObject(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_).administrativeAreal2DReferencedObject'></a>

`administrativeAreal2DReferencedObject` [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>')

The AdministrativeAreal2DReferencedObject instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.AdministrativeAreal2DReferencedObject(System.Text.Json.Nodes.JsonObject)'></a>

## AdministrativeAreal2DReferencedObject\(JsonObject\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencedObject class from a JsonObject\.

```csharp
public AdministrativeAreal2DReferencedObject(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.AdministrativeAreal2DReferencedObject(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_.Id'></a>

## AdministrativeAreal2DReferencedObject\<TUniqueObject\>\.Id Property

Gets or sets the ID of the administrative area\.

```csharp
public int Id { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\> Class

Provides an abstract base implementation for a PostgreSQL converter specifically designed for administrative areal 2D referenced objects\.

```csharp
public abstract class AdministrativeAreal2DReferencedObjectPostgreSQLConverter<TAdministrativeAreal2DReferencedObject,TUniqueObject> : DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter<TAdministrativeAreal2DReferencedObject, TUniqueObject>
    where TAdministrativeAreal2DReferencedObject : DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject<TUniqueObject>
    where TUniqueObject : DiGi.Core.Interfaces.IUniqueObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject'></a>

`TAdministrativeAreal2DReferencedObject`

The type of the administrative areal 2D referenced object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TUniqueObject'></a>

`TUniqueObject`

The type of the unique object used for identification, which must implement the [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject') interface\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') → AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>

Derived  
↳ [AdministrativeAreal2DOccupancyDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DOccupancyDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DOccupancyDataPostgreSQLConverter')  
↳ [TypologyModelPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModelPostgreSQLConverter')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.AdministrativeAreal2DReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [AdministrativeAreal2DReferencedObjectPostgreSQLConverter&lt;TAdministrativeAreal2DReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>') class\.

```csharp
public AdministrativeAreal2DReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.AdministrativeAreal2DReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings required to establish a connection to the PostgreSQL database\. This value can be null\.
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.Create\(int, string, string, JsonObject, Nullable\<DateTime\>\) Method

Creates a new instance of a \<seeref name="TAdministrativeAreal2DReferencedObject" /\>\.

```csharp
protected abstract TAdministrativeAreal2DReferencedObject Create(int id, string? uniqueId, string? reference, System.Text.Json.Nodes.JsonObject? @object, System.Nullable<System.DateTime> createdAt);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier for the object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).uniqueId'></a>

`uniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional unique string identifier for the object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional reference string associated with the object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).object'></a>

`object` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The optional [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing additional data for the object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).createdAt'></a>

`createdAt` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime') indicating when the object was created\.

#### Returns
[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')  
A new instance of \<seeref name="TAdministrativeAreal2DReferencedObject" /\>\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(Npgsql.NpgsqlDataReader)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.Create\(NpgsqlDataReader\) Method

Creates an instance of \<seeref name="TAdministrativeAreal2DReferencedObject" /\> using the data provided by the [Npgsql\.NpgsqlDataReader](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqldatareader 'Npgsql\.NpgsqlDataReader')\.

```csharp
protected override TAdministrativeAreal2DReferencedObject Create(Npgsql.NpgsqlDataReader npgsqlDataReader);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.Create(Npgsql.NpgsqlDataReader).npgsqlDataReader'></a>

`npgsqlDataReader` [Npgsql\.NpgsqlDataReader](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqldatareader 'Npgsql\.NpgsqlDataReader')

The [Npgsql\.NpgsqlDataReader](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqldatareader 'Npgsql\.NpgsqlDataReader') used to read the record from the database\.

#### Returns
[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')  
An instance of \<seeref name="TAdministrativeAreal2DReferencedObject" /\> populated with data from the reader\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetCountAsync\(NpgsqlConnection, CancellationToken\) Method

Asynchronously retrieves the total count of records from the database\.

```csharp
public static System.Threading.Tasks.Task<long> GetCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetCountAsync(System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetCountAsync\(CancellationToken\) Method

Asynchronously retrieves the total count of records\.

```csharp
public System.Threading.Tasks.Task<long> GetCountAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetCountAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total record count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(bool,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetEstimatedCountAsync\(bool, CancellationToken\) Method

Asynchronously gets an estimated row count\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean indicating whether to run VACUUM ANALYZE before fetching the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated number of rows as a long, or \-1 if an error occurs or the table does not exist\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetEstimatedCountAsync\(NpgsqlConnection, bool, CancellationToken\) Method

Asynchronously retrieves an estimated count of records from the database\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to perform an ANALYZE operation to update statistics before retrieving the estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated number of records as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByIdAsync(int,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemByIdAsync\(int, CancellationToken\) Method

Asynchronously retrieves a \<seeref name="TAdministrativeAreal2DReferencedObject" /\> by its unique identifier\.

```csharp
public System.Threading.Tasks.Task<TAdministrativeAreal2DReferencedObject?> GetItemByIdAsync(int id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByIdAsync(int,System.Threading.CancellationToken).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the item to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByIdAsync(int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the \<seeref name="TAdministrativeAreal2DReferencedObject" /\> if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Threading.CancellationToken,int)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemByReferenceAsync\(string, CancellationToken, int\) Method

Asynchronously retrieves an item of type \<seeref name="TAdministrativeAreal2DReferencedObject" /\> using the specified reference\.

```csharp
public System.Threading.Tasks.Task<TAdministrativeAreal2DReferencedObject?> GetItemByReferenceAsync(string reference, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Threading.CancellationToken,int).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string reference used to locate the object\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the \<seeref name="TAdministrativeAreal2DReferencedObject" /\> if an item with the specified reference is found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemsByIdsAsync\(NpgsqlConnection, IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D referenced objects based on the provided identifiers\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TAdministrativeAreal2DReferencedObject>?> GetItemsByIdsAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<int>? ids, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the unique identifiers of the objects to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A token to observe for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of \<seeref name="TAdministrativeAreal2DReferencedObject" /\> matching the provided identifiers, or null if the connection or the collection of identifiers is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemsByIdsAsync\(IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of administrative areal 2D referenced objects based on the specified identifiers\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TAdministrativeAreal2DReferencedObject>?> GetItemsByIdsAsync(System.Collections.Generic.IEnumerable<int>? ids, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') representing the unique identifiers of the items to retrieve\. This parameter can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of matching objects, or null if no items are found or the provided identifiers collection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_long_,System.Threading.CancellationToken)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemsByReferenceAsync\(string, Nullable\<long\>, CancellationToken\) Method

Asynchronously retrieves a list of items that match the specified reference\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TAdministrativeAreal2DReferencedObject>?> GetItemsByReferenceAsync(string reference, System.Nullable<long> limit=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_long_,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string reference used to identify the items\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_long_,System.Threading.CancellationToken).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional long integer specifying the maximum number of items to return\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject') objects, or null if no items are found\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemsByReferencesAsync\(NpgsqlConnection, IEnumerable\<string\>, Nullable\<long\>, CancellationToken, int\) Method

Asynchronously retrieves items of type [TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject') based on the provided references\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TAdministrativeAreal2DReferencedObject>?> GetItemsByReferencesAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<string>? references, System.Nullable<long> limit=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection to use for the query\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of strings representing the references used to identify the items to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional maximum number of items to retrieve\. If null, no limit is applied\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of matching [TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject') items, or null if the connection or references are null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.GetItemsByReferencesAsync\(IEnumerable\<string\>, Nullable\<long\>, CancellationToken, int\) Method

Asynchronously retrieves a list of items that match the specified references\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TAdministrativeAreal2DReferencedObject>?> GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable<string>? references, System.Nullable<long> limit=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of strings representing the references of the objects to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional long integer specifying the maximum number of items to be returned\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_long_,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of \<seeref name="TAdministrativeAreal2DReferencedObject" /\> objects, or null if no matching items are found or the references collection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.UpdateAsync(System.Collections.Generic.IEnumerable_TAdministrativeAreal2DReferencedObject_,int)'></a>

## AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.UpdateAsync\(IEnumerable\<TAdministrativeAreal2DReferencedObject\>, int\) Method

Asynchronously updates the specified collection of administrative areal 2D referenced objects\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<int>?> UpdateAsync(System.Collections.Generic.IEnumerable<TAdministrativeAreal2DReferencedObject>? administrativeAreal2DReferencedObjects, int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.UpdateAsync(System.Collections.Generic.IEnumerable_TAdministrativeAreal2DReferencedObject_,int).administrativeAreal2DReferencedObjects'></a>

`administrativeAreal2DReferencedObjects` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[TAdministrativeAreal2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.TAdministrativeAreal2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>\.TAdministrativeAreal2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of \<seeref name="TAdministrativeAreal2DReferencedObject" /\> objects to be updated\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_.UpdateAsync(System.Collections.Generic.IEnumerable_TAdministrativeAreal2DReferencedObject_,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the batch\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of integers representing the IDs of the updated objects, or `null` if no objects were updated\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath'></a>

## AdministrativeAreal2DReferencePath Class

Represents a collection of administrative 2D area references indexed by their administrative area type\.

```csharp
public class AdministrativeAreal2DReferencePath : DiGi.Core.Classes.SerializableObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → AdministrativeAreal2DReferencePath

Implements [IGISPostgreSQLSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLSerializableObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath()'></a>

## AdministrativeAreal2DReferencePath\(\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencePath class\.

```csharp
public AdministrativeAreal2DReferencePath();
```

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath)'></a>

## AdministrativeAreal2DReferencePath\(AdministrativeAreal2DReferencePath\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencePath class by copying data from another AdministrativeAreal2DReferencePath instance\.

```csharp
public AdministrativeAreal2DReferencePath(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath).administrativeAreal2DReferencePath'></a>

`administrativeAreal2DReferencePath` [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')

The AdministrativeAreal2DReferencePath instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_)'></a>

## AdministrativeAreal2DReferencePath\(IEnumerable\<AdministrativeAreal2DReference\>\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencePath class from a collection of administrative 2D area references\.

```csharp
public AdministrativeAreal2DReferencePath(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference> administrativeAreal2DReferences);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_).administrativeAreal2DReferences'></a>

`administrativeAreal2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of administrative 2D area references to add\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath(System.Text.Json.Nodes.JsonObject)'></a>

## AdministrativeAreal2DReferencePath\(JsonObject\) Constructor

Initializes a new instance of the AdministrativeAreal2DReferencePath class from a JsonObject\.

```csharp
public AdministrativeAreal2DReferencePath(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferencePath(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.dictionary'></a>

## AdministrativeAreal2DReferencePath\.dictionary Field

Gets or sets the dictionary of administrative 2D area references indexed by type\.

```csharp
private readonly Dictionary<AdministrativeArealType,AdministrativeAreal2DReference> dictionary;
```

#### Field Value
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.AdministrativeAreal2DReferences'></a>

## AdministrativeAreal2DReferencePath\.AdministrativeAreal2DReferences Property

Gets or sets the list of administrative 2D area references, serialized in enum order\.

```csharp
public System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference> AdministrativeAreal2DReferences { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.this[DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType]'></a>

## AdministrativeAreal2DReferencePath\.this\[AdministrativeArealType\] Property

Gets the administrative 2D area reference for the specified administrative area type\.

```csharp
public DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference? this[DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType] { get; }
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.this[DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType].administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The administrative area type to look up\.

#### Property Value
[AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.Add(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference)'></a>

## AdministrativeAreal2DReferencePath\.Add\(AdministrativeAreal2DReference\) Method

Adds an administrative 2D area reference to the collection\.

```csharp
public bool Add(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference? administrativeAreal2DReference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.Add(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference).administrativeAreal2DReference'></a>

`administrativeAreal2DReference` [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')

The administrative 2D area reference to add\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the reference was added successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.Remove(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType)'></a>

## AdministrativeAreal2DReferencePath\.Remove\(AdministrativeArealType\) Method

Removes the administrative 2D area reference for the specified administrative area type\.

```csharp
public bool Remove(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType administrativeArealType);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath.Remove(DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType).administrativeArealType'></a>

`administrativeArealType` [AdministrativeArealType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType 'DiGi\.GIS\.PostgreSQL\.Enums\.AdministrativeArealType')

The administrative area type to remove\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the reference was removed successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_'></a>

## Areal2D\<TAreal2D\> Class

Abstract generic class for 2D area entities with bounding box and code information\.

```csharp
public abstract class Areal2D<TAreal2D> : DiGi.GIS.PostgreSQL.Classes.ReferencedObject<TAreal2D>
    where TAreal2D : DiGi.GIS.Classes.Areal2D
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.TAreal2D'></a>

`TAreal2D`

The type of the underlying GIS 2D area object\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[TAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.TAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>\.TAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[TAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.TAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>\.TAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → Areal2D\<TAreal2D\>

Derived  
↳ [AdministrativeAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2D')  
↳ [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.Areal2D()'></a>

## Areal2D\(\) Constructor

Initializes a new instance of the Areal2D class\.

```csharp
public Areal2D();
```

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.Areal2D(DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_)'></a>

## Areal2D\(Areal2D\<TAreal2D\>\) Constructor

Initializes a new instance of the Areal2D class by copying data from another Areal2D instance\.

```csharp
public Areal2D(DiGi.GIS.PostgreSQL.Classes.Areal2D<TAreal2D>? areal2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.Areal2D(DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_).areal2D'></a>

`areal2D` [DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>')[TAreal2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.TAreal2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>\.TAreal2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>')

The Areal2D instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.Areal2D(System.Text.Json.Nodes.JsonObject)'></a>

## Areal2D\(JsonObject\) Constructor

Initializes a new instance of the Areal2D class from a JsonObject\.

```csharp
public Areal2D(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.Areal2D(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.BoundingBox2D'></a>

## Areal2D\<TAreal2D\>\.BoundingBox2D Property

Gets or sets the bounding box of the 2D area\.

```csharp
public DiGi.Geometry.Planar.Classes.BoundingBox2D? BoundingBox2D { get; set; }
```

#### Property Value
[DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_.Code'></a>

## Areal2D\<TAreal2D\>\.Code Property

Gets or sets the code of the 2D area\.

```csharp
public string? Code { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference'></a>

## Areal2DReference Class

Abstract base class for 2D area references, providing serialization and common properties for area\-based data\.

```csharp
public abstract class Areal2DReference : DiGi.Core.Classes.SerializableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → Areal2DReference

Derived  
↳ [AdministrativeAreal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')  
↳ [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.Areal2DReference()'></a>

## Areal2DReference\(\) Constructor

Initializes a new instance of the Areal2DReference class\.

```csharp
public Areal2DReference();
```

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.Areal2DReference(DiGi.GIS.PostgreSQL.Classes.Areal2DReference)'></a>

## Areal2DReference\(Areal2DReference\) Constructor

Initializes a new instance of the Areal2DReference class by copying data from another Areal2DReference instance\.

```csharp
public Areal2DReference(DiGi.GIS.PostgreSQL.Classes.Areal2DReference? areal2DReference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.Areal2DReference(DiGi.GIS.PostgreSQL.Classes.Areal2DReference).areal2DReference'></a>

`areal2DReference` [Areal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2DReference')

The Areal2DReference instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.Areal2DReference(System.Text.Json.Nodes.JsonObject)'></a>

## Areal2DReference\(JsonObject\) Constructor

Initializes a new instance of the Areal2DReference class from a JsonObject\.

```csharp
public Areal2DReference(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.Areal2DReference(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.CountyId'></a>

## Areal2DReference\.CountyId Property

Gets or sets the county ID associated with this item\.

```csharp
public System.Nullable<int> CountyId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.Areal2DReference.Reference'></a>

## Areal2DReference\.Reference Property

Gets or sets the reference string associated with this item\.

```csharp
public string? Reference { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D'></a>

## Building2D Class

Represents a 2D building entity with county and subdivision identification\.

```csharp
public class Building2D : DiGi.GIS.PostgreSQL.Classes.Areal2D<DiGi.GIS.Classes.Building2D>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>') → Building2D
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.Building2D()'></a>

## Building2D\(\) Constructor

Initializes a new instance of the Building2D class\.

```csharp
public Building2D();
```

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.Building2D(DiGi.GIS.PostgreSQL.Classes.Building2D)'></a>

## Building2D\(Building2D\) Constructor

Initializes a new instance of the Building2D class by copying data from another Building2D instance\.

```csharp
public Building2D(DiGi.GIS.PostgreSQL.Classes.Building2D? building2D);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.Building2D(DiGi.GIS.PostgreSQL.Classes.Building2D).building2D'></a>

`building2D` [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')

The Building2D instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.Building2D(System.Text.Json.Nodes.JsonObject)'></a>

## Building2D\(JsonObject\) Constructor

Initializes a new instance of the Building2D class from a JsonObject\.

```csharp
public Building2D(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.Building2D(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.CountyId'></a>

## Building2D\.CountyId Property

Gets or sets the ID of the county associated with this building\.

```csharp
public System.Nullable<int> CountyId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.Id'></a>

## Building2D\.Id Property

Gets or sets the unique ID of the building\.

```csharp
public long Id { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2D.SubdivisionId'></a>

## Building2D\.SubdivisionId Property

Gets or sets the ID of the subdivision to which this building belongs\.

```csharp
public System.Nullable<int> SubdivisionId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData'></a>

## Building2DOccupancyData Class

Represents occupancy data associated with a 2D building\.

```csharp
public class Building2DOccupancyData : DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject<DiGi.GIS.Interfaces.IOccupancyData>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>') → Building2DOccupancyData
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData.Building2DOccupancyData()'></a>

## Building2DOccupancyData\(\) Constructor

Initializes a new instance of the Building2DOccupancyData class\.

```csharp
public Building2DOccupancyData();
```

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData.Building2DOccupancyData(DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData)'></a>

## Building2DOccupancyData\(Building2DOccupancyData\) Constructor

Initializes a new instance of the Building2DOccupancyData class by copying data from another Building2DOccupancyData instance\.

```csharp
public Building2DOccupancyData(DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData? building2DOccupancyData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData.Building2DOccupancyData(DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData).building2DOccupancyData'></a>

`building2DOccupancyData` [Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')

The Building2DOccupancyData instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData.Building2DOccupancyData(System.Text.Json.Nodes.JsonObject)'></a>

## Building2DOccupancyData\(JsonObject\) Constructor

Initializes a new instance of the Building2DOccupancyData class from a JsonObject\.

```csharp
public Building2DOccupancyData(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData.Building2DOccupancyData(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyDataPostgreSQLConverter'></a>

## Building2DOccupancyDataPostgreSQLConverter Class

Provides functionality to convert building 2D occupancy data for PostgreSQL database operations\.

```csharp
public class Building2DOccupancyDataPostgreSQLConverter : DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData, DiGi.GIS.Interfaces.IOccupancyData>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>')[Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IOccupancyData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.ioccupancydata 'DiGi\.GIS\.Interfaces\.IOccupancyData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>') → Building2DOccupancyDataPostgreSQLConverter
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyDataPostgreSQLConverter.Building2DOccupancyDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## Building2DOccupancyDataPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [Building2DOccupancyDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyDataPostgreSQLConverter') class\.

```csharp
public Building2DOccupancyDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyDataPostgreSQLConverter.Building2DOccupancyDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The connection data used to connect to the PostgreSQL database\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyDataPostgreSQLConverter.TableName'></a>

## Building2DOccupancyDataPostgreSQLConverter\.TableName Property

Gets the name of the table in the PostgreSQL database where building 2D occupancy data is stored\.

```csharp
public override string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter'></a>

## Building2DPostgreSQLConverter Class

Provides functionality for converting and managing [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') entities within a PostgreSQL database, implementing the [IGISPostgreSQLConverter&lt;TTableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>') interface\.

```csharp
public class Building2DPostgreSQLConverter : DiGi.PostgreSQL.Classes.PostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.Building2D>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.Building2D>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → Building2DPostgreSQLConverter

Implements [DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>'), [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.Building2DPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## Building2DPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter') class\.

```csharp
public Building2DPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.Building2DPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings for the PostgreSQL database, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.ClearAsync()'></a>

## Building2DPostgreSQLConverter\.ClearAsync\(\) Method

Asynchronously clears all data from the specified table and restarts its identity sequence\.

```csharp
public System.Threading.Tasks.Task<bool> ClearAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A [System\.Threading\.Tasks\.Task&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1') representing the asynchronous operation\. The result is a [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean') indicating true if the operation succeeded; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.CountAsync\(int, IEnumerable\<int\>, CancellationToken\) Method

Asynchronously counts the number of 2D buildings for a specified county, with optional filtering by subdivision identifiers\.

```csharp
public System.Threading.Tasks.Task<long> CountAsync(int countyId, System.Collections.Generic.IEnumerable<int>? subdivisionIds=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county \(Partition Key\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).subdivisionIds'></a>

`subdivisionIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of integers representing the subdivision identifiers to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a long integer, or \-1 if the connection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.CountAsync\(NpgsqlConnection, int, IEnumerable\<int\>, CancellationToken\) Method

Asynchronously counts the number of 2D buildings for a specified county, with optional filtering by subdivision identifiers\.

```csharp
public static System.Threading.Tasks.Task<long> CountAsync(Npgsql.NpgsqlConnection npgsqlConnection, int countyId, System.Collections.Generic.IEnumerable<int>? subdivisionIds=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county \(Partition Key\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).subdivisionIds'></a>

`subdivisionIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of integers representing the subdivision identifiers to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a long integer, or \-1 if the connection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.CountAsync\(IEnumerable\<int\>, CancellationToken\) Method

Asynchronously counts the number of 2D buildings for a specified administrative areal 2D identifiers\.

```csharp
public System.Threading.Tasks.Task<long> CountAsync(System.Collections.Generic.IEnumerable<int> administrativeAreal2DIds, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).administrativeAreal2DIds'></a>

`administrativeAreal2DIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the administrative areal 2D identifiers to filter by\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CountAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
Number of buidlings in the specified administrative areas\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CreateTableAsync(int)'></a>

## Building2DPostgreSQLConverter\.CreateTableAsync\(int\) Method

Asynchronously creates the table in the database\.

```csharp
public System.Threading.Tasks.Task<bool> CreateTableAsync(int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.CreateTableAsync(int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The time interval, in seconds, to wait for the command to complete before timing out\. The default value is 30 seconds\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a boolean value indicating whether the table was successfully created\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DByIdAsync\(long, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') instance by its unique identifier and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.Building2D?> GetBuilding2DByIdAsync(long id, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).id'></a>

`id` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The long unique identifier of the building to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to scope the search\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') instance if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,double)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DByPoint2DAsync\(Point2D, double\) Method

Asynchronously retrieves a [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') located at or near the specified 2D point within a given tolerance\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.Building2D?> GetBuilding2DByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D? point2D, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,double).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') coordinate to search for\. This value can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByPoint2DAsync(DiGi.Geometry.Planar.Classes.Point2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') distance tolerance used to determine if a building is associated with the given point\. Defaults to [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') found at the specified location, or null if no building is found within the tolerance or if the provided point is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DByReferenceAsync\(string, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') instance based on the specified reference string and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.Building2D?> GetBuilding2DByReferenceAsync(string reference, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') reference used to identify the building\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional nullable integer identifier for the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') instance if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferenceByIdAsync\(long, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a building 2D reference by its unique identifier and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.Building2DReference?> GetBuilding2DReferenceByIdAsync(long id, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).id'></a>

`id` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The long integer representing the unique identifier of the building\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer representing the county identifier used to filter the search\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') if a match is found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferenceByReferenceAsync\(string, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a building 2D reference based on the specified reference string and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.Building2DReference?> GetBuilding2DReferenceByReferenceAsync(string reference, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique reference string of the building to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the search\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferenceByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [Building2DReference](https://learn.microsoft.com/en-us/dotnet/api/building2dreference 'Building2DReference') if a match is found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferencesAsync\(IEnumerable\<Building2DReference\>\) Method

Retrieves full Building2DReference data from building\_2d table based on input references\.
Performs batch processing using UNNEST to avoid N\+1 query performance issues\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference> building2DReferences);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

Collection of partial references \(must have Reference, optional CountyId\)\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A list of populated Building2DReference objects found in the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync\(IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of building 2D references associated with the specified administrative areal 2D identifiers\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync(System.Collections.Generic.IEnumerable<int> administrativeAreal2DIds, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).administrativeAreal2DIds'></a>

`administrativeAreal2DIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the administrative areal 2D identifiers to filter by\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync(System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects, or null if no references are found or an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,string,int,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferencesByCountyIdAsync\(int, Nullable\<int\>, string, int, CancellationToken\) Method

Asynchronously retrieves a keyset\-paginated list of Building2DReference objects for a specified county\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(int countyId, System.Nullable<int> subdivisionId=null, string? lastReference=null, int pageSize=250, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,string,int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county \(Partition Key\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,string,int,System.Threading.CancellationToken).subdivisionId'></a>

`subdivisionId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the subdivision\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,string,int,System.Threading.CancellationToken).lastReference'></a>

`lastReference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The last reference string from the previous page, used as the pagination cursor\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,string,int,System.Threading.CancellationToken).pageSize'></a>

`pageSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The maximum number of references to return in a single page\. Defaults to 250\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,string,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects, or null if connection fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,System.Threading.CancellationToken,int)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferencesByCountyIdAsync\(int, Nullable\<int\>, IEnumerable\<string\>, CancellationToken, int\) Method

Retrieves all Building2DReferences for a specific county, with an optional exclusion list\.
Optimized for partitioned tables using the partition key \(county\_id\)\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(int countyId, System.Nullable<int> subdivisionId=null, System.Collections.Generic.IEnumerable<string>? excludedReferences=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,System.Threading.CancellationToken,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The ID of the county \(Partition Key\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,System.Threading.CancellationToken,int).subdivisionId'></a>

`subdivisionId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The ID of the subdivision\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,System.Threading.CancellationToken,int).excludedReferences'></a>

`excludedReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

Optional collection of references to be excluded from the result\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(int,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A list of Building2DReference objects, or null if connection fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DReferencesByCountyIdAsync\(NpgsqlConnection, int, IEnumerable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of 2D building references for a specified county, with optional filtering by subdivision identifiers\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetBuilding2DReferencesByCountyIdAsync(Npgsql.NpgsqlConnection npgsqlConnection, int countyId, System.Collections.Generic.IEnumerable<int>? subdivisionIds=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).subdivisionIds'></a>

`subdivisionIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of integers representing the subdivision identifiers to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DReferencesByCountyIdAsync(Npgsql.NpgsqlConnection,int,System.Collections.Generic.IEnumerable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects, or null if no references are found or an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DsByBoundingBox2DAsync\(BoundingBox2D, double, CancellationToken\) Method

Asynchronously retrieves a list of [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') objects located within the specified bounding box, applying a distance tolerance\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2D>?> GetBuilding2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double tolerance=0.001, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double,System.Threading.CancellationToken).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D') defining the spatial area to search for buildings; may be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double,System.Threading.CancellationToken).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The double value representing the distance tolerance used during the spatial query\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBoundingBox2DAsync(DiGi.Geometry.Planar.Classes.BoundingBox2D,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A [System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task') representing the asynchronous operation, containing a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of buildings found within the specified area\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,int)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DsByBuilding2DReferences\(IEnumerable\<Building2DReference\>, int\) Method

Retrieves full building data based on a collection of references using optimized UNNEST batching\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2D>?> GetBuilding2DsByBuilding2DReferences(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference> building2DReferences, int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,int).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects used to identify and retrieve the corresponding buildings from the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') objects matching the provided references, or null if the input collection was null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DsByCircle2DAsync\(Circle2D, double, CancellationToken\) Method

Asynchronously retrieves a list of [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') objects whose bounding box lies within or intersects the specified circular area \(the radius expanded by the tolerance\)\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2D>?> GetBuilding2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D? circle2D, double tolerance=0.001, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double,System.Threading.CancellationToken).circle2D'></a>

`circle2D` [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D')

The [DiGi\.Geometry\.Planar\.Classes\.Circle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.circle2d 'DiGi\.Geometry\.Planar\.Classes\.Circle2D') defining the search area; can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double,System.Threading.CancellationToken).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The double value representing the distance tolerance for the spatial query, defaulting to [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCircle2DAsync(DiGi.Geometry.Planar.Classes.Circle2D,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of buildings found within the specified area, an empty list if none match, or null if the input is invalid or the connection could not be established\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCountyIdAsync(int,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetBuilding2DsByCountyIdAsync\(int, CancellationToken\) Method

Asynchronously retrieves a list of [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') objects for a specified county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2D>?> GetBuilding2DsByCountyIdAsync(int countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCountyIdAsync(int,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county used to filter the search\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetBuilding2DsByCountyIdAsync(int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') objects if matches are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetCountAsync\(NpgsqlConnection, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves the total count of records, optionally filtered by a specific county identifier\.

```csharp
public static System.Threading.Tasks.Task<long> GetCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token that can be used by other methods as part of cooperating cancellation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetCountAsync\(Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves the count of records, optionally filtered by a specific county identifier\.

```csharp
public System.Threading.Tasks.Task<long> GetCountAsync(System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county to filter the results; if null, the total count is retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetCountAsync(System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total row count as a long\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetEstimatedCountAsync\(NpgsqlConnection, IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously gets the estimated row count across specified counties in a PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<int> countyIds, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') to use for the query\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the county identifiers\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean indicating whether to run an analysis before fetching the estimated count\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated total row count as a long, or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetEstimatedCountAsync\(NpgsqlConnection, Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated row count from the database, optionally filtered by a specific county identifier and with an optional statistics update\.

```csharp
public static System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Nullable<int> countyId, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county for which the count is estimated; if null, the estimate may be calculated across all counties\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to perform an ANALYZE operation on the table before retrieving the count to ensure statistics are current\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated row count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetEstimatedCountAsync\(IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves the estimated row count for the specified collection of county identifiers\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(System.Collections.Generic.IEnumerable<int> countyIds, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the IDs of the counties to be counted\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to run a database analysis before fetching the estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated total row count as a long, or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetEstimatedCountAsync\(Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated row count, optionally filtered by a specific county identifier\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(System.Nullable<int> countyId, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county to filter the estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to run an analysis operation before fetching the count to ensure higher accuracy\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated row count as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64'), or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetPoint2DsByReferences\(NpgsqlConnection, IEnumerable\<string\>, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects associated with the specified references and an optional county identifier\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.Point2D>?> GetPoint2DsByReferences(Npgsql.NpgsqlConnection npgsqlConnection, System.Collections.Generic.IEnumerable<string> references, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') containing the references to query\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') representing the county identifier for filtering results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects if matches are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.GetPoint2DsByReferences\(IEnumerable\<string\>, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects associated with the specified references and optional county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.Point2D>?> GetPoint2DsByReferences(System.Collections.Generic.IEnumerable<string> references, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') containing the reference identifiers for the points to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') representing the unique identifier of the county used to filter the search\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.GetPoint2DsByReferences(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects if matches are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken)'></a>

## Building2DPostgreSQLConverter\.RefreshAsync\(PostgreSQLBuilding2DRefreshOptions, IProgress\<long\>, CancellationToken\) Method

Asynchronously refreshes the 2D building data in the PostgreSQL database\.

```csharp
public System.Threading.Tasks.Task<bool> RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions? postgreSQLBuilding2DRefreshOptions=null, System.IProgress<long>? progress=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).postgreSQLBuilding2DRefreshOptions'></a>

`postgreSQLBuilding2DRefreshOptions` [PostgreSQLBuilding2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshOptions')

The options to configure the refresh process for PostgreSQL 2D buildings\. Can be null to use default settings\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

The progress reporter used to report the current progress as a long value\. Can be null if no progress reporting is required\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.RefreshAsync(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions,System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the refresh succeeded; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,double)'></a>

## Building2DPostgreSQLConverter\.UpdateAsync\(IEnumerable\<Building2D\>, double\) Method

Asynchronously updates the specified collection of 2D buildings using a defined distance tolerance\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<long>?> UpdateAsync(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2D>? building2Ds, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,double).building2Ds'></a>

`building2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The enumerable collection of [Building2D](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2D 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2D') objects to be updated; may be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2D_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The double precision value used as the distance tolerance for the update operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a nullable long indicating the outcome of the update, or null if the operation could not be completed\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference'></a>

## Building2DReference Class

Represents a reference to a 2D building within a subdivision\.

```csharp
public class Building2DReference : DiGi.GIS.PostgreSQL.Classes.Areal2DReference
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [Areal2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2DReference') → Building2DReference
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.Building2DReference()'></a>

## Building2DReference\(\) Constructor

Initializes a new instance of the Building2DReference class\.

```csharp
public Building2DReference();
```

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.Building2DReference(DiGi.GIS.PostgreSQL.Classes.Building2DReference)'></a>

## Building2DReference\(Building2DReference\) Constructor

Initializes a new instance of the Building2DReference class by copying data from another Building2DReference instance\.

```csharp
public Building2DReference(DiGi.GIS.PostgreSQL.Classes.Building2DReference? building2DReference);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.Building2DReference(DiGi.GIS.PostgreSQL.Classes.Building2DReference).building2DReference'></a>

`building2DReference` [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')

The Building2DReference instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.Building2DReference(System.Text.Json.Nodes.JsonObject)'></a>

## Building2DReference\(JsonObject\) Constructor

Initializes a new instance of the Building2DReference class from a JsonObject\.

```csharp
public Building2DReference(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.Building2DReference(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.Id'></a>

## Building2DReference\.Id Property

Gets or sets the unique ID of the building\.

```csharp
public long Id { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReference.SubdivisionId'></a>

## Building2DReference\.SubdivisionId Property

Gets or sets the ID of the subdivision to which this building belongs\.

```csharp
public System.Nullable<int> SubdivisionId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_'></a>

## Building2DReferencedObject\<TUniqueObject\> Class

Abstract base class for referenced objects associated with 2D buildings, providing county and identification properties\.

```csharp
public abstract class Building2DReferencedObject<TUniqueObject> : DiGi.GIS.PostgreSQL.Classes.ReferencedObject<TUniqueObject>
    where TUniqueObject : DiGi.Core.Interfaces.IUniqueObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.TUniqueObject'></a>

`TUniqueObject`

The type of the unique object this referenced object points to\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → Building2DReferencedObject\<TUniqueObject\>

Derived  
↳ [Building2DOccupancyData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyData 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyData')  
↳ [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')  
↳ [YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.Building2DReferencedObject()'></a>

## Building2DReferencedObject\(\) Constructor

Initializes a new instance of the Building2DReferencedObject class\.

```csharp
public Building2DReferencedObject();
```

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.Building2DReferencedObject(DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_)'></a>

## Building2DReferencedObject\(Building2DReferencedObject\<TUniqueObject\>\) Constructor

Initializes a new instance of the Building2DReferencedObject class by copying data from another Building2DReferencedObject instance\.

```csharp
public Building2DReferencedObject(DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject<TUniqueObject>? building2DReferencedObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.Building2DReferencedObject(DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_).building2DReferencedObject'></a>

`building2DReferencedObject` [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>')

The Building2DReferencedObject instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.Building2DReferencedObject(System.Text.Json.Nodes.JsonObject)'></a>

## Building2DReferencedObject\(JsonObject\) Constructor

Initializes a new instance of the Building2DReferencedObject class from a JsonObject\.

```csharp
public Building2DReferencedObject(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.Building2DReferencedObject(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.CountyId'></a>

## Building2DReferencedObject\<TUniqueObject\>\.CountyId Property

Gets or sets the county ID associated with this referenced object\.

```csharp
public System.Nullable<int> CountyId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_.Id'></a>

## Building2DReferencedObject\<TUniqueObject\>\.Id Property

Gets or sets the unique ID of this referenced object\.

```csharp
public long Id { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\> Class

Provides a base implementation for a PostgreSQL converter specifically designed for building 2D referenced objects\.

```csharp
public abstract class Building2DReferencedObjectPostgreSQLConverter<TBuilding2DReferencedObject,TUniqueObject> : DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter<TBuilding2DReferencedObject, TUniqueObject>
    where TBuilding2DReferencedObject : DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject<TUniqueObject>
    where TUniqueObject : DiGi.Core.Interfaces.IUniqueObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject'></a>

`TBuilding2DReferencedObject`

The type of the building 2D referenced object\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TUniqueObject'></a>

`TUniqueObject`

The type of the unique object used for identification, which must implement the [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject') interface\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') → Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>

Derived  
↳ [Building2DOccupancyDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DOccupancyDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DOccupancyDataPostgreSQLConverter')  
↳ [YearBuiltDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltDataPostgreSQLConverter')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Building2DReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## Building2DReferencedObjectPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [Building2DReferencedObjectPostgreSQLConverter&lt;TBuilding2DReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>') class\.

```csharp
public Building2DReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Building2DReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings required to establish a connection to the PostgreSQL database\. This value can be null\.
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.Create\(long, Nullable\<int\>, string, string, JsonObject, Nullable\<DateTime\>\) Method

Creates a new instance of a building referenced object using the specified identification and metadata\.

```csharp
protected abstract TBuilding2DReferencedObject Create(long id, System.Nullable<int> countyId, string? uniqueId, string? reference, System.Text.Json.Nodes.JsonObject? @object, System.Nullable<System.DateTime> createdAt);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).id'></a>

`id` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The unique [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64') identifier for the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') identifier for the county associated with the building\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).uniqueId'></a>

`uniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') representing a unique identification code\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') used as a reference for the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).object'></a>

`object` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The optional [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing raw data for initialization\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).createdAt'></a>

`createdAt` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime') indicating when the record was created\.

#### Returns
[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')  
A new instance of \<seeref name="TBuilding2DReferencedObject" /\>\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(Npgsql.NpgsqlDataReader)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.Create\(NpgsqlDataReader\) Method

Creates an instance of \<seeref name="TBuilding2DReferencedObject" /\> using the data provided by the [Npgsql\.NpgsqlDataReader](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqldatareader 'Npgsql\.NpgsqlDataReader')\.

```csharp
protected override TBuilding2DReferencedObject Create(Npgsql.NpgsqlDataReader npgsqlDataReader);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.Create(Npgsql.NpgsqlDataReader).npgsqlDataReader'></a>

`npgsqlDataReader` [Npgsql\.NpgsqlDataReader](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqldatareader 'Npgsql\.NpgsqlDataReader')

The [Npgsql\.NpgsqlDataReader](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqldatareader 'Npgsql\.NpgsqlDataReader') containing the database record to be used for object creation\.

#### Returns
[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')  
A new instance of \<seeref name="TBuilding2DReferencedObject" /\> populated with data from the reader\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetCountAsync\(NpgsqlConnection, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves the total count of records, optionally filtered by a specific county identifier\.

```csharp
public System.Threading.Tasks.Task<long> GetCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The PostgreSQL connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional unique identifier of the county to filter the count; if null, the total count across all counties is retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetEstimatedCountAsync\(NpgsqlConnection, IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously gets the estimated row count for the specified county identifiers in the PostgreSQL database\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<int> countyIds, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') to use for the query\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the county identifiers to estimate counts for\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean indicating whether to run a vacuum analyze operation before fetching the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated number of rows as a long, or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetEstimatedCountAsync\(NpgsqlConnection, Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated count of records, optionally filtered by a specific county identifier and with the option to update table statistics before estimation\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Nullable<int> countyId, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether an ANALYZE operation should be performed on the table to update statistics for a more accurate estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated record count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetEstimatedCountAsync\(IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves the estimated row count for the specified collection of county identifiers\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(System.Collections.Generic.IEnumerable<int> countyIds, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the unique identifiers of the counties to be counted\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to perform a database analysis operation before retrieving the estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated total row count as a long, or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetEstimatedCountAsync\(Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated row count, optionally filtered by a specific county identifier\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(System.Nullable<int> countyId, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county to filter the estimate\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to run an analysis operation before fetching the count to ensure higher accuracy\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated row count as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64'), or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemByIdAsync\(long, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a building 2D referenced object by its unique identifier and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<TBuilding2DReferencedObject?> GetItemByIdAsync(long id, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).id'></a>

`id` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The long integer unique identifier of the item to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county associated with the item\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByIdAsync(long,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the retrieved \<seeref name="TBuilding2DReferencedObject" /\>, or null if no item with the specified identifier was found\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemByReferenceAsync\(string, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a \<seeref name="TBuilding2DReferencedObject" /\> using the specified reference and optional county identifier\.

```csharp
public System.Threading.Tasks.Task<TBuilding2DReferencedObject?> GetItemByReferenceAsync(string reference, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string reference of the item to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier for the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the matching \<seeref name="TBuilding2DReferencedObject" /\> if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemsByIdsAsync\(NpgsqlConnection, IEnumerable\<long\>, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of items of type [TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject') based on the specified identifiers and county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TBuilding2DReferencedObject>?> GetItemsByIdsAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<long>? ids, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64') identifiers for the items to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of matching items, or null if the connection or identifiers are null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemsByIdsAsync\(IEnumerable\<long\>, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of building 2D referenced objects based on the specified identifiers and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TBuilding2DReferencedObject>?> GetItemsByIdsAsync(System.Collections.Generic.IEnumerable<long>? ids, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).ids'></a>

`ids` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of long identifiers of the items to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional nullable integer identifier of the county used to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByIdsAsync(System.Collections.Generic.IEnumerable_long_,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of matching items, or null if no items are found or the provided identifiers are null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemsByReferenceAsync\(string, Nullable\<int\>, Nullable\<long\>, CancellationToken\) Method

Asynchronously retrieves a list of building 2D referenced objects based on the specified reference and optional filters\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TBuilding2DReferencedObject>?> GetItemsByReferenceAsync(string reference, System.Nullable<int> countyId, System.Nullable<long> limit=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string reference used to identify the items\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional maximum number of items to retrieve, specified as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferenceAsync(string,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject') objects if matching items are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemsByReferencesAsync\(NpgsqlConnection, IEnumerable\<string\>, Nullable\<int\>, Nullable\<long\>, CancellationToken\) Method

Asynchronously retrieves a list of items that implement [TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject') based on the provided references and optional filters\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TBuilding2DReferencedObject>?> GetItemsByReferencesAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<string>? references, System.Nullable<int> countyId, System.Nullable<long> limit=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') representing the references of the items to be retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional maximum number of items to retrieve as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of matching items, or null if the connection or references are null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.GetItemsByReferencesAsync\(IEnumerable\<string\>, Nullable\<int\>, Nullable\<long\>, CancellationToken\) Method

Asynchronously retrieves a list of items based on the provided references and county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<TBuilding2DReferencedObject>?> GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable<string>? references, System.Nullable<int> countyId, System.Nullable<long> limit=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of strings representing the unique references of the items to be retrieved\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer specifying the county identifier to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).limit'></a>

`limit` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional long value that specifies the maximum number of items to return\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.GetItemsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Nullable_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject') objects if matches are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.UpdateAsync(System.Collections.Generic.IEnumerable_TBuilding2DReferencedObject_,int)'></a>

## Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.UpdateAsync\(IEnumerable\<TBuilding2DReferencedObject\>, int\) Method

Asynchronously updates the specified collection of building 2D referenced objects\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<long>?> UpdateAsync(System.Collections.Generic.IEnumerable<TBuilding2DReferencedObject>? building2DReferencedObjects, int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.UpdateAsync(System.Collections.Generic.IEnumerable_TBuilding2DReferencedObject_,int).building2DReferencedObjects'></a>

`building2DReferencedObjects` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[TBuilding2DReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.TBuilding2DReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>\.TBuilding2DReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') containing the referenced objects to be updated, or `null`\.

<a name='DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_.UpdateAsync(System.Collections.Generic.IEnumerable_TBuilding2DReferencedObject_,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64') identifiers for the objects that were updated, or `null` if no updates occurred\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter'></a>

## BuildingDataPostgreSQLConverter Class

Provides a PostgreSQL converter for building data, facilitating the translation between building table representations
using [DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column') and their corresponding PostgreSQL database structures, while implementing GIS\-specific conversion functionality\.

```csharp
public class BuildingDataPostgreSQLConverter : DiGi.PostgreSQL.Table.Classes.TablePostgreSQLConverter<DiGi.Core.IO.Table.Classes.Column>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table-1 'DiGi\.Core\.IO\.Table\.Classes\.Table\`1')[DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table-1 'DiGi\.Core\.IO\.Table\.Classes\.Table\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.PostgreSQL\.Table\.Classes\.TablePostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.tablepostgresqlconverter-1 'DiGi\.PostgreSQL\.Table\.Classes\.TablePostgreSQLConverter\`1')[DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.tablepostgresqlconverter-1 'DiGi\.PostgreSQL\.Table\.Classes\.TablePostgreSQLConverter\`1') → BuildingDataPostgreSQLConverter

Implements [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.BuildingDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## BuildingDataPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [BuildingDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter') class\.

```csharp
public BuildingDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.BuildingDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings required to establish a connection to the PostgreSQL database\. This value can be [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.TableConversionOptions'></a>

## BuildingDataPostgreSQLConverter\.TableConversionOptions Property

Gets the table conversion options specifically configured for [DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column')\.

```csharp
protected override DiGi.PostgreSQL.Table.Classes.TableConversionOptions<DiGi.Core.IO.Table.Classes.Column>? TableConversionOptions { protected get; }
```

#### Property Value
[DiGi\.PostgreSQL\.Table\.Classes\.TableConversionOptions&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.tableconversionoptions-1 'DiGi\.PostgreSQL\.Table\.Classes\.TableConversionOptions\`1')[DiGi\.Core\.IO\.Table\.Classes\.Column](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.column 'DiGi\.Core\.IO\.Table\.Classes\.Column')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.tableconversionoptions-1 'DiGi\.PostgreSQL\.Table\.Classes\.TableConversionOptions\`1')  
A [DiGi\.PostgreSQL\.Table\.Classes\.TableConversionOptions&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.tableconversionoptions-1 'DiGi\.PostgreSQL\.Table\.Classes\.TableConversionOptions\`1') instance containing the configuration settings for the table conversion, or `null`\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.TableName'></a>

## BuildingDataPostgreSQLConverter\.TableName Property

Gets the name of the database table associated with building data\.

```csharp
public override string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction,int,string,DiGi.PostgreSQL.Table.Classes.FilterGroup)'></a>

## BuildingDataPostgreSQLConverter\.GetAggregateSummaryAsync\(string, MultivalueAggregateFunction, int, string, FilterGroup\) Method

Asynchronously computes multi\-value aggregate statistics on a specific building data column inside a county partition, applying optional dynamic filters\.

```csharp
public System.Threading.Tasks.Task<System.Text.Json.Nodes.JsonNode?> GetAggregateSummaryAsync(string columnUniqueId, DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction multivalueAggregateFunction, int countyId, string? separator=null, DiGi.PostgreSQL.Table.Classes.FilterGroup? filterGroup=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction,int,string,DiGi.PostgreSQL.Table.Classes.FilterGroup).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the column to aggregate\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction,int,string,DiGi.PostgreSQL.Table.Classes.FilterGroup).multivalueAggregateFunction'></a>

`multivalueAggregateFunction` [DiGi\.PostgreSQL\.Table\.Enums\.MultivalueAggregateFunction](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.enums.multivalueaggregatefunction 'DiGi\.PostgreSQL\.Table\.Enums\.MultivalueAggregateFunction')

The multi\-value aggregate calculation function\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction,int,string,DiGi.PostgreSQL.Table.Classes.FilterGroup).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The partition county identifier\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction,int,string,DiGi.PostgreSQL.Table.Classes.FilterGroup).separator'></a>

`separator` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional custom string delimiter; if null, it is automatically detected\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.MultivalueAggregateFunction,int,string,DiGi.PostgreSQL.Table.Classes.FilterGroup).filterGroup'></a>

`filterGroup` [DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.filtergroup 'DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup')

The optional dynamic hierarchical filters to apply prior to aggregation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Text\.Json\.Nodes\.JsonNode](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonnode 'System\.Text\.Json\.Nodes\.JsonNode')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the async operation, returning the aggregate result as a [System\.Text\.Json\.Nodes\.JsonNode](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonnode 'System\.Text\.Json\.Nodes\.JsonNode')\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction,int,DiGi.PostgreSQL.Table.Classes.FilterGroup)'></a>

## BuildingDataPostgreSQLConverter\.GetAggregateSummaryAsync\(string, SinglevalueAggregateFunction, int, FilterGroup\) Method

Asynchronously computes single\-value aggregate statistics on a specific building data column inside a county partition, applying optional dynamic filters\.

```csharp
public System.Threading.Tasks.Task<System.Text.Json.Nodes.JsonNode?> GetAggregateSummaryAsync(string columnUniqueId, DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction singlevalueAggregateFunction, int countyId, DiGi.PostgreSQL.Table.Classes.FilterGroup? filterGroup=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the column to aggregate\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).singlevalueAggregateFunction'></a>

`singlevalueAggregateFunction` [DiGi\.PostgreSQL\.Table\.Enums\.SinglevalueAggregateFunction](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.enums.singlevalueaggregatefunction 'DiGi\.PostgreSQL\.Table\.Enums\.SinglevalueAggregateFunction')

The single\-value aggregate calculation function\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The partition county identifier\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetAggregateSummaryAsync(string,DiGi.PostgreSQL.Table.Enums.SinglevalueAggregateFunction,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).filterGroup'></a>

`filterGroup` [DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.filtergroup 'DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup')

The optional dynamic hierarchical filters to apply prior to aggregation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Text\.Json\.Nodes\.JsonNode](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonnode 'System\.Text\.Json\.Nodes\.JsonNode')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the async operation, returning the aggregate result as a [System\.Text\.Json\.Nodes\.JsonNode](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonnode 'System\.Text\.Json\.Nodes\.JsonNode')\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetHistogramSummaryAsync(string,int,int,DiGi.PostgreSQL.Table.Classes.FilterGroup)'></a>

## BuildingDataPostgreSQLConverter\.GetHistogramSummaryAsync\(string, int, int, FilterGroup\) Method

Asynchronously generates a value distribution histogram for a specific building data column inside a county partition, applying optional dynamic filters\.

```csharp
public System.Threading.Tasks.Task<System.Text.Json.Nodes.JsonArray?> GetHistogramSummaryAsync(string columnUniqueId, int bucketCount, int countyId, DiGi.PostgreSQL.Table.Classes.FilterGroup? filterGroup=null);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetHistogramSummaryAsync(string,int,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the column to aggregate\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetHistogramSummaryAsync(string,int,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).bucketCount'></a>

`bucketCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The total number of buckets to segment the value range into\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetHistogramSummaryAsync(string,int,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The partition county identifier\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetHistogramSummaryAsync(string,int,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).filterGroup'></a>

`filterGroup` [DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.filtergroup 'DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup')

The optional dynamic hierarchical filters to apply prior to generating the histogram\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Text\.Json\.Nodes\.JsonArray](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonarray 'System\.Text\.Json\.Nodes\.JsonArray')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the async operation, returning the histogram aggregate result as a [System\.Text\.Json\.Nodes\.JsonArray](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonarray 'System\.Text\.Json\.Nodes\.JsonArray')\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup)'></a>

## BuildingDataPostgreSQLConverter\.GetUniqueValuesAsync\<T\>\(NpgsqlConnection, string, int, FilterGroup\) Method

Asynchronously retrieves a collection of unique values for a specified identifier and county from the database, applying optional dynamic filters\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<T?>?> GetUniqueValuesAsync<T>(Npgsql.NpgsqlConnection? npgsqlConnection, string? uniqueId, int countyId, DiGi.PostgreSQL.Table.Classes.FilterGroup? filterGroup=null);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).T'></a>

`T`

The type of the unique values to be retrieved\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The Npgsql connection instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).uniqueId'></a>

`uniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string identifier used to filter for unique values\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).filterGroup'></a>

`filterGroup` [DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.filtergroup 'DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup')

The optional dynamic hierarchical filters to apply prior to retrieving the unique values\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[T](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(Npgsql.NpgsqlConnection,string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).T 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter\.GetUniqueValuesAsync\<T\>\(Npgsql\.NpgsqlConnection, string, int, DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup\)\.T')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains an enumerable collection of nullable values of type T, or null if no results are found or the connection is invalid\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup)'></a>

## BuildingDataPostgreSQLConverter\.GetUniqueValuesAsync\<T\>\(string, int, FilterGroup\) Method

Asynchronously retrieves a collection of unique values based on the specified unique identifier and county identifier, applying optional dynamic filters\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<T?>?> GetUniqueValuesAsync<T>(string? uniqueId, int countyId, DiGi.PostgreSQL.Table.Classes.FilterGroup? filterGroup=null);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).T'></a>

`T`

The type of the values to be retrieved\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).uniqueId'></a>

`uniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The nullable string representing the unique identifier used for filtering\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).filterGroup'></a>

`filterGroup` [DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.filtergroup 'DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup')

The optional dynamic hierarchical filters to apply prior to retrieving the unique values\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[T](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.GetUniqueValuesAsync_T_(string,int,DiGi.PostgreSQL.Table.Classes.FilterGroup).T 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter\.GetUniqueValuesAsync\<T\>\(string, int, DiGi\.PostgreSQL\.Table\.Classes\.FilterGroup\)\.T')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a nullable collection of nullable elements of type \<typeparam ref="T" /\>, or null if no values are found\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(int,System.Collections.Generic.IEnumerable_string_,string,int)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\(int, IEnumerable\<string\>, string, int\) Method

Asynchronously pulls a keyset\-paginated chunk of building data from a partition county\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync(int countyId, System.Collections.Generic.IEnumerable<string>? columnUniqueIds, string? lastReference, int pageSize=250);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(int,System.Collections.Generic.IEnumerable_string_,string,int).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The partition key identifying the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(int,System.Collections.Generic.IEnumerable_string_,string,int).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The optional list of column unique identifiers to project\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(int,System.Collections.Generic.IEnumerable_string_,string,int).lastReference'></a>

`lastReference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The last reference string from the previous page used as the cursor seek\-key\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(int,System.Collections.Generic.IEnumerable_string_,string,int).pageSize'></a>

`pageSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The page size count limit\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the async operation, returning the populated [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') if successful; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\(NpgsqlConnection, IEnumerable\<string\>, Nullable\<int\>, IEnumerable\<string\>, int\) Method

Asynchronously retrieves a table based on the specified references, county identifier, and optional column filters\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<string> references, System.Nullable<int> countyId, System.Collections.Generic.IEnumerable<string>? columnUniqueIds=null, int batchSize=1000);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to communicate with the PostgreSQL database; may be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') values representing the references to be pulled\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer identifying the specific county\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') unique identifiers for columns to include in the operation\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).batchSize'></a>

`batchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer number of records to process per batch\. Defaults to 1000\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [DiGi\.PostgreSQL\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.table.classes.table 'DiGi\.PostgreSQL\.Table\.Classes\.Table') object if the data is successfully retrieved; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\(IEnumerable\<string\>, Nullable\<int\>, IEnumerable\<string\>, int\) Method

Asynchronously retrieves a table based on the specified references, county identifier, and optional column filters\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync(System.Collections.Generic.IEnumerable<string> references, System.Nullable<int> countyId, System.Collections.Generic.IEnumerable<string>? columnUniqueIds=null, int batchSize=1000);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') containing the references to pull\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') representing the unique identifier of the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String') specifying the unique identifiers of the columns to retrieve\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Collections.Generic.IEnumerable_string_,int).batchSize'></a>

`batchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') specifying the number of records to process per batch\. Defaults to 1000\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A [System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task') representing the asynchronous operation, containing a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') object if successful; otherwise, [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,object,System.Collections.Generic.IEnumerable_string_)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(NpgsqlConnection, string, object, IEnumerable\<string\>\) Method

Asynchronously retrieves a table record from the database based on a specified unique identifier and value\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync<TObject>(Npgsql.NpgsqlConnection? npgsqlConnection, string columnUniqueId, object? value, System.Collections.Generic.IEnumerable<string>? columnUniqueIds=null);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,object,System.Collections.Generic.IEnumerable_string_).TObject'></a>

`TObject`

The type of object associated with the data retrieval operation\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,object,System.Collections.Generic.IEnumerable_string_).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to communicate with the PostgreSQL database\. Can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,object,System.Collections.Generic.IEnumerable_string_).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the column used as the unique identifier for filtering the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,object,System.Collections.Generic.IEnumerable_string_).value'></a>

`value` [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

The value to match against the specified unique identifier column\. Can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,object,System.Collections.Generic.IEnumerable_string_).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of strings representing additional column identifiers to be processed\. Can be null\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') object if a matching record is found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(NpgsqlConnection, string, IEnumerable\<TObject\>, IEnumerable\<string\>\) Method

Asynchronously retrieves data from the database based on specified column identifiers and values\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync<TObject>(Npgsql.NpgsqlConnection? npgsqlConnection, string columnUniqueId, System.Collections.Generic.IEnumerable<TObject>? values, System.Collections.Generic.IEnumerable<string>? columnUniqueIds=null);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).TObject'></a>

`TObject`

The type of the values used for filtering the table data\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance to be used for the database operation\. Can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the column used as the primary filter\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).values'></a>

`values` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[TObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).TObject 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(Npgsql\.NpgsqlConnection, string, System\.Collections\.Generic\.IEnumerable\<TObject\>, System\.Collections\.Generic\.IEnumerable\<string\>\)\.TObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of values of type \<typeparam ref="TObject" /\> to retrieve from the table\. Can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(Npgsql.NpgsqlConnection,string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of additional column unique identifiers to include in the retrieval process\. Defaults to null\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') object if data was successfully retrieved; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,object,System.Collections.Generic.IEnumerable_string_)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(string, object, IEnumerable\<string\>\) Method

Asynchronously retrieves a table record based on a specified column unique identifier and value\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync<TObject>(string columnUniqueId, object? value, System.Collections.Generic.IEnumerable<string>? columnUniqueIds=null);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,object,System.Collections.Generic.IEnumerable_string_).TObject'></a>

`TObject`

The type of the object used for the retrieval operation\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,object,System.Collections.Generic.IEnumerable_string_).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the column to be used as the primary filter\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,object,System.Collections.Generic.IEnumerable_string_).value'></a>

`value` [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

The value to search for in the specified column; can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,object,System.Collections.Generic.IEnumerable_string_).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of string unique identifiers for additional columns to be retrieved\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') instance if a matching record is found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_)'></a>

## BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(string, IEnumerable\<TObject\>, IEnumerable\<string\>\) Method

Asynchronously pulls data from a table based on the specified column unique identifiers and values\.

```csharp
public System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> PullAsync<TObject>(string columnUniqueId, System.Collections.Generic.IEnumerable<TObject>? values, System.Collections.Generic.IEnumerable<string>? columnUniqueIds=null);
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).TObject'></a>

`TObject`

The type of the objects contained in the values collection\.
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).columnUniqueId'></a>

`columnUniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the primary column used for the pull operation\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).values'></a>

`values` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[TObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).TObject 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(string, System\.Collections\.Generic\.IEnumerable\<TObject\>, System\.Collections\.Generic\.IEnumerable\<string\>\)\.TObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An enumerable collection of [TObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).TObject 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter\.PullAsync\<TObject\>\(string, System\.Collections\.Generic\.IEnumerable\<TObject\>, System\.Collections\.Generic\.IEnumerable\<string\>\)\.TObject') values to be used as criteria\. Can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter.PullAsync_TObject_(string,System.Collections.Generic.IEnumerable_TObject_,System.Collections.Generic.IEnumerable_string_).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional enumerable collection of additional unique identifiers for columns\. Defaults to null\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') instance if data is successfully retrieved; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter'></a>

## EPWFilePostgreSQLConverter Class

Provides functionality for converting and managing [DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile') entities within a PostgreSQL database\.

```csharp
public class EPWFilePostgreSQLConverter : DiGi.PostgreSQL.Classes.PostgreSQLConverter<DiGi.EPW.Classes.EPWFile>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → EPWFilePostgreSQLConverter

Implements [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.EPWFilePostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## EPWFilePostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [EPWFilePostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.EPWFilePostgreSQLConverter') class\.

```csharp
public EPWFilePostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.EPWFilePostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings required to establish a connection to the PostgreSQL database\. This value can be null\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.TableName'></a>

## EPWFilePostgreSQLConverter\.TableName Property

Gets the name of the database table associated with EPW files\.

```csharp
public static string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.ClearAsync\(NpgsqlConnection, CancellationToken\) Method

Asynchronously clears all records from the epw\_file table\.

```csharp
public static System.Threading.Tasks.Task<bool> ClearAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the operation succeeded; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.ClearAsync(System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.ClearAsync\(CancellationToken\) Method

Asynchronously clears all records from the epw\_file table, automatically managing the connection\.

```csharp
public System.Threading.Tasks.Task<bool> ClearAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.ClearAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the operation succeeded; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.GetEPWFileAsync\(Point2D, CancellationToken\) Method

Asynchronously retrieves the closest EPWFile to the given Point2D, automatically managing the connection\.

```csharp
public System.Threading.Tasks.Task<DiGi.EPW.Classes.EPWFile?> GetEPWFileAsync(DiGi.Geometry.Planar.Classes.Point2D point2D, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The coordinate point\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the closest [DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile'), or null if not found\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(double,double,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.GetEPWFileAsync\(double, double, CancellationToken\) Method

Asynchronously retrieves the closest EPWFile to the given coordinate \(x, y\), automatically managing the connection\.

```csharp
public System.Threading.Tasks.Task<DiGi.EPW.Classes.EPWFile?> GetEPWFileAsync(double x, double y, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(double,double,System.Threading.CancellationToken).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate \(EPSG:2180 easting, in metres\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(double,double,System.Threading.CancellationToken).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate \(EPSG:2180 northing, in metres\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(double,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the closest [DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile'), or null if not found\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.GetEPWFileAsync\(NpgsqlConnection, Point2D, CancellationToken\) Method

Asynchronously retrieves the closest EPWFile to the given Point2D\.

```csharp
public static System.Threading.Tasks.Task<DiGi.EPW.Classes.EPWFile?> GetEPWFileAsync(Npgsql.NpgsqlConnection? npgsqlConnection, DiGi.Geometry.Planar.Classes.Point2D point2D, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The coordinate point\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,DiGi.Geometry.Planar.Classes.Point2D,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the closest [DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile'), or null if not found\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,double,double,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.GetEPWFileAsync\(NpgsqlConnection, double, double, CancellationToken\) Method

Asynchronously retrieves the closest EPWFile to the given coordinate \(x, y\)\.

```csharp
public static System.Threading.Tasks.Task<DiGi.EPW.Classes.EPWFile?> GetEPWFileAsync(Npgsql.NpgsqlConnection? npgsqlConnection, double x, double y, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,double,double,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,double,double,System.Threading.CancellationToken).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate \(EPSG:2180 easting, in metres\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,double,double,System.Threading.CancellationToken).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate \(EPSG:2180 northing, in metres\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.GetEPWFileAsync(Npgsql.NpgsqlConnection,double,double,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the closest [DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile'), or null if not found\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.InsertAsync\(NpgsqlConnection, IEnumerable\<EPWFile\>, CancellationToken\) Method

Asynchronously inserts or updates a collection of EPWFile objects in the database\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<int>> InsertAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<DiGi.EPW.Classes.EPWFile> ePWFiles, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken).ePWFiles'></a>

`ePWFiles` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of EPW files to insert or update\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of database identifiers for the inserted or updated records\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken)'></a>

## EPWFilePostgreSQLConverter\.InsertAsync\(IEnumerable\<EPWFile\>, CancellationToken\) Method

Asynchronously inserts or updates a collection of EPWFile objects in the database, automatically managing the connection\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<int>> InsertAsync(System.Collections.Generic.IEnumerable<DiGi.EPW.Classes.EPWFile> ePWFiles, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken).ePWFiles'></a>

`ePWFiles` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.EPW\.Classes\.EPWFile](https://learn.microsoft.com/en-us/dotnet/api/digi.epw.classes.epwfile 'DiGi\.EPW\.Classes\.EPWFile')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of EPW files to insert or update\.

<a name='DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter.InsertAsync(System.Collections.Generic.IEnumerable_DiGi.EPW.Classes.EPWFile_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of database identifiers for the inserted or updated records\.

<a name='DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager'></a>

## GISPostgreSQLConverterManager Class

Manages the conversion processes specifically for GIS data within a PostgreSQL database context\.

```csharp
public class GISPostgreSQLConverterManager : DiGi.PostgreSQL.Classes.PostgreSQLConverterManager<DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverterManager&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconvertermanager-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverterManager\`1')[IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconvertermanager-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverterManager\`1') → GISPostgreSQLConverterManager

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas'></a>

## OrtoDatas Class

Represents orthophoto data associated with a 2D building\.

```csharp
public class OrtoDatas : DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject<DiGi.GIS.Classes.OrtoDatas>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>') → OrtoDatas
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.OrtoDatas()'></a>

## OrtoDatas\(\) Constructor

Initializes a new instance of the OrtoDatas class\.

```csharp
public OrtoDatas();
```

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.OrtoDatas(DiGi.GIS.PostgreSQL.Classes.OrtoDatas)'></a>

## OrtoDatas\(OrtoDatas\) Constructor

Initializes a new instance of the OrtoDatas class by copying data from another OrtoDatas instance\.

```csharp
public OrtoDatas(DiGi.GIS.PostgreSQL.Classes.OrtoDatas? ortoDatas);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.OrtoDatas(DiGi.GIS.PostgreSQL.Classes.OrtoDatas).ortoDatas'></a>

`ortoDatas` [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')

The OrtoDatas instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.OrtoDatas(System.Text.Json.Nodes.JsonObject)'></a>

## OrtoDatas\(JsonObject\) Constructor

Initializes a new instance of the OrtoDatas class from a JsonObject\.

```csharp
public OrtoDatas(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.OrtoDatas(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.BoundingBox2D'></a>

## OrtoDatas\.BoundingBox2D Property

Gets or sets the bounding box of the orthophoto data\.

```csharp
public DiGi.Geometry.Planar.Classes.BoundingBox2D? BoundingBox2D { get; set; }
```

#### Property Value
[DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatas.SubdivisionId'></a>

## OrtoDatas\.SubdivisionId Property

Gets or sets the ID of the subdivision to which this orthophoto data belongs\.

```csharp
public System.Nullable<int> SubdivisionId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter'></a>

## OrtoDatasPostgreSQLConverter Class

Provides a specialized implementation for converting [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') objects to and from a PostgreSQL database format, incorporating GIS\-specific conversion capabilities\.

```csharp
public class OrtoDatasPostgreSQLConverter : DiGi.PostgreSQL.Classes.PostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.OrtoDatas>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.OrtoDatas>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → OrtoDatasPostgreSQLConverter

Implements [DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>')[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')[&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>'), [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.OrtoDatasPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## OrtoDatasPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [OrtoDatasPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter') class\.

```csharp
public OrtoDatasPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.OrtoDatasPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData') containing the connection settings required to establish a connection to the PostgreSQL database\. This value can be null\.
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ClearAsync(System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.ClearAsync\(CancellationToken\) Method

Asynchronously clears all data and restarts the identity sequence\.

```csharp
public System.Threading.Tasks.Task<bool> ClearAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ClearAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is true if the operation succeeded; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ContainsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.ContainsByReferencesAsync\(IEnumerable\<string\>, Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously checks for the existence of a collection of references, optionally filtered by a county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<string>?> ContainsByReferencesAsync(System.Collections.Generic.IEnumerable<string> references, System.Nullable<int> countyId, bool inverted=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ContainsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,bool,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of strings representing the references to be checked\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ContainsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier for the county; if null, the search is not filtered by county\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ContainsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,bool,System.Threading.CancellationToken).inverted'></a>

`inverted` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to return the set of references that do not exist \(true\) or those that do exist \(false\)\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.ContainsByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of strings containing the filtered references, or null if the operation fails or no results are found\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetCountAsync\(NpgsqlConnection, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves the count of records from the database, optionally filtered by a specific county identifier\.

```csharp
public static System.Threading.Tasks.Task<long> GetCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county used to filter the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total count as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(System.Nullable_int_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetCountAsync\(Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves the count of records, optionally filtered by a specific county identifier\.

```csharp
public System.Threading.Tasks.Task<long> GetCountAsync(System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county to filter the count; if null, the count is retrieved for all counties\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetCountAsync(System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total row count as a long\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetEstimatedCountAsync\(NpgsqlConnection, IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves the estimated row count for the specified county identifiers in the PostgreSQL database\.

```csharp
public static System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<int> countyIds, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') to use for the query\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the county identifiers to estimate counts for\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean indicating whether to run an analysis operation before fetching the estimated count\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the total estimated row count as a long, or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetEstimatedCountAsync\(NpgsqlConnection, Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated count of records, optionally filtered by a specific county identifier\.

```csharp
public static System.Threading.Tasks.Task<long> GetEstimatedCountAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Nullable<int> countyId, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') instance used to execute the command\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier for the county; if null, the estimate is calculated across all counties\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A value indicating whether to perform an ANALYZE operation on the database table to update statistics before retrieving the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(Npgsql.NpgsqlConnection,System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated count as a long integer\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetEstimatedCountAsync\(IEnumerable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated row count for the specified collection of county identifiers\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(System.Collections.Generic.IEnumerable<int> countyIds, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of integers representing the IDs of the counties to estimate counts for\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to perform a database analysis before fetching the count\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Collections.Generic.IEnumerable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated total row count as a long, or \-1 if an error occurs\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetEstimatedCountAsync\(Nullable\<int\>, bool, CancellationToken\) Method

Asynchronously retrieves an estimated row count, optionally filtered by a specific county identifier\.

```csharp
public System.Threading.Tasks.Task<long> GetEstimatedCountAsync(System.Nullable<int> countyId, bool analyze=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') identifier of the county to filter the estimate; if null, the estimate is calculated for all counties\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).analyze'></a>

`analyze` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean') value indicating whether to run an analysis operation before fetching the count to improve accuracy\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetEstimatedCountAsync(System.Nullable_int_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the estimated number of rows as a [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64'), or \-1 if an error occurs or the target does not exist\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetExistingBuilding2DReferencesAsync\(NpgsqlConnection, IEnumerable\<Building2DReference\>, bool, CancellationToken\) Method

Asynchronously retrieves existing building 2D references from the database based on the provided collection\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetExistingBuilding2DReferencesAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference>? building2DReferences, bool inverted=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the PostgreSQL database\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects to check for existence in the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).inverted'></a>

`inverted` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to invert the search criteria, returning references that do not exist if set to true\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects, or null if the connection or input collection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetExistingBuilding2DReferencesAsync\(IEnumerable\<Building2DReference\>, bool, CancellationToken\) Method

Asynchronously retrieves existing building 2D references based on the provided collection and inversion criteria\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetExistingBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference>? building2DReferences, bool inverted=false, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of building 2D references to check for existence\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).inverted'></a>

`inverted` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A boolean value indicating whether to invert the result; if set to [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), retrieves references that do not exist\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetExistingBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of matching references, or null if the input collection is null\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetNextBuilding2DReferencesAsync(int)'></a>

## OrtoDatasPostgreSQLConverter\.GetNextBuilding2DReferencesAsync\(int\) Method

Asynchronously retrieves a list of building 2D references\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> GetNextBuilding2DReferencesAsync(int count=100);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetNextBuilding2DReferencesAsync(int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The maximum number of [Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference') objects to retrieve\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') if references are found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetOrtoDatasByReferenceAsync\(string, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves orthodata based on a specified reference and an optional county identifier\.

```csharp
public System.Threading.Tasks.Task<DiGi.GIS.PostgreSQL.Classes.OrtoDatas?> GetOrtoDatasByReferenceAsync(string reference, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string reference used to identify the orthodata\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferenceAsync(string,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to propagate notification that the operation should be canceled\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains the [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') object if found; otherwise, null\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetOrtoDatasByReferencesAsync\(NpgsqlConnection, IEnumerable\<string\>, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') based on the specified references and optional county identifier\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.OrtoDatas>?> GetOrtoDatasByReferencesAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Collections.Generic.IEnumerable<string>? references, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of strings representing the references to search for\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional integer identifier of the county to filter the results\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(Npgsql.NpgsqlConnection,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to monitor for cancellation requests\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') objects, or null if the connection is null or no matching data is found\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.GetOrtoDatasByReferencesAsync\(IEnumerable\<string\>, Nullable\<int\>, CancellationToken\) Method

Asynchronously retrieves a list of [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') based on the specified references and county identifier\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.OrtoDatas>?> GetOrtoDatasByReferencesAsync(System.Collections.Generic.IEnumerable<string>? references, System.Nullable<int> countyId, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of strings representing the references to filter by\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer representing the unique identifier of the county\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.GetOrtoDatasByReferencesAsync(System.Collections.Generic.IEnumerable_string_,System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a list of [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') objects, or null if no matching data is found\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.OrtoDatas_,double)'></a>

## OrtoDatasPostgreSQLConverter\.UpdateAsync\(IEnumerable\<OrtoDatas\>, double\) Method

Asynchronously updates the data based on the provided orthodata and tolerance\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.HashSet<long>?> UpdateAsync(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.OrtoDatas>? ortoDatas, double tolerance=0.001);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.OrtoDatas_,double).ortoDatas'></a>

`ortoDatas` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A nullable enumerable collection of [OrtoDatas](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatas 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatas') to be processed for the update\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.OrtoDatas_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double\-precision floating\-point number representing the distance tolerance used during the update process\. Defaults to [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance')\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a nullable long, which may represent a unique identifier or the number of updated records\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.UpdateBuilding2DReferencesAsync\(IEnumerable\<Building2DReference\>, CancellationToken\) Method

Asynchronously updates the specified collection of building 2D references\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> UpdateBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference> building2DReferences, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,System.Threading.CancellationToken).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') containing the building 2D references to update\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateBuilding2DReferencesAsync(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of the updated references, or null if the operation failed or the input collection was null\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateSubdivisionIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,System.Threading.CancellationToken)'></a>

## OrtoDatasPostgreSQLConverter\.UpdateSubdivisionIds\(IEnumerable\<Building2DReference\>, CancellationToken\) Method

Asynchronously updates the subdivision identifiers for the specified collection of building 2D references\.

```csharp
public System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReference>?> UpdateSubdivisionIds(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.Building2DReference>? building2DReferences, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateSubdivisionIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,System.Threading.CancellationToken).building2DReferences'></a>

`building2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') containing the building 2D references to be updated, or null\.

<a name='DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter.UpdateSubdivisionIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.Building2DReference_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[Building2DReference](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReference 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of the updated references, or null if the input collection was null or an error occurred\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateDatabaseTask'></a>

## PostgreSQLAdministrativeAreal2DCreateDatabaseTask Class

Represents a background task that creates the PostgreSQL database for AdministrativeAreal2D\.

```csharp
public class PostgreSQLAdministrativeAreal2DCreateDatabaseTask : DiGi.Core.Classes.BackgroundTask, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → PostgreSQLAdministrativeAreal2DCreateDatabaseTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateDatabaseTask.PostgreSQLAdministrativeAreal2DCreateDatabaseTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager)'></a>

## PostgreSQLAdministrativeAreal2DCreateDatabaseTask\(GISPostgreSQLConverterManager\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLAdministrativeAreal2DCreateDatabaseTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager gISPostgreSQLConverterManager);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateDatabaseTask.PostgreSQLAdministrativeAreal2DCreateDatabaseTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The GIS PostgreSQL converter manager used to create the database\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateDatabaseTask.gISPostgreSQLConverterManager'></a>

## PostgreSQLAdministrativeAreal2DCreateDatabaseTask\.gISPostgreSQLConverterManager Field

Gets the GIS PostgreSQL converter manager used to create the database\.

```csharp
private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;
```

#### Field Value
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateDatabaseTask.ExecuteAsync()'></a>

## PostgreSQLAdministrativeAreal2DCreateDatabaseTask\.ExecuteAsync\(\) Method

Executes the background task to create the PostgreSQL database for AdministrativeAreal2D\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the database was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateTableTask'></a>

## PostgreSQLAdministrativeAreal2DCreateTableTask Class

Represents a background task that creates the PostgreSQL table for AdministrativeAreal2D\.

```csharp
public class PostgreSQLAdministrativeAreal2DCreateTableTask : DiGi.Core.Classes.BackgroundTask, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → PostgreSQLAdministrativeAreal2DCreateTableTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateTableTask.PostgreSQLAdministrativeAreal2DCreateTableTask(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter)'></a>

## PostgreSQLAdministrativeAreal2DCreateTableTask\(AdministrativeAreal2DPostgreSQLConverter\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLAdministrativeAreal2DCreateTableTask(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateTableTask.PostgreSQLAdministrativeAreal2DCreateTableTask(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter).administrativeAreal2DPostgreSQLConverter'></a>

`administrativeAreal2DPostgreSQLConverter` [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')

The AdministrativeAreal2D PostgreSQL converter used to create the table\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateTableTask.administrativeAreal2DPostgreSQLConverter'></a>

## PostgreSQLAdministrativeAreal2DCreateTableTask\.administrativeAreal2DPostgreSQLConverter Field

Gets the AdministrativeAreal2D PostgreSQL converter used to create the table\.

```csharp
private readonly AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter;
```

#### Field Value
[AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateTableTask.ExecuteAsync()'></a>

## PostgreSQLAdministrativeAreal2DCreateTableTask\.ExecuteAsync\(\) Method

Executes the background task to create the PostgreSQL table for AdministrativeAreal2D\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions'></a>

## PostgreSQLAdministrativeAreal2DRefreshOptions Class

Options for refreshing PostgreSQL administrative areal 2D data\.

```csharp
public class PostgreSQLAdministrativeAreal2DRefreshOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → PostgreSQLAdministrativeAreal2DRefreshOptions
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions.PostgreSQLAdministrativeAreal2DRefreshOptions()'></a>

## PostgreSQLAdministrativeAreal2DRefreshOptions\(\) Constructor

Initializes a new instance of the [PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions') class\.

```csharp
public PostgreSQLAdministrativeAreal2DRefreshOptions();
```

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions.PostgreSQLAdministrativeAreal2DRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions)'></a>

## PostgreSQLAdministrativeAreal2DRefreshOptions\(PostgreSQLAdministrativeAreal2DRefreshOptions\) Constructor

Initializes a new instance of the [PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions') class by copying another instance\.

```csharp
public PostgreSQLAdministrativeAreal2DRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions postgreSQLAdministrativeAreal2DRefreshOptions);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions.PostgreSQLAdministrativeAreal2DRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions).postgreSQLAdministrativeAreal2DRefreshOptions'></a>

`postgreSQLAdministrativeAreal2DRefreshOptions` [PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions')

The source options to copy from\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions.PostgreSQLAdministrativeAreal2DRefreshOptions(System.Text.Json.Nodes.JsonObject)'></a>

## PostgreSQLAdministrativeAreal2DRefreshOptions\(JsonObject\) Constructor

Initializes a new instance of the [PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions') class from a JSON object\.

```csharp
public PostgreSQLAdministrativeAreal2DRefreshOptions(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions.PostgreSQLAdministrativeAreal2DRefreshOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the refresh options\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions.Tolerance'></a>

## PostgreSQLAdministrativeAreal2DRefreshOptions\.Tolerance Property

Gets or sets the tolerance value used for the refresh process\.

```csharp
public double Tolerance { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask'></a>

## PostgreSQLAdministrativeAreal2DRefreshTask Class

Represents a background task that refreshes the PostgreSQL data for AdministrativeAreal2D\.

```csharp
public class PostgreSQLAdministrativeAreal2DRefreshTask : DiGi.Core.Classes.ReportableBackgroundTask<long>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → [DiGi\.Core\.Classes\.CancelableBackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cancelablebackgroundtask 'DiGi\.Core\.Classes\.CancelableBackgroundTask') → [DiGi\.Core\.Classes\.ReportableBackgroundTask&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1') → PostgreSQLAdministrativeAreal2DRefreshTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.PostgreSQLAdministrativeAreal2DRefreshTask(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter)'></a>

## PostgreSQLAdministrativeAreal2DRefreshTask\(AdministrativeAreal2DPostgreSQLConverter\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLAdministrativeAreal2DRefreshTask(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.PostgreSQLAdministrativeAreal2DRefreshTask(DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter).administrativeAreal2DPostgreSQLConverter'></a>

`administrativeAreal2DPostgreSQLConverter` [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')

The AdministrativeAreal2D PostgreSQL converter used to refresh the data\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.administrativeAreal2DPostgreSQLConverter'></a>

## PostgreSQLAdministrativeAreal2DRefreshTask\.administrativeAreal2DPostgreSQLConverter Field

Gets the AdministrativeAreal2D PostgreSQL converter used to refresh the data\.

```csharp
private readonly AdministrativeAreal2DPostgreSQLConverter administrativeAreal2DPostgreSQLConverter;
```

#### Field Value
[AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.PostgreSQLAdministrativeAreal2DRefreshOptions'></a>

## PostgreSQLAdministrativeAreal2DRefreshTask\.PostgreSQLAdministrativeAreal2DRefreshOptions Property

Gets the configuration for the PostgreSQL operation\.
These options will be used when RunAsync is triggered\.

```csharp
public DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions PostgreSQLAdministrativeAreal2DRefreshOptions { get; set; }
```

#### Property Value
[PostgreSQLAdministrativeAreal2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshOptions')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken)'></a>

## PostgreSQLAdministrativeAreal2DRefreshTask\.ExecuteAsync\(IProgress\<long\>, CancellationToken\) Method

Executes the background task to refresh the PostgreSQL data for AdministrativeAreal2D\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync(System.IProgress<long> progress, System.Threading.CancellationToken cancellationToken);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

A progress reporter for reporting the number of processed items\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the refresh was successful; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DCreateTableTask'></a>

## PostgreSQLBuilding2DCreateTableTask Class

Represents a background task that creates the PostgreSQL table for Building2D\.

```csharp
public class PostgreSQLBuilding2DCreateTableTask : DiGi.Core.Classes.BackgroundTask, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → PostgreSQLBuilding2DCreateTableTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DCreateTableTask.PostgreSQLBuilding2DCreateTableTask(DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter)'></a>

## PostgreSQLBuilding2DCreateTableTask\(Building2DPostgreSQLConverter\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLBuilding2DCreateTableTask(DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter building2DPostgreSQLConverter);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DCreateTableTask.PostgreSQLBuilding2DCreateTableTask(DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter).building2DPostgreSQLConverter'></a>

`building2DPostgreSQLConverter` [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')

The Building2D PostgreSQL converter used to create the table\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DCreateTableTask.building2DPostgreSQLConverter'></a>

## PostgreSQLBuilding2DCreateTableTask\.building2DPostgreSQLConverter Field

Gets the Building2D PostgreSQL converter used to create the table\.

```csharp
private readonly Building2DPostgreSQLConverter building2DPostgreSQLConverter;
```

#### Field Value
[Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DCreateTableTask.ExecuteAsync()'></a>

## PostgreSQLBuilding2DCreateTableTask\.ExecuteAsync\(\) Method

Executes the background task to create the PostgreSQL table for Building2D\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the table was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions'></a>

## PostgreSQLBuilding2DRefreshOptions Class

Options for refreshing 2D building data in a PostgreSQL database\.

```csharp
public class PostgreSQLBuilding2DRefreshOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → PostgreSQLBuilding2DRefreshOptions
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.PostgreSQLBuilding2DRefreshOptions()'></a>

## PostgreSQLBuilding2DRefreshOptions\(\) Constructor

Initializes a new instance of the [PostgreSQLBuilding2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshOptions') class\.

```csharp
public PostgreSQLBuilding2DRefreshOptions();
```

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.PostgreSQLBuilding2DRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions)'></a>

## PostgreSQLBuilding2DRefreshOptions\(PostgreSQLBuilding2DRefreshOptions\) Constructor

Initializes a new instance of the [PostgreSQLBuilding2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshOptions') class by copying an existing options instance\.

```csharp
public PostgreSQLBuilding2DRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions postgreSQLBuilding2DRefreshOptions);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.PostgreSQLBuilding2DRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions).postgreSQLBuilding2DRefreshOptions'></a>

`postgreSQLBuilding2DRefreshOptions` [PostgreSQLBuilding2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshOptions')

The source options to copy from\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.PostgreSQLBuilding2DRefreshOptions(System.Text.Json.Nodes.JsonObject)'></a>

## PostgreSQLBuilding2DRefreshOptions\(JsonObject\) Constructor

Initializes a new instance of the [PostgreSQLBuilding2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshOptions') class using a JSON object\.

```csharp
public PostgreSQLBuilding2DRefreshOptions(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.PostgreSQLBuilding2DRefreshOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object used to initialize the options\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.BatchSize'></a>

## PostgreSQLBuilding2DRefreshOptions\.BatchSize Property

Gets or sets the number of records to process in a single batch\.

```csharp
public int BatchSize { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.OverrideExistingSubdivisionIds'></a>

## PostgreSQLBuilding2DRefreshOptions\.OverrideExistingSubdivisionIds Property

Gets or sets a value indicating whether existing subdivision IDs should be overridden during the refresh process\.

```csharp
public bool OverrideExistingSubdivisionIds { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions.Tolerance'></a>

## PostgreSQLBuilding2DRefreshOptions\.Tolerance Property

Gets or sets the distance tolerance used for processing building data\.

```csharp
public double Tolerance { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask'></a>

## PostgreSQLBuilding2DRefreshTask Class

Represents a background task that refreshes the PostgreSQL data for Building2D\.

```csharp
public class PostgreSQLBuilding2DRefreshTask : DiGi.Core.Classes.ReportableBackgroundTask<long>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → [DiGi\.Core\.Classes\.CancelableBackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cancelablebackgroundtask 'DiGi\.Core\.Classes\.CancelableBackgroundTask') → [DiGi\.Core\.Classes\.ReportableBackgroundTask&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1') → PostgreSQLBuilding2DRefreshTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.PostgreSQLBuilding2DRefreshTask(DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter)'></a>

## PostgreSQLBuilding2DRefreshTask\(Building2DPostgreSQLConverter\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLBuilding2DRefreshTask(DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter building2DPostgreSQLConverter);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.PostgreSQLBuilding2DRefreshTask(DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter).building2DPostgreSQLConverter'></a>

`building2DPostgreSQLConverter` [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')

The Building2D PostgreSQL converter used to refresh the data\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.building2DPostgreSQLConverter'></a>

## PostgreSQLBuilding2DRefreshTask\.building2DPostgreSQLConverter Field

Gets the Building2D PostgreSQL converter used to refresh the data\.

```csharp
private readonly Building2DPostgreSQLConverter building2DPostgreSQLConverter;
```

#### Field Value
[Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.PostgreSQLBuilding2DRefreshOptions'></a>

## PostgreSQLBuilding2DRefreshTask\.PostgreSQLBuilding2DRefreshOptions Property

Gets the configuration for the PostgreSQL operation\.
These options will be used when RunAsync is triggered\.

```csharp
public DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions PostgreSQLBuilding2DRefreshOptions { get; set; }
```

#### Property Value
[PostgreSQLBuilding2DRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshOptions')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken)'></a>

## PostgreSQLBuilding2DRefreshTask\.ExecuteAsync\(IProgress\<long\>, CancellationToken\) Method

Executes the background task to refresh the PostgreSQL data for Building2D\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync(System.IProgress<long> progress, System.Threading.CancellationToken cancellationToken);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

A progress reporter for reporting the number of processed items\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the refresh was successful; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateOptions'></a>

## PostgreSQLBuildingDataUpdateOptions Class

Represents the options for updating building data in a PostgreSQL database, specifying which types of building data updates should be performed\.

```csharp
public class PostgreSQLBuildingDataUpdateOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → PostgreSQLBuildingDataUpdateOptions
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateOptions.PostgreSQLBuildingDataUpdateOptions()'></a>

## PostgreSQLBuildingDataUpdateOptions\(\) Constructor

Initializes a new instance of the [PostgreSQLBuildingDataUpdateOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuildingDataUpdateOptions') class\.

```csharp
public PostgreSQLBuildingDataUpdateOptions();
```
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateOptions.BuildingDataUpdateTypes'></a>

## PostgreSQLBuildingDataUpdateOptions\.BuildingDataUpdateTypes Property

Gets or sets the collection of building data update types that specify which types of building data updates should be performed\.

```csharp
public System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType>? BuildingDataUpdateTypes { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[BuildingDataUpdateType](DiGi.GIS.PostgreSQL.Enums.md#DiGi.GIS.PostgreSQL.Enums.BuildingDataUpdateType 'DiGi\.GIS\.PostgreSQL\.Enums\.BuildingDataUpdateType')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask'></a>

## PostgreSQLBuildingDataUpdateTask Class

Represents a background task that updates building data from AdministrativeAreal2D and Building2D sources\.

```csharp
public class PostgreSQLBuildingDataUpdateTask : DiGi.Core.Classes.ReportableBackgroundTask<long>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → [DiGi\.Core\.Classes\.CancelableBackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cancelablebackgroundtask 'DiGi\.Core\.Classes\.CancelableBackgroundTask') → [DiGi\.Core\.Classes\.ReportableBackgroundTask&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1') → PostgreSQLBuildingDataUpdateTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.PostgreSQLBuildingDataUpdateTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager)'></a>

## PostgreSQLBuildingDataUpdateTask\(GISPostgreSQLConverterManager\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLBuildingDataUpdateTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager gISPostgreSQLConverterManager);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.PostgreSQLBuildingDataUpdateTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The GIS PostgreSQL converter manager used to retrieve converters and execute operations\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.gISPostgreSQLConverterManager'></a>

## PostgreSQLBuildingDataUpdateTask\.gISPostgreSQLConverterManager Field

Gets the GIS PostgreSQL converter manager used to retrieve converters and execute operations\.

```csharp
private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;
```

#### Field Value
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.uIBuildingDataUpdateOptions'></a>

## PostgreSQLBuildingDataUpdateTask\.uIBuildingDataUpdateOptions Property

Gets or sets the options used to configure the PostgreSQL building data update process\.

```csharp
public DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateOptions uIBuildingDataUpdateOptions { get; set; }
```

#### Property Value
[PostgreSQLBuildingDataUpdateOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuildingDataUpdateOptions')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken)'></a>

## PostgreSQLBuildingDataUpdateTask\.ExecuteAsync\(IProgress\<long\>, CancellationToken\) Method

Executes the background task to update building data from AdministrativeAreal2D and Building2D sources\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync(System.IProgress<long> progress, System.Threading.CancellationToken cancellationToken);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

A progress reporter for reporting the number of processed items\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the update was successful; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasCreateDatabaseTask'></a>

## PostgreSQLOrtoDatasCreateDatabaseTask Class

Represents a background task that creates the PostgreSQL database for OrtoDatas\.

```csharp
public class PostgreSQLOrtoDatasCreateDatabaseTask : DiGi.Core.Classes.BackgroundTask, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → PostgreSQLOrtoDatasCreateDatabaseTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasCreateDatabaseTask.PostgreSQLOrtoDatasCreateDatabaseTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager)'></a>

## PostgreSQLOrtoDatasCreateDatabaseTask\(GISPostgreSQLConverterManager\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLOrtoDatasCreateDatabaseTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager gISPostgreSQLConverterManager);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasCreateDatabaseTask.PostgreSQLOrtoDatasCreateDatabaseTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The GIS PostgreSQL converter manager used to create the database\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasCreateDatabaseTask.gISPostgreSQLConverterManager'></a>

## PostgreSQLOrtoDatasCreateDatabaseTask\.gISPostgreSQLConverterManager Field

Gets the GIS PostgreSQL converter manager used to create the database\.

```csharp
private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;
```

#### Field Value
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasCreateDatabaseTask.ExecuteAsync()'></a>

## PostgreSQLOrtoDatasCreateDatabaseTask\.ExecuteAsync\(\) Method

Executes the background task to create the PostgreSQL database for OrtoDatas\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the database was created successfully; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions'></a>

## PostgreSQLOrtoDatasRefreshOptions Class

Provides options for refreshing orthodata within a PostgreSQL database\.

```csharp
public class PostgreSQLOrtoDatasRefreshOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → PostgreSQLOrtoDatasRefreshOptions
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.PostgreSQLOrtoDatasRefreshOptions()'></a>

## PostgreSQLOrtoDatasRefreshOptions\(\) Constructor

Initializes a new instance of the [PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions') class\.

```csharp
public PostgreSQLOrtoDatasRefreshOptions();
```

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.PostgreSQLOrtoDatasRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions)'></a>

## PostgreSQLOrtoDatasRefreshOptions\(PostgreSQLOrtoDatasRefreshOptions\) Constructor

Initializes a new instance of the [PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions') class by copying an existing options instance\.

```csharp
public PostgreSQLOrtoDatasRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions postgreSQLOrtoDatasRefreshOptions);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.PostgreSQLOrtoDatasRefreshOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions).postgreSQLOrtoDatasRefreshOptions'></a>

`postgreSQLOrtoDatasRefreshOptions` [PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions')

The source options instance to copy from\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.PostgreSQLOrtoDatasRefreshOptions(System.Text.Json.Nodes.JsonObject)'></a>

## PostgreSQLOrtoDatasRefreshOptions\(JsonObject\) Constructor

Initializes a new instance of the [PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions') class using a JSON object\.

```csharp
public PostgreSQLOrtoDatasRefreshOptions(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.PostgreSQLOrtoDatasRefreshOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the configuration settings\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.CountyIds'></a>

## PostgreSQLOrtoDatasRefreshOptions\.CountyIds Property

Gets or sets the set of county identifiers for which data should be refreshed\.

```csharp
public System.Collections.Generic.HashSet<int>? CountyIds { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.OverrideExistsing'></a>

## PostgreSQLOrtoDatasRefreshOptions\.OverrideExistsing Property

Gets or sets a value indicating whether existing data should be overridden during the refresh process\.

```csharp
public bool OverrideExistsing { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions.UpdateSubdivisionIds'></a>

## PostgreSQLOrtoDatasRefreshOptions\.UpdateSubdivisionIds Property

Gets or sets a value indicating whether subdivision identifiers should be updated during the refresh process\.

```csharp
public bool UpdateSubdivisionIds { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask'></a>

## PostgreSQLOrtoDatasRefreshTask Class

Represents a background task that refreshes the PostgreSQL data for OrtoDatas\.

```csharp
public class PostgreSQLOrtoDatasRefreshTask : DiGi.Core.Classes.ReportableBackgroundTask<long>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → [DiGi\.Core\.Classes\.CancelableBackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cancelablebackgroundtask 'DiGi\.Core\.Classes\.CancelableBackgroundTask') → [DiGi\.Core\.Classes\.ReportableBackgroundTask&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1') → PostgreSQLOrtoDatasRefreshTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.PostgreSQLOrtoDatasRefreshTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager)'></a>

## PostgreSQLOrtoDatasRefreshTask\(GISPostgreSQLConverterManager\) Constructor

Constructor with Dependency Injection\.

```csharp
public PostgreSQLOrtoDatasRefreshTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager gISPostgreSQLConverterManager);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.PostgreSQLOrtoDatasRefreshTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The GIS PostgreSQL converter manager used to refresh the data\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.gISPostgreSQLConverterManager'></a>

## PostgreSQLOrtoDatasRefreshTask\.gISPostgreSQLConverterManager Field

Gets the GIS PostgreSQL converter manager used to refresh the data\.

```csharp
private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;
```

#### Field Value
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.PostgreSQLOrtoDatasRefreshOptions'></a>

## PostgreSQLOrtoDatasRefreshTask\.PostgreSQLOrtoDatasRefreshOptions Property

Gets the configuration for the PostgreSQL operation\.
These options will be used when RunAsync is triggered\.

```csharp
public DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions PostgreSQLOrtoDatasRefreshOptions { get; set; }
```

#### Property Value
[PostgreSQLOrtoDatasRefreshOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshOptions')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken)'></a>

## PostgreSQLOrtoDatasRefreshTask\.ExecuteAsync\(IProgress\<long\>, CancellationToken\) Method

Executes the background task to refresh the PostgreSQL data for OrtoDatas\.

```csharp
protected override System.Threading.Tasks.Task<bool> ExecuteAsync(System.IProgress<long> progress, System.Threading.CancellationToken cancellationToken);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

A progress reporter for reporting the number of processed items\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask.ExecuteAsync(System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token that can be used to cancel the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task representing the asynchronous operation\. Returns true if the refresh was successful; otherwise, false\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions'></a>

## PostgreSQLUpdateOccupancyOptions Class

Provides configuration options for updating occupancy data within a PostgreSQL database\.

```csharp
public class PostgreSQLUpdateOccupancyOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → PostgreSQLUpdateOccupancyOptions
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.PostgreSQLUpdateOccupancyOptions()'></a>

## PostgreSQLUpdateOccupancyOptions\(\) Constructor

Initializes a new instance of the [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions') class\.

```csharp
public PostgreSQLUpdateOccupancyOptions();
```

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.PostgreSQLUpdateOccupancyOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions)'></a>

## PostgreSQLUpdateOccupancyOptions\(PostgreSQLUpdateOccupancyOptions\) Constructor

Initializes a new instance of the [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions') class by copying the values from an existing [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions') instance\.

```csharp
public PostgreSQLUpdateOccupancyOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions postgreSQLUpdateOccupancyOptions);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.PostgreSQLUpdateOccupancyOptions(DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions).postgreSQLUpdateOccupancyOptions'></a>

`postgreSQLUpdateOccupancyOptions` [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions')

The source [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions') instance to copy settings from\.

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.PostgreSQLUpdateOccupancyOptions(System.Text.Json.Nodes.JsonObject)'></a>

## PostgreSQLUpdateOccupancyOptions\(JsonObject\) Constructor

Initializes a new instance of the [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions') class using the provided JSON object\.

```csharp
public PostgreSQLUpdateOccupancyOptions(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.PostgreSQLUpdateOccupancyOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the configuration data used to populate the options\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.Clear'></a>

## PostgreSQLUpdateOccupancyOptions\.Clear Property

Gets or sets a value indicating whether the existing occupancy data should be cleared before performing the update operation\.

```csharp
public bool Clear { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.IncludeAdministrativeAreal2Ds'></a>

## PostgreSQLUpdateOccupancyOptions\.IncludeAdministrativeAreal2Ds Property

Gets or sets a value indicating whether administrative areal 2D data should be included during the occupancy update process\.

```csharp
public bool IncludeAdministrativeAreal2Ds { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions.IncludeBuilding2Ds'></a>

## PostgreSQLUpdateOccupancyOptions\.IncludeBuilding2Ds Property

Gets or sets a value indicating whether building 2D data should be included during the occupancy update process\.

```csharp
public bool IncludeBuilding2Ds { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask'></a>

## PostgreSQLUpdateOccupancyTask Class

Represents a background task responsible for updating occupancy data within a PostgreSQL GIS database\.

This class leverages the [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager') to execute the update process based on the provided [PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyTask\.PostgreSQLUpdateOccupancyOptions').

```csharp
public class PostgreSQLUpdateOccupancyTask : DiGi.Core.Classes.ReportableBackgroundTask<long>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.BackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.backgroundtask 'DiGi\.Core\.Classes\.BackgroundTask') → [DiGi\.Core\.Classes\.CancelableBackgroundTask](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cancelablebackgroundtask 'DiGi\.Core\.Classes\.CancelableBackgroundTask') → [DiGi\.Core\.Classes\.ReportableBackgroundTask&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.reportablebackgroundtask-1 'DiGi\.Core\.Classes\.ReportableBackgroundTask\`1') → PostgreSQLUpdateOccupancyTask

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask.PostgreSQLUpdateOccupancyTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager)'></a>

## PostgreSQLUpdateOccupancyTask\(GISPostgreSQLConverterManager\) Constructor

Initializes a new instance of the [PostgreSQLUpdateOccupancyTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyTask') class\.

```csharp
public PostgreSQLUpdateOccupancyTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager gISPostgreSQLConverterManager);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask.PostgreSQLUpdateOccupancyTask(DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager).gISPostgreSQLConverterManager'></a>

`gISPostgreSQLConverterManager` [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')

The [GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager') used to refresh the occupancy data\.
### Fields

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask.gISPostgreSQLConverterManager'></a>

## PostgreSQLUpdateOccupancyTask\.gISPostgreSQLConverterManager Field

Gets the GIS PostgreSQL converter manager used to refresh the data\.

```csharp
private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;
```

#### Field Value
[GISPostgreSQLConverterManager](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.GISPostgreSQLConverterManager 'DiGi\.GIS\.PostgreSQL\.Classes\.GISPostgreSQLConverterManager')
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask.PostgreSQLUpdateOccupancyOptions'></a>

## PostgreSQLUpdateOccupancyTask\.PostgreSQLUpdateOccupancyOptions Property

Gets or sets the options used to configure the PostgreSQL occupancy update process\.

```csharp
public DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions PostgreSQLUpdateOccupancyOptions { get; set; }
```

#### Property Value
[PostgreSQLUpdateOccupancyOptions](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyOptions 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyOptions')

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_'></a>

## ReferencedObject\<TUniqueObject\> Class

Abstract base class for referenced objects, providing common properties for serialization and identification\.

```csharp
public abstract class ReferencedObject<TUniqueObject> : DiGi.GIS.PostgreSQL.Classes.TableSerializableObject<TUniqueObject>
    where TUniqueObject : DiGi.Core.Interfaces.IUniqueObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.TUniqueObject'></a>

`TUniqueObject`

The type of the unique object this referenced object points to\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → ReferencedObject\<TUniqueObject\>

Derived  
↳ [AdministrativeAreal2DReferencedObject&lt;TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>')  
↳ [Areal2D&lt;TAreal2D&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Areal2D_TAreal2D_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Areal2D\<TAreal2D\>')  
↳ [Building2DReferencedObject&lt;TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.ReferencedObject()'></a>

## ReferencedObject\(\) Constructor

Initializes a new instance of the ReferencedObject class\.

```csharp
public ReferencedObject();
```

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.ReferencedObject(DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_)'></a>

## ReferencedObject\(ReferencedObject\<TUniqueObject\>\) Constructor

Initializes a new instance of the ReferencedObject class by copying data from another ReferencedObject instance\.

```csharp
public ReferencedObject(DiGi.GIS.PostgreSQL.Classes.ReferencedObject<TUniqueObject>? referencedObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.ReferencedObject(DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_).referencedObject'></a>

`referencedObject` [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[TUniqueObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.TUniqueObject 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>\.TUniqueObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')

The ReferencedObject instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.ReferencedObject(System.Text.Json.Nodes.JsonObject)'></a>

## ReferencedObject\(JsonObject\) Constructor

Initializes a new instance of the ReferencedObject class from a JsonObject\.

```csharp
public ReferencedObject(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.ReferencedObject(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.CreatedAt'></a>

## ReferencedObject\<TUniqueObject\>\.CreatedAt Property

Gets or sets the UTC timestamp when this object was created\.

```csharp
public System.Nullable<System.DateTime> CreatedAt { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.Reference'></a>

## ReferencedObject\<TUniqueObject\>\.Reference Property

Gets or sets the reference string associated with this object\.

```csharp
public string? Reference { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_.UniqueId'></a>

## ReferencedObject\<TUniqueObject\>\.UniqueId Property

Gets or sets the unique ID of the referenced object\.

```csharp
public string? UniqueId { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_'></a>

## ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\> Class

Provides a base implementation for converting PostgreSQL database records into referenced objects\.

```csharp
public abstract class ReferencedObjectPostgreSQLConverter<TReferencedObject,TUniqueObject> : DiGi.PostgreSQL.Classes.PostgreSQLConverter<TReferencedObject>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter<TReferencedObject>, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
    where TReferencedObject : DiGi.GIS.PostgreSQL.Classes.ReferencedObject<TUniqueObject>
    where TUniqueObject : DiGi.Core.Interfaces.IUniqueObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.TReferencedObject'></a>

`TReferencedObject`

The type of the referenced object, which must inherit from [ReferencedObject&lt;TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')\.

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.TUniqueObject'></a>

`TUniqueObject`

The type of the unique object identifier, which must implement [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject')\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[TReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.TReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>\.TReferencedObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>

Derived  
↳ [AdministrativeAreal2DReferencedObjectPostgreSQLConverter&lt;TAdministrativeAreal2DReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>')  
↳ [Building2DReferencedObjectPostgreSQLConverter&lt;TBuilding2DReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>')

Implements [DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>')[TReferencedObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.TReferencedObject 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>\.TReferencedObject')[&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>'), [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## ReferencedObjectPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [ReferencedObjectPostgreSQLConverter&lt;TReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') class\.

```csharp
public ReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ReferencedObjectPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The connection data used to establish a connection to the PostgreSQL database\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.TableName'></a>

## ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>\.TableName Property

Gets the name of the table in the PostgreSQL database that this converter operates on\.

```csharp
public abstract string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken,int)'></a>

## ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>\.ClearAsync\(NpgsqlConnection, CancellationToken, int\) Method

Asynchronously clears all records from the referenced object table using the provided PostgreSQL connection\.

```csharp
public System.Threading.Tasks.Task<bool> ClearAsync(Npgsql.NpgsqlConnection? npgsqlConnection, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken,int).npgsqlConnection'></a>

`npgsqlConnection` [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection')

The [Npgsql\.NpgsqlConnection](https://learn.microsoft.com/en-us/dotnet/api/npgsql.npgsqlconnection 'Npgsql\.NpgsqlConnection') used to connect to the database\. This value can be null\.

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') used to cancel the asynchronous operation\.

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(Npgsql.NpgsqlConnection,System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a boolean value indicating whether the clear operation was successful\.

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(System.Threading.CancellationToken,int)'></a>

## ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>\.ClearAsync\(CancellationToken, int\) Method

Asynchronously clears all records from the referenced object table in the PostgreSQL database\.

```csharp
public System.Threading.Tasks.Task<bool> ClearAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken), int commandTimeout=30);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(System.Threading.CancellationToken,int).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The cancellation token used to cancel the asynchronous operation\.

<a name='DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_.ClearAsync(System.Threading.CancellationToken,int).commandTimeout'></a>

`commandTimeout` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The timeout in seconds for the execution of the command\. A value of 0 disables the timeout\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a boolean value indicating whether the clear operation was successful\.

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_'></a>

## TableSerializableObject\<TSerializableObject\> Class

Abstract base class for table\-serializable objects, providing conversion between JSON and GIS serializable objects\.

```csharp
public abstract class TableSerializableObject<TSerializableObject> : DiGi.Core.Classes.SerializableObject, DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject<TSerializableObject>, DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject, DiGi.GIS.PostgreSQL.Interfaces.ITableObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
    where TSerializableObject : DiGi.Core.Interfaces.ISerializableObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TSerializableObject'></a>

`TSerializableObject`

The type of the GIS serializable object\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → TableSerializableObject\<TSerializableObject\>

Derived  
↳ [ReferencedObject&lt;TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')

Implements [DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')[TSerializableObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>\.TSerializableObject')[&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>'), [ITableSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject'), [ITableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLSerializableObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TableSerializableObject()'></a>

## TableSerializableObject\(\) Constructor

Initializes a new instance of the TableSerializableObject class\.

```csharp
public TableSerializableObject();
```

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TableSerializableObject(DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_)'></a>

## TableSerializableObject\(TableSerializableObject\<TSerializableObject\>\) Constructor

Initializes a new instance of the TableSerializableObject class by copying data from another TableSerializableObject instance\.

```csharp
public TableSerializableObject(DiGi.GIS.PostgreSQL.Classes.TableSerializableObject<TSerializableObject>? tableSerializableObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TableSerializableObject(DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_).tableSerializableObject'></a>

`tableSerializableObject` [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[TSerializableObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>\.TSerializableObject')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')

The TableSerializableObject instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TableSerializableObject(System.Text.Json.Nodes.JsonObject)'></a>

## TableSerializableObject\(JsonObject\) Constructor

Initializes a new instance of the TableSerializableObject class from a JsonObject\.

```csharp
public TableSerializableObject(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TableSerializableObject(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.Object'></a>

## TableSerializableObject\<TSerializableObject\>\.Object Property

Gets or sets the JSON object containing the serialized GIS serializable object data\.

```csharp
public System.Text.Json.Nodes.JsonObject? Object { get; set; }
```

Implements [Object](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject.Object 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\.Object')

#### Property Value
[System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.FromDiGi(TSerializableObject)'></a>

## TableSerializableObject\<TSerializableObject\>\.FromDiGi\(TSerializableObject\) Method

Sets the Object property from a DiGi GIS serializable object\.

```csharp
public void FromDiGi(TSerializableObject? gISSerializableObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.FromDiGi(TSerializableObject).gISSerializableObject'></a>

`gISSerializableObject` [TSerializableObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>\.TSerializableObject')

The GIS serializable object to serialize\.

<a name='DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.ToDiGi()'></a>

## TableSerializableObject\<TSerializableObject\>\.ToDiGi\(\) Method

Converts the serialized Object property to a DiGi GIS serializable object\.

```csharp
public TSerializableObject? ToDiGi();
```

Implements [ToDiGi\(\)](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_.ToDiGi() 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\.ToDiGi\(\)')

#### Returns
[TSerializableObject](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_.TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>\.TSerializableObject')  
The deserialized TSerializableObject, or default if conversion fails\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModel'></a>

## TypologyModel Class

Represents a typology model associated with an administrative 2D area reference\.

```csharp
public class TypologyModel : DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject<DiGi.Typology.Classes.TypologyModel>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.Typology\.Classes\.TypologyModel](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typologymodel 'DiGi\.Typology\.Classes\.TypologyModel')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.Typology\.Classes\.TypologyModel](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typologymodel 'DiGi\.Typology\.Classes\.TypologyModel')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>')[DiGi\.Typology\.Classes\.TypologyModel](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typologymodel 'DiGi\.Typology\.Classes\.TypologyModel')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObject\<TUniqueObject\>') → TypologyModel
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModel.TypologyModel()'></a>

## TypologyModel\(\) Constructor

Initializes a new instance of the TypologyModel class\.

```csharp
public TypologyModel();
```

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModel.TypologyModel(DiGi.GIS.PostgreSQL.Classes.TypologyModel)'></a>

## TypologyModel\(TypologyModel\) Constructor

Initializes a new instance of the TypologyModel class by copying data from another TypologyModel instance\.

```csharp
public TypologyModel(DiGi.GIS.PostgreSQL.Classes.TypologyModel? typologyModel);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModel.TypologyModel(DiGi.GIS.PostgreSQL.Classes.TypologyModel).typologyModel'></a>

`typologyModel` [TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel')

The TypologyModel instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModel.TypologyModel(System.Text.Json.Nodes.JsonObject)'></a>

## TypologyModel\(JsonObject\) Constructor

Initializes a new instance of the TypologyModel class from a JsonObject\.

```csharp
public TypologyModel(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModel.TypologyModel(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter'></a>

## TypologyModelPostgreSQLConverter Class

Converter class for handling PostgreSQL database operations for [TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel') objects\.

```csharp
public class TypologyModelPostgreSQLConverter : DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.TypologyModel, DiGi.Typology.Classes.TypologyModel>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[DiGi\.Typology\.Classes\.TypologyModel](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typologymodel 'DiGi\.Typology\.Classes\.TypologyModel')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>')[TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>')[DiGi\.Typology\.Classes\.TypologyModel](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typologymodel 'DiGi\.Typology\.Classes\.TypologyModel')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencedObjectPostgreSQLConverter_TAdministrativeAreal2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencedObjectPostgreSQLConverter\<TAdministrativeAreal2DReferencedObject,TUniqueObject\>') → TypologyModelPostgreSQLConverter
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.TypologyModelPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## TypologyModelPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [TypologyModelPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModelPostgreSQLConverter') class\.

```csharp
public TypologyModelPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.TypologyModelPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The connection data used to connect to the PostgreSQL database\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.TableName'></a>

## TypologyModelPostgreSQLConverter\.TableName Property

Gets the name of the table in the PostgreSQL database associated with typology models\.

```csharp
public override string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_)'></a>

## TypologyModelPostgreSQLConverter\.Create\(int, string, string, JsonObject, Nullable\<DateTime\>\) Method

Creates a new instance of [TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel') using the provided data from the database\.

```csharp
protected override DiGi.GIS.PostgreSQL.Classes.TypologyModel Create(int id, string? uniqueId, string? reference, System.Text.Json.Nodes.JsonObject? @object, System.Nullable<System.DateTime> createdAt);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).id'></a>

`id` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The primary key identifier\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).uniqueId'></a>

`uniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique string identifier\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The reference string\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).object'></a>

`object` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the model's properties\.

<a name='DiGi.GIS.PostgreSQL.Classes.TypologyModelPostgreSQLConverter.Create(int,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).createdAt'></a>

`createdAt` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The timestamp of when the record was created\.

#### Returns
[TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel')  
A newly constructed [TypologyModel](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TypologyModel 'DiGi\.GIS\.PostgreSQL\.Classes\.TypologyModel') instance\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltData'></a>

## YearBuiltData Class

Represents year built data associated with a 2D building\.

```csharp
public class YearBuiltData : DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject<DiGi.GIS.Interfaces.IYearBuiltData>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')[DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObject\<TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObject_TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObject\<TUniqueObject\>') → YearBuiltData
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltData.YearBuiltData()'></a>

## YearBuiltData\(\) Constructor

Initializes a new instance of the YearBuiltData class\.

```csharp
public YearBuiltData();
```

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltData.YearBuiltData(DiGi.GIS.PostgreSQL.Classes.YearBuiltData)'></a>

## YearBuiltData\(YearBuiltData\) Constructor

Initializes a new instance of the YearBuiltData class by copying data from another YearBuiltData instance\.

```csharp
public YearBuiltData(DiGi.GIS.PostgreSQL.Classes.YearBuiltData? yearBuiltData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltData.YearBuiltData(DiGi.GIS.PostgreSQL.Classes.YearBuiltData).yearBuiltData'></a>

`yearBuiltData` [YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')

The YearBuiltData instance to copy data from\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltData.YearBuiltData(System.Text.Json.Nodes.JsonObject)'></a>

## YearBuiltData\(JsonObject\) Constructor

Initializes a new instance of the YearBuiltData class from a JsonObject\.

```csharp
public YearBuiltData(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltData.YearBuiltData(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JsonObject containing the serialized data\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter'></a>

## YearBuiltDataPostgreSQLConverter Class

Provides a converter for YearBuiltData objects when interacting with a PostgreSQL database\.

```csharp
public class YearBuiltDataPostgreSQLConverter : DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter<DiGi.GIS.PostgreSQL.Classes.YearBuiltData, DiGi.GIS.Interfaces.IYearBuiltData>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1')[YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.postgresqlconverter-1 'DiGi\.PostgreSQL\.Classes\.PostgreSQLConverter\`1') → [DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>') → [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter&lt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>')[YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')[,](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>')[DiGi\.GIS\.Interfaces\.IYearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.iyearbuiltdata 'DiGi\.GIS\.Interfaces\.IYearBuiltData')[&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DReferencedObjectPostgreSQLConverter_TBuilding2DReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferencedObjectPostgreSQLConverter\<TBuilding2DReferencedObject,TUniqueObject\>') → YearBuiltDataPostgreSQLConverter
### Constructors

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.YearBuiltDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData)'></a>

## YearBuiltDataPostgreSQLConverter\(ConnectionData\) Constructor

Initializes a new instance of the [YearBuiltDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltDataPostgreSQLConverter') class\.

```csharp
public YearBuiltDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData? connectionData);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.YearBuiltDataPostgreSQLConverter(DiGi.PostgreSQL.Classes.ConnectionData).connectionData'></a>

`connectionData` [DiGi\.PostgreSQL\.Classes\.ConnectionData](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.classes.connectiondata 'DiGi\.PostgreSQL\.Classes\.ConnectionData')

The connection data used to establish a connection to the PostgreSQL database\.
### Properties

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.TableName'></a>

## YearBuiltDataPostgreSQLConverter\.TableName Property

Gets the name of the database table associated with year built data\.

```csharp
public override string TableName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_)'></a>

## YearBuiltDataPostgreSQLConverter\.Create\(long, Nullable\<int\>, string, string, JsonObject, Nullable\<DateTime\>\) Method

Creates an instance of [YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData') based on the provided database values\.

```csharp
protected override DiGi.GIS.PostgreSQL.Classes.YearBuiltData Create(long id, System.Nullable<int> countyId, string? uniqueId, string? reference, System.Text.Json.Nodes.JsonObject? @object, System.Nullable<System.DateTime> createdAt);
```
#### Parameters

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).id'></a>

`id` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The unique identifier of the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The county identifier associated with the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).uniqueId'></a>

`uniqueId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique external identifier for the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The reference string for the record\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).object'></a>

`object` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the data attributes\.

<a name='DiGi.GIS.PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter.Create(long,System.Nullable_int_,string,string,System.Text.Json.Nodes.JsonObject,System.Nullable_System.DateTime_).createdAt'></a>

`createdAt` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The timestamp when the record was created\.

#### Returns
[YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData')  
A new [YearBuiltData](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.YearBuiltData 'DiGi\.GIS\.PostgreSQL\.Classes\.YearBuiltData') instance\.