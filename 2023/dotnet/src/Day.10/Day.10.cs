using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day10(args);
        }

        static void Main_Day10(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 10");
            string? rawLine;
            using StreamReader reader = new("var/day_10/sample.txt");
            int row = 0;
            int n = 0;
            int animalRow = -1;
            int animalCol = -1;
            int[][] grid = new int[n][];
            while ((rawLine = reader.ReadLine()) != null)
            {
                if (row == 0) {
                    n = rawLine.Length;
                    grid = new int[n][];
                }
                grid[row] = new int[n];
                int col = 0;
                foreach (char c in rawLine.ToCharArray())
                {
                    grid[row][col] = c;
                    if (c == 'S') {
                        animalRow = row;
                        animalCol = col;
                    }
                    col += 1;
                }
            }
            
        }
    }

}
