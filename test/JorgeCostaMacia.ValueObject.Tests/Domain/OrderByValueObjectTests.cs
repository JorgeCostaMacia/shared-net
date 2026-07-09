using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderByValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  name  ", new OrderByValueObject("  name  ").Value);

    [Fact]
    public void From_TrimsAndUppercases()
        => Assert.Equal("NAME", OrderByValueObject.From("  name  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("", OrderByValueObject.From("").Value);

    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("NAME", OrderByValueObject.Create("  name  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsOrderByValueObjectValidationException()
        => Assert.Throws<OrderByValueObjectValidationException>(() => OrderByValueObject.Create(""));
}
