using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UuidValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutValidating()
        => Assert.Equal(Guid.Empty, new UuidValueObject(Guid.Empty).Value);

    [Fact]
    public void From_KeepsValue()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id, UuidValueObject.From(id).Value);
    }

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal(Guid.Empty, UuidValueObject.From(Guid.Empty).Value);

    [Fact]
    public void Create_KeepsValue()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id, UuidValueObject.Create(id).Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsUuidValueObjectValidationException()
        => Assert.Throws<UuidValueObjectValidationException>(() => UuidValueObject.Create(Guid.Empty));

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        Guid id = Guid.NewGuid();
        Guid value = UuidValueObject.Create(id);
        Assert.Equal(id, value);
    }
}
