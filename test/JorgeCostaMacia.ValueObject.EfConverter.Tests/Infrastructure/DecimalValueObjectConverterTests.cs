using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class DecimalValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        DecimalValueObjectConverter<TestDecimal> converter = new();

        Assert.Equal(1.5m, (decimal)converter.ConvertToProvider(new TestDecimal(1.5m))!);
        Assert.Equal(1.5m, ((TestDecimal)converter.ConvertFromProvider(1.5m)!).Value);
    }

    public record TestDecimal : DecimalValueObject { public TestDecimal(decimal value) : base(value) { } }
}
