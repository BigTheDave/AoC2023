// See https://aka.ms/new-console-template for more information


using (TextReader tr = new StreamReader(File.OpenRead("input.txt")))
{
    string? line = null;
    int index = 0;
    int total = 0;
    int totalScratchcards = 0;
    List<int> scratchcardCounts = new List<int>();
    while ((line = tr.ReadLine()) != null)
    {
        var scratchcardLine = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        var parts = scratchcardLine[1].Split('|', StringSplitOptions.RemoveEmptyEntries);
        var scratchcardNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p=> int.Parse(p.Trim()));
        var scratchcardWinningNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p=> int.Parse(p.Trim()));

        while(scratchcardCounts.Count <= index)
        {
            scratchcardCounts.Add(1);
        } 

        var Numbers = scratchcardNumbers.ToArray();
        var WinningNumbers = scratchcardWinningNumbers.ToArray();
        var MatchingNumbers = scratchcardNumbers.Intersect(scratchcardWinningNumbers).ToArray();
        var Score = MatchingNumbers.Count() > 0 ? (int)Math.Pow(2, MatchingNumbers.Count() - 1) : 0;

        if (MatchingNumbers.Count() > 0)
        {
            for(int n = 0; n < MatchingNumbers.Count(); n++)
            {

                while (scratchcardCounts.Count <= index + 1 + n)
                {
                    scratchcardCounts.Add(1);
                }
                scratchcardCounts[index + 1 + n] += scratchcardCounts[index];
            }
        }

        Console.WriteLine($"{index}.Count +=  {scratchcardCounts[index]}");
        totalScratchcards += scratchcardCounts[index];
        total += Score;
        index++;
    }

    Console.WriteLine($"Part 1 Answer: {total}");
    Console.WriteLine($"Part 2 Answer: {totalScratchcards}");
}