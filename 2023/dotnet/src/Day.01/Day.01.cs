using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using AdventOfCode;

Console.WriteLine("Advent of Code 2023");

List<string> inputData = new List<string>();

// Load data from file
using StreamReader reader = new("var/day_01/part_01.txt");
while (!reader.EndOfStream)
{
    string encodedCalibrationValue = reader.ReadLine();
    inputData.Add(encodedCalibrationValue);
}

int sumOfCalibrationValues = 0;
foreach (string encodedCalibrationValue in inputData)
{
    var NumeralExtraction = new NumeralExtraction();
    int calibrationValue = NumeralExtraction.DecodeDay1Part1(encodedCalibrationValue);
    Console.WriteLine($">> {calibrationValue}");
    sumOfCalibrationValues += calibrationValue;
}

Console.WriteLine("");
Console.WriteLine(sumOfCalibrationValues);
