using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class IntRangeValueObjectTests
{
    [Fact]
    public void Create_FromInts_SetsStartAndEnd()
    {
        IntRangeValueObject range = IntRangeValueObject.Create(1, 5);

        Assert.Equal(1, range.ValueStart.Value);
        Assert.Equal(5, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_FromStrings_Parses()
    {
        IntRangeValueObject range = IntRangeValueObject.Create("1", "5");

        Assert.Equal(1, range.ValueStart.Value);
        Assert.Equal(5, range.ValueEnd.Value);
    }
}
