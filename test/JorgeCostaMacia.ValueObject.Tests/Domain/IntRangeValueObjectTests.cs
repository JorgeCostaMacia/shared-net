using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IntRangeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        IntRangeValueObject range = new IntRangeValueObject(new IntValueObject(10), new IntValueObject(1));

        Assert.Equal(10, range.ValueStart.Value);
        Assert.Equal(1, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        IntRangeValueObject range = IntRangeValueObject.From(1, 5);

        Assert.Equal(1, range.ValueStart.Value);
        Assert.Equal(5, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        IntRangeValueObject range = IntRangeValueObject.From(10, 1);   // start > end, still materializes

        Assert.Equal(10, range.ValueStart.Value);
        Assert.Equal(1, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        IntRangeValueObject range = IntRangeValueObject.Create(1, 5);

        Assert.Equal(1, range.ValueStart.Value);
        Assert.Equal(5, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsIntRangeValueObjectValidationException()
        => Assert.Throws<IntRangeValueObjectValidationException>(() => IntRangeValueObject.Create(10, 1));
}
