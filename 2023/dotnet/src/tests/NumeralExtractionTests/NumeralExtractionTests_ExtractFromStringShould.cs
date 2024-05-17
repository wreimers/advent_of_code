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

    }
}
