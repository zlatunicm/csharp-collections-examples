namespace Collections;

// File-scoped namespace (C# 10, 2021) — one namespace per file without the curly
// braces, so the whole file is indented one level less.

/// <summary>
/// Helper methods for tidier console output.
/// </summary>
public static class Helper
{
    /// <summary>Prints a small sub-heading inside a single demo example.</summary>
    public static void Heading(string text)
    {
        Console.WriteLine();
        Console.WriteLine($"── {text} " + new string('─', Math.Max(3, 58 - text.Length)));
    }

    /// <summary>Prints an indented line of information.</summary>
    public static void Info(string text) => Console.WriteLine("   " + text);

    /// <summary>Returns the elements of a collection as the string "[a, b, c]".</summary>
    public static string AsText<T>(IEnumerable<T> collection) =>
        "[" + string.Join(", ", collection) + "]";
}
