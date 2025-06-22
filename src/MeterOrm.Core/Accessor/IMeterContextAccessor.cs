namespace MeterOrm.Core.Accessor;

/// <summary>
/// Interface for accessing and managing meter contexts
/// </summary>
public interface IMeterContextAccessor
{
    /// <summary>
    /// Creates a new meter context accessor
    /// </summary>
    IMeterContextAccessor CreateAccessor<TContext, TOption>(
        string contextName,
        TContext context,
        TOption options)
        where TContext : MeterContext
        where TOption : IMeterContextAccessorOption;

    /// <summary>
    /// Gets a meter context by name
    /// </summary>
    TContext GetContext<TContext>(string contextName) where TContext : MeterContext;
}