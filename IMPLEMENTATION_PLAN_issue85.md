# Implementation Plan — Issue #85: External Components Area open-envelope signal

- Issue: [ZiolkowskiJakub/DiGi.GIS.PostgreSQL#85](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/85) — open, `type: enhancement`, `priority: medium`, `ai: standard`, no `blocked_by` dependencies.
- Follow-up to: #84 (closed by `e19289e` on `0.8.10`; the deferral of this signal is recorded in `IMPLEMENTATION_PLAN_issue84.md` §8 "Deferred").
- Date: 2026-09-21 · Branch: `0.8.10` (current).
- Scope decision (user, 2026-09-21): surface form **(a) + (b) combined** — the per-model `ClosingTolerance` column in `building_data` **and** the open-envelope count in the `Modify` result, the task counter and the per-county/final logs. The DiGi.GIS.IO column lands as a **direct commit** in that repository.

## 1. Verified premises

| #85 claim | Status | Evidence |
|---|---|---|
| `Modify.Update_ExternalComponentsArea` returns `long` (skip count only); the shell is built and its closeness discarded | ✅ | `DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs:23` (signature), `:172` (`GetExternalShell(Side.External)`), `:339` (`return skippedComponentCount`) |
| `DiGi.Geometry.Spatial.Query.ClosingTolerance(shell, tolerances)` is available for that shell | ✅ | `DiGi.Geometry/DiGi.Geometry/Spatial/Query/ClosingTolerance.cs:20` — `ClosingTolerance<TPolygonalFace3D>(this Polyhedron<TPolygonalFace3D>?, IEnumerable<double>?, bool manifold = false)`; `DiGi.Analytical/Classes/AnalyticalGeometry/Shell.cs:13` — `Shell : Polyhedron<Face>`, so the extension applies unchanged |
| 6 of 27 envelopes on the #84 sample are open over the ladder `1e-6 … 0.2` | ✅ | `IMPLEMENTATION_PLAN_issue84.md` §8 "Deployed-model parity" — measured, closed ones close at 1e-6 (most), 0.001, 0.01, 0.02, 0.05 |
| The Result shape is guideline-conformant | ✅ | `Coding - General.md` §"Result Type Naming" (`IResult` implementations end in `Result`); 19 in-repo precedents in `DiGi.GIS.PostgreSQL/Classes/Result/` (e.g. `PostgreSQLOrtoDatasRefreshResult.cs`) |
| A new column reaches `building_data` without manual DDL | ✅ | `DiGi.PostgreSQL.Table/Classes/TablePostgreSQLConverter.cs:1578` (push → `CreateTableAsync`) → `DiGi.PostgreSQL.Table/Create/TableAsync.cs:257` (`ADD COLUMN IF NOT EXISTS`) |
| Nothing else records or logs openness today | ✅ | `PostgreSQLBuildingDataExternalComponentsUpdateTask.cs:293` (per-county log) and `:296-303` (final log) carry only the skip count |

## 2. Design decisions

1. **Ladder** — the #84-verification ladder, with the canonical `DiGi.Core.Constants.Tolerance` rungs where they exist (`Distance` = 1e-6, `MacroDistance` = 1e-3; ascending order proven by `DiGi.Analytical.Building/Query/IsEnclosed.cs:52`): `[Distance, 1e-5, 1e-4, MacroDistance, 0.01, 0.02, 0.05, 0.1, 0.2]`. Method-local array (allocated once per call, not per model).
2. **Manifold criterion left default (`false`)** — ray parity needs an edge-welded watertight face set, not manifoldness; the default criterion is monotone and bisected (≈ 4 `IsClosed` passes per model — `Polyhedron_IsClosed_Monotonicity.cs:181-192`).
3. **NULL semantics** — `ClosingTolerance` NULL means *no closed-envelope signal*: the envelope is open at every rung, **or** the model produced no envelope (no components → zero row; only internal partitions → zero row). Zero rows carry no orientation-dependent values, so the conflation is safe and is stated verbatim in the column description.
4. **Counting** — `OpenEnvelopeCount` increments only for a **non-null shell that fails to close**; a null shell is "no envelope", not "open".
5. **`Columns_ExternalComponentsArea()` untouched** — the 35-column contract and the index arithmetic (`Update_ExternalComponentsArea.cs:115-125`, `index_FlatRoof = 8`, `index_Floor = count_Breakdown - 1`, total last) stay intact; the new column is a separate `Constants.Column` member added to the table by the `Modify` itself.
6. **`ReportableBackgroundTask<long>` type parameter unchanged** — `long` is the progress type; only the local result variable, the new counter and the log lines change.

## 3. Part 1 — DiGi.GIS.IO `ClosingTolerance` column

`DiGi.GIS.IO/Constants/Column.cs`, one member (pattern of the neighbouring `UnitColumn`s, unit of `BoundingBoxHeight` = `Unit.Enums.LengthUnit.Meter`):

```csharp
public static UnitColumn ClosingTolerance = new("Closing tolerance", (Unit.Classes.Unit?)Unit.Enums.LengthUnit.Meter, Category.ExternalComponentsArea.Description(), "Finest distance tolerance at which the model's external envelope edge-pairs into a closed surface, over the candidate ladder 1e-6 to 0.2 m; NULL when the envelope does not close on the ladder or the model carries no external components - the signal that this row's sector and tilt values may rest on an arbitrary face side", Unit.Enums.UnitDataType.Float);
```

- Worded as the **finest** closing rung, not the coarsest: the default closure criterion is monotone in tolerance (closed at a finer rung implies closed at every coarser one), so `Query.ClosingTolerance` answers the finest candidate that closes - the informative end of the ladder. Corrected after reading the query's own contract (`DiGi.Geometry/Spatial/Query/ClosingTolerance.cs`); the column and its description landed the coarsest wording first and were fixed in a follow-up DiGi.GIS.IO commit.

- Not added to `Create.Columns_ExternalComponentsArea()` (`DiGi.GIS.IO/Create/Columns.cs:164`).
- Build → `documentation/API/DiGi.GIS.IO/…` regenerates → commit on the current DiGi.GIS.IO SemVer branch, message referencing #85.

## 4. Part 2 — `ExternalComponentsAreaResult` (new)

`DiGi.GIS.PostgreSQL/Classes/Result/ExternalComponentsAreaResult.cs`, following `PostgreSQLOrtoDatasRefreshResult.cs` member-for-member (JsonInclude private readonly fields, main/copy/`JsonObject` constructors, `[JsonIgnore]` get-only properties, XML docs on all):

```csharp
public class ExternalComponentsAreaResult : SerializableResult, IGISPostgreSQLSerializableObject
```

- Fields: `skippedComponentCount` (`long`), `openEnvelopeCount` (`long`) — alphabetical, constructor `(long skippedComponentCount, long openEnvelopeCount)`.
- `<summary>`: the outcome of one classification run; `SkippedComponentCount` keeps the #82/#84 meaning verbatim; `OpenEnvelopeCount` counts models whose external envelope exists but does not close on the ladder — the rows whose sector/tilt rest on an arbitrary face side.

## 5. Part 3 — `Modify.Update_ExternalComponentsArea`

`DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs`:

1. Signature → `public static ExternalComponentsAreaResult Update_ExternalComponentsArea(this Table? table, IEnumerable<BuildingModel>? buildingModels)`; every early `return` returns `new ExternalComponentsAreaResult(0, 0)` (replaces `return skippedComponentCount`); the method never returns null.
2. Method-local ladder (§2.1) with a comment citing `IMPLEMENTATION_PLAN_issue84.md` §8.
3. After `:172`:
   ```csharp
   double? closingTolerance = shell?.ClosingTolerance(tolerances_Closing);
   if (closingTolerance is null && shell is not null)
   {
       openEnvelopeCount++;
   }
   ```
4. Column setup beside `column_CountyId`/`column_Reference`: `Column? column_ClosingTolerance = table.UpdateColumn<Column>(IO.Constants.Column.ClosingTolerance);` (a null column skips the write, as the county/reference guards already do).
5. Per row (both the fresh-row and the upsert path — the write sits with the 35 area writes): `IO.Modify.SetValue(row, column_ClosingTolerance, closingTolerance is null ? null : (float)closingTolerance.Value);` — NULL is the default cell state, so "open"/"no envelope" needs no explicit write (§9.1).
6. Rewrite `<summary>`/`<returns>` (signal semantics, NULL meaning, the result counts); rebuild → `documentation/API/DiGi.GIS.PostgreSQL/…` regenerates.

## 6. Part 4 — task counters and logs

`DiGi.GIS.PostgreSQL/Classes/BackgroundTask/PostgreSQLBuildingDataExternalComponentsUpdateTask.cs`:

1. New `public long OpenEnvelopeCount { get; private set; }` with a `<summary>` in the style of `SkippedComponentCount` (`:62`); reset in `ExecuteAsync` (`:82` block).
2. `:242` → `ExternalComponentsAreaResult countyResult = Modify.Update_ExternalComponentsArea(table, models_Latest);` with `countySkippedComponentCount = countyResult.SkippedComponentCount;` and `countyOpenEnvelopeCount = countyResult.OpenEnvelopeCount;`.
3. Per-county log (`:293`) and final summary (`:296-303`) gain `{OpenEnvelopeCount} open envelopes`; accumulate alongside `SkippedComponentCount += …` (`:287`).
4. Class `<summary>` paragraph: an open envelope is not a failure — the row is written with a NULL `ClosingTolerance` and the count is the county's share of arbitrary-side faces.

## 7. Part 5 — Facts (`DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/Update_ExternalComponentsArea.cs`)

Mechanical: every `long skipped = Modify.Update_ExternalComponentsArea(...)` call site reads `result.SkippedComponentCount` off the new return type; every closed-fixture fact additionally asserts `result.OpenEnvelopeCount == 0`.

| # | Fact | Fixture & assertions |
|---|---|---|
| new A | `Update_ExternalComponentsArea_OpenEnvelopeSignal` | `Box()` minus its roof (west, east, south, north walls + floor = 5 faces ≥ 4, so the shell exists and is open): `ClosingTolerance` cell is **NULL** (asserted as a null cell, not 0 — needs a `float?` reader beside the `Value` helper at `:265`), `result.OpenEnvelopeCount == 1`, the four wall sector cells still populated |
| new B | `Update_ExternalComponentsArea_ClosedEnvelopeClosingTolerance` | Closed `Box()`: cell `== (float)DiGi.Core.Constants.Tolerance.Distance` (deterministic — `DiGi.Test/DiGi.Geometry.xUnit/Facts/Polyhedron_IsClosed_Monotonicity.cs:170` proves an exact box closes at 1E-06), `OpenEnvelopeCount == 0` |
| existing 9 runnable | assertion updates only | closed fixtures (`WallSectors`, both `Inward…`, `RoofTiltBands`, `TotalEquals…`, `SharedComponent…`, `LatestModelWins`, `ZeroVsNull` valid part) assert `OpenEnvelopeCount == 0`; `ZeroVsNull` (b) additionally asserts the skipped blank-reference/missing-county envelopes do **not** count as open |
| `DeployedBuildingModels` (conf-skip) | reporting | per §8 of #84: no throw; report to `user files/reports/` the split NULL-vs-ladder-rung per row and the classification elapsed time (the `ClosingTolerance` cost measurement the issue asks for); assert every non-NULL cell matches a ladder rung compared as float |
| deployed acceptance (manual, not in DiGi.Test) | `GET api.digiproject.uk/gis/buildingmodel/itemsbycircle?x=638000&y=486000&radius=100` | repeat the #84 §8 harness: rehydrate the 27 models, run the new `Modify` on an in-memory `Table`, count `OpenEnvelopeCount` — **expected 6** (sequential GETs, 500 retried in isolation, read-only — `Coding - Deployed WebAPI`) |

## 8. Guideline alignment

| Guideline | How the plan conforms |
|---|---|
| Coding - General §"Result Type Naming" | `ExternalComponentsAreaResult` ends in `Result`, no stacked noun; follows the 19 in-repo `SerializableResult` precedents |
| Coding - General §"Standard Tolerance Constants" | canonical `Tolerance.Distance`/`Tolerance.MacroDistance` rungs; the remaining rungs are the measured candidate ladder of the #84 §8 verification (a domain input list, not invented single tolerances), documented in place |
| Coding - General §2 (anemic + static extensions; no private static helpers) | the change stays inside the existing static `Modify` member plus local data; the ladder is a method-local array |
| Coding - General §4 (HintPath build order) | DiGi.GIS.IO → DiGi.GIS.PostgreSQL → DiGi.Test, each built before the next consumes the rebuilt DLL; no new NuGet |
| Coding - References | untouched — the Guid-keyed face matching is unchanged, no reference equality introduced |
| Coding - PostgreSQL | no converter, DDL, batching or timeout change — the push path already runs `ADD COLUMN IF NOT EXISTS` for new columns |
| Coding - Automatic Tests | partial `Facts`, no `using Xunit`, XML `<summary>`s; closed/open boundary pair; the DB fact keeps the `Skip` + `GIS_PostgreSQL_Storage.conf` convention; reports to `user files/reports/`; cost measured in isolation |
| Coding - Deployed WebAPI | deployed verification is read-only `GET`, sequential, retried-in-isolation, never added to `DiGi.Test` |
| Coding - API Documentation | `documentation/API/` consulted for `ClosingTolerance`; both repositories' generated docs are regenerated by the builds and committed in sync |
| XML Documentation | new class fully documented; `Modify` `<returns>` and task `<summary>` rewritten; no `CS1591` |
| Coding - Editor Config | byte-preserving edits (CRLF kept); no scripted line rewrites |
| GitHub - Issues §2/§3 | premises verified before building; resolution comment with commits, changes, tests and deployed results; if any measured number differs from the issue's (6/27), the correction goes in a comment, not an edit of the issue body |

## 9. Risks & open questions

1. **`UnitColumn.TryGetValidValue(null)`** — `SetValue` with a null value may not "write" a null; the cell is null by default either way, so the signal lands as NULL regardless. Fact A pins the outcome (null cell, not 0). If validation turns out to write `0f` for null, the write is skipped for nulls explicitly (never write a sentinel).
2. **Closed-fixture rung** — expected `1e-6`; if the shell's face orientation perturbs vertices so an exact fixture closes at a coarser rung, record the measured rung in the fact and in the resolution comment and assert that member — never silently weaken to "non-null" (the acceptance criterion is that the tolerance is *shown*).
3. **Float storage** — 1e-6 stored as `real` rounds (`≈ 1.0000001E-06`); it is a signal rung, not a computation input; facts compare `(float)` conversions like-for-like.
4. **Cost** — ≈ 4 bisected `IsClosed` passes per model, the same order as the shell build itself; measured in the DB fact and on the 27-model harness before any full-county run; if slow, trim the ladder from the top (0.2, 0.1) — the #84 measurement says the deployed closed envelopes close at ≤ 0.05.
5. **NULL conflation** ("open" vs "no envelope") — deliberate (§2.3), stated in the column description; a reader filtering `WHERE closing_tolerance IS NULL` gets exactly the rows not backed by a closed envelope.
6. **`Row.GetValue<float?>` availability** — if the nullable generic read is unavailable in `DiGi.Core.IO`, the fact reads `row[column.Index] as float?`; decide at implementation time, both are single-line readers.

## 10. Execution order

1. **This file first** — write `IMPLEMENTATION_PLAN_issue85.md`, commit on `0.8.10` (`Record the approved implementation plan for the open-envelope signal (#85)`). Nothing else runs before it.
2. DiGi.GIS.IO: column member → build → docs regen → commit (references #85).
3. DiGi.GIS.PostgreSQL: `ExternalComponentsAreaResult` → `Modify` → task → build (zero warnings) → docs regen.
4. DiGi.Test: facts (GIS.PostgreSQL built first) → full `Update_ExternalComponentsArea` set green (11 runnable + 1 conf-skip).
5. Deployed verifications: dev-DB fact (conf) + the 27-model harness → 6 open envelopes.
6. Commits on `0.8.10` referencing #85 (DiGi.GIS.PostgreSQL implementation, DiGi.Test facts); tick #85's acceptance boxes; resolution comment (commits, changes, tests, deployed results, corrections if any measurement differs); close.

## 11. Implementation record (2026-09-21)

Landed on the branches below; the acceptance criteria are met.

| Repository | Branch | Commits |
|---|---|---|
| DiGi.GIS.PostgreSQL | `0.8.10` | `17370be` plan · `1891c94` finest-rung wording · `8930745` implementation |
| DiGi.GIS.IO | `0.8.8` | `7198bf4` column · `94eac99` finest-rung wording · `05aa2f7` base-typed accessor |
| DiGi.Test | `0.8.11` | `5e5b328` facts |

### Corrections made while implementing

1. **The query answers the finest closing rung, not the coarsest.** `IsClosed` is monotone in tolerance, so a solid closed at a fine rung is closed at every coarser one; `ClosingTolerance` therefore returns the finest candidate that closes. The column, its description and §3's sketch were corrected in follow-up commits before any consumer existed.
2. **A `DiGi.Unit.IO` assembly reference is not the way in.** `Constants.Column.ClosingTolerance` is a `UnitColumn`, so reading that field directly forced a reference to `DiGi.Unit.IO` — and merely referencing it made the `DiGi.Unit` namespace shadow `DiGi.BDL.Classes.Unit` in every file of `DiGi.GIS.PostgreSQL`, breaking twelve references in `UnitPostgreSQLConverter.cs` (`CS0118`). It would also have rippled the assembly into every deployed host. Added `Create.Column_ClosingTolerance()` in DiGi.GIS.IO returning the base-typed `Column` — the shape `Columns_ExternalComponentsArea()` already uses — and left the project's reference set unchanged.
3. **The closure signal found a defect in a #84 fixture.** `Update_ExternalComponentsArea_SharedComponentIsInternalPartition` built its two-space model with each floor's origin corner at `y = 0`; for an outward `-z` normal the plane's local y axis runs along `-y`, so those floors sat at `y ∈ [-10, 0]` while the walls stood at `y ∈ [0, 10]`. Areas were unaffected (100 m² each), so every #84 assertion passed on a model that was not watertight. The floors now use the `y = s` corner the closed `Box()` helper uses, and the fact asserts a closed envelope.
   - The `RoofTiltBands` fixture is open by geometry — a tilted plane over the walls' flat tops leaves the head wedges — and records that as its measured `OpenEnvelopeCount` of 1.
4. **A cross-project reflection fact broke, and only a workspace-wide run shows it.** `DiGi.GIS.xUnit/Facts/Columns_ExternalComponentsArea.cs` enumerates `Constants.Column` by reflection and asserted the non-area column count was 36; the new field makes 37. The `DiGi.GIS.PostgreSQL.xUnit` fact set was green while that fact was red, because it lives in a different test project. Updated to 37 and extended with assertions for the new column (name, `closing_tolerance` unique id, `UnitColumn`/`Float`, category, description, disjointness from the 35).
5. **`Column_ClosingTolerance` was moved to `Create/Column.cs`.** DiGi.GIS.IO keeps singular `Column_*` accessors in `Column.cs` and plural `Columns_*` in `Columns.cs`; the first landing had it in `Columns.cs`.
6. **The deployed fact's open-count invariant was too strong.** It equated `OpenEnvelopeCount` with every null-closing-tolerance cell, but a model with no components (or only internal partitions) writes a zero row with a null tolerance and is deliberately not counted as open. It now counts a null cell as open only when the row carries areas, reports the no-envelope rows separately, and `ZeroVsNull` gained the no-components case (zero row, null tolerance, not counted).

### Facts

`DiGi.GIS.PostgreSQL.xUnit` — 15 passed / 1 conf-driven skip (`Update_ExternalComponentsArea_DeployedBuildingModels`). New: `Update_ExternalComponentsArea_OpenEnvelopeSignal` (box minus roof → null cell, 1 open), `Update_ExternalComponentsArea_ClosedEnvelopeClosingTolerance` (closed box → `(float)Tolerance.Distance`), `ExternalComponentsAreaResult_Serialization`, `ExternalComponentsAreaResult_Closed`; the task constructor fact gained the `OpenEnvelopeCount == 0` default. The `ZeroVsNull` fact gained the no-components case.

`DiGi.GIS.xUnit` — 74 passed / 0 failed after the `Columns_ExternalComponentsArea` count and assertions were updated for the new column (correction 4).

### Deployed verification (read-only)

`GET https://api.digiproject.uk/gis/buildingmodel/itemsbycircle?x=638000&y=486000&radius=100` — the 27-model sample of #84 §8, deserialized locally and classified with this build:

- 27/27 models deserialized, 27 unique references, 27 rows, **6 open envelopes** — the acceptance figure.
- The six are the rows with a null closing tolerance carrying areas (references `…DFA0-…`, `…DEFB-…`, `…DEFC-…`, `…DDA9-…`, `…DE50-…`, `…DD0F-…`); `38F62225-DEFC-F520-E053-CA2BA8C0BE14` is among them, the open-envelope model #84 §8 named.
- The closed envelopes record the rung set #84 reported: 1e-6, 0.001, 0.01, 0.02, 0.05.
- Skip count **105** here against #84 §8's **104**; the counting code is shared with #84 (unchanged by this issue), so a one-component difference is data drift or a harness dedupe difference, not a behaviour change.
