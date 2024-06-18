using System;
using System.Data;
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
            GalaxyUtilities.transcribeStringToGridRow(rawLine, grid, 0);
            int row = 1;
            while ((rawLine = reader.ReadLine()) != null)
            {
                GalaxyUtilities.transcribeStringToGridRow(rawLine, grid, row);
                row += 1;
            }
            // GalaxyUtilities.displayGrid(grid);
            grid = GalaxyUtilities.expandRowsWithoutGalaxies(grid);
            // GalaxyUtilities.displayGrid(grid);
            grid = GalaxyUtilities.expandColsWithoutGalaxies(grid);
            // GalaxyUtilities.displayGrid(grid);
            List<Galaxy> galaxies = new List<Galaxy>();
            int gridRows = grid.GetLength(0);
            int gridCols = grid.GetLength(1);
            for (int r = 0; r < gridRows; r += 1)
            {
                for (int c = 0; c < gridCols; c += 1)
                {
                    if (grid[r, c] == '#')
                    {
                        Galaxy g = new Galaxy { row = r, col = c };
                        Console.WriteLine($"g:{g}");
                        galaxies.Add(g);
                    }
                }
            }
            int sumOfDistances = 0;
            foreach (Galaxy galaxy in galaxies)
            {
                foreach (Galaxy neighbor in galaxies)
                {
                    if (galaxy == neighbor) { continue; }
                    if (galaxy.neighbors.Contains(neighbor)) { continue; }
                    galaxy.neighbors.Add(neighbor);
                    neighbor.neighbors.Add(galaxy);
                    int distanceRows = Math.Abs(galaxy.row - neighbor.row);
                    int distanceCols = Math.Abs(galaxy.col - neighbor.col);
                    int distance = distanceRows + distanceCols;
                    sumOfDistances += distance;
                }
            }
            Console.WriteLine($"sumOfDistances:{sumOfDistances}");

        }

    } // program

    public record class Galaxy
    {
        public required int row { get; set; }
        public required int col { get; set; }
        public List<Galaxy> neighbors = new List<Galaxy>();
    }

    public class GalaxyUtilities
    {
        static public char[,] expandRowsWithoutGalaxies(char[,] grid)
        {
            int startRow = 0;
            while (true)
            {
                int gridRows = grid.GetLength(0);
                for (int row = startRow; row < gridRows; row += 1)
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

        static public char[,] expandColsWithoutGalaxies(char[,] grid)
        {
            int startCol = 0;
            while (true)
            {
                int gridCols = grid.GetLength(1);
                for (int col = startCol; col < gridCols; col += 1)
                {
                    if (hasGalaxyInCol(grid, col)) { continue; }
                    grid = expandGridAtCol(grid, col);
                    startCol = col + 2;
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

        static public char[,] expandGridAtCol(char[,] grid, int col)
        {
            int gridRows = grid.GetLength(0);
            int gridCols = grid.GetLength(1);
            char[,] newGrid = new char[gridRows, gridCols + 1];
            for (int c = 0; c <= col; c += 1)
            {
                for (int r = 0; r < gridRows; r += 1)
                {
                    newGrid[r, c] = grid[r, c];
                }
            }
            for (int r = 0; r < gridRows; r += 1)
            {
                newGrid[r, col + 1] = '.';
            }
            for (int c = col + 2; c < gridCols + 1; c += 1)
            {
                for (int r = 0; r < gridRows; r += 1)
                {
                    newGrid[r, c] = grid[r, c - 1];
                }
            }
            return newGrid;
        }

        static public void displayGrid(char[,] grid)
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
            int gridCols = grid.GetLength(1);
            for (int col = 0; col < gridCols; col += 1)
            {
                if (grid[row, col] == '#') { return true; }
            }
            return false;
        }

        static public bool hasGalaxyInCol(char[,] grid, int col)
        {
            int gridRows = grid.GetLength(0);
            for (int row = 0; row < gridRows; row += 1)
            {
                if (grid[row, col] == '#') { return true; }
            }
            return false;
        }

        static public void transcribeStringToGridRow(string? line, char[,] grid, int row)
        {
            if (line is null) { throw new Exception("WHY IS THE STRING EMPTY"); }
            for (int i = 0; i < line.Length; i += 1)
            {
                grid[row, i] = line.ToCharArray()[i];
            }
        }

    }

} // namespace