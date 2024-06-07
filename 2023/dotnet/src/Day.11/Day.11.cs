using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day11(args);
        }

        static void Main_Day11(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 11");
            using StreamReader reader = new("var/day_11/sample.txt");
            string? rawLine = reader.ReadLine();
            if (rawLine is null) { throw new Exception("WHY IS THE FILE EMPTY"); }
            char[][] grid = new char[rawLine.Length][];
            // row 0
            grid[0] = new char[rawLine.Length];
            transcribe(rawLine, grid[0]);
            int row = 1;
            while ((rawLine = reader.ReadLine()) != null)
            {
                grid[row] = new char[rawLine.Length];
                transcribe(rawLine, grid[row]);
                row += 1;
                // Console.WriteLine($"{rawLine}");
                Console.WriteLine($"{String.Join("", grid[row-1].ToList())}");
            }
            // expand rows
            for (int row=0; row<grid.Rank; row+=1) {
                
            }
        }

        static public void transcribe(string? line, char[] gridRow) {
            if (line is null) { throw new Exception("WHY IS THE STRING EMPTY"); }
            for (int i=0; i<line.Length; i+=1)
            {
                gridRow[i] = line.ToCharArray()[i];
            }
        }

    } // program
} // namespace

