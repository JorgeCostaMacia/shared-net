namespace JorgeCostaMacia.GuidMySqlConverter.Infrastructure;

/// <summary>
/// Converts a <see cref="Guid"/> to and from the 16-byte big-endian (RFC 4122) layout used by
/// MySQL/MariaDB <c>BINARY(16)</c> columns, instead of .NET's SQL Server-native little-endian layout.
/// </summary>
/// <remarks>
/// <para>
/// .NET's <see cref="Guid.ToByteArray()"/> matches SQL Server's <c>uniqueidentifier</c> layout, and
/// PostgreSQL handles GUIDs natively — neither needs this. MySQL/MariaDB store GUIDs as
/// <c>BINARY(16)</c> and expect the first three fields in big-endian (string) order.
/// </para>
/// <para>
/// This type is intentionally dependency-free (no EF Core reference). For EF Core, wrap it in a
/// <c>ValueConverter&lt;Guid, byte[]&gt;</c> inside your own solution, bound to that solution's
/// EF Core version — so this package never forces an EF Core version on its consumers.
/// </para>
/// </remarks>
public static class GuidMySqlConverter
{
    /// <summary>Converts a <see cref="Guid"/> to its MySQL/MariaDB <c>BINARY(16)</c> byte layout.</summary>
    /// <param name="guid">The GUID to convert.</param>
    /// <returns>The 16-byte big-endian representation.</returns>
    public static byte[] ConvertToBytes(Guid guid)
    {
        byte[] value = guid.ToByteArray();

        Array.Reverse(value, 0, 4);
        Array.Reverse(value, 4, 2);
        Array.Reverse(value, 6, 2);

        return value;
    }

    /// <summary>Converts a MySQL/MariaDB <c>BINARY(16)</c> byte layout back to a <see cref="Guid"/>.</summary>
    /// <param name="guid">The 16-byte big-endian representation.</param>
    /// <returns>The reconstructed <see cref="Guid"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="guid"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="guid"/> is not exactly 16 bytes.</exception>
    public static Guid ConvertFromBytes(byte[] guid)
    {
        ArgumentNullException.ThrowIfNull(guid);

        if (guid.Length != 16)
        {
            throw new ArgumentException("A MySQL/MariaDB BINARY(16) GUID must be exactly 16 bytes.", nameof(guid));
        }

        byte[] value = (byte[])guid.Clone();

        Array.Reverse(value, 0, 4);
        Array.Reverse(value, 4, 2);
        Array.Reverse(value, 6, 2);

        return new Guid(value);
    }
}
