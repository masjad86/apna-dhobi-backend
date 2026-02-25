using Core.Infrastructure.Models.Results;

namespace Core.Infrastructure.Abstractions.Interfaces.Cqrs;

/// <summary>
/// Marker interface for a command that returns no payload.
/// </summary>
public interface ICommand// : IRequest<Result>
{
}

/// <summary>
/// Marker interface for a command that returns a payload wrapped in Result.
/// </summary>
public interface ICommand<TResponse>// : IRequest<Result<TResponse>>
{
}

/// <summary>
/// Marker interface for a query (read-only).
/// </summary>
public interface IQuery<TResponse>// : IRequest<Result<TResponse>>
{
}