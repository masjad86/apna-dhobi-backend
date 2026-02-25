using Core.Infrastructure.Models.Results;

namespace Core.Infrastructure.Abstractions.Interfaces.Cqrs;

public interface IRunner
{
    Task<Result> Run(ICommand command, CancellationToken ct = default);
    Task<Result<T>> Run<T>(ICommand<T> command, CancellationToken ct = default);
    Task<Result<T>> Run<T>(IQuery<T> query, CancellationToken ct = default);
}