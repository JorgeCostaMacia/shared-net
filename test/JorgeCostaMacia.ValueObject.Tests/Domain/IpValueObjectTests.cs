using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IpValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  999.999.999.999  ", new IpValueObject("  999.999.999.999  ").Value);

    [Fact]
    public void From_Trims()
        => Assert.Equal("192.168.1.1", IpValueObject.From("  192.168.1.1  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("999.999.999.999", IpValueObject.From("999.999.999.999").Value);

    [Fact]
    public void Create_Trims()
        => Assert.Equal("192.168.1.1", IpValueObject.Create("  192.168.1.1  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsIpValueObjectValidationException()
        => Assert.Throws<IpValueObjectValidationException>(() => IpValueObject.Create("999.999.999.999"));
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(IpValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(IpValueObject.FromOrNull("192.168.1.1"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(IpValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(IpValueObject.CreateOrNull("192.168.1.1"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<IpValueObjectValidationException>(() => IpValueObject.CreateOrNull("notanip"));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("192.168.1.1", IpValueObject.From("192.168.1.1").ToString());
}
