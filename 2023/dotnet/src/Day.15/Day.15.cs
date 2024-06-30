using System.Text.RegularExpressions;

namespace Day15
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_15/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 15");
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is not null)
            {
                Console.WriteLine($"rawLine:{rawLine}");
                char[] splitters = [',',];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                // int grandTotal = 0;
                string pattern = @"^([a-z]+)([\=\-]+)(\d*)$";
                Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
                foreach (string t in tokens)
                {
                    Match m = rg.Match(t);
                    if (!m.Success)
                    {
                        throw new Exception("REGEX DIDN'T MATCH");
                    }
                    var label = m.Groups[1].ToString();
                    var operation = m.Groups[2].ToString();
                    var focalLength = m.Groups[3].ToString();
                    Console.WriteLine($"label:{label} operation:{operation} focalLength:{focalLength}");
                    // int value = calculateHASH(t);
                    // grandTotal += value;


                }
                // Console.WriteLine($"grandTotal:{grandTotal}");
            }

        }

        /* part 1
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 15");
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is not null)
            {
                Console.WriteLine($"rawLine:{rawLine}");
                char[] splitters = [',',];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                int grandTotal = 0;
                foreach (string t in tokens)
                {
                    int value = calculateHASH(t);
                    grandTotal += value;
                }
                Console.WriteLine($"grandTotal:{grandTotal}");
            }

        }
        */

        public static int calculateHASH(string word)
        {
            int value = 0;
            for (int i = 0; i < word.Length; i += 1)
            {
                char currentChar = word[i];
                int charValue = currentChar;
                value += charValue;
                value *= 17;
                value %= 256;
            }
            Console.WriteLine($"calculateHASH word:{word} value:{value}");
            return value;
        }
    }
}

public class LensBox
{

}
