using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class StringValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        StringValueObjectConverter<TestString> converter = new();

        Assert.Equal("hi", (string)converter.ConvertToProvider(new TestString("hi"))!);
        Assert.Equal("hi", ((TestString)converter.ConvertFromProvider("hi")!).Value);
    }

    public record TestString : StringValueObject { public TestString(string value) : base(value) { } }
}
