namespace Core.Infrastructure.Abstractions.Cqrs;

/// <summary>
/// Marker interface for a request that returns a response of type <typeparamref name="TResponse"/>.
/// Compatible with MediatR-style pipelines but framework-agnostic.
/// </summary>
public interface IQuery<TResponse>// : IRequest<Result<TResponse>>
{ }

/// <summary>
/// Marker interface for a request that returns no data (use Result).
/// </summary>
public interface IQuery<TRequest, TResponse>// : IRequest<TRequest, out TResponse>
{ }
