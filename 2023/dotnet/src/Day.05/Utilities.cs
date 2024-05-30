public static class Utilities {
    public static double MapValue(MapEntry entry, double srcValue) {
        double offset = entry.srcRangeStart + entry.rangeLength - srcValue;
        double mappedValue = entry.dstRangeStart + entry.rangeLength - offset;
        return mappedValue;
    }

    public static double? RangeOverlap(CategoryRange r1, CategoryRange r2)
    {
        double r1start = r1.start;
        double r2start = r2.start;
        double r1end = r1.start + r1.length - 1;
        double r2end = r2.start + r2.length - 1;
        if (r1start >= r2start && r1.start <= r2end)
        {
            return r1start;
        }
        if (r2start >= r1start && r2start <= r1end)
        {
            return r2start;
        }
        if (r1end >= r2start && r1end <= r2end )
        {
            return r2start;
        }
        if (r2end >= r1start && r2end <= r1end )
        {
            return r1start;
        }
        return null;
    }
}

public class CategoryRange {
    
    required public double start {get; set;}
    required public double length {get; set;}
    public override string? ToString()
    {
        return $"{start}:{length}";
    }
}
