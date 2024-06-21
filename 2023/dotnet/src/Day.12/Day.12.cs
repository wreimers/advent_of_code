using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day12
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_11/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 12");
            Main_Day12(args);
        }

        static void Main_Day12(string[] args)
        {
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is null) { throw new Exception("WHY IS THE FILE EMPTY"); }
            char[,] grid = new char[rawLine.Length, rawLine.Length];
            // row 0
            transcribeStringToGridRow(rawLine, grid, 0);
            long row = 1;
            while ((rawLine = reader.ReadLine()) != null)
            {
                transcribeStringToGridRow(rawLine, grid, row);
                row += 1;
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