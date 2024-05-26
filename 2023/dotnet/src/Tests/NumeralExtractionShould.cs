using System;
using Xunit;
using AdventOfCode2023;

namespace NumeralExtractionShould
{
    public class NumeralExtractionShould
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

        [Fact]
        public void ExtractFromString_InputIsEmpty_ReturnEmpty()
        {
            List<string> result = NumeralExtraction.ExtractFromString("");
            Assert.Empty(result);
        }

        [Fact]
        public void ExtractFromString_InputHasOne_ReturnOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("one");
            Assert.Equal(new List<string>(["one", ]), result);
        }

        [Fact]
        public void ExtractFromString_InputHasOneOne_ReturnOneOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("oneone");
            Assert.Equal(new List<string>(["one", "one", ]), result);
        }

        [Fact]
        public void ExtractFromString_InputHasOneOneZeroOneWithGarbage_ReturnOneOneZeroOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("onepdonewzeroooooone");
            Assert.Equal(new List<string>(["one", "one", "zero", "one", ]), result);
        }

        [Fact]
        public void ExtractFromString_InputHasOneZeroOne_ReturnOneZeroOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("onezeroone");
            Assert.Equal(new List<string>(["one", "zero", "one", ]), result);
        }

        [Fact]
        public void ExtractFromString_InputHasThreEight_ReturnThreeEight()
        {
            List<string> result = NumeralExtraction.ExtractFromString("threeight");
            Assert.Equal(new List<string>(["three", "eight", ]), result);
        }


    }
}
