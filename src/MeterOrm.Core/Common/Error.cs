namespace MeterOrm.Core.Common;

public readonly record struct Error(string Code, string Message, Exception? Exception = null) : IError
{
    public override string ToString() => $"{Code}: {Message}";
}