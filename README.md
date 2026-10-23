# C# Collections — Hands-on Examples

A small, self-contained console project that walks through **every major collection type in C#**, from the humble array all the way to the newest additions in **C# 12 / .NET 8**. Each topic lives in its own file with heavily commented, runnable examples so you can read the explanation *and* watch the output.

> The data inside the examples (fruit names, cities, people…) is intentionally left in the author's native language — everything else (code, names, comments, explanations) is in English.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or newer

## Running

Run **all** examples in order:

```bash
dotnet run
```

Run a **single** example by its number (e.g. dictionaries):

```bash
dotnet run 3
```

## What's inside

| #  | Topic                        | Key types |
|----|------------------------------|-----------|
| 01 | Arrays                       | `T[]`, `int[,]` |
| 02 | Lists                        | `List<T>` |
| 03 | Dictionaries                 | `Dictionary<TKey,TValue>` |
| 04 | Sets                         | `HashSet<T>`, `SortedSet<T>` |
| 05 | Queue & Stack                | `Queue<T>`, `Stack<T>` |
| 06 | Linked list                  | `LinkedList<T>` |
| 07 | Sorted collections           | `SortedList<,>`, `SortedDictionary<,>` |
| 08 | Read-only & immutable        | `IReadOnlyList<T>`, `ImmutableList<T>` … |
| 09 | Concurrent (thread-safe)     | `ConcurrentDictionary<,>`, `ConcurrentQueue<T>` … |
| 10 | Collection expressions       | `[ ... ]`, spread `..` (C# 12) |
| 11 | Index, Range & Span          | `^`, `..`, `Span<T>` |
| 12 | Frozen collections           | `FrozenDictionary<,>`, `FrozenSet<T>` (.NET 8) |
| 13 | IEnumerable, yield & LINQ    | `IEnumerable<T>`, `yield`, LINQ |
| 14 | Common pitfalls              | foreach + modify, deferred execution, null vs `[]`, key equality |
| 15 | Choosing & performance       | Big-O cheat sheet, live `List` vs `HashSet` benchmark, interfaces |

Each example also notes **which C# / .NET version** introduced the feature, so the project doubles as a quick timeline of how collections evolved. Every file's header ends with the **complexity (Big-O)** of its operations and **real-world uses**, and examples 14–15 cover the questions developers ask (and get asked) most: common pitfalls, how `Dictionary` works internally, and how to pick the right collection.

## Quick decision guide

| You need… | Reach for |
|---|---|
| Items in order, access by position | `List<T>` (or `T[]` if the size is fixed) |
| Lookup by a unique key | `Dictionary<TKey,TValue>` |
| Uniqueness / fast "does it exist?" | `HashSet<T>` |
| Keys/elements always sorted | `SortedDictionary<,>` / `SortedSet<T>` |
| First-in-first-out / last-in-first-out | `Queue<T>` / `Stack<T>` |
| Shared between threads | `Concurrent*` |
| Built once at startup, read forever | `Frozen*` (.NET 8) |
| Guaranteed never to change | `Immutable*` |
| Slices of arrays/strings without copying | `Span<T>` / `ReadOnlySpan<T>` |

## Project layout

```
.
├── Program.cs          # menu that runs the examples
├── Helper.cs           # tiny console-output helpers
├── Collections.csproj  # net8.0, C# 12, nullable + implicit usings enabled
└── Examples/
    ├── 01_Arrays.cs
    ├── 02_Lists.cs
    └── ...             # 15 examples in total
```

## Further reading

- [Collections in .NET](https://learn.microsoft.com/en-us/dotnet/standard/collections/) — the official overview
- [Selecting a collection class](https://learn.microsoft.com/en-us/dotnet/standard/collections/selecting-a-collection-class) — Microsoft's own decision guide
- [Framework Design Guidelines: Collections](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/guidelines-for-collections) — what to accept and return from APIs

## The series

Next: [02 LINQ](https://github.com/zlatunicm/csharp-linq-examples) →

| Part | Topic |
|---|---|
| 01 | **Collections** (this repo) |
| 02 | [LINQ](https://github.com/zlatunicm/csharp-linq-examples) |
| 03 | [Delegates & events](https://github.com/zlatunicm/csharp-delegates-events-examples) |
| 04 | [Async/await](https://github.com/zlatunicm/csharp-async-examples) |
| 05 | [Generics](https://github.com/zlatunicm/csharp-generics-examples) |
| 06 | [Types & memory](https://github.com/zlatunicm/csharp-types-memory-examples) |
| 07 | [Exceptions](https://github.com/zlatunicm/csharp-exceptions-examples) |
| 08 | [Modern C# 8–12](https://github.com/zlatunicm/csharp-modern-examples) |
| 09 | [Strings & text](https://github.com/zlatunicm/csharp-strings-examples) |
| 10 | [Files & JSON](https://github.com/zlatunicm/csharp-files-json-examples) |
| 11 | [MiniShop microservices (finale)](https://github.com/zlatunicm/minishop-microservices) |

## Author

**Matej Zlatunić**

## License

Released under the [MIT License](LICENSE). Feel free to use it for learning.
