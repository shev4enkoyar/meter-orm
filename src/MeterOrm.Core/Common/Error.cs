namespace MeterOrm.Core.Common;

public sealed class Error
{
    public string Code { get; }
    public string Message { get; }
    public Exception? Exception { get; }

    public Error(string code, string message, Exception? exception = null)
    {
        Code = code;
        Message = message;
        Exception = exception;
    }

    public override string ToString() => $"{Code}: {Message}";
}