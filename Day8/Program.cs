string file = "input.txt";

void Part1()
{
    using(TextReader reader = new StreamReader(file))
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
        Console.WriteLine($"Answer 2: {steps}");
    }
}

Part1();
Part2();