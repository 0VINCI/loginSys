using System;

namespace profsysinf.Core.Events
{
    public class UserLoggedOutEvent : IDomainEvent
    {
        public int UserId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public UserLoggedOutEvent(int userId)
        {
            UserId = userId;
        }
    }
}