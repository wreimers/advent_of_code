using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day8_Part2(args);
        }

        static void Main_Day8_Part2(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 8 Part 2");
            var networkNodes = new Dictionary<string, NetworkNode>();
            var nodesEndingWithA = new List<NetworkNode>();
            string? rawLine;
            using StreamReader reader = new("var/day_08/sample3.txt");
            string? rawInstructions = reader.ReadLine();
            if (rawInstructions is null)
            {
                throw new Exception("WHY IS RAWINSTRUCTIONS NULL");
            }
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
                if (name.EndsWith("A")) {
                    nodesEndingWithA.Add(node);
                }
            }
            var currentNodes = nodesEndingWithA;
            bool finishState = false;
            int steps = 0;
            int instructionIndex = 0;
            while (true) {
                finishState = true;
                for (int i=0; i<currentNodes.Count; i+=1)
                {
                    var node = currentNodes[i];
                    if (node.name == node.left && node.name == node.right)
                    {
                        throw new Exception("REACHED LEAF NODE");
                    }
                    if (! node.name.EndsWith("Z"))
                    {
                        Console.WriteLine($"node {node.name} disqualifies finishState");
                        finishState = false;
                        break;
                    }
                }
                
                if (finishState is true)
                {
                    break;
                }
                steps += 1;
                if (instructionIndex == instructions.Count)
                {
                    instructionIndex = 0;
                }
                for (int i=0; i<currentNodes.Count; i+=1)
                {
                    var node = currentNodes[i];
                    Console.WriteLine($"node {node.name} traverse");
                    var instruction = instructions[instructionIndex];
                    if (instruction == 'L')
                    {
                        currentNodes[i] = networkNodes[node.left];
                        Console.WriteLine($"going left to {node.left}");
                    }
                    if (instruction == 'R')
                    {
                        currentNodes[i] = networkNodes[node.right];
                        Console.WriteLine($"going right to {node.right}");
                    }
                }
                instructionIndex += 1;
            }
            Console.WriteLine($"steps {steps}");
        }

static void Main_Day8_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 8 Part 1");
            var networkNodes = new Dictionary<string, NetworkNode>();
            
            string? rawLine;
            using StreamReader reader = new("var/day_08/input.txt");
            string? rawInstructions = reader.ReadLine();
            if (rawInstructions is null)
            {
                throw new Exception("WHY IS RAWINSTRUCTIONS NULL");
            }
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
