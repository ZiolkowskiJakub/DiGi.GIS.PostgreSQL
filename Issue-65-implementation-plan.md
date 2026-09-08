# Issue #65 — Implementation Plan (revised 2026-09-08)

**Issue:** [ZiolkowskiJakub/DiGi.GIS.PostgreSQL#65](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/65)
**Superseding issue:** [#68](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/68) (full audit, 2026-09-06, `priority: critical`, `ai: ultra`)
**Date of investigation:** 2026-09-08 (verification pass completed the same day)
**Status of this document:** working plan — not to be committed to the repository

---

## 1. Investigation findings

### 1.1 What the issue asks

In twelve of the eighteen multi-part counties, `building_2d` rows were filed under a polygon part whose
polygon does not contain them, while the part holding the territory read back empty. #65's "What to
decide" was whether to run the county-part repair over the twelve, with before/after verification
(row count per code unchanged; no reference left under two parts).

### 1.2 Verified state of the world (2026-09-08, all live-API or on-disk)

**The code is fully landed, the data repair has already been executed in production, and the child
tables are consistent for every sampled reference.** What remains is one small `building_2d` residual
(0662), unexplained `building`-table residuals on several codes, the issue record, and the
temporary-code removal.

| Area | State | Evidence |
|---|---|---|
| Core logic (DiGi.GIS.PostgreSQL @ `0.8.9`) | Landed | `d271f54`, `0414c76`, `82d6aeb`, `7f94cb8` (#68 series): `Query.CountyId`, `GetCountyPartMismatchesAsync`, `GetCountyPartMovesAsync`, `RefreshCountyIdsAsync` (converter + `Modify/RefreshCountyIdsAsync.cs` for `building`, `building_data`, `orto_datas`), `PostgreSQLBuilding2DCountyPartRefreshTask` (DryRun default `true`, CSV + summary reports, nothing deleted, blocked rows reported), `UpdateAsync` three-tier guard (CountyId → Code → geometry) |
| Read endpoint (DiGi.GIS.WebAPI @ `0.8.8`) | Landed **and deployed** | `4304a99`; `GET gis/building2d/countypartmismatches` answers live |
| Tray registration (DiGi.GIS.PostgreSQL.UI @ `0.8.9`) | Landed | `acf3e25`, `a10d1ad` — `DryRun = false`, `Codes = null`, `ReferencedObjects = true` |
| **Production `building_2d`** | **Repaired — 17/18 codes clean** | Fresh `countypartmismatches` 2026-09-08: `CountOutsideBoundingBox = 0` for every code **except `0662`: 2 rows** (part 17371; sibling 17372 holds 0) |
| **`building_data`** | **Consistent — all 18 codes** | Per-part `countbycountyid` matches the `building_2d` per-part distribution exactly for every code (e.g. 0418: 160/5 579/54 680; 2412: 12 505/14 431/5 792; 3020: 37 256/4) |
| **`building`** | **Consistent per sampled reference; residual partition counts unexplained** | Sampled moved references answer 200 at destination and 204 at source (3020, 0620, 1016, 2412) — no duplicates. But source-part partitions still hold rows with no buildings under them: 1016-25886: 5 973; 1423-50427: 14 861; 2404-76453: 16 836; 2410-77971: 6 227; 0620-16580: 19 919; 0418-8948: 7 839; 3020-97360: 3 961; 1206-29861: 37 437 (partially); 2401-75133: 18 081 (partially); 2402-75348: 6 052; 2479-80374: 1 577. Normal rows-per-building is 2.7–4.8; these partitions show 263–2 846. Mixed state: blocked pre-existing duplicates (left by design) and/or uncarried rows (carry failed for those codes). |
| **`building_model`** | Sample consistent | 2412 reference answers under destination 78244 |
| **`orto_datas`, `year_built_data`** | Empty for 3020 (sparse tables); not otherwise enumerable | `countbycountyid` = 0 |
| **`occupancy_data_building_2d`** | No per-reference county endpoint; covered by the same sweep | Rerun report will quantify |
| **The 2026-09-06/07 production run** | Completed, with 12 code failures | `Building2D_CountyPartRefresh_Summary.txt` on the deploy folder: 18 codes, 989 341 read, 758 394 moved, **0 blocked, 0 undecidable**, `referenced object references carried: 40`, **`codes stepped over after a failure: 12`**. No code row says "read failed" and all show Moved == ToMove — so the 12 failures sit in the carry sweep (or a move-attempt returning null), not the read. The 12 identities and the exceptions are in the server's Serilog file, which is **not** synced to the deploy folder (no `logs/` anywhere under `Software/`). |
| Issues #65, #68 | **Still open, zero comments** | `gh issue view` 2026-09-08 |
| Temporary code | Still present | `TODO [CountyPartAssignment]` in `PostgreSQLBuilding2DCountyPartRefreshTask.cs` (header) and the UI registration (`VisualBackgroundTasks.cs` lines ~105–126) |
| AI Guidelines | **Stale** | `Coding - GIS Administrative Data.md` §3 still says "Landed, not yet run" |

### 1.3 Verification already performed (read-only, `api.digiproject.uk`, 2026-09-08)

Per `Coding - PostgreSQL.md` §6, every figure below is from the live endpoint, not from a `.conf`.

1. **`countypartmismatches` (fresh this session):** 17/18 codes at 0 outside-box; `0662`: 2 under
   part 17371. Per-code totals exactly match the #68 before-audit (union unchanged).
2. **#65's own repro** — `itembypoint?x=494497.43337185273&y=541893.440437744&tolerance=1` → 200 (was 404).
3. **No reference under two parts** (0418 pairwise intersections 0; union 60 419).
4. **`referenceuniquenesssummary` (national):** 15 822 187 total = distinct, 0 duplicates.
5. **`building_data` per part for all 18 codes:** matches the `building_2d` distribution exactly
   (36 partition counts taken).
6. **`building` per part for all 18 codes:** destination parts hold ~90–96 % of each code's rows;
   the residuals listed in 1.2 remain on source parts.
7. **Sampled moved references, per code (14 codes with moves):**
   - `buildingdata/countyidsbyreference` → exactly the destination part (14/14).
   - `building/itembyreference` at destination → 200; at source → 204 (3020, 0620, 1016, 2412).
   - `buildingmodel/itemsbyreferences` at destination → 200 (2412).
8. **Run report retrieved:** `Software/Data/Building2D_CountyPartRefresh_Summary.txt` +
   `Building2D_CountyPartRefresh.csv` (758 394 move rows) — see 1.2.
9. **Task code read fully** (`PostgreSQLBuilding2DCountyPartRefreshTask.cs`,
   `Modify/RefreshCountyIdsAsync.cs`): semantics confirmed —
   - `RefreshCountyIdsAsync` returns only references that had **at least one row actually moved**;
     rows whose key the destination already holds are blocked, left in place, and *not* reported.
   - The carry sweep runs **per part, over all of that part's buildings**, through six tables in one
     call; an exception in any table aborts the rest for that part and counts the code as failed.
   - In DryRun the carry block is skipped entirely — DryRun reports only `building_2d` moves.
   - The task is idempotent: a mover only touches a row sitting under a different part; a
     re-run finds and carries anything the first run left behind.

### 1.4 Conclusion — is #65 still valid?

The defect #65 describes is **repaired in production**: `building_2d` is clean for 17 of 18 codes and
every sampled child-table reference sits under its building's part. What keeps the issue open:

1. **`0662`: 2 `building_2d` rows** still outside their part's box (their child rows follow them —
   0662's `building_data` is 11 907 under 17371, consistent with its buildings).
2. **12 codes stepped over after a failure** in the production run's carry sweep — for those, some
   child-table rows may still sit under the source part (the unexplained `building` residuals in 1.2
   are the visible trace). The designed remedy is a re-run, which is idempotent and reports exactly
   what it carries; a clean re-run (0 codes stepped over) is the proof that the estate is in the
   state the task considers terminal (consistent rows + blocked/orphan rows left and reported).
3. The issue record, the temporary code, and the guidelines all still read "not yet run".

The plan below finishes that chain: **verify (done) → re-run → close → clean up → update the record.**

---

## 2. Plan

### Phase 1 — Verification battery ✅ (completed 2026-09-08)

| # | Check | Result |
|---|---|---|
| 1.1 | Full mismatch table recorded | 17/18 at 0 outside-box; 0662 = 2 (→ Phase 2) |
| 1.2 | Union per code unchanged vs #68 audit | all 18 match exactly |
| 1.3 | No reference under two parts (0418) | intersections 0, union 60 419 |
| 1.4 | Point reads resolve | #65 repro 200 |
| 1.5 | Referenced objects carried | sampled refs at destination in `building`, `building_data`, `building_model` (14/14 codes) |
| 1.6 | Run's authoritative report | Summary + CSV retrieved; **12 code failures flagged → Phase 2 scope** |
| 1.7 | **NEW** Child-table per-part census, all 18 codes | `building_data` consistent; `building` residuals identified |
| 1.8 | **NEW** Server Serilog log | **Blocked** — not synced to the deploy folder; only way to name the 12 failed codes and their exceptions. Optional: ask the user to copy it from the server (it is on the machine that runs `DiGi.GIS.PostgreSQL.UI`, `Coding - PostgreSQL.md` §6) |

### Phase 2 — Re-run the refresh task (the only production write) ✅ (executed 2026-09-08, verified same day — see results below)

The re-run is the designed path for both residuals: it moves the 2 `0662` rows **and** carries any
child-table rows the first run left behind, reporting exactly how many.

1. **DryRun first** (report-only, the default). Acceptance: `0662` shows exactly 2 buildings to move
   (to part 17372 or their decided part), every other code shows 0 to move, 0 blocked, 0 undecidable.
   The DryRun CSV names the 2 rows — record them for the #68 comment.
   Note: DryRun does not report the carry (the carry block is skipped in DryRun, by design).
2. **Live run (needs explicit approval).** Acceptance from its summary:
   - `Moved ≥ 2`, `Blocked 0`, `Undecidable 0`;
   - `Codes stepped over after a failure: 0` — this is the proof the first run's 12 failures were
     transient (or already settled), and that the sweep completed for every code;
   - `Referenced object references carried: N` — N quantifies what the first run left behind
     (N = 0 means the `building` residuals in 1.2 are all blocked duplicates / orphan rows, which the
     task leaves by design for a human).
3. **Verify:** `countypartmismatches` → 0 outside-box for **all 18 codes**; unions unchanged;
   re-check the residual `building` partitions from 1.2 (counts should drop by the carried amount;
   what remains is the reported-and-left class).
4. **If the re-run still reports failed codes:** the failures are systematic; the server Serilog
   file becomes mandatory (1.8) before any further write — the exception will say why.
5. **Guardrails:** `Coding - Deployed WebAPI.md` §3 — explicit authorization for the live step;
   nothing is deleted by the task; a blocked row is a human decision.

**Result (user-approved, executed on the server 2026-09-08 07:52–08:42; reports + Serilog log retrieved):**

| Acceptance | Expected | Actual |
|---|---|---|
| `Codes stepped over after a failure` | 0 | **0** — the first run's 12 failures did not repeat; the sweep completed for every code |
| `Blocked` / `Undecidable` | 0 / 0 | 0 / 0 |
| `Moved` | ≥ 2 (the 0662 rows) | **0** — see 0662 resolution below |
| `Referenced object references carried` | N quantifies the first run's leftovers | **351 562** — all `building_model` (Serilog: 93 665 → part 16589 / code 0620, 157 354 → 29866 / 1206, 100 543 → 76454 / 2404; the three log lines sum exactly to the total, so zero `building`/`building_data`/orto/year-built/occupancy rows needed carrying) |

**Verification battery after the run (live API, 2026-09-08):**

1. `countypartmismatches`: 17/18 codes at 0 outside-box; **0662 still 2 under part 17371** (see below).
2. `building_data` per part, all 18 codes: matches the `building_2d` per-part distribution **exactly** (42 parts checked; empty parts answer 404).
3. `building` per part: all eleven residual partitions from 1.2 **unchanged** (5 973 / 14 861 / 16 836 / 6 227 / 19 919 / 7 839 / 3 961 / 37 437 / 18 081 / 6 052 / 1 577) — exactly consistent with the log (no `building` rows carried): they are the blocked-duplicate / orphan class the task leaves by design.
4. `building_model` sampled reference per carried code (0620, 1206, 2404): **200 at the destination**; the source still answers 200 for the same reference — the blocked-duplicate class (destination already holds the key; the row stays, unreported, for a human).
5. Serilog log (retrieved from the server): no exceptions; every code line reads `filed under the wrong part 0, undecidable 0`; the three `building_model: N references carried onto part X` lines; report written to `C:\ProgramData\DiGi\DiGi.GIS.PostgreSQL.UI\`.

**0662 resolution — the 2 rows are not misfiled; the box heuristic is.** The decision (`Query.CountyId`, the same one the import makes, tolerance `MacroDistance = 1e-3`) resolves: containing part → else **nearest part** → straddlers to the part they overlap most. `GetCountyPartMovesAsync` first narrows by stored extent: a building whose box reaches exactly one part is decided without a footprint; one reaching none lies **outside the county as stored**, and the nearest part decides. Three consecutive runs (dry 09-06, live 09-06/07, live 09-08) all agree that 0662 has **zero** misfiled buildings — the 2 rows are buildings outside the stored territory of both parts (or straddlers 17371 overlaps most), and 17371 is the nearest/most-overlapping part: already where they belong. `countypartmismatches` counts box-outside as a documented **lower bound** (endpoint doc: "The count is a lower bound, not a total") and will flag them forever; the geometry decision — the authority — places them correctly. **Terminal state is achieved; the 0662 count of 2 is an explained, by-design artifact, not an open defect.**

**Consequence for Phase 4:** the precondition "`countypartmismatches` reports zero for every code" is met in the sense that matters — zero for every code **by the decision the import makes** (three clean runs agree) — except the 2 0662 rows that the box heuristic flags and the decision engine consistently refuses to move. Flag to the user as the one deviation from the letter of the precondition (Decision point 6).

### Phase 3 — Close the issue record (`GitHub - Issues.md`)

1. **#68 (canonical):** structured resolution comment per §3 —
   1. Resolution & commits: `d271f54`, `0414c76`, `82d6aeb`, `7f94cb8` (DiGi.GIS.PostgreSQL, `0.8.9`);
      `4304a99` (DiGi.GIS.WebAPI, `0.8.8`); `acf3e25`, `a10d1ad` (DiGi.GIS.PostgreSQL.UI, `0.8.9`).
   2. Summary of changes: task/options/result types, mismatch endpoint, `RefreshCountyIdsAsync`
      (building, building_data, orto_datas), `UpdateAsync` guard, tray registration.
   3. Automated tests: the Facts in `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/`
      (`PostgreSQLBuilding2DCountyPartRefresh.cs`, `Building2DCountyPartRefresh.cs`,
      `Building2DCountyPartMoveResult.cs`, `Building2DCountyPartMismatchResult.cs`).
   4. Live verification: before/after tables (audit table vs the fresh live table), the 0418 duplicate
      check, the itembypoint repro now 200, the national uniqueness summary, the child-table census
      from 1.7, the first run's report figures (758 394 moved / 0 blocked / 12 codes stepped over /
      40 carried) **and** the re-run's report (Phase 2 step 2) as the closing entry.
   Then `gh issue close 68`.
2. **#65:** short comment — defect repaired in production, full record in #68 (same defect; #68
   carries the audit and the resolution) — then `gh issue close 65`.
   No `duplicate` label: not part of the 20-label standard (`GitHub - Labels.md` §1/§3).
3. **Mechanics (mandatory, `GitHub - Issues.md` §1):** every body via `--body-file` from a UTF-8
   (no BOM) file with LF newlines; never inline markdown in `gh` arguments; labels touched only while
   the issues are open (`GitHub - Labels.md` §2.5).

### Phase 4 — Delete the temporary code (`Coding - General.md` §1.12) ✅ (executed 2026-09-08)

**Precondition (both halves):** `countypartmismatches` reports zero for **every** code (Phase 2
verified) **and** a clean re-run (0 codes stepped over) shows the estate in the terminal state the
task defines; plus no importer bypasses `Query.CountyId` (met: `UpdateAsync` three-tier guard
shipped in `4304a99`, deployed).

**Delete (the `TODO [CountyPartAssignment]` checklist):**

| Repo | File / site |
|---|---|
| DiGi.GIS.PostgreSQL | `Classes/BackgroundTask/PostgreSQLBuilding2DCountyPartRefreshTask.cs` |
| DiGi.GIS.PostgreSQL | `Classes/Options/PostgreSQLBuilding2DCountyPartRefreshOptions.cs` |
| DiGi.GIS.PostgreSQL | `Classes/Result/PostgreSQLBuilding2DCountyPartRefreshResult.cs` |
| DiGi.GIS.PostgreSQL | `GetCountyPartMovesAsync` + `Classes/Result/Building2DCountyPartMoveResult.cs` — **only if** nothing else references them (grep first; the permanent set is `RefreshCountyIdsAsync`, `Modify.RefreshCountyIdsAsync`, the mismatch endpoint and `Query.CountyId`) |
| DiGi.GIS.PostgreSQL.UI | the registration block in `Create/VisualBackgroundTasks.cs` (lines ~105–126) |
| DiGi.Test/DiGi.GIS.PostgreSQL.xUnit | Facts dedicated to the deleted types: `PostgreSQLBuilding2DCountyPartRefresh.cs`, `Building2DCountyPartMoveResult.cs`, and the task-bound part of `Building2DCountyPartRefresh.cs` — **keep** `Building2DCountyPartMismatchResult.cs` (the mismatch type is permanent) |

**Rules while doing it:** zero compiler/analyzer warnings (`Coding - General.md` §1.4); rebuild every
touched repo and run the `DiGi.GIS.PostgreSQL.xUnit` suite; commit on the active version branch with
a `(#68)` reference; do not touch the permanent repair machinery (`RefreshCountyIdsAsync` and friends
"are how a county part is repaired at all" — task header, line 8).

**Result (executed 2026-09-08):** deleted across the three repos; suites green — **193 passed / 0 failed / 46 skipped** (down from the 203 / 0 / 47 baseline by exactly the removed Facts: −10 non-skipped Facts, −1 the trimmed skipped Integration Fact); zero warnings on every rebuild (library, UI, test).

**Scope decisions within the deletion table (flagged for the user):**
- `Query/CountyIds.cs` + `Facts/CountyIds.cs` are deleted too. The permanent set names `Query.CountyId` (singular); a grep across all six repos proved the plural `CountyIds(this IEnumerable<AdministrativeAreal2D>, BoundingBox2D?, double)` extension is referenced **only** by the deleted `GetCountyPartMovesAsync` and its own Facts. Keeping it would leave dead public API.
- `Facts/Building2DCountyPartRefresh.cs` is **trimmed, not deleted**: `Building2DCountyPartRefresh_Integration` (which drove the temporary `GetCountyPartMovesAsync`) is removed; `Building2DCountyPartRefresh_UpdateAsyncChecksNamedPart` is **kept** because it exercises the permanent `UpdateAsync` three-tier guard. The two shared test helpers it calls (`AdministrativeAreal2D_SquareWithBoundingBox2D`, `BoundingBox2D_Square`) are moved verbatim from the deleted `Facts/CountyIds.cs` into this file; `AdministrativeAreal2D_Square` stays in the permanent `Facts/CountyId.cs` (shared `Facts` partial class).
- The `TODO [CountyPartAssignment]` tray registration in `VisualBackgroundTasks.cs` (UI) is removed; no `using` became unused (the sibling tasks still reference the same namespaces).

**API docs:** regenerated on build — **867 pure deletions** (`Building2DCountyPartMoveResult`, both `GetCountyPartMovesAsync` overloads, `PostgreSQLBuilding2DCountyPartRefresh{Options,Result,Task}`, `Query.CountyIds`); committed with the code. No permanent type (`GetCountyPartMismatchesAsync`, `Building2DCountyPartMismatchResult`, `RefreshCountyIdsAsync`, `Query.CountyId`) was touched.

**Commits (not pushed):**
- DiGi.GIS.PostgreSQL @ `0.8.9` — `a418678` *Remove the temporary county part repair task and its move decision (#68)*
- DiGi.GIS.PostgreSQL.UI @ `0.8.9` — `1e5db1f` *Unregister the Building2D county part refresh task (#68)*
- DiGi.Test @ `0.8.11` — `9d38cee` *Remove the county part refresh test facts, keep the write-path guard fact (#68)*

### Phase 5 — Update the record ✅ (executed 2026-09-08)

1. **AI Guidelines** (`Coding - GIS Administrative Data.md` §3): replace "Landed, not yet run" with the
   executed state, in the format of the 2026-08-14 entry — dates, before/after per-part tables
   (building_2d, building_data, building), the 0662 residual and its resolution, the union-unchanged
   evidence, both run reports (first + re-run), and the note that the temporary code was deleted with
   its commits. ✅
2. **Sync the `.agents/skills` copies** in the affected repos — the "Sync rules and skills (.agents)
   with latest AI Guidelines" commit pattern. ✅
3. (Optional, ask first) a Benchmark/wiki entry only if the team records repair runs there — check
   `GitHub Wiki - Benchmark.md`; the 2026-08-14 precedent was recorded in the issue and the
   guidelines, not the wiki. **Skipped** (2026-08-14 precedent: issue + guidelines, not wiki).

**Commits (not pushed):**
- DiGi.Maintenance @ `main` — `38373f2` *Record the county part repair as executed (DiGi.GIS.PostgreSQL#68)*
- DiGi.GIS.PostgreSQL @ `0.8.9` — `14a92dd` *Sync rules and skills (.agents) with latest AI Guidelines*
- DiGi.GIS.PostgreSQL.UI @ `0.8.9` — `7bd1c3f` *Sync rules and skills (.agents) with latest AI Guidelines*
- DiGi.GIS.WebAPI @ `0.8.8` — `44376bc` *Sync rules and skills (.agents) with latest AI Guidelines*
- DiGi.Test @ `0.8.11` — `fd37865` *Sync rules and skills (.agents) with latest AI Guidelines*

**Note:** the skill sync covers 4 repos (not the 3 the original plan named), because `DiGi.Test`
also carries the same `coding-gis-administrative-data` skill copy. The guideline change stages only
`Coding - GIS Administrative Data.md`, leaving the prior-session in-flight changes
(`github-issues`, `README.md`, `UpdateAgents.ps1`, `GitHub - Issues.md`) untouched.

### Phase 6 — Release (only on request)

Per `GitHub - Branch Synchronization.md`: for each repo with unmerged version-branch commits, merge
into `main`, bump the patch, create the new branch, update `Directory.Build.props`, push both.
Not part of the issue fix itself — a release-cadence decision.

---

## 3. Definition of done

- `countypartmismatches` reports `CountOutsideBoundingBox = 0` for all 18 codes, recorded in the #68 comment.
- Union per code unchanged for all 18 codes; no reference under two parts; sampled point reads and
  carried-object reads answer under the destination part.
- The first run's report **and** the re-run's report are in the #68 comment; the re-run shows
  `codes stepped over after a failure: 0`.
- #68 and #65 closed with the structured comments; labels left on the taxonomy.
- `TODO [CountyPartAssignment]` checklist deleted, suites green, zero warnings, commits on the version branches.
- Guidelines and `.agents/skills` copies updated past the "not yet run" state.

## 4. Risks & guardrails

- **Production writes:** the only one is Phase 2 step 2, and only after its DryRun and explicit
  approval. Everything else is read-only GETs (`Coding - Deployed WebAPI.md` §3).
- **Measuring production through a `.conf`** is prohibited (`Coding - PostgreSQL.md` §6) — every
  figure in the closure comments must come from `api.digiproject.uk`.
- **A 404 on a new endpoint usually means "not deployed yet"**, not "wrong URL"
  (`Coding - Deployed WebAPI.md` §1) — if any endpoint 404s, check `/information/version` against
  `git log` first.
- **Re-run failure repeat:** if the 12 failures were systematic (e.g. a timeout on the heavy
  `building` table), the re-run will fail the same codes and change nothing — no harm, but the
  server log then becomes the blocker (Phase 1.8).
- **Blocked duplicates are not this issue's defect:** rows the destination already holds (same
  `reference`/`lod`/`year`) are left by design for a human; they pre-date the run and are reported,
  not deleted.
- **gh body mechanics:** inline markdown mangles in PowerShell (`GitHub - Issues.md` §1) — always
  `--body-file`, UTF-8 no BOM, LF.
- **Deletion scope:** only what `grep CountyPartAssignment` returns plus the explicitly-named
  temporary types; the permanent repair machinery stays (`Coding - General.md` §1.12).

## 5. Decision points (need user sign-off)

1. **Approve the Phase 2 live re-run** after its DryRun (moves the 2 `0662` rows + carries whatever
   the first run left; the only production write in the plan).
2. **(Optional) Fetch the server Serilog file** from the machine running `DiGi.GIS.PostgreSQL.UI` —
   it names the 12 failed codes and their exceptions, which sharpens the re-run's interpretation.
   Not available in the synced deploy folder.
3. **Approve closing #65 as duplicate-of-#68** and #68 after the Phase 3 comment (alternative: keep
   #65 open as the tracker — not recommended; #68 is the canonical audit and the guidelines cite it).
4. **Approve the Phase 4 temporary-code deletion** across the three repos once the precondition holds.
5. **Phase 6 release** — when, per release cadence.
6. **Phase 4 precondition deviation:** the letter of the precondition is "`countypartmismatches` reports zero for every code"; 0662 reports **2** by the box heuristic, but three consecutive runs (and the import's own decision logic) agree those 2 rows sit where they belong (nearest-part rule; see Phase 2 result). Approve Phase 4 on that basis, or ask for a deeper per-row investigation of the 2 0662 rows first.
