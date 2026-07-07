using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class BoolValueObjectTests
{
    [Theory]
    [InlineData("TRUE", true)]
    [InlineData("true", true)]
    [InlineData("1", true)]
    [InlineData("SI", true)]
    [InlineData("YES", true)]
    [InlineData("false", false)]
    [InlineData("0", false)]
    [InlineData("nope", false)]
    public void Create_FromString_ParsesTruthyTokens(string input, bool expected)
        => Assert.Equal(expected, BoolValueObject.Create(input).Value);

    [Fact]
    public void Create_FromNumbers_OnlyOneMapsToTrue()
    {
        Assert.True(BoolValueObject.Create(1).Value);
        Assert.False(BoolValueObject.Create(0).Value);
        Assert.False(BoolValueObject.Create(2).Value);
        Assert.True(BoolValueObject.Create(1L).Value);
        Assert.True(BoolValueObject.Create(1.0).Value);
        Assert.True(BoolValueObject.Create(1m).Value);
    }
}
