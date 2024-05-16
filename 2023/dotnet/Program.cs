Console.WriteLine("Advent of Code 2023");

List<string> inputData = new List<string>();

// Load data from file
using StreamReader reader = new("day_1_input.txt");
while (!reader.EndOfStream)
{
    string encodedCalibrationValue = reader.ReadLine();
    inputData.Add(encodedCalibrationValue);
}

int sumOfCalibrationValues = 0;
foreach (string encodedCalibrationValue in inputData)
{
    int calibrationValue = DecodeDay1Part1(encodedCalibrationValue);
    Console.WriteLine($">> {calibrationValue}");
    sumOfCalibrationValues += calibrationValue;
}

Console.WriteLine("");
Console.WriteLine(sumOfCalibrationValues);

// Day 1 brute force
static int DecodeDay1Part1(string encodedCalibrationValue)
{
    bool foundFirstNumeral = false;
    char firstNumeral = (char)0;
    char lastNumeral = (char)0;
    foreach (char character in encodedCalibrationValue.ToCharArray())
    {
        if (character >= (char)48 && character <= (char)57)
        {
            if (foundFirstNumeral is false)
            {
                foundFirstNumeral = true;
                firstNumeral = character;
            }
            lastNumeral = character;
        }
    }
    if (foundFirstNumeral is false)
    {
        System.Environment.Exit(1);
    }
    string calibrationValueString = $"{firstNumeral}{lastNumeral}";
    int calibrationValue = Int32.Parse(calibrationValueString);
    return calibrationValue;
}
