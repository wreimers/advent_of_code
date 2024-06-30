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
            var p = new Puzzle();
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                p.lines.Add(rawLine);
            }
            Console.WriteLine();
            p.display();
            var mirrors = new List<Mirror>();
            for (int row = 0; row < p.rowCount; row += 1)
            {
                for (int col = 0; col < p.colCount; col += 1)
                {
                    if (p.grid[row, col] == 'O')
                    {
                        // Console.WriteLine($"mirror row:{row} col:{col}");
                        mirrors.Add(new Mirror { row = row, col = col, grid = p.grid });
                    }
                }
            }
            int score = 0;
            foreach (Mirror m in mirrors)
            {
                while (m.north is not null && m.north == '.')
                {
                    m.goNorth();
                }
                score += p.rowCount - m.row;
            }
            Console.WriteLine();
            p.display();
            Console.WriteLine();
            Console.WriteLine($"score:{score}");

        }
    }
}

public class Mirror
{
    required public int row { get; set; }
    required public int col { get; set; }
    required public char[,] grid { get; set; }
    public char? north
    {
        get
        {
            if (row == 0) { return null; }
            return grid[row - 1, col];
        }
    }
    public void goNorth()
    {
        grid[row, col] = '.';
        row = row - 1;
        grid[row, col] = 'O';
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