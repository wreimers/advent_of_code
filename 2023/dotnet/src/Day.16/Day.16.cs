namespace Day16
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_16/input.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 16");
            var energizedCoordinates = new List<(int x, int y)>();
            var puzzle = new EnergyPuzzle();
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                puzzle.lines.Add(rawLine);
                // Console.WriteLine(rawLine);
            }
            // Console.WriteLine();
            puzzle.display();
            var usedSplitters = new List<(int, int)>();
            var beamQueue = new Queue<EnergyBeam>();
            var b = new EnergyBeam
            {
                row = 0,
                col = 0,
                direction = Direction.Right,
            };
            beamQueue.Enqueue(b);
            while (beamQueue.Any())
            {
                Console.WriteLine($"beamQueue.Count:{beamQueue.Count}");
                var beam = beamQueue.Dequeue();
                Console.WriteLine($"beamQueue.Count:{beamQueue.Count} beam:{beam}");
                while (beam.hitEdge is false)
                {
                    Console.WriteLine($"beam.row:{beam.row} beam.col:{beam.col} beam.direction:{beam.direction}");
                    if (!energizedCoordinates.Contains((beam.row, beam.col)))
                    {
                        energizedCoordinates.Add((beam.row, beam.col));
                    }
                    char currentChar = puzzle.grid[beam.row, beam.col];
                    (int, int) coords = (-1, -1);
                    Direction newDirection = Direction.None;
                    switch (currentChar)
                    {
                        case '-':
                            if (beam.direction == Direction.Right || beam.direction == Direction.Left) { break; }
                            coords = (beam.row, beam.col);
                            if (!usedSplitters.Contains(coords))
                            {
                                Console.WriteLine($"beam.split '-' usedSplitters:{String.Join(", ", usedSplitters)}");
                                beam.direction = Direction.Left;
                                usedSplitters.Add(coords);
                                b = new EnergyBeam
                                {
                                    row = beam.row,
                                    col = beam.col,
                                    direction = Direction.Right,
                                };
                                beamQueue.Enqueue(b);
                            }
                            break;
                        case '|':
                            if (beam.direction == Direction.Up || beam.direction == Direction.Down) { break; }
                            coords = (beam.row, beam.col);
                            if (!usedSplitters.Contains(coords))
                            {
                                Console.WriteLine($"beam.split '|' usedSplitters:{String.Join(", ", usedSplitters)}");
                                beam.direction = Direction.Down;
                                usedSplitters.Add(coords);
                                b = new EnergyBeam
                                {
                                    row = beam.row,
                                    col = beam.col,
                                    direction = Direction.Up,
                                };
                                beamQueue.Enqueue(b);
                            }
                            break;
                        case '/':
                            if (beam.direction == Direction.Right) { newDirection = Direction.Up; }
                            else if (beam.direction == Direction.Left) { newDirection = Direction.Down; }
                            else if (beam.direction == Direction.Up) { newDirection = Direction.Right; }
                            else if (beam.direction == Direction.Down) { newDirection = Direction.Left; }
                            beam.direction = newDirection;
                            break;
                        case '\\':
                            if (beam.direction == Direction.Right) { newDirection = Direction.Down; }
                            else if (beam.direction == Direction.Left) { newDirection = Direction.Up; }
                            else if (beam.direction == Direction.Up) { newDirection = Direction.Left; }
                            else if (beam.direction == Direction.Down) { newDirection = Direction.Right; }
                            beam.direction = newDirection;
                            break;
                        case '.': break;
                        default:
                            throw new Exception("INVALID CHAR IN PUZZLE");
                    }
                    beam.travel(puzzle);
                }

            }
            foreach ((int, int) coord in energizedCoordinates)
            {
                Console.WriteLine($"energized {coord}");
            }
            Console.WriteLine($"total:{energizedCoordinates.Count}");
        }
    }
}

public enum Direction
{
    Right,
    Left,
    Up,
    Down,
    None,
}

public class EnergyBeam
{
    required public int row { get; set; }
    required public int col { get; set; }
    required public Direction direction { get; set; }
    public bool hitEdge = false;
    public List<(int, int)> usedSplitters = new List<(int x, int y)>();
    public void travel(EnergyPuzzle puzzle)
    {
        switch (direction)
        {
            case Direction.Right:
                if (col == puzzle.colCount - 1)
                {
                    hitEdge = true;
                }
                else
                {
                    col += 1;
                }
                break;
            case Direction.Left:
                if (col == 0)
                {
                    hitEdge = true;
                }
                else
                {
                    col -= 1;
                }
                break;
            case Direction.Up:
                if (row == 0)
                {
                    hitEdge = true;
                }
                else
                {
                    row -= 1;
                }
                break;
            case Direction.Down:
                if (row == puzzle.rowCount - 1)
                {
                    hitEdge = true;
                }
                else
                {
                    row += 1;
                }
                break;
        }
    }
}

public class EnergyPuzzle : Puzzle
{

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
