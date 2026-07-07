using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class ByteValueObjectTests
{
    [Fact]
    public void Create_FromByteArray_KeepsValue()
    {
        byte[] data = { 1, 2, 3 };
        Assert.Equal(data, ByteValueObject.Create(data).Value);
    }

    [Fact]
    public void Create_FromBase64String_Decodes()
    {
        // "hi" => bytes { 104, 105 } => base64 "aGk="
        Assert.Equal(new byte[] { 104, 105 }, ByteValueObject.Create("aGk=").Value);
    }
}
