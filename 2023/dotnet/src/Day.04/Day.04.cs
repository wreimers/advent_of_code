using System;
using System.Reflection.PortableExecutable;

namespace Day04
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Main_Day4_Part2(args);
        }

        static void Main_Day4_Part2(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 4 Part 2");
            using StreamReader reader = new("var/day_04/sample.txt");
            int row = 1;
            string? rawLine;
            var gameCards = new List<GameCard>();
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"({row}) {rawLine}");
                var card = new GameCard { row=row, text=rawLine, };
                gameCards.Add(card);
                char[] splitters = [':', '|', ];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                card.winningNumbers = tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                card.haveNumbers = tokens[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string number in card.haveNumbers ) 
                {
                    if (card.winningNumbers.Contains(number)) 
                    {
                        card.matches += 1;
                    }
                }
                Console.WriteLine($"({row}) matches {card.matches}");
                row += 1;
            }
            row = 1;
            int totalMagnitude = 0;
            foreach (GameCard card in gameCards) 
            {
                Console.WriteLine($"({row}) magnitude {card.magnitude}");
                for (int tmpMatches = card.matches; tmpMatches > 0; tmpMatches -= 1) {
                    gameCards[row-1+tmpMatches].magnitude += (1 * card.magnitude);
                }
                totalMagnitude += card.magnitude;
                row += 1;
            }
            Console.WriteLine($"Total magnitude {totalMagnitude}");
        }

        static void Main_Day4_Part1(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 4 Part 1");
            using StreamReader reader = new("var/day_04/input.txt");
            int sum = 0;
            int row = 1;
            string? rawLine;
            while ((rawLine = reader.ReadLine()) != null)
            {
                Console.WriteLine($"({row}) {rawLine}");
                char[] splitters = [':', '|', ];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                string[] winningNumbers = tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] haveNumbers = tokens[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int matches = 0;
                foreach (string number in haveNumbers ) {
                    if (winningNumbers.Contains(number)) {
                        matches += 1;
                    }
                }
                double cardScore = 0;
                if ( matches > 0) {
                    cardScore = Math.Pow(2, matches - 1);
                }
                Console.WriteLine($"Card score: {cardScore}");
                row += 1;
                sum += (int)cardScore;
            }
            Console.WriteLine($"Sum: {sum}");
        }
    }

    public class GameCard {
        public required int row {get; set;}
        public required string text {get; set;}
        public List<string> winningNumbers = new List<string>();
        public List<string> haveNumbers = new List<string>();
        public int matches = 0;
        public int magnitude = 1;

    }

}