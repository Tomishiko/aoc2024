using System.IO;
namespace AdventOfCode2024;

static class Day_1
{
    public static void Run()
    {
        var (left,right) = ReadData();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine("LINQ solution:");
        LinqSolution(left,right);
        watch.Stop();
        var elapsed = watch.Elapsed.TotalMicroseconds;
        Console.WriteLine($"Time: {elapsed}");
        Console.WriteLine("Loop solution:");
        watch.Reset();
        watch.Start();
        LoopSolution(left,right);
        watch.Stop();
        elapsed = watch.Elapsed.TotalMicroseconds;
        Console.WriteLine($"Time: {elapsed}");


    }
    static (List<int>,List<int>) ReadData()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        List<int> left = new List<int>(1000);
        List<int> right = new List<int>(1000);
        using (var reader = new StreamReader("./input/day1", System.Text.Encoding.UTF8, true, 1024))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var tmp = line.Split("  ", 2);
                left.Add(int.Parse(tmp[0]));
                right.Add(int.Parse(tmp[1]));

            }
        }
        watch.Stop();
        var time = watch.Elapsed.TotalMicroseconds;
        Console.WriteLine($"Time read: {time}\r\n");

        return (left,right);
    }
    // Nasty oneliners, (slow, but simple)
    static void LinqSolution(List<int>left,List<int>right)
    {
        int total = left.Order().Zip(right.Order(), (x, y) => Math.Abs(x - y)).Sum();
        var index = left
        .GroupBy(x => x, (value, appearances) => (value: value, count: appearances.Count()))
        .ToDictionary(pair => pair.value, pair => pair.count);

        var similarity = right
            .Where(index.ContainsKey)
            .Sum(rightValue => rightValue * index[rightValue]);
        Console.WriteLine($"{total} {similarity}");
    }
    static void LoopSolution(List<int>left,List<int>right)
    {
        left.Sort();
        right.Sort();
        int total = 0;
        for (int i = 0; i < left.Count; i++)
        {
            total += Math.Abs(left[i] - right[i]);
        }
        // Part 2 ********************************
        int indL = 0;
        int indR = 0;
        int repL = 1;
        int repR = 1;
        int score = 0;

        while (indL < left.Count && indR < right.Count)
        {
            while (indL < left.Count - 1 && left[indL] == left[indL + 1])
            {
                indL++;
                repL++;
            }
            while (indR < right.Count - 1 && right[indR] == right[indR + 1])
            {
                indR++;
                repR++;
            }
            if (left[indL] < right[indR])
            {
                indL++;
                repL = 1;
            }
            else if (left[indL] > right[indR])
            {
                indR++;
                repR = 1;
            }
            else
            {
                score += left[indL] * repR * repL;
                indL++;
                indR++;
                repR = 1;
                repL = 1;
            }
        }
        Console.WriteLine($"{total} {score}");
    }
}
