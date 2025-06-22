using MeterOrm.Core;
using MeterOrm.Core.Common;
using MeterOrm.Dlms.Domain.Entities.Common;
using MeterOrm.Core.Transport;

namespace MeterOrm.Dlms;

/// <summary>
/// DLMS-specific meter context for working with DLMS/COSEM meters
/// </summary>
public abstract class DlmsMeterContext : MeterContext
{
    private readonly Dictionary<Type, List<DlmsClass>> _objects = new();
    private readonly List<IAttributeOperation> _pendingAttributeOperations = [];
    private readonly ITransport _transport;

    protected DlmsMeterContext(ITransport transport)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    public override IMeterClassCollection Classes => new DlmsClassCollection(this);

    /// <summary>
    /// Gets the transport used by this context
    /// </summary>
    protected ITransport Transport => _transport;

    /// <summary>
    /// Connects to the meter using the transport
    /// </summary>
    public async Task<Result<Unit>> ConnectAsync()
    {
        return await _transport.ConnectAsync();
    }

    /// <summary>
    /// Disconnects from the meter
    /// </summary>
    public async Task<Result<Unit>> DisconnectAsync()
    {
        return await _transport.DisconnectAsync();
    }

    /// <summary>
    /// Gets all objects of the specified type
    /// </summary>
    internal IEnumerable<T> GetObjects<T>() where T : DlmsClass
    {
        var type = typeof(T);
        return _objects.TryGetValue(type, out var objects) 
            ? objects.Cast<T>() 
            : [];
    }

    /// <summary>
    /// Gets all objects as DlmsClass and casts them to the requested type
    /// </summary>
    internal IEnumerable<T> GetObjectsAs<T>() where T : class, IMeterClass
    {
        if (!typeof(T).IsSubclassOf(typeof(DlmsClass)))
            return [];

        var type = typeof(T);
        return _objects.TryGetValue(type, out var objects) 
            ? objects.Cast<T>() 
            : [];
    }

    /// <summary>
    /// Adds an object to the context
    /// </summary>
    internal void AddObject<T>(T obj) where T : DlmsClass
    {
        var type = typeof(T);
        if (!_objects.ContainsKey(type))
            _objects[type] = [];
        
        _objects[type].Add(obj);
    }

    /// <summary>
    /// Adds an attribute operation to the pending operations
    /// </summary>
    internal void AddAttributeOperation(IAttributeOperation operation)
    {
        _pendingAttributeOperations.Add(operation);
    }

    /// <summary>
    /// Executes all pending attribute operations
    /// </summary>
    protected async Task<Result<Unit>> ExecuteAttributeOperationsAsync()
    {
        try
        {
            foreach (var operation in _pendingAttributeOperations)
            {
                var result = await operation.ExecuteAsync();
                if (result.IsFailure)
                    return Result<Unit>.Failure(result.Error);
            }
            
            _pendingAttributeOperations.Clear();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("ATTRIBUTE_OPERATION_ERROR", "Failed to execute attribute operations", ex));
        }
    }

    /// <summary>
    /// Override to implement the actual DLMS communication
    /// </summary>
    protected abstract Task<Result<Unit>> ExecuteDlmsOperationAsync(IDlmsOperation operation);

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _transport?.Dispose();
        }
        
        base.Dispose(disposing);
    }
}

/// <summary>
/// Collection of DLMS classes
/// </summary>
internal class DlmsClassCollection : IMeterClassCollection
{
    private readonly DlmsMeterContext _context;

    public DlmsClassCollection(DlmsMeterContext context)
    {
        _context = context;
    }

    public IEnumerable<T> Where<T>(Func<T, bool> predicate) where T : class, IMeterClass
    {
        var objects = _context.GetObjectsAs<T>();
        return objects.Where(predicate);
    }

    public Option<T> FirstOrDefault<T>(Func<T, bool> predicate) where T : class, IMeterClass
    {
        var objects = _context.GetObjectsAs<T>();
        var result = objects.FirstOrDefault(predicate);
        return result != null ? Option<T>.Some(result) : Option<T>.None;
    }

    public IEnumerable<T> Select<T>() where T : class, IMeterClass
    {
        return _context.GetObjectsAs<T>();
    }

    public IEnumerable<TResult> Select<T, TResult>(Func<T, TResult> selector) where T : class, IMeterClass
    {
        var objects = _context.GetObjectsAs<T>();
        return objects.Select(selector);
    }
}

/// <summary>
/// Interface for attribute operations
/// </summary>
internal interface IAttributeOperation
{
    Task<Result<Unit>> ExecuteAsync();
}

/// <summary>
/// Interface for DLMS operations
/// </summary>
public interface IDlmsOperation
{
    Task<Result<Unit>> ExecuteAsync();
}