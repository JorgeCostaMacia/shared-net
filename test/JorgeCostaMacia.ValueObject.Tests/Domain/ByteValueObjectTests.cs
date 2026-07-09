using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class ByteValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
    {
        byte[] data = { 1, 2, 3 };
        Assert.Same(data, new ByteValueObject(data).Value);
    }

    [Fact]
    public void From_KeepsValue()
    {
        byte[] data = { 1, 2, 3 };
        Assert.Equal(data, ByteValueObject.From(data).Value);
    }

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
    {
        byte[] data = { 1, 2, 3 };
        Assert.Equal(data, ByteValueObject.Create(data).Value);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        byte[] data = { 1, 2, 3 };
        byte[] value = ByteValueObject.Create(data);
        Assert.Equal(data, value);
    }

    [Fact]
    public void ToString_DecodesBytesAsUtf8()
        => Assert.Equal("hi", ByteValueObject.Create(new byte[] { 104, 105 }).ToString());
}
