# Issue #67 — Implementation Plan (2026-09-08)

**Issue:** [ZiolkowskiJakub/DiGi.GIS.PostgreSQL#67](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/67)
**Split from:** [#64](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/64) (radial ratios — fixed and closed) · related county-part re-filing in [#68](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/68)
**Labels:** `type: bug` · `priority: medium` · `ai: standard`
**Date of investigation:** 2026-09-08
**Status of this document:** working plan — **not to be committed to the repository** (kept beside `Issue-65-implementation-plan.md`)

---

## 1. Investigation findings

### 1.1 What the issue asks

In counties populated from a standing start, `Calculated occupancy` is unwritten (0–14 % filled)
against 57–67 % in established counties, and for `2404` (part 76453) the column is absent from the
catalogue entirely. The radial columns in the same counties are complete, so this is **not** the #64
radial-ratio cause. Three things to determine:

1. Does `building_2d_occupancy_data` hold rows for these counties at all (coverage gap vs. defect)?
2. If it does, why does the update leave the column unwritten — and why is it unregistered for 76453?
3. Does any downstream consumer read `Calculated occupancy` assuming coverage? (`Is occupied` and
   `Subdivision occupancy` sit in the same group and must be measured together.)

### 1.2 Verified state of the world (2026-09-08, on-disk / git)

**The code defect is already fixed, committed, and covered by an integration fact.** What keeps the
issue open is the production re-run + re-measure, a possible data-coverage follow-up, and the record.

| Area | State | Evidence |
|---|---|---|
| Pairing fix (DiGi.GIS.PostgreSQL @ `0.8.9`) | **Landed, committed** | `git blame`: `PostgreSQLUpdateOccupancyTask.cs:307–308` in commit `7f94cb8` (2026-09-08). The building phase now derives the in-scope subdivision set through `Query.SiblingCountyGroups` + `Query.InScopeSubdivisionIds` — the same rule `PostgreSQLBuildingDataUpdateTask.cs:220,225` scopes its runs by — so a building and the subdivision holding its occupancy pair across sibling parts, and the record still files under the part holding the building |
| Shared helpers | **Landed, permanent** (not temp code) | `Query/InScopeSubdivisionIds.cs` (`1abf92b`, #53), `Query/SiblingCountyGroups.cs` (`d46deec`, #58). Both already documented in `documentation/API/...` — regenerated, no pending doc delta |
| Automated coverage | **Written, skipped by default** | `DiGi.GIS.PostgreSQL.xUnit/Facts/PostgreSQLUpdateOccupancyTask.cs:85` — `PostgreSQLUpdateOccupancyTask_Building2D_SiblingCountyPart_Pairing_Integration`, `[Fact(Skip = "Requires ... a database populated ... the multi-part county layout.")]`. It clears + rebuilds both occupancy datasets and asserts a cross-part building reads back a non-empty occupancy record. Not exercised unless a populated test DB is pointed at |
| Working tree | **Clean** for the fix files | `git status`: only `?? Issue-65-implementation-plan.md`. No uncommitted or `TODO [Marker]` code tied to #67 (the one live marker in the repo is `[ReferencedObjectIndexes]` for #6; #65's `[CountyPartAssignment]` is already deleted) |
| Branch position | `0.8.9` is 29 ahead of `main` | the fix ships on the active version branch, not yet merged/released |
| Downstream exposure | **Limited** (per the 2026-09-07 audit) | `Is occupied` and `Subdivision occupancy` read 100 % across all twelve affected codes; only `calculated_occupancy` consumers are affected. **To confirm by code audit in Phase 1** that no feature treats a NULL `calculated_occupancy` as a value |
| #68 re-filing caveat | **Live — shapes the re-measure** | Since 2026-09-06, #68 re-filed `building_2d` under the geometric parts (e.g. `3020`: 37 256 of 37 260 now under 97358) while most `building_data` partitions still hold the rows under the **old** parts. A post-fix measurement must target the part where `building_data` actually lives, or it will read the wrong partition and report a false gap |
| Issue record | **Open, one comment** | the 2026-09-07 root-cause comment (two stacked failures + "Remaining (operational, not code)"); no code change is proposed for the residual |

### 1.3 Conclusion — is #67 still valid?

The **code defect** (point 2 of the issue — the cross-part pairing failure that left
`calculated_occupancy` unwritten and unregistered for 76453) is **fixed and committed on `0.8.9`**,
with a (skipped) integration fact and regenerated API docs. No temporary code needs removing.

What keeps the issue open, and what this plan finishes:

1. **The production re-run has not been executed.** The fix only takes effect once
   `PostgreSQLUpdateOccupancyTask` (both phases) and `PostgreSQLBuildingDataUpdateTask` (Occupancy type)
   are re-run against the estate — the only production write in this plan.
2. **The re-measure has not been taken.** `CheckBuildingDataColumns.ps1` must be re-run against the
   2026-09-06 table (now targeting the correct part, per the #68 caveat), with `Is occupied` and
   `Subdivision occupancy` measured in the same pass.
3. **A data-coverage follow-up may materialize.** If the re-measure lands as zeros rather than values,
   the source BDOO `ot:liczbaMieszkancow` is missing for those counties — a **new** data-availability
   issue, not this defect (point 1 of the issue).
4. **The record** (issue comment/close; guideline entry if it carries an occupancy-gap statement).

So #67 is still valid **as a verification-and-closure task, not a code task.** The plan below does not
write new C#; it verifies the landed fix, executes the operational re-run with a guardrail, re-measures,
and closes the record.

---

## 2. Plan

### Phase 1 — Confirm the fix is complete (read-only + local build)

1. **Code audit of downstream consumers (point 3 of the issue).** Grep the estate's consumers of
   `calculated_occupancy` / `Calculated occupancy` (WebAPI building-data reads, any feature that
   derives from it) and confirm none treat a NULL as a value or assume coverage. `Is occupied` and
   `Subdivision occupancy` already read healthy, so the expected outcome is "no consumer is harmed";
   record the grep result in the closing comment.
2. **Build + suite (local).** Rebuild `DiGi.GIS.PostgreSQL` and run `DiGi.GIS.PostgreSQL.xUnit`.
   Acceptance: green, **zero compiler/analyzer warnings** (`Coding - General.md` §1.4). The
   cross-part pairing Fact is `[Skip]`-by-default (`Coding - Automatic Tests.md`) — it is exercised
   instead by the production re-measure in Phase 2, so a green local suite does not prove the pairing;
   do not read its skip as verification.
3. **Confirm no temp code.** `grep -rn "TODO ["` returns nothing tied to #67 (verified 2026-09-08) —
   so there is no deletion phase, unlike #65.
4. **Confirm the API docs are current** — `documentation/API/DiGi.GIS.PostgreSQL/...` already carries
   `Query.InScopeSubdivisionIds` and `Query.SiblingCountyGroups` (verified), so no doc regeneration is
   owed for the fix itself.

### Phase 2 — Re-run the occupancy pipeline (the only production write; needs explicit approval)

Guardrails (`Coding - Deployed WebAPI.md` §3, `Coding - PostgreSQL.md` §6): `api.digiproject.uk` is
live production — read-only GETs are safe, the re-run is a write and proceeds only after explicit
approval; every figure in the closure comment comes from the live estate, never a `.conf`.

1. **Re-run `PostgreSQLUpdateOccupancyTask` (both phases, full rebuild).** Fills
   `building_2d_occupancy_data` across every part (now pairing across siblings) **and** the 204-row gap
   in `occupancy_data_administrative_areal_2d`.
2. **Run `PostgreSQLBuildingDataUpdateTask` with the `Occupancy` update type** for the affected parts.
   Writes `calculated_occupancy` and registers the column for 76453. Scope the parts the same way the
   task already does — by the sibling county group, not a single part id
   (`Coding - GIS Administrative Data.md` §4: "Need every building of a county → query each part id").
3. **Re-measure (the closing evidence).**
   ```powershell
   PowerShell -ExecutionPolicy Bypass -File "DiGi.Maintenance/Scripts/CheckBuildingDataColumns.ps1" -CountyId <id> -Distribution -Column "Calculated occupancy" -All
   ```
   - Run **per county, sequentially**, never several at once (the #64/#68 method note).
   - Measure `Is occupied` and `Subdivision occupancy` in the same pass (same base group — the issue's point 3).
   - **Target the part where `building_data` actually lives** for each code (the #68 caveat in 1.2) —
     a 204 on the "expected" part is a wrong-partition read, not a data gap.
   - Record the new per-code fill table against the 2026-09-06 table in the issue body.

### Phase 3 — Decide: resolved vs. data-coverage follow-up

- **Values land populated (non-zero, non-placeholder):** the pairing defect is resolved end-to-end →
  Phase 4. Confirm the fills are real values, not placeholder zeros — a fill count alone can pass on
  zeros (the reason for the `-Distribution` flag, per [DiGi.GIS.WebAPI#24](https://github.com/ZiolkowskiJakub/DiGi.GIS.WebAPI/issues/24)).
- **Values land as zeros / still sparse:** the source BDOO `ot:liczbaMieszkancow` is missing for those
  counties. That residue is **not** this defect — open a **new** data-coverage issue
  (`GitHub - Issues.md` §1: `type: bug`/`data` + a `priority` + an `ai:` tier per `GitHub - AI Issue
  Classification.md`, assigned to `ZiolkowskiJakub`, body via `--body-file`), linking #67 and quoting
  the zero-fill table as evidence. #67 itself then closes as "code fix landed; residual is
  data-availability, tracked in <new issue>."

### Phase 4 — Close the issue record (`GitHub - Issues.md` §3)

Structured resolution comment, then `gh issue close 67`:

1. **Resolution & commits:** the pairing fix `7f94cb8` (DiGi.GIS.PostgreSQL, `0.8.9`), reusing the
   permanent helpers `1abf92b` (#53) / `d46deec` (#58). No temporary code introduced or left.
2. **Summary of changes:** the building phase now pairs buildings with subdivisions through the sibling
   county group, so a building whose subdivision sits under a sibling part gets its stored occupancy
   record and `calculated_occupancy` is written (and the column registered for 76453).
3. **Automated & integration tests:** `PostgreSQLUpdateOccupancyTask_Building2D_SiblingCountyPart_Pairing_Integration`
   (skipped by default; exercised by the Phase 2 production re-run).
4. **Live verification:** the Phase 2 before/after per-code fill tables for `Calculated occupancy`,
   `Is occupied`, `Subdivision occupancy` — the closing entry.
5. **Mechanics (mandatory, `GitHub - Issues.md` §1):** the comment body via `--body-file` from a
   UTF-8 (no BOM) file with LF newlines; never inline markdown in `gh` arguments; labels untouched
   while closing.

### Phase 5 — Update the record (guidelines)

1. **`Coding - GIS Administrative Data.md`:** if the guideline carries an occupancy-gap statement
   (it does not today — §3 records the county-part repair, §4 the `fallbackByReference` rule), add a
   concise entry in the §3 repair-record style: the pairing cause, the `7f94cb8` fix, and the
   Phase 2 before/after result. Otherwise no guideline edit is owed — do not force one.
2. **Sync the `.agents/skills` copies** in the affected repos — the "Sync rules and skills (.agents)
   with latest AI Guidelines" commit pattern — only if step 1 actually changed a guideline.

### Phase 6 — Release (only on request)

Per `GitHub - Branch Synchronization.md`: merge `0.8.9` into `main`, bump the patch, create the new
branch, update `Directory.Build.props`, push both. A release-cadence decision, not part of the fix.

---

## 3. Definition of done

- Phase 1: local build + `DiGi.GIS.PostgreSQL.xUnit` suite green, zero warnings; downstream-consumer
  grep recorded; no `TODO [Marker]` tied to #67 (confirmed); API docs already current (confirmed).
- Phase 2: both tasks re-run in production; `CheckBuildingDataColumns.ps1` re-measured per affected
  code (correct part), `Calculated occupancy` + `Is occupied` + `Subdivision occupancy`, before/after
  table captured from the live estate.
- Phase 3: an explicit decision — resolved (values populated) **or** a new data-coverage issue opened
  with the mandatory labels + assignee and the zero-fill evidence.
- Phase 4: #67 closed with the structured comment; labels left on the taxonomy.
- Phase 5: guideline entry added **only if** warranted, and the `.agents/skills` copies synced.

## 4. Risks & guardrails

- **The only production write is Phase 2**, and only after its read-only checks and explicit approval
  (`Coding - Deployed WebAPI.md` §3). Everything else is local build or read-only GET.
- **Measuring production through a `.conf` is prohibited** (`Coding - PostgreSQL.md` §6) — every figure
  in the closure comment comes from `api.digiproject.uk`.
- **Wrong-partition false gap:** after #68's re-filing, a 204 on the "expected" part means the read hit
  the old part, not a data gap. Target the part where `building_data` lives (the 1.2 caveat) before
  concluding anything.
- **Fill count ≠ populated:** a plain fill count can pass on placeholder zeros — always use
  `-Distribution` (the DiGi.GIS.WebAPI#24 reason this issue calls out).
- **The skipped integration Fact is not verification** (`Coding - Automatic Tests.md`) — a green local
  suite with the pairing Fact `[Skip]`-ed proves nothing about the pairing; the production re-run is
  the check.
- **gh body mechanics:** inline markdown mangles in PowerShell (`GitHub - Issues.md` §1) — always
  `--body-file`, UTF-8 no BOM, LF.
- **Do not conflate with #64/#68:** the radial-ratio cause is fixed and closed (#64); the county-part
  re-filing is #68. #67 is the occupancy column failing on its own terms.

## 5. Decision points (need user sign-off)

1. **Approve the Phase 2 production re-run** — `PostgreSQLUpdateOccupancyTask` (both phases) then
   `PostgreSQLBuildingDataUpdateTask` (Occupancy type) for the affected parts. This is the only write
   in the plan.
2. **Approve the Phase 3 outcome path:** if the re-measure lands zeros, approve opening the **new**
   BDOO data-coverage issue (and closing #67 as "code fixed; residual tracked in <new>") rather than
   keeping #67 open as the data tracker.
3. **Approve closing #67** after its Phase 4 structured comment.
4. **Phase 6 release** — when, per release cadence (`GitHub - Branch Synchronization.md`).
