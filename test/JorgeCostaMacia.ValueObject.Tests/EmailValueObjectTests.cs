using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class EmailValueObjectTests
{
    [Fact]
    public void Create_TrimsViaStringBase()
        => Assert.Equal("user@host.com", EmailValueObject.Create("  user@host.com  ").Value);
}
