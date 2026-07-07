using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class OrderTypeValueObjectTests
{
    [Fact]
    public void Create_FromString_KeepsValue()
        => Assert.Equal("DESC", OrderTypeValueObject.Create("DESC").Value);
}
