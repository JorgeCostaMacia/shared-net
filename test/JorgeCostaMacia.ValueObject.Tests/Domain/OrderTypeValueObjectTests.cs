using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderTypeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  asc  ", new OrderTypeValueObject("  asc  ").Value);

    [Fact]
    public void From_TrimsAndUppercases()
        => Assert.Equal("ASC", OrderTypeValueObject.From("  asc  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("UP", OrderTypeValueObject.From("up").Value);

    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("ASC", OrderTypeValueObject.Create("  asc  ").Value);

    [Fact]
    public void Create_KeepsValidDirection()
        => Assert.Equal("DESC", OrderTypeValueObject.Create("DESC").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsOrderTypeValueObjectValidationException()
        => Assert.Throws<OrderTypeValueObjectValidationException>(() => OrderTypeValueObject.Create("UP"));
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(OrderTypeValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(OrderTypeValueObject.FromOrNull("ASC"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(OrderTypeValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(OrderTypeValueObject.CreateOrNull("ASC"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<OrderTypeValueObjectValidationException>(() => OrderTypeValueObject.CreateOrNull("SIDEWAYS"));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("ASC", OrderTypeValueObject.From("ASC").ToString());
}
