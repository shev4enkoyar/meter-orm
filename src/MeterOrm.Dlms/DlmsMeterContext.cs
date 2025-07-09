using MeterOrm.Core;
using MeterOrm.Core.Transport;

namespace MeterOrm.Dlms;

public class DlmsMeterContext : MeterContext
{
    public DlmsMeterContext(ITransport transport) : base(transport)
    {
    }
}