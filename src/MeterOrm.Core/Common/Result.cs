namespace MeterOrm.Core.Common;

public readonly struct Result<T>
{
    private readonly T _value;
    private readonly Error? _error;

    public bool IsSuccess { get; }
    
    public bool IsFailure => !IsSuccess;
    
    public T Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("No value. Result is Failure.");
    
    public Error Error => IsFailure
        ? _error!.Value
        : throw new InvalidOperationException("No error. Result is Success.");

    private Result(T value)
    {
        IsSuccess = true;
        _value = value!;
        _error = null;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        _value = default!;
        _error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

    public static Result<T> Try(Func<T> func, Func<Exception, Error>? onException = null)
    {
        try { return Success(func()); }
        catch (Exception ex)
        {
            var err = onException?.Invoke(ex) ?? new Error("EXCEPTION", ex.Message, ex);
            return Failure(err);
        }
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        => IsSuccess ? onSuccess(_value) : onFailure(_error!.Value);

    public void Match(Action<T> onSuccess, Action<Error> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(_value);
        }
        else
        {
            onFailure(_error!.Value);
        }
    }

    public Result<TResult> Map<TResult>(Func<T, TResult> mapper)
        => IsSuccess ? Result<TResult>.Success(mapper(_value)) : Result<TResult>.Failure(_error!.Value);

    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> binder)
        => IsSuccess ? binder(_value) : Result<TResult>.Failure(_error!.Value);
}