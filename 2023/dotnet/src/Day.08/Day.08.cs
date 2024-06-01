using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day8_Part1(args);
        }

        static void Main_Day8_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 8 Part 1");
            var networkNodes = new Dictionary<string, NetworkNode>();
            
            string? rawLine;
            using StreamReader reader = new("var/day_08/sample2.txt");
            string? rawInstructions = reader.ReadLine();
            var instructions = rawInstructions.ToCharArray().ToList();
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"{rawLine}");
                if (rawLine == "")
                {
                    continue;
                }
                char[] splitters = [' ', '=', '(', ',', ')', ];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                string left = tokens[1];
                string right = tokens[2];
                NetworkNode node = new NetworkNode {name=name, left=left, right=right};
                networkNodes[name] = node;
            }

            var currentNode = "AAA";
            var targetNode = "ZZZ";
            int steps = 0;
            int instructionIndex = 0;
            while (currentNode != targetNode) {
                Console.WriteLine($"currentNode {currentNode}");
                steps += 1;
                var node = networkNodes[currentNode];
                if (instructionIndex == instructions.Count)
                {
                    instructionIndex = 0;
                }
                var instruction = instructions[instructionIndex];
                instructionIndex += 1;
                if (instruction == 'L')
                {
                    currentNode = node.left;
                    Console.WriteLine($"going left to {node.left}");
                }
                else if (instruction == 'R')
                {
                    currentNode = node.right;
                    Console.WriteLine($"going right to {node.right}");
                }
            }
            Console.WriteLine($"steps {steps}");
        }
    }

    public class NetworkNode {
        public required string name;
        public required string left;
        public required string right;
    }

}
