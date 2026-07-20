namespace Collections.Examples;

/// <summary>
/// IEnumerable&lt;T&gt;, 'yield' and LINQ — the foundation of working with ALL collections.
///
/// IEnumerable&lt;T&gt; (C# 2, 2005) is the most general collection interface: "something
/// you can walk through with foreach". All the collections above implement it.
///
/// LINQ (Language Integrated Query, C# 3, 2007) is a set of methods (Where, Select,
/// OrderBy, GroupBy, Sum...) that work over ANY IEnumerable. It behaves the same for
/// a List, an array, a Dictionary, a HashSet...
///
/// 'yield return' (C# 2) lets us build a collection ourselves that is generated
/// "lazily" — element by element, only when it's actually needed.
/// </summary>
public static class LinqDemo
{
    // A record (C# 9, 2020) — a short way to declare an immutable data type.
    private record Person(string Name, int Age, string City);

    public static void Run()
    {
        var people = new List<Person>
        {
            new("Ana",   28, "Zagreb"),
            new("Marko", 34, "Split"),
            new("Iva",   22, "Zagreb"),
            new("Petar", 41, "Split"),
            new("Luka",  30, "Rijeka"),
        };

        // ── Filtering and projection ─────────────────────────────────────────
        Helper.Heading("Where + Select (filter then reshape)");
        var adultNames = people
            .Where(p => p.Age >= 30)             // filter
            .Select(p => p.Name)                 // take just the name
            .ToList();
        Helper.Info($"Names (30+): {Helper.AsText(adultNames)}");

        // ── Sorting ──────────────────────────────────────────────────────────
        Helper.Heading("OrderBy / ThenBy");
        var ordered = people
            .OrderBy(p => p.City)
            .ThenByDescending(p => p.Age);
        foreach (var p in ordered)
            Console.WriteLine($"   {p.City,-8} {p.Name,-6} ({p.Age})");

        // ── Grouping ─────────────────────────────────────────────────────────
        Helper.Heading("GroupBy (group by city)");
        var byCity = people.GroupBy(p => p.City);
        foreach (var group in byCity)
            Console.WriteLine($"   {group.Key}: {group.Count()} person(s), average {group.Average(p => p.Age):F0} yrs");

        // ── Aggregations ─────────────────────────────────────────────────────
        Helper.Heading("Aggregate functions");
        Helper.Info($"Total people (Count):    {people.Count}");
        Helper.Info($"Oldest (MaxBy):          {people.MaxBy(p => p.Age)!.Name}");   // MaxBy: .NET 6
        Helper.Info($"Average age (Average):   {people.Average(p => p.Age):F1}");
        Helper.Info($"Anyone 40+? (Any):       {people.Any(p => p.Age >= 40)}");
        Helper.Info($"Everyone 18+? (All):     {people.All(p => p.Age >= 18)}");

        // ── ToDictionary — from a list to a dictionary ───────────────────────
        Helper.Heading("ToDictionary (list -> dictionary)");
        var byName = people.ToDictionary(p => p.Name, p => p.Age);
        Helper.Info($"byName[\"Ana\"] = {byName["Ana"]}");

        // ── Your own generator via 'yield' ───────────────────────────────────
        Helper.Heading("yield return — your own lazy collection");
        Helper.Info($"First 8 Fibonacci numbers: {Helper.AsText(Fibonacci().Take(8))}");
    }

    // An infinite sequence generated lazily — Take(8) takes only the first 8,
    // the rest is never computed. That's what 'yield return' enables.
    private static IEnumerable<int> Fibonacci()
    {
        int a = 0, b = 1;
        while (true)
        {
            yield return a;
            (a, b) = (b, a + b);   // tuple swap (C# 7) — swap without a temporary variable
        }
    }
}
