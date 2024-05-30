using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace Day05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main_Day5_Part2(args);
        }

        static void Main_Day5_Part2(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 5 Part 2");
            var seeds = new List<Seed>();
            var almanacMaps = new List<AlmanacMap>();            
            double row = 1;
            string? rawLine;
            AlmanacMap? currentMap = null;
            using StreamReader reader = new("var/day_05/input.txt");
            while ((rawLine = reader.ReadLine()) != null)
            {
                if (row==1) {
                    char[] splitters = [' ', ];
                    string[] seedTokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    var seedNumbers = seedTokens[1..].ToList();
                    for (int n=0; n<seedNumbers.Count; n+=2)
                    {
                        var rangeStart = Int64.Parse(seedNumbers[n]);
                        var rangeLength = Int64.Parse(seedNumbers[n+1]);
                        var seed = new Seed {rangeStart=rangeStart, rangeLength=rangeLength};
                        seeds.Add(seed);
                    }
                    Console.WriteLine($"({row}) seeds {String.Join(", ", seeds)}");
                }
                else if (rawLine == "") {
                    currentMap = null;
                }
                else if (currentMap is null) {
                    char[] splitters = ['-', ' ', ];
                    string[] mapTokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    string srcCategory = mapTokens[0];
                    string dstCategory = mapTokens[2];
                    currentMap = new AlmanacMap {row=row, srcCategory=srcCategory, dstCategory=dstCategory};
                    almanacMaps.Add(currentMap);
                    Console.WriteLine($"({row}) {currentMap}");
                }
                else if (currentMap is not null) {
                    char[] splitters = [' ', ];
                    string[] rangeTokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    double dstRangeStart = Int64.Parse(rangeTokens[0]);
                    double srcRangeStart = Int64.Parse(rangeTokens[1]);
                    double rangeLength = Int64.Parse(rangeTokens[2]);
                    var mapEntry = new MapEntry() {row=row, dstRangeStart=dstRangeStart, srcRangeStart=srcRangeStart, rangeLength=rangeLength};
                    currentMap.entries.Add(mapEntry);
                    Console.WriteLine($"({row}) {mapEntry}");
                }
                else {
                    Console.WriteLine($"({row}) {rawLine}");
                }
                row += 1;
            }
            var locationNumbers = new List<double>();
            foreach (Seed seed in seeds)
            {
                string currentCategory = "seed";
                string finalCategory = "location";

                while (true)
                {
                    foreach (AlmanacMap map in almanacMaps)
                    {
                        if (map.srcCategory == currentCategory)
                        {
                            foreach (MapEntry entry in map.entries)
                            {
                                // if (Utilities.RangesOverlap(
                                //     new CategoryRange {start=seed.rangeStart, length=seed.rangeLength},
                                //     new CategoryRange {start=entry.srcRangeStart, length=entry.rangeLength}
                                // ))
                                // {
                                //     double mappedValue = Utilities.MapValue(entry, seed.)
                                // }
                                
                                //     double currentValue = 0;
                                //     for (double k=seedRangeStart; k<seedRangeStart+seed.rangeLength; k+=1)
                                //     {
                                //         currentValue = k;
                                //         if (currentValue >= entryRangeStart && currentValue <= entryRangeStart + entry.rangeLength)
                                //         {
                                //             double offset = entryRangeStart + entry.rangeLength - currentValue;
                                //             currentValue = entry.dstRangeStart + entry.rangeLength - offset;
                                //             break;
                                //         }
                                //     }
                                    
                                // }
                            }
                        }
                    }
                }
            }

            double closestLocation = locationNumbers.Min();
            Console.WriteLine($">> >> minimum location {closestLocation}");
        }

        static void Main_Day5_Part1(string[] args) 
        {
            Console.WriteLine("Advent of Code 2023 Day 5 Part 1");
            using StreamReader reader = new("var/day_05/input.txt");
            double row = 1;
            string? rawLine;
            var seedNumbers = new List<string>();
            var almanacMaps = new List<AlmanacMap>();
            var almanacMapsLeftover = new List<AlmanacMap>();
            AlmanacMap? currentMap = null;
            
            while ((rawLine = reader.ReadLine()) != null)
            {
                if (row==1) {
                    char[] splitters = [' ', ];
                    string[] seedTokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    seedNumbers.AddRange(seedTokens[1..].ToList());
                    Console.WriteLine($"({row}) seeds {String.Join(", ", seedNumbers)}");
                }
                else if (rawLine == "") {
                    currentMap = null;
                }
                else if (currentMap is null) {
                    char[] splitters = ['-', ' ', ];
                    string[] mapTokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    string srcCategory = mapTokens[0];
                    string dstCategory = mapTokens[2];
                    currentMap = new AlmanacMap {row=row, srcCategory=srcCategory, dstCategory=dstCategory};
                    almanacMaps.Add(currentMap);
                    Console.WriteLine($"({row}) {currentMap}");
                }
                else if (currentMap is not null) {
                    char[] splitters = [' ', ];
                    string[] rangeTokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                    double dstRangeStart = Int64.Parse(rangeTokens[0]);
                    double srcRangeStart = Int64.Parse(rangeTokens[1]);
                    double rangeLength = Int64.Parse(rangeTokens[2]);
                    var mapEntry = new MapEntry() {row=row, dstRangeStart=dstRangeStart, srcRangeStart=srcRangeStart, rangeLength=rangeLength};
                    currentMap.entries.Add(mapEntry);
                    Console.WriteLine($"({row}) {mapEntry}");
                }
                else {
                    Console.WriteLine($"({row}) {rawLine}");
                }
                row += 1;
            }
            var locationNumbers = new List<double>();
            foreach (string number in seedNumbers) {
                double currentValue = Int64.Parse(number);
                string currentCategory = "seed";
                string finalCategory = "location";
                // string trackCategory = currentCategory;
                // bool finalLoop = false;
                while (true)
                {
                    // Console.WriteLine($"category {currentCategory}");
                    foreach (AlmanacMap map in almanacMaps)
                    {
                        // todo track the currentValue to detect success
                        if (map.srcCategory == currentCategory)
                        {
                            Console.WriteLine($"category {currentCategory} {currentValue}");
                            foreach (MapEntry entry in map.entries)
                            {
                                if (currentValue >= entry.srcRangeStart && currentValue <= entry.srcRangeStart + entry.rangeLength)
                                {
                                    double offset = entry.srcRangeStart + entry.rangeLength - currentValue;
                                    currentValue = entry.dstRangeStart + entry.rangeLength - offset;
                                    break;
                                }
                            }
                            currentCategory = map.dstCategory;
                            break;
                        }
                    }
                    Console.WriteLine($"next category {currentCategory} {currentValue}");
                    if (currentCategory == finalCategory)
                    {
                        locationNumbers.Add(currentValue);
                        Console.WriteLine($">> LOCATION {currentValue}");
                        break;
                    }
                }
            }
            double closestLocation = locationNumbers.Min();
            Console.WriteLine($">> >> minimum location {closestLocation}");
        }

    }
}
