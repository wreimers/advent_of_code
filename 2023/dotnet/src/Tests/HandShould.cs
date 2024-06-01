using System;
using Xunit;
using Day07;

namespace HandShould
{
    public class HandShould
    {

        [Fact]
        public void HandShould_Kind_AAAAA_5kind()
        {
            // Given
            var hand = new Hand {cards="AAAAA", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("5kind", result);
        }

        [Fact]
        public void HandShould_Kind_7AAAA_4kind()
        {
            // Given
            var hand = new Hand {cards="7AAAA", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("4kind", result);
        }

        [Fact]
        public void HandShould_Kind_2A2AA_Fullhouse()
        {
            // Given
            var hand = new Hand {cards="2A2AA", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("fullhouse", result);
        }

        [Fact]
        public void HandShould_Kind_QQJJJ_Fullhouse()
        {
            // Given
            var hand = new Hand {cards="QQJJJ", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("fullhouse", result);
        }

        [Fact]
        public void HandShould_Kind_5A5J5_3kind()
        {
            // Given
            var hand = new Hand {cards="5A5J5", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("3kind", result);
        }

        [Fact]
        public void HandShould_Kind_6655K_2pair()
        {
            // Given
            var hand = new Hand {cards="6655K", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("2pair", result);
        }

        [Fact]
        public void HandShould_Kind_1JJKQ_1pair()
        {
            // Given
            var hand = new Hand {cards="1JJKQ", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("1pair", result);
        }

        [Fact]
        public void HandShould_Kind_56789_Highcard()
        {
            // Given
            var hand = new Hand {cards="56789", bid=0};
            // When
            var result = hand.kind;
            // Then
            Assert.Equal("highcard", result);
        }

        [Fact]
        public void HandShould_BetterHand_L5kindR4kind_LeftHand()
        {
            // Given
            var leftHand  = new Hand {cards="22222", bid=0};
            var rightHand = new Hand {cards="AAAAK", bid=0};
            // When
            var result = Hand.BetterHand(leftHand, rightHand);
            // Then
            Assert.Equal(leftHand, result);
        }

        [Fact]
        public void HandShould_BetterHand_L3kindR4kind_RightHand()
        {
            // Given
            var leftHand  = new Hand {cards="7477A", bid=0};
            var rightHand = new Hand {cards="AAAAK", bid=0};
            // When
            var result = Hand.BetterHand(leftHand, rightHand);
            // Then
            Assert.Equal(rightHand, result);
        }

        [Fact]
        public void HandShould_BetterHand_LFullHouse63RFullHouseJQ_RightHand()
        {
            // Given
            var leftHand  = new Hand {cards="66363", bid=0};
            var rightHand = new Hand {cards="QQJJJ", bid=0};
            // When
            var result = Hand.BetterHand(leftHand, rightHand);
            // Then
            Assert.Equal(rightHand, result);
        }

        [Fact]
        public void HandShould_BetterHand_LKK677RKTJJT_LeftHand()
        {
            // Given
            var leftHand  = new Hand {cards="KK677", bid=0};
            var rightHand = new Hand {cards="KTJJT", bid=0};
            // When
            var result = Hand.BetterHand(leftHand, rightHand);
            // Then
            Assert.Equal(leftHand, result);
        }

        [Fact]
        public void HandShould_BetterHand_LKTJJTRKK677_RightHand()
        {
            // Given
            var leftHand  = new Hand {cards="KTJJT", bid=0};
            var rightHand = new Hand {cards="KK677", bid=0};
            // When
            var result = Hand.BetterHand(leftHand, rightHand);
            // Then
            Assert.Equal(rightHand, result);
        }

        [Fact]
        public void HandShould_sortedHand_KTJJT_KTTJJ()
        {
            // Given
            var hand  = new Hand {cards="KTJJT", bid=0};
            // When
            var result = hand.sortedHand;
            // Then
            Assert.Equal(new int[] {11, 8, 8, 9, 9}, result);
        }

    }
}
