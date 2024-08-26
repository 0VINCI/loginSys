using profsysinf.Core.Aggregates;

namespace profsysinf.Core.Entities;

public class DicOperationType
{
    public short IdOperationType { get; set; }
    public string OperationTypeName { get; set; }
    public ICollection<Log> Logs { get; set; }
}
