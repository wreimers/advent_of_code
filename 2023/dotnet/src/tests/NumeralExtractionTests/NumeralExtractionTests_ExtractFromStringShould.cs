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
