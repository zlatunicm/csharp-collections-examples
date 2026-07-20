namespace Collections.Examples;

/// <summary>
/// ARRAY (T[]) — the most basic collection in C#.
///
/// - FIXED size (the number of elements can't change after creation).
/// - Elements share the same type and sit next to each other in memory (fast access).
/// - Elements are accessed by INDEX (starting at 0).
/// - Lives in System (since the very beginning of C#, 2002).
///
/// When to use: when you know the exact number of elements up front and want
/// maximum speed. If the count changes -> use List<T> (example 02).
/// </summary>
public static class ArraysDemo
{
    public static void Run()
    {
        // ── Creating an array ────────────────────────────────────────────────
        Helper.Heading("Creating an array");

        // The classic way: an array of 5 ints (all 0 to begin with).
        int[] numbers = new int[5];
        numbers[0] = 10;
        numbers[1] = 20;
        Helper.Info($"Empty array then written: {Helper.AsText(numbers)}");

        // Initialization with values (collection initializer).
        string[] names = { "Ana", "Marko", "Iva" };
        Helper.Info($"Filled right away:        {Helper.AsText(names)}");

        // Since C# 12 (2023) we can also use a "collection expression" [ ... ]:
        int[] grades = [5, 4, 3, 5, 2];
        Helper.Info($"C# 12 syntax [ ]:         {Helper.AsText(grades)}");

        // ── Access and modification ──────────────────────────────────────────
        Helper.Heading("Accessing elements");
        Helper.Info($"First element (index 0):     {grades[0]}");
        Helper.Info($"Array length (Length):       {grades.Length}");

        // The "from the end" index operator ^ (C# 8, 2019): ^1 = last element.
        Helper.Info($"Last element (index ^1):     {grades[^1]}");

        // ── Walking through the array ────────────────────────────────────────
        Helper.Heading("Iteration (foreach)");
        foreach (var grade in grades)
            Console.Write($"{grade} ");
        Console.WriteLine();

        // ── Handy static methods of the Array class ──────────────────────────
        Helper.Heading("Array helper methods");
        int[] unsorted = [5, 1, 4, 2, 3];
        Array.Sort(unsorted);                          // sorts IN PLACE
        Helper.Info($"Array.Sort:        {Helper.AsText(unsorted)}");

        Array.Reverse(unsorted);
        Helper.Info($"Array.Reverse:     {Helper.AsText(unsorted)}");

        int position = Array.IndexOf(unsorted, 4);
        Helper.Info($"Array.IndexOf(4):  {position}");

        // ── Multidimensional array (matrix) ──────────────────────────────────
        Helper.Heading("2D array (matrix)");
        int[,] matrix =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
        };
        Helper.Info($"Rows: {matrix.GetLength(0)}, columns: {matrix.GetLength(1)}");
        Helper.Info($"Element [1,2] = {matrix[1, 2]}");   // -> 6
    }
}
