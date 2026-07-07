using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class LongValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        LongValueObjectConverter<TestLong> converter = new();

        Assert.Equal(9L, (long)converter.ConvertToProvider(new TestLong(9L))!);
        Assert.Equal(9L, ((TestLong)converter.ConvertFromProvider(9L)!).Value);
    }

    public record TestLong : LongValueObject { public TestLong(long value) : base(value) { } }
}
