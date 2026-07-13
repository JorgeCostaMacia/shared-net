using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class DoubleValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        DoubleValueObjectConverter<TestDouble> converter = new DoubleValueObjectConverter<TestDouble>();

        Assert.Equal(2.5d, (double)converter.ConvertToProvider(new TestDouble(2.5d))!);
        Assert.Equal(2.5d, ((TestDouble)converter.ConvertFromProvider(2.5d)!).Value);
    }

    public record TestDouble : DoubleValueObject { public TestDouble(double value) : base(value) { } }
}
