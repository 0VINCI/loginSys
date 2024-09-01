using System.Threading.Tasks;
using profsysinf.Core.Events;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly IEventDispatcher _eventDispatcher;

        public LogoutCommandHandler(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(LogoutCommand command)
        {
            var logoutEvent = new UserLoggedOutEvent(command.UserId);
            await _eventDispatcher.DispatchAsync(logoutEvent);
        }
    }
}