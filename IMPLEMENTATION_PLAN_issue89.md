# Implementation Plan — DiGi.GIS.PostgreSQL #89

**`randombuilding2dreference`: a `countyIds`-filtering overload of `GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync`, wired into the endpoint as an optional `countyids` query parameter.**

- Issue: https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/89 (open, `type: feature`, `priority: medium`, `ai: standard`, no comments)
- Parent context: [DiGi.GIS.WebAPI#36](https://github.com/ZiolkowskiJakub/DiGi.GIS.WebAPI/issues/36) (still OPEN) shipped the baseline endpoint in `7fc9a25`; this issue owns the deferred `countyids` half
- Targets: `DiGi.GIS.PostgreSQL` branch `0.8.10` (clean tree, HEAD `8e58872` = #88) and `DiGi.GIS.WebAPI` branch `0.8.8` (clean tree, HEAD `7fc9a25` = #36); tests in `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit` and `DiGi.Test/DiGi.GIS.WebAPI.xUnit`
- Plan date: 2026-09-19

---

## 1. Validity assessment

The issue is **valid and actionable**. Every premise holds against the tree; the work is small and additive. Two details of the issue body are corrected below (a binding type and a test placement) — neither changes the scope.

### Premises verified against code

| Premise | Status | Evidence |
|---|---|---|
| Static + instance pair exists, takes only `commandTimeout` + `cancellationToken` | ✅ | `DiGi.GIS.PostgreSQL/Classes/Converter/OrtoDatasPostgreSQLConverter.cs:2059` (static, `NpgsqlConnection?` first) and `:2218` (instance) |
| Stage 1 = county-part draw; stage 2 prunes by `ANY(@countyIds)` of the drawn code's parts | ✅ | `:2081-2160`; `parts_ByCode` groups `AdministrativeAreal2DReference.Id` by `Code`, weights by `GetEstimatedCountsAsync`, stage 2 binds `countyIds_Drawn` |
| Controller calls it with only `commandTimeout` | ✅ | `DiGi.GIS.WebAPI/Classes/Controller/OrtoDatasController.cs:154`; action signature `:132` has `commandtimeout` + `CancellationToken` only |
| `updateitemsbycountyids` already binds a repeated `countyids` int list | ✅ | `Building2DController.cs:1215`, `BuildingController.cs:131`, `BuildingDataController.cs:1336` — all as `[FromQuery(Name = "countyids")] int[]? countyIds` |
| Existing tests: null-connection fact (CI) + skipped scratch-DB facts | ✅ | `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/GetRandomBuilding2DReferenceWithoutUserYearBuilt.cs:16` (static called as `(null)`, instance as `()`), `:29` (`[Fact(Skip = …)]`, seeds county 990101) |
| WebAPI test convention for this endpoint | ✅ | `DiGi.Test/DiGi.GIS.WebAPI.xUnit/Facts/OrtoDataUserToken.cs:16` — controller built with `null`-connection converters, asserts 401 anonymously; no DB facts exist in that project for this controller |
| `countyids` are `building_2d` part ids, not codes | ✅ | `Coding - GIS Administrative Data.md` Mandatory Rule + §4 |

### Corrections to the issue body (recorded here; post as an issue comment on resolution per `GitHub - Issues.md` §2)

| Issue says | Finding | Ruling |
|---|---|---|
| Bind as `List<int>? countyIds` | Every existing `countyids` binding in `DiGi.GIS.WebAPI` is `int[]? countyIds` | Use `int[]?` — the "repeated-int binding `updateitemsbycountyids` already uses" **is** `int[]?`; the converter takes `IEnumerable<int>?` so either binds |
| The DB-dependent "restricts to parts" fact goes in `DiGi.GIS.WebAPI.xUnit` | The controller action requires a valid user token (`Query.GetUserEmail`) before it touches the converter, and `DiGi.GIS.WebAPI.xUnit` has no token/DB harness for it; the sibling converter facts already have the scratch-county seeding harness | The restriction fact goes in `DiGi.GIS.PostgreSQL.xUnit` beside the existing draw facts (same `Skip` convention). `DiGi.GIS.WebAPI.xUnit` gets the contract-level facts it can run without a DB (§4.2) |

### Design decisions the issue leaves open — this plan rules on them

| Decision | Ruling | Rationale |
|---|---|---|
| One code path or two | The **existing** overloads delegate to the new ones with `countyIds: null`; the filter logic lives only in the new static overload | "Byte-identical baseline" is guaranteed structurally, not by copy; `Coding - PostgreSQL.md` §5 "delegate, not reimplement" |
| Where the filter applies | Stage 1: after `parts_ByCode` is built, drop every part id not in the requested set, then drop codes left with no parts. Stage 2 is unchanged — it already binds the (now filtered) part list of the drawn code | Confining the *parts* means a caller naming one part of a multi-part code draws from that part only; the drawn `CountyId` is therefore always in the requested set (acceptance criterion 2). Weighting stays per code over the surviving parts |
| `countyIds` default value | **No default** on the new overloads: `(IEnumerable<int>? countyIds, int commandTimeout = 30, CancellationToken cancellationToken = default)` | With a default, `Get…Async()` and the test's `Get…Async(null)` become CS0121-ambiguous between the two overloads. With `countyIds` required, arity separates them. **Verify with a probe** (`Coding - General.md` §1 rule 17): build both call forms in a scratch console app before relying on this |
| Unknown / negative / duplicate ids | No validation; a `HashSet<int>` dedupes; ids that name no covered part simply leave the pool empty → `null` → endpoint 404 | Same semantics as an exhausted draw; an unknown part is not a client error the converter can tell apart from an uncovered one |
| Controller pass-through | Pass the bound array as-is (`null` when omitted, possibly empty); log the filter when non-empty | Both `null` and empty mean "no filter" by the issue's own criterion |
| Versions | No version bump in either repo; both branches are the current SemVer release branches and the change is additive | Consistent with #88 (landed on `0.8.10` without a bump) |

---

## 2. Scope

**In scope**

- `DiGi.GIS.PostgreSQL/Classes/Converter/OrtoDatasPostgreSQLConverter.cs`: new static + instance overloads with `countyIds`; existing pair delegates.
- `DiGi.GIS.WebAPI/Classes/Controller/OrtoDatasController.cs`: `countyids` query parameter on `GetRandomBuilding2DReferenceAsync`.
- `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/GetRandomBuilding2DReferenceWithoutUserYearBuilt.cs`: null-guard fact for the new overloads + skipped scratch-DB restriction fact.
- `DiGi.Test/DiGi.GIS.WebAPI.xUnit/Facts/OrtoDataUserToken.cs`: anonymous-with-`countyids` 401 fact.
- Issue resolution comment on #89 and a pointer comment on DiGi.GIS.WebAPI#36 (its Amendment D is what this delivers).

**Out of scope**

- Any change to the drawn weighting model, stage-2 SQL, or the user-year-built write (#88).
- UI consumption of `countyids` (DiGi.GIS.WebAPI.UI — tracked there).
- Deployment; per `feedback_production_runs_separate_issue` a deploy/verify run is its own issue if wanted.

---

## 3. Implementation

### 3.1 `OrtoDatasPostgreSQLConverter` — new overloads (DiGi.GIS.PostgreSQL)

**Static (the only body):**

```csharp
public static async Task<Building2DReference?> GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(NpgsqlConnection? npgsqlConnection, IEnumerable<int>? countyIds, int commandTimeout = 30, CancellationToken cancellationToken = default)
```

Move the current body of the `(npgsqlConnection, commandTimeout, cancellationToken)` overload here verbatim, then insert one block **between building `parts_ByCode` and the `parts_ByCode.Count == 0` guard**:

```csharp
// An explicit part filter confines the draw to the named building_2d parts. null/empty is "every part", so
// the unfiltered call stays exactly the baseline. Filtering at the part level (not the code level) is what
// lets a caller draw from one part of a multi-part code.
HashSet<int>? countyIds_Filter = countyIds is null ? null : [.. countyIds];
if (countyIds_Filter is { Count: > 0 })
{
    foreach (string code in parts_ByCode.Keys.ToList())
    {
        parts_ByCode[code].RemoveAll(x => !countyIds_Filter.Contains(x));
        if (parts_ByCode[code].Count == 0)
        {
            parts_ByCode.Remove(code);
        }
    }
}
```

Everything after (`countyIds_All`, estimates, weighted loop, stage-2 SQL bound to `countyIds_Drawn`) is untouched — the filtered part list flows through naturally.

**Existing static overload** becomes a one-liner: `return await GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(npgsqlConnection, null, commandTimeout, cancellationToken);` — keep its `<summary>` (it is what the docs and existing callers read), add one `<para>` noting it draws from every part and points at the filtering overload.

**Instance overload:**

```csharp
public async Task<Building2DReference?> GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(IEnumerable<int>? countyIds, int commandTimeout = 30, CancellationToken cancellationToken = default)
```

Same shape as the existing instance method (`Create.NpgsqlConnection(ConnectionData)`, null → `null`, `OpenAsync`, delegate to the static). The existing instance overload delegates to it with `null`.

**XML docs** (`XML Documentation - Create.md`): full `<summary>` on the new static overload (carry the two-stage description over, add a `<para>` on the filter semantics: part ids not codes, `null`/empty = every part, unknown ids fall out of the pool), `<param name="countyIds">` in signature order before `commandTimeout`, `<returns>` extended with "or no requested part yields a candidate".

**Rules touched:** `CancellationToken` last, new parameter inserted before it (`Coding - General.md` §1.8); ≤ 7 parameters so single-line signature (§1.6); explicit types, collection expressions (§1.2); overloads stay in the same file (§2 File Organisation); parameterised SQL unchanged (`Coding - PostgreSQL.md` §4).

### 3.2 `OrtoDatasController.GetRandomBuilding2DReferenceAsync` (DiGi.GIS.WebAPI)

Signature (still ≤ 7 parameters → one line):

```csharp
public async Task<IActionResult> GetRandomBuilding2DReferenceAsync([FromQuery(Name = "countyids")] int[]? countyIds = null, [FromQuery(Name = "commandtimeout")] int commandTimeout = 30, CancellationToken cancellationToken = default)
```

- `countyids` goes **first** so the existing test call `GetRandomBuilding2DReferenceAsync()` and any positional-free callers keep compiling; the HTTP contract is by name, so order is irrelevant on the wire (`Coding - WebAPI Contracts.md` §1 — route and existing parameter **names** unchanged).
- Body: after the `commandTimeout` guard, `if (countyIds is { Length: > 0 }) Serilog.Modify.Log("{Type}:{Name} restricted to county parts {CountyIds}", …, string.Join(",", countyIds));` then `ortoDatasPostgreSQLConverter.GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(countyIds, commandTimeout, cancellationToken: cancellationToken)`.
- `<param name="countyIds">` doc: "Optional `building_2d` part ids (repeated `countyids=…`, one per polygon part — never a county code) that confine the draw; omitted or empty draws from every covered part." Extend `<returns>`: 404 also when none of the requested parts holds a candidate.
- No change to `[ProducesResponseType]` set, auth flow, or error mapping.

### 3.3 Overload-ambiguity probe (before 3.1 is committed)

Scratch console app in the session scratchpad referencing `bin\DiGi.GIS.PostgreSQL.dll` after the build; compile these four call forms and confirm no CS0121:

```csharp
_ = OrtoDatasPostgreSQLConverter.GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(null);
_ = OrtoDatasPostgreSQLConverter.GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(null, null);
_ = converter.GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync();
_ = converter.GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync(30, cancellationToken: default);
```

If the probe reports ambiguity, the fallback is to keep `countyIds` required (it already is) and rename nothing — the probe is there to prove it, not to pick a design.

---

## 4. Tests

### 4.1 `DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/Facts/GetRandomBuilding2DReferenceWithoutUserYearBuilt.cs`

- **`GetRandomBuilding2DReferenceWithoutUserYearBuiltAsync_CountyIds_NullConnection_ReturnsNull`** (runs in CI): static `(null, [1])` and instance on a `new(null)` converter with `[1]` → both `null`. Mirrors the existing null-guard fact.
- **`RandomBuilding2DReference_CountyIds_RestrictsToRequestedParts_DevDb`** — `[Fact(Skip = "Seeds scratch county 990101 …")]`, same harness as `RandomBuilding2DReference_WithoutUserYearBuilt_DevDb`:
  1. Seed scratch county 990101 with one eligible building, analyse `orto_datas`.
  2. Draw with `countyIds = [countyId]` **20 times**; assert every `drawn.CountyId == countyId` (the scratch part is one of hundreds of covered parts, so an unfiltered draw would land elsewhere with overwhelming probability — this is the differential that makes the fact fail if the filter is dead, `Coding - Automatic Tests.md` §4 "A Guard Must Be Shown To Fail").
  3. Draw with `countyIds = [countyId + 1_000_000]` (a part id that does not exist) → `null`.
  4. Draw with `countyIds = []` → non-null (behaves as the baseline).
  Cleanup in `finally` as the sibling fact does.

### 4.2 `DiGi.Test/DiGi.GIS.WebAPI.xUnit/Facts/OrtoDataUserToken.cs`

- **`RandomBuilding2DReference_CountyIds_Anonymous_Answers401`**: `controller.GetRandomBuilding2DReferenceAsync(countyIds: [1, 2])` → `UnauthorizedResult`. Proves the new parameter is additive and reaches the same auth gate; the existing `RandomBuilding2DReference_Anonymous_Answers401` (no-arg call) must keep compiling unchanged — that is the non-breaking check.

### 4.3 Execution discipline (`Coding - Automatic Tests.md` §4 — HintPath drops the build)

`DiGi.GIS.WebAPI` and `DiGi.GIS.WebAPI.xUnit` reach `DiGi.GIS.PostgreSQL` through `<HintPath>..\..\DiGi.GIS.PostgreSQL\bin\…dll`. Order is mandatory:

```bash
dotnet build "DiGi.GIS.PostgreSQL/DiGi.GIS.PostgreSQL/DiGi.GIS.PostgreSQL.csproj" -c Debug -m:1
```
```bash
dotnet test "DiGi.Test/DiGi.GIS.PostgreSQL.xUnit/DiGi.GIS.PostgreSQL.xUnit.csproj" -c Debug -m:1
```
```bash
dotnet build "DiGi.GIS.WebAPI/DiGi.GIS.WebAPI/DiGi.GIS.WebAPI.csproj" -c Debug -m:1 -warnaserror
```
```bash
dotnet test "DiGi.Test/DiGi.GIS.WebAPI.xUnit/DiGi.GIS.WebAPI.xUnit.csproj" -c Debug -m:1
```

The scratch-DB fact is run once by hand with `GIS_PostgreSQL_Main.conf` pointed at a scratch database (never production — `Coding - PostgreSQL.md` §6), removing the `Skip` locally and restoring it before commit; record the run's output in the resolution comment.

---

## 5. Guideline compliance map

| Guideline | How the plan satisfies it |
|---|---|
| `Coding - General.md` §1.2/§1.3 | Explicit types, `HashSet<int>? countyIds_Filter`, `[.. countyIds]` |
| §1.4 zero warnings | WebAPI built with `-warnaserror`; nullable flow handled by `is null` / `is { Count: > 0 }` |
| §1.6 ≤ 7 parameters one line | New static overload has 4, instance 3, action 3 |
| §1.8 `CancellationToken` last, new param before it, named at call sites | Yes throughout |
| §1.17 probe overload claims | §3.3 |
| §2 overloads stay together | All four overloads in `OrtoDatasPostgreSQLConverter.cs` |
| `Coding - PostgreSQL.md` §3 `commandTimeout`, §4 parameterised SQL, §5 delegate | Unchanged SQL/params; baseline delegates to the filtering overload |
| `Coding - WebAPI Contracts.md` §1/§2 | Route + existing names untouched; nullable array, `null` = no filter; repeated-int binding matches siblings |
| `Coding - GIS Administrative Data.md` Mandatory Rule | Filter is by part `id`; docs say so explicitly; no code→id resolution added |
| `Coding - Automatic Tests.md` §2/§4 | `[Fact]` + `<summary>` on every test; differential 20-draw assertion; build-before-test order; isolated scratch DB |
| `GitHub - Issues.md` §2/§3 | Premise corrections recorded (§1) and posted as a comment; standard resolution comment with SHA + branch per repo |
| `XML Documentation - Create.md` | Full docs on new members, `<param>` order mirrors signature |

---

## 6. Risks & open questions

- **Cross-repo landing order.** `DiGi.GIS.WebAPI` will not compile against a `DiGi.GIS.PostgreSQL` bin that predates 3.1. Commit and build PostgreSQL first; the WebAPI commit references the PostgreSQL SHA.
- **`int[]?` binding of an absent parameter.** MVC may bind an omitted `countyids` as `null` or as an empty array depending on version; both are handled identically, so no behaviour hinges on it — but the 401 fact and a manual `curl` without `countyids` after deploy should both be checked.
- **Weighting with a partial filter.** A code reduced to a subset of its parts is weighted by those parts' estimates only — intended, but worth one sentence in the docs so nobody "fixes" it back.
- **DiGi.GIS.WebAPI#36 is still open.** Its acceptance list includes this `countyids` item; after landing, comment there with the SHAs so #36 can close on its own terms.

---

## 7. Execution order

1. `DiGi.GIS.PostgreSQL` (`0.8.10`): §3.1 → build → §3.3 probe → §4.1 facts → `dotnet test` green → commit `feat: countyIds-filtering overload for random unverified building draw (#89)`.
2. `DiGi.GIS.WebAPI` (`0.8.8`): §3.2 → build `-warnaserror` (0 warnings) → §4.2 fact → `dotnet test` green → commit `feat: randombuilding2dreference accepts optional countyids (#89)`.
3. Hand-run the scratch-DB fact once; capture output.
4. Resolution comment on #89 (both SHAs, branches, test output, the two body corrections); pointer comment on DiGi.GIS.WebAPI#36; close #89.
