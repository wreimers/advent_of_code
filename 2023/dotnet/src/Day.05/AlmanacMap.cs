
public class Seed
{
    required public double rangeStart {get; set;}
    required public double rangeLength {get; set;}
    public override string? ToString()
    {
        return $"{rangeStart}:{rangeLength}";
    }
}

public class MapEntry
{
    public required double row {get; set;}
    public required double dstRangeStart {get; set;}
    public required double srcRangeStart {get; set;}
    public required double rangeLength {get; set;}
    public override string? ToString()
    {
        return $"<{row}>{dstRangeStart},{srcRangeStart},{rangeLength}";
    }
}

public class AlmanacMap : IEquatable<AlmanacMap>
{
    public required double row {get; set;}
    public required string srcCategory {get; set;}
    public required string dstCategory {get; set;}
    public List<MapEntry> entries = new List<MapEntry>();
    public override string? ToString()
    {
        return $"<{row}>{srcCategory}.{dstCategory}";
    }
    public bool Equals(AlmanacMap? other)
    {
        if (other is null) {
            return false;
        }
        return $"{row}.{srcCategory}.{dstCategory}".ToLower() == $"{other.row}.{other.srcCategory}.{other.dstCategory}".ToLower();
    }
}
