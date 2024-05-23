using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Collections.Generic;   

Console.WriteLine("Advent of Code 2023 - Day 3");

// Load data from file
List<string> inputData = new List<string>();
using StreamReader reader = new("var/day_03/sample.txt");
string? gameRecord;
while ((gameRecord = reader.ReadLine()) != null)
{
    inputData.Add(gameRecord);
}
