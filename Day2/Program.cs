 
Dictionary<string,int> targets
    = new () {
    {"red",12},
    {"green",13},
    {"blue",14}
    };
    int answer = 0;
using(var fs = File.OpenRead("input.txt")) {
    using(var tr = new StreamReader(fs)) {
        string line = null;
        int n = 0;
        while(( line = tr.ReadLine()) != null) {
            n++;
            Dictionary<string,int> maximums
             = new () {
                {"red",0},
                {"green",0},
                {"blue",0}
             };
            var games = line.Split(":")[1].Split(";");
            foreach(var game in games) {
                var dice = game.Split(",");
                foreach(var die in dice) {
                    var details = die.Trim().Split(" ");
                    var colour = details[1];
                    int value = int.Parse(details[0]);
                    maximums[colour] = Math.Max(value, maximums[colour]);
                }
            }
            int power = maximums["red"] * maximums["green"] * maximums["blue"];
            answer += power;
            if(maximums["red"] <= targets["red"] && maximums["green"] <= targets["green"] && maximums["blue"] <= targets["blue"]) {
                Console.Write($"{n},");
                //answer += n;
            }
        }

    }
}

Console.WriteLine($"\n ANSWER: {answer}");