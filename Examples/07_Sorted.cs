namespace Collections.Examples;

/// <summary>
/// SortedList&lt;TKey,TValue&gt; and SortedDictionary&lt;TKey,TValue&gt; — dictionaries that
/// keep their keys ALWAYS SORTED.
///
/// Both (generics -> C# 2 / .NET 2.0, 2005) do the same thing (a key->value map
/// with ordered keys), but they have different performance characteristics:
///
/// SortedList&lt;,&gt;   — internally 2 arrays. Less memory, fast read by index,
///                    BUT slow insert/remove in the middle (the arrays get shifted).
///                    Good when you READ a lot and rarely change anything.
///
/// SortedDictionary&lt;,&gt; — internally a (red-black) tree. Faster insert/remove
///                    (O(log n)), but uses more memory.
///                    Good when you ADD/REMOVE often.
///
/// For comparison: a plain Dictionary&lt;,&gt; has NO order, but is the fastest for lookup.
/// </summary>
public static class SortedDemo
{
    public static void Run()
    {
        // ── SortedDictionary — keys ordered automatically ────────────────────
        Helper.Heading("SortedDictionary — keys sort themselves");
        var capitals = new SortedDictionary<string, string>
        {
            ["Njemacka"] = "Berlin",
            ["Austrija"] = "Bec",
            ["Francuska"] = "Pariz",
            ["Belgija"]  = "Bruxelles",
        };
        foreach (var (country, city) in capitals)
            Console.WriteLine($"   {country,-10} -> {city}");   // ALPHABETICAL by country

        // ── SortedList — also sorted, but with access by INDEX ───────────────
        Helper.Heading("SortedList — bonus: access by index");
        var scores = new SortedList<int, string>
        {
            [30] = "Petar",
            [10] = "Ana",
            [20] = "Marko",
        };
        // The keys are ordered ascending: 10, 20, 30
        Helper.Info($"Keys (sorted): {Helper.AsText(scores.Keys)}");

        // SortedList lets you fetch by POSITION (index), which a plain Dictionary can't:
        Helper.Info($"Value at index 0 (smallest key): {scores.Values[0]}");
        Helper.Info($"Key at index 0: {scores.Keys[0]}");

        // ── Which one when: a short comparison ───────────────────────────────
        Helper.Heading("Summary of the differences");
        Helper.Info("Dictionary        -> fastest, NO ordering");
        Helper.Info("SortedList        -> sorted, less memory, slower to change");
        Helper.Info("SortedDictionary  -> sorted, faster to change, more memory");
    }
}
