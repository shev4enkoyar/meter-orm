using MeterOrm.Core.Accessor;

namespace MeterOrm.Dlms.Accessor;

public class DlmsMeterContextAccessorOptions : IDlmsMeterContextAccessorOption
{
    private DlmsMeterContextAccessorOptions()
    {
        
    }
    
    public static IDlmsMeterContextAccessorOption Default => new DlmsMeterContextAccessorOptions();

    public static IDlmsMeterContextAccessorOption Create() => new DlmsMeterContextAccessorOptions();
    
    public bool UseKeepAlive { get; set; }

    /// <summary>
    /// Keep-alive period
    /// </summary>
    public TimeSpan KeepAlivePeriod { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Fluent API for configuring keep-alive
    /// </summary>
    public IDlmsMeterContextAccessorOption WithKeepAlive(bool useKeepAlive)
    {
        UseKeepAlive = useKeepAlive;
        return this;
    }

    /// <summary>
    /// Fluent API for setting keep-alive period
    /// </summary>
    public IDlmsMeterContextAccessorOption SetKeepAlivePeriod(TimeSpan period)
    {
        KeepAlivePeriod = period;
        return this;
    }
}

public interface IDlmsMeterContextAccessorOption : IMeterContextAccessorOption
{
    IDlmsMeterContextAccessorOption WithKeepAlive(bool useKeepAlive);
    IDlmsMeterContextAccessorOption SetKeepAlivePeriod(TimeSpan period);
}