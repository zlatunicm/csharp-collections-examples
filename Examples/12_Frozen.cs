using System.Collections.Frozen;

namespace Collections.Examples;

/// <summary>
/// FROZEN collections — FrozenDictionary&lt;K,V&gt; and FrozenSet&lt;T&gt; (.NET 8, 2023).
///
/// The idea: a collection that is built ONCE and after that NEVER CHANGES, but is
/// READ a great many times. When "freezing", .NET optimizes the internal structure
/// so that later reads (Contains / TryGetValue) are FAST — often faster than a
/// regular Dictionary/HashSet.
///
/// Difference from Immutable collections:
///   - Immutable: optimized for FREQUENT changes that return new versions.
///   - Frozen:    optimized for "build once, read a million times". The build
///                itself is SLOWER (it analyzes the data), but reading is faster.
///
/// When to use: constant "lookup" tables loaded at application startup
/// (e.g. mappings, permissions, configuration) that get checked often.
/// </summary>
public static class FrozenDemo
{
    public static void Run()
    {
        // ── FrozenDictionary ─────────────────────────────────────────────────
        Helper.Heading("FrozenDictionary — a frozen lookup table");

        // First we make a plain dictionary, then we "freeze" it:
        var source = new Dictionary<string, int>
        {
            ["ponedjeljak"] = 1,
            ["utorak"]      = 2,
            ["srijeda"]     = 3,
            ["cetvrtak"]    = 4,
            ["petak"]       = 5,
        };
        FrozenDictionary<string, int> frozen = source.ToFrozenDictionary();

        // Reading is the same as with a Dictionary — but internally optimized:
        Helper.Info($"TryGetValue('srijeda'): {(frozen.TryGetValue("srijeda", out var d) ? d : -1)}");
        Helper.Info($"ContainsKey('petak'):   {frozen.ContainsKey("petak")}");
        Helper.Info($"Key count:              {frozen.Count}");
        // frozen["subota"] = 6;  // <-- DOESN'T EXIST: a frozen collection can't be changed

        // ── FrozenSet ────────────────────────────────────────────────────────
        Helper.Heading("FrozenSet — a frozen set for fast checks");
        FrozenSet<string> allowed = new[] { "GET", "POST", "PUT", "DELETE" }.ToFrozenSet();

        foreach (var method in new[] { "GET", "PATCH", "POST" })
            Console.WriteLine($"   Is '{method}' allowed? {allowed.Contains(method)}");

        // ── Summary ──────────────────────────────────────────────────────────
        Helper.Heading("Rule of thumb");
        Helper.Info("Lots of READS + NO changes  -> Frozen (fastest reading)");
        Helper.Info("Lots of CHANGES (always new) -> Immutable");
        Helper.Info("Ordinary mutable usage       -> Dictionary / HashSet");
    }
}
