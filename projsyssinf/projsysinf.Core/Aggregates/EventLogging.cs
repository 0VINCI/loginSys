using profsysinf.Core.Entities;

namespace profsysinf.Core.Aggregates;

public class Log
{
    public int IdLog { get; set; }
    public int UserId { get; set; }
    public short OperationTypeId { get; set; }
    public DateTime Tmstmp { get; set; }

    public User User { get; set; }
    public DicOperationType OperationType { get; set; }
}