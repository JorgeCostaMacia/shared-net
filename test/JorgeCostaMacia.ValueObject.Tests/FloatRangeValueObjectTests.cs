using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class FloatRangeValueObjectTests
{
    [Fact]
    public void Create_FromFloats_SetsStartAndEnd()
    {
        FloatRangeValueObject range = FloatRangeValueObject.Create(1.5f, 9.5f);

        Assert.Equal(1.5f, range.ValueStart.Value);
        Assert.Equal(9.5f, range.ValueEnd.Value);
    }
}
