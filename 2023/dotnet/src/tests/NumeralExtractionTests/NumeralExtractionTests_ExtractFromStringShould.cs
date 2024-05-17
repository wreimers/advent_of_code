using System;
using Xunit;
using AdventOfCode2023;

namespace NumeralExtractionTests
{
    public class NumeralExtraction_ExtractFromStringShould
    {
        [Fact]
        public void ExtractFromString_InputIsEmpty_ReturnEmpty()
        {
            List<string> result = NumeralExtraction.ExtractFromString("");
            Assert.Empty(result);
        }

        [Fact]
        public void ExtractFromString_InputHasOne_ReturnSingle()
        {
            List<string> result = NumeralExtraction.ExtractFromString("one");
            Assert.Single(result);
        }

        [Fact]
        public void ExtractFromString_InputHasOneOne_ReturnTwo()
        {
            List<string> result = NumeralExtraction.ExtractFromString("oneone");
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ExtractFromString_InputHasOneOne_ReturnOneOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("oneone");
            Assert.Equal(result, new List<string>(["one", "one",]));
        }

        [Fact]
        public void ExtractFromString_InputHasOneOneZeroOneWithGarbage_ReturnOneOneZeroOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("onepdonewzeroooooone");
            Assert.Equal(result, new List<string>(["one", "one", "zero", "one"]));
        }

        [Fact]
        public void ExtractFromString_InputHasOneZeroOne_ReturnOneZeroOne()
        {
            List<string> result = NumeralExtraction.ExtractFromString("onezeroone");
            Assert.Equal(result, new List<string>(["one", "zero", "one",]));
        }

        [Fact]
        public void ExtractFromString_InputHasOneZerOne_ReturnOneZero()
        {
            List<string> result = NumeralExtraction.ExtractFromString("onezerone");
            Assert.Equal(result, new List<string>(["one", "zero",]));
        }




    }
}
