namespace AdventOfCode2024;

public static class day4
{

    static char[] word = { 'X', 'M', 'A', 'S' };

    public static void Run()
    {

        string[] lines = File.ReadAllLines("./input/day4");
        char[][] data = lines.Select(l => l.ToCharArray()).ToArray();
        //Console.WriteLine(partOne(data));
        Console.WriteLine(partTwo(data));
    }

    static int partTwo(char[][] data)
    {
        int count = 0;
        int countM = 0;
        (int, int)[] diags = { (-1, -1), (-1, 1), (1, -1), (1, 1) };

        for (int i = 1; i < data.Length - 1; ++i)
        {
            for (int j = 1; j < data[i].Length - 1; ++j)
            {
                if (data[i][j] == 'A')
                {
                    foreach ((int di, int dj) in diags)
                        if (data[i + di][j + dj] == 'M' && data[i-di][j-dj] == 'S')
                            countM++;

                    if (countM == 2 )
                        count++;
                    countM = 0;
                }
            }
        }
        return count;
    }

    static int partOne(char[][] data)
    {

        int count = 0;
        for (int i = 0; i < data.Length; ++i)
        {
            for (int j = 0; j < data[i].Length; ++j)
            {
                if (data[i][j] == word[0])
                {
                    checkAdjacent(i, j, data, ref count);
                }
            }
        }
        return count;
    }
    static bool checkAdjacent(int i, int j, char[][] data, ref int count)
    {

        if (i >= data.Length)
            return false;

        if (j >= data[i].Length)
            return false;

        for (int di = -1; di <= 1; ++di)
        {
            for (int dj = -1; dj <= 1; ++dj)
            {
                if (!checkBoundaries(i, j, di, dj, data.Length, data[i].Length))
                    continue;
                //Console.WriteLine($"i={i}, di={di}\nj={j}, dj={dj}\nli={data.Length}, lj = {data[i].Length}");
                if (data[i + di][j + dj] == word[1])
                    if (tryWord(2, i + di, j + dj, di, dj, data)) // if full word found
                        count++;
            }
        }

        return false;
    }
    static bool tryWord(int wordIndex, int i, int j, int di, int dj, char[][] data)
    {
        if (wordIndex >= word.Length)
            return true;

        if (!checkBoundaries(i, j, di, dj, data.Length, data[i].Length))
            return false;

        if (data[i + di][j + dj] == word[wordIndex])
            return tryWord(wordIndex + 1, i + di, j + dj, di, dj, data);

        return false;

    }
    static bool checkBoundaries(int i, int j, int di, int dj, int leni, int lenj)
    {

        if (di == 0 && dj == 0) //dont include current element
            return false;

        if ((i + di) >= leni || (j + dj) >= lenj) //if we are at lower/right edge
            return false;

        if ((i + di) < 0 || j + dj < 0) //if we are at the upper/left edge
            return false;
        return true;
    }
}
