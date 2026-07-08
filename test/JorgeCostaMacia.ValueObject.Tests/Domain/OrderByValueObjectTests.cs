using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderByValueObjectTests
{
    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("NAME", OrderByValueObject.Create("  name  ").Value);
}
