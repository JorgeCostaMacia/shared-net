using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class GroupByValueObjectTests
{
    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("CATEGORY", GroupByValueObject.Create("  category  ").Value);
}
