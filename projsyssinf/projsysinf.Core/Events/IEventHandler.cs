using System.Threading.Tasks;
using profsysinf.Core.Events;

namespace projsysinf.Application.Events
{
    public interface IEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent eventItem);
    }
}