using System.Data;
using System.Diagnostics;

namespace Day18
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_18/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 18");
            var instructions = new List<Instruction>();
            string? rawLine;
            using StreamReader reader = new(DATA_FILE);
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine(rawLine);
                char[] splitters = [' ', '(', ')', '#',];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                var direction = Direction.None;
                switch (tokens[0].ToCharArray()[0])
                {
                    case 'R':
                        direction = Direction.Right;
                        break;
                    case 'L':
                        direction = Direction.Left;
                        break;
                    case 'U':
                        direction = Direction.Up;
                        break;
                    case 'D':
                        direction = Direction.Down;
                        break;
                }
                var i = new Instruction
                {
                    direction = direction,
                    magnitude = Int32.Parse(tokens[1]),
                    colorCode = tokens[2],
                };
                instructions.Add(i);
            }
            int leftCubes = 0;
            int rightCubes = 0;
            int upCubes = 0;
            int downCubes = 0;
            foreach (Instruction i in instructions)
            {
                switch (i.direction)
                {
                    case Direction.Right:
                        rightCubes += i.magnitude;
                        break;
                    case Direction.Left:
                        leftCubes += i.magnitude;
                        break;
                    case Direction.Up:
                        upCubes += i.magnitude;
                        break;
                    case Direction.Down:
                        downCubes += i.magnitude;
                        break;
                }
                Console.WriteLine(i);
            }
            Console.WriteLine($"leftCubes:{leftCubes} rightCubes:{rightCubes} upCubes:{upCubes} downCubes:{downCubes}");
            int rows = upCubes + downCubes + 1;
            int cols = leftCubes + rightCubes + 1;
            var holes = new CubeHole[rows, cols];
            int currentRow = upCubes + 1;
            int currentCol = leftCubes + 1;
            holes[currentRow, currentCol] = new CubeHole
            {
                row = currentRow,
                col = currentCol,
                colorCode = null,
            };
            foreach (Instruction i in instructions)
            {
                for (int m = 0; m < i.magnitude; m += 1)
                {
                    switch (i.direction)
                    {
                        case Direction.Right:
                            currentCol += 1;
                            break;
                        case Direction.Left:
                            currentCol -= 1;
                            break;
                        case Direction.Up:
                            currentRow += 1;
                            break;
                        case Direction.Down:
                            currentRow -= 1;
                            break;
                    }
                    holes[currentRow, currentCol] = new CubeHole
                    {
                        row = currentRow,
                        col = currentCol,
                        colorCode = i.colorCode,
                    };
                }
            }
        }


    }
}


public class CubeHole
{
    public required int row { get; set; }
    public required int col { get; set; }
    public required string? colorCode { get; set; }

}
public enum Direction
{
    Right,
    Left,
    Up,
    Down,
    None,
}
class Instruction
{
    public required Direction direction { get; set; }
    public required int magnitude { get; set; }
    public required string? colorCode { get; set; }

    public override string? ToString()
    {
        return $"<instruction direction:{direction} magnitude:{magnitude} colorCode:{colorCode}>";
    }
}