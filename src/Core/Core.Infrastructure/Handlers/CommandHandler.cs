using Core.Infrastructure.Abstractions.Interfaces.Cqrs;

public sealed class CommandHandler<TCommand, TResponse>(IRunner runner) : ICommandHandler<TCommand, out TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var result = await runner.Run<TCommand>(command, cancellationToken);
        return result.IsSuccess ? Core.Infrastructure.Models.Results.Result<TResponse>.Success(result.Value) : throw new Exception(result.ErrorMessage);
    }
}