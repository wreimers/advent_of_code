using System;
using Xunit;
using Day07;

namespace HandShould
{
    public class HandShould
    {

        [Fact]
        public void HandShould_FiveAces_5kind()
        {
            // Given
            var hand = new Hand {hand="AAAAA", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("5kind", result);
        }

        [Fact]
        public void HandShould_FourAces_4kind()
        {
            // Given
            var hand = new Hand {hand="7AAAA", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("4kind", result);
        }

        [Fact]
        public void HandShould_ThreeAcesTwoTwos_Fullhouse()
        {
            // Given
            var hand = new Hand {hand="2A2AA", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("fullhouse", result);
        }

        [Fact]
        public void HandShould_ThreeFives_3kind()
        {
            // Given
            var hand = new Hand {hand="5A5J5", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("3kind", result);
        }

        [Fact]
        public void HandShould_TwoFivesTwoSixes_2pair()
        {
            // Given
            var hand = new Hand {hand="6655K", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("2pair", result);
        }

        [Fact]
        public void HandShould_TwoJacks_1pair()
        {
            // Given
            var hand = new Hand {hand="1JJKQ", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("1pair", result);
        }

        [Fact]
        public void HandShould_Straight_Highcard()
        {
            // Given
            var hand = new Hand {hand="56789", bid=0};
            // When
            var result = hand.Kind();
            // Then
            Assert.Equal("highcard", result);
        }

    }
}
