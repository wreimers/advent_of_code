using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day10
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
            using StreamReader reader = new("var/day_10/input.txt");
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

            var animalLoc = new GridLoc { row = animalRow, col = animalCol, grid = grid, from = FromDirection.Nowhere };
            GridLoc pathOne = null;
            GridLoc pathTwo = null;
            char[] validAbove = { '|', '7', 'F', };
            char[] validBelow = ['|', 'J', 'L',];
            char[] validLeft = ['-', 'L', 'F',];
            char[] validRight = ['-', 'J', '7',];
            if (validAbove.Contains(animalLoc.charAboveIn(grid)))
            {
                pathOne = new GridLoc { row = animalLoc.row - 1, col = animalLoc.col, grid = grid, from = FromDirection.Below };
            }
            if (validBelow.Contains(animalLoc.charBelowIn(grid)))
            {
                if (pathOne is null)
                {
                    pathOne = new GridLoc { row = animalLoc.row + 1, col = animalLoc.col, grid = grid, from = FromDirection.Above };
                }
                else
                {
                    pathTwo = new GridLoc { row = animalLoc.row + 1, col = animalLoc.col, grid = grid, from = FromDirection.Above };
                }
            }
            if (validLeft.Contains(animalLoc.charLeftIn(grid)))
            {
                if (pathOne is null)
                {
                    pathOne = new GridLoc { row = animalLoc.row, col = animalLoc.col - 1, grid = grid, from = FromDirection.Right };
                }
                else
                {
                    pathTwo = new GridLoc { row = animalLoc.row, col = animalLoc.col - 1, grid = grid, from = FromDirection.Right };
                }
            }
            if (validRight.Contains(animalLoc.charRightIn(grid)))
            {
                if (pathOne is null)
                {
                    pathOne = new GridLoc { row = animalLoc.row, col = animalLoc.col + 1, grid = grid, from = FromDirection.Left };
                }
                else
                {
                    pathTwo = new GridLoc { row = animalLoc.row, col = animalLoc.col + 1, grid = grid, from = FromDirection.Left };
                }
            }

            if (pathOne is null || pathTwo is null) { throw new Exception("MUST HAVE 2 PATHS"); }

            Console.WriteLine($"pathOne:{pathOne}");
            Console.WriteLine($"pathTwo:{pathTwo}");

            GridLoc locOne = pathOne;
            GridLoc locTwo = pathTwo;

            int counter = 0;
            while (true)
            {
                counter += 1;
                GridLoc nextOne = locOne.traverse();
                GridLoc nextTwo = locTwo.traverse();
                Console.WriteLine($"nextOne:{nextOne}");
                Console.WriteLine($"nextTwo:{nextTwo}");
                if (nextOne.Equals(nextTwo))
                {
                    counter += 1;
                    break;
                }
                locOne = nextOne;
                locTwo = nextTwo;
            }
            Console.WriteLine($"counter:{counter}");
        }
    }

}

public enum FromDirection
{
    Above,
    Below,
    Left,
    Right,
    Nowhere,
}

public class GridLoc : IEquatable<GridLoc>
{
    public required char[][] grid;
    public required FromDirection from;
    public required int row;
    public required int col;

    public char character
    {
        get
        {
            return grid[row][col];
        }
        set { }
    }

    public char charAboveIn(char[][] grid)
    {
        // if (row == 0) { return null; }
        char result = grid[row - 1][col];
        Console.WriteLine($"charAboveIn row:{row} col:{col} result:{result}");
        return result;
    }

    public char charBelowIn(char[][] grid)
    {
        // if (row == grid.Rank - 1) { return null; }
        char result = grid[row + 1][col];
        Console.WriteLine($"charBelowIn row:{row} col:{col} result:{result}");
        return result;
    }

    public char charLeftIn(char[][] grid)
    {
        // if (col == 0) { return null; }
        char result = grid[row][col - 1];
        Console.WriteLine($"charLeftIn row:{row} col:{col} result:{result}");
        return result;
    }

    public char charRightIn(char[][] grid)
    {
        // if (col == grid.GetLength(col) - 1) { return null; }
        char result = grid[row][col + 1];
        Console.WriteLine($"charRightIn row:{row} col:{col} result:{result}");
        return result;
    }

    public GridLoc traverse()
    {
        if (from == FromDirection.Below)
        {
            if (character == '|')
            {
                //go up
                return new GridLoc { row = row - 1, col = col, grid = grid, from = FromDirection.Below };
            }
            else if (character == '7')
            {
                //go left
                return new GridLoc { row = row, col = col - 1, grid = grid, from = FromDirection.Right };
            }
            else if (character == 'F')
            {
                //go right
                return new GridLoc { row = row, col = col + 1, grid = grid, from = FromDirection.Left };
            }
        }
        else if (from == FromDirection.Above)
        {
            if (character == '|')
            {
                // go down
                return new GridLoc { row = row + 1, col = col, grid = grid, from = FromDirection.Above };
            }
            else if (character == 'J')
            {
                //go left
                return new GridLoc { row = row, col = col - 1, grid = grid, from = FromDirection.Right };
            }
            else if (character == 'L')
            {
                //go right
                return new GridLoc { row = row, col = col + 1, grid = grid, from = FromDirection.Left };
            }
        }
        else if (from == FromDirection.Right)
        {
            if (character == '-')
            {
                // go left
                return new GridLoc { row = row, col = col - 1, grid = grid, from = FromDirection.Right };
            }
            else if (character == 'L')
            {
                //go up
                return new GridLoc { row = row - 1, col = col, grid = grid, from = FromDirection.Below };
            }
            else if (character == 'F')
            {
                //go down
                return new GridLoc { row = row + 1, col = col, grid = grid, from = FromDirection.Above };
            }
        }
        else if (from == FromDirection.Left)
        {
            if (character == '-')
            {
                // go right
                return new GridLoc { row = row, col = col + 1, grid = grid, from = FromDirection.Left };
            }
            else if (character == 'J')
            {
                //go up
                return new GridLoc { row = row - 1, col = col, grid = grid, from = FromDirection.Below };
            }
            else if (character == '7')
            {
                //go down
                return new GridLoc { row = row + 1, col = col, grid = grid, from = FromDirection.Above };
            }
        }
        throw new Exception("ILLEGAL DIRECTION GLYPH");
    }

    public override string? ToString()
    {
        return $"<character:{character} row:{row} col:{col} from:{from}>";
    }

    public bool Equals(GridLoc? other)
    {
        if (other is null) { return false; }
        return row == other.row && col == other.col;
    }
}