using System;

namespace profsysinf.Core.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
