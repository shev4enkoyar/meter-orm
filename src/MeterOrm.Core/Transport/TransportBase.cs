using MeterOrm.Core.Common;

namespace MeterOrm.Core.Transport;

/// <summary>
/// Base class for transport implementations
/// </summary>
public abstract class TransportBase : ITransport
{
    protected bool Disposed;

    public abstract bool IsConnected { get; }
    public abstract TimeSpan ConnectionTimeout { get; }
    public abstract TimeSpan ReadTimeout { get; }
    public abstract TimeSpan WriteTimeout { get; }

    public abstract Task<Result<Unit>> ConnectAsync();
    public abstract Task<Result<Unit>> DisconnectAsync();
    public abstract Task<Result<byte[]>> SendReceiveAsync(byte[] data);
    public abstract Task<Result<Unit>> SendAsync(byte[] data);
    public abstract Task<Result<byte[]>> ReceiveAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!Disposed && disposing)
        {
            // Cleanup managed resources
            Disposed = true;
        }
    }

    /// <summary>
    /// Validates that the transport is not disposed
    /// </summary>
    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(Disposed, GetType().Name);
    }

    /// <summary>
    /// Validates that the transport is connected
    /// </summary>
    protected void ThrowIfNotConnected()
    {
        if (!IsConnected)
            throw new InvalidOperationException("Transport is not connected");
    }
} 