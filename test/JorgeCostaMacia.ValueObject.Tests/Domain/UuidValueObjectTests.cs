using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UuidValueObjectTests
{
    [Fact]
    public void Create_FromGuid_KeepsValue()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id, UuidValueObject.Create(id).Value);
    }

    [Fact]
    public void Create_FromString_ParsesGuid()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id, UuidValueObject.Create(id.ToString()).Value);
    }
}
