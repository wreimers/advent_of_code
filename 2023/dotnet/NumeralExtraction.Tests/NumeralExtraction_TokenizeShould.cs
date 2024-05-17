using Xunit;
using AdventOfCode;

namespace AdventOfCode.UnitTests
{
    public class NumeralExtraction_TokenizeShould
    {
        [Fact]
        public void Tokenize_InputIsBlank_ReturnEmptyList()
        {
            var NumeralExtraction = new NumeralExtraction();
            List<string> result = NumeralExtraction.Tokenize("");
            Assert.Empty(result);
        }

        [Fact]
        public void Tokenize_InputHasNoNumerals_ReturnSingleItemList()
        {
            var NumeralExtraction = new NumeralExtraction();
            List<string> result = NumeralExtraction.Tokenize("abcdefghijklmnopqrstuvwxyz");
            Assert.Single(result);
            Assert.Equal("abcdefghijklmnopqrstuvwxyz", result[0]);
        }

    }
}
