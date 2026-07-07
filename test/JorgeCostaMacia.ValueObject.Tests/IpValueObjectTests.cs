using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class IpValueObjectTests
{
    [Fact]
    public void Create_FromString_TrimsViaStringBase()
        => Assert.Equal("192.168.1.1", IpValueObject.Create("  192.168.1.1  ").Value);
}
