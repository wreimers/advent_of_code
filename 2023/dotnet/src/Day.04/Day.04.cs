using System;
using System.Reflection.PortableExecutable;

namespace Day04
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Main_Day4_Part2(args);
        }

        static void Main_Day4_Part2(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 4 Part 2");
            using StreamReader reader = new("var/day_04/input.txt");
            while ((rawLine = reader.ReadLine()) != null)
            {
                
            }
        }

        static void Main_Day4_Part1(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 4 Part 1");
            using StreamReader reader = new("var/day_04/input.txt");
            int sum = 0;
            int row = 1;
            string? rawLine;
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"({row}) {rawLine}");
                char[] splitters = [':', '|', ];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                string[] winningNumbers = tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] haveNumbers = tokens[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int matches = 0;
                foreach (string number in haveNumbers ) {
                    if (winningNumbers.Contains(number)) {
                        matches += 1;
                    }
                }
                double cardScore = 0;
                if ( matches > 0) {
                    cardScore = Math.Pow(2, matches - 1);
                }
                Console.WriteLine($"Card score: {cardScore}");
                row += 1;
                sum += (int)cardScore;
            }
            Console.WriteLine($"Sum: {sum}");
        }
    }
}