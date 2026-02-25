using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ApnaDhobi.Infrastructure.Handlers;

/// <summary>
/// Implements the IRunner interface using MediatR to execute commands and queries. This class serves as a central point for handling all CQRS operations, allowing for consistent logging and error handling across the application. By using MediatR, it decouples the execution of commands and queries from their handlers, promoting a clean separation of concerns and making it easier to maintain and test the application.
/// </summary>
/// <param name="mediator">Cqrs request runner.</param>
/// <param name="logger">Logger for logging operations.</param>
public sealed class Runner(IMediator mediator, ILogger<Runner> logger) : IRunner
{
    /// <inheritdoc/>
    public async Task Run(ICommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Running command: {Command}", command.GetType().Name);
        await mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Running command: {Command} and type: {Name}", command.GetType().Name, typeof(T).Name);
        return await mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc/>
    public Task Run(IQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Running query: {Query}", query.GetType().Name);
        return mediator.Send(query, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Running query: {Query} and type: {Name}", query.GetType().Name, typeof(T).Name);
        return await mediator.Send(query, cancellationToken);
    }
}