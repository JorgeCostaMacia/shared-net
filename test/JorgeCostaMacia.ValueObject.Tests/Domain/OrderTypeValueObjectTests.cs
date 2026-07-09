using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderTypeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  asc  ", new OrderTypeValueObject("  asc  ").Value);

    [Fact]
    public void From_TrimsAndUppercases()
        => Assert.Equal("ASC", OrderTypeValueObject.From("  asc  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("UP", OrderTypeValueObject.From("up").Value);

    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("ASC", OrderTypeValueObject.Create("  asc  ").Value);

    [Fact]
    public void Create_KeepsValidDirection()
        => Assert.Equal("DESC", OrderTypeValueObject.Create("DESC").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsOrderTypeValueObjectValidationException()
        => Assert.Throws<OrderTypeValueObjectValidationException>(() => OrderTypeValueObject.Create("UP"));
}
