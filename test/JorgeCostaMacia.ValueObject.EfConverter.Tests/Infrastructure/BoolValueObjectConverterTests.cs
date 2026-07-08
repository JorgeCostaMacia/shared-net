using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class BoolValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        BoolValueObjectConverter<TestBool> converter = new();

        Assert.True((bool)converter.ConvertToProvider(new TestBool(true))!);
        Assert.True(((TestBool)converter.ConvertFromProvider(true)!).Value);
    }

    public record TestBool : BoolValueObject { public TestBool(bool value) : base(value) { } }
}
