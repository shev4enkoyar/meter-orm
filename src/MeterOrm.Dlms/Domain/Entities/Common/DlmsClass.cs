using MeterOrm.Core;
using MeterOrm.Core.Common;
using System.Linq.Expressions;

namespace MeterOrm.Dlms.Domain.Entities.Common;

/// <summary>
/// Base class for all DLMS/COSEM objects
/// </summary>
public abstract class DlmsClass : IMeterClass
{
    private readonly Dictionary<string, object> _attributes = new();
    private readonly Dictionary<string, Func<Task<Result<Core.Common.Unit>>>> _pendingMethods = new();

    /// <summary>
    /// Gets the logical name of the DLMS object
    /// </summary>
    public LogicalName LogicalName { get; }

    /// <summary>
    /// Gets the DLMS class ID
    /// </summary>
    public abstract ushort ClassId { get; }

    /// <summary>
    /// Gets the logical name as string
    /// </summary>
    string IMeterClass.LogicalName => LogicalName.ToString();

    protected DlmsClass(LogicalName logicalName)
    {
        LogicalName = logicalName;
    }

    /// <summary>
    /// Gets an attribute value
    /// </summary>
    protected T GetAttribute<T>(string attributeName)
    {
        return _attributes.TryGetValue(attributeName, out var value) ? (T)value : default!;
    }

    /// <summary>
    /// Sets an attribute value
    /// </summary>
    protected void SetAttribute<T>(string attributeName, T value)
    {
        _attributes[attributeName] = value!;
    }

    /// <summary>
    /// Marks an attribute for reading from the meter
    /// </summary>
    protected void MarkForReading<T>(Expression<Func<T>> attributeExpression)
    {
        // This will be implemented by the DLMS context
    }

    /// <summary>
    /// Marks an attribute for writing to the meter
    /// </summary>
    protected void MarkForWriting<T>(Expression<Func<T>> attributeExpression)
    {
        // This will be implemented by the DLMS context
    }

    /// <summary>
    /// Adds a method call to the pending operations
    /// </summary>
    protected void AddMethodCall(string methodName, Func<Task<Result<Core.Common.Unit>>> methodCall)
    {
        _pendingMethods[methodName] = methodCall;
    }

    /// <summary>
    /// Gets all pending method calls
    /// </summary>
    internal IEnumerable<Func<Task<Result<Core.Common.Unit>>>> GetPendingMethods() => _pendingMethods.Values;

    /// <summary>
    /// Clears all pending method calls
    /// </summary>
    internal void ClearPendingMethods() => _pendingMethods.Clear();
}