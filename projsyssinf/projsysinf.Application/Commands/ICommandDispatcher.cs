namespace projsysinf.Application.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class;
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class;
    }
}