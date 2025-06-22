namespace MeterOrm.Core.Meter;

/// <summary>
/// Base interface for all meter classes/objects
/// </summary>
public interface IMeterClass
{
    /// <summary>
    /// Gets the logical name of the meter object
    /// </summary>
    string LogicalName { get; }
} 