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
        get => GetAttribute<double>(2);
        set => SetAttribute(2, value);
    }

    /// <summary>
    /// Attribute 3: ScalerUnit - The scaler and unit of the register
    /// </summary>
    public ScalerUnit ScalerUnit
    {
        get => GetAttribute<ScalerUnit>(3);
        set => SetAttribute(3, value);
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
            return Result<Unit>.Success(Unit.Value);
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