using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day6_Part2(args);
        }

        static void Main_Day6_Part2(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 6 Part 2");

            // Time:      7  15   30
            // Distance:  9  40  200
            // Time:      71530
            // Distance:  940200
            var races = new List<Race>();
            races.Add(new Race {time=71530, distance=940200});

            double product = 1;
            foreach(Race race in races) {
                var winningTimes = new List<double>();
                for (double time=0; time<race.time; time += 1) {
                    double travelTime = race.time - time;
                    double velocity = time;
                    double travelDistance = travelTime * velocity;
                    if (travelDistance > race.distance) {
                        winningTimes.Add(time);
                    }
                }
                product *= winningTimes.Count;
                Console.WriteLine($"winningTimes {String.Join(", ", winningTimes)}");
                Console.WriteLine($"product      {product}");
            }
        }

        static void Main_Day6_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 6 Part 1");

            // Time:        57     72     69     92
            // Distance:   291   1172   1176   2026
            var races = new List<Race>();
            races.Add(new Race {time=57, distance=291});
            races.Add(new Race {time=72, distance=1172});
            races.Add(new Race {time=69, distance=1176});
            races.Add(new Race {time=92, distance=2026});

            double product = 1;
            foreach(Race race in races) {
                var winningTimes = new List<double>();
                for (double time=0; time<race.time; time += 1) {
                    double travelTime = race.time - time;
                    double velocity = time;
                    double travelDistance = travelTime * velocity;
                    if (travelDistance > race.distance) {
                        winningTimes.Add(time);
                    }
                }
                product *= winningTimes.Count;
                Console.WriteLine($"winningTimes {String.Join(", ", winningTimes)}");
                Console.WriteLine($"product      {product}");
            }
        }
    }

}