using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class GroupByValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  category  ", new GroupByValueObject("  category  ").Value);

    [Fact]
    public void From_TrimsAndUppercases()
        => Assert.Equal("CATEGORY", GroupByValueObject.From("  category  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("", GroupByValueObject.From("").Value);

    [Fact]
    public void Create_TrimsAndUppercases()
        => Assert.Equal("CATEGORY", GroupByValueObject.Create("  category  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsGroupByValueObjectValidationException()
        => Assert.Throws<GroupByValueObjectValidationException>(() => GroupByValueObject.Create(""));
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(GroupByValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(GroupByValueObject.FromOrNull("name"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(GroupByValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(GroupByValueObject.CreateOrNull("name"));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<GroupByValueObjectValidationException>(() => GroupByValueObject.CreateOrNull(""));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("NAME", GroupByValueObject.From("name").ToString());   // Convert upper-cases: these are SQL fragments
}
