using System;
using Xunit;
using Day05;

namespace AlmanacMapShould
{
    public class AlmanacMapShould
    {

        [Fact]
        public void AlmanacMapShould_BasicEquality()
        {
            // Given
            AlmanacMap mapOne = new AlmanacMap {row=0, srcCategory="cat1", dstCategory="cat2"};
            AlmanacMap mapTwo = new AlmanacMap {row=0, srcCategory="cat1", dstCategory="cat2"};
            // When

            // Then
            Assert.Equal(mapOne, mapTwo);
        }

        [Fact]
        public void AlmanacMapShould_RowsNotEqual_MapsNotEqual()
        {
            // Given
            AlmanacMap mapOne = new AlmanacMap {row=0, srcCategory="cat1", dstCategory="cat2"};
            AlmanacMap mapTwo = new AlmanacMap {row=1, srcCategory="cat1", dstCategory="cat2"};
            // When

            // Then
            Assert.NotEqual(mapOne, mapTwo);
        }
    }
}