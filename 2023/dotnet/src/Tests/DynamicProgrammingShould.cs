using System;
using Xunit;
using DynamicProgrammingEXE;

namespace DynamicProgrammingShould
{
    public class DynamicProgrammingShould
    {

        [Fact]
        public void DynamicProgrammingShould_fib_recursive_n_1_is_1()
        {
            // Given
            int n = 1;
            // When
            int f = DynamicProgramming.fib_recursive(n);
            // Then
            Assert.Equal(1, f);
        }

        [Fact]
        public void DynamicProgrammingShould_fib_recursive_n_2_is_1()
        {
            // Given
            int n = 2;
            // When
            int f = DynamicProgramming.fib_recursive(n);
            // Then
            Assert.Equal(1, f);
        }

        [Fact]
        public void DynamicProgrammingShould_fib_recursive_n_3_is_2()
        {
            // Given
            int n = 3;
            // When
            int f = DynamicProgramming.fib_recursive(n);
            // Then
            Assert.Equal(2, f);
        }

    }
}