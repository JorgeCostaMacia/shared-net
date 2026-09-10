using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UrlValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  notaurl  ", new UrlValueObject("  notaurl  ").Value);

    [Fact]
    public void From_Trims()
        => Assert.Equal("https://example.com", UrlValueObject.From("  https://example.com  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("notaurl", UrlValueObject.From("notaurl").Value);

    [Fact]
    public void Create_Trims()
        => Assert.Equal("https://example.com", UrlValueObject.Create("  https://example.com  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsUrlValueObjectValidationException()
        => Assert.Throws<UrlValueObjectValidationException>(() => UrlValueObject.Create("notaurl"));

    [Fact]
    public void Create_OnEmpty_ReportsAllFailuresInOneException()
    {
        // Empty violates the three rules (NotEmpty + MinimumLength + absolute-URI check) — one exception, the complete failure list.
        UrlValueObjectValidationException exception =
            Assert.Throws<UrlValueObjectValidationException>(() => UrlValueObject.Create(""));

        Assert.Equal(3, exception.Validations.Count);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(UrlValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(UrlValueObject.FromOrNull("https://a.com"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(UrlValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(UrlValueObject.CreateOrNull("https://a.com"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<UrlValueObjectValidationException>(() => UrlValueObject.CreateOrNull("notaurl"));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("https://a.com", UrlValueObject.From("https://a.com").ToString());
}
