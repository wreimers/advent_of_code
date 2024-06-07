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
            int extraRows = 0;
            char[][] newGrid = new char[grid.Rank][];
            for( row=0; row<grid.Rank; row+=1) {
                char[] gridRow = grid[row];
                if (!hasGalaxy(gridRow)) {
                    newGrid = expandGridAtRow(newGrid, row+extraRows);
                    extraRows+=1;
                }
            }
            foreach(char[] gridRow in newGrid) {
                Console.WriteLine($"{gridRow}");
            }
        }

        static public void transcribe(string? line, char[] gridRow) {
            if (line is null) { throw new Exception("WHY IS THE STRING EMPTY"); }
            for (int i=0; i<line.Length; i+=1)
            {
                gridRow[i] = line.ToCharArray()[i];
            }
        }

        static public bool hasGalaxy(char[] row) {
            foreach (char c in row) {
                if (c=='#') {
                    Console.WriteLine($"hasGalaxy row:{String.Join("", row.ToList())} TRUE");
                    return true;
                }
            }
            Console.WriteLine($"hasGalaxy row:{row} FALSE");
            return false;
        }

        static public char[][] expandGridAtRow(char[][] grid, int row) {
            Console.WriteLine($"expandGridAtRow grid.Rank:{grid.Rank} row:{row}");
            char[][] newGrid = new char[grid.Rank+1][];
            int currentRow=0;
            for(int i=0; i<grid.Rank; i+=1)
            {
                newGrid[currentRow] = grid[i];
                if (i==row) {
                    currentRow += 1;
                    newGrid[currentRow] = new char[grid[row].Length];
                    for(int j=0; j<newGrid[currentRow].Length; j+=1)
                    {
                        newGrid[currentRow][j] = '.';
                    }
                }
            }
            return newGrid;
        }

    } // program
} // namespace

