using Microsoft.Extensions.DependencyInjection;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task DispatchAsync<TEvent>(TEvent eventItem) where TEvent : IDomainEvent
    {
        var handler = serviceProvider.GetService<IEventHandler<TEvent>>();
        if (handler != null)
        {
            await handler.HandleAsync(eventItem);
        }
    }
}
