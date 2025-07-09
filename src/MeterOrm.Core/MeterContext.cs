using MeterOrm.Core.Common;
using MeterOrm.Core.Transport;

namespace MeterOrm.Core;

public abstract class MeterContext : IDisposable
{
    private readonly ITransport _transport;
    
    protected MeterContext(ITransport transport)
    {
        _transport = transport;
    }
    
    protected ITransport Transport => _transport;
    
    public async Task<Result<Unit>> SendRaw(byte[] data)
    {
        return await Transport.SendAsync(data);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _transport.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}