namespace MeterOrm.Core.Transport;

public sealed class TransportBaseConfiguration
{
    public TransportBaseConfiguration(TimeSpan? connectionTimeout, TimeSpan? readTimeout, TimeSpan? writeTimeout)
    {
        ConnectionTimeout = connectionTimeout ?? TimeSpan.FromSeconds(30);
        ReadTimeout = readTimeout ?? TimeSpan.FromSeconds(30);
        WriteTimeout = writeTimeout ?? TimeSpan.FromSeconds(30);
    }
    public TimeSpan ConnectionTimeout { get; init; }
    
    public TimeSpan ReadTimeout { get; init; }
    
    public TimeSpan WriteTimeout { get; init; }
}