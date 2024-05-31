using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day6_Part1(args);
        }

        static void Main_Day6_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 6 Part 1");

            // Time:      7  15   30
            // Distance:  9  40  200
            var races = new List<Race>();
            races.Add(new Race {time=7, distance=9});
            races.Add(new Race {time=15, distance=40});
            races.Add(new Race {time=30, distance=200});

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