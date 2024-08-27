using System;
using profsysinf.Core.Events;

namespace projsysinf.Application.Events
{
    public class UserFailedLoginEvent : IDomainEvent
    {
        public int UserId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public UserFailedLoginEvent(int userId)
        {
            UserId = userId;
        }
    }
}