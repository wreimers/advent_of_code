using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day7_Part1(args);
        }

        static void Main_Day7_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 7 Part 1");
            string? rawLine;
            using StreamReader reader = new("var/day_07/sample.txt");
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"{rawLine}");
                string[] tokens = rawLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
