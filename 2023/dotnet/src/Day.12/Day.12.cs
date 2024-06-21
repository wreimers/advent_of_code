using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day12
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_12/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 12");
            Main_Day12(args);
        }

        static void Main_Day12(string[] args)
        {
            List<ConditionRecord> records = new List<ConditionRecord>();
            using StreamReader reader = new(DATA_FILE);
            string? rawLine;
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine(rawLine);
                char[] splitters = [' ',];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                var record = new ConditionRecord { conditions = tokens[0], counts = tokens[1] };
                records.Add(record);
            }
            foreach (ConditionRecord record in records)
            {
                Console.WriteLine($"conditions:{record.conditions} countsList:[{string.Join(",", record.countsList)}]");
                Console.WriteLine($"permutations:");
                foreach (List<int> permutation in record.permutations)
                {
                    Console.Write($"[{string.Join(",", permutation)}]");
                }
                Console.WriteLine();
            }

        }

        public class ConditionRecord
        {
            public required string conditions { get; set; }
            public required string counts { get; set; }

            public List<int> countsList
            {
                get
                {
                    var countsResult = new List<int>();
                    char[] splitters = [' ', ','];
                    string[] tokens = counts.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string token in tokens)
                    {
                        // convert string to int and add them to countsResult
                        int result = Int32.Parse(token);
                        countsResult.Add(result);
                    }
                    return countsResult;
                }
            }

            public List<List<int>> permutations
            {
                get
                {
                    var results = new List<List<int>>();
                    var rawPermutations = GetPermutations<int>(countsList, countsList.Count);
                    foreach (IEnumerable<int> permutation in rawPermutations)
                    {
                        results.Add(permutation.ToList());
                        // Console.WriteLine($"permutation:{permutations.ToList()}");
                    }
                    Console.WriteLine($"permutations result: {results}");
                    return results;
                }
            }

            // shamelessly stolen from:
            // https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array/10629938#10629938
            private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
            {
                if (length == 1) return list.Select(t => new T[] { t });
                return GetPermutations(list, length - 1)
                    .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
            }
        }

        static public void transcribeStringToGridRow(string? line, char[,] grid, long row)
        {
            if (line is null) { throw new Exception("WHY IS THE STRING EMPTY"); }
            for (long i = 0; i < line.Length; i += 1)
            {
                grid[row, i] = line.ToCharArray()[i];
            }
        }

    }
}