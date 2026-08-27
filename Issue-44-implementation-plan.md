# Implementation Plan — Issue #44

**Issue:** `Estimated count sums silently discard unanalysed partitions, making coverage factors a lower bound` — `ZiolkowskiJakub/DiGi.GIS.PostgreSQL#44`
**Labels:** `type: bug`, `priority: medium`, `ai: standard`
**Date:** 2026-08-27 · **Re-verified:** 2026-08-27 (issue still open, no fix in any repo; every claim below re-checked against the `0.8.8` source and the live host today)

## Execution status (2026-08-27)

| Step | Status |
|---|---|
| 0 — Branch sync | ✅ 0.8.8, clean, in sync (see note) |
| 1 — Five converter overloads | ⬜ |
| 2 — Controller | ⬜ |
| 3 — Test | ⬜ |
| 4 — Build + API doc | ⬜ |
| 5 — Test run | ⬜ |
| 6 — Commit | ⬜ |
| 7 — Deploy + live verification | ⬜ pending explicit go-ahead |
| 8 — Close issue | ⬜ pending user approval |
| 9 — File the code-collision issue (§1.1) | ⬜ owner decision, explicit go-ahead |

*Step 0 note:* `DiGi.GIS.PostgreSQL`, `DiGi.GIS.WebAPI` on `0.8.8`, clean, in sync with origin (`## 0.8.8...origin/0.8.8`). `DiGi.Test` is on `0.8.8`, clean, **one local commit ahead, unpushed** (`ed45658 DiGi.Serilog.xUnit added`) — pre-existing, left untouched; the new fact lands on top of it.

---

## 1. Verdict: the issue is still valid — verified against the source and the live host

| Claim in the issue | Evidence (re-verified 2026-08-27) |
|---|---|
| The plural summing overloads keep only `count > 0` | Grep-verified today, the `if (count > 0)` loop **plus the "being decided in #44" comment** present in all five converters: `DiGi.GIS.PostgreSQL/Classes/Converter/OrtoDatasPostgreSQLConverter.cs:150-161`, `Building2DPostgreSQLConverter.cs:277-291`, `BuildingPostgreSQLConverter.cs:280-291`, `Building2DReferencedObjectPostgreSQLConverter.cs:167-181`, `TerrainPointPostgreSQLConverter.cs:171-184`. |
| `null` = no partition, `-1` = unanalysed | `DiGi.PostgreSQL/Query/EstimatedCountAsync.cs:17-28` (existence check, then `pg_class.reltuples`, `-1` fallback) and `EstimatedCountsAsync.cs:14-15, 41-48` (dictionary keyed by table name; absent = no row; `-1` = never analysed). |
| The sums feed `estimatedcoveragefactor(s)` | Whole-workspace sweep of `GetEstimatedCountAsync(` call sites (today): the **only production call of the `IEnumerable<int>` overload** is `DiGi.GIS.WebAPI/DiGi.GIS.WebAPI/Classes/Controller/OrtoDatasController.cs:214-215` (singular endpoint, voivodeship/country path). Every other call site is the singular `int?` overload — `BuildingController.cs:334`, `BuildingDataController.cs:393`, `OrtoDatasController.cs:190-191, 633`, `TerrainController.cs:233` — unaffected by this plan. |
| A county with analysed `building_2d_X` but unanalysed `orto_datas_X` pushes the factor down with no warning | `OrtoDatasController.cs:396-424`: the plural endpoint's `hasCount_Building2D` / `hasCount_OrtoDatas` flags flip on **any** `count >= 0` and the comment says it: *"Where some counties did answer, the sum stands as the lower bound it has always been, tracked by ZiolkowskiJakub/DiGi.GIS.PostgreSQL#44."* **The mixed case still returns a lower-bound number.** |
| The not-measured vocabulary already exists | Singular endpoint, `OrtoDatasController.cs:219-232`: `count < 0` (absent or unanalysed) or `count == 0` (no buildings) → **204 NoContent**, with the #8 rationale in the comment ("a caller handed 0.0 for it cannot tell the two apart"). Plural endpoint: identifier with **no** counted county gets no dictionary entry → `null` in the result list. |
| The `0.4986` country figure (2026-08-26) is a lower bound | Re-measured today, below: both endpoints still answer confident numbers for the country. |
| Prerequisite `ZiolkowskiJakub/DiGi.GIS.WebAPI#9` has landed | Closed 2026-08-26, fixed and verified in production; the batched `GetEstimatedCountsAsync` this plan builds on is present in all five converters and deployed (0.8.8). |

**Live re-verification, 2026-08-27 (read-only, per `Coding - Deployed WebAPI.md`):**

| request | result |
|---|---|
| `GET gis/ortodatas/estimatedcoveragefactor?administrativeareal2did=7` (country) | `0.5097741245693407` — HTTP 200 |
| `POST gis/ortodatas/estimatedcoveragefactors` body `[7]` | `[0.49994097678398625]` — HTTP 200 |

**Gap:** the five overloads' partial-sum semantics plus the plural endpoint's any-county-answers case. One loop in each converter, one block in one controller, one fact.

**Branch state (matters for where to implement):**

| Repo | Local checkout (2026-08-27) | Highest SemVer branch |
|---|---|---|
| `DiGi.GIS.PostgreSQL` | **0.8.8**, clean, in sync | **0.8.8** |
| `DiGi.GIS.WebAPI` | 0.8.8, clean, in sync | 0.8.8 |
| `DiGi.Test` | 0.8.8, clean, 1 local commit ahead (unpushed) | 0.8.8 |

**All work happens on `0.8.8`.**

### 1.1 A second defect was confirmed during re-verification (out of scope for #44)

The singular and plural endpoints **disagree for the same identifier** (country: `0.5098` vs `0.4999`). This is not a deployed-build-lag artifact — the root cause was found and reproduced:

**Code `10` is both the country (Polska) and a voivodeship (łódzkie), and the parent-code resolution does not filter by type.**

Evidence chain:

1. Data model (`Coding - GIS Administrative Data.md` §1): `administrative_areal_2d` stores 406 parallel part-chains; the country has exactly **one** code, `10`; the 16 voivodeship codes include `10`; county codes are 4-digit. So `10` collides across levels, and only `10` does today.
2. Live (today): `GET gis/administrativeareal2d/idbycode?code=10&administrativearealtype=0` → `7` (country); `…&administrativearealtype=1` → `19719` (voivodeship łódzkie).
3. Code: `AdministrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByParentCodeAsync` (`DiGi.GIS.PostgreSQL/Classes/Converter/AdministrativeAreal2DPostgreSQLConverter.cs:491`) resolves the parent with `GetIdsByCodeAsync(npgsqlConnection, parentCode, null, null, ...)` — **no type filter** (`:498`). `GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync` (`:255`) then expands **every** parent group below the target type — Country rows through `country_id` *and* Voivodeship rows through `voivodeship_id` — and `AddRange`s both results **without de-duplication**.
4. Consequence for `10` → County: all 406 county rows **plus** łódzkie's county rows a second time. The singular endpoint's summing overload collapses the duplicates (its dictionary is keyed by partition name); the plural endpoint's per-identifier list keeps them, so łódzkie's counties are **double-counted** in the plural sums.

**Live proof (today, four requests):**

| request | result |
|---|---|
| `GET estimatedcoveragefactor?administrativeareal2did=7` (country) | `0.5097741245693407` |
| `GET estimatedcoveragefactor?administrativeareal2did=19719` (voivodeship łódzkie) | `0.5097741245693407` — **a voivodeship request is answered with the whole-country factor** |
| `POST estimatedcoveragefactors` `[7]` | `[0.49994097678398625]` |
| `POST estimatedcoveragefactors` `[19719]` | `[0.49994097678398625]` — **identical to the country; łódzkie's counties double-counted** |

**Impact:**

- Voivodeship łódzkie (all its part-chain ids): factor wrong on **both** endpoints — the resolved county set is the whole country, not łódzkie.
- Country: factor wrong on the plural endpoint (double-count); the singular is set-correct (still a lower bound per #44 itself).
- Latent for any identifier whose code also exists at a different level above the target type; other call sites of the same method — `DiGi.GIS.UI/DiGi.GIS.UI.Application/Windows/MainWindow.xaml.cs:1245` (code `02` → Municipality; `02` is a voivodeship code and resolves correctly today) and the skipped integration fact in `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/GetAdministrativeAreal2DReferencesByCode.cs` (code `02`).
- Orthogonal to #44: a type-correct parent set and all-or-nothing measured state compose cleanly. Related but separate: `ZiolkowskiJakub/DiGi.GIS.PostgreSQL#45` (N+1 in the same resolution chain).

**Recommendation:** file a new issue (`type: bug`, `priority: high`, `ai: standard` — mandatory labels, `GitHub - Issues.md` §1) with the evidence above and a proposed fix direction: type-aware parent resolution (an additive nullable parent-type parameter on `GetAdministrativeAreal2DReferencesByParentCodeAsync`; the controller already knows the type of the reference it is expanding) plus de-duplication of the expansion result. **Out of scope for this plan** — Step 9, needs explicit go-ahead.

---

## 2. Scope

**In scope (the issue):**

- `DiGi.GIS.PostgreSQL` — the five plural summing overloads: all-or-`-1` semantics, decision-record comment, `<returns>` update.
- `DiGi.GIS.WebAPI` — `DiGi.GIS.WebAPI/Classes/Controller/OrtoDatasController.cs`: both endpoints decide not-measured from the batched dictionary, the Serilog warning names the specific unmeasured county ids, XML `<summary>` updates.
- `DiGi.Test` — one reproduce-before-fix `[Fact]` (mixed analysed state, integration, skipped by default).
- Regenerate `documentation/API/` (compile-time), build, commit, deploy, live verification, issue closure.

**Out of scope (flagged, owner decision):**

- The code-collision defect of §1.1 → new issue (Step 9).
- Option 1 of the issue (report the unanalysed count alongside the sum) — rejected, D1.
- Deleting the five summing overloads — after Step 2 no production caller remains (the sweep above); public-API removal is a breaking change and its own decision, not a drive-by fix.

---

## 3. Decisions (all recommended, pending owner approval)

### D1 — Identifier level: any unmeasured county → the identifier is answered not-measured

The issue asks for a decision between three options. Recommended: **Option 2, in its strongest form** — signal without changing the wire shape, applied at identifier level (generalising the #8 all-unknown → not-measured rule to any-unknown → not-measured):

| | Option 1 — report unanalysed count alongside | **Option 2 — signal, no shape change (recommended)** | Option 3 — log + document |
|---|---|---|---|
| Wire change | breaking: new response shape on two deployed endpoints; no client in the workspace can handle it today | none — reuses the vocabulary both endpoints already document and already emit (204 / `null`) | none |
| Caller knowledge | full | "could not be measured" + *which* counties (server log names them; per-county state via `countbycountyid?estimated=true`: 200 analysed / 204 unanalysed / 404 absent) | a number that still reads as a measurement |
| Version bump / client work | yes | no | no |
| The hole | closed | closed | **open** |

Rationale:

- The contract already promises "null where the coverage could not be measured" (plural) and "204 NoContent when it could not be measured" (singular). A partition that has never been analysed **is** "could not be measured" — D1 fulfils the documented contract instead of stretching it.
- Option 1's transparency is already obtainable per county today (`countbycountyid?estimated=true` distinguishes 204 from 404), and `analyze=true` on the plural endpoint (or `countbycountyid…&analyze=true` per county) is the documented lever to re-measure after a bulk import.
- The singular endpoint's county path already answers 204 for an unmeasured county (`count < 0`); D1 makes the voivodeship/country path and the plural endpoint consistent with it.

### D2 — The five overloads keep their signature, change their answer

Keep all five public overloads; change the semantics to all-or-`-1`: **any** named county absent from the dictionary or carrying `-1` → return `-1`; otherwise the sum.

- No public-API removal (`Coding - WebAPI Contracts.md` — a breaking change needs its own decision).
- `-1` is already these overloads' documented "not available" sentinel (same as the singular sibling's), so the answer space is unchanged; only the *partial sum* answer disappears.
- Deletion of the overloads (no production caller remains after D3) is flagged as a separate owner decision.

### D3 — The controllers read the dictionary, not the sum

Both endpoints decide from the `GetEstimatedCountsAsync` dictionary instead of the summing overloads, so the Serilog warning can **name the specific unmeasured county ids** — actionable for an operator who then runs the `analyze` lever — instead of one anonymous `-1`.

- Singular: the voivodeship/country path switches from the summing call to the batched dictionary read; any unmeasured county → 204 (the county path and its `count < 0` / `count == 0` guards are untouched).
- Plural: the `hasCount_` flags become all-or-nothing; an unmeasured identifier gets no dictionary entry → `null` (the existing vocabulary); the warning names the counties.
- **Half-deploy safety in both directions:** old controller + new library — the existing `count < 0 → NoContent` guard already treats the new `-1` as not-measured; new controller + old library — the dictionary's absent/`-1` entries already mean unmeasured. Deploy order does not matter and neither half-state crashes.

**Rejected:**

- **Option 1** — a breaking wire change with no client that can consume the new shape today; the information it would add is already reachable per county without a version bump.
- **Option 3 alone** — keeps the hole: the API still returns a number that reads as a measurement. Its logging half is absorbed by D1 + D3, which log *and* answer not-measured.

---

## 4. Implementation steps

### Step 0 — Branch sync (`GitHub - Branch Pull.md`) — **done, verified 2026-08-27**

All three repos on `0.8.8` with clean worktrees and in sync with their remotes (see the Step 0 note in the execution table). If the plan is picked up after other work, re-run the sync pipeline in all three repos first:

```bash
git fetch --all --prune
git checkout 0.8.8
git pull origin 0.8.8
```

### Step 1 — The five summing overloads (`DiGi.GIS.PostgreSQL/Classes/Converter/`)

One mechanical shape, five files (loops at `OrtoDatasPostgreSQLConverter.cs:150-161`, `Building2DPostgreSQLConverter.cs:277-291`, `BuildingPostgreSQLConverter.cs:280-291`, `Building2DReferencedObjectPostgreSQLConverter.cs:167-181`, `TerrainPointPostgreSQLConverter.cs:171-184`; the `Building2DReferencedObject` one is an instance method, the rest static):

1. After the `counts is null` guard, materialise and iterate the **named** counties instead of `counts.Values`:

```csharp
    List<int> countyIds_Temp = [.. countyIds];

    long result = 0;
    foreach (int countyId in countyIds_Temp)
    {
        // A county that has never been imported has no partition and is absent from the dictionary.
        // An unanalysed partition answers -1. In either case the sum is a lower bound, not a
        // measurement of the counties named, so the overload answers -1 instead (decided in
        // ZiolkowskiJakub/DiGi.GIS.PostgreSQL#44).
        if (!counts.TryGetValue(countyId, out long count) || count < 0)
        {
            return -1;
        }

        result += count;
    }

    return result;
```

2. The materialisation stays **after** the `counts is null` check: in the instance overload a null `countyIds` makes `counts` null, and the existing fact `GetEstimatedCountAsync_NullCountyIds_ReturnsMinusOne` pins that path to `-1` without a database.
3. Update each `<returns>` XML: "…or -1 when any named county has no partition or has never been analysed."
4. Replace the "being decided in #44" comment with the decision record above — the code is final, so it carries **no** `TODO` marker (`Coding - General.md` §1.12: mark only what is actually temporary).
5. No signature change → no client breakage; the existing guard facts (`GetEstimatedCountsAsync_NoConnection_ReturnDefaults`, `GetEstimatedCountAsync_NullCountyIds_ReturnsMinusOne`) keep compiling and passing unchanged.

### Step 2 — Controller (`DiGi.GIS.WebAPI/DiGi.GIS.WebAPI/Classes/Controller/OrtoDatasController.cs`)

**2a. Singular `GetEstimatedCoverageFactorAsync`** — the voivodeship/country case (lines ~200-216) switches from the summing overloads to the batched reads:

```csharp
                    List<int>? countyIds = (await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByParentCodeAsync(administrativeAreal2DReference.Code, AdministrativeArealType.County, cancellationToken: cancellationToken))?.ConvertAll(x => x.Id);
                    if (countyIds is null || countyIds.Count == 0)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "Could not find given County AdministrativeAreal2Ds for given Id");
                        return BadRequest();
                    }

                    Serilog.Modify.Log("Calculating estimated count for {Ids}", string.Join(",", countyIds));

                    Dictionary<int, long>? counts_Building2D = await building2DPostgreSQLConverter.GetEstimatedCountsAsync(countyIds, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    Dictionary<int, long>? counts_OrtoDatas = await ortoDatasPostgreSQLConverter.GetEstimatedCountsAsync(countyIds, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
                    if (counts_Building2D is null || counts_OrtoDatas is null)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Coverage could not be measured. Id: {Id}", administrativeAreal2DId);
                        return NoContent();
                    }

                    // A county absent from a dictionary has no partition and one carrying -1 has a partition
                    // that has never been analysed; in either case the sum would be a lower bound, so the
                    // identifier is answered as not measured (Issue #44 decision: any unmeasured county makes
                    // the whole identifier unmeasured).
                    long sum_Building2D = 0;
                    long sum_OrtoDatas = 0;
                    List<int> countyIds_NotMeasured = [];
                    foreach (int countyId in countyIds)
                    {
                        if (!counts_Building2D.TryGetValue(countyId, out long count_Building2D_County) || count_Building2D_County < 0 || !counts_OrtoDatas.TryGetValue(countyId, out long count_OrtoDatas_County) || count_OrtoDatas_County < 0)
                        {
                            countyIds_NotMeasured.Add(countyId);
                            continue;
                        }

                        sum_Building2D += count_Building2D_County;
                        sum_OrtoDatas += count_OrtoDatas_County;
                    }

                    if (countyIds_NotMeasured.Count > 0)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Coverage could not be measured. Unmeasured counties: {CountyIds}", string.Join(",", countyIds_NotMeasured));
                        return NoContent();
                    }

                    count_Building2D = sum_Building2D;
                    count_OrtoDatas = sum_OrtoDatas;
                    break;
```

The existing `count < 0` / `count == 0` guards below the switch stay — they still serve the county path and act as a backstop.

**2b. Plural `GetEstimatedCoverageFactorsAsync`** — the `hasCount_` block (lines ~396-424) becomes all-or-nothing: an identifier is stored in the `dictionary` **only when every county behind it has a figure in both tables**; otherwise it gets no entry (→ `null` in the result, the existing vocabulary) and the warning names the counties:

```csharp
                    // An identifier is answered as a measurement only when every county behind it has a
                    // figure in both tables: a county absent from a dictionary has no partition and one
                    // carrying -1 has a partition that has never been analysed, and in either case the sum
                    // would be a lower bound, not a measurement (Issue #44 decision, generalising the #8
                    // all-unknown rule to any-unknown).
                    long count_Building2D = 0;
                    long count_OrtoDatas = 0;
                    List<int> countyIds_NotMeasured = [];
                    foreach (int countyId in keyValuePair.Value)
                    {
                        if (!counts_Building2D!.TryGetValue(countyId, out long count_Building2D_County) || count_Building2D_County < 0 || !counts_OrtoDatas!.TryGetValue(countyId, out long count_OrtoDatas_County) || count_OrtoDatas_County < 0)
                        {
                            countyIds_NotMeasured.Add(countyId);
                            continue;
                        }

                        count_Building2D += count_Building2D_County;
                        count_OrtoDatas += count_OrtoDatas_County;
                    }

                    if (countyIds_NotMeasured.Count > 0)
                    {
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "AdministrativeAreal2D {Id} has no measurement. Unmeasured counties: {CountyIds}", keyValuePair.Key, string.Join(",", countyIds_NotMeasured));
                        continue;
                    }

                    dictionary[keyValuePair.Key] = (count_Building2D, count_OrtoDatas);
```

The `coverageFactor` lambda (line ~560) is untouched — its `count_Building2D <= 0` guard still serves the below-county path (a municipality with no buildings).

**2c. XML summaries.** Both actions: state that "could not be measured" now includes any county behind the identifier having no partition or never having been analysed, and where to look per county (`countbycountyid?estimated=true`: 200 / 204 / 404) and the re-measure lever (`analyze=true` on the plural endpoint). Replace the "tracked by …#44" comment pointer with the decision record.

**2d. Wire-safety check (`Coding - WebAPI Contracts.md` §1, §5):** no route change, no parameter rename or removal, no new wire shape — the response vocabulary (204 / `null`) is already documented and already emitted for the all-unknown case. Consumers of both endpoints swept: `DiGi.GIS.WebAPI.UI/DiGi.GIS.WebAPI.UI/Controllers/OrtoDatasController.cs` (transparent relay; maps empty to NoContent) and `DiGi.GIS.WebAPI.UI/DiGi.GIS.WebAPI.UI/wwwroot/js/gis-common.js` (`loadAllOrtoCoverages` renders `null` as "N/A") — both already handle the not-measured answer, which is exactly what the fix now returns for mixed identifiers.

### Step 3 — Test (`DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/GetEstimatedCountsAsync.cs`)

The existing guard facts keep passing unchanged (null connection → `-1`; null `countyIds` → `-1`). Add the reproduce-before-fix fact (`Coding - Automatic Tests.md` §4) — it **fails on the unfixed code with the reported symptom** (the sum keeps the analysed county's figure):

```csharp
        /// <summary>
        /// Reproduces the Issue #44 mixed state: one named county unanalysed, the other analysed.
        /// <para>Against the unfixed overload the plural sum silently keeps the analysed county's figure - a lower bound; the fixed overload answers -1. The two scratch tables are created and dropped around the read, so the fact is self-contained.</para>
        /// <para>Skipped by default: it executes an integration query requiring <c>GIS_PostgreSQL_Main.conf</c> pointing at a database. That conf resolves to a development database, so these figures describe that database and not the deployed estate.</para>
        /// </summary>
        [Fact(Skip = "Executes an integration query. Point GIS_PostgreSQL_Main.conf at a database before running.")]
        public async Task GetEstimatedCountAsync_MixedAnalysedState_Integration()
        {
            GISPostgreSQLConverterManager? gISPostgreSQLConverterManager = Create.GISPostgreSQLConverterManager();
            Assert.NotNull(gISPostgreSQLConverterManager);

            OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<OrtoDatasPostgreSQLConverter>();
            Assert.NotNull(ortoDatasPostgreSQLConverter);

            const int countyId_Unanalysed = 99998;
            const int countyId_Analysed = 99999;

            // A raw connection built from the converter's ConnectionData (the same asset its instance methods use):
            //   CREATE TABLE "public"."orto_datas_99998" (x integer);              -- exists, never analysed -> reltuples -1
            //   CREATE TABLE "public"."orto_datas_99999" (x integer);
            //   INSERT INTO "public"."orto_datas_99999" VALUES (1);
            //   ANALYZE "public"."orto_datas_99999";                               -- reltuples 1
            try
            {
                long? count_Single = await ortoDatasPostgreSQLConverter.GetEstimatedCountAsync(countyId_Analysed);
                Assert.NotNull(count_Single);
                Assert.True(count_Single >= 1);

                // Pre-fix this assertion fails with the analysed county's count (a lower bound); the fix answers -1.
                Assert.Equal(-1, await ortoDatasPostgreSQLConverter.GetEstimatedCountAsync([countyId_Unanalysed, countyId_Analysed]));
            }
            finally
            {
                // DROP TABLE IF EXISTS "public"."orto_datas_99998";
                // DROP TABLE IF EXISTS "public"."orto_datas_99999";
            }
        }
```

The controller's mixed-state logic is not unit-testable without a database (the converter must supply the dictionary) — it is covered by this fact plus the live verification of Step 7.

### Step 4 — Build + API docs

```bash
dotnet build DiGi.GIS.PostgreSQL.slnx -c Release
dotnet build DiGi.GIS.WebAPI.slnx -c Release
```

- Zero warnings required (`Coding - General.md` §1.4).
- `documentation/API/*.md` regenerates on compile (`Coding - API Documentation.md`) and is git-tracked — commit the regenerated diff (the `<returns>` wording and the two controller summaries change). The Issue-14 run on this machine found DefaultDocumentation blocked by Smart App Control (0x800711C7) — hand-match the md to the generator output, or regenerate on an unblocked machine to confirm byte-identical.

### Step 5 — Test run

```bash
dotnet test DiGi.Test/DiGi.GIS.PostgreSQL.xUnit -c Release --filter "FullyQualifiedName~GetEstimatedCount"
dotnet test DiGi.Test/DiGi.GIS.PostgreSQL.xUnit -c Release
dotnet test DiGi.Test/DiGi.GIS.WebAPI.xUnit -c Release
```

Run the new fact isolated with `GIS_PostgreSQL_Main.conf` pointed at a development database, and **record the pre-fix failure** (plural sum = the analysed county's count, not `-1`) before applying Step 1 — the reproduce-before-fix evidence (`Coding - Automatic Tests.md` §4). Note: the Issue-14 run found Smart App Control blocks loading the unsigned test assembly on this machine (0x800711C7) — if so, run the suites on another machine / CI before shipping.

### Step 6 — Commit

One commit per repo on `0.8.8`, matching the `Fixes #N` convention (Issue-14 precedent: `Expose commandtimeout on nextbuilding2dreferences (Fixes #14)`):

- `DiGi.GIS.PostgreSQL` — e.g. `Answer unanalysed partitions as not measured in the summing overloads (Fixes #44)`
- `DiGi.GIS.WebAPI` — e.g. `Answer coverage as not measured when any county is unmeasured (Fixes #44)`
- `DiGi.Test` — the new fact.

Do **not** push unless asked; release via the standard `GitHub - Branch Synchronization.md` pipeline (merge `0.8.8` → `main`, bump to `0.8.9`, push both) when the owner is ready to ship.

### Step 7 — Deploy + live verification (`Coding - Deployed WebAPI.md`)

1. Deploy `DiGi.GIS.PostgreSQL.dll` and `DiGi.GIS.WebAPI.dll` (into `extensions\gis\`). Order-independent per D3 — both half-states are safe (see D3).
2. Confirm the build carries the fix: `curl -s -H "key: <key>" "https://api.digiproject.uk/information/controllers"` — the key lives in `user files/WebAPI_Diagnostics.conf` (git-ignored) and travels in the `key` header, never the query string.
3. Classify the current state read-only, per county: `GET gis/ortodatas/countbycountyid?countyid=<id>&estimated=true` → 200 analysed / 204 unanalysed / 404 absent, and the buildingdata sibling for `building_2d` — build the list of identifiers that must flip to not-measured.
4. Expect after deploy: identifiers containing any unmeasured county → **204** (singular) / **`null`** (plural), with the server Serilog warning naming the county ids; identifiers fully measured → **unchanged numbers** (county 5: `0.999940629916585`, county 204: `0.9684792204819671` — measured 2026-08-26/27, singular and plural agreeing). Read the server's Serilog (`<install dir>\logs\log-<date>.txt` on the server — the log lives where the request ran, not on the editing machine, `Coding - PostgreSQL.md` §6).
5. **Do not call `analyze=true` in production without explicit go-ahead** — it runs one `VACUUM ANALYZE` per existing partition (a maintenance write on live partitions; for a country that is several hundred).
6. The §1.1 code-collision defect stays live after this deploy (out of scope): the country's plural figure and łódzkie's figures keep their wrong behaviour until that separate issue is fixed.

### Step 8 — Close the issue (`GitHub - Issues.md` §1, §3)

Structured resolution comment covering: (1) resolution & commit SHAs per repo on `0.8.8`; (2) summary of changes (the five overloads, the two controller blocks, the two summaries, the regenerated API md); (3) the `DiGi.Test` fact, the commands, and the recorded pre-fix failure; (4) live verification (204/`null` for the mixed identifiers, unchanged numbers for the measured ones, the warning lines from the server log). Write the markdown to a file and use `gh issue comment 44 --body-file <md>` — **never** inline markdown (PowerShell backtick mangling) — then `gh issue close 44`.

### Step 9 — File the code-collision issue (§1.1) — **owner go-ahead required**

`gh issue create --repo ZiolkowskiJakub/DiGi.GIS.PostgreSQL --title "…" --body-file <md> --label "type: bug,priority: high,ai: standard"` with the §1.1 evidence, the four-request repro recipe, and the proposed fix direction. Link it from #44's resolution comment as a related defect found during re-verification.

---

## 5. Guideline alignment checklist

- [x] **Issue premises verified against code and the live host before planning** (`GitHub - Issues.md` §2) — all five overloads, the controller blocks, the consumer sweep, the live re-measurement (2026-08-27).
- [x] **Highest SemVer branch selected** for implementation: `0.8.8` (`GitHub - Branch Pull.md` §2).
- [x] **No wire-shape change**; the 204 / `null` vocabulary is already documented and already emitted for the all-unknown case; consumers swept (`Coding - WebAPI Contracts.md` §1, §5).
- [x] **No signature change** on the five overloads; the `-1` sentinel is already their documented answer; deletion flagged as a separate decision (`Coding - WebAPI Contracts.md`, `Coding - API Documentation.md`).
- [x] **`CancellationToken` last, passed by name; explicit typing; collection expressions** (`Coding - General.md` §1.2, §1.8).
- [x] **Zero-warnings build as the gate** (`Coding - General.md` §1.4).
- [x] **No `TODO` marker** — the decision is final and the code is not temporary; the "being decided in #44" comments are *replaced* by the decision record, not accumulated (`Coding - General.md` §1.12).
- [x] **Reproduce-before-fix `[Fact]` committed in the same change**; existing guard facts kept; the fact's summary states its figures describe the dev database (`Coding - Automatic Tests.md` §4, `Coding - PostgreSQL.md` §6).
- [x] **Production questions measured through the API, never through a `.conf`**; server logs read on the server (`Coding - PostgreSQL.md` §6).
- [x] **API markdown regenerated and committed** (`Coding - API Documentation.md`).
- [x] **County data handled per the storage rules**: keyed by id, parts never deduplicated, codes never assumed unique — and the one cross-level code collision found is flagged as its own issue rather than fixed here (`Coding - GIS Administrative Data.md`).
- [x] **Live checks are manual curl only, never in `DiGi.Test`; `analyze=true` in production only with explicit go-ahead** (`Coding - Deployed WebAPI.md`).
- [x] **Structured resolution comment via `--body-file`; mandatory Type + Priority + AI labels on the new issue** (`GitHub - Issues.md` §1, §3).
- [x] **Relative paths only in this document** (portability rule, `DigiProject/CLAUDE.md`).

---

## 6. Effort estimate

- `DiGi.GIS.PostgreSQL`: five methods, ~10 lines each (loop + comment + `<returns>` line) — one mechanical shape.
- `DiGi.GIS.WebAPI`: two blocks in one controller (~30 lines) + two XML summaries.
- `DiGi.Test`: one new fact, skipped by default, with scratch-table setup/teardown.
- Build ×2, test runs, API-doc regen (or hand-match), three commits, deploy + read-only live verification.

Consistent with the `ai: standard` label: three repos touched, no wire change, one new fact; the only non-trivial step is the live-verification county classification.
