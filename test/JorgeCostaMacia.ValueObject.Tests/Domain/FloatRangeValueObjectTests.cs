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
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(FloatRangeValueObject.FromOrNull(null, null));

    // A window needs both bounds, so a half-supplied one gives no window rather than half of one.
    [Fact]
    public void FromOrNull_WithOnlyOneBound_ShortCircuitsToNull()
    {
        Assert.Null(FloatRangeValueObject.FromOrNull(1f, null));
        Assert.Null(FloatRangeValueObject.FromOrNull(null, 5f));
    }

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(FloatRangeValueObject.FromOrNull(1f, 5f));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(FloatRangeValueObject.CreateOrNull(null, null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(FloatRangeValueObject.CreateOrNull(1f, 5f));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<FloatRangeValueObjectValidationException>(() => FloatRangeValueObject.CreateOrNull(5f, 1f));
    // A range prints both bounds joined, not the record dump of the two value objects.
    [Fact]
    public void ToString_ShowsBothBoundsJoined()
        => Assert.Equal(1f.ToString() + " - " + 5f.ToString(), FloatRangeValueObject.From(1f, 5f).ToString());
}
