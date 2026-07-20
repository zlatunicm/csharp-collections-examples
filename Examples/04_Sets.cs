namespace Collections.Examples;

/// <summary>
/// HashSet&lt;T&gt; and SortedSet&lt;T&gt; — sets of UNIQUE elements.
///
/// HashSet&lt;T&gt; (C# 3 / .NET 3.5, 2007):
///   - Does not allow DUPLICATES.
///   - Checking "does this element exist" is very fast (O(1)).
///   - The order is NOT guaranteed.
///
/// SortedSet&lt;T&gt; (.NET 4.0, 2010):
///   - Also no duplicates, but the elements are always SORTED.
///   - Operations are O(log n) (internally a tree).
///
/// When to use: when all you care about is "is it in / not in" and you want to
/// avoid duplicates, or for mathematical set operations (union, intersection, difference).
///
/// Complexity: HashSet Contains/Add/Remove O(1) average; SortedSet O(log n).
/// Real-world uses: deduplication, "already visited/processed" bookkeeping, tags,
/// permission sets, fast allow/deny lists.
/// </summary>
public static class SetsDemo
{
    public static void Run()
    {
        // ── HashSet: duplicates are ignored ──────────────────────────────────
        Helper.Heading("HashSet — no duplicates");
        var tags = new HashSet<string> { "c#", "dotnet", "c#", "backend", "c#" };
        Helper.Info($"Inserted 5 times, left: {Helper.AsText(tags)}  (duplicate 'c#' dropped)");

        bool added = tags.Add("api");        // Add returns true if it was actually added
        Helper.Info($"Add('api') returned: {added}");
        added = tags.Add("c#");
        Helper.Info($"Add('c#') again:     {added}  (false because it already exists)");

        Helper.Info($"Contains('dotnet'): {tags.Contains("dotnet")}");

        // ── Set operations ───────────────────────────────────────────────────
        Helper.Heading("Mathematical set operations");
        var a = new HashSet<int> { 1, 2, 3, 4 };
        var b = new HashSet<int> { 3, 4, 5, 6 };
        Helper.Info($"A = {Helper.AsText(a)}   B = {Helper.AsText(b)}");

        var union = new HashSet<int>(a);
        union.UnionWith(b);                  // A ∪ B
        Helper.Info($"Union (UnionWith):            {Helper.AsText(union)}");

        var intersection = new HashSet<int>(a);
        intersection.IntersectWith(b);       // A ∩ B
        Helper.Info($"Intersection (IntersectWith): {Helper.AsText(intersection)}");

        var difference = new HashSet<int>(a);
        difference.ExceptWith(b);            // A \ B
        Helper.Info($"Difference (ExceptWith):      {Helper.AsText(difference)}");

        Helper.Info($"Is {{1,2}} a subset of A? {new HashSet<int> { 1, 2 }.IsSubsetOf(a)}");

        // ── SortedSet: always sorted ─────────────────────────────────────────
        Helper.Heading("SortedSet — sorted automatically");
        var sorted = new SortedSet<int> { 5, 1, 4, 1, 3, 2, 5 };
        Helper.Info($"Inserted shuffled with duplicates -> {Helper.AsText(sorted)}");
        Helper.Info($"Smallest (Min): {sorted.Min}, Largest (Max): {sorted.Max}");

        // Range of values between 2 and 4 (only SortedSet has GetViewBetween):
        Helper.Info($"GetViewBetween(2,4): {Helper.AsText(sorted.GetViewBetween(2, 4))}");

        // ── Practical example: remove duplicates from a list ─────────────────
        Helper.Heading("Practical example: removing duplicates");
        var withDuplicates = new List<int> { 1, 2, 2, 3, 3, 3, 4 };
        var withoutDuplicates = new HashSet<int>(withDuplicates).ToList();
        Helper.Info($"Input:  {Helper.AsText(withDuplicates)}");
        Helper.Info($"Output: {Helper.AsText(withoutDuplicates)}");
    }
}
