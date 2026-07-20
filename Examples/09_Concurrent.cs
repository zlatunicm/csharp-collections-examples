using System.Collections.Concurrent;

namespace Collections.Examples;

/// <summary>
/// CONCURRENT (thread-safe) collections — System.Collections.Concurrent (.NET 4.0, 2010).
///
/// Ordinary collections (List, Dictionary...) are NOT safe when several THREADS
/// access them at the same time — you can end up with errors or corrupted data.
/// Concurrent collections are internally locked/optimized so that this is safe.
///
/// The main ones:
///   - ConcurrentDictionary&lt;K,V&gt; — a thread-safe dictionary.
///   - ConcurrentQueue&lt;T&gt;         — a thread-safe FIFO queue.
///   - ConcurrentStack&lt;T&gt;         — a thread-safe LIFO stack.
///   - ConcurrentBag&lt;T&gt;           — an unordered "bag", fast when each thread
///                                     adds and takes its own elements.
///   - BlockingCollection&lt;T&gt;      — producer/consumer scenarios.
///
/// When to use: as soon as several threads (Task, Parallel, async workers) share a collection.
///
/// Caution (a frequent interview trap): thread-safe METHODS don't make a SEQUENCE
/// of calls atomic — 'if (!d.ContainsKey(k)) d[k] = v;' is still a race condition;
/// use the atomic GetOrAdd / AddOrUpdate / TryXxx methods instead.
/// Real-world uses: in-memory caches (GetOrAdd), producer/consumer pipelines,
/// collecting results from Parallel.ForEach.
/// </summary>
public static class ConcurrentDemo
{
    public static void Run()
    {
        // ── ConcurrentDictionary from several threads ────────────────────────
        Helper.Heading("ConcurrentDictionary — safe counting from several threads");
        var counter = new ConcurrentDictionary<string, int>();

        // Parallel.For runs the iterations on several threads AT THE SAME TIME.
        Parallel.For(0, 1000, i =>
        {
            string key = (i % 3) switch    // switch expression (C# 8)
            {
                0 => "a",
                1 => "b",
                _ => "c",
            };
            // AddOrUpdate is ATOMIC: safe even when several threads hit the same key.
            counter.AddOrUpdate(key, addValue: 1, updateValueFactory: (_, old) => old + 1);
        });

        foreach (var (key, count) in counter.OrderBy(p => p.Key))
            Console.WriteLine($"   '{key}' counted {count} times");
        Helper.Info($"Total: {counter.Values.Sum()} (always exactly 1000 — none lost)");

        // GetOrAdd — get it if present, otherwise add and return:
        var value = counter.GetOrAdd("d", _ => 42);
        Helper.Info($"GetOrAdd('d'): {value}");

        // ── ConcurrentQueue ──────────────────────────────────────────────────
        Helper.Heading("ConcurrentQueue — a thread-safe queue");
        var queue = new ConcurrentQueue<int>();
        Parallel.For(0, 5, i => queue.Enqueue(i * 10));   // different threads add them

        // TryDequeue instead of Dequeue — because between the check and the take
        // another thread could empty the queue, so we use the "Try" pattern:
        var collected = new List<int>();
        while (queue.TryDequeue(out var item))
            collected.Add(item);
        collected.Sort();
        Helper.Info($"Collected from the queue: {Helper.AsText(collected)}");

        // ── ConcurrentBag ────────────────────────────────────────────────────
        Helper.Heading("ConcurrentBag — an unordered thread-safe 'bag'");
        var bag = new ConcurrentBag<int>();
        Parallel.For(0, 5, i => bag.Add(i));
        Helper.Info($"Contents (order is not guaranteed): {Helper.AsText(bag.OrderBy(x => x))}");
    }
}
