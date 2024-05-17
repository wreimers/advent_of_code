using System;
using Xunit;
using AdventOfCode;

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
    }
}
