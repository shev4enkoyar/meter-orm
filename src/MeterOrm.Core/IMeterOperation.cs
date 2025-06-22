using MeterOrm.Core.Common;

namespace MeterOrm.Core;

/// <summary>
/// Represents a meter operation that can be executed
/// </summary>
public interface IMeterOperation
{
    /// <summary>
    /// Executes the operation
    /// </summary>
    Task<Result<Unit>> ExecuteAsync();
} 