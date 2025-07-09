namespace MeterOrm.Core.Common;

/// <summary>
/// Represents a unit value (void) for operations that don't return a value
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
    /// <summary>
    /// Represents the default (and only) value of <see cref="Unit"/>.
    /// </summary>
    public static readonly Unit Value = new();

    public bool Equals(Unit other) => true;

    public override bool Equals(object? obj) => obj is Unit;

    public override int GetHashCode() => 0;

    public static bool operator ==(Unit left, Unit right) => left.Equals(right);

    public static bool operator !=(Unit left, Unit right) => !left.Equals(right);

    public override string ToString() => string.Empty;
}