

using Core.Infrastructure.Abstractions.Interfaces.Cqrs;
using Core.Infrastructure.Models.Results;

namespace Core.Infrastructure.Handlers;

public class Runner : IRunner
{
    private readonly IMediator _mediator;

    public Runner(IMediator mediator) => _mediator = mediator;

    public Task<Result> Run(ICommand command, CancellationToken ct = default) =>
        _mediator.Send(command, ct);

    public Task<Result<T>> Run<T>(ICommand<T> command, CancellationToken ct = default) =>
        _mediator.Send(command, ct);

    public Task<Result<T>> Run<T>(IQuery<T> query, CancellationToken ct = default) =>
        _mediator.Send(query, ct);
}