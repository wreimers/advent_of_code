using System;
using Xunit;
using Day05;

namespace UtilitiesMapShould
{
    public class UtilitiesMapShould
    {

        [Fact]
        public void UtilitiesShould_RangesOverlap_RangeSubset_ReturnValue()
        {
            // Given
            CategoryRange r1 = new CategoryRange {start=10, length=10};
            CategoryRange r2 = new CategoryRange {start=12, length=6};
            // When
            double? answer = Utilities.RangeOverlap(r1, r2);
            // Then
            Assert.Equal(12, answer);
        }

        [Fact]
        public void UtilitiesShould_RangesOverlap_RangeAfter_ReturnNull()
        {
            // Given
            CategoryRange r1 = new CategoryRange {start=10, length=10};
            CategoryRange r2 = new CategoryRange {start=30, length=10};
            // When
            double? answer = Utilities.RangeOverlap(r1, r2);
            // Then
            Assert.Null(answer);
        }

        [Fact]
        public void UtilitiesShould_RangesOverlap_RangeBefore_ReturnNull()
        {
            // Given
            CategoryRange r1 = new CategoryRange {start=10, length=10};
            CategoryRange r2 = new CategoryRange {start=0, length=10};
            // When
            double? answer = Utilities.RangeOverlap(r1, r2);
            // Then
            Assert.Null(answer);
        }

        [Fact]
        public void UtilitiesShould_RangesOverlap_OverlapInFront_ReturnValue()
        {
            // Given
            CategoryRange r1 = new CategoryRange {start=10, length=10};
            CategoryRange r2 = new CategoryRange {start=5, length=10};
            // When
            double? answer = Utilities.RangeOverlap(r1, r2);
            // Then
            Assert.Equal(10, answer);
        }

        [Fact]
        public void UtilitiesShould_RangesOverlap_OverlapInBack_ReturnTrue()
        {
            // Given
            CategoryRange r1 = new CategoryRange {start=10, length=10};
            CategoryRange r2 = new CategoryRange {start=15, length=10};
            // When
            double? answer = Utilities.RangeOverlap(r1, r2);
            // Then
            Assert.Equal(15, answer);
        }

        [Fact]
        public void UtilitiesShould_MapValue_MapTest1_ReturnValue()
        {
            // Given
            MapEntry entry = new MapEntry {row=0, dstRangeStart=10, srcRangeStart=100, rangeLength=10};
            double value = 105;
            // When
            double answer = Utilities.MapValue(entry, value);
            // Then
            Assert.Equal(15, answer);
        }

        [Fact]
        public void UtilitiesShould_MapValue_MapTest2_ReturnValue()
        {
            // Given
            MapEntry entry = new MapEntry {row=0, dstRangeStart=100, srcRangeStart=10, rangeLength=10};
            double value = 15;
            // When
            double answer = Utilities.MapValue(entry, value);
            // Then
            Assert.Equal(105, answer);
        }
    }
}
