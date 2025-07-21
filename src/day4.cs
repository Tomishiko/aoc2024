namespace AdventOfCode2024;

public static class day4
{

    static char[] word = { 'X', 'M', 'A', 'S' };

    public static void Run()
    {

        string[] lines = File.ReadAllLines("./input/day4");
        char[][] data = lines.Select(l => l.ToCharArray()).ToArray();
        int count = 0;
        for (int i = 0; i < data.Length; ++i)
        {
            for (int j = 0; j < data[i].Length; ++j)
            {
                if (data[i][j] == 'X')
                {
                    if (checkAdjacent(1, i, j, data))
                        count++;
                }
            }
        }
        Console.WriteLine(count);
    }
    static bool checkAdjacent(int wordIndex, int i, int j, char[][] data)
    {
        if (wordIndex >= word.Length)
            return true;

        if (i >= data.Length)
            return false;

        if (j >= data[i].Length)
            return false;

        for (int di = -1; di <= 1; ++di)
        {
            for (int dj = -1; dj <= 1; ++dj)
            {
                if (di == 0 && dj == 0) //dont include current element
                    continue;

                if ((i + di) >= data.Length || (j + dj) >= data[i].Length) //if we are at lower/right edge
                    continue;

                if ((i + di) < 0 || j + dj < 0) //if we are at the upper/left edge
                    continue;

                //Console.WriteLine($"i={i}, di={di}\nj={j}, dj={dj}\nli={data.Length}, lj = {data[i].Length}");
                if (data[i + di][j + dj] == word[wordIndex])
                    return checkAdjacent(wordIndex + 1, i + di, j + dj, data);
            }
        }
        return false;
    }
}
