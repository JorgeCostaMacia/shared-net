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

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesGuid_TrimmingWhitespace()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id, TestUuid.Convert($"  {id}  "));
    }

    [Fact]
    public void Convert_FromInvalidString_Throws()
        => Assert.Throws<FormatException>(() => TestUuid.Convert("not-a-guid"));

    public sealed record TestUuid : UuidValueObject
    {
        public TestUuid(Guid value) : base(value) { }

        public static new Guid Convert(string value) => UuidValueObject.Convert(value);
    }
}
