// See https://aka.ms/new-console-template for more information

int total = 0;
Dictionary<int, int> scratchcardCount = new Dictionary<int, int>();
using (TextReader tr = new StreamReader(File.OpenRead("input.txt")))
{
    string? line = null;
    int index = 0;
    int totalScratchcards = 0;
    while ((line = tr.ReadLine()) != null)
    {
        var scratchcardLine = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        var parts = scratchcardLine[1].Split('|', StringSplitOptions.RemoveEmptyEntries);
        var scratchcardNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p=> int.Parse(p.Trim()));
        var scratchcardWinningNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p=> int.Parse(p.Trim()));

        if(!scratchcardCount.ContainsKey(index))
        {
            scratchcardCount.Add(index, 1);
        }

        var Numbers = scratchcardNumbers.ToArray();
        var WinningNumbers = scratchcardWinningNumbers.ToArray();
        var MatchingNumbers = scratchcardNumbers.Intersect(scratchcardWinningNumbers).ToArray();
        var Score = MatchingNumbers.Count() > 0 ? (int)Math.Pow(2, MatchingNumbers.Count() - 1) : 0;

        if (MatchingNumbers.Count() > 0)
        {
            for(int n = 0; n < MatchingNumbers.Count(); n++)
            {
                if(!scratchcardCount.TryGetValue(index + 1 + n, out int count))
                {
                    scratchcardCount.Add(index + 1 + n, 1);
                }
                scratchcardCount[index + 1 + n] += scratchcardCount[index];
            }
        }

        Console.WriteLine($"{index}.Count +=  {scratchcardCount[index]}");
        totalScratchcards += scratchcardCount[index];
        total += Score;
        index++;
    }

    Console.WriteLine($"Part 1 Answer: {total}");
    Console.WriteLine($"Part 2 Answer: {totalScratchcards}");
}