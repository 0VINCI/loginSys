namespace profsysinf.Core.Events;

public class UserLockedOutEvent(int userId) : IDomainEvent
{
    public int UserId { get; } = userId;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
