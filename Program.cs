using Collections;
using Collections.Examples;

// ─────────────────────────────────────────────────────────────────────────────
//  MAIN PROGRAM  —  a menu of all the demo examples
// ─────────────────────────────────────────────────────────────────────────────
//  We use "top-level statements" (C# 9, 2020): no need to write
//  'class Program { static void Main(string[] args) { ... } }' — we write the
//  code directly. The 'args' variable is available automatically.
// ─────────────────────────────────────────────────────────────────────────────

// The registry of all examples. We use an array of tuples (ValueTuple, C# 7).
var demos = new (string Title, Action Method)[]
{
    ("01 — Arrays",                               ArraysDemo.Run),
    ("02 — List<T>",                              ListsDemo.Run),
    ("03 — Dictionary<TKey,TValue>",              DictionariesDemo.Run),
    ("04 — HashSet<T> and SortedSet<T>",          SetsDemo.Run),
    ("05 — Queue<T> and Stack<T>",                QueueAndStackDemo.Run),
    ("06 — LinkedList<T>",                        LinkedListDemo.Run),
    ("07 — SortedList / SortedDictionary",        SortedDemo.Run),
    ("08 — Read-only & Immutable",                ImmutableDemo.Run),
    ("09 — Concurrent",                           ConcurrentDemo.Run),
    ("10 — Collection expressions (C# 12)",       CollectionExpressionsDemo.Run),
    ("11 — Index, Range and Span<T>",             SpanAndRangesDemo.Run),
    ("12 — Frozen collections (.NET 8)",          FrozenDemo.Run),
    ("13 — IEnumerable, yield and LINQ",          LinqDemo.Run),
    ("14 — Common pitfalls",                      PitfallsDemo.Run),
    ("15 — Choosing & performance (Big-O)",       ChoosingAndPerformanceDemo.Run),
};

// If a number was passed as an argument (e.g. `dotnet run 3`), run only that
// example. Otherwise run all of them in order.
if (args.Length > 0 && int.TryParse(args[0], out var number) && number >= 1 && number <= demos.Length)
{
    RunDemo(demos[number - 1]);
}
else
{
    Console.WriteLine("Running ALL examples. (Tip: `dotnet run 3` runs only example #3.)");
    foreach (var demo in demos)
        RunDemo(demo);
}

// Local function (C# 7) — a helper method visible only inside this file.
static void RunDemo((string Title, Action Method) demo)
{
    Console.WriteLine();
    Console.WriteLine(new string('═', 62));
    Console.WriteLine($"  {demo.Title}");
    Console.WriteLine(new string('═', 62));
    demo.Method();
}
