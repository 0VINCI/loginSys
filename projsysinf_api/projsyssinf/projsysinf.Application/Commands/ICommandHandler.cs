namespace projsysinf.Application.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task HandleAsync(TCommand command);
    }
}