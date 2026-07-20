namespace Collections.Examples;

/// <summary>
/// LinkedList&lt;T&gt; — a doubly linked list.
///
/// - Each node (LinkedListNode&lt;T&gt;) points to the PREVIOUS and the NEXT node.
/// - Inserting/removing IN THE MIDDLE is very fast (O(1)) IF you already have the
///   node, because only the pointers get relinked — no other elements move.
/// - BUT: there is NO access by index (no list[5]); getting the n-th element is O(n).
/// - Uses more memory (each node stores 2 pointers).
/// - System.Collections.Generic (C# 2 / generics, 2005).
///
/// When to use: rarely. In 95% of cases List&lt;T&gt; is the better choice. A LinkedList
/// makes sense with FREQUENT inserting/removing in the middle of a large collection.
///
/// Why List usually still wins (a good interview answer): array elements sit next
/// to each other, which the CPU cache loves; linked nodes are scattered across the
/// heap — so the "O(1) insert" often loses to List's O(n) copy in practice.
/// Real-world use: an LRU cache = LinkedList (usage order) + Dictionary (fast lookup).
/// </summary>
public static class LinkedListDemo
{
    public static void Run()
    {
        // ── Creating ─────────────────────────────────────────────────────────
        Helper.Heading("Creating and adding");
        var list = new LinkedList<string>();
        list.AddLast("B");            // add to the end
        list.AddLast("C");
        list.AddFirst("A");           // add to the front
        Helper.Info($"After AddFirst/AddLast: {Helper.AsText(list)}");

        // ── Working with nodes ───────────────────────────────────────────────
        Helper.Heading("Inserting relative to a node");
        LinkedListNode<string>? nodeB = list.Find("B");    // find the "B" node
        if (nodeB is not null)                             // 'is not null' (C# 9)
        {
            list.AddBefore(nodeB, "A2");    // insert BEFORE node B
            list.AddAfter(nodeB, "B2");     // insert AFTER node B
        }
        Helper.Info($"After AddBefore/AddAfter around 'B': {Helper.AsText(list)}");

        // ── First / last ─────────────────────────────────────────────────────
        Helper.Heading("First and Last");
        Helper.Info($"First: {list.First?.Value}, Last: {list.Last?.Value}");

        // ── Walking the nodes ────────────────────────────────────────────────
        Helper.Heading("Walking the nodes manually (Next)");
        var current = list.First;
        while (current is not null)
        {
            Console.Write($"{current.Value} ");
            current = current.Next;    // .Previous would go backwards
        }
        Console.WriteLine();

        // ── Removing ─────────────────────────────────────────────────────────
        Helper.Heading("Removing");
        list.Remove("A2");
        list.RemoveFirst();
        list.RemoveLast();
        Helper.Info($"After removals: {Helper.AsText(list)}");
    }
}
