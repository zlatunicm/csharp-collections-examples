using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Collections.Examples;

/// <summary>
/// IMMUTABLE (read-only) collections — collections that can't (easily) be changed.
///
/// There are two different ideas here:
///
/// 1) ReadOnly "wrappers" (System.Collections.ObjectModel):
///    - ReadOnlyCollection&lt;T&gt; / IReadOnlyList&lt;T&gt; are just a VIEW onto an existing
///      collection. You can't change it through the view, but if the underlying
///      collection changes, the change shows through the view too. (C# 2 / .NET 2.0)
///
/// 2) Immutable collections (System.Collections.Immutable, a .NET package, ~2013):
///    - ImmutableList/ImmutableArray/ImmutableDictionary/ImmutableHashSet...
///    - TRULY immutable: every "change" returns a NEW collection, while the old one
///      stays untouched. Ideal for safety in multi-threaded code.
///
/// When to use: when you want to guarantee that nobody can accidentally change
/// your data (e.g. configuration, sharing between threads).
///
/// Good to know: a "changed" immutable collection shares most of its memory with
/// the previous version (structural sharing), so it's cheaper than full copying.
/// Real-world uses: configuration snapshots, Redux-style app state, public APIs
/// that must not let callers mutate internals — the C# compiler itself (Roslyn)
/// is built on immutable collections.
/// </summary>
public static class ImmutableDemo
{
    public static void Run()
    {
        // ── 1) ReadOnly view ─────────────────────────────────────────────────
        Helper.Heading("IReadOnlyList — a view that can't be changed");
        var source = new List<int> { 1, 2, 3 };
        IReadOnlyList<int> view = source.AsReadOnly();
        Helper.Info($"View: {Helper.AsText(view)}");
        // view.Add(4);  // <-- DOESN'T EXIST, won't compile (there is no Add method)

        source.Add(4);     // BUT we can still change the source...
        Helper.Info($"After changing the SOURCE, the view sees: {Helper.AsText(view)}  (the view is NOT a copy!)");

        // ── 2) ImmutableList — truly immutable ───────────────────────────────
        Helper.Heading("ImmutableList — every change returns a NEW collection");
        ImmutableList<string> original = ImmutableList.Create("a", "b", "c");
        ImmutableList<string> extended = original.Add("d");   // returns a NEW list

        Helper.Info($"original (unchanged):     {Helper.AsText(original)}");
        Helper.Info($"extended (new collection): {Helper.AsText(extended)}");

        // Chained (fluent) changes — each call produces a new version:
        var result = original.Add("x").Remove("a").Insert(0, "POCETAK");
        Helper.Info($"Chained changes: {Helper.AsText(result)}");
        Helper.Info($"original is STILL: {Helper.AsText(original)}");

        // ── Builder — many changes efficiently ───────────────────────────────
        Helper.Heading("ImmutableList.Builder — when you make many changes");
        var builder = ImmutableList.CreateBuilder<int>();
        for (int i = 1; i <= 5; i++)
            builder.Add(i * i);
        ImmutableList<int> squares = builder.ToImmutable();   // "freeze" at the end
        Helper.Info($"Squares: {Helper.AsText(squares)}  (the builder avoids creating 5 intermediate copies)");

        // ── ImmutableArray and ImmutableDictionary ───────────────────────────
        Helper.Heading("Other immutable variants");
        ImmutableArray<int> array = [10, 20, 30];   // the C# 12 collection expression works here too
        Helper.Info($"ImmutableArray: {Helper.AsText(array)}");

        ImmutableDictionary<string, int> map = ImmutableDictionary<string, int>.Empty
            .Add("jedan", 1)
            .Add("dva", 2);
        Helper.Info($"ImmutableDictionary key count: {map.Count}");
    }
}
