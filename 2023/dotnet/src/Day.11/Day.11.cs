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
            char[,] grid = new char[rawLine.Length, rawLine.Length];
            // row 0
            transcribe(rawLine, grid, 0);
            int row = 1;
            while ((rawLine = reader.ReadLine()) != null)
            {
                transcribe(rawLine, grid, row);
                row += 1;
            }
            displayGrid(grid);
            // expand rows
            int startRow = 0;
            while (true)
            {
                for (row = startRow; row < grid.GetLength(0); row += 1)
                {
                    if (hasGalaxyInRow(grid, row)) { continue; }
                    grid = GalaxyUtilities.expandGridAtRow(grid, row);
                    startRow = row + 2;
                    break;

                }
                if (row == grid.GetLength(0) - 1) { break; }

            }
            displayGrid(grid);
        }

        static public string stringRow(char[,] grid, int row)
        {
            string returnValue = "";
            for (int col = 0; col < grid.GetLength(1); col += 1)
            {
                returnValue = $"{returnValue}{grid[row, col]}";
            }
            return returnValue;
        }

        static public void displayGrid(char[,] grid)
        {
            // Console.WriteLine($"displayGrid grid.Length:{grid.Length} grid.GetLength(1):{grid.GetLength(1)}");
            for (int row = 0; row < grid.GetLength(0); row += 1)
            {
                for (int col = 0; col < grid.GetLength(1); col += 1)
                {
                    Console.Write($"{grid[row, col]}");
                }
                Console.WriteLine("");

            }
        }

        static public void transcribe(string? line, char[,] grid, int row)
        {
            if (line is null) { throw new Exception("WHY IS THE STRING EMPTY"); }
            for (int i = 0; i < line.Length; i += 1)
            {
                grid[row, i] = line.ToCharArray()[i];
            }
        }

        static public bool hasGalaxyInRow(char[,] grid, int row)
        {
            for (int col = 0; col < grid.GetLength(1); col += 1)
            {
                if (grid[row, col] == '#')
                {
                    Console.WriteLine($"hasGalaxy row:{row} {stringRow(grid, row)} TRUE");
                    return true;
                }
            }
            Console.WriteLine($"hasGalaxy row:{row} {stringRow(grid, row)} FALSE");
            return false;
        }


    } // program


    public class GalaxyUtilities
    {
        static public char[,] expandRowsWithoutGalaxies(char[,] grid)
        {
            int startRow = 0;
            while (true)
            {
                int gridRows = grid.GetLength(0);
                int gridCols = grid.GetLength(1);
                int row = 0;
                for (row = startRow; row < gridRows; row += 1)
                {
                    if (hasGalaxyInRow(grid, row)) { continue; }
                    grid = expandGridAtRow(grid, row);
                    startRow = row + 2;
                    goto OuterLoop;
                }
                break;
            OuterLoop:;
            }
            return grid;
        }

        static public char[,] expandGridAtRow(char[,] grid, int row)
        {
            // Console.WriteLine($"expandGridAtRow row:{row}");
            int gridRows = grid.GetLength(0);
            int gridCols = grid.GetLength(1);
            char[,] newGrid = new char[gridRows + 1, gridCols];
            for (int r = 0; r <= row; r++)
            {
                for (int c = 0; c < gridCols; c++)
                {
                    newGrid[r, c] = grid[r, c];
                }
            }
            for (int c = 0; c < gridCols; c++)
            {
                newGrid[row + 1, c] = '.';
            }
            for (int r = row + 2; r < gridRows + 1; r++)
            {
                for (int c = 0; c < gridCols; c++)
                {
                    newGrid[r, c] = grid[r - 1, c];
                }
            }
            // displayGrid(newGrid);
            return newGrid;
        }

        static private void displayGrid(char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row += 1)
            {
                for (int col = 0; col < grid.GetLength(1); col += 1)
                {
                    Console.Write($"{grid[row, col]}");
                }
                Console.WriteLine("");

            }
        }

        static public bool hasGalaxyInRow(char[,] grid, int row)
        {
            for (int col = 0; col < grid.GetLength(1); col += 1)
            {
                if (grid[row, col] == '#')
                {
                    return true;
                }
            }
            return false;
        }
        static private string stringRow(char[,] grid, int row)
        {
            string returnValue = "";
            for (int col = 0; col < grid.GetLength(1); col += 1)
            {
                returnValue = $"{returnValue}{grid[row, col]}";
            }
            return returnValue;
        }

    }

} // namespace