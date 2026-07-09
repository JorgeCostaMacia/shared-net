using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberRangeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        PageNumberRangeValueObject range = new PageNumberRangeValueObject(new PageNumberValueObject(0), new PageNumberValueObject(-1));

        Assert.Equal(0, range.ValueStart.Value);
        Assert.Equal(-1, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.From(2, 10);

        Assert.Equal(2, range.ValueStart.Value);
        Assert.Equal(10, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.From(0, -1);   // invalid pages, still materializes

        Assert.Equal(0, range.ValueStart.Value);
        Assert.Equal(-1, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.Create(2, 10);

        Assert.Equal(2, range.ValueStart.Value);
        Assert.Equal(10, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsPageNumberRangeValueObjectValidationException()
        => Assert.Throws<PageNumberRangeValueObjectValidationException>(() => PageNumberRangeValueObject.Create(10, 1));

    [Fact]
    public void Create_OnInvalid_ReportsAllFailuresInOneException()
    {
        // (0, -1) violates the part rules (both pages must be > 0) and the range invariant in both
        // directions — ONE composed pass, one exception, the complete failure list.
        PageNumberRangeValueObjectValidationException exception =
            Assert.Throws<PageNumberRangeValueObjectValidationException>(() => PageNumberRangeValueObject.Create(0, -1));

        Assert.Equal(4, exception.Validations.Count);
        Assert.Contains(exception.Validations, v => v.PropertyName == "ValueStart.Value");
        Assert.Contains(exception.Validations, v => v.PropertyName == "ValueEnd.Value");
    }
}
