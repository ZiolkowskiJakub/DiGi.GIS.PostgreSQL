# Implementation Plan — DiGi.GIS.PostgreSQL #84

**External Components Area: face normals must come from the space shell (`normalSide`), not from the component geometry**

- Issue: https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/84 (open, `type: bug`, `priority: high`, `ai: heavy`)
- Parent tracking issue: DiGi.GIS.PostgreSQL #83 (this issue blocks its close)
- Corrects: #82 (closed, `0aa8cf6` on `0.8.10`) — `Modify.Update_ExternalComponentsArea`
- **Supersedes #82 plan's design decision 1** (the floor-internal-point orientation contract) — the issue explicitly re-examines it "with the shell in place"

---

## 1. Validity assessment

The issue is **still valid and actionable**. The pre-fix implementation is in the tree (branch `0.8.10`, HEAD `67c62a2`, clean working tree), and every API premise of the required approach checks out against the code.

### Premises verified against code (with evidence)

| Issue claim | Status | Evidence |
|---|---|---|
| #82 classifies via `Query.Geometry3D<PolygonalFace3D>` + floor-internal-point orientation | ✅ | `DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs` — `Face()` helper at line 33; no-floor throw at 146–149; interior reference `Q` at 153–158; wall face point `P` at 173–177; cosine flip at 202–214; roof `Z < 0` flip at 254–258 |
| `Geometry3D<T>` never resolves the normal's direction against the building's interior/exterior | ✅ | `DiGi.Analytical.Building/Query/Geometry3D.cs` — reads the stored `.Geometry` and `Convert<T>` returns a clone carrying the **stored** normal; the orientation is done (heuristic) in `Update_ExternalComponentsArea` itself |
| `BuildingModel.GetShell(ISpace, Side?, …)` / `GetShells<TSpace>(…, Side?, …)` exist | ✅ | `DiGi.Analytical.Building/Classes/BuildingModel.cs` — `GetShell` at line 819, `GetShells(IEnumerable<TSpace>, …)` at 842, `GetShells<TSpace>()` at 874; all delegate to `CreateShell` (895–937) |
| A `Shell` carries the space's `GuidReference`; every `Face` carries the `GuidReference` of the component it was built from | ✅ | `CreateShell` verbatim: `faces.Add(new Face(new GuidReference(component), polygonalFace3D))`, `Shell shell = new(new GuidReference(space), faces)`; `DiGi.Analytical.Building.xml` on `GetShell` confirms the same |
| `Side` = `{ Undefined, Internal, External }` (external side = `Side.External`) | ✅ | `DiGi.Geometry.xml` — `F:DiGi.Geometry.Core.Enums.Side.{External,Internal,Undefined}` |
| `Side.External` orients by topology, not guesswork | ✅ | `DiGi.Geometry/Spatial/Classes/Updater/PolyhedronNormalizationUpdater.cs` → `Polyhedron.SetNormal(i, side)` → `Polyhedron.GetNormal` (line 275): internal point of the face, **half-infinite ray along the raw plane normal, counting ALL faces it crosses** (one point each, not first-hit); **even = external, odd = internal**; flipped to the requested side |
| **Parity is sound only for a closed face set, and silently falls back to the stored normal otherwise** | ⚠️ | `GetNormal` returns `null` on a degenerate intersection (ray coplanar with another face, or >1 point with one face) and for an **open** face set the even/odd count is geometry-dependent and meaningless; `SetNormal` then returns `false` **leaving the stored normal unchanged** (`Polyhedron.cs` 688–714); and `CreateShell` **discards `Update()`'s return value** and returns the shell unconditionally (`BuildingModel.cs` 927–936). Consequence: a face that cannot be oriented keeps its stored (possibly inward) direction with **no throw, no skip, no signal** — see Risks §6.1 |
| `GetShells<TSpace>()` returns null only when the model has no `TSpace` spaces; per-space null (no relations) is skipped; a space with relations but no face **throws** (`InvalidOperationException`) | ✅ | `BuildingModel.cs` lines 842–894 + `CreateShell` 920–923 |
| Stored/deployed `BuildingModel`s carry a `Space` and `SpaceRelation`s | ✅ | `DiGi.Test/files/buildingmodel_5072294.json` — 1× `DiGi.Analytical.Building.Classes.Space`, 7× `SpaceRelation` in `BuildingRelationCluster.RelationListCluster`; the concrete space type is exactly `Space` |
| `Face.UniqueReference` is a fresh clone on every call | ✅ | `DiGi.Analytical/Classes/AnalyticalGeometry/Face.cs` line 73 — `this.uniqueReference = Core.Query.Clone(uniqueReference)`; XML docs `<remarks>` on `P:Face.UniqueReference` repeat the `==` warning and name `Core.Query.Equals` |
| Shell faces are enumerable; `Face` inherits `PolygonalFace3D` (`GetArea()`, `Plane.Normal`) | ✅ | `DiGi.Geometry/Spatial/Classes/Polyhedron.cs` line 160 — `List<TPolygonalFace3D>? PolygonalFaces` (cloned on access); `Face : PolygonalFace3D` (`Face.cs` line 12) |
| Fixture construction API exists and is used by existing Facts | ✅ | `BuildingModel.Assign(IComponent, ISpace, ISpace?)` (`BuildingModel.cs` 225–257; the relation **replaces** any existing one; pass both spaces in one call for a shared component); existing usage in `DiGi.Test/DiGi.Analytical.xUnit/Facts/` (`SpaceRelation.cs`, `BuildingModelUnassign.cs`, `BuildingModelTrySplit.cs`) |
| `Space` construction in tests | ✅ | `new Space(new Point3D(…), "Space 1")` — the `Point3D` is the space's internal point |
| Deployed verification endpoint is live | ✅ | `GET https://api.digiproject.uk/information/endpoints` — `BuildingModel/GetItemsByCircle`: `x` (required), `y` (required), `radius`/`diameter`/`tolerance` (optional); **no `countyid` parameter** — the radius-100 sample (27 models) is a pure circle query |
| Pre-fix code and #82's plan are the baseline | ✅ | branch `0.8.10`; `IMPLEMENTATION_PLAN_issue82.md` in this repository |

### Design decisions the issue leaves open — this plan rules on them

The issue requires an explicit, deliberate decision for the uncovered/shared cases, "made against the #82 throw/skip contract, not left to the old heuristic":

| Case | Decision | Rationale |
|---|---|---|
| Component referenced by **exactly one** shell face | Classify from that shell face (normal + area) | The fix itself |
| Component referenced by **two or more** shell faces (shared by ≥ 2 spaces) | **Internal partition → excluded from the external area, counted in the returned skip count** | The columns are the *external* components area; a face bounding two spaces is internal to the building. #82 counted it under the floor-point side — an arbitrary orientation for a face that is not external at all |
| Component referenced by **no** shell face | **Throw** (surfaces as a county failure, message names the component `Guid` + envelope `Reference`) | #82's contract treats "cannot be classified because the model's structure is missing" as a data defect (its no-floor throw was exactly that class). A wall/roof/floor that bounds no space is a defect in the stored model's space structure, not a geometrically-valid-but-undefined bucket (which is the counted-skip class) |
| Model with **no spaces at all** (`GetShells<Space>` → null) | Throw when the model carries components (they are all uncovered); a model with **no components at all** writes a zero row | The "stored model gets a row" 0-vs-NULL contract stands; the #82 no-floor throw was a by-product of the interior-reference need, which the shell removes |
| Space that has relations but no polygonal face | `CreateShell`'s `InvalidOperationException` **propagates** (their throw, same handling as #82's "their throw propagates") | — |
| **Roof `Z < 0` flip** (line 254–258) | **Removed** — the shell normal is authoritative | The flip was part of the heuristic the issue removes. A legitimate upper roof's shell normal points up (tilt ∈ [0°, 90°]); a downward-facing `IRoof` is a mislabelled ceiling (degenerate — see Risks) |
| **Floor** | Area only, from its shell face; no normal, no orientation | A floor's bucket (`ExternalFloorArea`) needs no direction; the shell face is still the area source, so "classify from the shell" holds uniformly |

---

## 2. Scope

**In scope (this issue):**

- `DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs` — replace the `Query.Geometry3D` + internal-point orientation with the shell-based normal source. Signature, 35 columns, sector/tilt rules, total = sum of the 34, and the 0-vs-NULL semantics are **unchanged**.
- `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/Update_ExternalComponentsArea.cs` — fixture redesign (models must now carry a `Space` + relations) and the new Facts.
- The method's XML `<summary>` (it currently describes the internal-point orientation) and the regenerated `documentation/API/` entry.

**Out of scope:**

- `PostgreSQLBuildingDataExternalComponentsUpdateTask` / its options — the `Modify` signature is unchanged, so the task is untouched (its catch/log/county-failure handling already surfaces a throw as `FailedCountyCount++`).
- Column definitions (DiGi.GIS.IO #11), UI (DiGi.GIS.PostgreSQL.UI #12), net-of-openings areas.

---

## 3. Part 1 — `Modify.Update_ExternalComponentsArea` (new algorithm)

Signature unchanged:

```cs
public static long Update_ExternalComponentsArea(this Table? table, IEnumerable<BuildingModel>? buildingModels)
```

Per envelope (guards, dedupe on `(countyId, reference)`, first-record-wins, `ToDiGi()` null → throw — all unchanged):

1. **Fetch component lists once each** (clones — unchanged): `List<IWall>? walls`, `List<IRoof>? roofs`, `List<IFloor>? floors` via `GetComponents<T>()`.
   - **No components at all** → write the row with 0 in all 35 columns (the 0-vs-NULL contract) and continue. (The #82 no-floor throw goes away — see decision table.)
2. **Build the shells** — one call, external side:

   ```cs
   List<Shell>? shells = buildingModel.GetShells<DiGi.Analytical.Building.Classes.Space>(DiGi.Geometry.Core.Enums.Side.External);
   ```

   - `null` (no `Space` in the model) with components present → **throw** `InvalidOperationException` naming the first uncovered component and the envelope `Reference`.
   - `CreateShell`'s `InvalidOperationException` (a space with relations but no face) propagates — their throw.
3. **Collect the shell faces and match them to components** (acceptance: "faces are matched back to components by the face's `UniqueReference`"):

   ```cs
   Dictionary<Guid, DiGi.Analytical.Classes.Face> face_ByComponentGuid = [];
   HashSet<Guid> sharedComponentGuids = [];

   foreach (Shell shell in shells)
   {
       List<DiGi.Analytical.Classes.Face>? faces = shell.PolygonalFaces;   // cloned on access — read once per shell
       Assert-free guard: faces is null or empty → throw (a shell with no face is a defect)

       foreach (DiGi.Analytical.Classes.Face face in faces)
       {
           // Face.UniqueReference is a fresh clone on every call — hoist before any use (References guideline §4).
           IUniqueReference? uniqueReference_Face = face.UniqueReference;
           if (uniqueReference_Face is not DiGi.Core.Classes.GuidReference guidReference_Face)
           {
               continue;   // defensive — CreateShell always stores a GuidReference
           }

           Guid guid_Component = guidReference_Face.Guid;   // GuidReference(IGuidObject) stored component.Guid
           if (face_ByComponentGuid.ContainsKey(guid_Component))
           {
               face_ByComponentGuid.Remove(guid_Component);
               sharedComponentGuids.Add(guid_Component);    // bounds ≥ 2 spaces → internal partition
           }
           else
           {
               face_ByComponentGuid.Add(guid_Component, face);
           }
       }
   }
   ```

   - Keying by the `Guid` (pattern-matched off the concrete `GuidReference`) compares **object instance identity** — the References guideline's blessed route for that intent (§5) — and sidesteps the interface `==` trap entirely (no `IUniqueReference == IUniqueReference` anywhere in the change).
4. **Classify each component** (walls, then roofs, then floors — loop order and bucket math unchanged):

   For each component of kind `K` (wall / roof / floor):

   ```cs
   Guid guid_Component = component.Guid;   // IComponent → IConstructable → IBuildingGuidObject → IGuidObject exposes Guid directly

   if (sharedComponentGuids.Contains(guid_Component))
   {
       // Bounded by two or more spaces: an internal partition, not an external component.
       skippedComponentCount++;
       continue;
   }

   if (!face_ByComponentGuid.TryGetValue(guid_Component, out DiGi.Analytical.Classes.Face? face_Shell))
   {
       throw new InvalidOperationException($"Building {reference} of county {countyId}: the {K} component {guid_Component} bounds no space, so no shell face carries its outward normal and it cannot be classified.");
   }

   Vector3D? normal = face_Shell.Plane?.Normal;   // shell-oriented (Side.External) — the stored direction only if parity could not orient it (open face set; Risks §6.1, verified against #82 in §4.3)
   if (normal is null || normal.Length <= 0) { throw … }   // same defect class as today
   Vector3D? normal_Unit = normal.Unit;
   if (normal_Unit is null) { throw … }

   double area = face_Shell.GetArea();             // the shell face wraps the component's own geometry — identical value
   if (double.IsNaN(area) || area <= 0) { throw … } // same contract as the current Area() helper
   ```

   - **Wall:** `azimuth = (Math.Atan2(normal_Unit.X, normal_Unit.Y) * 180.0 / Math.PI + 360.0) % 360.0;` → `DiGi.GIS.Query.CardinalDirection(azimuth)` → sector index (unchanged; `Undefined` → counted skip, unchanged).
   - **Roof:** `tilt = normal_Unit.Angle(Vector3D.WorldZ) * 180.0 / Math.PI;` flat `< 5°` → flat column; else band `[5°, 20°]` / `(20°, 45°]` / `> 45°` crossed with the sector (unchanged). **No `Z < 0` flip** (decision table).
   - **Floor:** `areas[index_Floor] += area;` (no orientation consumed).
5. **Write the row** — unchanged: existing row reused or `AddRow()`, 34 breakdowns (0 where empty) + total = sum of the 34, `table.AddRow(row, false)`.
6. **Return** the counted-skip total (now: internal partitions + `CardinalDirection.Undefined`; the #82 degenerate-`|cosine|` skip disappears with the interior point).

**Deleted from the current file:** the `Face()` helper (line 31), the floor `Q` block (146–158), the wall `P` + cosine block (171–214), the roof flip (254–258), and the `using` entries that only they required. **Kept:** the `Area()` helper (now fed shell faces — `Face` is a `PolygonalFace3D`), the sector/tilt constants, the column resolution, the row write.

**`<summary>` rewrite (English only):** replace the "outward direction … resolved against the building's interior … internal point of the first floor" paragraph with: the wall outward and roof upward normals are the normals of the shell faces of the model's spaces, built with `Side.External` (ray-parity orientation over the closed face set), matched to components by the face's `UniqueReference`; a component bounding no space is a data defect (throw), one bounding ≥ 2 spaces is an internal partition (counted skip). `<returns>` unchanged (skip count).

---

## 4. Tests — `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit`

One file, `Facts/Update_ExternalComponentsArea.cs`, existing `partial class Facts` (no `using Xunit`, XML `<summary>` on every Fact, English only).

### 4.1 Fixture redesign (private helpers)

- **The model must now carry a `Space` and relations.** New helper shape:

  ```cs
  private static BuildingModel Model(string reference, int countyId, params IComponent[] components)
  {
      DiGi.Analytical.Building.Classes.BuildingModel buildingModel = new();
      Space space = new(new Point3D(0, 0, 0), "Space 1");
      Assert.True(buildingModel.Update(space));
      foreach (IComponent component in components)
      {
          Assert.True(buildingModel.Assign(component, space));   // Update + SpaceRelation in one call
      }
      Assert.True(buildingModel.SetValue(BuildingModelParameter.Reference, reference, new SetValueSettings(true, false)));
      BuildingModel? envelope = buildingModel.ToPostgreSQL(countyId);
      Assert.NotNull(envelope);
      return envelope!;
  }
  ```

  (The current `Model` helper's `Update(component)` calls are replaced by `Assign(component, space)`; `Assign` already stores the component and the space.)
- **Parity-closure constraint on the geometry (load-bearing, not stylistic):** `Side.External` orientation is ray-parity — the half-infinite ray along a face's raw normal is counted against the *other* faces, even = external, odd = internal. That count is meaningful **only for a closed face set** (topologically valid polyhedron, or at least closed along each tested normal). It is not: a face that parity cannot orient is **not** flagged — `GetNormal` returns null, `SetNormal` bails, `CreateShell` discards the result, and the face **silently keeps its stored normal** (Risks §6.1). So a fixture whose face set is open does not fail loudly; it just orients arbitrarily and the assertion passes or fails for the wrong reason. Every fixture space that asserts a wall/roof normal therefore uses a **closed volume**: a box (4 walls + floor + roof), an N-gonal prism (N walls + floor + roof), or a gable (2 end walls + 2 gable-end walls + 2 roof planes + floor). (A single-face space is acceptable for the floor-only fact, because the floor consumes no orientation — its arbitrary parity is irrelevant to the area-only assertion.)
- The **inward stored normal** is produced exactly as today — `Wall(azimuth, outward: false)` stores the inverse normal — but it now has to survive *parity*, not the floor-point test: with a closed volume, parity re-orients it outward regardless of the stored direction. That is the regression guard the issue asks for.
- **Two-space fixtures** for the shared-partition fact: `buildingModel.Assign(sharedWall, space_1, space_2)` (one call, both spaces — the documented way to bound a component by two spaces); each space's own boundary stays a closed volume around its side of the partition.

### 4.2 Facts

| # | Fact | Change |
|---|---|---|
| 1 | `Update_ExternalComponentsArea_WallSectorBoundaries` | 16-gonal prism, every wall assigned to the space; stored normals mixed inward/outward (both sides of each boundary keep their `(azimuth, outward)` pairing); same column assertions |
| 2 | `Update_ExternalComponentsArea_RoofTiltBands` | closed gable (or box + tilted roof plane), roof assigned; same band assertions |
| 3 | `Update_ExternalComponentsArea_FlatRoofRegardlessOfAzimuth` | box with a flat roof, assigned |
| 4 | `Update_ExternalComponentsArea_FloorOnly` | single floor assigned to a space (one-face shell; parity keeps the stored direction — irrelevant for a floor); same assertions |
| 5 | `Update_ExternalComponentsArea_TotalEqualsSumOfBreakdowns` | box with walls + roof + floor, all assigned; same invariant |
| 6 | `Update_ExternalComponentsArea_NonPlanarThrows` | a `CurveWall` with null base curve, assigned to the space: `CreateShell` skips it (no polygonal face) → it is **uncovered** → our throw. Assert `InvalidOperationException` and that the message names the component. If the non-planar wall is the space's *only* component, `CreateShell`'s own throw fires instead — assert `ThrowsAny<InvalidOperationException>` in that shape |
| 7 | `Update_ExternalComponentsArea_LatestModelWins` | both envelopes carry a space + assigned components; first-record-wins assertions unchanged |
| 8 | `Update_ExternalComponentsArea_ZeroVsNull` | (a) floor-only model (now space-assigned) writes 0s in the 34 buckets; (b) blank-`Reference` envelope skipped — unchanged |
| 9 | **new** `Update_ExternalComponentsArea_InwardStoredNormalLandsOutward` | **the acceptance fact — must fail on the pre-fix implementation and pass after.** A model in which the old code's reference (the first floor's internal point `Q`) sits on the **outward** side of the wall under test, with that wall's stored normal pointing **into** the volume. **Two conditions, each load-bearing:** (a) the face set must be **closed along the test wall's normal** — the test wall needs its **opposing wall** present, because parity flips only when the ray along the stored normal crosses an odd number of other faces (with no opposing wall the ray crosses 0 = "external" and parity keeps the stored direction, so the fact would fail *post-fix* too); (b) `Q` must be on the outward side so the old `cosine` test does **not** flip. Construction: west wall (test, `x = -5`) **+ east wall (`x = +5`, the opposing face)** + a floor slab placed on the outward (west) side, e.g. `x ∈ [-20, -15]`, `z = 0`, so `floors[0]`'s internal point is west of the west wall; west wall stored normal `+x` (inward, toward `x ∈ (-5, 5)`). Trace — pre-fix: `Q - P` points west, `cosine = (+x)·(west) < 0` → no flip → filed under **east** (90°), the 180° error the issue names; post-fix: the `+x` ray crosses the east wall (1 = odd = internal) → flipped to `-x` → filed under **west** (270°) = the true outward sector. The implementer **runs this fact against the current implementation first and records the failure** (Reproduce-Before-Fixing); if the geometry does not fail the old code, the `Q` placement is wrong and must be corrected (a fact that cannot fail pre-fix is not the acceptance fact) |
| 10 | **new** `Update_ExternalComponentsArea_SharedComponentIsInternalPartition` | two spaces sharing one wall (`Assign(wall, space_1, space_2)`): the shared wall is **excluded** from every area bucket and **counted** in the returned skip count; each space's own external walls classify normally. (Fails pre-fix by necessity — the old code counts the shared wall under the floor-point side; the assertion is the new contract) |
| 11 | **new** `Update_ExternalComponentsArea_UncoveredComponentThrows` | a space (closed box) plus an **unassigned** wall: `Assert.Throws<InvalidOperationException>`, message names the component `Guid` and the envelope `Reference`; the other (assigned) components' presence does not save the model |
| 12 | `Update_ExternalComponentsArea_DeployedBuildingModels` | unchanged flow (dev DB via `GIS_PostgreSQL_Storage.conf`, `[Fact(Skip = …)]` convention, read-only, no push); now asserts the shell contract on the deployed models: no throw, skip count reported (expected 0 — if non-zero, that is a data finding to report before deciding), total = sum of the 34 per row, wall/roof/floor buckets populated |

**Boundary discipline** (Automatic Tests §4): sector and tilt boundaries stay tested 0.0001° on both sides, exactly as today.

**Execution discipline:** build the library before the facts (HintPath rule — `dotnet build "DiGi.GIS.PostgreSQL\DiGi.GIS.PostgreSQL.csproj"` then `dotnet test`), zero warnings in both projects.

### 4.3 Deployed-models verification (acceptance: "same rows, total = sum of the 34 breakdowns in each row")

Read-only, manual, **not** added to `DiGi.Test` (Deployed WebAPI guideline):

1. `GET https://api.digiproject.uk/gis/buildingmodel/itemsbycircle?x=<X>&y=<Y>&radius=100` — the same sample point #82 used (27 models). Query the host sequentially; retry a 500 once before believing it.
2. Rehydrate each returned model, run the **new** `Update_ExternalComponentsArea` on a fresh in-memory `Table` (per county batch, as the task does).
3. Assert per row: the 35 values are **identical to the #82 run** (the stored `building_data` values from the dev DB are the comparison baseline where the task ran them; otherwise re-run the pre-fix implementation in a scratch console against the same 27 models as the baseline) and total = sum of the 34 (float-tolerant).
4. Any model that throws or skips is a **data finding** — record the reference, county, and which decision (uncovered / shared) fired, and report before deciding whether the decision table needs to bend to the data.

---

## 5. Guideline compliance map

| Guideline | How the plan honours it |
|---|---|
| GitHub - Issues §2 (verify before implementing) | every premise in §1 checked against code/source/XML docs with citations; the acceptance fact is required to fail pre-fix before the fix is written |
| Coding - General §1 (naming/typing) | explicit typing, `camelCase` type-prefixed locals (`face_ByComponentGuid`, `guid_Component`, `face_Shell`), plural collections (`shells`, `faces`), block-scoped namespace, no `var`, `[]` collection expressions, target-typed `new()` |
| Coding - General §2 (anemic + static extensions) | the change stays inside the existing static `Modify` member; no new types, services or managers |
| Coding - General §1.12 (temporary code) | no `TODO [Marker]` — the shell source is the permanent design; the removed heuristic is deleted, not bypassed |
| Coding - References §4/§5 | `face.UniqueReference` hoisted to a local before any use (clone-per-call trap); matching by the `Guid` pattern-matched off the concrete `GuidReference` (object instance identity); zero `==`/`!=` on interface-typed references in the change |
| Coding - Automatic Tests §2/§4 | Facts structure unchanged (partial `Facts`, no `using Xunit`, `<summary>`s); boundary tests on both sides; **Reproduce-Before-Fixing** for the acceptance fact; build-before-test (HintPath); the DB fact keeps the project's `Skip` + conf convention |
| Coding - GIS Administrative Data | untouched by this change; county keying stays on the `CountyId` identifier (no `code` anywhere) |
| Coding - PostgreSQL | untouched — no converter, DDL, batching or timeout change; the task's existing county-failure handling already surfaces the new throws |
| Coding - Deployed WebAPI | verification is read-only `GET`, sequential, 500-retried-in-isolation, and stays out of `DiGi.Test`; `itemsbycircle` parameter names confirmed live (`x`, `y`, `radius` — no `countyid`) |
| Coding - API Documentation | `documentation/API/` consulted for the `Modify` entry; the method's `<summary>` is rewritten and the project rebuilt so the generated entry stays in sync (docs regenerate on compile) |
| XML Documentation | `<summary>`/`<param>`/`<returns>` rewritten to describe the shell-based orientation; no `CS1591` |
| Coding - Editor Config | edits via `edit_file_tool` (byte-preserving, CRLF kept); no scripted line rewrites, no BOM changes |

---

## 6. Risks & open questions

1. **Parity needs a closed face set — and the failure mode is silent.** Ray-parity classifies a face's normal by the faces its ray crosses — sound for a closed volume, **arbitrary when the set is open along the ray**. Worse, the fallback is not a throw: `GetNormal` returns null, `SetNormal` leaves the stored normal untouched, and `CreateShell` discards `Update()`'s result — so an un-oriented face **silently keeps its stored (possibly inward) normal**, classified as if it were correct, with no throw and no skip count. This is the one way the new code can be wrong *and say nothing*. Mitigation: (a) fixtures use closed volumes by construction (§4.1), so the synthetic facts cannot be silently mis-oriented; (b) the 27-model verification (§4.3) is the safety net for deployed data — it compares row-for-row against #82, and #82's floor-point heuristic *always* re-orients (it never keeps a stored normal), so any deployed face that parity leaves un-oriented will show up as a row that differs from #82. A differing row is a data finding (that space's face set is open), to be reported before deciding whether the decision table needs a case for it.
2. **A single-space interior partition** (a wall inside one space's face set) would be mis-oriented by parity and, being in one shell, classified as external by this design. The #82 heuristic had the same exposure (arbitrary floor-point side). The verification will show whether any deployed model carries one; if so, the decision table needs a third case (component in one shell whose face set is non-convex around it) and the evidence decides the answer.
3. **Downward-facing `IRoof` (a ceiling)** now classifies with tilt > 90° (Above45 band, inverted azimuth) instead of the #82 flip (flat/up-to-20). Degenerate data (a ceiling stored as a roof); the verification confirms none in the 27 models.
4. **Vertical wall normal → North** (`Math.Atan2(0, 0) == 0`). The #82 plan text listed this as a skip, but the shipped code never had the check — the behaviour is kept as-is (the issue says "keep the sector/tilt rules"); noted here so the next reader does not "fix" it without a decision.
5. **`GetShells<Space>` type filter — low risk.** The deployed spaces are exactly `DiGi.Analytical.Building.Classes.Space` (verified in the fixture JSON). The filter is a runtime `is` check (`ValueCluster.GetValues<U>`), so a future pipeline emitting a **subtype** of `Space` is still found by `GetShells<Space>()`; only an *unrelated* `ISpace` implementation (not deriving from `Space`) would be missed — in which case the call widens to `GetShells<ISpace>()`. Not a blocker, noted for completeness.
6. **Clone costs.** `GetComponents` clones (fetched once, unchanged), `GetShells` clones the spaces, `Shell.PolygonalFaces` clones on access (read once per shell, §3.3). Bounded per model; the batch size (1000 references, task-side) is unchanged.
7. **The `FloorOnly` behaviour change.** A floor with **no** space now throws (uncovered) where #82 classified it. Deliberate: the shell is the single source of face data, and a floor that bounds no space is a space-structure defect. The fact's fixture gains a space; the change is recorded here and in the `<summary>`.
8. **Multi-space buildings.** Shared components are now excluded (internal). Expected zero impact on the deployed 27 models (one space per building — the verification asserts it via the skip count); for future multi-space data this is the semantically correct treatment of the *external* columns.

---

## 7. Execution order

1. **Facts 9–11 first, run against the current implementation** — record the pre-fix failures (evidence for the issue; Reproduce-Before-Fixing).
2. Rewrite `DiGi.GIS.PostgreSQL/Modify/Update_ExternalComponentsArea.cs` per §3 → `dotnet build` the library → run the full `Update_ExternalComponentsArea` fact set → green (12+ facts / 1 conf-driven skip, or the new equivalent).
3. Rewrite the method `<summary>` → rebuild → `documentation/API/DiGi.GIS.PostgreSQL/DiGi.GIS.PostgreSQL.md` regenerates in sync.
4. Fact 12 (`DeployedBuildingModels`) with the dev database via `GIS_PostgreSQL_Storage.conf` — read-only; report the skip count.
5. Deployed-models verification (§4.3) — 27 models, radius 100, sequential GETs; same rows as #82, total = sum of the 34 per row.
6. Full build, zero warnings; facts green.
7. Commit on `0.8.10` (or the branch the maintainer designates); update #84's checkboxes; leave #83's status to the maintainer.

---

## 8. Switch-over to `BuildingModel.GetExternalShell` (2026-09-18)

### Why

After §1–§7 landed (`5eecc6f`), the issue was re-scoped: the shell-based, `normalSide`-oriented face extraction is implemented **once** in `DiGi.Analytical.Building` and consumed here and by the glTF converter (DiGi.GLTF#1). That blocker, DiGi.Analytical#3, is closed — `BuildingModel.GetExternalShell(Side?, Orientation?, Orientation?, double) → Shell?` is on DiGi.Analytical `0.8.8` (`e90fa42`, `c4ede4e`), and DiGi.Geometry#6 (`cc87da1`, `0.8.9`) fixed the mirroring `Planar.Flip` applied to every turned face on both paths. This section records the switch; §3 stays as the record of the per-space design it replaces.

### What changed

- `Modify/Update_ExternalComponentsArea.cs` — the `GetShells<Space>(Side.External)` loop and the `face_ByComponentGuid` / `sharedComponentGuids` rebuild collapse to one `GetExternalShell(Side.External)` call and one `Guid → Face` map (`TryAdd`; a component twice in the envelope throws). The selection rule is read from where `GetExternalShell` states it; a component the envelope does not carry is resolved by its space count, taken from `GetRelation<SpaceRelation>(component).UniqueReferences_To.Count` — the same field the selection rule reads (`GetSpaces(component)` filters missing spaces and could disagree with it on a dangling reference). The clone `GetRelation` returns is paid only for components absent from the envelope.
- Sector and tilt rules, the 35 columns, total = sum of the 34, 0-vs-NULL, the `long` return and the background task are untouched.

| Component | Decision (§1 table, restated for the envelope) |
|---|---|
| In the envelope | classify from that face's normal and area |
| Absent, bounds 2 spaces | internal partition → counted skip (was: "in two shells") |
| Absent, bounds 0 spaces | throw (was: "no shell face carries it") |
| Absent, bounds 1 space — no polygonal face, or `GetExternalShell` answered `null` because the model has fewer than four external faces | throw; the message states the count, so this case is told apart from the orphan |
| Absent, bounds ≥ 3 spaces | throw (defect) |
| `null` envelope, no components | zero row (unchanged) |
| `null` envelope, only partitions | every component a counted skip, zero row — the per-component rule applied uniformly |

### Facts (DiGi.Test `0.8.11`)

- New: `Update_ExternalComponentsArea_EnvelopeBelowFourFacesThrows` — one space with a floor and two walls; asserts the throw names the reference and `bounds 1 space`. **Differential recorded against `5eecc6f`:** the pre-switch path threw as well, but with "a space shell carries no face" — the `Polyhedron` constructor silently keeps no face below four — so the fact distinguishes the two implementations by the message on the space count, not by throw-versus-classify (the plan's §4 expectation that the old path would classify a three-face shell was wrong; corrected here).
- Existing 9 runnable Facts unchanged in their assertions; two `<summary>` wordings updated. Suite: **10 passed / 1 conf-driven skip.**

### Deployed-model parity (read-only)

`GET https://api.digiproject.uk/gis/buildingmodel/itemsbycircle?x=638000&y=486000&radius=100` — the 27-model sample of §4.3 — classified by the pre-switch build (`5eecc6f`, rebuilt against DiGi.Analytical `c4ede4e` / DiGi.Geometry `cc87da1`) and by the post-switch build, 35 columns per row compared:

- 27/27 rows on both sides, **0 throws**, skip count **104 = 104** (the same partitions), every total equal to the sum of its 34 breakdowns, **every total identical** between the two builds.
- **6 of 27 envelopes are open** (`Query.ClosingTolerance` over the ladder `1e-6 … 0.2` answers `null`) — the one-in-six of `c4ede4e`. The closed ones close at 1e-6 (most), 0.001, 0.01, 0.02 and 0.05.
- **4 cells differ, all on one open-envelope model** (`38F62225-DEFC-F520-E053-CA2BA8C0BE14`, county 55417, total 6347.23 m² on both sides): two roof slivers of 0.077 m² and 0.023 m² move from the "above 45" band into the "up to 20" band of the same sectors — the envelope orients those two faces the other way than the per-space shell did. This is the §6.1 arbitrariness on an open face set, at sliver scale; it does not move a sector area and the row total is unchanged.

### Deferred (user decision: switch-over only)

The open-envelope signal — recording `ClosingTolerance == null` per model so a county's share of arbitrary-side faces is known — is not added here because it changes the `Modify` return contract and the task's counters. It is filed as a follow-up issue (see the closing comment on #84).
