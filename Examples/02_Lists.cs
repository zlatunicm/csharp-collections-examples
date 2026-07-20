namespace Collections.Examples;

/// <summary>
/// List&lt;T&gt; — the most commonly used collection in practice.
///
/// - DYNAMIC size: it grows and shrinks as needed (internally it's an array that
///   is enlarged when it runs out of room).
/// - Keeps the INSERTION ORDER and allows DUPLICATES.
/// - Access by index is fast (O(1)); appending to the end is fast (amortized).
/// - Lives in System.Collections.Generic.
///
/// "Generic" collections (List&lt;T&gt;, Dictionary&lt;,&gt; ...) arrived in C# 2 (2005).
/// Before that there were only untyped ones (ArrayList) which are NOT used today.
/// </summary>
public static class ListsDemo
{
    public static void Run()
    {
        // ── Creating and adding ──────────────────────────────────────────────
        Helper.Heading("Creating and adding");

        var fruit = new List<string>();    // 'var' (C# 3, 2007) — the type is inferred
        fruit.Add("jabuka");
        fruit.Add("kruska");
        fruit.Add("banana");
        Helper.Info($"After Add:         {Helper.AsText(fruit)}");

        fruit.AddRange(["mango", "sljiva"]);  // adds several at once (here the C# 12 [ ])
        Helper.Info($"After AddRange:    {Helper.AsText(fruit)}");

        fruit.Insert(0, "limun");             // insert at an exact position
        Helper.Info($"Insert at index 0: {Helper.AsText(fruit)}");

        // Collection initializer (C# 3) — fill the list right away:
        var numbers = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6 };
        Helper.Info($"Initialized:       {Helper.AsText(numbers)}");

        // ── Reading ──────────────────────────────────────────────────────────
        Helper.Heading("Reading and properties");
        Helper.Info($"Element count (Count): {fruit.Count}");
        Helper.Info($"Element [2]:           {fruit[2]}");
        Helper.Info($"Contains 'banana'?     {fruit.Contains("banana")}");
        Helper.Info($"IndexOf('banana'):     {fruit.IndexOf("banana")}");

        // ── Removing ─────────────────────────────────────────────────────────
        Helper.Heading("Removing");
        fruit.Remove("limun");          // remove by value (the first match)
        fruit.RemoveAt(0);              // remove by index
        Helper.Info($"After removals:   {Helper.AsText(fruit)}");

        fruit.RemoveAll(f => f.StartsWith("m"));  // remove all by condition (lambda, C# 3)
        Helper.Info($"RemoveAll 'm*':   {Helper.AsText(fruit)}");

        // ── Sorting and searching ────────────────────────────────────────────
        Helper.Heading("Sorting");
        numbers.Sort();
        Helper.Info($"Sort (ascending):  {Helper.AsText(numbers)}");

        numbers.Sort((a, b) => b.CompareTo(a));   // custom comparator -> descending
        Helper.Info($"Sort (descending): {Helper.AsText(numbers)}");

        // ── LINQ over a list (C# 3) ──────────────────────────────────────────
        Helper.Heading("LINQ over a list");
        var evens = numbers.Where(x => x % 2 == 0).OrderBy(x => x).ToList();
        Helper.Info($"Even numbers:     {Helper.AsText(evens)}");
        Helper.Info($"Sum:              {numbers.Sum()}");
        Helper.Info($"Average:          {numbers.Average():F2}");

        // ── Capacity (an advanced detail) ────────────────────────────────────
        Helper.Heading("Count vs Capacity");
        var l = new List<int>(capacity: 10);   // reserve room up front
        l.Add(1); l.Add(2);
        Helper.Info($"Count={l.Count}, Capacity={l.Capacity}  (Count = how many there are, Capacity = how many fit without reallocation)");
    }
}
