using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Day15
{
    internal class Program
    {
        private static string DATA_FILE = "var/day_15/sample.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 15");
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is null) { return; }
            Console.WriteLine($"rawLine:{rawLine}");

            LensBox[] boxes = new LensBox[256];
            for (int i = 0; i < 256; i += 1)
            {
                boxes[i] = new LensBox();
            }

            char[] splitters = [',',];
            string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            string pattern = @"^([a-z]+)([\=\-]+)(\d*)$";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);

            foreach (string t in tokens)
            {
                Match m = rg.Match(t);
                if (!m.Success)
                {
                    throw new Exception("REGEX DIDN'T MATCH");
                }
                var label = m.Groups[1].ToString();
                var operation = m.Groups[2].ToString()[0];
                var focalLength = m.Groups[3].ToString();
                Console.WriteLine($"label:{label} operation:{operation} focalLength:{focalLength}");
                int boxNumber = calculateHASH(label);
                LensBox box = boxes[boxNumber];
                switch (operation)
                {
                    case '=':
                        var lens = new Lens { label = label, focalLength = Int32.Parse(focalLength) };
                        box.Add(lens);
                        break;
                    case '-':
                        box.Remove(label);
                        break;
                    default:
                        throw new Exception("ILLEGAL OPERATION");
                }
            }

            int grandTotal = 0;
            for (int boxNumber = 0; boxNumber < 256; boxNumber += 1)
            {
                LensBox box = boxes[boxNumber];
                for (int lensSlot = 0; lensSlot < box.slotCount; lensSlot += 1)
                {
                    Lens lens = box.lenses[lensSlot];
                    int focusingPower = boxNumber + 1;
                    focusingPower *= lensSlot + 1;
                    focusingPower *= lens.focalLength;
                    grandTotal += focusingPower;
                    Console.WriteLine($"boxNumber:{boxNumber} lensSlot:{lensSlot} label:{lens.label} focalLength:{lens.focalLength} focusingPower:{focusingPower}");
                }
            }
            Console.WriteLine($"grandTotal:{grandTotal}");
        }

        /* part 1
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2023 Day 15");
            using StreamReader reader = new(DATA_FILE);
            string? rawLine = reader.ReadLine();
            if (rawLine is not null)
            {
                Console.WriteLine($"rawLine:{rawLine}");
                char[] splitters = [',',];
                string[] tokens = rawLine.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                int grandTotal = 0;
                foreach (string t in tokens)
                {
                    int value = calculateHASH(t);
                    grandTotal += value;
                }
                Console.WriteLine($"grandTotal:{grandTotal}");
            }

        }
        */

        public static int calculateHASH(string word)
        {
            int value = 0;
            for (int i = 0; i < word.Length; i += 1)
            {
                char currentChar = word[i];
                int charValue = currentChar;
                value += charValue;
                value *= 17;
                value %= 256;
            }
            // Console.WriteLine($"calculateHASH word:{word} value:{value}");
            return value;
        }
    }
}

public class LensBox
{
    private List<Lens> _lenses = new List<Lens>();
    public void Add(Lens lens)
    {
        Lens? updateLens = null;
        foreach (Lens l in _lenses)
        {
            if (l.label == lens.label)
            {
                updateLens = l;
                break;
            }
        }
        if (updateLens is not null)
        {
            updateLens.focalLength = lens.focalLength;
        }
        else
        {
            _lenses.Add(lens);
        }
    }
    public void Remove(string label)
    {
        Lens? removeLens = null;
        foreach (Lens l in _lenses)
        {
            if (l.label == label)
            {
                removeLens = l;
                break;
            }
        }
        if (removeLens is not null)
        {
            _lenses.Remove(removeLens);
        }
    }
    public int slotCount
    {
        get
        {
            return _lenses.Count;
        }
    }
    public List<Lens> lenses
    {
        get
        {
            return _lenses;
        }
    }
}

public class Lens
{
    public required string label { get; set; }
    public required int focalLength { get; set; }
}