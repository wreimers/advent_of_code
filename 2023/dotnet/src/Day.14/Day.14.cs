namespace Day14
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_14/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 14");
            Main_Day14(args);
        }

        static void Main_Day14(string[] args)
        {
            var p = new MirrorPuzzle();
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                p.lines.Add(rawLine);
            }
            Console.WriteLine();
            p.display();

            long cycles = 1_000_000_000;
            // long cycles = 10_000_000;
            // long cycles = 3;
            for (long k = 0; k < cycles; k += 1)
            {
                foreach (Mirror m in p.mirrorsNorthToSouth)
                {
                    while (m.north is not null && m.north == '.')
                    {
                        m.goNorth();
                    }
                }
                // Console.WriteLine();
                // p.display();
                foreach (Mirror m in p.mirrorsWestToEast)
                {
                    while (m.west is not null && m.west == '.')
                    {
                        m.goWest();
                    }
                }
                // Console.WriteLine();
                // p.display();
                foreach (Mirror m in p.mirrorsSouthToNorth)
                {
                    while (m.south is not null && m.south == '.')
                    {
                        m.goSouth();
                    }
                }
                // Console.WriteLine();
                // p.display();
                foreach (Mirror m in p.mirrorsEastToWest)
                {
                    while (m.east is not null && m.east == '.')
                    {
                        m.goEast();
                    }
                }
                // Console.WriteLine();
                // p.display();
            }

            int score = 0;
            foreach (Mirror m in p.mirrorsNorthToSouth)
            {
                score += p.rowCount - m.row;
            }
            Console.WriteLine();
            p.display();
            Console.WriteLine();
            Console.WriteLine($"score:{score}");

        }
    }
}

public enum Direction
{
    North,
    East,
    South,
    West,
}

public class Mirror
{
    required public int row { get; set; }
    required public int col { get; set; }
    required public MirrorPuzzle puzzle { get; set; }
    public char? north
    {
        get
        {
            if (row == 0) { return null; }
            return puzzle.grid[row - 1, col];
        }
    }
    public char? south
    {
        get
        {
            if (row == puzzle.rowCount - 1) { return null; }
            return puzzle.grid[row + 1, col];
        }
    }
    public char? west
    {
        get
        {
            if (col == 0) { return null; }
            return puzzle.grid[row, col - 1];
        }
    }
    public char? east
    {
        get
        {
            if (col == puzzle.colCount - 1) { return null; }
            return puzzle.grid[row, col + 1];
        }
    }
    public void goNorth()
    {
        puzzle.grid[row, col] = '.';
        row = row - 1;
        puzzle.grid[row, col] = 'O';
    }
    public void goSouth()
    {
        puzzle.grid[row, col] = '.';
        row = row + 1;
        puzzle.grid[row, col] = 'O';
    }
    public void goWest()
    {
        puzzle.grid[row, col] = '.';
        col = col - 1;
        puzzle.grid[row, col] = 'O';
    }
    public void goEast()
    {
        puzzle.grid[row, col] = '.';
        col = col + 1;
        puzzle.grid[row, col] = 'O';
    }
}

public class MirrorPuzzle : Puzzle
{
    public List<Mirror> mirrorsNorthToSouth
    {
        get
        {
            var mirrors = new List<Mirror>();
            for (int row = 0; row < rowCount; row += 1)
            {
                for (int col = 0; col < colCount; col += 1)
                {
                    if (grid[row, col] == 'O')
                    {
                        mirrors.Add(new Mirror { row = row, col = col, puzzle = this });
                    }
                }
            }
            return mirrors;
        }
    }
    public List<Mirror> mirrorsSouthToNorth
    {
        get
        {
            var mirrors = new List<Mirror>();
            for (int row = rowCount - 1; row >= 0; row -= 1)
            {
                for (int col = 0; col < colCount; col += 1)
                {
                    if (grid[row, col] == 'O')
                    {
                        mirrors.Add(new Mirror { row = row, col = col, puzzle = this });
                    }
                }
            }
            return mirrors;
        }
    }
    public List<Mirror> mirrorsWestToEast
    {
        get
        {
            var mirrors = new List<Mirror>();
            for (int col = 0; col < colCount; col += 1)
            {
                for (int row = 0; row < rowCount; row += 1)
                {
                    if (grid[row, col] == 'O')
                    {
                        mirrors.Add(new Mirror { row = row, col = col, puzzle = this });
                    }
                }
            }
            return mirrors;
        }
    }
    public List<Mirror> mirrorsEastToWest
    {
        get
        {
            var mirrors = new List<Mirror>();
            for (int col = colCount - 1; col >= 0; col -= 1)
            {
                for (int row = 0; row < rowCount; row += 1)
                {
                    if (grid[row, col] == 'O')
                    {
                        mirrors.Add(new Mirror { row = row, col = col, puzzle = this });
                    }
                }
            }
            return mirrors;
        }
    }
}

public class Puzzle
{
    public List<string> lines = new List<string>();
    private char[,]? _grid = null;
    public char[,] grid
    {
        get
        {
            if (_grid is not null)
            {
                return _grid;
            }
            _grid = new char[lines.Count, lines[0].Length];
            for (int row = 0; row < lines.Count; row += 1)
            {
                string rowLine = lines[row];
                for (int col = 0; col < rowLine.Length; col += 1)
                {
                    _grid[row, col] = rowLine.ToCharArray()[col];
                }
            }
            return _grid;
        }
    }
    public void display()
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
    public int rowCount
    {
        get
        {
            return grid.GetLength(0);
        }
    }
    public int colCount
    {
        get
        {
            return grid.GetLength(1);
        }
    }

}