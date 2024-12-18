using System.IO;
namespace AdventOfCode2024;

static class day2
{
    public static void Run()
    {

        using (var file = new StreamReader("./input/day2", System.Text.Encoding.UTF8, true, 1024))
        {
            int countP1 = 0; // counter for part 1
            int countP2 = 0; // counter for part 2
            string line;
            while ((line = file.ReadLine()) != null)
            {
                int[] report = Array.ConvertAll(line.Split(" "), int.Parse);
                int[] diffs = report.Zip(report.Skip(1), (x, y) => x - y).ToArray();
                if (diffs.All(x => (x < 0 && x >= -3)) || diffs.All(x => x > 0 && x <= 3))
                    countP1++;
                if (valid_with_exception(diffs, true) || valid_with_exception(diffs, false))
                    countP2++;


            }
            Console.WriteLine($"{countP1}");



            Console.WriteLine($"{countP2}");
        }

    }
    static bool valid_with_exception(int[] diffs, bool asc)
    {
        int[] valid_diffs = new int[] { 1, 2, 3 };
        if (asc)
            for (int i = 0; i < valid_diffs.Length; i++)
                valid_diffs[i] = -valid_diffs[i];
        var invalid = diffs.Select((x, i) => new { val = x, index = i })
                           .Where(e => !valid_diffs.Contains(e.val)).ToArray();
        switch (invalid.Length)
        {
            case 0: return true;
            case 2:
                return (Math.Abs(invalid[0].index - invalid[1].index) == 1)
                    && valid_diffs.Contains(invalid[0].val + invalid[1].val);
            case 1:
                if (invalid[0].index == 0 || invalid[0].index == diffs.Length - 1)
                    return true;
                else
                {
                    int i = invalid[0].index;
                    return valid_diffs.Contains(diffs[i - 1] + diffs[i]) || valid_diffs.Contains(diffs[i] + diffs[i + 1]);


                }
            default:
                return false;




        }


    }
}

