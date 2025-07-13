using MeterOrm.Core;
using MeterOrm.Core.Common;
using MeterOrm.Core.Transport;

namespace MeterOrm.Dlms;

public class DlmsMeterContext : MeterContext
{   
    public DlmsMeterContext(ITransport transport, DlmsMeterContextParameterModel parameters) 
        : base(transport, parameters)
    {
    }
}

public class DlmsMeterContextParameterModel : IMeterContextParameterModel
{
    public ushort PhysicalAddress { get; set; }
    
    public ushort LogicalAddress { get; set; }
    
    public SecurityLevel SecurityLevel { get; set; }
    
    public Result<Unit> Validate()
    {
        throw new NotImplementedException();
    }
}

public enum SecurityLevel
{
    None = 0,
    LowLevelSecurity = 1,
    HighLevelSecurity = 2,
}