using System.Text.RegularExpressions;

CreateOutput();
CalculateValue();

void CalculateValue() {
    Regex findFirstAndLast = new Regex(@"^.*?([0-9]{1}).*([0-9]{1}).*$|^.*?([0-9]{1}).*$");

    using(var fs = File.OpenRead("output.txt")) {
        using(TextReader sr = new StreamReader(fs) ) {
            string? line = null;
            int total = 0;
            while((line = sr.ReadLine()) != null) {
                var startIndex = line.IndexOfAny(new char[] {'1','2','3','4','5','6','7','8','9'});
                var lastIndex = line.LastIndexOfAny(new char[] {'1','2','3','4','5','6','7','8','9'});
                //Console.Write($"{startIndex}{lastIndex},");
                Console.Write($"{line[startIndex]}{line[lastIndex]},");
                total += int.Parse($"{line[startIndex]}{line[lastIndex]}");
            }
            Console.WriteLine($"\n\nValue = {total}");
        }
    }
}
void CreateOutput() {
    using(var fs = File.OpenRead("input.txt")) {
        using(TextReader sr = new StreamReader(fs) ) {
            string? line = null; 
            using(var os = File.OpenWrite("output.txt")) {
                using(TextWriter or = new StreamWriter(os) ) {
                    while((line = sr.ReadLine()) != null) {
                        or.WriteLine(ConvertNumberStringsToCharacters(line));  
                    }
                }    
            }
        }
    }
}
string ConvertNumberStringsToCharacters(string line) {
    string[] replacements = {"zero","one","two","three","four","five","six","seven","eight","nine"};
    for(int i = 0; i < replacements.Length; i++) {
        line = line.Replace(replacements[i],$"{replacements[i]}{i}{replacements[i]}");
    }
    return line;
}