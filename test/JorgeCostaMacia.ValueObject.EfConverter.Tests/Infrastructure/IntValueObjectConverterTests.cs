using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class IntValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        IntValueObjectConverter<TestInt> converter = new IntValueObjectConverter<TestInt>();

        Assert.Equal(5, (int)converter.ConvertToProvider(new TestInt(5))!);
        Assert.Equal(5, ((TestInt)converter.ConvertFromProvider(5)!).Value);
    }

    public record TestInt : IntValueObject { public TestInt(int value) : base(value) { } }
}
