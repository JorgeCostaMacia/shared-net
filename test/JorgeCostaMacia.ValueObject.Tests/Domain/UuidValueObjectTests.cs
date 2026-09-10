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
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(UuidValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(UuidValueObject.FromOrNull(Guid.NewGuid()));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(UuidValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(UuidValueObject.CreateOrNull(Guid.NewGuid()));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<UuidValueObjectValidationException>(() => UuidValueObject.CreateOrNull(Guid.Empty));
}
