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
}
