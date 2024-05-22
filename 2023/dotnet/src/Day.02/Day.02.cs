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

// Set up game parameters
Dictionary<string, int> gameParameters = new Dictionary<string, int>
{
    {"red", 12},
    {"green", 13},
    {"blue", 14},
};
Console.WriteLine($"Game Parameters");
foreach (KeyValuePair<string, int> parameter in gameParameters) {
    Console.WriteLine($" - {parameter.Key} {parameter.Value}");
}

// Parse records
int sumOfPossibleGameNumbers = 0;
char[] splitters = [':', ' ', ',', ';', ];
foreach (string record in inputData) {
    string[] subStrings = record.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
    int gameNumber = Int32.Parse(subStrings[1]);
    Console.WriteLine($"Game {gameNumber}");
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
    bool gameIsPossible = true;
    foreach (KeyValuePair<string, int> maxCount in cubeCounts) {
        Console.WriteLine($" - {maxCount.Key} {maxCount.Value}");
        if (gameParameters.ContainsKey(maxCount.Key) is false) {
            gameIsPossible = false;
        }
        else if ( maxCount.Value > gameParameters[maxCount.Key]) {
            gameIsPossible = false;
        }
    }
    Console.WriteLine($"This game is possible: {gameIsPossible}");
    if (gameIsPossible is true) {
        sumOfPossibleGameNumbers += gameNumber;
    }
}
Console.WriteLine($"Sum: {sumOfPossibleGameNumbers}");
