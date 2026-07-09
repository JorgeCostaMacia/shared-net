using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class FloatRangeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        FloatRangeValueObject range = new FloatRangeValueObject(new FloatValueObject(9.5f), new FloatValueObject(1.5f));

        Assert.Equal(9.5f, range.ValueStart.Value);
        Assert.Equal(1.5f, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        FloatRangeValueObject range = FloatRangeValueObject.From(1.5f, 9.5f);

        Assert.Equal(1.5f, range.ValueStart.Value);
        Assert.Equal(9.5f, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        FloatRangeValueObject range = FloatRangeValueObject.From(9.5f, 1.5f);   // start > end, still materializes

        Assert.Equal(9.5f, range.ValueStart.Value);
        Assert.Equal(1.5f, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        FloatRangeValueObject range = FloatRangeValueObject.Create(1.5f, 9.5f);

        Assert.Equal(1.5f, range.ValueStart.Value);
        Assert.Equal(9.5f, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsFloatRangeValueObjectValidationException()
        => Assert.Throws<FloatRangeValueObjectValidationException>(() => FloatRangeValueObject.Create(9.5f, 1.5f));
}
