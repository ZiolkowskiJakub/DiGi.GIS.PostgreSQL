# Implementation Plan — Issue #60: Document the column-clobbering risk of `IO.Modify.Update_Building2D` on upsert

> Repository: `DiGi.GIS.PostgreSQL` · Issue: [ZiolkowskiJakub/DiGi.GIS.PostgreSQL#60](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/60) · Labels: `type: documentation`, `priority: low`, `ai: light`
> Date: 2026-09-03 · Status: **still valid** — premise verified against code; one existing doc line found to be *incorrect* and is added to the scope.

---

## 1. Verification findings (per `GitHub - Issues.md` §2 — verify the issue before implementing)

The issue asks *whether* the upsert writes every column or only the set columns. That is now answered with evidence:

| # | Finding | Evidence |
|---|---------|----------|
| 1 | The upsert writes **every column present on the `Table`**, not just set cells. `ON CONFLICT (…) DO UPDATE SET "col" = EXCLUDED."col"` is built per non-PK column; **no COALESCE, no NULL skipping** — unset cells are bound as `DBNull.Value` and **clobber stored values**. Columns absent from the `Table` are never touched. | `DiGi.PostgreSQL/DiGi.PostgreSQL.Table/Classes/TablePostgreSQLConverter.cs` — SQL build ~L1530–1570 (`"col" = EXCLUDED."col"` loop, L1564–1566), `DBNull` binding ~L1619 |
| 2 | `UpdateColumn<TColumn>(this Table, TColumn)` is **get-or-create**: every column an overload mentions is added to the `Table` even if the value ends up null. | `DiGi.GIS.IO/DiGi.GIS.IO/Modify/UpdateColumn.cs:14–27` |
| 3 | The fallback (unassigned-buildings) pass is therefore safe today for **two independent caller-side reasons**: (a) its SELECT excludes in-scope buildings, (b) its `Table` never contains the administrative columns because it only calls the geometry overload. Neither reason is documented on the `Update_Building2D` contract. | (a) `Building2DPostgreSQLConverter.cs` — `GetBuilding2DsUnreachedByCountyAsync` ~L2648–2681 (`subdivision_id IS NULL OR NOT (subdivision_id = ANY(@…))`); (b) `PostgreSQLBuildingDataUpdateTask.cs` ~L517 (fallback calls only the geometry overload); `Update_Occupancy` writes only `Reference`/`County Id`/`Calculated occupancy` (`DiGi.GIS.PostgreSQL/Modify/Update_Occupancy.cs:17–90`) |
| 4 | **Record defect found:** the name-taking overload's summary claims *"a name that was not resolved leaves the stored one alone rather than clearing it."* That is **false for existing DB rows**: by the time the overload runs, the name columns are already on the `Table` (finding 2), so `PushAsync` writes their NULL cells over the stored values (finding 1). An unresolved name on a re-run **erases the previously stored name**. Must be corrected in the same change. | `DiGi.GIS.IO/DiGi.GIS.IO/Modify/Update_Building2D.cs` — summary of the 8-arg overload, ~L424–426 |

Conclusion: the issue's risk is real and currently guarded only by undocumented caller-side invariants. The documentation gap is confirmed, and finding 4 adds one stale sentence to fix.

---

## 2. Deliverables

### A. DiGi.GIS.IO — XML docs on `Update_Building2D` (core, in scope per issue)

File: `DiGi.GIS.IO/DiGi.GIS.IO/Modify/Update_Building2D.cs`

1. **Geometry overload** `Update_Building2D(Table, int, IEnumerable<Building2D>, string?)` — summary states the columns it writes:
   - identity: `Reference`, `County Id`
   - function/phase: `Building general function`, `Building specific functions`, `Building Phase`
   - shape: `Storeys`, `Floor area`, `Total area`, `Internal Point X/Y`, `BoundingBox X/Y/width/height`, `Cardinal direction`, `Azimuth`, `Isoperimetric ratio`, `Thinness ratio`, `Rectangular thinnes ratio`, `Square thinness ratio`, `Convex hull thinness ratio`, `Calculated Building Shape`
   - occupancy flags: `Is occupied`, `Is residential`
   - links/coverage: orthophotomap link columns (per year), grid cell coverage columns
   - note the conditional cells (area only when not NaN, internal point/bounding box only when computable, shape only when the solver resolves) — a cell left unset is written as NULL on push.
2. **Name-taking overloads** (both) — list the administrative columns: `Subdivision Id`, `County name`, `Municipality name`, `Voivodeship name`, `Subdivision name`, `Subdivision occupancy`, `Settlement type`, each written only when present.
3. **Correct the false claim** (finding 4): replace "leaves the stored one alone rather than clearing it" with the true end-to-end behavior — the cell stays unset in the `Table`, but the column is on the `Table`, so `PushAsync` writes NULL and **clears the stored value** on an existing row.
4. **Add the upsert-contract paragraph** to the overload summaries:
   > When pushed via `TablePostgreSQLConverter.PushAsync`, the `ON CONFLICT (county_id, reference) DO UPDATE SET col = EXCLUDED.col` clause covers every column present on the `Table`; a cell left unset on a row is written as NULL and overwrites the stored value. A column that is never added to the `Table` is never touched.
5. Rules (`XML Documentation - Create.md` / `- Audit.md`): exactly one `<summary>` per member, no blank `///` lines, `<para>` for paragraphs, all `<param>` in order, **zero logic changes**, zero compiler warnings.
6. Rebuild the project — `documentation/API/DiGi.GIS.IO/` regenerates on compile (`Coding - API Documentation.md` §2); commit the regenerated markdown.

### B. DiGi.Test — optional test fact (in scope per issue, "Optionally")

Location: `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/PostgreSQLBuildingDataUpdateTask.cs` (existing partial `Facts` class, existing DB facts `PostgreSQLBuildingDataUpdateTask_CrossCountySubdivision_Integration` and `_MultiPartCounty_Integration` set the convention).

- New fact `PostgreSQLBuildingDataUpdateTask_FallbackExcludesInScopeBuildings_Integration`, `[Fact(Skip = "Requires the PostgreSQL configuration files pointing at a database.")]`:
  1. Take a test county with in-scope subdivisions (reuse the established test county, e.g. `76453`).
  2. Build the in-scope set via `Query.InScopeSubdivisionIds` (already covered by the pure-logic facts `InScopeSubdivisionIds_*`).
  3. Read the in-scope buildings (`GetBuilding2DsByCountyIdAsync` per subdivision) and the fallback set (`GetBuilding2DsUnreachedByCountyAsync(countyId, inScopeSubdivisionIds)`).
  4. Assert the two reference sets are **disjoint** — that is the invariant "a fallback run never re-processes in-scope buildings", currently proven only by the SQL predicate.
- **Guard must be shown to fail** (`Coding - Automatic Tests.md` §4): temporarily break the exclusion (pass an empty in-scope set to the unreached query), watch the fact go red, restore, watch it go green. Commit the fact either way.
- Runs against the local test DB pointed at by the `user files/` conf (`Coding - PostgreSQL.md` — connection asset isolation).

### C. DiGi.PostgreSQL — optional, recommended (outside the issue's stated scope)

- Add the same upsert-semantics `<para>` to the `PushAsync` summaries in `DiGi.PostgreSQL/DiGi.PostgreSQL.Table/Classes/TablePostgreSQLConverter.cs` (~L1390, ~L1425) — the contract's authoritative home; the DiGi.GIS.IO docs then reference it instead of restating it.
- Two-line doc addition; requires write access to the DiGi.PostgreSQL repo. Include only if the user approves the extra repo.

### Out of scope (per issue)

- Changing the upsert key `(county_id, reference)`.
- Changing what the subdivision loop path writes.
- No production behavior change in A/B; C is documentation only.

---

## 3. Workflow (per the guidelines)

1. **Branch setup** (`GitHub - Branch Pull.md`): for each affected repo — `DiGi.GIS.IO` (local branches `0.8.7`, `0.8.8`, `main` → work on the highest SemVer branch, verify against remote) and `DiGi.GIS.PostgreSQL` (if B) — fetch, check out, pull.
2. **A:** apply the XML doc changes → `dotnet build` (0 warnings) → API markdown regenerates on compile → commit on the version branch.
3. **B:** add the fact → run it in isolation against the test DB with the red/green guard check → commit in `DiGi.GIS.PostgreSQL`.
4. **C (if approved):** doc line → build → commit in `DiGi.PostgreSQL`.
5. **Issue hygiene** (`GitHub - Issues.md`):
   - Comment on #60 with the verified answer and evidence (findings 1–4) — write the body to a UTF-8 (no BOM) `.md` file with LF newlines and pass it via `--body-file`; never inline multi-line markdown in PowerShell.
   - Resolution comment per §3 structure (commits, files, tests, verification) → close the issue.
   - **Do not push** without an explicit request.

## 4. Verification checklist

- [ ] `dotnet build` of DiGi.GIS.IO: 0 warnings (CS1591-sensitive XML edits).
- [ ] `documentation/API/DiGi.GIS.IO/DiGi.GIS.IO.md` regenerated and consistent with the new summaries.
- [ ] The false "leaves the stored one alone" sentence is gone; the upsert contract paragraph is present on both overload groups.
- [ ] (B) New fact: red when the exclusion is broken, green when restored; passes in isolation against the test DB.
- [ ] (C) `PushAsync` summary carries the upsert semantics; DiGi.GIS.IO docs reference it.
- [ ] Issue #60: evidence comment posted, resolution comment posted, issue closed.

## 5. Impact of the in-flight Issue #59 changes (verified 2026-09-03)

The working tree carries uncommitted #59 work (coverage cross-county breakdown): `Classes/Result/BuildingDataCoverageResult.cs` gains `CrossCountySubdivisionCount` (7th constructor arg, default 0), `Create/BuildingDataCoverageResult.cs` computes it, and `Facts/BuildingDataCoverageResult.cs` populates it in the serialization fact.

- **Findings 1–4 are unaffected:** #59 touches neither DiGi.GIS.IO, DiGi.PostgreSQL, nor the update task's write path or the fallback exclusion predicate (re-verified: `PostgreSQLBuildingDataUpdateTask.cs` L199/L204/L490–491 still build the in-scope set via `SiblingCountyGroups` + `InScopeSubdivisionIds` and exclude it via `GetBuilding2DsUnreachedByCountyAsync`).
- **Deliverable A is unaffected** (different repo; #59's changes here are additive and read-only).
- **Deliverable B remains non-duplicative and complementary:** #59's facts measure the *diagnostic* figures; the new fact asserts the *task* invariant (fallback set disjoint from in-scope buildings). No conflict with the existing `MissingReferenceCount == 0` integration assertions — the new property is additive with a default.
- **Observation (out of scope, note only):** #59's coverage factory classifies cross-county with a narrower notion of in-scope — `parentId == countyId` only, no sibling code groups — while the task's loop reaches subdivisions filed under *sibling parts* of the county's code. For multi-part counties the diagnostic can therefore count loop-reached buildings as cross-county. If the user wants the diagnostic aligned with the task, it should reuse `Query.InScopeSubdivisionIds(subdivisions, countyReferences.SiblingCountyGroups())`. Do not fold into #60.

## 6. Risks / access notes

- DiGi.GIS.IO and DiGi.PostgreSQL are outside the current writable workspace — write access must be granted per repo before implementation (DiGi.GIS.IO and DiGi.PostgreSQL were already granted **read** access during this investigation).
- Finding 4 changes a sentence that shipped with the #53 fix; it is a record correction with no code impact — call it out in the issue comment so the next reader trusts the corrected text.
- (B) is DB-dependent and skip-by-default, matching the repo's existing integration-fact convention; CI will not run it.
