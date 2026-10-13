using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class UriValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        UriValueObjectConverter<TestUri> converter = new UriValueObjectConverter<TestUri>();

        Assert.Equal("https://example.com/a?b=1", (string)converter.ConvertToProvider(new TestUri(new Uri("https://example.com/a?b=1")))!);
        Assert.Equal(new Uri("https://example.com/a?b=1"), ((TestUri)converter.ConvertFromProvider("https://example.com/a?b=1")!).Value);
    }

    // The column keeps the text as it arrived, not Uri's normalized form.
    [Fact]
    public void ToProvider_StoresTheOriginalText()
        => Assert.Equal("https://EXAMPLE.com", (string)new UriValueObjectConverter<TestUri>().ConvertToProvider(new TestUri(new Uri("https://EXAMPLE.com")))!);

    // Rehydration does not validate: a stored relative URI comes back as it was.
    [Fact]
    public void FromProvider_RehydratesWithoutValidating()
        => Assert.False(((TestUri)new UriValueObjectConverter<TestUri>().ConvertFromProvider("relative/path")!).Value.IsAbsoluteUri);

    public record TestUri : UriValueObject { public TestUri(Uri value) : base(value) { } }
}
