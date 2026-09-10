using System.Globalization;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class StringValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizing()
        => Assert.Equal("  hi  ", new StringValueObject("  hi  ").Value);

    [Fact]
    public void From_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.From("  hi  ").Value);

    [Fact]
    public void Create_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.Create("  hi  ").Value);

    // The base validator has no rules, so Create never throws — not even on empty input.
    [Fact]
    public void Create_BaseHasNoRules_DoesNotThrowOnEmpty()
        => Assert.Equal("", StringValueObject.Create("").Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        string value = StringValueObject.Create("hi");
        Assert.Equal("hi", value);
    }

    [Fact]
    public void Equality_SameTypeSameValue_AreEqual()
        => Assert.Equal(EmailValueObject.Create("a@b.com"), EmailValueObject.Create("a@b.com"));

    [Fact]
    public void Equality_DifferentVoTypesSameValue_AreNotEqual()
        // Record equality includes the runtime type, so two different VO types with the same value differ.
        => Assert.NotEqual<StringValueObject>(EmailValueObject.From("abc"), UrlValueObject.From("abc"));

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromOtherTypes_FunnelsThroughRoot()
    {
        Assert.Equal("42", TestString.Convert(42));
        Assert.Equal("True", TestString.Convert(true));
        Assert.Equal("9", TestString.Convert(9L));
    }

    [Fact]
    public void Convert_FromFloatingPoint_UsesInvariantCulture()
    {
        Assert.Equal("2.5", TestString.Convert(2.5f));
        Assert.Equal("2.5", TestString.Convert(2.5d));
        Assert.Equal("2.5", TestString.Convert(2.5m));
    }

    // The integral overloads carry no culture of their own, so a culture whose negative sign is not the
    // ASCII hyphen (sv-SE uses U+2212) is what tells an invariant conversion from an ambient one.
    [Fact]
    public void Convert_FromNegativeIntegers_UsesInvariantCulture()
    {
        CultureInfo original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");

            Assert.Equal("-42", TestString.Convert(-42));
            Assert.Equal("-42", TestString.Convert(-42L));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    // A landing store keeps what it was given: the round-trip format is the only one that preserves
    // sub-second precision and the Kind, which the general invariant format silently drops.
    [Fact]
    public void Convert_FromDateTime_RoundTripsExactly()
    {
        DateTime value = new DateTime(2026, 9, 9, 14, 30, 15, 123, DateTimeKind.Utc).AddTicks(4567);

        string converted = TestString.Convert(value);
        DateTime parsed = DateTime.Parse(converted, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

        Assert.Equal(value, parsed);
        Assert.Equal(value.Kind, parsed.Kind);
    }

    [Fact]
    public void Convert_FromGuid_UsesGuidString()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id.ToString(), TestString.Convert(id));
    }

    public sealed record TestString : StringValueObject
    {
        public TestString(string value) : base(value) { }

        public static new string Convert(int value) => StringValueObject.Convert(value);

        public static new string Convert(float value) => StringValueObject.Convert(value);

        public static new string Convert(decimal value) => StringValueObject.Convert(value);

        public static new string Convert(bool value) => StringValueObject.Convert(value);

        public static new string Convert(long value) => StringValueObject.Convert(value);

        public static new string Convert(double value) => StringValueObject.Convert(value);

        public static new string Convert(Guid value) => StringValueObject.Convert(value);

        /// <summary>Exposes the DateTime overload, the one conversion the probe was missing.</summary>
        public static new string Convert(DateTime value) => StringValueObject.Convert(value);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(StringValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(StringValueObject.FromOrNull("hi"));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(StringValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(StringValueObject.CreateOrNull("hi"));
}
