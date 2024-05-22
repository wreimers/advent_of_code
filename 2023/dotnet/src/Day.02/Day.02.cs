using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using AdventOfCode2023;

Console.WriteLine("Advent of Code 2023 - Day 2");

// Load data from file
List<string> inputData = new List<string>();
using StreamReader reader = new("var/day_02/input.txt");
string? gameRecord;
while ((gameRecord = reader.ReadLine()) != null)
{
    inputData.Add(gameRecord);
}

// Parse records
int sumofGamePower = 0;
char[] splitters = [':', ' ', ',', ';', ];
foreach (string record in inputData) {
    string[] subStrings = record.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
    int gameNumber = Int32.Parse(subStrings[1]);
    Console.WriteLine($"Game {gameNumber}\n{record}");
    Dictionary<string, int> cubeCounts = new Dictionary<string, int>();
    for (int recordIndex = 2; recordIndex < subStrings.Length; recordIndex += 2) {
        int cubeCount = Int32.Parse(subStrings[recordIndex]);
        if ( cubeCount < 0 ) {
            throw new Exception("Couldn't parse int");
        }
        string cubeColor = subStrings[recordIndex + 1];
        if (cubeCounts.ContainsKey(cubeColor) is false) {
            cubeCounts[cubeColor] = cubeCount;
        }
        if (cubeCount > cubeCounts[cubeColor]) {
            cubeCounts[cubeColor] = cubeCount;
        }
    }
    int gamePower = 1;
    foreach (KeyValuePair<string, int> maxCount in cubeCounts) {
        Console.WriteLine($" - {maxCount.Key} {maxCount.Value}");
        gamePower *= maxCount.Value;
    }
    Console.WriteLine($"Game power: {gamePower}");
    sumofGamePower += gamePower;
}
Console.WriteLine($"Sum: {sumofGamePower}");
