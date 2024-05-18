using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using AdventOfCode2023;

Console.WriteLine("Advent of Code 2023");

List<string> inputData = new List<string>();

// Load data from file
using StreamReader reader = new("var/day_01/input.txt");
while (!reader.EndOfStream)
{
    string encodedCalibrationValue = reader.ReadLine();
    inputData.Add(encodedCalibrationValue);
}

int sumOfCalibrationValues = 0;
foreach (string encodedCalibrationValue in inputData)
{
    // int calibrationValue = NumeralExtraction.DecodeDay1Part1(encodedCalibrationValue);
    int calibrationValue = NumeralExtraction.DecodeDay1Part2(encodedCalibrationValue);
    sumOfCalibrationValues += calibrationValue;
}

Console.WriteLine($"Sum from {inputData.Count} values: {sumOfCalibrationValues}");
