using profsysinf.Core.Entities;

namespace profsysinf.Core.Aggregates;

public class EventLogging
{
    public int Id { get; set; }
    public User UserId { get; set; }
    public Operation Operation { get; set; }
}