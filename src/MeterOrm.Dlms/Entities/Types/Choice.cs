using MeterOrm.Core.Common;
using MeterOrm.Dlms.Enums;

namespace MeterOrm.Dlms.Entities.Types;

public class Choice
{
    public DlmsDataType DataType { get; set; }
    public object? Value { get; set; }

    public Option<T> GetValue<T>(Func<object, Option<T>>? converter = null)
    {
        switch (Value)
        {
            case null:
                return Option<T>.None;
            case T genericValue:
                return Option<T>.Some(genericValue);
            default:
                converter ??= DefaultConverter<T>();
                return converter.Invoke(Value);
        }
    }

    private static Func<object, Option<T>> DefaultConverter<T>() => value =>
    {
        try
        {
            return Option<T>.Some((T)Convert.ChangeType(value, typeof(T)));
        }
        catch (Exception)
        {
            return Option<T>.None;
        }
    };
}