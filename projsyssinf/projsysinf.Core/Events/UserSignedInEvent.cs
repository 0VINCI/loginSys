using System;

namespace profsysinf.Core.Events;

public class UserSignedInEvent(int userId) : IDomainEvent
{
    public int UserId { get; } = userId;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}