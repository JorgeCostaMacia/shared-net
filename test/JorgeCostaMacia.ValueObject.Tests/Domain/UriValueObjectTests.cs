using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UriValueObjectTests
{
    private static readonly Uri _uri = new Uri("https://example.com/api/v1?x=1");

    [Fact]
    public void Ctor_HydratesRaw_WithoutValidating()
        => Assert.Equal("relative/path", new UriValueObject(new Uri("relative/path", UriKind.Relative)).Value.OriginalString);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(_uri, UriValueObject.From(_uri).Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Null(UriValueObject.From(null!).Value);

    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(_uri, UriValueObject.Create(_uri).Value);

    [Fact]
    public void Create_OnRelative_KeepsIt()
        => Assert.Equal("relative/path", UriValueObject.Create(new Uri("relative/path", UriKind.Relative)).Value.OriginalString);

    [Fact]
    public void Create_OnNull_ThrowsUriValueObjectValidationException()
        => Assert.Throws<UriValueObjectValidationException>(() => UriValueObject.Create(null!));

    // The base takes any scheme: narrowing it is a derived Value Object's job.
    [Theory]
    [InlineData("https://example.com")]
    [InlineData("ftp://files.example.com/a.txt")]
    [InlineData("mailto:someone@example.com")]
    [InlineData("urn:isbn:0451450523")]
    public void Create_OnAnyAbsoluteScheme_Passes(string value)
        => Assert.Equal(value, UriValueObject.Create(new Uri(value)).Value.OriginalString);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        Uri value = UriValueObject.Create(_uri);
        Assert.Equal(_uri, value);
    }

    // ToString reads back the text as it arrived, not Uri's normalized form (which would add a trailing slash here).
    [Fact]
    public void ToString_ShowsTheOriginalText()
        => Assert.Equal("https://EXAMPLE.com", UriValueObject.From(new Uri("https://EXAMPLE.com")).ToString());

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesUri_TrimmingWhitespace()
        => Assert.Equal(_uri, TestUri.Convert("  https://example.com/api/v1?x=1  "));

    [Fact]
    public void Convert_FromString_KeepsARelativeUri()
        => Assert.False(TestUri.Convert("relative/path").IsAbsoluteUri);

    [Fact]
    public void Convert_FromInvalidString_Throws()
        => Assert.Throws<UriFormatException>(() => TestUri.Convert("http://"));

    public sealed record TestUri : UriValueObject
    {
        public TestUri(Uri value) : base(value) { }

        public static new Uri Convert(string value) => UriValueObject.Convert(value);
    }

    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(UriValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(UriValueObject.FromOrNull(_uri));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(UriValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(UriValueObject.CreateOrNull(_uri));

    // The base has no invalid non-null URI to supply: a relative one is as valid as an absolute one.
    [Fact]
    public void CreateOrNull_WithARelativeValue_ReturnsIt()
        => Assert.NotNull(UriValueObject.CreateOrNull(new Uri("relative/path", UriKind.Relative)));
}
