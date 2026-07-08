using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class EmailValueObjectTests
{
    [Fact]
    public void Create_TrimsViaStringBase()
        => Assert.Equal("user@host.com", EmailValueObject.Create("  user@host.com  ").Value);
}
