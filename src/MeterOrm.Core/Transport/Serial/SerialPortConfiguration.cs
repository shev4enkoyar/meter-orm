using System.IO.Ports;

namespace MeterOrm.Core.Transport.Serial;

/// <summary>
/// Configuration for serial port transport
/// </summary>
public sealed class SerialPortConfiguration
{
    public string PortName { get; init; } = string.Empty;
    public int BaudRate { get; init; } = 9600;
    public Parity Parity { get; init; } = Parity.None;
    public int DataBits { get; init; } = 8;
    public StopBits StopBits { get; init; } = StopBits.One;
    public Handshake Handshake { get; init; } = Handshake.None;
}