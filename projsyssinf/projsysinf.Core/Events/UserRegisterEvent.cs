using System;

namespace profsysinf.Core.Events;

public class UserRegisterEvent(int userId) : IDomainEvent
{
    public int UserId { get; } = userId;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}