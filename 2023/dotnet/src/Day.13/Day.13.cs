
using System.Runtime.CompilerServices;

namespace Day13
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_13/input.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 13");
            Main_Day13(args);
        }

        static void Main_Day13(string[] args)
        {
            var puzzles = new List<Puzzle>();
            var currentPuzzle = new Puzzle();
            puzzles.Add(currentPuzzle);
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                if (rawLine == "")
                {
                    currentPuzzle = new Puzzle();
                    puzzles.Add(currentPuzzle);
                    continue;
                }
                currentPuzzle.lines.Add(rawLine);
            }
            Console.WriteLine($"puzzles:{puzzles} count:{puzzles.Count}");
            int grandTotal = 0;
            foreach (Puzzle p in puzzles)
            {
                Console.WriteLine();
                p.display();
                // -- check for row symmetry
                bool symmetrical = false;
                for (int row = 0; row < p.lines.Count - 1; row += 1)
                {
                    if (p.lines[row] == p.lines[row + 1])
                    {
                        Console.WriteLine($"possible symmetry rows:{row}:{row + 1}");
                        int offset = 0;
                        symmetrical = true;
                        while (row - offset >= 0 && row + 1 + offset < p.lines.Count)
                        {
                            int frontIndex = row - offset;
                            int backIndex = row + 1 + offset;
                            Console.WriteLine($"frontIndex:{frontIndex} backIndex:{backIndex}");
                            if (p.lines[frontIndex] != p.lines[row + 1 + offset])
                            {
                                symmetrical = false;
                                break;
                            }
                            offset += 1;
                        }
                        if (symmetrical is true)
                        {
                            Console.WriteLine($"symmetry found rows:{row}:{row + 1}");
                            p.symmetryType = Symmetry.Horizontal;
                            p.symmetryIndex1 = row;
                            p.symmetryIndex2 = row + 1;
                            break;
                        }
                    }
                }
                // -- check for column symmetry
                if (symmetrical is false)
                {
                    int columns = p.lines[0].Length;
                    for (int col = 0; col < columns - 1; col += 1)
                    {
                        string leftColumn = p.columnString(col);
                        string rightColumn = p.columnString(col + 1);
                        Console.WriteLine($"leftColumn:{leftColumn} rightColumn:{rightColumn}");
                        if (leftColumn == rightColumn)
                        {
                            Console.WriteLine($"possible symmetry cols:{col}:{col + 1}");
                            int offset = 0;
                            symmetrical = true;
                            while (col - offset >= 0 && col + 1 + offset < columns)
                            {
                                int frontIndex = col - offset;
                                int backIndex = col + 1 + offset;
                                Console.WriteLine($"frontIndex:{frontIndex} backIndex:{backIndex}");
                                Console.WriteLine($"- leftColumn:{p.columnString(frontIndex)} rightColumn:{p.columnString(backIndex)}");
                                if (p.columnString(frontIndex) != p.columnString(backIndex))
                                {
                                    symmetrical = false;
                                    break;
                                }
                                offset += 1;
                            }
                            if (symmetrical is true)
                            {
                                Console.WriteLine($"symmetry found cols:{col}:{col + 1}");
                                p.symmetryType = Symmetry.Vertical;
                                p.symmetryIndex1 = col;
                                p.symmetryIndex2 = col + 1;
                                break;
                            }
                        }
                    }
                }
                int sum = 0;
                switch (p.symmetryType)
                {
                    case Symmetry.None:
                        continue;
                    case Symmetry.Horizontal:
                        // sum += 100 * the number of rows above the line of symmetry
                        // that's [0..symmetryIndex1]
                        sum += (p.symmetryIndex1 + 1) * 100;
                        break;
                    case Symmetry.Vertical:
                        // sum += the number of cols left of the line of symmetry
                        // that's [0..symmetryIndex1]
                        sum += p.symmetryIndex1 + 1;
                        break;
                }
                Console.WriteLine($"sum:{sum}");
                grandTotal += sum;
            }
            Console.WriteLine($"\ngrandTotal:{grandTotal}");
        }
    }
}

public enum Symmetry
{
    None,
    Horizontal,
    Vertical,
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
    public Symmetry symmetryType = Symmetry.None;
    public int symmetryIndex1 = -1;
    public int symmetryIndex2 = -1;
    public string columnString(int column)
    {
        string result = "";
        for (int i = 0; i < lines.Count; i += 1)
        {
            result = $"{result}{grid[i, column]}";
        }
        return result;
    }
}