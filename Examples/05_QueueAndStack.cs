namespace Collections.Examples;

/// <summary>
/// Queue&lt;T&gt; and Stack&lt;T&gt; — collections with a special removal order.
///
/// Queue&lt;T&gt; = FIFO (First In, First Out) — like a line in a shop:
///   the first to enter is the first to leave. (C# 2 / generics, 2005)
///
/// Stack&lt;T&gt; = LIFO (Last In, First Out) — like a stack of plates:
///   the last one placed is the first one taken off. (C# 2 / generics, 2005)
///
/// When to use:
///   - Queue: processing tasks in order, messages, to-do lists by arrival order.
///   - Stack: undo/redo, going back, recursion without recursion, a parser.
///
/// Complexity: Enqueue/Dequeue/Push/Pop/Peek are all O(1).
/// Real-world uses: BFS graph traversal uses a Queue, DFS uses a Stack; job and
/// message queues, browser history, the call stack itself, expression parsers.
/// </summary>
public static class QueueAndStackDemo
{
    public static void Run()
    {
        // ── Queue<T> (FIFO) ──────────────────────────────────────────────────
        Helper.Heading("Queue<T> — a queue (FIFO)");
        var queue = new Queue<string>();
        queue.Enqueue("prvi");    // Enqueue = add to the end of the queue
        queue.Enqueue("drugi");
        queue.Enqueue("treci");
        Helper.Info($"Queue after Enqueue: {Helper.AsText(queue)}");

        Helper.Info($"Peek (just look at the first one): {queue.Peek()}");   // does not remove
        var dequeued = queue.Dequeue();   // Dequeue = remove and return the first one
        Helper.Info($"Dequeue produced: {dequeued}");
        Helper.Info($"Queue now:        {Helper.AsText(queue)}");

        Helper.Heading("Draining the queue in arrival order");
        var jobs = new Queue<string>(["mail", "backup", "izvjestaj"]);
        while (jobs.Count > 0)
            Console.WriteLine($"   Processing: {jobs.Dequeue()}");

        // ── Stack<T> (LIFO) ──────────────────────────────────────────────────
        Helper.Heading("Stack<T> — a stack (LIFO)");
        var stack = new Stack<string>();
        stack.Push("stranica A");   // Push = put on top
        stack.Push("stranica B");
        stack.Push("stranica C");
        Helper.Info($"Stack after Push: {Helper.AsText(stack)}  (top is on the left)");

        Helper.Info($"Peek (top):     {stack.Peek()}");
        var popped = stack.Pop();   // Pop = take off the top and return it
        Helper.Info($"Pop took off:   {popped}");
        Helper.Info($"Stack now:      {Helper.AsText(stack)}");

        // ── Practical example: bracket checking (balancing) ──────────────────
        Helper.Heading("Practical example (Stack): bracket checking");
        foreach (var expression in new[] { "(a+b)*[c-d]", "(a+b))", "([)]" })
            Console.WriteLine($"   '{expression}' -> {(AreBracketsBalanced(expression) ? "VALID" : "ERROR")}");
    }

    // A classic use of a stack: check whether the brackets are correctly nested.
    private static bool AreBracketsBalanced(string expression)
    {
        var stack = new Stack<char>();
        var pairs = new Dictionary<char, char> { [')'] = '(', [']'] = '[', ['}'] = '{' };

        foreach (var ch in expression)
        {
            if (ch is '(' or '[' or '{')            // the 'is ... or ...' pattern (C# 9)
                stack.Push(ch);
            else if (pairs.ContainsKey(ch))
            {
                if (stack.Count == 0 || stack.Pop() != pairs[ch])
                    return false;
            }
        }
        return stack.Count == 0;
    }
}
