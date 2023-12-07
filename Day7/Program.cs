string file = "test.txt";


using (TextReader tr = new StreamReader(file))
{
    string? line;
    List<Hand> Hands = new List<Hand>();
    while ((line = tr.ReadLine()) != null)
    {
        Hands.Add(new Hand(line));
    }
    Hands = Hands.OrderBy(t => t.HandType)
        .ThenBy(t => t.CardValues[0])
        .ThenBy(t => t.CardValues[1])
        .ThenBy(t => t.CardValues[2])
        .ThenBy(t => t.CardValues[3])
        .ThenBy(t => t.CardValues[4])
        .ToList();
    var score = 0;
    for (int i = 0; i < Hands.Count; i++)
    {
        //Console.WriteLine($"[{i}] [{Hands[i].Cards}] {Hands[i].HandType} ");
        score += Hands[i].Bid * (i + 1);
    }
    Console.WriteLine($"Answer: {score}");
}

class Hand
{
    public Hand(string line)
    {
        var split = line.Split(' ');
        Cards = split[0].Trim();
        Bid = int.Parse(split[1].Trim());
        var cardTracking = new Dictionary<int, int>();

        for (int n = 0; n <= 14; n++)
        {
            cardTracking.Add(n, 0);
        }
        CardValues = new int[Cards.Length];
        for (int i = 0; i < Cards.Length; i++)
        {
            if(!int.TryParse($"{Cards[i]}", out var score))
            {
                switch (Cards[i])
                {
                    case 'T':
                        score = 10;
                        break;
                    case 'J':
                        score = 0; //For Part 1, change this to 11
                        break;
                    case 'Q':
                        score = 12;
                        break;
                    case 'K':
                        score = 13;
                        break;
                    case 'A':
                        score = 14;
                        break;
                }
            }
            CardValues[i] = score;
            cardTracking[score]++;
        }

        //Ignore Jokers, add the number of jokers to each found card
        switch(cardTracking.Skip(1).Max(kv=>Math.Min(5, kv.Value + cardTracking[0]))) //Part 2
        //switch(cardTracking.Max(kv=>Math.Min(5, kv.Value))) //Part 1
        {
            case 5: //5 of a kind
                HandType = HandType.FIVEOFAKIND;
                break;
            case 4: //4 of a kind
                HandType = HandType.FOUROFAKIND;
                break;
            case 3: //3 of a kind or full house
                if(cardTracking.Count(kv=>kv.Value >= 2) >= 2) //Part 2
                //if(cardTracking.Count(kv=>kv.Value == 2) == 2) //Part 1
                {
                    HandType = HandType.FULLHOUSE;
                }
                else
                {
                    HandType = HandType.THREEOFAKIND;
                }
                break;
            case 2: //Pair or Two Pair
                if(cardTracking.Count(kv=> kv.Value >= 2) >= 2) //Part 2
                //if(cardTracking.Any(kv=> kv.Value == 3)) //Part 1
                {
                    HandType = HandType.TWOPAIR;
                } 
                else
                {
                    HandType = HandType.ONEPAIR;
                }
                break;
            default: //High card
                HandType = HandType.HIGHCARD;
                break;
        }
    }
    public string Cards { get; set; }
    public int[] CardValues { get; set; }
    
    public HandType HandType { get; set; }
    public int Bid { get; set; }
}
enum HandType {
    FIVEOFAKIND = 7,
    FOUROFAKIND = 6,
    FULLHOUSE = 5,
    THREEOFAKIND = 4,
    TWOPAIR = 3,
    ONEPAIR = 2,
    HIGHCARD = 1

}