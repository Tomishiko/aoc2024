using System.IO;
namespace AdventOfCode2024;

static class Day_1
{
    public static void Hello()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here

        List<int> left = new List<int>(1000);
        List<int> right = new List<int>(1000);
        //FileStream fs  = File.OpenRead("./input/day1");
        //using (var reader = new StreamReader("./input/day1_test"))
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
        Console.WriteLine("LINQ solution:\r\n");
        LinqSolution(left, right);
        watch.Stop();
        var elapsed = watch.Elapsed.TotalMicroseconds;
        Console.WriteLine($"Time: {elapsed}");
        //LoopSolution(left,right);
        //watch.Stop();
        //var elapsed = watch.Elapsed.TotalMicroseconds;
        //Console.WriteLine("Loop soulution:\r\n");
        //Console.WriteLine($"Time: {elapsed}");


    }
    // Dirty oneliner (worst perfomance, least code)
    public static void LinqSolution(List<int> left, List<int> right)
    {
        //Part 1 -*********************************
        int total = left.Order().Zip(right.Order(), (x, y) => Math.Abs(x - y)).Sum();

        Console.WriteLine($"Part1: {total}");
        var index = left
        .GroupBy(x => x, (value, appearances) => (value: value, count: appearances.Count()))
        .ToDictionary(pair => pair.value, pair => pair.count);

        var similarity = right
            .Where(index.ContainsKey)
            .Sum(rightValue => rightValue * index[rightValue]);
        Console.WriteLine($"Part2: {similarity}");
    }
    public static void LoopSolution(List<int> left, List<int> right)
    {
        left.Sort();
        right.Sort();
        int total = 0;
        for (int i = 0; i < left.Count; i++)
        {
            total += Math.Abs(left[i] - right[i]);
        }
        Console.WriteLine($"{total}");
        // Part 2 ********************************


    }
}
