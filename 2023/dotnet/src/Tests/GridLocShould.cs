using System;
using Xunit;
using Day10;

namespace GridLocShould
{
    public class GridLocShould
    {
        char[][] grid;

        public GridLocShould()
        {
            char[] first = new char[] { '.', '.', '.', '.', '.' };
            char[] second = new char[] { '.', 'S', '-', '7', '.' };
            char[] third = new char[] { '.', '|', '.', '|', '.' };
            char[] fourth = new char[] { '.', 'L', '-', 'J', '.' };
            char[] fifth = new char[] { '.', '.', '.', '.', '.' };
            grid = new char[][] { first, second, third, fourth, fifth };
        }

        [Fact]
        public void GridLocShould_EqualityTest_Equal()
        {
            // Given
            var locA = new GridLoc { row = 1, col = 2, grid = grid, from = FromDirection.Left };
            var locB = new GridLoc { row = 1, col = 2, grid = grid, from = FromDirection.Right };
            // When
            // Then
            // Assert.True(locA.row == locB.row);
            // Assert.True(locA.col == locB.col);
            Assert.True(locA.Equals(locB));
        }
    }
}

