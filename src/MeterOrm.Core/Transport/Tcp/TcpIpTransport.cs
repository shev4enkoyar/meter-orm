using System.Net.Sockets;
using MeterOrm.Core.Common;

namespace MeterOrm.Core.Transport.Tcp;

/// <summary>
/// TCP/IP transport implementation for DLMS/COSEM meters
/// </summary>
public sealed class TcpIpTransport : TransportBase
{
    private TcpClient? _tcpClient;
    private NetworkStream? _networkStream;
    private readonly string _host;
    private readonly int _port;

    public TcpIpTransport(string host, int port = 443, TransportBaseConfiguration? transportBaseConfiguration = null) 
        : base(transportBaseConfiguration)
    {
        _host = host ?? throw new ArgumentNullException(nameof(host));
        _port = port;
    }

    public override bool IsConnected => _tcpClient?.Connected == true;

    public override async Task<Result<Unit>> ConnectAsync()
    {
        ThrowIfDisposed();

        try
        {
            _tcpClient = new TcpClient();
            
            using var cts = new CancellationTokenSource(TransportBaseConfiguration.ConnectionTimeout);
            await _tcpClient.ConnectAsync(_host, _port, cts.Token);
            
            _networkStream = _tcpClient.GetStream();
            _networkStream.ReadTimeout = (int)TransportBaseConfiguration.ReadTimeout.TotalMilliseconds;
            _networkStream.WriteTimeout = (int)TransportBaseConfiguration.WriteTimeout.TotalMilliseconds;

            return Result<Unit>.Success(Unit.Value);
        }
        catch (OperationCanceledException)
        {
            return Result<Unit>.Failure(new Error("CONNECTION_TIMEOUT", $"Connection to {_host}:{_port} timed out"));
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("CONNECTION_ERROR", $"Failed to connect to {_host}:{_port}", ex));
        }
    }

    public override async Task<Result<Unit>> DisconnectAsync()
    {
        try
        {
            _networkStream?.Close();
            _tcpClient?.Close();

            if (_networkStream != null)
            {
                await _networkStream.DisposeAsync();
            }
            _tcpClient?.Dispose();
            
            _networkStream = null;
            _tcpClient = null;

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("DISCONNECT_ERROR", "Failed to disconnect", ex));
        }
    }

    public override async Task<Result<byte[]>> SendReceiveAsync(byte[] data)
    {
        ThrowIfDisposed();
        ThrowIfNotConnected();

        try
        {
            // Send data
            var sendResult = await SendAsync(data);
            if (sendResult.IsFailure)
                return Result<byte[]>.Failure(sendResult.Error);

            // Receive response
            return await ReceiveAsync();
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("SEND_RECEIVE_ERROR", "Failed to send/receive data", ex));
        }
    }

    public override async Task<Result<Unit>> SendAsync(byte[] data)
    {
        ThrowIfDisposed();
        ThrowIfNotConnected();

        if (data.Length == 0)
            return Result<Unit>.Failure(new Error("INVALID_DATA", "Data cannot be null or empty"));

        try
        {
            await _networkStream!.WriteAsync(data);
            await _networkStream.FlushAsync();
            
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("SEND_ERROR", "Failed to send data", ex));
        }
    }

    public override async Task<Result<byte[]>> ReceiveAsync()
    {
        ThrowIfDisposed();
        ThrowIfNotConnected();

        try
        {
            var buffer = new byte[4096];
            var bytesRead = await _networkStream!.ReadAsync(buffer);
            
            if (bytesRead == 0)
                return Result<byte[]>.Failure(new Error("CONNECTION_CLOSED", "Connection was closed by the remote host"));

            var result = new byte[bytesRead];
            Array.Copy(buffer, result, bytesRead);
            
            return Result<byte[]>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("RECEIVE_ERROR", "Failed to receive data", ex));
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (!Disposed && disposing)
        {
            _networkStream?.Dispose();
            _tcpClient?.Dispose();
        }
        
        base.Dispose(disposing);
    }
} 