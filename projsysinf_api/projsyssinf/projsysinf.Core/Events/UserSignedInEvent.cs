namespace profsysinf.Core.Events;

public class UserSignedInEvent : IDomainEvent
{
    public Guid UserId { get; }

    public UserSignedInEvent(Guid userId)
    {
        UserId = userId;
    }
}