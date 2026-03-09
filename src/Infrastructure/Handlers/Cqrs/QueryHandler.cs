using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

namespace ApnaDhobi.Infrastructure.Handlers;

public class QueryHandler<TQuery, TResponse>(IRunner runner,
    IPerformanceMonitor monitor,
    ILogger<AbstractQueryHandler<TQuery, TResponse>>? logger = null,
    HandlerSetting? settings = null) : AbstractQueryHandler<TQuery, TResponse>(runner, monitor, logger, settings)
    where TQuery : ICommand<TResponse>
{
}