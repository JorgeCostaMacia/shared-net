using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderByValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  name  ", new OrderByValueObject("  name  ").Value);

    [Fact]
    public void From_TrimsAndUppercases()
        => Assert.Equal("NAME", OrderByValueObject.From("  name  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("", OrderByValueObject.From("").Value);

    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("NAME", OrderByValueObject.Create("  name  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsOrderByValueObjectValidationException()
        => Assert.Throws<OrderByValueObjectValidationException>(() => OrderByValueObject.Create(""));
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(OrderByValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(OrderByValueObject.FromOrNull("name"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(OrderByValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(OrderByValueObject.CreateOrNull("name"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<OrderByValueObjectValidationException>(() => OrderByValueObject.CreateOrNull(""));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("NAME", OrderByValueObject.From("name").ToString());   // Convert upper-cases: these are SQL fragments
}
