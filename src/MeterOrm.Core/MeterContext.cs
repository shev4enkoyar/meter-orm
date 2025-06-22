using MeterOrm.Core.Common;

namespace MeterOrm.Core;

/// <summary>
/// Base context for meter operations
/// </summary>
public abstract class MeterContext : IDisposable
{
    private bool _disposed;
    private readonly List<IMeterOperation> _pendingOperations = [];

    /// <summary>
    /// Gets the collection of meter classes/objects
    /// </summary>
    public abstract IMeterClassCollection Classes { get; }

    /// <summary>
    /// Adds an operation to the pending operations queue
    /// </summary>
    protected void AddOperation(IMeterOperation operation)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(MeterContext));
            
        _pendingOperations.Add(operation);
    }

    /// <summary>
    /// Executes all pending operations
    /// </summary>
    public async Task<Result<Unit>> ExecuteChangesAsync()
    {
        if (_disposed)
            return Result<Unit>.Failure(new Error("DISPOSED", "Context has been disposed"));

        try
        {
            foreach (var operation in _pendingOperations)
            {
                var result = await operation.ExecuteAsync();
                if (result.IsFailure)
                    return Result<Unit>.Failure(result.Error);
            }
            
            _pendingOperations.Clear();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("EXECUTION_ERROR", "Failed to execute operations", ex));
        }
    }

    /// <summary>
    /// Saves changes to the meter (alias for ExecuteChangesAsync)
    /// </summary>
    public Task<Result<Unit>> SaveChangesAsync() => ExecuteChangesAsync();

    /// <summary>
    /// Clears all pending operations
    /// </summary>
    public void ClearOperations()
    {
        _pendingOperations.Clear();
    }

    /// <summary>
    /// Gets the number of pending operations
    /// </summary>
    public int PendingOperationsCount => _pendingOperations.Count;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _pendingOperations.Clear();
            _disposed = true;
        }
    }
}