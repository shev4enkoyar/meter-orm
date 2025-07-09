namespace MeterOrm.Core.Common;

public interface IError
{
    string Code { get; }
    string Message { get; }
    Exception? Exception { get; }
}