using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Combinatorics.Collections;

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
                    var permutations = new Permutations<int>(countsList, GenerateOption.WithoutRepetition);
                    // Console.WriteLine($"permutations:{permutations}");
                    foreach (var p in permutations)
                    {
                        // Console.WriteLine($"p:{p}");
                        results.Add(p.ToList());
                    }
                    return results;
                }
            }
        }

    }
}