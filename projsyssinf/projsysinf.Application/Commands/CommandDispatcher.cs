using Microsoft.Extensions.DependencyInjection;

namespace projsysinf.Application.Commands
{
    public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await handler.HandleAsync(command);
        }
    }
}