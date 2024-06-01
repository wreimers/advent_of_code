using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day7_Part1(args);
        }

        public static int CompareHands(Hand leftHand, Hand rightHand) {
            if (leftHand.cards == rightHand.cards) { return 0; }
            var result = Hand.BetterHand(leftHand, rightHand);
            if (result == leftHand) { return 1; }
            return -1;
        }

        static void Main_Day7_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 7 Part 1");
            var hands = new List<Hand>();
            string? rawLine;
            using StreamReader reader = new("var/day_07/sample.txt");
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"{rawLine}");
                string[] tokens = rawLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string cards = tokens[0];
                string bid = tokens[1];
                var hand = new Hand {cards=cards, bid=Int32.Parse(bid)};
                hands.Add(hand);
            }
            hands.Sort(CompareHands);
            double sum = 0;
            for (int i=0; i<hands.Count; i+=1)
            {
                Hand hand = hands[i];
                sum += hand.bid * (i+1);
                Console.WriteLine($"{hand}");
            }
            Console.WriteLine($"sum {sum}");
        }
    }
}
