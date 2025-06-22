using MeterOrm.Core.Meter;

namespace MeterOrm.Core.Accessor;

/// <summary>
/// Implementation of meter context accessor
/// </summary>
public class MeterContextAccessor : IMeterContextAccessor
{
    private readonly Dictionary<string, MeterContext> _contexts = new();
    private readonly Dictionary<string, IMeterContextAccessorOption> _contextOptions = new();

    public IMeterContextAccessor CreateAccessor<TContext, TOption>(
        string contextName, 
        TContext context, 
        TOption options)
        where TContext : MeterContext
        where TOption : IMeterContextAccessorOption
    {
        if (string.IsNullOrWhiteSpace(contextName))
            throw new ArgumentException("Context name cannot be null or empty", nameof(contextName));

        ArgumentNullException.ThrowIfNull(context);

        ArgumentNullException.ThrowIfNull(options);
        
        _contexts[contextName] = context;
        _contextOptions[contextName] = options;

        return this;
    }

    public TContext GetContext<TContext>(string contextName) where TContext : MeterContext
    {
        if (string.IsNullOrWhiteSpace(contextName))
            throw new ArgumentException("Context name cannot be null or empty", nameof(contextName));

        if (!_contexts.TryGetValue(contextName, out var context))
            throw new InvalidOperationException($"Context '{contextName}' not found");

        if (context is not TContext typedContext)
            throw new InvalidOperationException($"Context '{contextName}' is not of type {typeof(TContext).Name}");

        return typedContext;
    }
} 