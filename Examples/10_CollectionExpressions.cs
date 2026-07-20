namespace Collections.Examples;

/// <summary>
/// COLLECTION EXPRESSIONS — the biggest collection news in C# 12 (2023, .NET 8).
///
/// It introduces a SINGLE syntax with square brackets [ ... ] for creating
/// collections, no matter whether it's an array, a list, a Span, etc. The
/// compiler picks the right way to build it based on the TARGET type (target-typed).
///
/// It also includes the "spread" operator '..' which "pours" one collection into another.
/// </summary>
public static class CollectionExpressionsDemo
{
    public static void Run()
    {
        // ── Before C# 12 vs. now ─────────────────────────────────────────────
        Helper.Heading("The same [ ] syntax for different types (C# 12)");

        // Based on the variable's type, the compiler knows what to build:
        int[]        array    = [1, 2, 3];
        List<int>    list     = [1, 2, 3];
        Span<int>    span     = [1, 2, 3];
        IReadOnlyList<int> readOnly = [1, 2, 3];

        Helper.Info($"int[]:              {Helper.AsText(array)}");
        Helper.Info($"List<int>:          {Helper.AsText(list)}");
        Helper.Info($"Span<int> [1]:      {span[1]}");
        Helper.Info($"IReadOnlyList<int>: {Helper.AsText(readOnly)}");

        // Before C# 12 you had to write:
        //   int[] p = new int[] { 1, 2, 3 };
        //   List<int> l = new List<int> { 1, 2, 3 };
        Helper.Info("(before C# 12: 'new int[] {..}', 'new List<int> {..}' — now just [ ])");

        // ── Empty collection ─────────────────────────────────────────────────
        Helper.Heading("Empty collection");
        List<string> empty = [];
        Helper.Info($"Empty list, Count = {empty.Count}");

        // ── Spread operator '..' (C# 12) ─────────────────────────────────────
        Helper.Heading("Spread operator '..' — joining collections");
        int[] first  = [1, 2, 3];
        int[] second = [4, 5, 6];

        // '..' pours the elements of the other collection in at that spot:
        int[] joined = [.. first, .. second];
        Helper.Info($"[.. first, .. second]        -> {Helper.AsText(joined)}");

        int[] withInserts = [0, .. first, 99, .. second, 100];
        Helper.Info($"[0, ..first, 99, ..second, 100] -> {Helper.AsText(withInserts)}");

        // ── Combined with LINQ ───────────────────────────────────────────────
        Helper.Heading("Spread + LINQ");
        List<int> evens = [.. joined.Where(x => x % 2 == 0)];
        Helper.Info($"Only evens via spread+Where: {Helper.AsText(evens)}");

        // ── Nested collections ───────────────────────────────────────────────
        Helper.Heading("Nested (a matrix via [ ])");
        int[][] matrix =
        [
            [1, 2, 3],
            [4, 5, 6],
        ];
        Helper.Info($"matrix[1][2] = {matrix[1][2]}");

        // ── Passing to a method that expects a collection ────────────────────
        Helper.Heading("Passing directly to a method");
        Helper.Info($"Sum of [10,20,30] = {Total([10, 20, 30])}");   // the literal goes straight into the method
    }

    private static int Total(IEnumerable<int> numbers) => numbers.Sum();
}
