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

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        Guid id = Guid.NewGuid();
        Guid value = UuidValueObject.Create(id);
        Assert.Equal(id, value);
    }
}
