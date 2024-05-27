namespace SchematicUtilitiesShould;

using Xunit;

public class SchematicUtilitiesShould {
    [Fact]
    public void ParseSchematicLine_EmptyLine_EmptyTuple()
    {
        // Given
        SchematicLine schematicLine = new SchematicLine {
            text = "",
            row = 0,
        };
        // When
        var result = SchematicUtilities.ParseSchematicLine(schematicLine);
        // Then
        Assert.Empty(result.parts);
        Assert.Empty(result.symbols);
        Assert.Empty(result.gears);
    }

    [Fact]
    public void ParseSchematicLine_OneSymbol_SingleSecondItem()
    {
        // Given
        SchematicLine schematicLine = new SchematicLine { text = "...*......", row = 0, };
        // When
        var result = SchematicUtilities.ParseSchematicLine(schematicLine);
        // Then
        Assert.Empty(result.parts);
        Assert.Single(result.symbols);
        Assert.Single(result.gears);
    }

    [Fact]
    public void ParseSchematicLine_OnePartNumber_SingleFirstItem()
    {
        // Given
        SchematicLine schematicLine = new SchematicLine {
            text = "..123.....",
            row = 0,
        };
        // When
        var result = SchematicUtilities.ParseSchematicLine(schematicLine);
        // Then
        Assert.Single(result.parts);
        Assert.Empty(result.symbols);
        Assert.Empty(result.gears);
    }

    [Fact]
    public void ParseSchematicLine_TwoPartNumbers_TwoFirstItem()
    {
        // Given
        SchematicLine schematicLine = new SchematicLine {
            text = "..123.456.",
            row = 0,
        };
        // When
        var result = SchematicUtilities.ParseSchematicLine(schematicLine);
        // Then
        Assert.Equal(2, result.parts.Count);
        Assert.Empty(result.symbols);
        Assert.Empty(result.gears);
    }

    [Fact]
    public void ParseSchematicLine_TwoPartNumbersOneSymbol_TwoFirstItemOneSecondItem()
    {
        // Given
        SchematicLine schematicLine = new SchematicLine {
            text = "..123#456.",
            row = 0,
        };
        // When
        var result = SchematicUtilities.ParseSchematicLine(schematicLine);
        // Then
        Assert.Equal(2, result.parts.Count);
        Assert.Single(result.symbols);
        Assert.Empty(result.gears);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_NoLines_Empty()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "",           row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "",           row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Empty(testList);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_CurrentLineOnly_Empty()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "",           row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "467..114..", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Empty(testList);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_NoHits_Empty()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "467..114..", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "..........", row = 1,  };
        
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Empty(testList);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DiagonalPreviousLineHit_Single()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "467..114..", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "...*......", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Single(testList);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DiagonalPreviousLineDoubleHit_TwoPartNumbers()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "467.114...", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "...*......", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_AbovePreviousLineHit_Single()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "...*......", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "44.45.114.", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Single(testList);
        Assert.Equal(45, testList[0].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_BelowPreviousLineHit_Single()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "44.45.114.", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "...*......", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Single(testList);
        Assert.Equal(45, testList[0].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitSplitBySymbol_TwoPartNumbers()
    {
        // Given
        SchematicLine previousLine  = new SchematicLine { text = "..........", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = ".467^114..", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
        Assert.Equal(467, testList[0].number);
        Assert.Equal(114, testList[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitSplitAndHeadedBySymbol_TwoPartNumbers()
    {
        // Given
        SchematicLine previousLine  = new SchematicLine { text = "..........", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "^467^114..", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
        Assert.Equal(467, testList[0].number);
        Assert.Equal(114, testList[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitSplitAndTailedBySymbol_TwoPartNumbers()
    {
        // Given
        SchematicLine previousLine  = new SchematicLine { text = "..........", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = ".467^114^.", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
        Assert.Equal(467, testList[0].number);
        Assert.Equal(114, testList[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitSplitAndHeadedByAndTailedBySymbol_TwoPartNumbers()
    {
        // Given
        SchematicLine previousLine  = new SchematicLine { text = "..........", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "^467^114^.", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
        Assert.Equal(467, testList[0].number);
        Assert.Equal(114, testList[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitOneSameLineOneDiagonalAfterPreviousLine_TwoPartNumbers()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = ".....114..", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = ".467^.....", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
        Assert.Equal(467, testList[0].number);
        Assert.Equal(114, testList[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitOneSameLineOneDiagonalBeforePreviousLine_TwoPartNumbers()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = ".114......", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = ".467^.....", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        // Then
        Assert.Equal(2, testList.Count);
        Assert.Equal(467, testList[0].number);
        Assert.Equal(114, testList[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_DoubleHitOneSameLineOneDiagonalBeforeNextLine_TwoPartNumbers()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = ".114^.....", row = 0, };
        SchematicLine currentLine   = new SchematicLine { text = ".467......", row = 1, };
        // When
        List<PartNumber> testList1 = SchematicUtilities.FindSymbolAdjacentPartNumbers(previousLine, macguffinLine);
        List<PartNumber> testList2 = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        testList1.AddRange(testList2);
        // Then
        Assert.Equal(2, testList1.Count);
        Assert.Equal(114, testList1[0].number);
        Assert.Equal(467, testList1[1].number);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_TripleHitTwoSameLineOneDiagonalBeforePreviousLine_ThreePartNumbers()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",           row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = ".114......", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = ".467^467..", row = 1,  };
        // When
        List<PartNumber> testList1 = SchematicUtilities.FindSymbolAdjacentPartNumbers(previousLine, macguffinLine);
        List<PartNumber> testList2 = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        testList1.AddRange(testList2);
        // Then
        Assert.Equal(3, testList1.Count);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_TwoSymbolsTwoHitsOnePartNumber_Single()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",            row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "..*.*......", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "..467......", row = 1,  };
        // When
        List<PartNumber> testList1 = SchematicUtilities.FindSymbolAdjacentPartNumbers(previousLine, macguffinLine);
        List<PartNumber> testList2 = SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine);
        testList1.AddRange(testList2);
        // Then
        Assert.Single(testList1);
    }

    [Fact]
    public void FindSymbolAdjacentPartNumbers_TwoSymbolsTwoHitsOnePartNumberReveresed_Single()
    {
        // Given
        SchematicLine macguffinLine = new SchematicLine { text = "",            row = -1, };
        SchematicLine previousLine  = new SchematicLine { text = "..467......", row = 0,  };
        SchematicLine currentLine   = new SchematicLine { text = "..*.*......", row = 1,  };
        // When
        List<PartNumber> testList = SchematicUtilities.FindSymbolAdjacentPartNumbers(previousLine, macguffinLine);
        testList.AddRange(
            SchematicUtilities.FindSymbolAdjacentPartNumbers(currentLine, previousLine)
        );
        // Then
        Assert.Single(testList);
    }

}
