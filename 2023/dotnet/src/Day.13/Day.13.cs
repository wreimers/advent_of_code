﻿
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
            foreach (Puzzle p in puzzles)
            {
                Console.WriteLine();
                p.display();
                for (int row = 0; row < p.lines.Count - 1; row += 1)
                {
                    if (p.lines[row] == p.lines[row + 1])
                    {
                        Console.WriteLine($"possible symmetry rows:{row}:{row + 1}");
                        int offset = 0;
                        bool symmetrical = true;
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
            }
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
        for (long row = 0; row < grid.GetLength(0); row += 1)
        {
            for (long col = 0; col < grid.GetLength(1); col += 1)
            {
                Console.Write($"{grid[row, col]}");
            }
            Console.WriteLine("");

        }
    }

    public Symmetry symmetryType = Symmetry.None;

    public int symmetryIndex1 = -1;
    public int symmetryIndex2 = -1;
}