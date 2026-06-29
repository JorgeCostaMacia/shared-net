using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class UrlValueObjectTests
{
    [Fact]
    public void Create_TrimsViaStringBase()
        => Assert.Equal("https://example.com", UrlValueObject.Create("  https://example.com  ").Value);
}
