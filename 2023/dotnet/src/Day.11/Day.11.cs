using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day11(args);
        }

        static void Main_Day11(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 11");
            string? rawLine;
            using StreamReader reader = new("var/day_11/sample.txt");
            int row = 0;
            while ((rawLine = reader.ReadLine()) != null)
            {
                row += 1;
                Console.WriteLine($"{rawLine}");
            }
        }
    }
}
