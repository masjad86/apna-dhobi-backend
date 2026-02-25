using MediatR;

namespace ApnaDhobi.Infrastructure.Interfaces;

/// <summary>
/// Marker interface for a request that returns a response of type <typeparamref name="TResponse"/>.
/// Compatible with MediatR-style pipelines but framework-agnostic.
/// </summary>
public interface IQuery<TResponse> : IRequest<TResponse>
{ }

/// <summary>
/// Marker interface for a request that returns no data (use Result).
/// </summary>
public interface IQuery : IRequest
{ }
