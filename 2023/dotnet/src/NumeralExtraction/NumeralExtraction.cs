using System;

namespace AdventOfCode
{
    public class NumeralExtraction
    {
        public static int DecodeDay1Part1(string encodedCalibrationValue)
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

    }
}
