using System;
using System.Collections.Generic;
using profsysinf.Core.Events;

namespace projsysinf.Application.Services
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent eventItem) where TEvent : IDomainEvent;
    }
    
}