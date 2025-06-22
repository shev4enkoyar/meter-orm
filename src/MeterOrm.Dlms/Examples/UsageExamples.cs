using MeterOrm.Dlms.Domain.Entities;
using MeterOrm.Core.Accessor;
using MeterOrm.Core.Transport;
using MeterOrm.Core.Transport.Tcp;
using MeterOrm.Dlms.Accessor;

namespace MeterOrm.Dlms.Examples;

/// <summary>
/// Examples of how to use MeterOrm with DLMS meters
/// </summary>
public static class UsageExamples
{
    /// <summary>
    /// Example: Creating meter context with custom transport
    /// </summary>
    public static void CreateMeterContextWithCustomTransportExample()
    {
        IMeterContextAccessor meterContextAccessor = new MeterContextAccessor();
        
        // Using custom transport
        var transport = new TcpIpTransport(
            host: "192.168.1.100", 
            port: 4059
        );

        meterContextAccessor.CreateAccessor(
            "UserContextName",
            new DlmsSpecificMeterContext(transport),
            DlmsMeterContextAccessorOptions.Default);
    }

    /// <summary>
    /// Example: Getting a meter context and connecting
    /// </summary>
    public static async Task GetMeterContextAndConnectExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        using (var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName"))
        {
            // Connect to the meter
            var connectResult = await meterContext.ConnectAsync();
            if (connectResult.IsFailure)
            {
                Console.WriteLine($"Failed to connect: {connectResult.Error.Message}");
                return;
            }

            // Work with the meter context
            meterContext.Initialize();
            
            // Disconnect when done
            await meterContext.DisconnectAsync();
        }
    }

    /// <summary>
    /// Example: Calling a method on a meter object
    /// </summary>
    public static async Task CallMethodExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        using (var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName"))
        {
            await meterContext.ConnectAsync();
            meterContext.Initialize();
            
            var register = meterContext
                .Classes
                .FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "0.0.100.0.10.255");

            if (register.IsSome)
            {
                register.Value.Reset(); // Plan the reset method call
                await meterContext.ExecuteChangesAsync(); // Execute the transaction
            }
            
            await meterContext.DisconnectAsync();
        }
    }

    /// <summary>
    /// Example: Reading data from a meter
    /// </summary>
    public static async Task ReadDataExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        using (var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName"))
        {
            await meterContext.ConnectAsync();
            meterContext.Initialize();
            
            var register = meterContext
                .Classes
                .FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "1.0.1.8.0.255");

            if (register.IsSome)
            {
                // In a real implementation, this would trigger reading the Value and ScalerUnit attributes
                Console.WriteLine($"Register value: {register.Value.Value}");
                Console.WriteLine($"Scaler unit: {register.Value.ScalerUnit}");
            }
            
            await meterContext.DisconnectAsync();
        }
    }

    /// <summary>
    /// Example: Reading multiple registers
    /// </summary>
    public static async Task ReadMultipleRegistersExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        using (var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName"))
        {
            await meterContext.ConnectAsync();
            meterContext.Initialize();
            
            var registers = meterContext
                .Classes
                .Where<DlmsClassRegister>(x => x.LogicalName.StartsWith("1.0.1."))
                .ToList();

            foreach (var register in registers)
            {
                Console.WriteLine($"Register {register.LogicalName}: {register.Value}");
            }
            
            await meterContext.DisconnectAsync();
        }
    }

    /// <summary>
    /// Example: Updating data on a meter
    /// </summary>
    public static async Task UpdateDataExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        using (var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName"))
        {
            await meterContext.ConnectAsync();
            meterContext.Initialize();
            
            var register = meterContext
                .Classes
                .FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "0.0.100.0.10.255");

            if (register.IsSome)
            {
                register.Value.Value = 10; // Update the value
                await meterContext.SaveChangesAsync(); // Apply changes to the meter
            }
            
            await meterContext.DisconnectAsync();
        }
    }

    /// <summary>
    /// Example: Reading without using statement
    /// </summary>
    public static async Task ReadWithoutUsingExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName");
        await meterContext.ConnectAsync();
        meterContext.Initialize();
        
        var register = meterContext
            .Classes
            .FirstOrDefault<DlmsClassRegister>(x => x.LogicalName == "1.0.1.8.0.255");

        if (register.IsSome)
        {
            Console.WriteLine(register.Value.Value);
        }

        await meterContext.DisconnectAsync();
        meterContext.Dispose(); // Manually dispose
    }

    /// <summary>
    /// Example: Custom object mapping
    /// </summary>
    public static async Task CustomObjectMappingExample()
    {
        var meterContextAccessor = new MeterContextAccessor();
        
        using (var meterContext = meterContextAccessor.GetContext<DlmsSpecificMeterContext>("UserContextName"))
        {
            await meterContext.ConnectAsync();
            meterContext.Initialize();
            
            var energyInfos = meterContext
                .Classes
                .Select<DlmsClassRegister, EnergyInfo>(x => new EnergyInfo
                {
                    Name = x.LogicalName,
                    Value = x.Value
                })
                .ToList();
            
            await meterContext.DisconnectAsync();
        }
    }
}

/// <summary>
/// Custom energy info class for mapping
/// </summary>
public class EnergyInfo
{
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
} 