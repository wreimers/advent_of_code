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
            foreach (Instruction i in instructions)
            {
                Console.WriteLine(i);
            }
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
class Instruction
{
    public required Direction direction { get; set; }
    public required int magnitude { get; set; }
    public required string colorCode { get; set; }

    public override string? ToString()
    {
        return $"<instruction direction:{direction} magnitude:{magnitude} colorCode:{colorCode}>";
    }
}