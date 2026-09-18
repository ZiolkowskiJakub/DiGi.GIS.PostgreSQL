# Implementation Plan — DiGi.GIS.PostgreSQL #86

**The building-data run must not fail because a county's unassigned buildings could not be measured for their radial ratios — the miss on a subdivision does, and only that one gates the result.**

- Issue: https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/86 (open, `type: bug`, `priority: medium`, `ai: light`)
- Shown by: [#78](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/78) D5 — the national run of 2026-09-18 that went **Failed** on 2 unmeasured unassigned buckets after 19 h 34 min
- Invariant source: [#64](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/64) — `Query.SubjectCount` and the "subjects must come back in their own surroundings" rule
- Target: `DiGi.GIS.PostgreSQL/Classes/BackgroundTask/PostgreSQLBuildingDataUpdateTask.cs` (branch `0.8.10`, clean working tree)

---

## 1. Validity assessment

The issue is **still valid and actionable**. The pre-fix implementation is in the tree (branch `0.8.10`, HEAD clean), and every premise of the fix checks out against the code.

### Premises verified against code (with evidence)

| Issue claim | Status | Evidence |
|---|---|---|
| `ExecuteAsync` ends on a three-counter gate | ✅ | line 821: `return FailedSubdivisionCount == 0 && UnfulfilledUpdateTypeCount == 0 && RadialRatiosUnmeasuredSubdivisionCount == 0;` |
| `RadialRatiosUnmeasuredSubdivisionCount` is bumped in **four** places | ✅ | lines **405**, **434** (subdivision loop) and **596**, **617** (per-county fallback) |
| Two of the four are a **subdivision** miss; two are a county's **unassigned bucket** | ✅ | 405/434 sit in the `foreach (AdministrativeAreal2DReference …)` subdivision loop; 596/617 sit in the `foreach (int countyId in unassignedCountyScope)` fallback (buildings `subdivision_id IS NULL` or cross-county) |
| The name says "subdivision" but two cases are not a subdivision | ✅ | 596/617 count a **county's** unassigned-building group, not a subdivision — the property name is a lie for two of its four sites |
| `SubjectCount` makes "none back" and "stored box missing" indistinguishable for 1–2 subjects | ✅ | `Query.SubjectCount(building2Ds, building2Ds_Neighbour, countyId)` at lines 427 / 612; with 1–2 subjects the two faults are not separable by it (#64) |
| The 2026-09-18 national run went red on 2 unmeasured unassigned buckets | ✅ (as reported) | #78 D5 comment `5734230600`: county 17371 (2 unassigned buildings, 9 318 neighbours, none a subject) and county 90517 (1 unassigned, 623 neighbours); tray row **Failed** |
| The counters are public (readable) so a fact can drive and read them | ✅ | `{ get; private set; }` on all counters (e.g. line 90) — the **setter** is private, so a fact can read but not write them without the live run (see §4) |
| No other code reads `RadialRatiosUnmeasuredSubdivisionCount` | ✅ | search across `DiGi.GIS.PostgreSQL/` and `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/`: the only references are the 4 increments, the reset (107), the class `<summary>` (22), the property (90), the `<returns>` (97), the finish line (819), the return gate (821), and the generated `documentation/API/…/DiGi.GIS.PostgreSQL.Classes.md` (regenerates on compile). No other task, and no test, references it. |

### Design decisions the issue leaves open — this plan rules on them

| Decision | Ruling | Rationale |
|---|---|---|
| **Name of the new counter** — `RadialRatiosUnmeasuredUnassignedCountyCount` vs. a building count | **`RadialRatiosUnmeasuredUnassignedCountyCount`** (a `long`, `{ get; private set; }`) | The fallback bumps the counter **once per county** (per unassigned bucket), the same per-unit granularity as the existing per-subdivision counter — so "count of buckets" is the natural unit and keeps the increment a plain `++`. It is parallel to `RadialRatiosUnmeasuredSubdivisionCount`. A building count would parallel `UnassignedSubdivisionBuildingCount` but would change both fallback increments from `++` to `+= building2Ds_Unassigned.Count` — a larger change for no clarity gain in the gate or the finish line. |
| **Return gate expression** | **Text unchanged** — `… && RadialRatiosUnmeasuredSubdivisionCount == 0` | After the split, the fallback bumps the *new* counter, so the existing gate already covers only the subdivision cases. No edit to line 821 is needed; the gate's wording stays accurate. |
| **Log level at the two fallback sites** | **Stays `Warning`** (issue: "the unassigned counter stays a `[WRN]`") | No level change at 596 / 617; only the counter they bump changes. |
| **Finish line** | **Names both counters** | Extend `{UnmeasuredCount} subdivisions left without radial ratios` to name the subdivision count **and** the unassigned-bucket count. |
| **Docs** | Class `<summary>` (22) + `ExecuteAsync` `<returns>` (97) say **which counter fails the run and which does not**; the existing property `<summary>` (87–88) gains a contrast | The public API currently implies all four sites gate the run; after the split only two do. |
| **Whether the unassigned miss is a defect at all** | **Out of scope** | The issue defers it: for county 17371 the likely cause is the multi-part filing of #68 (sibling of 17372, one of #78's empty secondary parts). Not a code change here. |

---

## 2. Scope

**In scope (this issue):**

- `DiGi.GIS.PostgreSQL/Classes/BackgroundTask/PostgreSQLBuildingDataUpdateTask.cs`:
  - A new public counter `RadialRatiosUnmeasuredUnassignedCountyCount` (placed directly after `RadialRatiosUnmeasuredSubdivisionCount`, line 90).
  - Reset it to `0` at the top of `ExecuteAsync` (next to line 107).
  - Redirect the two fallback increments (lines **596**, **617**) to the new counter.
  - Extend the finish-line log (lines 808–819) to name both counters.
  - Update the class `<summary>` (line 22), the `ExecuteAsync` `<returns>` (line 97), and the existing property `<summary>` (lines 86–88) to state which counter fails the run and which does not.
- The generated `documentation/API/DiGi.GIS.PostgreSQL/DiGi.GIS.PostgreSQL.Classes.md` entry — **regenerates on compile**, not hand-edited.
- One new `[Fact]` in `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/PostgreSQLBuildingDataUpdateTask.cs` (the acceptance fact, `Skip`-ped like the other integration facts — see §4).

**Out of scope:**

- The two **subdivision** sites (405, 434) — they keep `RadialRatiosUnmeasuredSubdivisionCount` and keep failing the run.
- The return gate expression (line 821) — text unchanged.
- Log level at 596/617 — stays `Warning`.
- Whether the unassigned-bucket miss is a data defect (#68's multi-part filing) — deferred.
- The next national run — it reports on #78's successor, not here (code-only issue).

---

## 3. Implementation — `PostgreSQLBuildingDataUpdateTask`

All edits are in one file. Explicit typing, `camelCase` type-prefixed identifiers, English-only XML docs, `<para>` for paragraph breaks (no blank `///` lines), no `var`.

### 3.1 New property (after line 90)

```cs
/// <summary>
/// Gets the number of a county's unassigned-building buckets whose radial ratios could not be measured during the last run, so the radial columns of those buildings were left as they stood.
/// <para>Unlike <see cref="RadialRatiosUnmeasuredSubdivisionCount"/> this does not make the run incomplete and does not stop it being reported as succeeded. A bucket holds one or two buildings, and for that size "none of them came back" is indistinguishable, by the <c>SubjectCount</c> rule, from "a stored bounding box does not match its own geometry" - so the miss is a warning to read, not a failure to act on. The run's result is judged on the subdivision counter alone.</para>
/// </summary>
public long RadialRatiosUnmeasuredUnassignedCountyCount { get; private set; }
```

### 3.2 Reset (after line 107)

```cs
RadialRatiosUnmeasuredSubdivisionCount = 0;
RadialRatiosUnmeasuredUnassignedCountyCount = 0;
```

### 3.3 Redirect the two fallback increments

Line **596** (fallback, none of the unassigned buildings carries an outline) and line **617** (fallback, none of the subjects came back) both change from `RadialRatiosUnmeasuredSubdivisionCount++;` to:

```cs
RadialRatiosUnmeasuredUnassignedCountyCount++;
```

The `Serilog` lines beneath each stay as they are (`Warning`, "…for unassigned buildings…"). **Lines 405 and 434 are untouched.**

### 3.4 Finish line (lines 808–819)

Replace the trailing `{UnmeasuredCount} subdivisions left without radial ratios` with both counters named, and add the new counter as the last positional argument (Serilog binds `{Name}` placeholders to the arguments in order, so the argument order must match the placeholder order):

```cs
Serilog.Modify.Log(
    "{Type}: finished - {ProcessedCount} subdivisions written, {UnassignedCount} unassigned buildings written, {CrossCountyCount} cross-county buildings written, {RowCount} total rows, {FailedCount} failed, {SkippedCount} skipped for want of a parent county, {UnfulfilledCount} update types unfulfilled, {UnmeasuredSubdivisionCount} subdivisions and {UnmeasuredUnassignedCountyCount} unassigned buckets left without radial ratios",
    nameof(PostgreSQLBuildingDataUpdateTask),
    ProcessedSubdivisionCount,
    UnassignedSubdivisionBuildingCount,
    CrossCountySubdivisionBuildingCount,
    UpdatedRowCount,
    FailedSubdivisionCount,
    SkippedSubdivisionCount,
    UnfulfilledUpdateTypeCount,
    RadialRatiosUnmeasuredSubdivisionCount,
    RadialRatiosUnmeasuredUnassignedCountyCount);
```

### 3.5 Docs

- **Class `<summary>` (line 22)** — the radial-ratios paragraph currently reads "…<see cref='RadialRatiosUnmeasuredSubdivisionCount'/> counts the subdivisions that happened to, and also stops the run being reported as succeeded." Extend it to name the second counter and state that it does **not** fail the run (and why: at 1–2 buildings the miss cannot be told apart from a stored box that does not match its geometry).
- **`ExecuteAsync` `<returns>` (line 97)** — currently "…otherwise, false - including when a selected update type was counted against <see cref='UnfulfilledUpdateTypeCount'/> or a subdivision against <see cref='RadialRatiosUnmeasuredSubdivisionCount'/>." Append: "A county's unassigned buildings whose radial ratios could not be measured are counted against <see cref='RadialRatiosUnmeasuredUnassignedCountyCount'/> and do not affect the result."
- **Existing property `<summary>` (lines 86–88)** — add one sentence contrasting with the new counter: "The same miss on a county's unassigned buildings is counted separately, against <see cref='RadialRatiosUnmeasuredUnassignedCountyCount'/>; it is reported and does not fail the run."

### 3.6 Return gate (line 821)

**Unchanged:** `return FailedSubdivisionCount == 0 && UnfulfilledUpdateTypeCount == 0 && RadialRatiosUnmeasuredSubdivisionCount == 0;` — after §3.3 the fallback no longer touches `RadialRatiosUnmeasuredSubdivisionCount`, so this already gates on the subdivision cases only.

---

## 4. Tests — `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit`

One new `[Fact]` in the existing `partial class Facts` (`Facts/PostgreSQLBuildingDataUpdateTask.cs`), no `using Xunit`, XML `<summary>` on the method, English only.

### 4.1 The acceptance fact

```cs
/// <summary>
/// Verifies the run's result separates the two radial-ratio misses: a county whose unassigned buildings could not be measured for their radial ratios still reports success (<c>true</c>), while a subdivision whose radial ratios could not be measured fails the run (<c>false</c>).
/// <para>Both counters are private-set and are driven only through the live run, so the fact is <c>Skip</c>-ped like the other integration facts in this file: it needs the PostgreSQL configuration files pointing at a database whose data places the miss in exactly one of the two buckets - a county whose unassigned bucket misses and no subdivision does (the <c>true</c> case, counties 17371 / 90517 from the #78 D5 run) and a subdivision that misses and no unassigned bucket does (the <c>false</c> case).</para>
/// </summary>
[Fact(Skip = "Requires the PostgreSQL configuration files pointing at a database.")]
public async Task PostgreSQLBuildingDataUpdateTask_RadialRatiosUnmeasured_Result()
{
    GISPostgreSQLConverterManager? gISPostgreSQLConverterManager = Create.GISPostgreSQLConverterManager();
    Assert.NotNull(gISPostgreSQLConverterManager);

    // Case 1 (the defect the issue fixes): scope a county whose unassigned bucket misses
    // and no subdivision does. Assert the run reports success and the new counter is > 0.
    //   Assert.True(task.IsSucceeded);
    //   Assert.Equal(0, task.RadialRatiosUnmeasuredSubdivisionCount);
    //   Assert.True(task.RadialRatiosUnmeasuredUnassignedCountyCount > 0);

    // Case 2 (unchanged behaviour): scope a subdivision that misses and no unassigned
    // bucket does. Assert the run fails and the subdivision counter is > 0.
    //   Assert.False(task.IsSucceeded);
    //   Assert.True(task.RadialRatiosUnmeasuredSubdivisionCount > 0);
    //   Assert.Equal(0, task.RadialRatiosUnmeasuredUnassignedCountyCount);
}
```

**Why `Skip`-ped (and not a running assertion):** the counters have **private** setters, and the gate is computed inline in `ExecuteAsync` — the only way to make either counter non-zero is to run the task against a live database with data that places the miss in exactly one bucket. The issue's acceptance explicitly allows this ("…or be `Skip`ped like them with the reasoning in the summary"). It therefore documents the contract and is runnable by whoever has the dev database.

**Stronger form (optional, not required by the issue):** extract the gate into a testable helper —

```cs
private bool IsComplete() =>
    FailedSubdivisionCount == 0 &&
    UnfulfilledUpdateTypeCount == 0 &&
    RadialRatiosUnmeasuredSubdivisionCount == 0;
```

— return it from `ExecuteAsync`, and make the two counters' setters `internal` under an `InternalsVisibleTo("DiGi.GIS.PostgreSQL.xUnit")` so a **non-skipped** `[Fact]` can set `{ Unassigned = 2, Subdivision = 0 }` → assert `true`, and `{ Subdivision = 1 }` → assert `false`. This is a larger change (touches the public setter surface), which is why it is **not** the default — the issue is `ai: light` and its acceptance is satisfied by the `Skip`-ped fact. Choose it only if a running assertion on the gate is wanted.

### 4.2 Execution discipline (Automatic Tests §4)

The xUnit project reaches the code under test through `<Reference><HintPath>` — `dotnet test` runs against whatever was last built into the library's `bin`, so **build the changed library first**, then test:

```powershell
dotnet build "DiGi.GIS.PostgreSQL\DiGi.GIS.PostgreSQL.csproj" -c Debug -m:1
dotnet test  "DiGi.Test\DiGi.GIS.PostgreSQL.xUnit\DiGi.GIS.PostgreSQL.xUnit.csproj" -c Debug -m:1
```

Zero warnings in both projects. The new fact is `Skip`-ped, so the suite stays green without a database; the existing `PostgreSQLBuildingDataUpdateTask` facts (constructor, `InScopeSubdivisionIds`, the `Skip`-ped integration set) are unaffected because none of them reads `RadialRatiosUnmeasuredSubdivisionCount` (verified in §1).

---

## 5. Guideline compliance map

| Guideline | How the plan honours it |
|---|---|
| GitHub - Issues §2 (verify before implementing) | every premise in §1 checked against the source with line numbers; the #78 D5 figures cited as reported (the fix does not depend on them); the "no other consumer" claim verified by search across the library and the test project |
| GitHub - AI Issue Classification §3 | localised fix with an obvious solution → `ai: light` is defensible; see §6.1 for the borderline `ai: standard` case |
| Coding - General §1 (naming/typing) | explicit typing, no `var`, `camelCase` type-prefixed identifiers, the new member named after the unit it counts; block-scoped namespace untouched; `CancellationToken` last (the touched method signature is unchanged) |
| Coding - General §2 (anemic + static extensions) | the change stays inside the existing background-task class; no new services, managers or types — one counter property and its reset |
| Coding - General §1.12 (temporary code) | no `TODO [Marker]` — the split counter is the permanent design; nothing is bypassed, the mis-named site is corrected |
| Coding - General §1.14 (line endings) | edits via `edit_file_tool` (byte-preserving, CRLF kept); no scripted line rewrites, no BOM change |
| Coding - Automatic Tests §2/§4 | Fact structure unchanged (`partial class Facts`, no `using Xunit`, XML `<summary>`); **Reproduce-Before-Fixing** satisfied by the `Skip`-ped acceptance fact naming the live data (#78 D5) and the two scenarios; build-before-test (HintPath) stated in §4.2 |
| Coding - GIS Administrative Data | untouched — no county-code keying, no `administrative_areal_2d` / `building_2d` read change; the counters are in-memory tallies only |
| Coding - PostgreSQL | untouched — no converter, DDL, batching or `commandTimeout` change; the task's existing write/`PushAsync` handling is unchanged |
| Coding - API Documentation | `documentation/API/…/DiGi.GIS.PostgreSQL.Classes.md` consulted for the current `<summary>`/`<returns>`; the project is rebuilt so the generated entry (new property + updated docs) stays in sync — docs regenerate on compile |
| XML Documentation - Audit | `<summary>`/`<returns>` rewritten to match the new behaviour; `<see cref>` links added to the new counter; no `CS1591`/`CS1573`; no blank `///` lines; single `<summary>` per member (overwritten, not appended) |
| Coding - Editor Config | explicit typing, no `var`, block-scoped namespace; edits via `edit_file_tool` |

---

## 6. Risks & open questions

1. **Tier is borderline.** The fix is localised with an obvious solution (fits `ai: light`), but the run-success gate is core to the task's contract — if the reviewer reads it as "core business logic," `ai: standard` is the defensible re-tier (GitHub - AI Issue Classification §3: "err on the side of a higher tier if the task involves … core business logic"). Leave `ai: light` unless the reviewer prefers the higher tier; re-tier in the same `gh issue edit` call if so.
2. **The `Skip`-ped fact proves nothing by itself.** It documents the contract and is runnable with the dev database, but a green CI run will not have exercised it. If a *running* assertion on the gate is required, use the §4.1 optional helper extraction. This is the main place the acceptance could be read as wanting more than the default delivers.
3. **Counter unit — counties, not buildings.** The new counter counts **buckets** (one per county's fallback), so a county with 2 unmeasured unassigned buildings adds **1**, not 2. This matches the per-unit granularity of `RadialRatiosUnmeasuredSubdivisionCount` (one per subdivision) and keeps the increment a `++`. If a per-building figure is wanted in the finish line, the existing `UnassignedSubdivisionBuildingCount` already carries the building total — no new counter needed for that.
4. **The unassigned miss may not be a defect.** The issue defers this; for county 17371 the likely cause is #68's multi-part filing (sibling of 17372). This plan only stops a clean run going red on it — it does not make the miss disappear. The next national run will still show the non-zero `RadialRatiosUnmeasuredUnassignedCountyCount` in the finish line, and that is the signal to chase #68, not to re-run 19 hours.
5. **Serilog positional binding.** The finish line binds `{Name}` placeholders to arguments by order; §3.4 adds the new placeholder **and** the new argument in the same position (last). A mismatch would log the wrong number under the wrong name without failing — verify the rendered finish line once on a local run.

---

## 7. Execution order

1. Add the `RadialRatiosUnmeasuredUnassignedCountyCount` property + reset (§3.1, §3.2).
2. Redirect the two fallback increments to it (§3.3) — lines 596 and 617 only.
3. Extend the finish line to name both counters (§3.4).
4. Update the class `<summary>` (22), `ExecuteAsync` `<returns>` (97), and the existing property `<summary>` (86–88) (§3.5).
5. `dotnet build "DiGi.GIS.PostgreSQL\DiGi.GIS.PostgreSQL.csproj"` → zero warnings; `documentation/API/…/DiGi.GIS.PostgreSQL.Classes.md` regenerates in sync.
6. Add the acceptance fact (§4.1).
7. `dotnet test "DiGi.Test\DiGi.GIS.PostgreSQL.xUnit\DiGi.GIS.PostgreSQL.xUnit.csproj"` → green (new fact `Skip`-ped; existing facts unaffected).
8. Optionally, with the dev database: run the new fact un-skipped to exercise both scenarios against #78 D5's live data.
9. Commit on `0.8.10` (or the branch the maintainer designates); post the resolution comment on #86 per GitHub - Issues §3 (resolution + commits, summary of changes, tests, and the note that the next national run reports the non-zero unassigned-bucket count on #78's successor).
