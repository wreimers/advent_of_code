using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Security.AccessControl;

namespace Day03
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Main_Day3_Part2(args);
        }

        static void Main_Day3_Part2(string[] args) {
            Console.WriteLine("Advent of Code 2023 - Day 3 Part 2");
            // Load data from file
            using StreamReader reader = new("var/day_03/input.txt");
            int row = 0;
            var partNumbers = new List<PartNumber>();
            SchematicLine? previousLine = null;
            string? rawLine;
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"({row}) {rawLine}");
                row += 1;
            }
        }

        static void Main_Day3_Part1(string[] args)
        {

            Console.WriteLine("Advent of Code 2023 - Day 3 Part 1");

            // Load data from file
            using StreamReader reader = new("var/day_03/input.txt");
            int row = 0;
            var partNumbers = new List<PartNumber>();
            SchematicLine? previousLine = null;
            string? rawLine;
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"({row}) {rawLine}");

                SchematicLine currentLine = new SchematicLine {row=row, text=rawLine};
                if (previousLine is not null)
                {
                    List<PartNumber> symbolAdjacentPartNumbers = 
                        SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
                    partNumbers.AddRange(symbolAdjacentPartNumbers);
                }
                else
                {
                    List<PartNumber> symbolAdjacentPartNumbers = 
                        SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, new SchematicLine {row=row-1, text=""});
                    partNumbers.AddRange(symbolAdjacentPartNumbers);
                }
                previousLine = currentLine;
                row += 1;
            }

            int sum = 0;
            foreach (PartNumber partNumber in partNumbers) 
            {
                if (partNumber.symbolAdjacent is true)
                {   
                    sum += partNumber.number;
                    Console.WriteLine($"{partNumber.number}");
                }
            }

            Console.WriteLine($"Sum {sum}");

        }
    }
}
