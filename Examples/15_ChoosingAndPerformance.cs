using System.Diagnostics;

namespace Collections.Examples;

/// <summary>
/// CHOOSING THE RIGHT COLLECTION + PERFORMANCE (Big-O) — the most practical piece
/// of collection knowledge there is, and the most common interview theme.
///
/// The whole decision usually comes down to three questions:
///   1. How do I LOOK THINGS UP?  by position -> List/array, by key -> Dictionary,
///      "does it exist?" -> HashSet.
///   2. Does ORDER matter?  insertion order -> List, sorted -> Sorted*,
///      FIFO/LIFO -> Queue/Stack.
///   3. Who TOUCHES it?  several threads -> Concurrent*, nobody after startup ->
///      Frozen*, nobody ever -> Immutable*.
///
/// This example also MEASURES two classics live: List vs HashSet lookup, and
/// pre-sizing the capacity.
/// </summary>
public static class ChoosingAndPerformanceDemo
{
    public static void Run()
    {
        CheatSheet();
        ContainsBenchmark();
        CapacityBenchmark();
        Interfaces();
    }

    // ── Big-O cheat sheet ────────────────────────────────────────────────────
    private static void CheatSheet()
    {
        Helper.Heading("Big-O cheat sheet (average case)");
        // Raw string literal (C# 11) — multi-line text without escaping.
        Console.WriteLine("""
               collection            lookup     insert      remove     by index
               ────────────────────────────────────────────────────────────────
               T[] / List<T>         O(n)       O(1)* end   O(n)       O(1)
               Dictionary<K,V>       O(1)       O(1)        O(1)       —
               HashSet<T>            O(1)       O(1)        O(1)       —
               SortedDictionary<K,V> O(log n)   O(log n)    O(log n)   —
               SortedList<K,V>       O(log n)   O(n)        O(n)       O(1)
               SortedSet<T>          O(log n)   O(log n)    O(log n)   —
               LinkedList<T>         O(n)       O(1)**      O(1)**     —
               Queue<T> / Stack<T>   — (ends)   O(1)        O(1)       —

               *  amortized — occasionally the internal array grows (copy)
               ** only when you already hold the LinkedListNode
            """);
    }

    // ── Measured: the "wrong collection" bug ─────────────────────────────────
    private static void ContainsBenchmark()
    {
        Helper.Heading("Measured: List.Contains vs HashSet.Contains");
        const int N = 100_000, Lookups = 2_000;
        var list = Enumerable.Range(0, N).ToList();
        var set  = list.ToHashSet();

        var sw = Stopwatch.StartNew();
        int found = 0;
        for (int i = 0; i < Lookups; i++)
            if (list.Contains(N - 1 - i)) found++;      // O(n) — scans the list
        sw.Stop();
        var listMs = sw.Elapsed.TotalMilliseconds;

        sw.Restart();
        for (int i = 0; i < Lookups; i++)
            if (set.Contains(N - 1 - i)) found++;       // O(1) — hash jump
        sw.Stop();

        Helper.Info($"{Lookups:N0} lookups in {N:N0} items: List {listMs:F1} ms  vs  HashSet {sw.Elapsed.TotalMilliseconds:F1} ms");
        Helper.Info("Same result, wildly different cost — the classic 'wrong collection at scale' bug.");
        Helper.Info("(Stopwatch here is just an illustration — for real numbers use BenchmarkDotNet.)");
    }

    // ── Measured: tell it the size up front ──────────────────────────────────
    private static void CapacityBenchmark()
    {
        Helper.Heading("Measured: pre-sizing the capacity");
        const int M = 1_000_000;

        // Here the interesting cost is MEMORY, not time: a growing list doubles its
        // internal array (4, 8, 16, ...) and copies it every time it runs out of room.
        long before = GC.GetAllocatedBytesForCurrentThread();
        var grown = new List<int>();
        for (int i = 0; i < M; i++) grown.Add(i);       // ~20 grow+copy cycles
        long grownBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        before = GC.GetAllocatedBytesForCurrentThread();
        var sized = new List<int>(capacity: M);
        for (int i = 0; i < M; i++) sized.Add(i);       // zero copies
        long sizedBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        Helper.Info($"{M:N0} Adds allocated: growing {grownBytes / 1024.0 / 1024:F1} MB  vs  pre-sized {sizedBytes / 1024.0 / 1024:F1} MB");
        Helper.Info("All those temporary arrays are garbage the GC must clean up.");
        Helper.Info("Know the size in advance? Say so — Dictionary and HashSet take a capacity too.");
    }

    // ── The interface family ─────────────────────────────────────────────────
    private static void Interfaces()
    {
        Helper.Heading("The interface family (what to accept, what to return)");
        Helper.Info("IEnumerable<T>   -> 'can be foreach-ed'. The minimum — great as an INPUT parameter.");
        Helper.Info("ICollection<T>   -> adds Count, Add, Remove.");
        Helper.Info("IList<T>         -> adds access by index.");
        Helper.Info("IReadOnlyList<T> -> read-only view — great as a RETURN type.");
        Helper.Info("IQueryable<T>    -> LINQ run by a provider (Entity Framework -> SQL in the database).");

        // The point in practice: one method accepts a list, an array AND a set,
        // because it only asks for the minimum it needs:
        static int SumAll(IEnumerable<int> xs) => xs.Sum();
        Helper.Info($"SumAll(List) = {SumAll([1, 2, 3])}, SumAll(HashSet) = {SumAll(new HashSet<int> { 4, 5 })}");
        Helper.Info("Guideline: ACCEPT the most general type you can, RETURN the most informative.");
    }
}
