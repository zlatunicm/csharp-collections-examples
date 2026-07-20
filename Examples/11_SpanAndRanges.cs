namespace Collections.Examples;

/// <summary>
/// INDEX, RANGE and Span&lt;T&gt; — modern, fast work with parts of collections.
///
/// The Index (^) and Range (..) operators — C# 8 (2019):
///   - Index: ^1 means "first from the end". collection[^1] = the last element.
///   - Range: a..b means "from a (inclusive) to b (exclusive)". Takes a "slice".
///
/// Span&lt;T&gt; / ReadOnlySpan&lt;T&gt; (.NET Core 2.1 / C# 7.2, 2017):
///   - A "window" over existing memory (an array, a string...) WITHOUT copying.
///   - Very fast: slicing does not allocate new memory.
///   - Limitation: it can only live on the stack (it can't be an array element or a
///     class field), it can't be used in async methods, etc.
///
/// When to use: processing large arrays/texts where you want to avoid needless
/// copies for the sake of performance (parsing, a network buffer, and so on).
/// </summary>
public static class SpanAndRangesDemo
{
    public static void Run()
    {
        // ── Index operator ^ (C# 8) ──────────────────────────────────────────
        Helper.Heading("Index operator '^' (from the end)");
        int[] numbers = [10, 20, 30, 40, 50];
        Helper.Info($"Array:            {Helper.AsText(numbers)}");
        Helper.Info($"numbers[^1] (last):            {numbers[^1]}");
        Helper.Info($"numbers[^2] (second to last):  {numbers[^2]}");

        // ── Range operator .. (C# 8) ─────────────────────────────────────────
        Helper.Heading("Range operator '..' (slices)");
        Helper.Info($"numbers[1..4]  (from 1 to 4 exclusive): {Helper.AsText(numbers[1..4])}");
        Helper.Info($"numbers[..2]   (first two):             {Helper.AsText(numbers[..2])}");
        Helper.Info($"numbers[3..]   (from index 3 to end):   {Helper.AsText(numbers[3..])}");
        Helper.Info($"numbers[1..^1] (without first and last): {Helper.AsText(numbers[1..^1])}");

        // Works on strings too:
        string word = "programiranje";
        Helper.Info($"\"{word}\"[0..4] = \"{word[0..4]}\"");

        // ── Span<T> — slicing without copying ────────────────────────────────
        Helper.Heading("Span<T> — a 'window' without copying");
        int[] data = [1, 2, 3, 4, 5, 6, 7, 8];
        Span<int> window = data.AsSpan(2, 3);   // a view of 3 elements starting at index 2
        Helper.Info($"Span (data[2..5]): {Helper.AsText(window.ToArray())}");

        // KEY POINT: changing through the Span changes the ORIGINAL array (same memory):
        window[0] = 999;
        Helper.Info($"After window[0]=999, the original array: {Helper.AsText(data)}  (changed!)");

        // ── ReadOnlySpan over a string (no allocations) ──────────────────────
        Helper.Heading("ReadOnlySpan<char> — fast text processing");
        ReadOnlySpan<char> text = "2026-07-20";
        ReadOnlySpan<char> year = text[..4];     // does not create a new string
        ReadOnlySpan<char> month = text[5..7];
        Helper.Info($"Year: {year}, month: {month}  (sliced without new string objects)");
    }
}
