using System;
using Xunit;
using Day11;

namespace GalaxyUtilitiesShould
{
    public class GalaxyUtilitiesShould
    {
        [Fact]
        public void GalaxyUtilitiesShould_expandGridAtRow_3x5_4x5()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandGridAtRow(grid, 1);
            // Then
            Assert.Equal(4, newGrid.GetLength(0));
            Assert.Equal(5, newGrid.GetLength(1));
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandGridAtRow_3rows_4rows_Correct()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            char[,] newGridShould = new char[4, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandGridAtRow(grid, 1);
            // Then
            Assert.Equal(newGridShould, newGrid);
        }

        [Fact]
        public void GalaxyUtilitiesShould_hasGalaxyInRow_hasGalaxy_true()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var result = GalaxyUtilities.hasGalaxyInRow(grid, 0);
            // Then
            Assert.True(result);
        }

        [Fact]
        public void GalaxyUtilitiesShould_hasGalaxyInRow_noGalaxy_false()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var result = GalaxyUtilities.hasGalaxyInRow(grid, 1);
            // Then
            Assert.False(result);
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandRowsWithoutGalaxies_3rows_4rows_Correct()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            char[,] newGridShould = new char[4, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandRowsWithoutGalaxies(grid);
            // Then
            Assert.Equal(newGridShould, newGrid);
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandRowsWithoutGalaxies_5rows_7rows_Correct()
        {
            // Given
            char[,] grid = new char[5, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
                {'.','.','.','.','.',},
                {'.','.','#','.','.',},
            };
            char[,] newGridShould = new char[7, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','.','#','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandRowsWithoutGalaxies(grid);
            // Then
            Assert.Equal(newGridShould, newGrid);
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandRowsWithoutGalaxies_3rows_4rows_EndLineCorrect()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','#','.','.','.',},
                {'.','.','.','.','.',},
            };
            char[,] newGridShould = new char[4, 5]
            {   {'.','.','.','#','.',},
                {'.','#','.','.','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandRowsWithoutGalaxies(grid);
            // Then
            Assert.Equal(newGridShould, newGrid);
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandRowsWithoutGalaxies_4rows_6rows_TwoInARowCorrect()
        {
            // Given
            char[,] grid = new char[4, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            char[,] newGridShould = new char[6, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandRowsWithoutGalaxies(grid);
            // Then
            Assert.Equal(newGridShould, newGrid);
        }

        [Fact]
        public void GalaxyUtilitiesShould_hasGalaxyInCol_hasGalaxy_true()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var result = GalaxyUtilities.hasGalaxyInCol(grid, 1);
            // Then
            Assert.True(result);
        }

        [Fact]
        public void GalaxyUtilitiesShould_hasGalaxyInCol_noGalaxy_false()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var result = GalaxyUtilities.hasGalaxyInCol(grid, 2);
            // Then
            Assert.False(result);
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandGridAtCol_3x5_3x6()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandGridAtCol(grid, 2);
            // Then
            Assert.Equal(3, newGrid.GetLength(0));
            Assert.Equal(6, newGrid.GetLength(1));
        }

        [Fact]
        public void GalaxyUtilitiesShould_expandGridAtCol_5cols_6cols_Correct()
        {
            // Given
            char[,] grid = new char[3, 5]
            {   {'.','.','.','#','.',},
                {'.','.','.','.','.',},
                {'.','#','.','.','.',},
            };
            char[,] gridShould = new char[3, 6]
            {   {'.','.','.','.','#','.',},
                {'.','.','.','.','.','.',},
                {'.','.','#','.','.','.',},
            };
            // When
            var newGrid = GalaxyUtilities.expandGridAtCol(grid, 0);
            // Then
            Assert.Equal(gridShould, newGrid);
        }
    }
}
