using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests;

public class ValueObjectConverterTests
{
    [Fact]
    public void Int_RoundTrips()
    {
        IntValueObjectConverter<TestInt> converter = new();

        Assert.Equal(5, (int)converter.ConvertToProvider(new TestInt(5))!);
        Assert.Equal(5, ((TestInt)converter.ConvertFromProvider(5)!).Value);
    }

    [Fact]
    public void Long_RoundTrips()
    {
        LongValueObjectConverter<TestLong> converter = new();

        Assert.Equal(9L, (long)converter.ConvertToProvider(new TestLong(9L))!);
        Assert.Equal(9L, ((TestLong)converter.ConvertFromProvider(9L)!).Value);
    }

    [Fact]
    public void Decimal_RoundTrips()
    {
        DecimalValueObjectConverter<TestDecimal> converter = new();

        Assert.Equal(1.5m, (decimal)converter.ConvertToProvider(new TestDecimal(1.5m))!);
        Assert.Equal(1.5m, ((TestDecimal)converter.ConvertFromProvider(1.5m)!).Value);
    }

    [Fact]
    public void Double_RoundTrips()
    {
        DoubleValueObjectConverter<TestDouble> converter = new();

        Assert.Equal(2.5d, (double)converter.ConvertToProvider(new TestDouble(2.5d))!);
        Assert.Equal(2.5d, ((TestDouble)converter.ConvertFromProvider(2.5d)!).Value);
    }

    [Fact]
    public void Float_RoundTrips()
    {
        FloatValueObjectConverter<TestFloat> converter = new();

        Assert.Equal(3.5f, (float)converter.ConvertToProvider(new TestFloat(3.5f))!);
        Assert.Equal(3.5f, ((TestFloat)converter.ConvertFromProvider(3.5f)!).Value);
    }

    [Fact]
    public void Bool_RoundTrips()
    {
        BoolValueObjectConverter<TestBool> converter = new();

        Assert.True((bool)converter.ConvertToProvider(new TestBool(true))!);
        Assert.True(((TestBool)converter.ConvertFromProvider(true)!).Value);
    }

    [Fact]
    public void String_RoundTrips()
    {
        StringValueObjectConverter<TestString> converter = new();

        Assert.Equal("hi", (string)converter.ConvertToProvider(new TestString("hi"))!);
        Assert.Equal("hi", ((TestString)converter.ConvertFromProvider("hi")!).Value);
    }

    [Fact]
    public void DateTime_RoundTrips()
    {
        DateTime now = new(2026, 7, 7, 12, 0, 0, DateTimeKind.Utc);
        DateTimeValueObjectConverter<TestDateTime> converter = new();

        Assert.Equal(now, (DateTime)converter.ConvertToProvider(new TestDateTime(now))!);
        Assert.Equal(now, ((TestDateTime)converter.ConvertFromProvider(now)!).Value);
    }

    [Fact]
    public void Uuid_RoundTrips()
    {
        Guid id = Guid.NewGuid();
        UuidValueObjectConverter<TestUuid> converter = new();

        Assert.Equal(id, (Guid)converter.ConvertToProvider(new TestUuid(id))!);
        Assert.Equal(id, ((TestUuid)converter.ConvertFromProvider(id)!).Value);
    }

    [Fact]
    public void Byte_RoundTrips()
    {
        byte[] bytes = [1, 2, 3];
        ByteValueObjectConverter<TestByte> converter = new();

        Assert.Equal(bytes, (byte[])converter.ConvertToProvider(new TestByte(bytes))!);
        Assert.Equal(bytes, ((TestByte)converter.ConvertFromProvider(bytes)!).Value);
    }

    [Fact]
    public void FromProvider_CallsTheConstructor_NotCreate()
    {
        IntValueObjectConverter<TestCreateThrows> converter = new();

        // Create throws; if the converter used it this would blow up. It uses the constructor instead.
        Assert.Equal(7, ((TestCreateThrows)converter.ConvertFromProvider(7)!).Value);
    }

    [Fact]
    public void FromProvider_UsesNonPublicConstructor()
    {
        StringValueObjectConverter<TestPrivateCtor> converter = new();

        Assert.Equal("x", ((TestPrivateCtor)converter.ConvertFromProvider("x")!).Value);
    }

    public record TestInt : IntValueObject { public TestInt(int value) : base(value) { } }
    public record TestLong : LongValueObject { public TestLong(long value) : base(value) { } }
    public record TestDecimal : DecimalValueObject { public TestDecimal(decimal value) : base(value) { } }
    public record TestDouble : DoubleValueObject { public TestDouble(double value) : base(value) { } }
    public record TestFloat : FloatValueObject { public TestFloat(float value) : base(value) { } }
    public record TestBool : BoolValueObject { public TestBool(bool value) : base(value) { } }
    public record TestString : StringValueObject { public TestString(string value) : base(value) { } }
    public record TestDateTime : DateTimeValueObject { public TestDateTime(DateTime value) : base(value) { } }
    public record TestUuid : UuidValueObject { public TestUuid(Guid value) : base(value) { } }
    public record TestByte : ByteValueObject { public TestByte(byte[] value) : base(value) { } }

    public record TestCreateThrows : IntValueObject
    {
        public TestCreateThrows(int value) : base(value) { }

        public static new TestCreateThrows Create(int value) => throw new InvalidOperationException("Create must not be called by the converter.");
    }

    public record TestPrivateCtor : StringValueObject
    {
        private TestPrivateCtor(string value) : base(value) { }

        public static new TestPrivateCtor Create(string value) => new(value);
    }
}
