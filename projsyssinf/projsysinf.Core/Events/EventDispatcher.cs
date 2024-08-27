using Microsoft.Extensions.DependencyInjection;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(TEvent eventItem) where TEvent : IDomainEvent
    {
        var handler = _serviceProvider.GetService<IEventHandler<TEvent>>();
        if (handler != null)
        {
            await handler.HandleAsync(eventItem);
        }
    }
}
