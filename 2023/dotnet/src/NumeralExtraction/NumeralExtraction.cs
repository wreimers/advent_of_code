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

        public List<string> Tokenize(string encodedCalibrationValue)
        {
            List<string> tokens = new List<string>();
            List<char> buffer = new List<char>();
            foreach (char character in encodedCalibrationValue.ToCharArray())
            {
                if (character >= (char)48 && character <= (char)57)
                {
                    if (buffer.Count > 0)
                    {
                        char[] bufferArray = buffer.Cast<char>().ToArray();
                        string bufferString = new string(bufferArray);
                        tokens.Add(bufferString);
                        buffer.Clear();
                    }
                    tokens.Add($"{character}");
                }
                else
                {
                    buffer.Add(character);
                }
            }
            if (buffer.Count > 0)
            {
                char[] bufferArray = buffer.Cast<char>().ToArray();
                string bufferString = new string(bufferArray);
                tokens.Add(bufferString);
            }
            return tokens;
        }

        public List<string> ExtractFromString(string token) {
            throw new NotImplementedException();
        }

/*
        public static int DecodeDay1Part2(string encodedCalibrationValue)
        {
            string[] numerals = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", };
            char[] numeralsFirstLetters = { 'z', 'o', 't', 'f', 's', 'e', 'n', };



            bool foundFirstNumeral = false;
            char firstNumeral = (char)0;
            char lastNumeral = (char)0;
            bool currentCharIsNumeric = false;
            string currentNonNumericBuffer = "";

            foreach (char character in encodedCalibrationValue.ToCharArray())
            {
                if (character >= (char)48 && character <= (char)57)
                {
                    currentCharIsNumeric = true;
                    currentNonNumericBuffer = "";
                    if (foundFirstNumeral is false)
                    {
                        foundFirstNumeral = true;
                        firstNumeral = character;
                    }
                    lastNumeral = character;
                }
                else
                {
                    // Switched to a new non-numeric span; eval buffer
                    if (currentCharIsNumeric is true)
                    {
                        int bufferLength = currentNonNumericBuffer.Length;

                        // Shortest number word is 3 chars long
                        while (bufferLength >= 3)
                        {
                            char firstLetterOfBuffer = currentNonNumericBuffer[0];
                            // First letter isn't a numeral word letter, throw it away
                            if (Array.IndexOf(numeralsFirstLetters, firstLetterOfBuffer) < 0)
                            {
                                currentNonNumericBuffer = currentNonNumericBuffer[1..];
                                bufferLength = currentNonNumericBuffer.Length;
                                continue;
                            }
                            // Loop over the numeralWords and see if our buffer starts with one
                            for (int wordIndex = 0; wordIndex < numerals.Length; wordIndex++)
                            {
                                string numeralWord = numerals[wordIndex];
                                // Found it!
                                if (currentNonNumericBuffer.IndexOf(numeralWord) == 0)
                                {
                                    lastNumeral = (char)(wordIndex + 48);
                                    currentNonNumericBuffer = currentNonNumericBuffer[numeralWord.Length..];
                                    bufferLength = currentNonNumericBuffer.Length;
                                    break;
                                }
                            }
                        }
                    }
                    currentCharIsNumeric = false;
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
*/

    }
}
