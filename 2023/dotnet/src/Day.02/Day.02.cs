using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using AdventOfCode2023;

Console.WriteLine("Advent of Code 2023 - Day 2");

List<string> inputData = new List<string>();

// Load data from file
using StreamReader reader = new("var/day_02/sample_1.txt");
string? gameRecord;
while ((gameRecord = reader.ReadLine()) != null)
{
    inputData.Add(gameRecord);
}

char[] splitters = [':', ' ', ',', ';', ];
foreach (string record in inputData) {
    string[] subStrings = record.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
    string gameNumber = subStrings[1];
    Console.WriteLine($"Game {gameNumber}");
    for (int recordIndex = 2; recordIndex < subStrings.Length; recordIndex += 2) {
        Console.WriteLine($"{subStrings[recordIndex]} {subStrings[recordIndex+1]}");
    }
    
}

public class GameRecord {
    string rawRecord = "";
    int gameNumber = 0;
    

}
