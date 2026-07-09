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
}
