using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class OrderByValueObjectTests
{
    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("NAME", OrderByValueObject.Create("  name  ").Value);
}
