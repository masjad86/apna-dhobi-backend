using Core.Infrastructure.Abstractions.Interfaces.Cqrs;

public sealed class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, out TResponse>
    where TCommand : ICommand<TResponse>
{
    private readonly IRunner _runner;

    public CommandHandler(IRunner runner)
    {
        _runner = runner;
    }

    public async Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _runner.Run<TCommand>(command, cancellationToken);
        return result.IsSuccess ? Core.Infrastructure.Models.Results.Result<TResponse>.Success(result.Value) : throw new Exception(result.ErrorMessage);
    }
}