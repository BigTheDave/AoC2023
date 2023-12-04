char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
List<string> lines = new List<string>();

using (TextReader tr = new StreamReader(File.OpenRead("input.txt")))
{
    string? line;
    while ((line = tr.ReadLine()) != null) {
        lines.Add(line);
    }
}

int lineSize = lines.First().Length;

int answer = 0;
int gearRatioAnswer = 0;

Dictionary<P, int> pendingGearRatios = new();
for(int y = 0; y < lines.Count; y++)
{
    int x = 0;
    while(ReadNumber(y,x,out int _x, out int _length, out int value ))
    {
        x = _x + _length; 
        if (IsPositionPartNumber(_x,y,_length, out char code, out var cx, out var cy))
        {
            if(code == '*')
            {
                P pos = new P(cx, cy);
                if(pendingGearRatios.ContainsKey(pos))
                {
                    Console.WriteLine($"Found gear ratio: {value} x {pendingGearRatios[pos]}");
                    gearRatioAnswer += pendingGearRatios[pos] * value;
                } else
                {
                    Console.WriteLine($"Found One half of a gear ratio: {value} {pos}");
                    pendingGearRatios.Add(pos, value);
                }
            }
            answer += value;
        }
    }
}

Console.WriteLine($"\n{answer}");
Console.WriteLine($"gearRatioAnswer = {gearRatioAnswer}");
bool IsCharacterPartNumber(int x, int y,out char code)
{
    code = lines[y][x];
    return lines[y][x] != '.' && !numbers.Any(n => n == lines[y][x]);
}
bool IsCharacterAndNearbyPartNumber(int x, int y, out char code, out int cx, out int cy)
{
    cx = x; cy = y;
    if (IsCharacterPartNumber(x, y, out code)) return true;
    cy = y - 1;
    if (y - 1 >= 0 && IsCharacterPartNumber(x, y - 1, out code)) return true;
    cy = y + 1;
    if (y + 1 < lines.Count() && IsCharacterPartNumber(x, y + 1, out code)) return true;
    code = '.';
    return false;
}
bool IsPositionPartNumber(int x,int y, int length, out char code, out int cx, out int cy)
{
    for(int _x = Math.Max(0, x-1); _x < Math.Min(x + length + 1, lineSize); _x++)
    {
        if (IsCharacterAndNearbyPartNumber(_x,y, out code, out cx, out cy))
        {
            return true;
        }       
    }
    cx = -1;
    cy = -1;
    code = '.';
    return false;
}

bool ReadNumber(int line_index, int start_x, out int x, out int length, out int value)
{
    var line = lines[line_index];

    for(int _x = start_x; _x < line.Length; _x++)
    {
        while(numbers.Any(n=> n == line[_x]))
        {
            int _length = 1;
            while (_x + _length < line.Length && numbers.Any(n => n == line[_x + _length]))
            {
                _length++;
            }
            x = _x;
            length = _length;
            var v = line.Substring(_x, _length);
            value = int.Parse(v);
            return true;
        }
    }

    x = -1;
    length = -1;
    value = 0;
    return false;
}

struct P
{
    public P(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X, Y;
};