using ApnaDhobi.Infrastructure.Models;
using MediatR;

namespace ApnaDhobi.Infrastructure.Interfaces;

/// <summary>
/// Marker interface for a command that returns no payload.
/// </summary>
public interface ICommand : IRequest
{
}

/// <summary>
/// Marker interface for a command that returns a payload wrapped in Result.
/// </summary>
public interface ICommand<TResponse> : IRequest<TResponse>
{
}