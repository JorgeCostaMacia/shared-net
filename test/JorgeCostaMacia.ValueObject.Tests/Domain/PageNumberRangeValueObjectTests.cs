using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberRangeValueObjectTests
{
    [Fact]
    public void Create_FromInts_SetsStartAndEnd()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.Create(2, 10);

        Assert.Equal(2, range.ValueStart.Value);
        Assert.Equal(10, range.ValueEnd.Value);
    }
}
