using System;
using profsysinf.Core.Events;

namespace projsysinf.Application.Events
{
    public class UserFailedLoginEvent(int userId) : IDomainEvent
    {
        public int UserId { get; } = userId;
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}