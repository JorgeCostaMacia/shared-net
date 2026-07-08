using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class FloatValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        FloatValueObjectConverter<TestFloat> converter = new();

        Assert.Equal(3.5f, (float)converter.ConvertToProvider(new TestFloat(3.5f))!);
        Assert.Equal(3.5f, ((TestFloat)converter.ConvertFromProvider(3.5f)!).Value);
    }

    public record TestFloat : FloatValueObject { public TestFloat(float value) : base(value) { } }
}
