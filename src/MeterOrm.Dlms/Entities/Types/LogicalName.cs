namespace MeterOrm.Dlms.Entities.Types;

/// <summary>
/// Represents the DLMS Logical Name (OBIS code) type, consisting of 6 octets.
/// </summary>
public class LogicalName
{
    private readonly byte[] _value = new byte[6];

    /// <summary>
    /// Initializes a new instance of the <see cref="LogicalName"/> class from a byte array.
    /// </summary>
    /// <param name="value">A byte array containing exactly 6 bytes representing the Logical Name.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> does not contain exactly 6 bytes.</exception>
    public LogicalName(byte[] value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        if (value.Length != 6)
            throw new ArgumentException("LogicalName must contain exactly 6 bytes.", nameof(value));
        Array.Copy(value, _value, 6);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogicalName"/> class from an OBIS code string.
    /// </summary>
    /// <param name="obis">A string representing the OBIS code, consisting of 6 parts separated by '.', '-', or ':'.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="obis"/> is null or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="obis"/> does not have 6 parts or contains invalid byte values.</exception>
    public LogicalName(string obis)
    {
        if (string.IsNullOrWhiteSpace(obis))
            throw new ArgumentNullException(nameof(obis));
        var parts = obis.Split('.', '-', ':');
        if (parts.Length != 6)
            throw new ArgumentException("OBIS code must have 6 parts separated by dots.", nameof(obis));
        for (int i = 0; i < 6; i++)
        {
            if (!byte.TryParse(parts[i], out _value[i]))
                throw new ArgumentException($"OBIS code part '{parts[i]}' is not a valid byte.", nameof(obis));
        }
    }

    /// <summary>
    /// Returns a copy of the internal byte array representing the Logical Name.
    /// </summary>
    /// <returns>A byte array of length 6.</returns>
    public byte[] GetBytes()
    {
        var result = new byte[6];
        Array.Copy(_value, result, 6);
        return result;
    }

    /// <summary>
    /// Returns the string representation of the Logical Name in OBIS code format (dot-separated).
    /// </summary>
    /// <returns>A string representing the Logical Name as an OBIS code.</returns>
    public override string ToString()
    {
        return string.Join(".", _value);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current LogicalName instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current LogicalName.</param>
    /// <returns><c>true</c> if the specified object is a LogicalName and has the same value; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is LogicalName other)
            return _value.SequenceEqual(other._value);
        return false;
    }

    /// <summary>
    /// Returns a hash code for the LogicalName.
    /// </summary>
    /// <returns>An integer hash code.</returns>
    public override int GetHashCode()
    {
        return BitConverter.ToInt32(_value, 0) ^ BitConverter.ToInt16(_value, 4);
    }
}