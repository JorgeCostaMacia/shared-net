using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class GroupByValueObjectTests
{
    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("CATEGORY", GroupByValueObject.Create("  category  ").Value);
}
