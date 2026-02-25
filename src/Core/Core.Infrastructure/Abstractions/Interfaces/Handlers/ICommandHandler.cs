namespace Core.Infrastructure.Abstractions.Interfaces.Handlers
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}