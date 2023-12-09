
void Part1()
{
    string file = "input.txt";
    using(TextReader reader = new StreamReader(file))
    {
        string? line;
        long total = 0;
        while((line = reader.ReadLine()) != null)
        {
            var sequence = line.Split(' ').Select(t => long.Parse(t)).ToArray();
            long nextNumber = sequence.Last();

            while (sequence.Any(t => t != sequence.First()))
            {
                sequence = GetDiffSequence(sequence);
                nextNumber += sequence.Last();
            }

            total += nextNumber;
            //Console.WriteLine($"{line} [{nextNumber}]");
        }
        Console.WriteLine($"Answer 1: {total}");
    }
    long[] GetDiffSequence(long[] sequence)
    {
        List<long> result = new List<long>();
        for (int i = 1; i < sequence.Length; i++)
        {
            result.Add(sequence[i] - sequence[i - 1]);
        }
        return result.ToArray();
    }
}
void Part2()
{
    string file = "input.txt";
    using (TextReader reader = new StreamReader(file))
    {
        string? line;
        long total = 0;
        while ((line = reader.ReadLine()) != null)
        {
            var sequence = line.Split(' ').Select(t => long.Parse(t)).ToArray();
            
            List<long> previousNumbers = new List<long>();
            previousNumbers.Add(sequence.First());
            while (sequence.Any(t => t != sequence.First()))
            {
                sequence = GetDiffSequence(sequence);
                previousNumbers.Add(sequence.First());                
            }
            long previousNumber = 0;
            for(int i = previousNumbers.Count - 1; i >=0; i--)
            {
                previousNumber = previousNumbers[i] - previousNumber;
            }
            total += previousNumber;
            //Console.WriteLine($"[{previousNumber}] {line}");
        }
        Console.WriteLine($"Answer 2: {total}");
    }

    long[] GetDiffSequence(long[] sequence)
    {
        List<long> result = new List<long>();
        for (int i = 1; i < sequence.Length; i++)
        {
            result.Add(sequence[i] - sequence[i - 1]);
        }
        return result.ToArray();
    }
}
Part1();
Part2();