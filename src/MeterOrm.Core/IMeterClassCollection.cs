using MeterOrm.Core.Common;

namespace MeterOrm.Core;

/// <summary>
/// Represents a collection of meter classes/objects
/// </summary>
public interface IMeterClassCollection
{
    /// <summary>
    /// Gets all objects of the specified type
    /// </summary>
    IEnumerable<T> Where<T>(Func<T, bool> predicate) where T : class, IMeterClass;

    /// <summary>
    /// Gets the first object of the specified type that matches the predicate
    /// </summary>
    Option<T> FirstOrDefault<T>(Func<T, bool> predicate) where T : class, IMeterClass;

    /// <summary>
    /// Gets all objects of the specified type
    /// </summary>
    IEnumerable<T> Select<T>() where T : class, IMeterClass;

    /// <summary>
    /// Projects each element of a sequence into a new form
    /// </summary>
    IEnumerable<TResult> Select<T, TResult>(Func<T, TResult> selector) where T : class, IMeterClass;
} 