using MeterOrm.Core.Common;
using MeterOrm.Dlms.Domain.Entities.Common;

namespace MeterOrm.Dlms.Domain.Entities;

/// <summary>
/// DLMS Register class (Class ID: 3)
/// Represents a register object that can store a single value
/// </summary>
public class DlmsClassRegister : DlmsClass
{
    public override ushort ClassId => 3;

    /// <summary>
    /// Attribute 2: Value - The current value of the register
    /// </summary>
    public double Value
    {
        get => GetAttribute<double>("Value");
        set => SetAttribute("Value", value);
    }

    /// <summary>
    /// Attribute 3: ScalerUnit - The scaler and unit of the register
    /// </summary>
    public ScalerUnit ScalerUnit
    {
        get => GetAttribute<ScalerUnit>("ScalerUnit");
        set => SetAttribute("ScalerUnit", value);
    }

    /// <summary>
    /// Attribute 4: Status - The status of the register
    /// </summary>
    public RegisterStatus Status
    {
        get => GetAttribute<RegisterStatus>("Status");
        set => SetAttribute("Status", value);
    }

    /// <summary>
    /// Attribute 5: CaptureTime - The time when the value was captured
    /// </summary>
    public DateTime CaptureTime
    {
        get => GetAttribute<DateTime>("CaptureTime");
        set => SetAttribute("CaptureTime", value);
    }

    public DlmsClassRegister(LogicalName logicalName) : base(logicalName)
    {
    }

    /// <summary>
    /// Method 1: Reset - Resets the register value to zero
    /// </summary>
    public void Reset()
    {
        AddMethodCall("Reset", async () =>
        {
            // This will be implemented by the DLMS context
            await Task.CompletedTask;
            return Result<Core.Common.Unit>.Success(Core.Common.Unit.Value);
        });
    }

    /// <summary>
    /// Method 2: Capture - Captures the current value
    /// </summary>
    public void Capture()
    {
        AddMethodCall("Capture", async () =>
        {
            // This will be implemented by the DLMS context
            await Task.CompletedTask;
            return Result<Core.Common.Unit>.Success(Core.Common.Unit.Value);
        });
    }
}

/// <summary>
/// Represents the scaler and unit of a register
/// </summary>
public readonly struct ScalerUnit
{
    public sbyte Scaler { get; }
    public ushort Unit { get; }

    public ScalerUnit(sbyte scaler, ushort unit)
    {
        Scaler = scaler;
        Unit = unit;
    }

    public override string ToString() => $"Scaler: {Scaler}, Unit: {Unit}";
}

/// <summary>
/// Represents the status of a register
/// </summary>
[Flags]
public enum RegisterStatus : byte
{
    None = 0,
    Valid = 1,
    Invalid = 2,
    Reserved = 4,
    Questionable = 8
}

public class DlmsScalerUnit : DlmsStructure
{
    public sbyte Scaler { get; set; }
    
    public Unit Unit { get; set; } 
}

public enum UnitType
{
    TimeYear = 1,
    TimeMonth = 2,
    TimeWeek = 3,
    TimeDay = 4,
    TimeHour = 5,
    TimeMinute = 6,
    TimeSecond = 7,
    Degree = 8,
    Temperature = 9,
    Currency = 10,
    Length = 11,
    Speed = 12,
}

public struct Unit
{
    public UnitType Type { get; set; }
    
    public string Quantity { get; set; }
    
    public string UnitName { get; set; }
    
    public string UnitSymbol { get; set; }
}