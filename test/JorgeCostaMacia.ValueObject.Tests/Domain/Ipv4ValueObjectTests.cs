using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class Ipv4ValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  999.999.999.999  ", new Ipv4ValueObject("  999.999.999.999  ").Value);

    [Fact]
    public void From_Trims()
        => Assert.Equal("192.168.1.1", Ipv4ValueObject.From("  192.168.1.1  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("999.999.999.999", Ipv4ValueObject.From("999.999.999.999").Value);

    [Fact]
    public void Create_Trims()
        => Assert.Equal("192.168.1.1", Ipv4ValueObject.Create("  192.168.1.1  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsIpv4ValueObjectValidationException()
        => Assert.Throws<Ipv4ValueObjectValidationException>(() => Ipv4ValueObject.Create("999.999.999.999"));
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(Ipv4ValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(Ipv4ValueObject.FromOrNull("192.168.1.1"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(Ipv4ValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(Ipv4ValueObject.CreateOrNull("192.168.1.1"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<Ipv4ValueObjectValidationException>(() => Ipv4ValueObject.CreateOrNull("notanip"));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("192.168.1.1", Ipv4ValueObject.From("192.168.1.1").ToString());
}
