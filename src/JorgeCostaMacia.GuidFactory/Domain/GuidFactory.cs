namespace JorgeCostaMacia.GuidFactory.Domain;

/// <summary>
/// Creates <see cref="Guid"/> values for use as identifiers.
/// </summary>
/// <remarks>
/// It produces a time-ordered <b>UUIDv7</b> (<c>Guid.CreateVersion7()</c>), whose time-based
/// prefix keeps generated keys roughly sequential — improving database index locality and
/// insert performance.
/// </remarks>
public static class GuidFactory
{
    /// <summary>
    /// Creates a new globally unique identifier: a time-ordered UUIDv7.
    /// </summary>
    /// <returns>A new <see cref="Guid"/>.</returns>
    public static Guid Create() => Guid.CreateVersion7();
}
