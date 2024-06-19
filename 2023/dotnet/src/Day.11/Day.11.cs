using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day11
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_11/input.txt";
        private static long DISTANCE_FACTOR = 1_000_000;

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 11");
            Main_Day11_Part2(args);
        }

        static void Main_Day11_Part2(string[] args)
        {
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is null) { throw new Exception("WHY IS THE FILE EMPTY"); }
            char[,] grid = new char[rawLine.Length, rawLine.Length];
            // row 0
            GalaxyUtilities.transcribeStringToGridRow(rawLine, grid, 0);
            long row = 1;
            while ((rawLine = reader.ReadLine()) != null)
            {
                GalaxyUtilities.transcribeStringToGridRow(rawLine, grid, row);
                row += 1;
            }
            List<Galaxy> galaxies = GalaxyUtilities.getGalaxiesFromGrid(grid);
            long sumOfDistances = 0;
            foreach (Galaxy galaxy in galaxies)
            {
                foreach (Galaxy neighbor in galaxies)
                {
                    if (galaxy == neighbor) { continue; }
                    if (galaxy.neighbors.Contains(neighbor)) { continue; }
                    galaxy.neighbors.Add(neighbor);
                    neighbor.neighbors.Add(galaxy);

                    long distanceRows = Math.Abs(galaxy.row - neighbor.row);
                    long startRow = galaxy.row;
                    long endRow = neighbor.row;
                    if (startRow > endRow)
                    {
                        startRow = neighbor.row;
                        endRow = galaxy.row;
                    }
                    for (long r = startRow; r <= endRow; r += 1)
                    {
                        if (!GalaxyUtilities.hasGalaxyInRow(grid, r))
                        {
                            distanceRows = distanceRows - 1 + DISTANCE_FACTOR;
                        }
                    }

                    long distanceCols = Math.Abs(galaxy.col - neighbor.col);
                    long startCol = galaxy.col;
                    long endCol = neighbor.col;
                    if (startCol > endCol)
                    {
                        startCol = neighbor.col;
                        endCol = galaxy.col;
                    }
                    for (long c = startCol; c <= endCol; c += 1)
                    {
                        if (!GalaxyUtilities.hasGalaxyInCol(grid, c))
                        {
                            distanceCols = distanceCols - 1 + DISTANCE_FACTOR;
                        }
                    }

                    long distance = distanceRows + distanceCols;
                    sumOfDistances += distance;
                }
            }
            Console.WriteLine($"sumOfDistances:{sumOfDistances}");

        }

        static void Main_Day11(string[] args)
        {
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is null) { throw new Exception("WHY IS THE FILE EMPTY"); }
            char[,] grid = new char[rawLine.Length, rawLine.Length];
            // row 0
            GalaxyUtilities.transcribeStringToGridRow(rawLine, grid, 0);
            long row = 1;
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
            List<Galaxy> galaxies = GalaxyUtilities.getGalaxiesFromGrid(grid);
            long sumOfDistances = 0;
            foreach (Galaxy galaxy in galaxies)
            {
                foreach (Galaxy neighbor in galaxies)
                {
                    if (galaxy == neighbor) { continue; }
                    if (galaxy.neighbors.Contains(neighbor)) { continue; }
                    galaxy.neighbors.Add(neighbor);
                    neighbor.neighbors.Add(galaxy);
                    long distanceRows = Math.Abs(galaxy.row - neighbor.row);
                    long distanceCols = Math.Abs(galaxy.col - neighbor.col);
                    long distance = distanceRows + distanceCols;
                    sumOfDistances += distance;
                }
            }
            Console.WriteLine($"sumOfDistances:{sumOfDistances}");

        }

    } // program

    public record class Galaxy
    {
        public required long row { get; set; }
        public required long col { get; set; }
        public List<Galaxy> neighbors = new List<Galaxy>();
    }

    public class GalaxyUtilities
    {
        static public char[,] expandRowsWithoutGalaxies(char[,] grid)
        {
            long startRow = 0;
            while (true)
            {
                long gridRows = grid.GetLength(0);
                for (long row = startRow; row < gridRows; row += 1)
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
            long startCol = 0;
            while (true)
            {
                long gridCols = grid.GetLength(1);
                for (long col = startCol; col < gridCols; col += 1)
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

        static public char[,] expandGridAtRow(char[,] grid, long row)
        {
            // Console.WriteLine($"expandGridAtRow row:{row}");
            long gridRows = grid.GetLength(0);
            long gridCols = grid.GetLength(1);
            char[,] newGrid = new char[gridRows + 1, gridCols];
            for (long r = 0; r <= row; r++)
            {
                for (long c = 0; c < gridCols; c++)
                {
                    newGrid[r, c] = grid[r, c];
                }
            }
            for (long c = 0; c < gridCols; c++)
            {
                newGrid[row + 1, c] = '.';
            }
            for (long r = row + 2; r < gridRows + 1; r++)
            {
                for (long c = 0; c < gridCols; c++)
                {
                    newGrid[r, c] = grid[r - 1, c];
                }
            }
            // displayGrid(newGrid);
            return newGrid;
        }

        static public char[,] expandGridAtCol(char[,] grid, long col)
        {
            long gridRows = grid.GetLength(0);
            long gridCols = grid.GetLength(1);
            char[,] newGrid = new char[gridRows, gridCols + 1];
            for (long c = 0; c <= col; c += 1)
            {
                for (long r = 0; r < gridRows; r += 1)
                {
                    newGrid[r, c] = grid[r, c];
                }
            }
            for (long r = 0; r < gridRows; r += 1)
            {
                newGrid[r, col + 1] = '.';
            }
            for (long c = col + 2; c < gridCols + 1; c += 1)
            {
                for (long r = 0; r < gridRows; r += 1)
                {
                    newGrid[r, c] = grid[r, c - 1];
                }
            }
            return newGrid;
        }

        static public void displayGrid(char[,] grid)
        {
            for (long row = 0; row < grid.GetLength(0); row += 1)
            {
                for (long col = 0; col < grid.GetLength(1); col += 1)
                {
                    Console.Write($"{grid[row, col]}");
                }
                Console.WriteLine("");

            }
        }

        static public bool hasGalaxyInRow(char[,] grid, long row)
        {
            long gridCols = grid.GetLength(1);
            for (long col = 0; col < gridCols; col += 1)
            {
                if (grid[row, col] == '#') { return true; }
            }
            return false;
        }

        static public bool hasGalaxyInCol(char[,] grid, long col)
        {
            long gridRows = grid.GetLength(0);
            for (long row = 0; row < gridRows; row += 1)
            {
                if (grid[row, col] == '#') { return true; }
            }
            return false;
        }

        static public void transcribeStringToGridRow(string? line, char[,] grid, long row)
        {
            if (line is null) { throw new Exception("WHY IS THE STRING EMPTY"); }
            for (long i = 0; i < line.Length; i += 1)
            {
                grid[row, i] = line.ToCharArray()[i];
            }
        }

        static public List<Galaxy> getGalaxiesFromGrid(char[,] grid)
        {
            List<Galaxy> galaxies = new List<Galaxy>();
            long gridRows = grid.GetLength(0);
            long gridCols = grid.GetLength(1);
            for (long r = 0; r < gridRows; r += 1)
            {
                for (long c = 0; c < gridCols; c += 1)
                {
                    if (grid[r, c] == '#')
                    {
                        Galaxy g = new Galaxy { row = r, col = c };
                        Console.WriteLine($"g:{g}");
                        galaxies.Add(g);
                    }
                }
            }
            return galaxies;
        }

    }

} // namespace