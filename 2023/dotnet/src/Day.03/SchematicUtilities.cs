
public class SchematicUtilities 
{

    public static (List<PartNumber>, List<SchematicSymbol>) ParseSchematicLine(SchematicLine line) 
    {
        var partNumbers =  new List<PartNumber>();
        var symbols = new List<SchematicSymbol>();

        PartNumber? currentPartNumber = null;
        char[] chars = line.text.ToCharArray();
        for ( int position = 0; position < chars.Length ; position += 1)
        {
            char currentChar = chars[position];
            if (currentChar >= '0' && currentChar <= '9') 
            {
                if (currentPartNumber is null) 
                {
                    currentPartNumber = new PartNumber() {
                        row=line.row, 
                        position=position,
                    };
                    partNumbers.Add(currentPartNumber);
                }
                currentPartNumber.numerals.Add(currentChar);
                currentPartNumber.positions.Add(position);
            }
            else  if (currentChar == '.')
            {
                if (currentPartNumber is not null) 
                {
                    currentPartNumber = null;
                }
            }
            else 
            { 
                if (currentPartNumber is not null) 
                {
                    currentPartNumber = null;
                }
                symbols.Add(new SchematicSymbol {
                    row=line.row, 
                    position=position, 
                    glyph=currentChar,
                });
            }
        }
        return (partNumbers, symbols);
    }

    public static List<PartNumber> FindSymbolAdjacentPartNumbers(SchematicLine currentLine, SchematicLine previousLine)
    {
        var symbolAdjacentPartNumbers = new List<PartNumber>();
        (List<PartNumber> parts, List<SchematicSymbol> symbols) currentLineContents  = ParseSchematicLine(currentLine);
        (List<PartNumber> parts, List<SchematicSymbol> symbols) previousLineContents = ParseSchematicLine(previousLine);

        foreach (SchematicSymbol symbol in currentLineContents.symbols)
        {
            foreach (PartNumber pn in currentLineContents.parts)
            {
                if (pn.occupies(symbol.position-1) || pn.occupies(symbol.position+1))
                {
                    if (pn.symbolAdjacent is false) {
                        pn.symbolAdjacent = true;
                        symbolAdjacentPartNumbers.Add(pn);
                    }
                }
            }
            foreach (PartNumber pn in previousLineContents.parts) {
                if (pn.occupies(symbol.position-1) || pn.occupies(symbol.position) || pn.occupies(symbol.position+1))
                {
                    if (pn.symbolAdjacent is false) {
                        pn.symbolAdjacent = true;
                        symbolAdjacentPartNumbers.Add(pn);
                    }
                }
            }
        }

        foreach (SchematicSymbol symbol in previousLineContents.symbols) {
            foreach (PartNumber pn in currentLineContents.parts)
            {
                if (pn.occupies(symbol.position-1) || pn.occupies(symbol.position) || pn.occupies(symbol.position+1))
                {
                    if (pn.symbolAdjacent is false) {
                        pn.symbolAdjacent = true;
                        symbolAdjacentPartNumbers.Add(pn);
                    }
                    continue;
                }
            }
        }

        return symbolAdjacentPartNumbers;
    }

}

public class SchematicSymbol 
{
    public SchematicSymbol() { }
    public required int row;
    public required int position;
    public required char glyph;

    public override string? ToString()
    {
        return $"<{row}:{position}>{glyph}";
    }
}

public class SchematicLine 
{
    public SchematicLine() { }
    public required int row;
    public required string text;
}

public class PartNumber
{
    public required int row { get; set; }
    public required int position { get; set; }
    public List<char> numerals = new List<char>();
    public List<int> positions = new List<int>();
    public bool symbolAdjacent = false;

     public override string? ToString()
    {
        return $"<{row}:{position}>{number}";
    }

    public int number {
        get {
            string result= "";
            foreach (char n in this.numerals) {
                result = $"{result}{n}";
            }
            return Int32.Parse(result);
        }
    }

    public string index {
        get {
            string result= "";
            foreach (int n in this.positions) {
                result = $"{result}.{n}";
            }
            return result;
        }
    }

    public bool occupies(int position) {
        return this.positions.Contains(position);
    }

}
