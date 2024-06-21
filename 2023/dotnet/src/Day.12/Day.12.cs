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

        }

        public class ConditionRecord
        {
            public required string conditions { get; set; }
            public required string counts { get; set; }

            private List<int> countsList
            {
                var countsResult = new List<int>();
                char[] splitters = [' ', ','];
            string[] tokens = counts.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens) {
                    // convert string to int and add them to countsResult
                }
                return countsResult;
            };
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