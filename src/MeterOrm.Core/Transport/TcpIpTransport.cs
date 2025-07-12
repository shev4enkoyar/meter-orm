using System.Net.Sockets;
using MeterOrm.Core.Common;

namespace MeterOrm.Core.Transport;

/// <summary>
/// Transport implementation for TCP/IP communication with meters
/// </summary>
public class TcpIpTransport : ITransport
{
    protected TcpClient? TcpClient;
    protected NetworkStream? NetworkStream;
    private readonly string _host;
    private readonly int _port;
    private readonly int _timeout;

    public TcpIpTransport(string host, int port, int timeout = 5000)
    {
        _host = host ?? throw new ArgumentNullException(nameof(host));
        _port = port;
        _timeout = timeout;
    }

    public virtual bool IsConnected => TcpClient?.Connected ?? false;

    public virtual async Task<Result<Unit>> ConnectAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (IsConnected)
                return Result<Unit>.Success(Unit.Value);

            TcpClient = new TcpClient();
            await TcpClient.ConnectAsync(_host, _port, cancellationToken);
            TcpClient.ReceiveTimeout = _timeout;
            TcpClient.SendTimeout = _timeout;
            
            NetworkStream = TcpClient.GetStream();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("TCP_CONNECT_FAILED", $"Failed to connect to {_host}:{_port}: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<Unit>> DisconnectAsync()
    {
        try
        {
            NetworkStream?.Close();
            TcpClient?.Close();
            NetworkStream = null;
            TcpClient = null;
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("TCP_DISCONNECT_FAILED", $"Failed to disconnect: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<byte[]>> SendReceiveAsync(byte[] data, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsConnected)
                return Result<byte[]>.Failure(new Error("TCP_NOT_CONNECTED", "TCP client is not connected"));

            await SendAsync(data, cancellationToken);
            return await ReceiveAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("TCP_SEND_RECEIVE_FAILED", $"Failed to send/receive data: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<Unit>> SendAsync(byte[] data, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsConnected)
                return Result<Unit>.Failure(new Error("TCP_NOT_CONNECTED", "TCP client is not connected"));

            await NetworkStream!.WriteAsync(data, 0, data.Length, cancellationToken);
            await NetworkStream.FlushAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("TCP_SEND_FAILED", $"Failed to send data: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<byte[]>> ReceiveAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (!IsConnected)
                return Result<byte[]>.Failure(new Error("TCP_NOT_CONNECTED", "TCP client is not connected"));

            var buffer = new byte[4096];
            var bytesRead = await NetworkStream!.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
            
            if (bytesRead == 0)
                return Result<byte[]>.Failure(new Error("TCP_CONNECTION_CLOSED", "Connection closed by remote host"));

            var result = new byte[bytesRead];
            Array.Copy(buffer, result, bytesRead);
            
            return Result<byte[]>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("TCP_RECEIVE_FAILED", $"Failed to receive data: {ex.Message}", ex));
        }
    }

    public virtual void Dispose()
    {
        NetworkStream?.Dispose();
        TcpClient?.Dispose();
        GC.SuppressFinalize(this);
    }
}