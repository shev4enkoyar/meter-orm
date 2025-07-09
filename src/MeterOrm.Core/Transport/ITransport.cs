using MeterOrm.Core.Common;

namespace MeterOrm.Core.Transport;

/// <summary>
/// Interface for transport layer that handles data communication with meters
/// </summary>
public interface ITransport : IDisposable
{
    /// <summary>
    /// Gets whether the transport is connected
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Connects to the meter
    /// </summary>
    Task<Result<Unit>> ConnectAsync();

    /// <summary>
    /// Disconnects from the meter
    /// </summary>
    Task<Result<Unit>> DisconnectAsync();

    /// <summary>
    /// Sends data to the meter and receives response
    /// </summary>
    Task<Result<byte[]>> SendReceiveAsync(byte[] data);

    /// <summary>
    /// Sends data to the meter without expecting response
    /// </summary>
    Task<Result<Unit>> SendAsync(byte[] data);

    /// <summary>
    /// Receives data from the meter
    /// </summary>
    Task<Result<byte[]>> ReceiveAsync();
}