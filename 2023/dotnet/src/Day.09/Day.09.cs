using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day9_Part1(args);
        }

        static void Main_Day9_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 9 Part 1");
            string? rawLine;
            using StreamReader reader = new("var/day_05/input.txt");
            while ((rawLine = reader.ReadLine()) != null)
            {
                char[] splitters = [' ', ];
                string[] tokensRaw = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                double[] tokens = new double[tokensRaw.Length];
                for (int k=0; k<tokensRaw.Length; k+=1)
                {
                    string token = tokensRaw[k];
                    tokens[k] = Int64.Parse(token);
                }
                double[][] invertedPyramid = new double[tokensRaw.Length][];
                invertedPyramid[0] = new double[tokensRaw.Length];
                for (int k=0; k<tokens.Length; k+=1)
                {
                    invertedPyramid[0][k] = tokens[k];
                }
                bool allZeros = true;
                int row = 0;
                do
                {
                    allZeros = true;
                    var currentRow = invertedPyramid[row];
                    invertedPyramid[row+1] = new double[currentRow.Length-1];
                    var nextRow = invertedPyramid[row+1];
                    for (int j=0; j<nextRow.Length; j+=1)
                    {
                        double difference = currentRow[j+1] - currentRow[j];
                        nextRow[j] = difference;
                        if (difference != 0)
                        {
                            allZeros = false;
                        }
                    }
                    row += 1;
                }
                while (allZeros == false);
                row -= 1;
                
            }
        }
    }
}
