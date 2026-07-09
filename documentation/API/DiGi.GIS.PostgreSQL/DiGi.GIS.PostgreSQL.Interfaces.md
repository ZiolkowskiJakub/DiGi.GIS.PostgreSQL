#### [DiGi\.GIS\.PostgreSQL](DiGi.GIS.PostgreSQL.Overview.md 'DiGi\.GIS\.PostgreSQL\.Overview')

## DiGi\.GIS\.PostgreSQL\.Interfaces Namespace
### Interfaces

<a name='DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter'></a>

## IGISPostgreSQLConverter Interface

Defines the contract for a GIS\-specific PostgreSQL converter\.

```csharp
public interface IGISPostgreSQLConverter : DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
```

Derived  
↳ [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')  
↳ [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')  
↳ [BuildingDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter')  
↳ [EPWFilePostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.EPWFilePostgreSQLConverter')  
↳ [OrtoDatasPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter')  
↳ [ReferencedObjectPostgreSQLConverter&lt;TReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')  
↳ [IGISPostgreSQLConverter&lt;TTableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>')

Implements [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')

<a name='DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_'></a>

## IGISPostgreSQLConverter\<TTableObject\> Interface

Defines the contract for a generic GIS\-specific PostgreSQL converter for a specific table object type\.

```csharp
public interface IGISPostgreSQLConverter<TTableObject> : DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLConverter, DiGi.PostgreSQL.Interfaces.IPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject
    where TTableObject : DiGi.GIS.PostgreSQL.Interfaces.ITableObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_.TTableObject'></a>

`TTableObject`

The type of the table object that this converter handles\.

Derived  
↳ [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')  
↳ [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')  
↳ [OrtoDatasPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter')  
↳ [ReferencedObjectPostgreSQLConverter&lt;TReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')

Implements [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlconverter 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLConverter'), [DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject](https://learn.microsoft.com/en-us/dotnet/api/digi.postgresql.interfaces.ipostgresqlobject 'DiGi\.PostgreSQL\.Interfaces\.IPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject')

<a name='DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject'></a>

## IGISPostgreSQLObject Interface

Defines the contract for a Geographic Information System \(GIS\) object that can be persisted in a PostgreSQL database\.

```csharp
public interface IGISPostgreSQLObject : DiGi.Core.Interfaces.IObject
```

Derived  
↳ [AdministrativeAreal2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DPostgreSQLConverter')  
↳ [Building2DPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.Building2DPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DPostgreSQLConverter')  
↳ [BuildingDataPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.BuildingDataPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.BuildingDataPostgreSQLConverter')  
↳ [EPWFilePostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.EPWFilePostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.EPWFilePostgreSQLConverter')  
↳ [OrtoDatasPostgreSQLConverter](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.OrtoDatasPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Classes\.OrtoDatasPostgreSQLConverter')  
↳ [PostgreSQLAdministrativeAreal2DCreateDatabaseTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateDatabaseTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DCreateDatabaseTask')  
↳ [PostgreSQLAdministrativeAreal2DCreateTableTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DCreateTableTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DCreateTableTask')  
↳ [PostgreSQLAdministrativeAreal2DRefreshTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLAdministrativeAreal2DRefreshTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLAdministrativeAreal2DRefreshTask')  
↳ [PostgreSQLBuilding2DCreateTableTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DCreateTableTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DCreateTableTask')  
↳ [PostgreSQLBuilding2DRefreshTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuilding2DRefreshTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuilding2DRefreshTask')  
↳ [PostgreSQLBuildingDataUpdateTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLBuildingDataUpdateTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLBuildingDataUpdateTask')  
↳ [PostgreSQLOrtoDatasCreateDatabaseTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasCreateDatabaseTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasCreateDatabaseTask')  
↳ [PostgreSQLOrtoDatasRefreshTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLOrtoDatasRefreshTask')  
↳ [PostgreSQLUpdateOccupancyTask](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.PostgreSQLUpdateOccupancyTask 'DiGi\.GIS\.PostgreSQL\.Classes\.PostgreSQLUpdateOccupancyTask')  
↳ [ReferencedObjectPostgreSQLConverter&lt;TReferencedObject,TUniqueObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.ReferencedObjectPostgreSQLConverter_TReferencedObject,TUniqueObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.ReferencedObjectPostgreSQLConverter\<TReferencedObject,TUniqueObject\>')  
↳ [TableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')  
↳ [IGISPostgreSQLConverter](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter')  
↳ [IGISPostgreSQLConverter&lt;TTableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLConverter_TTableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLConverter\<TTableObject\>')  
↳ [ITableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableObject')  
↳ [ITableSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject')  
↳ [ITableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')

Implements [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')

<a name='DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject'></a>

## IGISPostgreSQLSerializableObject Interface

Defines the contract for objects that are serializable and compatible with PostgreSQL GIS storage\.

```csharp
public interface IGISPostgreSQLSerializableObject : DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IObject
```

Derived  
↳ [AdministrativeAreal2DReferencePath](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReferencePath 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReferencePath')  
↳ [TableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')  
↳ [ITableSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject')  
↳ [ITableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')

Implements [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')

<a name='DiGi.GIS.PostgreSQL.Interfaces.ITableObject'></a>

## ITableObject Interface

Defines the contract for an object that can be represented as a table record within a PostgreSQL GIS database\.

```csharp
public interface ITableObject : DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject
```

Derived  
↳ [TableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')  
↳ [ITableSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject')  
↳ [ITableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')

Implements [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')

<a name='DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject'></a>

## ITableSerializableObject Interface

Defines an object that can be serialized as part of a database table\.

```csharp
public interface ITableSerializableObject : DiGi.GIS.PostgreSQL.Interfaces.ITableObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Derived  
↳ [TableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')  
↳ [ITableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>')

Implements [ITableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLSerializableObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Properties

<a name='DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject.Object'></a>

## ITableSerializableObject\.Object Property

Gets or sets the JSON representation of the object\.

```csharp
System.Text.Json.Nodes.JsonObject? Object { get; set; }
```

#### Property Value
[System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

<a name='DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_'></a>

## ITableSerializableObject\<TSerializableObject\> Interface

Defines a generic object that can be serialized as part of a database table and converted to a specific serializable object type\.

```csharp
public interface ITableSerializableObject<TSerializableObject> : DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject, DiGi.GIS.PostgreSQL.Interfaces.ITableObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject, DiGi.Core.Interfaces.IObject, DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
    where TSerializableObject : DiGi.Core.Interfaces.ISerializableObject
```
#### Type parameters

<a name='DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_.TSerializableObject'></a>

`TSerializableObject`

The type of the serializable object this interface converts to\.

Derived  
↳ [TableSerializableObject&lt;TSerializableObject&gt;](DiGi.GIS.PostgreSQL.Classes.md#DiGi.GIS.PostgreSQL.Classes.TableSerializableObject_TSerializableObject_ 'DiGi\.GIS\.PostgreSQL\.Classes\.TableSerializableObject\<TSerializableObject\>')

Implements [ITableSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject'), [ITableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableObject'), [IGISPostgreSQLObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [IGISPostgreSQLSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.IGISPostgreSQLSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.IGISPostgreSQLSerializableObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Methods

<a name='DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_.ToDiGi()'></a>

## ITableSerializableObject\<TSerializableObject\>\.ToDiGi\(\) Method

Converts the current instance to a DiGi serializable object\.

```csharp
TSerializableObject? ToDiGi();
```

#### Returns
[TSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_.TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\.TSerializableObject')  
A [TSerializableObject](DiGi.GIS.PostgreSQL.Interfaces.md#DiGi.GIS.PostgreSQL.Interfaces.ITableSerializableObject_TSerializableObject_.TSerializableObject 'DiGi\.GIS\.PostgreSQL\.Interfaces\.ITableSerializableObject\<TSerializableObject\>\.TSerializableObject') representation of the object, or null if conversion fails\.