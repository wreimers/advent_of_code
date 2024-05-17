using System;
using Xunit;
using AdventOfCode2023;

namespace NumeralExtractionTests
{
    public class NumeralExtraction_TokenizeShould
    {
        [Fact]
        public void NumeralExtraction_InputIsEmpty_ReturnEmpty()
        {
            List<string> result = NumeralExtraction.Tokenize("");
            Assert.Empty(result);
        }

        [Fact]
        public void NumeralExtraction_InputIsAlphabet_ReturnSingle()
        {
            List<string> result = NumeralExtraction.Tokenize("abcdefghijklmnopqrstuvwxyz");
            Assert.Equal(result, ["abcdefghijklmnopqrstuvwxyz"]);
        }

        [Fact]
        public void NumeralExtraction_InputIsSplitAlphabet_ReturnThree()
        {
            List<string> result = NumeralExtraction.Tokenize("abcdefghijklm8nopqrstuvwxyz");
            Assert.Equal(result, ["abcdefghijklm", "8", "nopqrstuvwxyz",]);
        }

        [Fact]
        public void NumeralExtraction_InputIsFourNumerals_ReturnFour()
        {
            List<string> result = NumeralExtraction.Tokenize("2468");
            Assert.Equal(result, ["2", "4", "6", "8",]);
        }

        [Fact]
        public void NumeralExtraction_InputIsFourMixed_ReturnFour()
        {
            List<string> result = NumeralExtraction.Tokenize("2legit2quit");
            Assert.Equal(result, ["2", "legit", "2", "quit",]);
        }

    }
}
