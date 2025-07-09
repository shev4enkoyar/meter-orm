namespace MeterOrm.Core.Common;

public readonly struct Option<T>
{
    private readonly T _value;

    public bool IsSome { get; }
    public bool IsNone => !IsSome;
    public T Value => IsSome
        ? _value
        : throw new InvalidOperationException("Option has no value.");

    private Option(T value)
    {
        IsSome = true;
        _value = value!;
    }

    public static Option<T> Some(T value)
        => value is null ? throw new ArgumentNullException(nameof(value)) : new Option<T>(value);

    public static Option<T> None => new();

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        => IsSome ? some(_value) : none();

    public void Match(Action<T> some, Action none)
    {
        if (IsSome) some(_value);
        else none();
    }

    public Option<TResult> Map<TResult>(Func<T, TResult> mapper)
        => IsSome ? Option<TResult>.Some(mapper(_value)) : Option<TResult>.None;

    public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder)
        => IsSome ? binder(_value) : Option<TResult>.None;

    public T GetValueOrDefault(T fallback) => IsSome ? _value : fallback;
    public T GetValueOrDefault(Func<T> fallback) => IsSome ? _value : fallback();
}