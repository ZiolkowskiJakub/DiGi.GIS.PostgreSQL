# Implementation Plan — DiGi.GIS.PostgreSQL #82

**External Components Area: classification, computation and `building_data` update task from `BuildingModel`s**

- Issue: https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/82 (open, `type: feature`, `priority: high`, `ai: standard`)
- Parent tracking issue: DiGi.GIS.PostgreSQL #83
- Dependency DiGi.GIS.IO #11 (35 columns + `Columns_ExternalComponentsArea()`): **done**
- Downstream DiGi.GIS.PostgreSQL.UI #12 (UI wrapper): open, not in scope here
- **Status: v2 — maintainer feedback applied.** (1) The interior-point contract is now **throw-based** on the `Query.Geometry3D` → `PolygonalFace3D` → `GetInternalPoint` chain (the bbox-centre design is removed); (2) a **required** end-to-end Fact over the deployed `BuildingModel`s saved in PostgreSQL; (3) the `long` skip-count return is confirmed ("OK")

---

## 1. Validity assessment

The issue is **still valid and actionable**. None of its deliverables exist yet: `Update_ExternalComponentsArea` and `PostgreSQLBuildingDataExternalComponentsUpdateTask` have no hits in the repository, and the DiGi.GIS.IO prerequisite is complete in source.

### Premises verified against code (with evidence)

| Issue claim | Status | Evidence |
|---|---|---|
| `building_model_component` table, one JSONB row per model, keyed `(county_id, unique_id)`, index `(county_id, reference)` | ✅ | `DiGi.GIS.PostgreSQL/Classes/Converter/BuildingModelPostgreSQLConverter.cs` — `TableName => "building_model_component"`; DDL in `DiGi.GIS.PostgreSQL/Create/TableAsync.cs` (index `idx_{tableName}_county_id_reference`, deliberately non-unique) |
| Converter registered at `BuildingModelDetailLevel.Component` | ✅ | `DiGi.GIS.PostgreSQL/Create/GISPostgreSQLConverterManager.cs:62` |
| Multiple model versions per building; latest wins by `created_at DESC, id DESC` | ✅ | `DiGi.GIS.PostgreSQL/Classes/Converter/Building2DReferencedObjectPostgreSQLConverter.cs:411-420` (`ORDER BY created_at DESC, id DESC` on `GetItemsByReferencesAsync`) |
| `GetReferencesAsync(countyId)` returns distinct references only | ✅ | `Building2DReferencedObjectPostgreSQLConverter.cs:510, 553` |
| `GetItemsByReferencesAsync(references, countyId)` uses `ANY(@references)` | ✅ | `Building2DReferencedObjectPostgreSQLConverter.cs:392, 483` (also carries `fallbackByReference` — must stay `false` for this task) |
| `TableSerializableObject.ToDiGi()` rehydrates `DiGi.Analytical.Building.Classes.BuildingModel` | ✅ | `DiGi.GIS.PostgreSQL/Classes/BuildingModel.cs`, `DiGi.GIS.PostgreSQL/Classes/TableSerializableObject.cs` |
| `BuildingModel.GetComponents<IWall/IRoof/IFloor>()` exists | ✅ | reflection over `DiGi.Analytical.Building.dll`: `GetComponents``1()` (parameterless) → `List<TComponent>`; `IWall`/`IRoof`/`IFloor` all implement `IPhysicalComponent + IComponent` |
| Components expose planar `IPolygonalFace3D` geometry with `GetArea()` and a face normal | ✅ | reflection: `IPolygonalFace3D → IFace.GetArea()`, `→ IPlanar.Plane` (`Plane.Normal` → `Vector3D`); extraction helper `DiGi.Analytical.Building.Query.Geometry3D<T>(IBuildingGeometry3DObject)` (XML docs + reflection); components created via `DiGi.Analytical.Building.Create.SurfaceWall/SurfaceRoof/FaceFloor(IPolygonalFace3D, tolerance)` |
| `DiGi.GIS.Query.CardinalDirection(double)` performs the 8-sector bucketing | ✅ | `DiGi.GIS.Query.CardinalDirection(Double azimuth) → DiGi.GIS.Enums.CardinalDirection` (`Undefined, North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest`) |
| Sector edges and north wrap exactly as specified | ✅ | **behaviour sweep** of the compiled query: `22.5° → NorthEast`, `67.5° → East`, …, `337.4999° → NorthWest`, `337.5° → North`, `359.9999° → North`. Each boundary belongs to the clockwise-next sector; North = `[337.5°, 360°) ∪ [0°, 22.5°)` |
| Tilt band wording matches column descriptions | ✅ | `DiGi.GIS.IO/Constants/Column.cs`: flat “tilt below 5 degrees”; `UpTo20` “([5°, 20°])”; `Between20And45` “((20°, 45°])”; `Above45` “(> 45°)” |
| 35 target columns with `External Components Area` category | ✅ | `DiGi.GIS.IO/Create/Columns.cs` — `Create.Columns_ExternalComponentsArea()` returns exactly 35 `Column`s (8 wall + 1 flat roof + 24 tilted roof + 1 floor + 1 total), all `AreaUnit.SquareMeter`, `UnitDataType.Float` |
| Target table upserts on `(county_id, reference)` | ✅ | `DiGi.GIS.PostgreSQL/Classes/Converter/BuildingDataPostgreSQLConverter.cs` — `PrimaryKeyColumns = [CountyId, Reference]`, partitioned by `CountyId` |
| `PushAsync(table, batchSize = 1000, commandTimeout, ct)` | ✅ | `DiGi.PostgreSQL.Table/Classes/TablePostgreSQLConverter.cs:1439, 1466` |
| Reference task to mirror | ✅ | `DiGi.GIS.PostgreSQL/Classes/BackgroundTask/PostgreSQLBuildingDataUpdateTask.cs` (`ReportableBackgroundTask<long>, IGISPostgreSQLObject`, options with `CommandTimeout = 600`, `progress.Report(UpdatedRowCount)` per county, `FailedXCount == 0` success) |
| `Modify.Update_Occupancy` pattern for `Table` writes | ✅ | `DiGi.GIS.PostgreSQL/Modify/Update_Occupancy.cs` — `IO.Modify.UpdateColumn<Column>`, `Query.RowsByCountyIdAndReference`, `IO.Modify.SetValue`, `table.AddRow(row, false)`; first-record-wins dedupe via a `written` set |
| `Vector3D.Angle(other)` units | ✅ (probe) | returns **radians** (`Angle(X, Y) = π/2`) — tilt must go through `Math.ToDegrees` |
| County keying by `id`, never `code` | ✅ | `PostgreSQLBuildingDataUpdateOptions.CountyIds` doc: “Identifiers rather than codes”; matches the GIS Administrative Data guideline (18 multi-part county codes) |
| `Query.Geometry3D<T>` extraction contract | ✅ | `DiGi.Analytical.Building/Query/Geometry3D.cs` — reads `.Geometry` (or `CurveWall.GetSurface3D()`); `Geometry.Spatial.Query.Convert<T>` (`DiGi.Geometry/Spatial/Query/Convert.cs:821`) returns a **clone** when the geometry already is `T` (= `PolygonalFace3D`), wraps `IPolygonal3D` in a new `PolygonalFace3D`, else `null` → the method returns `null`; `> 1` geometry → `NotImplementedException` (their throw, propagates) |
| `IWall`/`IRoof`/`IFloor` are valid `Geometry3D<T>` receivers | ✅ | `IFloor : IPhysicalComponent : IComponent : IBuildingBoundable3D : IBuildingGeometry3DObject` (interface chain verified in `DiGi.Analytical.Building/Interfaces/`) |
| `PolygonalFace3D.GetInternalPoint` | ✅ | `PolygonalFace3D.cs:342` — `Point3D? GetInternalPoint(double tolerance = Core.Constants.Tolerance.Distance)` → `plane?.Convert(geometry2D?.GetInternalPoint(tolerance))` — **can return null** (→ throw per the contract) |
| `CurveWall` is not automatically non-planar | ⚠️ | `CurveWall.cs:130-175` — `GetSurface3D()` for a `Segment3D` base **returns a `PolygonalFace3D`** (it classifies like a planar wall); only a non-segment base is non-representable (and throws `NotImplementedException` in *their* code) — so the throw-path test needs a `CurveWall` with a null base curve, not just “a `CurveWall`” |
| DB-dependent Facts convention | ✅ | existing Facts use `[Fact(Skip = "Executes an integration query. Point GIS_PostgreSQL_Storage.conf at a database before running.")]` (e.g. `Facts/GetItemsByReferences.cs:93`, `Facts/Building2DOccupancyDataPostgreSQLConverter_Duplicates.cs:56`) |
| Dev DB wiring in Facts | ✅ | `Create.GISPostgreSQLConverterManager()` (`Create/GISPostgreSQLConverterManager.cs`) reads `PostgreSQL_Storage`/`PostgreSQL_Main` confs from the executing assembly’s directory; `user files/GIS_PostgreSQL_Storage.conf` (git-ignored) carries the dev connection |
| Test fixture construction | ✅ | `BuildingModel.Update(IComponent?)` (`BuildingModel.cs:1379`); `Create.SurfaceWall/SurfaceRoof/FaceFloor(IPolygonalFace3D?, tolerance)` — each requires area ≥ tolerance, else returns `null` |

### Two design decisions the issue leaves open — maintainer ruled on both

1. **The “internal point of the building”** used to orient wall normals outward. No existing API returns one. **Maintainer’s contract (binding):** extract the component’s geometry with `DiGi.Analytical.Building.Query.Geometry3D`; the result **must be a `PolygonalFace3D`** — then `GetInternalPoint()`; **otherwise throw** (no silent skip, no bbox-centre fallback).
   - Concretely: the interior reference `Q` is the **floor’s internal point** — the model’s first `IFloor` goes through that chain (`Query.Geometry3D<PolygonalFace3D>(floor)` → `GetInternalPoint()`), because a floor face’s internal point lies inside the building volume (also correct for concave/L-shaped footprints, where a bbox centre can sit in the notch). A model without a floor, a non-planar floor face, or a null internal point is a **data defect → throw**.
   - The wall’s own face point `P` uses the identical chain on the wall component (same throw contract).
   - Roofs and floors still need a `PolygonalFace3D` for their normal/area — the same “otherwise throw” applies to the extraction step.
   - The throw is deliberate: a component the method cannot classify is a defect in the stored model and must surface as a visible county failure, not a silently missing area. The required end-to-end Fact (§5.2, Fact 9) proves the deployed models do not hit the throw path.
2. **Plumbing of the skip count from the `Modify` method to the task.** The issue’s signature shows `void`, but its counter list requires the count. Decision — **maintainer-confirmed (“OK”)**: the method **returns `long`** (the number of skipped components) instead of `void`.
   - Note the skip set shrank with decision 1: non-planar components no longer skip — they throw. What remains skippable (geometrically valid components whose classification target is undefined): a wall with a (near-)vertical normal (azimuth undefined), a wall whose orientation test is degenerate (`|dot| ≈ 0`), and `CardinalDirection → Undefined` (defensive).

---

## 2. Scope

**In scope (this issue):**

- `DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs` — classification + `Table` population.
- `DiGi.GIS.PostgreSQL/Classes/Options/PostgreSQLBuildingDataExternalComponentsUpdateOptions.cs`
- `DiGi.GIS.PostgreSQL/Classes/BackgroundTask/PostgreSQLBuildingDataExternalComponentsUpdateTask.cs`
- Facts in `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/`.

**Out of scope:**

- Column definitions (DiGi.GIS.IO #11, done), UI wrapper (DiGi.GIS.PostgreSQL.UI #12), any WebAPI surface, net-of-openings areas (explicit follow-up per #83).
- `BuildingDataUpdateType` / `PostgreSQLBuildingDataUpdateTask` — **not touched** (standalone task; a model-less county must not fail the shared task’s success condition).

---

## 3. Part 1 — `Modify.Update_ExternalComponentsArea`

### 3.1 File and signature

- File: `DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs`
- `namespace DiGi.GIS.PostgreSQL`, `public static partial class Modify` (same convention as `Update_Occupancy.cs`).
- Signature (deviation from the issue’s `void` marked in §1):

```cs
public static long Update_ExternalComponentsArea(this Table? table, IEnumerable<BuildingModel>? buildingModels)
```

- Receiver is `Table?` with a guard, matching every other `Modify` in this codebase.
- `BuildingModel` here is the **storage envelope** `DiGi.GIS.PostgreSQL.Classes.BuildingModel`, so `CountyId` and `Reference` come from storage — no client-supplied county resolution.
- `<summary>` XML docs on the method (and `<returns>`), English only, `<param>` order mirrors the signature.

### 3.2 Column preparation

Resolve all 37 columns once, in a stable order, before the building loop:

```cs
Column? column_CountyId = IO.Modify.UpdateColumn(table, IO.Constants.Column.CountyId);
Column? column_Reference = IO.Modify.UpdateColumn(table, IO.Constants.Column.Reference);
List<Column> columns_External = [.. IO.Create.Columns_ExternalComponentsArea()];
// each: IO.Modify.UpdateColumn(table, column)
```

`IO.Modify.UpdateColumn<TColumn>` (in `DiGi.GIS.IO/Modify/UpdateColumn.cs`) updates an existing column or adds it — the same mechanism the existing `Update_*` methods use, so the pushed `Table` carries the 35 new columns and `PushAsync` writes only the columns present in the table.

Keep a lookup `Dictionary<string, Column>` keyed by `column.UniqueId()` (the slug, e.g. `external_north_wall_area`) so bucket accumulation addresses columns without re-lookup.

### 3.3 Per-building algorithm

For each envelope (the caller passes records in preference order; the method also dedupes defensively, exactly like `Update_Occupancy`’s `written` set):

1. Skip (no count) envelopes with blank `Reference` or null `CountyId` — same guard shape as `Update_Occupancy`.
2. Dedupe on `(countyId, reference)`: first record wins. The converter orders results `created_at DESC, id DESC`, so the first per reference is the latest model.
3. `DiGi.Analytical.Building.Classes.BuildingModel? buildingModel = envelope.ToDiGi();` — null → **throw** `InvalidOperationException` (an envelope exists in `building_model_component` but its payload does not rehydrate — a data defect, surfaced the same way as a bad face, never as a silent missing row).
4. **Fetch each component list exactly once** per model:
   `List<IWall>? walls = buildingModel.GetComponents<IWall>();` and likewise `IRoof`, `IFloor`.
   The XML docs warn `GetComponents` hands out **clones** — reaching the same components through the model twice pays for them twice.
5. **Interior reference point `Q`** (maintainer’s contract, §1): the model’s first `IFloor`, through the face chain:
   - `List<IFloor>? floors = buildingModel.GetComponents<IFloor>();` — null/empty → **throw** `InvalidOperationException` (no interior reference obtainable — data defect).
   - `PolygonalFace3D? floorFace = Query.Geometry3D<PolygonalFace3D>(floors[0]);` — null → **throw** (non-planar floor).
   - `Point3D? q = floorFace.GetInternalPoint();` — null → **throw** (degenerate floor).
   `Q` is computed **once per model**; every wall’s orientation test reuses it. (Roofs and floors do not consume it.)
6. Classify each component (below) into a `double` accumulator per bucket:
   `Dictionary<Column, double> areas_ByColumn` (or an aligned `double[]` over `columns_External`).
7. Write the row:
   - reuse the row from `Query.RowsByCountyIdAndReference(table, column_CountyId, column_Reference)` when the key already exists, else `table.AddRow()`;
   - `IO.Modify.SetValue(row, column_CountyId, countyId)`, `IO.Modify.SetValue(row, column_Reference, reference)`;
   - for **every** one of the 35 columns: `IO.Modify.SetValue(row, column, area is double value and value > 0 ? (float)value : 0f)` — a building **with** a stored model gets `0f` in empty buckets (0-vs-NULL semantics);
   - total column `ExternalComponentsArea` = the sum of the 34 breakdown accumulators (computed from the accumulators, never re-measured);
   - `table.AddRow(row, false)` (re-add existing row, per `Update_Occupancy`).
8. Return the total skip count accumulated across all components.

`IO.Modify.SetValue` validates through `column.TryGetValidValue`, so the column’s `float` type is enforced at the write boundary; cast to `float` explicitly anyway (areas are computed as `double`).

### 3.4 Classification rules (single source of truth per rule)

| Component type | Selection | Bucket |
|---|---|---|
| `IWall` (planar face, else throw — below) | azimuth of the **outward** normal’s horizontal projection → `GIS.Query.CardinalDirection(azimuth)` | `External{North/Northeast/East/Southeast/South/Southwest/West/Northwest}WallArea` |
| `IRoof` (planar face, else throw) | tilt < 5° | `ExternalFlatRoofArea` (no directional split) |
| `IRoof` (planar face, else throw) | 5° ≤ tilt ≤ 20° | `External{Direction}TiltedRoofAreaUpTo20` |
| `IRoof` (planar face, else throw) | 20° < tilt ≤ 45° | `External{Direction}TiltedRoofAreaBetween20And45` |
| `IRoof` (planar face, else throw) | tilt > 45° | `External{Direction}TiltedRoofAreaAbove45` |
| `IFloor` (planar face, else throw) | — (no split) | `ExternalFloorArea` |
| any | — | `ExternalComponentsArea` = sum of the 34 above |

Constants: `TiltFlatDegrees = 5.0`, `TiltUpTo20Degrees = 20.0`, `TiltBetween45Degrees = 45.0` as `const double` in this file, with `<summary>`s stating they must match the DiGi.GIS.IO column descriptions. Boundary ownership is exactly the issue’s: flat is strictly below 5°; `[5°, 20°]`; `(20°, 45°]`; `> 45°`.

**Azimuth** (clockwise from north of the horizontal projection): `double azimuth = (Math.Atan2(normal.X, normal.Y) * 180.0 / Math.PI + 360.0) % 360.0;` — `Math.Atan2(X, Y)` (east, north) gives 0° at north, 90° at east, matching the verified `CardinalDirection` sweep. Do **not** re-implement the bucketing: `GIS.Query.CardinalDirection(azimuth)` decides the sector (its `Undefined` answer is a skip, see below).

**Tilt** (degrees, from radians — verified by probe): `double tilt = Math.ToDegrees(normal.Angle(Vector3D worldZ))` with `worldZ = new(0, 0, 1)` (equivalently `Plane.WorldZ.Normal`).

**Face extraction — the maintainer’s contract (one code path for every component):**

```cs
PolygonalFace3D? face = Query.Geometry3D<PolygonalFace3D>(component);
if (face is null)
{
    throw new InvalidOperationException($"{component}: does not yield a PolygonalFace3D");
}
```

- `Query.Geometry3D<PolygonalFace3D>` returns a **clone** of the face when the component’s geometry is planar (verified: `Convert<PolygonalFace3D>` at Convert.cs:821), `null` when it is not, and throws `NotImplementedException` itself when the component carries more than one geometry (their throw — let it propagate, same class of failure).
- The message must identify the component (its `Guid` — `IComponent : … : IBuildingGuidObject`) and the envelope’s `Reference`, so a failed county pinpoints the offending row in `building_model_component`.
- **No `CurveWall` special-casing**: for a `Segment3D` base its `GetSurface3D()` already returns a `PolygonalFace3D` (CurveWall.cs:130-175), so it classifies like any planar wall; only a genuinely non-representable geometry reaches the throw.

**Normal orientation first, classification second:**

- **Walls — outward.** `Point3D? p = face.GetInternalPoint();` — null → **throw** (same contract). `interior = q - p` where `q` is the model’s floor internal point (§3.3 step 5). If `normal.DotProduct(interior) < 0` the stored normal already points away from the interior; otherwise use `normal.GetInversed()`. `|dot| ≈ 0` (floor point coplanar with the wall face — orientation genuinely ambiguous) → **skip + count** (a guess at 180° ambiguity is worse than a counted skip).
- **Roofs — upward.** If `normal.Z < 0` use `normal.GetInversed()`. After this, tilt ∈ [0°, 90°] and the flat/tilted split is well defined; a tilted roof (tilt ≥ 5°) always has a non-zero horizontal normal component, so its sector is always defined.
- **Floors** need no orientation and no internal point (area only).

**Throw (data defect — surfaces as a county failure in the task, never silent):**

- `envelope.ToDiGi()` returns null (payload does not rehydrate);
- the model has no `IFloor` (no interior reference `Q`);
- `Query.Geometry3D<PolygonalFace3D>(component)` returns null (non-planar component);
- `face.GetInternalPoint()` returns null (degenerate face) — for walls and for the floor used to obtain `Q`;
- `face.GetArea()` is `NaN` or `≤ 0`.

**Skip (count in the returned `long`, never throw) — geometrically valid components whose target bucket is undefined:**

- wall whose normal’s horizontal projection has (near-)zero length (vertical normal — azimuth undefined);
- wall whose orientation test is degenerate (`|dot(normal, q − p)| ≈ 0`);
- `GIS.Query.CardinalDirection(azimuth)` returns `Undefined` (defensive — a finite azimuth cannot produce it in the verified sweep).

**Gross areas:** openings are not subtracted — the component face area is the whole area by construction.

### 3.5 0 vs NULL

- Building **with** a stored model → a row is written with `0f` in every empty bucket; `ExternalComponentsArea` = sum of the 34 (so an all-skipped model writes 35 zeros — still distinguishable from `NULL`).
- Building **without** a stored model (no envelope in `building_model_component`) → no row is written; the columns stay `NULL` in `building_data`.
- A model that **cannot be classified** (throw, §3.4) → no row for it; in the task the whole county fails visibly (`FailedCountyCount++`), so a data defect can never masquerade as a zero row.
- `floor_area` / `total_area` (footprint-based) are never touched: this method only addresses the 37 columns above, and `PushAsync` writes only the columns present in the pushed table.

---

## 4. Part 2 — `PostgreSQLBuildingDataExternalComponentsUpdateTask`

### 4.1 Options

`DiGi.GIS.PostgreSQL/Classes/Options/PostgreSQLBuildingDataExternalComponentsUpdateOptions.cs` — `SerializableOptions` pattern, mirroring `PostgreSQLBuildingDataUpdateOptions`:

```cs
public class PostgreSQLBuildingDataExternalComponentsUpdateOptions : SerializableOptions
{
    public PostgreSQLBuildingDataExternalComponentsUpdateOptions(JsonObject jsonObject) : base(jsonObject) { }
    public PostgreSQLBuildingDataExternalComponentsUpdateOptions() : base() { }
    public PostgreSQLBuildingDataExternalComponentsUpdateOptions(PostgreSQLBuildingDataExternalComponentsUpdateOptions options) : base(options) { /* copy */ }

    [JsonInclude, JsonPropertyName(nameof(CountyIds))]
    public HashSet<int>? CountyIds { get; set; } = null;          // null = every county

    [JsonInclude, JsonPropertyName(nameof(CommandTimeout))]
    public int CommandTimeout { get; set; } = 600;
}
```

`CountyIds` carries **identifiers, not codes** (the GIS Administrative Data rule), with the same `<summary>` reasoning as the existing options.

### 4.2 Task

`DiGi.GIS.PostgreSQL/Classes/BackgroundTask/PostgreSQLBuildingDataExternalComponentsUpdateTask.cs`:

```cs
public class PostgreSQLBuildingDataExternalComponentsUpdateTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
{
    protected readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

    public PostgreSQLBuildingDataExternalComponentsUpdateOptions Options { get; set; } = new();

    public PostgreSQLBuildingDataExternalComponentsUpdateTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        : base() { gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager)); }

    public long ProcessedCountyCount { get; private set; }
    public long FailedCountyCount { get; private set; }
    public long ProcessedModelCount { get; private set; }
    public long SkippedComponentCount { get; private set; }
    public long UpdatedRowCount { get; private set; }

    protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken) { ... }
}
```

Every counter has a `<summary>` in the style of the reference task (what it counts, and what it does *not* cover).

### 4.3 `ExecuteAsync` flow

1. Reset all counters; `Options ??= new();`.
2. Resolve converters via `gISPostgreSQLConverterManager.GetPostgreSQLConverter<T>()`:
   - `BuildingModelPostgreSQLConverter` — null → log error, return `false` (nothing to read);
   - `BuildingDataPostgreSQLConverter` — null → log error, return `false` (nowhere to write);
   - `AdministrativeAreal2DPostgreSQLConverter` — null → log error, return `false` (cannot scope the run).
3. `int commandTimeout = Options.CommandTimeout;`
4. Counties: `GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(AdministrativeArealType.County, commandTimeout: commandTimeout, cancellationToken: cancellationToken)`; null/empty → log error, return `false`. Filter: keep the county reference when `Options.CountyIds is null || Options.CountyIds.Contains(reference.Id)`. (`reference.Id` is the county row identifier — never the code.)
5. Per county (top of loop: `cancellationToken.ThrowIfCancellationRequested();`):
   1. `HashSet<string>? references = await buildingModelPostgreSQLConverter.GetReferencesAsync(countyId, commandTimeout: commandTimeout, cancellationToken: cancellationToken);`
      - null → `FailedCountyCount++`, log with exception, `continue`.
      - empty → `ProcessedCountyCount++`, log the county as model-less, `continue` (buildings there keep `NULL`; this must **not** count as a failure).
   2. Build one `Table table = new();` for the county; `long countySkippedComponents = 0;`.
   3. Batch the references in **1000** (`const int batchSize = 1000;`) — the guideline standard; per batch:
      - `List<BuildingModel>? items = await buildingModelPostgreSQLConverter.GetItemsByReferencesAsync(batch, countyId, fallbackByReference: false, commandTimeout: commandTimeout, cancellationToken: cancellationToken);`
      - `fallbackByReference: false` is load-bearing: a reference the county does not hold must not be answered from another county, or the row would be filed under a county this run is not processing (same reasoning the reference task gives for occupancy).
      - null → `FailedCountyCount++`, log, `continue` (next county).
      - dedupe to the latest per `Reference` (first wins — the read is already `created_at DESC, id DESC`); `ProcessedModelCount += deduped.Count`.
      - `countySkippedComponents += Modify.Update_ExternalComponentsArea(table, deduped);`
        - the method **throws on a data defect** (§3.4: non-rehydrating payload, missing floor, non-planar face, null internal point, bad area) — catch it: log the full exception (its message names the component and the reference), `FailedCountyCount++`, `continue` to the next county. One bad model fails its county, never the whole run — and `FailedCountyCount == 0` still makes it a failed run.
   4. `SkippedComponentCount += countySkippedComponents;`
   5. If `table.RowCount > 0`:
      - `bool updated = await buildingDataPostgreSQLConverter.PushAsync(table, commandTimeout: commandTimeout, cancellationToken: cancellationToken);`
      - `OperationCanceledException when (cancellationToken.IsCancellationRequested)` → rethrow (same catch shape as the reference task).
      - other `Exception` → `FailedCountyCount++`, log, `continue`.
      - `!updated` → `FailedCountyCount++`, log the rollback, `continue`.
      - `UpdatedRowCount += table.RowCount; progress.Report(UpdatedRowCount);`
   6. `ProcessedCountyCount++;`
6. Final summary log (processed/failed counties, models, skipped components, rows), then:

```cs
return FailedCountyCount == 0;
```

**Idempotency** falls out of the design: the read is deterministic (latest model wins), the computation is pure, and the push upserts on `(county_id, reference)` — a re-run writes the same 35 values.

**Cancellation & timeout discipline** (PostgreSQL guideline): `ThrowIfCancellationRequested()` at the top of every loop level (county, batch); `commandTimeout` named-passed to every converter call; `cancellationToken` passed to every async call; the task never swallows `OperationCanceledException` under a requested cancellation.

---

## 5. Tests — `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit`

One file per concern under `Facts/`, all as members of the existing `public partial class Facts` (namespace `DiGi.GIS.PostgreSQL.xUnit`, `[Fact]`, XML `<summary>` on every method, **no** `using Xunit` — it is a global using).

### 5.1 Synthetic component fixture (private helpers in the Facts partial class)

Construction chain (all confirmed by reflection/source):

```cs
PolygonalFace3D face3D = new(
    new Plane(new Point3D(0, 0, 0), normal3D),              // Plane(Point3D, Vector3D)
    new PolygonalFace2D(new Polygon2D([p1, p2, p3, p4])));   // unit square in the face plane
SurfaceWall wall = DiGi.Analytical.Building.Create.SurfaceWall(face3D, Core.Constants.Tolerance.Distance);
SurfaceRoof roof = DiGi.Analytical.Building.Create.SurfaceRoof(face3D, Core.Constants.Tolerance.Distance);
FaceFloor floor = DiGi.Analytical.Building.Create.FaceFloor(face3D, Core.Constants.Tolerance.Distance);
DiGi.Analytical.Building.Classes.BuildingModel buildingModel = new();
Assert.True(buildingModel.Update(component));
```

**Every synthetic model must carry a floor** — the method throws when the model has no `IFloor` (§3.4), so the fixture always adds a horizontal `FaceFloor` (normal `±Z`, at `z = 0`) alongside the walls/roofs under test. Walls are vertical faces (horizontal normal at the test azimuth); the floor’s internal point then lies inside the footprint and the orientation test is well defined.

Then wrap in the storage envelope the way `ToPostgreSQL_BuildingModel` does (Reference + CountyId), or build the envelope directly from the serialized object.

### 5.2 Facts

1. `Update_ExternalComponentsArea_WallSectorBoundaries` — synthetic walls at 22.5°, 67.5°, 112.5°, 157.5°, 202.5°, 247.5°, 292.5°, 337.5°, plus 350° and 10° (north wrap) and 22.4999°/22.5001° pairs. Assert each lands in exactly the expected `External*WallArea` column and nowhere else; the returned skip count is 0.
2. `Update_ExternalComponentsArea_RoofTiltBands` — roofs at 4.9999° (→ flat), 5° (→ UpTo20), 20° (→ UpTo20), 20.0001° (→ Between20And45), 45° (→ Between20And45), 45.0001° (→ Above45), at a fixed azimuth (e.g. east) so the sector is constant.
3. `Update_ExternalComponentsArea_FlatRoofRegardlessOfAzimuth` — a 4° roof facing east lands in `ExternalFlatRoofArea` only.
4. `Update_ExternalComponentsArea_FloorOnly` — floors accumulate into `ExternalFloorArea` only, no directional or tilt leakage.
5. `Update_ExternalComponentsArea_TotalEqualsSumOfBreakdowns` — a model with walls, roofs and floors: `ExternalComponentsArea` equals the sum of the 34 breakdown columns (and equals the sum of all non-zero areas written).
6. `Update_ExternalComponentsArea_NonPlanarThrows` — a model whose wall is a `CurveWall` with a **null base curve** (so `GetSurface3D()` → null and `Query.Geometry3D<PolygonalFace3D>` → null): `Assert.Throws<InvalidOperationException>`, and the message names the component. A model **without a floor** throws the same way (no interior reference `Q`). *(Construction note: a `CurveWall` over a `Segment3D` is planar — CurveWall.cs:130-175 — so the null-curve form is the reliable throw case; if `BuildingModel.Update` ever refuses it, the assertion stays “throws, does not silently skip” with a non-segment base curve.)*
7. `Update_ExternalComponentsArea_LatestModelWins` — two envelopes with the same `(countyId, reference)` (different model contents, latest first): only the first’s areas are written; a second envelope with a different reference still gets its own row.
8. `Update_ExternalComponentsArea_ZeroVsNull` — (a) a model with a floor but no walls or roofs writes `0f` to all 8 wall and 25 roof columns while `ExternalFloorArea` > 0 and the total equals the floor area; (b) an envelope with blank `Reference` (or null `CountyId`) is skipped with no row and no count (the §3.3 step 1 guard).
9. `Update_ExternalComponentsArea_DeployedBuildingModels` — **required by the maintainer**: proves the throw-based face/internal-point contract (§3.4) holds for the **currently deployed `BuildingModel`s saved in PostgreSQL** (the dev database behind `GIS_PostgreSQL_Storage.conf`, named in the `<summary>`).
   - Follows the project’s integration-Fact convention: `[Fact(Skip = "Runs against the deployed BuildingModels. Point GIS_PostgreSQL_Storage.conf at a database before running.")]` — same shape as `Facts/GetItemsByReferences.cs:93`; the suite stays green on machines without the conf, and running the fact is a deliberate act with the dev conf present.
   - Flow: `Create.GISPostgreSQLConverterManager()` → `BuildingModelPostgreSQLConverter` → one county (first county reference from `GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(County)` that carries references) → `GetReferencesAsync(countyId)` → `GetItemsByReferencesAsync(batch, countyId, fallbackByReference: false, …)` → dedupe latest per reference → **`Modify.Update_ExternalComponentsArea(table, models)` on a fresh in-memory `Table`** — a read-only fact: no `PushAsync`, no write to the dev database.
   - Assert: the call **does not throw** (every deployed component is a `PolygonalFace3D` with a resolvable internal point and every model carries a floor — the maintainer’s contract holds on real data); the returned skip count is **0** (if it is not, that is a data finding — which components, in which county — to report before deciding whether those cases are acceptable); per row, `ExternalComponentsArea` equals the sum of the 34 breakdowns (float-tolerant); at least one row has `ExternalFloorArea > 0` and at least one row has a wall or roof bucket `> 0` (classification actually ran, not merely no-throw).
   - The write path (`PushAsync` upsert, idempotent re-run) is covered by design (pure computation + upsert on `(county_id, reference)`) and by the existing `BuildingDataPostgreSQLConverter` Facts — this fact stays read-only against the dev database.

### 5.3 Test execution discipline

- **Build the library first** — the xUnit project reaches the code under test through `HintPath` references, so `dotnet test` would otherwise run against the last built binaries:
  ```powershell
  dotnet build "DiGi.GIS.PostgreSQL\DiGi.GIS.PostgreSQL.csproj" -c Debug -m:1
  dotnet test "..\DiGi.Test\DiGi.GIS.PostgreSQL.xUnit\DiGi.GIS.PostgreSQL.xUnit.csproj" -c Debug -m:1
  ```
- Boundary values are tested exactly at the threshold and on both sides (the Automatic Tests guideline).
- Zero compiler/analyzer warnings in both projects.

---

## 6. Guideline compliance map

| Guideline | How the plan honours it |
|---|---|
| Coding - General §1 (naming/typing) | explicit typing, `camelCase` with type-name prefixes (`column_CountyId`, `buildingModels`), plural collections (`references`, `items`), block-scoped namespaces, no `var`, `[]` collection expressions, target-typed `new()` |
| Coding - General §1.7/1.8 (async/`CancellationToken`) | `ExecuteAsync` is the only async entry; `CancellationToken` last in every signature; named pass `cancellationToken:` at call sites; `commandTimeout` before the token |
| Coding - General §2 (anemic + static extensions) | logic lives in static `Modify`/`Query` partial classes; the task is the one permitted background-task class (mirrors the existing `PostgreSQLBuildingDataUpdateTask`); no new service/manager types |
| Coding - General §1.12 (temporary code) | no `TODO [Marker]` code expected; the throw-based face contract is permanent by maintainer decision, not a workaround — no marker |
| Coding - General §1.16 (options defaults are the contract) | `CommandTimeout = 600` and `CountyIds = null` (= all counties) are the contract wherever no options file is read; both documented in the `<summary>`s |
| Coding - PostgreSQL §1 (converter pattern) | reuses `BuildingModelPostgreSQLConverter` / `BuildingDataPostgreSQLConverter`; no new DDL (columns materialise on first push via `PushAsync`) |
| Coding - PostgreSQL §3 (batching/timeouts) | `batchSize = 1000` for reference batches; `ANY(@references)` via the existing converter; `commandTimeout` (default 600) on every converter call; no per-item queries in loops |
| Coding - PostgreSQL §4 (resources) | no raw `NpgsqlCommand` in this change — all statements live in the existing, already-compliant converters |
| Coding - GIS Administrative Data | county scoping by `CountyIds` **identifiers**; `CountyIds` `<summary>` repeats the code-vs-id warning; no `LIMIT`/`FirstOrDefault` over `administrative_areal_2d` without ordering (the converter query already orders) |
| Coding - References | `Reference` here is a plain `string` property on the envelope (not `IReference`), so string comparison/dictionary keying is correct; no `==` on interface-typed references anywhere in the change |
| Coding - Automatic Tests | Facts structure per §2; boundary tests per §4; the DB Fact uses the project’s established `[Fact(Skip = "…GIS_PostgreSQL_Storage.conf…")]` convention for integration facts (self-describing `<summary>` naming the dev database) — same shape as `Facts/GetItemsByReferences.cs:93`; build-before-test per §4; reports (if any) to `DiGi.Test/user files/reports/` |
| XML Documentation | `<summary>` (+ `<param>`/`<returns>`) on every new public member and every Fact; no `CS1591` (suppressed repo-wide, but docs are written anyway per the codebase convention) |
| Coding - Editor Config | block namespaces, no `var`, collection expressions, expression-bodied properties, CRLF preserved (edits via `edit_file_tool`, no scripted line rewrites) |

---

## 7. Risks & open questions

1. **Interior reference from the floor (maintainer’s contract).** A floor face’s internal point is inside the building volume for well-formed models — the right proxy for “the building interior”, and robust for concave footprints where a bbox centre can sit in the notch. Cost: a model without a usable floor now **throws** and fails its county — deliberate, per the contract. The required Fact 9 proves the deployed models all carry a usable floor; if a future pipeline emits floor-less models, the failure is loud and pinpointed (component `Guid` + `Reference` in the message), and the fix is upstream, not a silent guess here.
2. **Throw granularity is per-county.** A single bad model in a county fails that county’s rows; the run continues with the other counties and the success condition stays `FailedCountyCount == 0`. Finer granularity (per-model isolation with partial rows) was considered and rejected: it would hide defects behind partial rows and double the error surface — a visible county failure is what the maintainer asked for.
3. **Float precision.** Columns are `float` (`real`); areas are computed as `double` and cast once at the write. Sums of large areas (hundreds of thousands of m²) stay within `float` relative precision for reporting purposes; the total is derived from the same accumulators, so the invariant “total = sum of breakdowns” holds in the stored values up to the same rounding.
4. **`GetComponents` clone cost** — fetch each of the three lists once per model (the XML docs call out the double-fetch cost); the batch size (1000 references) bounds memory.
5. **Large model payloads.** `building_model_component` rows are JSONB with full component shells; reading 1000 references at a time with `commandTimeout = 600` mirrors the reference task’s bulk handling. A county with no models costs one cheap `GetReferencesAsync`.
6. **Upstream dependency.** The DiGi.GIS.IO column set is complete in source; if #11’s branch is not yet merged into the `DiGi.GIS.IO\bin` this repository builds against, the first build of this change fails on the missing `Column.External*` fields — build `DiGi.GIS.IO` first (the HintPath rule applies to it too).
7. **UI follow-up.** DiGi.GIS.PostgreSQL.UI #12 registers the task in `VisualBackgroundTasks.cs`; nothing in this change blocks it — the task type, options type and counters are its contract.

---

## 8. Execution order

1. `Modify/Update_ExternalComponentsArea.cs` + Facts 1–8 → build `DiGi.GIS.PostgreSQL` → run Facts (isolated where boundary-sensitive).
2. `Options/PostgreSQLBuildingDataExternalComponentsUpdateOptions.cs`.
3. `Classes/BackgroundTask/PostgreSQLBuildingDataExternalComponentsUpdateTask.cs`.
4. Fact 9 (the maintainer-required end-to-end over the deployed `BuildingModel`s) — run with the dev database via `GIS_PostgreSQL_Storage.conf`; verify: no throw, skip count 0, `ExternalComponentsArea` = sum of the 34 per row, floor and wall/roof buckets populated. Read-only against the dev DB (no push).
5. Full solution build, zero warnings; Facts green.
6. Commit; update #82 checkboxes; leave #83 tracking-issue status to the maintainer.
