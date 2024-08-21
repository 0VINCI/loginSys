using Microsoft.Extensions.DependencyInjection;

namespace projsysinf.Application.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"Handler for command '{typeof(TCommand).Name}' not found.");
            }

            await handler.HandleAsync(command);
        }
    }
}