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

Each example also notes **which C# / .NET version** introduced the feature, so the project doubles as a quick timeline of how collections evolved.

## Project layout

```
.
├── Program.cs          # menu that runs the examples
├── Helper.cs           # tiny console-output helpers
├── Collections.csproj  # net8.0, C# 12, nullable + implicit usings enabled
└── Examples/
    ├── 01_Arrays.cs
    ├── 02_Lists.cs
    └── ...             # 13 examples in total
```

## Author

**Matej Zlatunić**

## License

Released under the [MIT License](https://opensource.org/licenses/MIT). Feel free to use it for learning.
