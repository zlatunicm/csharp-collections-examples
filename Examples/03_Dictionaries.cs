namespace Collections.Examples;

/// <summary>
/// Dictionary&lt;TKey, TValue&gt; — a "key -> value" map.
///
/// - Stores pairs: each KEY is unique and leads to a single VALUE.
/// - Lookup/insert/delete by key are very fast (O(1) on average), because
///   internally it uses a hash table.
/// - The ORDER of the elements is NOT guaranteed.
/// - Lives in System.Collections.Generic (generics -> C# 2, 2005).
///
/// When to use: when you look data up by some "key" (e.g. a user by ID,
/// a price by product name, a count of occurrences by word...).
/// </summary>
public static class DictionariesDemo
{
    public static void Run()
    {
        // ── Creating ─────────────────────────────────────────────────────────
        Helper.Heading("Creating");

        // Collection initializer with key/value pairs:
        var prices = new Dictionary<string, decimal>
        {
            ["kava"]  = 1.50m,     // the 'm' marks a decimal literal
            ["caj"]   = 1.20m,
            ["sok"]   = 2.00m,
        };
        Helper.Info($"The price list has {prices.Count} entries.");

        // ── Adding and updating ──────────────────────────────────────────────
        Helper.Heading("Adding / updating");
        prices["voda"] = 1.00m;              // adds if missing, otherwise updates
        prices.Add("pivo", 2.50m);           // Add THROWS if the key already exists
        Helper.Info($"Price of 'kava': {prices["kava"]}");
        prices["kava"] = 1.70m;              // updating an existing value
        Helper.Info($"New price of 'kava': {prices["kava"]}");

        // ── Safe reading ─────────────────────────────────────────────────────
        Helper.Heading("Safe reading (without crashing)");

        // prices["missing"] would THROW KeyNotFoundException! So:
        if (prices.ContainsKey("sok"))
            Helper.Info($"ContainsKey: 'sok' costs {prices["sok"]}");

        // TryGetValue is the preferred way — one lookup instead of two:
        if (prices.TryGetValue("caj", out var teaPrice))
            Helper.Info($"TryGetValue: 'caj' costs {teaPrice}");

        // GetValueOrDefault (.NET Core 2.0+) — return the default if the key is missing:
        var milkPrice = prices.GetValueOrDefault("mlijeko", 0m);
        Helper.Info($"GetValueOrDefault('mlijeko', 0): {milkPrice}");

        // ── Iteration ────────────────────────────────────────────────────────
        Helper.Heading("Iterating over the pairs");
        foreach (var pair in prices)
            Console.WriteLine($"   {pair.Key,-6} -> {pair.Value:0.00} EUR");

        // Deconstructing a KeyValuePair (C# 7, 2017):
        Helper.Heading("Iteration with deconstruction (C# 7)");
        foreach (var (name, price) in prices)
            Console.WriteLine($"   {name,-6} = {price:0.00}");

        // Keys / values separately:
        Helper.Heading("Keys and Values");
        Helper.Info($"Keys:   {Helper.AsText(prices.Keys)}");
        Helper.Info($"Values: {Helper.AsText(prices.Values)}");

        // ── Practical example: counting word occurrences ─────────────────────
        Helper.Heading("Practical example: word counter");
        string sentence = "ma ma mi ma mi mo";
        var counter = new Dictionary<string, int>();
        foreach (var word in sentence.Split(' '))
            counter[word] = counter.GetValueOrDefault(word) + 1;

        foreach (var (word, count) in counter)
            Console.WriteLine($"   '{word}' -> {count}x");
    }
}
