// See https://aka.ms/new-console-template for more information

System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
stopwatch.Start();
using (TextReader tr = new StreamReader(File.OpenRead("input.txt")))
{
    string? line = null;
    int index = 0;
    int total = 0;
    int totalScratchcards = 0;
    List<int> scratchcardCounts = new List<int>();
    while ((line = tr.ReadLine()) != null)
    {

        // Line Parsing
        var scratchcardLine = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        var parts = scratchcardLine[1].Split('|', StringSplitOptions.RemoveEmptyEntries);
        var scratchcardNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p=> int.Parse(p.Trim())).ToArray();
        var scratchcardWinningNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p=> int.Parse(p.Trim())).ToArray();

        // Make sure we have space in the list
        while(scratchcardCounts.Count <= index)
        {
            scratchcardCounts.Add(1); //We will always have one copy of this card initially
        } 
        
        // Magic LINQ call to get common values of lists/arrays
        var matchingNumbers = scratchcardNumbers.Intersect(scratchcardWinningNumbers).ToArray();

        // Score is 0 if theres no matches, otherwise it's 2^Matching Count
        var Score = matchingNumbers.Count() > 0 ? (int)Math.Pow(2, matchingNumbers.Count() - 1) : 0;

        /* 
         * Every copy of this scratch card gives copies of subsequent cards,
         * Every copy gives 1 card. 
         * We are never going to parse this card again, so its count will not change
         * We can add its count to subsequent cards right away
         */
        if (matchingNumbers.Count() > 0)
        {
            for(int n = 0; n < matchingNumbers.Count(); n++)
            {
                // Make sure we have space in the list 
                while (scratchcardCounts.Count <= index + 1 + n)
                {
                    scratchcardCounts.Add(1);
                }
                scratchcardCounts[index + 1 + n] += scratchcardCounts[index];
            }
        }

        /*
         * We won't ever be coming back to this card so we can take the current
         * count of this card and add it to the running total
         * This also means we don't have to bounds check the List
         */
        totalScratchcards += scratchcardCounts[index];
        total += Score;
        index++;
    }

    stopwatch.Stop();
    Console.WriteLine($"Part 1 Answer: {total}");
    Console.WriteLine($"Part 2 Answer: {totalScratchcards}");
    Console.WriteLine($"in {stopwatch.ElapsedMilliseconds} ms");
}