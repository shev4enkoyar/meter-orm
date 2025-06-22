using System.IO.Ports;
using MeterOrm.Core.Common;

namespace MeterOrm.Core.Transport.Serial;

/// <summary>
/// Serial port transport implementation for DLMS/COSEM meters
/// </summary>
public class SerialPortTransport : TransportBase
{
    private SerialPort? _serialPort;
    private readonly SerialPortConfiguration _config;

    public SerialPortTransport(SerialPortConfiguration config, 
        TransportBaseConfiguration? transportBaseConfiguration = null) : base(transportBaseConfiguration)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        
        if (string.IsNullOrWhiteSpace(config.PortName))
            throw new ArgumentException("Port name cannot be null or empty", nameof(config));
    }

    public SerialPortTransport(string portName, int baudRate = 9600)
        : this(new SerialPortConfiguration 
        { 
            PortName = portName, 
            BaudRate = baudRate 
        })
    {
    }

    public override bool IsConnected => _serialPort?.IsOpen == true;

    public override async Task<Result<Unit>> ConnectAsync()
    {
        ThrowIfDisposed();

        try
        {
            _serialPort = new SerialPort
            {
                PortName = _config.PortName,
                BaudRate = _config.BaudRate,
                Parity = _config.Parity,
                DataBits = _config.DataBits,
                StopBits = _config.StopBits,
                Handshake = _config.Handshake,
                ReadTimeout = (int)TransportBaseConfiguration.ReadTimeout.TotalMilliseconds,
                WriteTimeout = (int)TransportBaseConfiguration.WriteTimeout.TotalMilliseconds
            };

            using var cts = new CancellationTokenSource(TransportBaseConfiguration.ConnectionTimeout);
            
            // Open the port asynchronously
            await Task.Run(() => _serialPort.Open(), cts.Token);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (OperationCanceledException)
        {
            return Result<Unit>.Failure(new Error("CONNECTION_TIMEOUT", $"Connection to {_config.PortName} timed out"));
        }
        catch (UnauthorizedAccessException)
        {
            return Result<Unit>.Failure(new Error("ACCESS_DENIED", $"Access denied to serial port {_config.PortName}"));
        }
        catch (ArgumentException)
        {
            return Result<Unit>.Failure(new Error("INVALID_PORT", $"Invalid serial port name: {_config.PortName}"));
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("CONNECTION_ERROR", $"Failed to connect to {_config.PortName}", ex));
        }
    }

    public override async Task<Result<Unit>> DisconnectAsync()
    {
        try
        {
            if (_serialPort?.IsOpen == true)
            {
                await Task.Run(() => _serialPort.Close());
            }

            _serialPort?.Dispose();
            _serialPort = null;

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
            await Task.Run(() => _serialPort!.Write(data, 0, data.Length));
            
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
            var bytesRead = await Task.Run(() => _serialPort!.Read(buffer, 0, buffer.Length));
            
            if (bytesRead == 0)
                return Result<byte[]>.Failure(new Error("NO_DATA", "No data received from serial port"));

            var result = new byte[bytesRead];
            Array.Copy(buffer, result, bytesRead);
            
            return Result<byte[]>.Success(result);
        }
        catch (TimeoutException)
        {
            return Result<byte[]>.Failure(new Error("READ_TIMEOUT", "Read operation timed out"));
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("RECEIVE_ERROR", "Failed to receive data", ex));
        }
    }

    /// <summary>
    /// Gets available serial ports
    /// </summary>
    public static string[] GetAvailablePorts()
    {
        return SerialPort.GetPortNames();
    }

    protected override void Dispose(bool disposing)
    {
        if (!Disposed && disposing)
        {
            _serialPort?.Dispose();
        }
        
        base.Dispose(disposing);
    }
} 