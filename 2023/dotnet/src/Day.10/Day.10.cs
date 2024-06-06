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
            char[][] grid = new char[n][];
            while ((rawLine = reader.ReadLine()) != null)
            {
                if (row == 0)
                {
                    n = rawLine.Length;
                    grid = new char[n][];
                }
                grid[row] = new char[n];
                int col = 0;
                foreach (char c in rawLine.ToCharArray())
                {
                    grid[row][col] = c;
                    if (c == 'S')
                    {
                        animalRow = row;
                        animalCol = col;
                    }
                    col += 1;
                }
                row += 1;
                Console.WriteLine($"{rawLine}");

            }
            Console.WriteLine($"animalRow:{animalRow} animalCol:{animalCol}");

            var animalLoc = new GridLoc { row = animalRow, col = animalCol };
            GridLoc pathOne = null;
            GridLoc pathTwo = null;
            char[] validAbove = { '|', '7', 'F', };
            char[] validBelow = ['|', 'J', 'L',];
            char[] validLeft = ['-', 'L', 'F',];
            char[] validRight = ['-', 'J', '7',];
            if (validAbove.Contains(animalLoc.charAboveIn(grid)))
            {
                pathOne = new GridLoc { row = animalLoc.row - 1, col = animalLoc.col };
            }
            if (validBelow.Contains(animalLoc.charBelowIn(grid)))
            {
                if (pathOne is null)
                {
                    pathOne = new GridLoc { row = animalLoc.row + 1, col = animalLoc.col };
                }
                else
                {
                    pathTwo = new GridLoc { row = animalLoc.row + 1, col = animalLoc.col };
                }
            }
            if (validLeft.Contains(animalLoc.charLeftIn(grid)))
            {
                if (pathOne is null)
                {
                    pathOne = new GridLoc { row = animalLoc.row, col = animalLoc.col - 1 };
                }
                else
                {
                    pathTwo = new GridLoc { row = animalLoc.row, col = animalLoc.col - 1 };
                }
            }
            if (validRight.Contains(animalLoc.charRightIn(grid)))
            {
                if (pathOne is null)
                {
                    pathOne = new GridLoc { row = animalLoc.row, col = animalLoc.col + 1 };
                }
                else
                {
                    pathTwo = new GridLoc { row = animalLoc.row, col = animalLoc.col + 1 };
                }
            }

            while (true)
            {
                // need to track source of path so we don't try to step backwards. left and right
                // branches perhaps?
            }

        }
    }

}

class GridLoc
{
    public required int row;
    public required int col;

    public char charAboveIn(char[][] grid)
    {
        // if (row == 0) { return null; }
        return grid[row - 1][col];
    }

    public char charBelowIn(char[][] grid)
    {
        // if (row == grid.Rank - 1) { return null; }
        return grid[row + 1][col];
    }

    public char charLeftIn(char[][] grid)
    {
        // if (col == 0) { return null; }
        return grid[row][col - 1];
    }

    public char charRightIn(char[][] grid)
    {
        // if (col == grid.GetLength(col) - 1) { return null; }
        return grid[row][col + 1];
    }
}