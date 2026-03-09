using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

namespace ApnaDhobi.Infrastructure.Handlers;

public class CommandHandler<TCommand, TResponse>(IRunner runner,
    IPerformanceMonitor monitor,
    ILogger<AbstractCommandHandler<TCommand, TResponse>>? logger = null,
    HandlerSetting? settings = null) : AbstractCommandHandler<TCommand, TResponse>(runner, monitor, logger, settings)
    where TCommand : ICommand<TResponse>
{
}