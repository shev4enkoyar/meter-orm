namespace MeterOrm.Core.Common;

/// <summary>
/// Represents a unit value (void) for operations that don't return a value
/// </summary>
public readonly struct Unit
{
    public static readonly Unit Value = new();
    
    public Unit() { }

    public override string ToString()
    {
        return string.Empty;
    }
}