// See https://aka.ms/new-console-template for more information

int total = 0;
Dictionary<int, Scratchcard> scratchcards = new Dictionary<int, Scratchcard>();
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

        var card = GetOrCreate(index);        
        card.Numbers = scratchcardNumbers.ToArray();
        card.WinningNumbers = scratchcardWinningNumbers.ToArray();
        card.MatchingNumbers = scratchcardNumbers.Intersect(scratchcardWinningNumbers).ToArray();
        
        if(card.MatchingNumbers.Count() > 0)
        {
            for(int n = 0; n < card.MatchingNumbers.Count(); n++)
            {
                var copyCard = GetOrCreate(index + 1 + n);
                copyCard.Count += card.Count;                
            }
        }
        Console.WriteLine($"{index}.Count +=  {card.Count}");
        totalScratchcards += card.Count;
        total += card.Score;
        index++;
    }

    Console.WriteLine($"Part 1 Answer: {total}");
    Console.WriteLine($"Part 2 Answer: {totalScratchcards}");
}
Scratchcard GetOrCreate(int index)
{
    if (!scratchcards.TryGetValue(index, out var card))
    {
        card = new Scratchcard();
        scratchcards.Add(index, card);
    }

    return card;
}
class Scratchcard
{
    public int[] Numbers;
    public int[] WinningNumbers;
    public int[] MatchingNumbers;
    public int Count = 1;
    public int Score => MatchingNumbers.Count() > 0 ? (int)Math.Pow(2, MatchingNumbers.Count() - 1) : 0;
}