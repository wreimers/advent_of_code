
using System.Runtime.CompilerServices;

namespace Day13
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_13/sample.txt";

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

                Console.WriteLine($"Checking puzzle");
                // -- check for row symmetry
                bool symmetrical = p.checkForHorizontalSymmetry();
                // -- check for column symmetry
                if (symmetrical is false)
                {
                    symmetrical = p.checkForVerticalSymmetry();
                }
                if (symmetrical is false)
                {
                    throw new Exception("UNSYMMETRIC PUZZLE");
                }

                Console.WriteLine($"Checking puzzle 2");
                // find the smudge
                Puzzle p2 = new Puzzle();
                for (int row = 0; row < p.lines.Count; row += 1)
                {
                    for (int col = 0; col < p.lines[0].Length; col += 1)
                    {
                        p2 = p.swapChar(row, col);
                        bool p2symmetrical = p2.checkForHorizontalSymmetry(p.symmetryType, p.symmetryIndex1, p.symmetryIndex2);
                        if (p2symmetrical is false)
                        {
                            p2symmetrical = p2.checkForVerticalSymmetry(p.symmetryType, p.symmetryIndex1, p.symmetryIndex2);
                        }
                        if (p2symmetrical is true)
                        {
                            goto SmudgeFound;
                        }
                    }
                }
            SmudgeFound:;

                int sum = 0;
                switch (p2.symmetryType)
                {
                    case Symmetry.None:
                        continue;
                    case Symmetry.Horizontal:
                        sum += (p2.symmetryIndex1 + 1) * 100;
                        break;
                    case Symmetry.Vertical:
                        sum += p2.symmetryIndex1 + 1;
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

    public bool checkForHorizontalSymmetry(Symmetry? ignoreSymmetryType = null, int? ignoreIndex1 = null, int? ignoreIndex2 = null)
    {
        // Console.WriteLine($"checkForHorizontalSymmetry ignoreSymmetryType:{ignoreSymmetryType} ignoreIndex1:{ignoreIndex1} ignoreIndex2:{ignoreIndex2}");
        bool symmetrical = false;
        for (int row = 0; row < lines.Count - 1; row += 1)
        {
            if (lines[row] == lines[row + 1])
            {
                if (ignoreSymmetryType == Symmetry.Horizontal && row == ignoreIndex1 && row + 1 == ignoreIndex2)
                {
                    continue;
                }
                // Console.WriteLine($"possible symmetry rows:{row}:{row + 1}");
                int offset = 0;
                symmetrical = true;
                while (row - offset >= 0 && row + 1 + offset < lines.Count)
                {
                    int frontIndex = row - offset;
                    int backIndex = row + 1 + offset;
                    // Console.WriteLine($"frontIndex:{frontIndex} backIndex:{backIndex}");
                    if (lines[frontIndex] != lines[row + 1 + offset])
                    {
                        symmetrical = false;
                        break;
                    }
                    offset += 1;
                }
                if (symmetrical is true)
                {
                    Console.WriteLine($"symmetry found rows:{row}:{row + 1}");
                    symmetryType = Symmetry.Horizontal;
                    symmetryIndex1 = row;
                    symmetryIndex2 = row + 1;
                    break;
                }
            }
        }
        return symmetrical;
    }

    public bool checkForVerticalSymmetry(Symmetry? ignoreSymmetryType = null, int? ignoreIndex1 = null, int? ignoreIndex2 = null)
    {
        bool symmetrical = false;
        int columns = lines[0].Length;
        for (int col = 0; col < columns - 1; col += 1)
        {
            string leftColumn = columnString(col);
            string rightColumn = columnString(col + 1);
            // Console.WriteLine($"leftColumn:{leftColumn} rightColumn:{rightColumn}");
            if (leftColumn == rightColumn)
            {
                // Console.WriteLine($"possible symmetry cols:{col}:{col + 1}");
                if (ignoreSymmetryType == Symmetry.Vertical && col == ignoreIndex1 && col + 1 == ignoreIndex2)
                {
                    continue;
                }
                int offset = 0;
                symmetrical = true;
                while (col - offset >= 0 && col + 1 + offset < columns)
                {
                    int frontIndex = col - offset;
                    int backIndex = col + 1 + offset;
                    // Console.WriteLine($"frontIndex:{frontIndex} backIndex:{backIndex}");
                    // Console.WriteLine($"- leftColumn:{columnString(frontIndex)} rightColumn:{columnString(backIndex)}");
                    if (columnString(frontIndex) != columnString(backIndex))
                    {
                        symmetrical = false;
                        break;
                    }
                    offset += 1;
                }
                if (symmetrical is true)
                {
                    Console.WriteLine($"symmetry found cols:{col}:{col + 1}");
                    symmetryType = Symmetry.Vertical;
                    symmetryIndex1 = col;
                    symmetryIndex2 = col + 1;
                    break;
                }
            }
        }
        return symmetrical;
    }

    public Puzzle swapChar(int row, int col)
    {
        // Console.WriteLine($"swapChar row:{row} col:{col}");
        Puzzle p = new Puzzle();
        foreach (string s in lines)
        {
            p.lines.Add(s);
        }
        string rowString = p.lines[row];
        // Console.WriteLine($"rowString:{rowString}");
        char currentChar = rowString[col];
        // Console.WriteLine($"currentChar:{currentChar}");
        char newChar = '.';
        if (currentChar == '.')
        {
            newChar = '#';
        }
        string newString = rowString.Remove(col, 1);
        // Console.WriteLine($"newString(remove):{newString}");
        newString = newString.Insert(col, newChar.ToString());
        // Console.WriteLine($"newString(insert):{newString}");
        p.lines[row] = newString;
        return p;
    }
}