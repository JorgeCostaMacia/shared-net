using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class JsonValueObjectTests
{
    [Fact]
    public void Create_FromString_KeepsValue()
        => Assert.Equal("{\"a\":1}", JsonValueObject.Create("{\"a\":1}").Value);
}
