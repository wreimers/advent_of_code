using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Collections.Generic;   

Console.WriteLine("Advent of Code 2023 - Day 3");

// Load data from file
using StreamReader reader = new("var/day_03/sample.txt");
int row = 0;
string? engineLayer;
var partNumbers = new List<PartNumber>();
while ((engineLayer = reader.ReadLine()) != null) {
    Console.WriteLine($"{row} {engineLayer}");
    char[] layerBits = engineLayer.ToCharArray();
    PartNumber? currentPartNumber = null;
    for ( int position = 0; position < layerBits.Length ; position += 1) {
        char currentChar = layerBits[position];
        var symbolPositions = new List<int>();
        if (currentChar == (char)46) {
            // period
            if (currentPartNumber is not null) {
                currentPartNumber = null;
            }
            continue;
        }
        else if (currentChar >= (char)48 && currentChar <= (char)57) {
            // numeral
            if (currentPartNumber is null) {
                currentPartNumber = new PartNumber() { row=row, position=position };
                partNumbers.Add(currentPartNumber);
            }
            currentPartNumber.numerals.Add(position);
        }
        else {
            // symbol
            if (currentPartNumber is not null) {
                currentPartNumber = null;
            }
            symbolPositions.Add(position);
        }
        foreach ( int symbolPosition in symbolPositions ) {
            // query the current row
            IEnumerable<PartNumber> queryCurrentLayer =
                from pn in partNumbers
                where pn.row == row && 
                    (pn.occupies(symbolPosition-1) || pn.occupies(symbolPosition+1))
                select pn;
            foreach (PartNumber pn in queryCurrentLayer) {
                Console.WriteLine($"PartNumber {pn.number}");
            }
            // after the first row, check the row above for new hits
            // if(row > 0) { 

            // }

        }

    }   
    row += 1;
}

public class PartNumber {
    public required int row { get; set; }
    public required int position { get; set; }
    public List<int> numerals = new List<int>();
    private int _number = -1;
    public int number {
        get {
            IEnumerable<char> mapIntsToChars =
                from n in numerals
                select (char)(n + 48);
            char[] numeralCharacters = mapIntsToChars.ToArray();
            return Int32.Parse(new string(numeralCharacters));
        }
    }

    public bool occupies(int position) {
        return this.numerals.Contains(position);
    }


}