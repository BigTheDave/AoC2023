

using System.Diagnostics;
using System.Text.RegularExpressions;

void Part1()
{
    string file = "input.txt";
    using (TextReader reader = new StreamReader(file))
    {
        string instructions = reader.ReadLine() ?? string.Empty;
        reader.ReadLine();
        string? line;
        Dictionary<string, Tuple<string, string>> Nodes = new Dictionary<string, Tuple<string, string>>();
        while ((line = reader.ReadLine()) != null)
        {
            Nodes.Add(line.Substring(0, 3), new Tuple<string, string>(line.Substring(7, 3), line.Substring(12, 3)));
        }
        var currentNode = "AAA";
        Console.WriteLine($"Instruction count {instructions.Length}");
        int steps = 0;
        Dictionary<string, int> visitCount = new Dictionary<string, int>();
        do
        {
            switch (instructions[steps % instructions.Length])
            {
                case 'L':
                    currentNode = Nodes[currentNode].Item1;
                    break;
                case 'R':
                    currentNode = Nodes[currentNode].Item2;
                    break;
            } 

            steps++;

        } while (currentNode != "ZZZ");
        Console.WriteLine($"Answer 1: {steps}");
    }
}

void Part2()
{
    Console.WriteLine("PART 2===============================");
    string file = "input.txt";
    using (TextReader reader = new StreamReader(file))
    {
        string instructions = reader.ReadLine() ?? string.Empty;
        reader.ReadLine();
        string? line;
        Dictionary<string, Tuple<string, string>> Nodes = new Dictionary<string, Tuple<string, string>>();
        while ((line = reader.ReadLine()) != null)
        {
            Nodes.Add(line.Substring(0, 3), new Tuple<string, string>(line.Substring(7, 3), line.Substring(12, 3)));
        }
        var startingNodes = Nodes.Select(t => t.Key).Where(t => t.EndsWith("A")).ToArray();
        long currentLCM = 1;
        foreach (var startingNode in startingNodes)
        {
            var currentNode = startingNode;
            int index = 0;
            while (!currentNode.EndsWith("Z"))
            {
                currentNode = instructions[index % instructions.Length] == 'L' ? Nodes[currentNode].Item1 : Nodes[currentNode].Item2;
                index++;
            }
            Console.WriteLine($"{startingNode} found {currentNode} in {index} steps");
            currentLCM = LCM(currentLCM, index);
        }

        Console.WriteLine($"Answer 2: {currentLCM} ");
    }
}

long LCM(long a, long b)
{
    var _A = Math.Max(a, b);
    var _B = Math.Min(a, b);
    for (long i = 1; i <= _A; i++)
    {
        if (_B * i % _A == 0)
        {
            return i * _B;
        }
    }
    return _A;
}

Part1();
Part2();