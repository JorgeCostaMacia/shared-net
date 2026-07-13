using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class ByteValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        byte[] bytes = new byte[] { 1, 2, 3 };
        ByteValueObjectConverter<TestByte> converter = new ByteValueObjectConverter<TestByte>();

        Assert.Equal(bytes, (byte[])converter.ConvertToProvider(new TestByte(bytes))!);
        Assert.Equal(bytes, ((TestByte)converter.ConvertFromProvider(bytes)!).Value);
    }

    public record TestByte : ByteValueObject { public TestByte(byte[] value) : base(value) { } }
}
