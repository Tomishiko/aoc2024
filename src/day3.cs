using System.Diagnostics;
using System.Text.RegularExpressions;
namespace AdventOfCode2024;


enum ActionType
{
    Do,
    Dont,
    Mull,
}
class HeapNode
{
    public ActionType action { get; set; }
    public string? value { get; set; } = null;
}

static partial class day3
{
    [GeneratedRegex(@"(?<=mul\()[0-9]+,[0-9]+(?=\))", RegexOptions.Compiled, "en-US")]
    private static partial Regex _regexP1();

    [GeneratedRegex(@"(do\(\)|don't\(\))", RegexOptions.Compiled, "en-US")]

    private static partial Regex _regexFlags();

    [GeneratedRegex(@"do\(\)", RegexOptions.Compiled, "en-US")]

    private static partial Regex _regexDoFlags();

    [GeneratedRegex(@"don't\(\)", RegexOptions.Compiled, "en-US")]

    private static partial Regex _regexDontFlags();

    public static void Run()
    {
        string text;
        var regexMul = _regexP1();
        var regexFlags = _regexFlags();
        var regexDoFlags = _regexDoFlags();
        var regexDontFlags = _regexDontFlags();
        using (var file = new StreamReader("./input/day3", System.Text.Encoding.UTF8, true, 1024))
        {
            text = file.ReadToEnd();
        }

        Stopwatch sw = Stopwatch.StartNew();
        var mulMatches = regexMul.Matches(text);
        int result = 0;
        foreach (Match match in mulMatches)
        {
            int[] tmp = Array.ConvertAll(match.Value.Split(","), int.Parse);
            result += tmp[0] * tmp[1];
        }

        sw.Stop();
        Console.WriteLine($"Part1: {result}");
        Console.WriteLine($"{sw.Elapsed.TotalMilliseconds} ms");

        //
        //
        //
        // Part2

        sw.Reset();
        sw.Start();
        result = 0;
        var dos = (IEnumerator<Match>)regexDoFlags.Matches(text).GetEnumerator();
        var donts = (IEnumerator<Match>)regexDontFlags.Matches(text).GetEnumerator();
        var muls = (IEnumerator<Match>)mulMatches.GetEnumerator();
        var heap = new PriorityQueue<HeapNode, int>();
        bool flag = true;
        do
        {
            HeapNode node;
            if (dos.MoveNext())
            {
                node = new HeapNode { action = ActionType.Do };
                heap.Enqueue(node, dos.Current.Index);
            }
            if (donts.MoveNext())
            {
                node = new HeapNode { action = ActionType.Dont };
                heap.Enqueue(node, donts.Current.Index);
            }
            if (muls.MoveNext())
            {
                node = new HeapNode
                {
                    action = ActionType.Mull,
                    value = muls.Current.Value
                };
                heap.Enqueue(node, muls.Current.Index);
            }
            switch (heap.Peek().action)
            {
                case ActionType.Do:
                    {
                        flag = true;
                        heap.Dequeue();
                        break;
                    }
                case ActionType.Dont:
                    {
                        flag = false;
                        heap.Dequeue();
                        break;
                    }
                default:
                    {
                        var value = heap.Dequeue().value;
                        if (flag)
                        {

                            int[] tmp = Array.ConvertAll(value.Split(","), int.Parse);
                            result += tmp[0] * tmp[1];
                        }
                        break;
                    }
            }

        } while (heap.Count > 0);

        sw.Stop();
        Console.WriteLine($"Part2: {result}");
        Console.WriteLine($"{sw.Elapsed.TotalMilliseconds} ms");

    }

}
