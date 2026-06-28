namespace JorgeCostaMacia.GuidFactory.Domain;

/// <summary>
/// Creates <see cref="Guid"/> values for use as identifiers.
/// </summary>
/// <remarks>
/// On .NET 9+ it produces a time-ordered <b>UUIDv7</b> (<see cref="Guid.CreateVersion7()"/>),
/// whose time-based prefix keeps generated keys roughly sequential — improving database
/// index locality and insert performance. On earlier runtimes it falls back to a random
/// <b>UUIDv4</b> (<see cref="Guid.NewGuid()"/>).
/// </remarks>
public static class GuidFactory
{
    /// <summary>
    /// Creates a new globally unique identifier: time-ordered (UUIDv7) on .NET 9+, otherwise random (UUIDv4).
    /// </summary>
    /// <returns>A new <see cref="Guid"/>.</returns>
    public static Guid Create()
    {
#if NET9_0_OR_GREATER
        return Guid.CreateVersion7();
#else
        return Guid.NewGuid();
#endif
    }
}
