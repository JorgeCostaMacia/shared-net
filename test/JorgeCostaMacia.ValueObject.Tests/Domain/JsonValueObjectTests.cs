using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class JsonValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  notjson  ", new JsonValueObject("  notjson  ").Value);

    [Fact]
    public void From_Trims()
        => Assert.Equal("{\"a\":1}", JsonValueObject.From("  {\"a\":1}  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("notjson", JsonValueObject.From("notjson").Value);

    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal("{\"a\":1}", JsonValueObject.Create("{\"a\":1}").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsJsonValueObjectValidationException()
        => Assert.Throws<JsonValueObjectValidationException>(() => JsonValueObject.Create("notjson"));
}
