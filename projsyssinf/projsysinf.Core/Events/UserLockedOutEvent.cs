namespace profsysinf.Core.Events;

public class UserLockedOutEvent : IDomainEvent
{
    public int UserId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public UserLockedOutEvent(int userId)
    {
        UserId = userId;
    }
}
