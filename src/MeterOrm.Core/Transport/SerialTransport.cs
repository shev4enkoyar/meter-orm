using System.IO.Ports;
using MeterOrm.Core.Common;

namespace MeterOrm.Core.Transport;

/// <summary>
/// Transport implementation for serial communication with meters
/// </summary>
public class SerialTransport : ITransport
{
    protected SerialPort? SerialPort;
    private readonly string _portName;
    private readonly int _baudRate;
    private readonly Parity _parity;
    private readonly int _dataBits;
    private readonly StopBits _stopBits;
    private readonly int _readTimeout;
    private readonly int _writeTimeout;

    public SerialTransport(
        string portName,
        int baudRate = 9600,
        Parity parity = Parity.None,
        int dataBits = 8,
        StopBits stopBits = StopBits.One,
        int readTimeout = 1000,
        int writeTimeout = 1000)
    {
        _portName = portName ?? throw new ArgumentNullException(nameof(portName));
        _baudRate = baudRate;
        _parity = parity;
        _dataBits = dataBits;
        _stopBits = stopBits;
        _readTimeout = readTimeout;
        _writeTimeout = writeTimeout;
    }

    public virtual bool IsConnected => SerialPort?.IsOpen ?? false;

    public virtual async Task<Result<Unit>> ConnectAsync()
    {
        try
        {
            if (IsConnected)
                return Result<Unit>.Success(Unit.Value);

            SerialPort = new SerialPort
            {
                PortName = _portName,
                BaudRate = _baudRate,
                Parity = _parity,
                DataBits = _dataBits,
                StopBits = _stopBits,
                ReadTimeout = _readTimeout,
                WriteTimeout = _writeTimeout
            };

            await Task.Run(() => SerialPort.Open());
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("SERIAL_CONNECT_FAILED", $"Failed to connect to serial port {_portName}: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<Unit>> DisconnectAsync()
    {
        try
        {
            if (SerialPort?.IsOpen == true)
            {
                await Task.Run(() => SerialPort.Close());
            }
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("SERIAL_DISCONNECT_FAILED", $"Failed to disconnect from serial port: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<byte[]>> SendReceiveAsync(byte[] data)
    {
        try
        {
            if (!IsConnected)
                return Result<byte[]>.Failure(new Error("SERIAL_NOT_CONNECTED", "Serial port is not connected"));

            await Task.Run(() => SerialPort!.Write(data, 0, data.Length));
            
            // Read response
            var response = await ReceiveAsync();
            return response;
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("SERIAL_SEND_RECEIVE_FAILED", $"Failed to send/receive data: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<Unit>> SendAsync(byte[] data)
    {
        try
        {
            if (!IsConnected)
                return Result<Unit>.Failure(new Error("SERIAL_NOT_CONNECTED", "Serial port is not connected"));

            await Task.Run(() => SerialPort!.Write(data, 0, data.Length));
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error("SERIAL_SEND_FAILED", $"Failed to send data: {ex.Message}", ex));
        }
    }

    public virtual async Task<Result<byte[]>> ReceiveAsync()
    {
        try
        {
            if (!IsConnected)
                return Result<byte[]>.Failure(new Error("SERIAL_NOT_CONNECTED", "Serial port is not connected"));

            var buffer = new byte[1024];
            var bytesRead = await Task.Run(() => SerialPort!.Read(buffer, 0, buffer.Length));
            
            var result = new byte[bytesRead];
            Array.Copy(buffer, result, bytesRead);
            
            return Result<byte[]>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Failure(new Error("SERIAL_RECEIVE_FAILED", $"Failed to receive data: {ex.Message}", ex));
        }
    }

    public virtual void Dispose()
    {
        SerialPort?.Dispose();
        GC.SuppressFinalize(this);
    }
}