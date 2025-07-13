using MeterOrm.Core.Common;
using MeterOrm.Core.Transport;

namespace MeterOrm.Core;

public abstract class MeterContext : IDisposable
{
    private readonly ITransport _transport;
    private readonly IMeterContextParameterModel _parameter;

    protected MeterContext(ITransport transport, IMeterContextParameterModel parameter)
    {
        _transport = transport;
        _parameter = parameter;
    }
    
    protected ITransport Transport => _transport;
    
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

public interface IMeterContextParameterModel
{
    Result<Unit> Validate();
}