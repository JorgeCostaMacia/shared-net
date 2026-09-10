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
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(JsonValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(JsonValueObject.FromOrNull("{}"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(JsonValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(JsonValueObject.CreateOrNull("{}"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<JsonValueObjectValidationException>(() => JsonValueObject.CreateOrNull("notjson"));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("{}", JsonValueObject.From("{}").ToString());
}
