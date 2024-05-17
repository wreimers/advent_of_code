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
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("");
            Assert.Empty(result);
        }

        [Fact]
        public void ExtractFromString_InputHasOne_ReturnSingle()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("one");
            Assert.Single(result);
        }

        [Fact]
        public void ExtractFromString_InputHasOneOne_ReturnTwo()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("oneone");
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ExtractFromString_InputHasOneOne_ReturnOneOne()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("oneone");
            Assert.Equal(result, new List<string>(["one", "one",]));
        }

        [Fact]
        public void ExtractFromString_InputHasOneOneZeroOneWithGarbage_ReturnOneOneZeroOne()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("onepdonewzeroooooone");
            Assert.Equal(result, new List<string>(["one", "one", "zero", "one"]));
        }

        [Fact]
        public void ExtractFromString_InputHasOneZeroOne_ReturnOneZeroOne()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("onezeroone");
            Assert.Equal(result, new List<string>(["one", "zero", "one",]));
        }

        [Fact]
        public void ExtractFromString_InputHasOneZerOne_ReturnOneZero()
        {
            var numeralExtraction = new NumeralExtraction();
            List<string> result = numeralExtraction.ExtractFromString("onezerone");
            Assert.Equal(result, new List<string>(["one", "zero",]));
        }




    }
}
