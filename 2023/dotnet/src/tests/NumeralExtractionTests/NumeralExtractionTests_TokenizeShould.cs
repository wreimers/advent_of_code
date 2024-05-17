using System;
using Xunit;
using AdventOfCode;

namespace NumeralExtractionTests
{
    public class NumeralExtraction_TokenizeShould
    {
        [Fact]
        public void NumeralExtraction_InputIsEmpty_ReturnEmpty()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.Tokenize("");
            Assert.Empty(result);
        }

        [Fact]
        public void NumeralExtraction_InputIsAlphabet_ReturnSingle()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.Tokenize("abcdefghijklmnopqrstuvwxyz");
            Assert.Equal(result, ["abcdefghijklmnopqrstuvwxyz"]);
        }

        [Fact]
        public void NumeralExtraction_InputIsSplitAlphabet_ReturnThree()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.Tokenize("abcdefghijklm8nopqrstuvwxyz");
            Assert.Equal(result, [ "abcdefghijklm", "8", "nopqrstuvwxyz", ]);
        }

        [Fact]
        public void NumeralExtraction_InputIsFourNumerals_ReturnFour()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.Tokenize("2468");
            Assert.Equal(result, [ "2", "4", "6", "8", ]);
        }

        [Fact]
        public void NumeralExtraction_InputIsFourMixed_ReturnFour()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.Tokenize("2legit2quit");
            Assert.Equal(result, [ "2", "legit", "2", "quit", ]);
        }

    }
}
