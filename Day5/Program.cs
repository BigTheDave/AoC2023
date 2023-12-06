using System.Diagnostics.CodeAnalysis;

string file = "input.txt";
void Part1()
{
    using (TextReader tr = new StreamReader(File.OpenRead(file)))
    {
        string? line;
        long[] _seeds = tr.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(n => long.Parse(n)).ToArray();

        Dictionary<string, List<long>> maps = new Dictionary<string, List<long>>();
        maps.Add("seed", _seeds.ToList());
        while ((line = tr.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var bits = line.Split(" ")[0].Split("-");
            var fromMap = bits[0];
            var toMap = bits[2];
            var source = maps[fromMap];
            string? mapLine;

            List<long> destination = new List<long>(source);
            while (!string.IsNullOrWhiteSpace(mapLine = tr.ReadLine()))
            {
                var map = mapLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => long.Parse(n)).ToArray();
                var destinationStart = map[0];
                var sourceStart = map[1];
                var range = map[2];
                for (int i = 0; i < source.Count(); i++)
                {
                    if (source[i] >= sourceStart && source[i] < sourceStart + range)
                    { 
                        destination[i] = destinationStart + source[i] - sourceStart;
                    }
                }

            }
            maps.Add(toMap, destination);
        }

        Console.WriteLine("----------------");
        Console.WriteLine($"Part 1 Answer: {maps["location"].Min()}");
        Console.WriteLine("----------------");
        Console.WriteLine();
    }
}

void Part2()
{
    Dictionary<string, List<RangeMap>> maps = new Dictionary<string, List<RangeMap>>();
    using (TextReader tr = new StreamReader(File.OpenRead(file)))
    {
        string? line;
        long[] _seeds = tr.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(n => long.Parse(n)).ToArray();
        Dictionary<long,long> splitSeedsRanges = new Dictionary<long,long>();
        for (int i = 0; i < _seeds.Length; i += 2)
        {
            splitSeedsRanges.Add(_seeds[i], _seeds[i + 1]);
        }
        Console.WriteLine("Seeds added");
        
        while ((line = tr.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var bits = line.Split(" ")[0].Split("-");
            var fromMap = bits[0];
            var toMap = bits[2];
            //var source = maps[fromMap];
            string? mapLine;

            List<RangeMap> destination = new List<RangeMap>();
            while (!string.IsNullOrWhiteSpace(mapLine = tr.ReadLine()))
            {
                var map = mapLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => long.Parse(n)).ToArray();
                var destinationStart = map[0];
                var sourceStart = map[1];
                var range = map[2];
                destination.Add(new RangeMap()
                {
                    Destination = destinationStart,
                    Source = sourceStart,
                    Range = range
                });

            }
            maps.Add(toMap, destination);
        }
        //Calculate Result

        long answer = long.MaxValue;
        foreach(var seedStart in splitSeedsRanges)
        {
            var start = seedStart.Key;
            var range = seedStart.Value;
            long min = GetMinimumResultFromSeedRange(seedStart.Key, seedStart.Key + seedStart.Value - 1);
            answer = Math.Min(answer, min);
            
        }


        Console.WriteLine("----------------");
        Console.WriteLine($"Part 2 Answer: {answer}");
        Console.WriteLine("----------------");
        Console.WriteLine();
    }
    long GetMinimumResultFromSeedRange(long min, long max)
    {
        HashSet<Range> sourceRanges = new HashSet<Range>()
        {
            new Range()
            {
                Min = min,
                Max = max
            }
        };
        Console.WriteLine($"Seeds {min} to {max}");
        foreach (var map in maps)
        {
            Console.WriteLine($"{map.Key}");
            sourceRanges = ParseRange(sourceRanges, map.Value);
        }
        return sourceRanges.Select(r=>r.Min).Min();
    }
    HashSet<Range> ParseRange(HashSet<Range> source, List<RangeMap> map )
    {
        Console.WriteLine($" src:{AggregateRange(source)}");
        foreach (var rangeMap in map)
        {
            var translatedSource = new HashSet<Range>();
            foreach (var rkv in source.Select(k=>k).ToList()) {
                var splitSource = SplitRangeForMap(rkv, rangeMap);
                foreach (var range in splitSource)
                {
                    translatedSource.Add(range);
                }
            }
            source = translatedSource;
        }

        var destination = new HashSet<Range>(source);
        foreach(var _result in source)
        {
            var range = _result;
            foreach(var rangeMap in map)
            {
                if(range.Min >= rangeMap.Source && range.Max <= rangeMap.SourceMax)
                {
                    destination.Remove(range);
                    var delta = rangeMap.Destination - rangeMap.Source;
                    range = new Range()
                    {
                        Min = range.Min + delta,
                        Max = range.Max + delta
                    };
                    destination.Add(range);
                    break;
                }
            }
        }
        Console.WriteLine($" src:{AggregateRange(source)}");
        Console.WriteLine($"dest:{AggregateRange(destination)}");
        return destination;

    }
    HashSet<Range> SplitRangeForMap(Range range, RangeMap rangeMap)
    { 
        HashSet<Range> result = new HashSet<Range>();

        //Wholly within range
        if(range.Min >= rangeMap.Source && range.Max <= rangeMap.SourceMax)
        {
            result.Add(range);            
            return result;
        }

        //Wholly outwith range
        if(range.Max < rangeMap.Source || range.Min > rangeMap.SourceMax)
        {
            result.Add(range);
            return result;
        }
        if(range.Min < rangeMap.Source && range.Max > rangeMap.SourceMax)
        {
            result.Add(new Range()
            {
                Min = range.Min,
                Max = rangeMap.Source - 1
            });
            result.Add(new Range()
            {
                Min = rangeMap.Source,
                Max = rangeMap.SourceMax
            });
            result.Add(new Range()
            {
                Min = rangeMap.SourceMax + 1,
                Max = range.Max
            });

            EnsureContinuity(result, range);
            return result;
        }

        if(range.Min < rangeMap.Source)
        {
            result.Add(new Range()
            {
                Min = range.Min,
                Max = rangeMap.Source - 1
            });
        }

        if(range.Min >= rangeMap.Source)
        {
            result.Add(new Range()
            {
                Min = range.Min,
                Max = rangeMap.SourceMax
            });
        }

        if (range.Max > rangeMap.SourceMax)
        {
            result.Add(new Range()
            {
                Min = rangeMap.SourceMax + 1,
                Max = range.Max
            });
        }

        if (range.Max <= rangeMap.SourceMax)
        {
            result.Add(new Range()
            {
                Min = rangeMap.Source,
                Max = range.Max
            });
        }

        EnsureContinuity(result, range);

        return result;
    }
}
string AggregateRange(IEnumerable<Range> ranges)=> ranges.Select(t => $"{t}").Aggregate((a, b) => $"{a} {b}");

void EnsureContinuity(HashSet<Range> result, Range range)
{
    var sanityCheck = new Range()
    {
        Min = result.Min(r => r.Min),
        Max = result.Max(r => r.Max)
    };
    if (!sanityCheck.Equals(range)) throw new Exception($"{range} != {sanityCheck}");
    Range? lastRange = null;
    foreach (var testRange in result.OrderBy(r => r.Min))
    {
        if (lastRange.HasValue)
        {
            if (testRange.Min != lastRange.Value.Max + 1) throw new Exception("RANGE MISSING");
        }
        lastRange = testRange;
    }
}


Part1();
Part2();
public struct Range
{
    public long Min { get; set; }
    public long Max { get; set; }
    public override string ToString()
    {
        return $"[{Min} > {Max}]";
    }
    
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if(obj is Range other)
        {
            if (other.Min != Min) return false;
            if (other.Max != Max) return false;
            return true;
        }
        return false;
    }
}
public struct RangeMap
{
    public long Source { get; set; }
    public long SourceMax => Source + Range - 1;
    public long Destination { get; set; }
    public long DestinationMax => Destination + Range - 1;
    public long Range { get; set; }
}