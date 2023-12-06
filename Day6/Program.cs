﻿string file = "input.txt";
void Part1()
{ 
    using (TextReader reader = new StreamReader(File.OpenRead(file)))
    {
        var times = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(t=>int.Parse(t))
            .ToArray();
        var distances = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(t => int.Parse(t))
            .ToArray();
        int answer = 1;
        for(int race = 0; race < times.Length; race++)
        {
            int waysToWin = 0;
            var totalRaceTime = times[race];
            var distanceToBeat = distances[race];
            for(int timeHeld = 0; timeHeld < totalRaceTime; timeHeld ++ )
            {
                int timeRun = totalRaceTime - timeHeld;
                int distanceRun = (timeHeld * timeRun);
                if (distanceRun > distanceToBeat)
                {
                    Console.WriteLine($"WIN Held {timeHeld} ms, Run {timeRun} ms {distanceRun} mm");
                    waysToWin++;
                } 
                else
                {
                    Console.WriteLine($"LOS Held {timeHeld} ms, Run {timeRun} ms {distanceRun} mm");
                }
            }
            answer *= waysToWin;
            Console.WriteLine($"Race {race}, #{waysToWin} ways to win");
        }
        Console.WriteLine($"Part 1 Answer: {answer}");
    }
}

void Part2()
{
    Console.WriteLine("Part 2");

    using (TextReader reader = new StreamReader(File.OpenRead(file)))
    {
        var totalRaceTime = reader.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(t => long.Parse(t.Replace(" ", "")))
            .First();
        var distanceToBeat = reader.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(t => long.Parse(t.Replace(" ", "")))
            .First();
        int waysToWin = 0;
        for (long timeHeld = 0; timeHeld < totalRaceTime; timeHeld++)
        {
            long timeRun = totalRaceTime - timeHeld;
            long distanceRun = (timeHeld * timeRun);
            if (distanceRun > distanceToBeat)
            {
                //Console.WriteLine($"WIN Held {timeHeld} ms, Run {timeRun} ms {distanceRun} mm");
                waysToWin++;
            }
            else
            {
                //Console.WriteLine($"LOS Held {timeHeld} ms, Run {timeRun} ms {distanceRun} mm");
            }
            if(timeHeld % 100 == 0)
            {
                Console.CursorLeft = 0;
                Console.Write($"{timeHeld}/{totalRaceTime} {(timeHeld / (double)totalRaceTime * 100): 0.0}%");
            }
        } 
        Console.WriteLine($"Part 2 Answer #{waysToWin} ways to win");
    }
}
Part1();
Part2();