using System.Text.RegularExpressions;

namespace MeterOrm.Dlms.Domain.Entities.Common;

/// <summary>
/// Represents a DLMS logical name (OBIS code)
/// </summary>
public readonly struct LogicalName : IEquatable<LogicalName>
{
    private static readonly Regex LogicalNamePattern = new(@"^\d+\.\d+\.\d+\.\d+\.\d+\.\d+$", RegexOptions.Compiled);
    
    public byte A { get; }
    public byte B { get; }
    public byte C { get; }
    public byte D { get; }
    public byte E { get; }
    public byte F { get; }

    public LogicalName(byte a, byte b, byte c, byte d, byte e, byte f)
    {
        A = a;
        B = b;
        C = c;
        D = d;
        E = e;
        F = f;
    }

    /// <summary>
    /// Creates a LogicalName from a string representation
    /// </summary>
    public static LogicalName Parse(string logicalName)
    {
        if (string.IsNullOrWhiteSpace(logicalName))
            throw new ArgumentException("Logical name cannot be null or empty", nameof(logicalName));

        if (!LogicalNamePattern.IsMatch(logicalName))
            throw new ArgumentException($"Invalid logical name format: {logicalName}", nameof(logicalName));

        var parts = logicalName.Split('.');
        return new LogicalName(
            byte.Parse(parts[0]),
            byte.Parse(parts[1]),
            byte.Parse(parts[2]),
            byte.Parse(parts[3]),
            byte.Parse(parts[4]),
            byte.Parse(parts[5])
        );
    }

    /// <summary>
    /// Tries to parse a logical name from string
    /// </summary>
    public static bool TryParse(string logicalName, out LogicalName result)
    {
        try
        {
            result = Parse(logicalName);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Checks if the logical name starts with the specified prefix
    /// </summary>
    public bool StartsWith(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
            return true;

        var current = ToString();
        return current.StartsWith(prefix);
    }

    public override string ToString() => $"{A}.{B}.{C}.{D}.{E}.{F}";

    public bool Equals(LogicalName other) => 
        A == other.A && B == other.B && C == other.C && 
        D == other.D && E == other.E && F == other.F;

    public override bool Equals(object? obj) => obj is LogicalName other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(A, B, C, D, E, F);

    public static bool operator ==(LogicalName left, LogicalName right) => left.Equals(right);
    public static bool operator !=(LogicalName left, LogicalName right) => !left.Equals(right);

    public static implicit operator string(LogicalName logicalName) => logicalName.ToString();
}