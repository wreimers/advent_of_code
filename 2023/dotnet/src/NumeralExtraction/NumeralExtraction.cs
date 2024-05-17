using System;

namespace AdventOfCode2023
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

        public static List<string> Tokenize(string encodedCalibrationValue)
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

        private static string[] numeralWords = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", };

        private static string FirstNumeralWordInString(string token)
        {
            string earliestWord = "";
            int earliestWordIndex = -1;
            foreach (string word in numeralWords)
            {
                int wordIndex = token.IndexOf(word);
                if (wordIndex < 0) continue;
                if (wordIndex == 0) return word;
                if (earliestWordIndex == -1 || wordIndex < earliestWordIndex)
                {
                    earliestWord = word;
                    earliestWordIndex = wordIndex;
                }
            }
            return earliestWord;
        }

        public static List<string> ExtractFromString(string token)
        {
            List<string> resultWords = new List<string>();
            while (true)
            {
                string wordMaybe = FirstNumeralWordInString(token);
                if (wordMaybe == "") break;
                resultWords.Add(wordMaybe);
                token = token[(token.IndexOf(wordMaybe) + wordMaybe.Length)..];
            }
            List<string> returnWords = resultWords.Where(w => w != "").ToList();
            return returnWords;
        }

        public static int DecodeDay1Part2(string encodedCalibrationValue)
        {
            Console.WriteLine($"[[ {encodedCalibrationValue} ]]");
            List<string> tokens = Tokenize(encodedCalibrationValue);
            Console.WriteLine($"+ Tokenize >> {String.Join(".", tokens)}");
            List<int> numerals = new List<int>();
            foreach (string token in tokens)
            {
                int number = 0;
                bool canConvert = Int32.TryParse(token, out number);
                if (canConvert is true)
                {
                    numerals.Add(number);
                    Console.WriteLine($"+ Raw Numeral >> [{number}]");
                    continue;
                }
                List<string> subTokens = ExtractFromString(token);
                Console.WriteLine($"+ ExtractFromString {token} >> [{String.Join(".", subTokens)}]");
                foreach (string subToken in subTokens)
                {
                    int indexOfSubToken = Array.IndexOf(numeralWords, subToken);
                    numerals.Add(indexOfSubToken);
                }
            }
            Console.WriteLine($"+ numerals >> {String.Join(".", numerals)}");
            string calibrationValueString = $"{numerals[0]}{numerals[numerals.Count - 1]}";
            int calibrationValue = Int32.Parse(calibrationValueString);
            Console.WriteLine($"{encodedCalibrationValue} >> {calibrationValue}");
            Console.WriteLine("");
            return calibrationValue;
        }

    }
}
