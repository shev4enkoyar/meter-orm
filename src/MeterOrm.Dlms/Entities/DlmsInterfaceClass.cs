using MeterOrm.Dlms.Entities.Types;
using MeterOrm.Dlms.Enums;

namespace MeterOrm.Dlms.Entities;

public class DlmsInterfaceClass
{
    public virtual DlmsClassId ClassId { get; }
    
    public LogicalName LogicalName { get; set; }
}

public class DlmsData : DlmsInterfaceClass
{
    public override DlmsClassId ClassId => DlmsClassId.Data;
    
    public DlmsAttribute<Choice> Value { get; set; }
}

public class DlmsAttribute<T>
{
    public T Content { get; set; }
}