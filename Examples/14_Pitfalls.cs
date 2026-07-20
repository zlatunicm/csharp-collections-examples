namespace Collections.Examples;

/// <summary>
/// COMMON PITFALLS — the collection mistakes every C# developer runs into sooner
/// or later (and interviewers love to ask about). Each one is demonstrated live,
/// with the fix right next to it.
///
/// 1) Modifying a collection while foreach-ing it -> InvalidOperationException.
/// 2) Deferred execution of LINQ — the query runs LATER (and possibly many times).
/// 3) Returning null instead of an empty collection -> null checks everywhere.
/// 4) Wrong key type: reference equality vs value equality in a Dictionary.
/// </summary>
public static class PitfallsDemo
{
    public static void Run()
    {
        ForeachModification();
        DeferredExecution();
        NullVsEmpty();
        KeyEquality();
    }

    // ── 1) Changing a collection inside foreach ──────────────────────────────
    private static void ForeachModification()
    {
        Helper.Heading("1) Changing a collection inside foreach");
        var numbers = new List<int> { 1, 2, 3, 4 };
        try
        {
            foreach (var n in numbers)
                if (n % 2 == 0)
                    numbers.Remove(n);      // BOOM — the enumerator detects the change
        }
        catch (InvalidOperationException e)
        {
            Helper.Info($"Caught: {e.Message}");
        }

        // The fixes: RemoveAll (best), a backwards for-loop, or iterate over a copy
        // (numbers.ToList()). Fun fact: since .NET Core 3.0 Dictionary DOES allow
        // Remove during foreach — but List does not.
        numbers.RemoveAll(n => n % 2 == 0);
        Helper.Info($"Fix with RemoveAll: {Helper.AsText(numbers)}");
    }

    // ── 2) Deferred execution of LINQ ────────────────────────────────────────
    private static void DeferredExecution()
    {
        Helper.Heading("2) Deferred execution of LINQ");
        var source = new List<int> { 1, 2, 3 };
        var query = source.Where(x => x >= 2);   // nothing has executed yet!
        source.Add(4);                           // change the source AFTER building the query

        Helper.Info($"Query built before Add(4), enumerated now: {Helper.AsText(query)}  (it SEES 4!)");

        // ...and it executes again EVERY time you enumerate it:
        int evaluations = 0;
        var counted = source.Where(x => { evaluations++; return x >= 2; });
        _ = counted.Count();     // 1st enumeration
        _ = counted.ToList();    // 2nd enumeration
        Helper.Info($"The lambda ran {evaluations}x for {source.Count} elements (two enumerations).");
        Helper.Info("Fix: materialize once with ToList()/ToArray() when you reuse the result.");
    }

    // ── 3) Return an empty collection, not null ──────────────────────────────
    private static void NullVsEmpty()
    {
        Helper.Heading("3) Return an empty collection, not null");
        // A method returning null forces every caller to null-check before foreach.
        // Return an empty collection instead — foreach over it simply does nothing:
        foreach (var order in FindOrders(customerExists: false))
            Console.WriteLine($"   {order}");    // never runs, never crashes
        Helper.Info("FindOrders returned [] — the caller needs no null check.");
        Helper.Info("(Use [] / Array.Empty<T>() / Enumerable.Empty<T>() — they allocate nothing.)");
    }

    private static IEnumerable<string> FindOrders(bool customerExists) =>
        customerExists ? ["narudzba-1", "narudzba-2"] : [];

    // ── 4) Dictionary keys: value vs reference equality ──────────────────────
    private static void KeyEquality()
    {
        Helper.Heading("4) Dictionary keys: value vs reference equality");

        // A class WITHOUT Equals/GetHashCode overrides compares by REFERENCE:
        var byClass = new Dictionary<ClassKey, int> { [new ClassKey("Ana")] = 1 };
        byClass[new ClassKey("Ana")] = 2;        // looks like the same key...
        Helper.Info($"class key 'Ana' twice:  Count = {byClass.Count}  (TWO entries — different references!)");

        // A record (C# 9) gets VALUE equality generated automatically:
        var byRecord = new Dictionary<RecordKey, int> { [new RecordKey("Ana")] = 1 };
        byRecord[new RecordKey("Ana")] = 2;      // genuinely the same key
        Helper.Info($"record key 'Ana' twice: Count = {byRecord.Count}  (ONE entry — value equality)");

        Helper.Info("Rule: keys need correct Equals+GetHashCode (records give you both),");
        Helper.Info("and NEVER mutate an object while it serves as a key — its hash goes stale.");
    }

    // Primary constructor (C# 12) — the class keeps default reference equality.
    private sealed class ClassKey(string name) { public string Name { get; } = name; }

    private sealed record RecordKey(string Name);
}
