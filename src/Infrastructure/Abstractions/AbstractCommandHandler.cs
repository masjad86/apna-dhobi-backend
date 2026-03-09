using MediatR;
using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace ApnaDhobi.Infrastructure.Abstractions;

public class AbstractCommandHandler<TCommand, TResponse>(
    IRunner runner, 
    IPerformanceMonitor monitor,
    ILogger<AbstractCommandHandler<TCommand, TResponse>>? logger = null,
    HandlerSetting? settings = null) : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public virtual async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (settings?.EnableMonitoring == true)
        {
            monitor.Start();
            try
            {
                return await runner.RunAsync(request, cancellationToken);
            }
            finally
            {
                monitor.Stop();
                logger?.LogInformation("Handled {CommandName} in {ElapsedMilliseconds} ms", typeof(TCommand).Name, monitor.ElapsedMilliseconds);
            }
        }
        return await runner.RunAsync(request, cancellationToken);
    }
}
